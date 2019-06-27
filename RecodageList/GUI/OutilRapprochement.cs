using System;
using System.Collections.Generic;
using System.Configuration;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using RecodageList.DAL;
using Converter;
using DbAccess;
using System.IO;
using RecodageList.BLL;
using RecodageList.GUI;

namespace RecodageList
{
    public partial class Form1 : Form
    {
        Fonc fonc = new Fonc();

        
        CorrespondanceDAL CorrObject = new CorrespondanceDAL();
        ReferentielDAL RefObject = new ReferentielDAL();
        GUIFonction GUI = new GUIFonction();
        ActionRecodage ActionRecodage = new ActionRecodage();
        ActionRecodageMODULE_GLOBAL ActionRecodageMODULE_GLOBAL = new ActionRecodageMODULE_GLOBAL();



        /// <summary>
        /// Est appelé au lancement de l'appli. 
        /// Permet par défaut de charger les composants. 
        /// Pour ma part j'ai rajouté les tooltips (infobulle) 
        /// ainsi que mes variables associées à mes Thread.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            GUI.InitToolTip(this);
        }

        public void InitDatagridReferentiel()
        {
            var source = new BindingSource();
            source.DataSource = VariablePartage.TableReferentiel;
            dataGridView_ref.DataSource = source;
            GUI.InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
            GUI.InitColorReferentiel(dataGridView_ref);
        }

        /// <summary>
        /// Affecte le nouveau code et le nouveau libellé referentiel dans la table de correspondance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_save_Click(object sender, EventArgs e)
        {
            ActionRecodage.AffecteCode(dataGridView_saisie, dataGridView_ref, this, (string)this.comboBox_filtre.SelectedValue);
        }

        /// <summary>
        /// Bouton filtre, permet de filtrer la table de correspondance par module
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_filter_Click(object sender, EventArgs e)
        {
            ActionRecodage.FilterModule(dataGridView_saisie, dataGridView_ref, this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //ChargementTableEnMemoire(Init.CheminBaseClient);
            //ActionRecodage.FilterModule(dataGridView_saisie, dataGridView_ref, this);
            Thread avancement_module = new Thread(new ParameterizedThreadStart(GUI.Avancement_rapprochement_thread));
            avancement_module.IsBackground = true;
            avancement_module.Start(this);
            Thread avancement_global = new Thread(new ParameterizedThreadStart(GUI.Avancement_rapprochement_global_thread));
            avancement_global.IsBackground = true;
            avancement_global.Start(this);
        }

        /// <summary>
        /// Est appelé lors que l'utilisateur clique sur une cellule nouveau code du tableau saisie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void dataGridView_saisie_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ActionRecodage.SaisieCellClick(dataGridView_saisie, dataGridView_ref, this, (string)this.comboBox_filtre.SelectedValue);

            //// Si la cellule quittée est nouveau_code ou libellé_nouveau_code
            //if (dataGridView_saisie.CurrentCell.ColumnIndex == 4 || dataGridView_saisie.CurrentCell.ColumnIndex == 5)
            //{
            //    // Si la cellule active est le libellé et que le code est renseigné, alors on passe celle-ci en edition
            //    if (dataGridView_saisie.CurrentCell == dataGridView_saisie.Rows[e.RowIndex].Cells[5]
            //        && dataGridView_saisie.Rows[e.RowIndex].Cells[4].Value != null
            //        && dataGridView_saisie.Rows[e.RowIndex].Cells[4].Value.ToString().Trim() != ""
            //        && (dataGridView_saisie.Rows[e.RowIndex].Cells[5] == null || dataGridView_saisie.Rows[e.RowIndex].Cells[5].Value.ToString().Trim() == ""))
            //    {
            //        dataGridView_saisie.ReadOnly = false;
            //        dataGridView_saisie.EditMode = DataGridViewEditMode.EditProgrammatically;
            //        dataGridView_saisie.BeginEdit(true);
            //    }
            //    // Si le libellé de la cellule quittée est null ou vide et que le nouveau_code de la cellule quittée est renseigné alors message d'erreur et reselection du libellé.
            //    else if ((dataGridView_saisie.Rows[e.RowIndex].Cells[5].Value == null || dataGridView_saisie.Rows[e.RowIndex].Cells[5].Value.ToString().Trim() == ""))
            //    {
            //        if ((dataGridView_saisie.Rows[e.RowIndex].Cells[4].Value != null && dataGridView_saisie.Rows[e.RowIndex].Cells[4].Value.ToString().Trim() != ""))
            //        {
            //            MessageBox.Show("Code et Libellé obligatoire. \r\ndataGridView_saisie.Rows[e.RowIndex].Cells[4].Value : " + dataGridView_saisie.Rows[e.RowIndex].Cells[4].Value + "\r\n(string)dataGridView_saisie.Rows[e.RowIndex].Cells[4].Value :" + (string)dataGridView_saisie.Rows[e.RowIndex].Cells[4].Value);
            //            dataGridView_saisie.CellClick -= dataGridView_saisie_CellClick;
            //            dataGridView_saisie.CurrentCell = dataGridView_saisie.Rows[e.RowIndex].Cells[5];
            //            dataGridView_saisie.CellClick += dataGridView_saisie_CellClick;
            //            dataGridView_saisie.ReadOnly = false;
            //            dataGridView_saisie.EditMode = DataGridViewEditMode.EditProgrammatically;
            //            dataGridView_saisie.BeginEdit(true);
            //        }
            //    }
            //}
        }

        

        /// <summary>
        /// Est appelé lors de l'appuie sur le bouton Affichage Creation
        /// Permet d'activer le module de creation de code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_afficherCreationCode_Click(object sender, EventArgs e)
        {
            //GUI.AfficherCreationCode(this);
        }

        /// <summary>
        /// Est appelé lors de l'appuie sur le bouton "creer code" 
        /// Permet de remplir les champs nouveau_code, libellé_nouveau_code ainsi que Nouveau_code_inactif en fonction 
        /// des valeurs rentrées dans les différents text_box et checkbox 
        /// (prérempli par les valeurs ancien_code, libelle_ancien_code)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_creercode_Click(object sender, EventArgs e)
        {
            //ActionRecodage.CreerCodeInterfaceAvancee(dataGridView_saisie, dataGridView_ref, this, textBox_codeacreer.Text, textBox_libellecodeacreer.Text);
        }

        private void button_deleteRecodage_Click(object sender, EventArgs e)
        {
            ActionRecodage.DeleteButton(dataGridView_saisie, dataGridView_ref, this);
        }

        private void dataGridView_saisie_KeyDown(object sender, KeyEventArgs e)
        {
            GUI.RaccourciClavierSaisie(sender, e, this);
        }

        private void button_rapprochementmodule_Click(object sender, EventArgs e)
        {
            Chargement myformchargement = new Chargement();
            myformchargement.label_trt_encours.Text = "Rapprochement automatique par module";
            myformchargement.Show();
            ActionRecodageMODULE_GLOBAL.RapprochementModule(this,myformchargement);
        }

        private void button_parambase_Click(object sender, EventArgs e)
        {
            new MainForm().Show();
        }

        private void button_chargement_Click(object sender, EventArgs e)
        {
            //ChargementTableEnMemoire(Init.CheminBaseClient);
            //MessageBox.Show("Chargement terminé.");
            //progressBar_admin.Value = 0;
        }

        private void button_testsqlserver_Click(object sender, EventArgs e)
        {
            //GUI.EnregistrerBaseSQLITE(this);
        }

        private void button_exportCorresp_Click(object sender, EventArgs e)
        {
            //GUI.ExporteCorrespondance(this);
        }

        private void button_rapprochement_global_Click(object sender, EventArgs e)
        {
            Chargement myformchargement = new Chargement();
            myformchargement.label_trt_encours.Text = "Rapprochement automatique global";
            ActionRecodageMODULE_GLOBAL.RapprochementGlobal(this, myformchargement);
        }

        private void button_acces_admin_Click(object sender, EventArgs e)
        {
            //GUI.AccesInterfaceAdmin(this);
        }

        private void button_okmdpadmin_Click(object sender, EventArgs e)
        {
            //GUI.Enter_OK_Admin(this);
        }

        private void textBox_pass_admin_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)13)
            //{
            //    GUI.Enter_OK_Admin(this);
            //}
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    ActionRecodageMODULE_GLOBAL.CreerCodeInactifModule(this);
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    ActionRecodageMODULE_GLOBAL.CreerInactifGlobal(this);
        //}

        private void button_recherche_Click(object sender, EventArgs e)
        {
            GUI.RechercheRef(dataGridView_saisie, dataGridView_ref, this);
        }

        private void button_del_filter_search_Click(object sender, EventArgs e)
        {
            GUI.EffacerFiltreReferentiel(this);
        }

        private void textBox_search_TextChanged(object sender, EventArgs e)
        {
            GUI.RechercheRef(dataGridView_saisie, dataGridView_ref, this);
        }

        private void button_nepasreprendre_Click(object sender, EventArgs e)
        {
            ActionRecodage.NePasReprendre(dataGridView_saisie, dataGridView_ref, this);
        }

        //private void button_nepasreprendre_module_Click(object sender, EventArgs e)
        //{
        //    ActionRecodageMODULE_GLOBAL.NePasReprendreModule(this);
        //}

        private void comboBox_filtre_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ActionRecodage.FilterModule(dataGridView_saisie, dataGridView_ref, this);
            label1.Focus();
        }

        //private void button_nepasreprendre_global_Click(object sender, EventArgs e)
        //{
        //    ActionRecodageMODULE_GLOBAL.NePasReprendreGlobal(this);
        //}

        private void comboBox_filtre_DrawItem(object sender, DrawItemEventArgs e)
        {
            GUI.ColorComboBox(sender, e);
        }

        private void checkBox_nontraite_CheckedChanged(object sender, EventArgs e)
        {
            GUI.AfficheSeulementNonTraite_search_libellé(this);
            GUI.AfficheSeulementNonTraite_search_code(this);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if ((e.KeyData & Keys.KeyCode) == Keys.Enter)
                return;
            else
                base.OnKeyDown(e);
        }

        private void dataGridView_ref_KeyDown(object sender, KeyEventArgs e)
        {
            GUI.RaccourciClavierSaisie(sender, e, this);
        }

        private void button_delete_recodage_global_Click(object sender, EventArgs e)
        {
            ActionRecodageMODULE_GLOBAL.SuppressionRecodageGlobal(this);
        }

        private void button_delete_recodage_module_Click(object sender, EventArgs e)
        {
            ActionRecodageMODULE_GLOBAL.SuppressionRecodageModule(this);
        }

        private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Chargement myformchargement = new Chargement();
            GUI.MenuOuvrir(this,myformchargement);
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUI.MenuQuitter();
        }

        private void textBox_search_corr_TextChanged(object sender, EventArgs e)
        {
            RechercheCorresp();
        }

        public void RechercheCorresp()
        {
            GUI.AfficheSeulementNonTraite_search_libellé(this);
        }

        private void button_delete_filter_corr_Click(object sender, EventArgs e)
        {
            EffacerFilterCorrespondance();
        }

        public void EffacerFilterCorrespondance()
        {
            textBox_search_saisie_code.Text = "";
            textBox_search_corr.Text = "";
            textBox_search_corr.Focus();
        }

        private void dataGridView_ref_Scroll(object sender, ScrollEventArgs e)
        {
            int additionneur = 30;
            GUI.ColorReferentiel(dataGridView_ref, dataGridView_ref.FirstDisplayedScrollingRowIndex, dataGridView_ref.FirstDisplayedScrollingRowIndex + additionneur);
        }



        private void dataGridView_saisie_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            DataGridViewRow row = dataGridView_saisie.Rows[e.RowIndex];
            DataGridViewCellStyle style1 = new DataGridViewCellStyle();
            if (Convert.ToInt32(row.Cells[20].Value) == 1 && row.Cells[12].Value.Equals(false))
            {
                style1.Font = new Font(Font, FontStyle.Regular);
                row.DefaultCellStyle = style1;
                row.DefaultCellStyle.BackColor = Color.LightGreen;
            }
            else if (Convert.ToInt32(row.Cells[20].Value) == 1 && row.Cells[12].Value.Equals(true))
            {
                style1.Font = new Font(Font, FontStyle.Italic);
                row.DefaultCellStyle = style1;
                row.DefaultCellStyle.BackColor = Color.Gray;
            }
            else if (Convert.ToInt32(row.Cells[20].Value) == 2 && row.Cells[12].Value.Equals(false))
            {
                style1.Font = new Font(Font, FontStyle.Regular);
                row.DefaultCellStyle = style1;
                row.DefaultCellStyle.BackColor = Color.Yellow;
            }
            else if (Convert.ToInt32(row.Cells[20].Value) == 2 && row.Cells[12].Value.Equals(true))
            {
                style1.Font = new Font(Font, FontStyle.Italic);
                row.DefaultCellStyle = style1;
                row.DefaultCellStyle.BackColor = Color.Yellow;
            }
            else if (Convert.ToInt32(row.Cells[20].Value) == 0)
            {
                style1.Font = new Font(Font, FontStyle.Regular);
                row.DefaultCellStyle = style1;
                row.DefaultCellStyle.BackColor = Color.White;
            }
            else if (Convert.ToInt32(row.Cells[20].Value) == 3)
            {
                style1.Font = new Font(Font, FontStyle.Regular);
                row.DefaultCellStyle = style1;
                row.DefaultCellStyle.BackColor = Color.LightSalmon;
            }
        }

        private void dataGridView_ref_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            /*
            DataGridViewRow row = dataGridView_ref.Rows[e.RowIndex];
            DataGridViewRow row_saisie = dataGridView_saisie.Rows[e.RowIndex];
            DataGridViewCellStyle style1 = new DataGridViewCellStyle();
            if (Convert.ToInt32(row.Cells[13].Value) == 0 && row.Cells[5].Value.Equals(false))
            {
                style1.Font = new Font(Font, FontStyle.Regular);
                row.DefaultCellStyle = style1;
                row.DefaultCellStyle.BackColor = Color.LightGreen;
            }
            else if (Convert.ToInt32(row.Cells[13].Value) == 0 && row.Cells[5].Value.Equals(true))
            {
                style1.Font = new Font(Font, FontStyle.Italic);
                row.DefaultCellStyle = style1;
                row.DefaultCellStyle.BackColor = Color.Gray;
            }
            else if (Convert.ToInt32(row.Cells[13].Value) == 2 && row.Cells[5].Value.Equals(false))
            {
                style1.Font = new Font(Font, FontStyle.Regular);
                row.DefaultCellStyle = style1;
                row.DefaultCellStyle.BackColor = Color.Yellow;
            }
            else if (Convert.ToInt32(row.Cells[13].Value) == 2 && row.Cells[5].Value.Equals(true))
            {
                style1.Font = new Font(Font, FontStyle.Italic);
                row.DefaultCellStyle = style1;
                row.DefaultCellStyle.BackColor = Color.Yellow;
            }
            else if (Convert.ToInt32(row_saisie.Cells[20].Value) == 1 && row_saisie.Cells[12].Value.Equals(false))
            {
                style1.Font = new Font(Font, FontStyle.Regular);
                row_saisie.DefaultCellStyle = style1;
                row_saisie.DefaultCellStyle.BackColor = Color.LightGreen;
            }
            else if (Convert.ToInt32(row_saisie.Cells[20].Value) == 1 && row_saisie.Cells[12].Value.Equals(true))
            {
                style1.Font = new Font(Font, FontStyle.Italic);
                row_saisie.DefaultCellStyle = style1;
                row_saisie.DefaultCellStyle.BackColor = Color.Gray;
            }
            else if (Convert.ToInt32(row_saisie.Cells[20].Value) == 2 && row_saisie.Cells[12].Value.Equals(false))
            {
                style1.Font = new Font(Font, FontStyle.Regular);
                row_saisie.DefaultCellStyle = style1;
                row_saisie.DefaultCellStyle.BackColor = Color.Yellow;
            }
            else if (Convert.ToInt32(row_saisie.Cells[20].Value) == 2 && row_saisie.Cells[12].Value.Equals(true))
            {
                style1.Font = new Font(Font, FontStyle.Italic);
                row_saisie.DefaultCellStyle = style1;
                row_saisie.DefaultCellStyle.BackColor = Color.Yellow;
            }
            else if (Convert.ToInt32(row_saisie.Cells[20].Value) == 0)
            {
                style1.Font = new Font(Font, FontStyle.Regular);
                row_saisie.DefaultCellStyle = style1;
                row_saisie.DefaultCellStyle.BackColor = Color.White;
            }
            else if (Convert.ToInt32(row_saisie.Cells[20].Value) == 3)
            {
                style1.Font = new Font(Font, FontStyle.Regular);
                row_saisie.DefaultCellStyle = style1;
                row_saisie.DefaultCellStyle.BackColor = Color.LightSalmon;
            }*/
        }


        public void testdatagridformat()
        {

        }

        private void dataGridView_ref_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private void dataGridView_saisie_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dataGridView_saisie_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ActionRecodage actionRecodage = new ActionRecodage();
                ContextMenu m = new ContextMenu();
                //m.MenuItems.Add(new MenuItem("Creer en inactif, même code, même libellé. (Raccourci clavier : C)", actionRecodage.CreerCodeInactifMemeCodeMemeLibelle_ByMenuItem(sender,e,dataGridView_saisie,dataGridView_ref,this)));
                m.MenuItems.Add(new MenuItem("Creer en inactif, même code, même libellé. (Raccourci clavier : C)", CreerCodeInactifMemeCodeMemeLibelle_ByMenuItem));
                m.MenuItems.Add(new MenuItem("Creer en actif, même code, même libellé. (Raccourci clavier : A)", CreerCodeActifMemeCodeMemeLibelle_ByMenuItem));
                m.MenuItems.Add(new MenuItem("Creer personnalisé  (Raccourci clavier : F2)", AfficherCreationCode_ByMenuItem));
                m.MenuItems.Add(new MenuItem("Supprimer recodage (Raccourci clavier : SUPPR.)", SupprimerRecodage_ByMenuItem));
                m.MenuItems.Add(new MenuItem("Ne pas reprendre (Raccourci clavier : N)", NePasReprendre_ByMenuItem));


                int currentMouseOverRow = dataGridView_saisie.HitTest(e.X, e.Y).RowIndex;
                int currentMouseOverColumns = dataGridView_saisie.HitTest(e.X, e.Y).ColumnIndex;

                if (currentMouseOverRow >= 0 && currentMouseOverColumns == 4)
                {
                    //m.MenuItems.Add(new MenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
                    m.Show(dataGridView_saisie, new Point(e.X, e.Y));
                }



            }
        }

        public void AffecterCode_ByMenuItem(Object sender, System.EventArgs e)
        {
            ActionRecodage actionRecodage = new ActionRecodage();
            actionRecodage.AffecteCode(dataGridView_saisie, dataGridView_ref, this, (string)comboBox_filtre.SelectedValue);
        }

        public void CreerCodeInactifMemeCodeMemeLibelle_ByMenuItem(Object sender, System.EventArgs e)
        {
            ActionRecodage actionRecodage = new ActionRecodage();
            actionRecodage.CreerCodeMemeCodeMemeLibelle(dataGridView_saisie, dataGridView_ref, this, false);
        }

        public void CreerCodeActifMemeCodeMemeLibelle_ByMenuItem(Object sender, System.EventArgs e)
        {
            ActionRecodage actionRecodage = new ActionRecodage();
            actionRecodage.CreerCodeMemeCodeMemeLibelle(dataGridView_saisie, dataGridView_ref, this, true);
        }

        public void AfficherCreationCode_ByMenuItem(Object sender, System.EventArgs e)
        {
            ActionRecodage.DeleteButton(dataGridView_saisie, dataGridView_ref, this);
            GUI.AfficherCreationCode_Datagrid(this);
            //InterfaceCreerCodeAvance interfaceCreerCodeAvance = new InterfaceCreerCodeAvance();
            //interfaceCreerCodeAvance.textBox_ancien_code = 
            //interfaceCreerCodeAvance.ShowDialog();
        }

        public void SupprimerRecodage_ByMenuItem(Object sender, System.EventArgs e)
        {
            ActionRecodage actionRecodage = new ActionRecodage();
            actionRecodage.DeleteButton(dataGridView_saisie, dataGridView_ref, this);
        }

        public void NePasReprendre_ByMenuItem(Object sender, System.EventArgs e)
        {
            ActionRecodage actionRecodage = new ActionRecodage();
            actionRecodage.NePasReprendre(dataGridView_saisie, dataGridView_ref, this);
        }
        /*
        private void dataGridView_saisie_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView_saisie.ReadOnly = true;
            dataGridView_saisie.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void DataGridView_saisie_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            throw new NotImplementedException();
        }
        */
        private void dataGridView_saisie_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==21)
            {
                //MessageBox.Show("Je clique sur CreerInactif");
                CreerCodeInactifMemeCodeMemeLibelle_ByMenuItem(sender,e);
            }
            else if (e.ColumnIndex == 22)
            {
                CreerCodeActifMemeCodeMemeLibelle_ByMenuItem(sender, e);
            }
            else if (e.ColumnIndex == 23)
            {
                //MessageBox.Show("Je clique sur NePasReprendre");
                NePasReprendre_ByMenuItem(sender, e);
            }
        }
        /*
        private void dataGridView_saisie_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_saisie.CurrentCell == dataGridView_saisie.Rows[e.RowIndex].Cells[5]
                && dataGridView_saisie.Rows[e.RowIndex].Cells[5].Value != null
                && dataGridView_saisie.Rows[e.RowIndex].Cells[5].Value.ToString().Trim() != "")
            {
                dataGridView_saisie.CurrentRow.Cells[20].Value = 0;
                ActionRecodage.CreerCodeInterfaceAvancee(dataGridView_saisie, dataGridView_ref, this, dataGridView_saisie.Rows[e.RowIndex].Cells[4].Value.ToString().Trim(), dataGridView_saisie.Rows[e.RowIndex].Cells[5].Value.ToString().Trim());
            }
            //else if (dataGridView_saisie.CurrentCell == dataGridView_saisie.Rows[e.RowIndex].Cells[5]
            //    && (dataGridView_saisie.Rows[e.RowIndex].Cells[5].Value == null
            //    || dataGridView_saisie.Rows[e.RowIndex].Cells[5].Value.ToString().Trim() == ""))
            //{
            //    MessageBox.Show("Code et Libellé obligatoire.");
            //    dataGridView_saisie.CurrentCell = dataGridView_saisie.Rows[e.RowIndex].Cells[5];
            //    dataGridView_saisie.EditMode = DataGridViewEditMode.EditProgrammatically;
            //    dataGridView_saisie.BeginEdit(true);
            //}
            else if (dataGridView_saisie.CurrentCell == dataGridView_saisie.Rows[e.RowIndex].Cells[4]
                && dataGridView_saisie.Rows[e.RowIndex].Cells[4].Value != null
                && dataGridView_saisie.Rows[e.RowIndex].Cells[4].Value.ToString().Trim() != "")
            {
                dataGridView_saisie.CurrentCell = dataGridView_saisie.Rows[e.RowIndex].Cells[5];
                dataGridView_saisie.ReadOnly = false;
                dataGridView_saisie.EditMode = DataGridViewEditMode.EditProgrammatically;
                dataGridView_saisie.BeginEdit(true);
            }
        }
        */
        private void dataGridView_ref_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ActionRecodage actionRecodage = new ActionRecodage();
                ContextMenu m = new ContextMenu();
                //m.MenuItems.Add(new MenuItem("Creer en inactif, même code, même libellé. (Raccourci clavier : C)", actionRecodage.CreerCodeInactifMemeCodeMemeLibelle_ByMenuItem(sender,e,dataGridView_saisie,dataGridView_ref,this)));
                m.MenuItems.Add(new MenuItem("Affecter code (Raccourci clavier : ESPACE ou Double-Click gauche)", AffecterCode_ByMenuItem));
                //MessageBox.Show("e.RowIndex.ToString() : " + e.RowIndex.ToString() + "\r\ne.X : " + e.X + "\r\ne.Y : " + e.Y);


                int currentMouseOverRow = dataGridView_ref.HitTest(e.X, e.Y).RowIndex;
                int currentMouseOverColumns = dataGridView_ref.HitTest(e.X, e.Y).ColumnIndex;

                if (currentMouseOverRow >= 0)
                {
                    //m.MenuItems.Add(new MenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
                    m.Show(dataGridView_ref, new Point(e.X, e.Y));
                }



            }
        }

        private void dataGridView_saisie_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        public int RetourneCelluleEnCourCreation(DataGridView dataGridView_saisie)
        {
            int index = -1;
            for (int i=0;i<dataGridView_saisie.RowCount;i++)
            {
                if((int)dataGridView_saisie.Rows[i].Cells[20].Value==4)
                {
                    return i;
                }
            }
            return index;
        }

        public int RetourneCelluleEnCourCreation_archive(DataGridView dataGridView_saisie)
        {
            int index = -1;
            for (int i = 0; i < dataGridView_saisie.RowCount; i++)
            {
                if ((int)dataGridView_saisie.Rows[i].Cells["FlagReferentiel"].Value == 4)
                {
                    return i;
                }
            }
            return index;
        }

        private void dataGridView_saisie_Click(object sender, EventArgs e)
        {
            int index = RetourneCelluleEnCourCreation(dataGridView_saisie);
            if (index != -1)
            {
                if ((dataGridView_saisie.Rows[index].Cells[5].Value == null || dataGridView_saisie.Rows[index].Cells[5].Value.ToString().Trim() == "")
                    && dataGridView_saisie.CurrentCell != dataGridView_saisie.Rows[index].Cells[4])
                {
                    MessageBox.Show("Code et Libellé obligatoire.");
                    dataGridView_saisie.CurrentCell = dataGridView_saisie.Rows[index].Cells[5];
                    dataGridView_saisie.ReadOnly = false;
                    dataGridView_saisie.EditMode = DataGridViewEditMode.EditProgrammatically;
                    dataGridView_saisie.BeginEdit(true);
                }
                else if(dataGridView_saisie.CurrentCell != dataGridView_saisie.Rows[index].Cells[4] 
                    && (dataGridView_saisie.Rows[index].Cells[4].Value == null || dataGridView_saisie.Rows[index].Cells[4].Value.ToString().Trim() == ""))
                {
                    MessageBox.Show("Code et Libellé obligatoire.");
                    dataGridView_saisie.CurrentCell = dataGridView_saisie.Rows[index].Cells[4];
                    dataGridView_saisie.ReadOnly = false;
                    dataGridView_saisie.EditMode = DataGridViewEditMode.EditProgrammatically;
                    dataGridView_saisie.BeginEdit(true);
                }
            }
        }

        private void nePasReprendreModuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Chargement myformchargement = new Chargement();
            myformchargement.label_trt_encours.Text = "Ne pas reprendre par module";
            myformchargement.Show();
            ActionRecodageMODULE_GLOBAL.NePasReprendreModule(this, myformchargement);
        }

        private void rapprochementAutomatiqueParModuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Chargement myformchargement = new Chargement();
            myformchargement.label_trt_encours.Text = "Rapprochement automatique par module";
            myformchargement.Show();
            ActionRecodageMODULE_GLOBAL.RapprochementModule(this, myformchargement);
        }

        private void rapprochementAutomatiqueGlobalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Chargement myformchargement = new Chargement();
            myformchargement.label_trt_encours.Text = "Rapprochement automatique global";
            myformchargement.Show();
            ActionRecodageMODULE_GLOBAL.RapprochementGlobal(this, myformchargement);
        }

        private void suppressionRecodageParModuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActionRecodageMODULE_GLOBAL.SuppressionRecodageModule(this);
        }

        private void supprimerRecodageGlobalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActionRecodageMODULE_GLOBAL.SuppressionRecodageGlobal(this);
        }

        private void creerCodeInactifParModuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Chargement myformchargement = new Chargement();
            myformchargement.label_trt_encours.Text = "Créer code inactif par module";
            myformchargement.Show();
            ActionRecodageMODULE_GLOBAL.CreerCodeModule(this, myformchargement,false);
        }

        private void créerCodeInactifGlobalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Chargement myformchargement = new Chargement();
            myformchargement.label_trt_encours.Text = "Créer code inactif global";
            myformchargement.Show();
            ActionRecodageMODULE_GLOBAL.CreerGlobal(this, myformchargement,false);
        }

        private void nePasReprendreGlobalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Chargement myformchargement = new Chargement();
            myformchargement.label_trt_encours.Text = "Ne pas reprendre global";
            myformchargement.Show();
            ActionRecodageMODULE_GLOBAL.NePasReprendreGlobal(this, myformchargement);
        }

        private void accèsInterfaceAdminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InterfaceAdmin_pass interfaceAdmin_Pass = new InterfaceAdmin_pass();
            interfaceAdmin_Pass.ShowDialog();
        }

        private void creerCodeActifParModuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Chargement myformchargement = new Chargement();
            myformchargement.label_trt_encours.Text = "Créer code actif par module";
            myformchargement.Show();
            ActionRecodageMODULE_GLOBAL.CreerCodeModule(this, myformchargement, true);
        }

        private void créerCodeActifGlobalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Chargement myformchargement = new Chargement();
            myformchargement.label_trt_encours.Text = "Créer code actif global";
            myformchargement.Show();
            ActionRecodageMODULE_GLOBAL.CreerGlobal(this, myformchargement, true);
        }

        private void dataGridView_ref_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView_ref_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 2)
            {
                ActionRecodage.AffecteCode(dataGridView_saisie, dataGridView_ref, this, (string)comboBox_filtre.SelectedValue);
            }
        }

        private void textBox_search_referentiel_code_TextChanged(object sender, EventArgs e)
        {
            GUI.RechercheRefCode(dataGridView_saisie, dataGridView_ref, this);
        }

        private void textBox_search_saisie_code_TextChanged(object sender, EventArgs e)
        {
            GUI.AfficheSeulementNonTraite_search_code(this);
        }

        private void dataGridView_saisie_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private void dataGridView_ref_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

        }

        private void dataGridView_saisie_Scroll(object sender, ScrollEventArgs e)
        {
            int additionneur = 30;
            GUI.ColorSaisie(dataGridView_saisie, dataGridView_saisie.FirstDisplayedScrollingRowIndex, dataGridView_saisie.FirstDisplayedScrollingRowIndex + additionneur);
        }

        private void comboBox_filtre_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
