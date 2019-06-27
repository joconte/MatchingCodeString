using RecodageList.DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RecodageList.BLL;
using DbAccess;
using System.Threading;
using RecodageList.GUI;


namespace RecodageList.BLL
{   
    /// <summary>
    /// Cette classe regroupe toutes les fonctionnalités graphiques de l'application.
    /// </summary>
    public class GUIFonction
    {
        //ActionRecodage ActionRecodage = new ActionRecodage();
        ChargementTablesEnMemoire ChargementTablesEnMemoire = new ChargementTablesEnMemoire();
        Fonc fonc = new Fonc();
        ReferentielBLL RefObjectBLL = new ReferentielBLL();
        ReferentielDAL RefObjectDAL = new ReferentielDAL();
        CorrespondanceBLL CorrObjectBLL = new CorrespondanceBLL();

        /// <summary>
        /// Initialise la structure du datagrid référentiel en prenant en paramètre les 2 datagridView saisie et référentiel.
        /// </summary>
        /// <param name="dataGridView_ref">DataGridView du referentiel</param>
        /// <param name="dataGridView_saisie">DataGridView des données sources</param>
        public void InitStructReferentiel(DataGridView dataGridView_ref, DataGridView dataGridView_saisie)
        {
            if (dataGridView_ref.Columns != null)
            {
                try
                {
                    dataGridView_ref.Columns["Type"].Visible = false;
                    dataGridView_ref.Columns["TypeItem"].Visible = false;
                    dataGridView_ref.Columns["CodeOrigine"].Visible = false;
                    dataGridView_ref.Columns["Cpl"].Visible = false;
                    dataGridView_ref.Columns["Cpl1"].Visible = false;
                    dataGridView_ref.Columns["Cpl2"].Visible = false;
                    dataGridView_ref.Columns["Cpl3"].Visible = false;
                    dataGridView_ref.Columns["DateFinValidite"].Visible = false;
                    dataGridView_ref.Columns["Canonical"].Visible = false;
                    dataGridView_ref.Columns["FlagPreventiel"].Visible = false;
                    dataGridView_ref.Columns["Code"].HeaderText = "Code Référentiel";
                    dataGridView_ref.Columns["Lib"].HeaderText = "Intitulé Référentiel";
                    dataGridView_ref.Columns["InActif"].HeaderText = "Inactif";
                    dataGridView_ref.Columns["Lib"].Width = dataGridView_saisie.Columns["Libelle_Ancien_Code"].Width + dataGridView_saisie.Columns["Nouveau_Code"].Width + dataGridView_saisie.Columns["Libelle_Nouveau_Code"].Width + dataGridView_saisie.Columns["Occurrence"].Width + dataGridView_saisie.Columns["NouveauCodeInactif"].Width + dataGridView_saisie.Columns["CreerInactif"].Width;
                    dataGridView_ref.Columns["IndiceLevenshtein"].HeaderText = "Indice de Rapprochement";
                    dataGridView_ref.Width = dataGridView_saisie.Width;
                }
                catch(Exception e)
                {
                    Console.WriteLine("Erreur lors de l'init struct ref. : " + e.Message);
                }
            }
        }

        /// <summary>
        /// Initialise la structure du DataGridView des données sources en prenant en paramètre le DataGridView des données sources ainsi que le formulaire de base de l'application.
        /// </summary>
        /// <param name="dataGridView_saisie"></param>
        /// <param name="myform"></param>
        public void InitStructSaisie(DataGridView dataGridView_saisie, Form1 myform)
        {
            if (dataGridView_saisie.Columns != null)
            {
                dataGridView_saisie.Columns["TypeItem"].Visible = false;
                dataGridView_saisie.Columns["AncienCodeActif"].Visible = false;
                dataGridView_saisie.Columns["Code_utilise"].Visible = false;
                dataGridView_saisie.Columns["NomSchema"].Visible = false;
                dataGridView_saisie.Columns["DateRecensement"].Visible = false;
                dataGridView_saisie.Columns["DateMAJ"].Visible = false;
                dataGridView_saisie.Columns["TypeRecodage"].Visible = false;
                dataGridView_saisie.Columns["UtilisateurCreation"].Visible = false;
                dataGridView_saisie.Columns["TypeRef"].Visible = false;
                dataGridView_saisie.Columns["NomRef"].Visible = false;
                dataGridView_saisie.Columns["Cpl"].Visible = false;
                dataGridView_saisie.Columns["Cpl1"].Visible = false;
                dataGridView_saisie.Columns["Cpl2"].Visible = false;
                dataGridView_saisie.Columns["Canonical"].Visible = false;
                dataGridView_saisie.Columns["FlagReferentiel"].Visible = false;
                dataGridView_saisie.Columns["NouveauCodeInactif"].HeaderText = "Inactif";
                dataGridView_saisie.Columns["Libelle_Ancien_Code"].Width = 300;
                dataGridView_saisie.Columns["Libelle_Nouveau_Code"].Width = 300;
                foreach (DataGridViewColumn dc in dataGridView_saisie.Columns)
                {
                    if (dc.Index.Equals(21) || dc.Index.Equals(22) || dc.Index.Equals(23))
                    {
                        dc.ReadOnly = false;
                    }
                    else
                    {
                        dc.ReadOnly = true;
                    }
                }
                if (dataGridView_saisie.CurrentCell != null && dataGridView_saisie.CurrentCell.RowIndex != -1)
                {
                    //dataGridView_saisie.CurrentCell = dataGridView_saisie.Rows[dataGridView_saisie.CurrentRow.Index].Cells[4];
                }

                dataGridView_saisie.Width =
                    dataGridView_saisie.Columns["Ancien_code"].Width +
                    dataGridView_saisie.Columns["Libelle_Ancien_Code"].Width +
                    dataGridView_saisie.Columns["Nouveau_Code"].Width +
                    dataGridView_saisie.Columns["Libelle_Nouveau_Code"].Width +
                    dataGridView_saisie.Columns["Occurrence"].Width +
                    dataGridView_saisie.Columns["NouveauCodeInactif"].Width +
                    dataGridView_saisie.Columns["CreerInactif"].Width +
                    dataGridView_saisie.Columns["CreerActif"].Width +
                    dataGridView_saisie.Columns["NePasReprendre"].Width + 20;

                if ((string)myform.comboBox_filtre.SelectedValue == "1000|0|0|CTRL")
                {
                    dataGridView_saisie.Columns["Ancien_Code"].HeaderText = "Code Etablissement";
                    dataGridView_saisie.Columns["Libelle_Ancien_Code"].HeaderText = "Libellé Etablissement";
                    dataGridView_saisie.Columns["Nouveau_Code"].HeaderText = "Code Entreprise";
                    dataGridView_saisie.Columns["Libelle_Nouveau_Code"].HeaderText = "Libellé Entreprise";
                }
                else
                {
                    
                    dataGridView_saisie.Columns["Ancien_Code"].HeaderText = "Code Source";
                    dataGridView_saisie.Columns["Libelle_Ancien_Code"].HeaderText = "Intitulé Source";
                    dataGridView_saisie.Columns["Nouveau_Code"].HeaderText = "Code Référentiel";
                    dataGridView_saisie.Columns["Libelle_Nouveau_Code"].HeaderText = "Intitulé Référentiel";
                    
                }
                
            }
        }

        /// <summary>
        /// Initialise les couleurs des lignes visibles du DataGridView des données sources ainsi qu'une cohérence de ces couleurs avec les case à cocher 'CreerActif','CreerInactif','NePasReprendre'
        /// </summary>
        /// <param name="dataGridView_saisie"></param>
        public void InitColorSaisie(DataGridView dataGridView_saisie)
        {
            int rowtocolor = 0;
            if (dataGridView_saisie.RowCount > 25)
            {
                rowtocolor = 25;
            }
            else
            {
                rowtocolor = dataGridView_saisie.RowCount;
            }
            int firstline = dataGridView_saisie.FirstDisplayedScrollingRowIndex;
            if (dataGridView_saisie.FirstDisplayedScrollingRowIndex==-1)
            {
                firstline = 0;
            }
            int i = firstline;
            while ( i < firstline + rowtocolor && i < dataGridView_saisie.RowCount)
            {
                //Si le code est présent dans le referentiel en actif alors on met la ligne en vert
                if (Convert.ToInt32(dataGridView_saisie.Rows[i].Cells[20].Value) == 1 && dataGridView_saisie.Rows[i].Cells[12].Value.Equals(false))
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(dataGridView_saisie.Font, FontStyle.Regular);
                    dataGridView_saisie.Rows[i].DefaultCellStyle = style;
                    dataGridView_saisie.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                    dataGridView_saisie.Rows[i].Cells[21].Value = false;
                    dataGridView_saisie.Rows[i].Cells[22].Value = false;
                    dataGridView_saisie.Rows[i].Cells[23].Value = false;
                }
                //Si le code est présent dans le référentiel en inactif on met la ligne en gris
                if (Convert.ToInt32(dataGridView_saisie.Rows[i].Cells[20].Value) == 1 && dataGridView_saisie.Rows[i].Cells[12].Value.Equals(true))
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(dataGridView_saisie.Font, FontStyle.Italic);
                    dataGridView_saisie.Rows[i].DefaultCellStyle = style;
                    dataGridView_saisie.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
                    dataGridView_saisie.Rows[i].Cells[21].Value = false;
                    dataGridView_saisie.Rows[i].Cells[22].Value = false;
                    dataGridView_saisie.Rows[i].Cells[23].Value = false;
                }
                //Si le code n'est pas présent dans le référentiel et qu'il n'est pas flagué en inactif
                //alors on affiche la ligne en jaune et on coche la case 'creer en actif' 
                if (Convert.ToInt32(dataGridView_saisie.Rows[i].Cells[20].Value) == 2 && dataGridView_saisie.Rows[i].Cells[12].Value.Equals(false))
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(dataGridView_saisie.Font, FontStyle.Regular);
                    dataGridView_saisie.Rows[i].DefaultCellStyle = style;
                    dataGridView_saisie.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    dataGridView_saisie.Rows[i].Cells[22].Value = true;
                }
                //Si le code n'est pas présent dans le référentiel et qu'il est flagué en inactif
                //alors on affiche la ligne en jaune et en italique et on coche la case 'creer en inactif'
                if (Convert.ToInt32(dataGridView_saisie.Rows[i].Cells[20].Value) == 2 && dataGridView_saisie.Rows[i].Cells[12].Value.Equals(true))
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(dataGridView_saisie.Font, FontStyle.Italic);
                    dataGridView_saisie.Rows[i].DefaultCellStyle = style;
                    dataGridView_saisie.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;
                    dataGridView_saisie.Rows[i].Cells[21].Value = true;
                }
                //Si le code n'est pas traité alors on affiche la ligne en blanc
                if (Convert.ToInt32(dataGridView_saisie.Rows[i].Cells[20].Value) == 0)
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(dataGridView_saisie.Font, FontStyle.Regular);
                    dataGridView_saisie.Rows[i].DefaultCellStyle = style;
                    dataGridView_saisie.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
                //Si le code est flagué 'ne pas reprendre' alors on affiche la ligne en saumon
                //et on coche la case 'ne pas reprendre'
                if (Convert.ToInt32(dataGridView_saisie.Rows[i].Cells[20].Value) == 3)
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(dataGridView_saisie.Font, FontStyle.Regular);
                    dataGridView_saisie.Rows[i].DefaultCellStyle = style;
                    dataGridView_saisie.Rows[i].DefaultCellStyle.BackColor = Color.LightSalmon;
                    dataGridView_saisie.Rows[i].Cells[23].Value = true;
                }
                i++;
            }
        }

        /// <summary>
        /// Initialise les couleurs des lignes visibles du DataGridView référentiel. 
        /// </summary>
        /// <param name="dataGridView_ref"></param>
        public void InitColorReferentiel(DataGridView dataGridView_ref)
        {
            
            int rowtocolor = 0;
            if (dataGridView_ref.RowCount > 25)
            {
                rowtocolor = 25;
            }
            else
            {
                rowtocolor = dataGridView_ref.RowCount;
            }
            for (int i = 0; i < rowtocolor; i++)
            {
                if (Convert.ToInt32(dataGridView_ref.Rows[i].Cells[13].Value) == 2 && dataGridView_ref.Rows[i].Cells[5].Value.Equals(false))
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(dataGridView_ref.Font, FontStyle.Regular);
                    dataGridView_ref.Rows[i].DefaultCellStyle = style;
                    dataGridView_ref.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
                else if (Convert.ToInt32(dataGridView_ref.Rows[i].Cells[13].Value) == 2 && dataGridView_ref.Rows[i].Cells[5].Value.Equals(true))
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(dataGridView_ref.Font, FontStyle.Italic);
                    dataGridView_ref.Rows[i].DefaultCellStyle = style;
                    dataGridView_ref.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;
                }
                else if (Convert.ToInt32(dataGridView_ref.Rows[i].Cells[13].Value) == 0 && (bool)dataGridView_ref.Rows[i].Cells[5].Value == false)
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(dataGridView_ref.Font, FontStyle.Regular);
                    dataGridView_ref.Rows[i].DefaultCellStyle = style;
                    dataGridView_ref.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else if (Convert.ToInt32(dataGridView_ref.Rows[i].Cells[13].Value) == 0 && (bool)dataGridView_ref.Rows[i].Cells[5].Value == true)
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(dataGridView_ref.Font, FontStyle.Italic);
                    dataGridView_ref.Rows[i].DefaultCellStyle = style;
                    dataGridView_ref.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
                }
            }
        }

        /// <summary>
        /// Colorie le DataGridView référentiel sur un ensemble de lignes donné.
        /// Ex : ColorReferentiel(datagridview_ref,50,100) va colorier les lignes 50 à 100 du DataGridView référentiel
        /// </summary>
        /// <param name="dataGridView_ref"></param>
        /// <param name="debutcoloration"></param>
        /// <param name="fincoloration"></param>
        public void ColorReferentiel(DataGridView dataGridView_ref, int debutcoloration, int fincoloration)
        {
            int i = debutcoloration;
            while (i< fincoloration && i < dataGridView_ref.RowCount)
            {
                if (Convert.ToInt32(dataGridView_ref.Rows[i].Cells[13].Value) == 2 && dataGridView_ref.Rows[i].Cells[5].Value.Equals(false))
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(dataGridView_ref.Font, FontStyle.Regular);
                    dataGridView_ref.Rows[i].DefaultCellStyle = style;
                    dataGridView_ref.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
                else if (Convert.ToInt32(dataGridView_ref.Rows[i].Cells[13].Value) == 2 && dataGridView_ref.Rows[i].Cells[5].Value.Equals(true))
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(dataGridView_ref.Font, FontStyle.Italic);
                    dataGridView_ref.Rows[i].DefaultCellStyle = style;
                    dataGridView_ref.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;
                }
                else if (Convert.ToInt32(dataGridView_ref.Rows[i].Cells[13].Value) == 0 && (bool)dataGridView_ref.Rows[i].Cells[5].Value == false)
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(dataGridView_ref.Font, FontStyle.Regular);
                    dataGridView_ref.Rows[i].DefaultCellStyle = style;
                    dataGridView_ref.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else if (Convert.ToInt32(dataGridView_ref.Rows[i].Cells[13].Value) == 0 && (bool)dataGridView_ref.Rows[i].Cells[5].Value == true)
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(dataGridView_ref.Font, FontStyle.Italic);
                    dataGridView_ref.Rows[i].DefaultCellStyle = style;
                    dataGridView_ref.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
                }
                i++;
            }
        }

        /// <summary>
        /// Colorie le DataGridView des données sources sur un ensemble de lignes donné.
        /// Ex : ColorSaisie(datagridview_saisie,50,100) va colorier les lignes 50 à 100 du DataGridView des données sources
        /// </summary>
        /// <param name="dataGridView_saisie"></param>
        /// <param name="debutcoloration"></param>
        /// <param name="fincoloration"></param>
        public void ColorSaisie(DataGridView dataGridView_saisie, int debutcoloration, int fincoloration)
        {
            int i = debutcoloration;
            while (i < fincoloration && i < dataGridView_saisie.RowCount)
            {
                if (Convert.ToInt32(dataGridView_saisie.Rows[i].Cells[20].Value) == 2 && dataGridView_saisie.Rows[i].Cells[12].Value.Equals(false))
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(dataGridView_saisie.Font, FontStyle.Regular);
                    dataGridView_saisie.Rows[i].DefaultCellStyle = style;
                    dataGridView_saisie.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
                else if (Convert.ToInt32(dataGridView_saisie.Rows[i].Cells[20].Value) == 2 && dataGridView_saisie.Rows[i].Cells[12].Value.Equals(true))
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(dataGridView_saisie.Font, FontStyle.Italic);
                    dataGridView_saisie.Rows[i].DefaultCellStyle = style;
                    dataGridView_saisie.Rows[i].DefaultCellStyle.BackColor = Color.LightYellow;
                }
                else if (Convert.ToInt32(dataGridView_saisie.Rows[i].Cells[20].Value) == 1 && (bool)dataGridView_saisie.Rows[i].Cells[12].Value == false)
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(dataGridView_saisie.Font, FontStyle.Regular);
                    dataGridView_saisie.Rows[i].DefaultCellStyle = style;
                    dataGridView_saisie.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else if (Convert.ToInt32(dataGridView_saisie.Rows[i].Cells[20].Value) == 1 && (bool)dataGridView_saisie.Rows[i].Cells[12].Value == true)
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(dataGridView_saisie.Font, FontStyle.Italic);
                    dataGridView_saisie.Rows[i].DefaultCellStyle = style;
                    dataGridView_saisie.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
                }
                else if (Convert.ToInt32(dataGridView_saisie.Rows[i].Cells[20].Value) == 3)
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(dataGridView_saisie.Font, FontStyle.Regular);
                    dataGridView_saisie.Rows[i].DefaultCellStyle = style;
                    dataGridView_saisie.Rows[i].DefaultCellStyle.BackColor = Color.LightSalmon;
                }
                else if (Convert.ToInt32(dataGridView_saisie.Rows[i].Cells[20].Value) == 0)
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(dataGridView_saisie.Font, FontStyle.Regular);
                    dataGridView_saisie.Rows[i].DefaultCellStyle = style;
                    dataGridView_saisie.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
                i++;
            }
        }

        /// <summary>
        /// Initialise les info-bulles du formulaire de base de l'application.
        /// </summary>
        /// <param name="myform"></param>
        public void InitToolTip(Form1 myform)
        {
            myform.rapprochementAutomatiqueGlobalToolStripMenuItem.ToolTipText = "Permet de rapprocher automatiquement tous les codes, de tous les modules, du référentiel Preventiel.\r\n" +
                "La règle est la suivante : si la forme canonique du libellé source est identique à la forme canonique du libellé d'un code Preventiel alors le rapprochement est effectué.\r\n" +
                "Forme canonique : \r\n" +
                "- Changement de caractère, ex : é -> e \r\n" +
                "- Suppression de caractère non significatif, ex : '(' -> '' ";

            myform.rapprochementAutomatiqueParModuleToolStripMenuItem.ToolTipText = "Permet de rapprocher automatiquement tous les codes, du module actuel, du référentiel Preventiel.\r\n" +
                "La règle est la suivante : si la forme canonique du libellé source est identique à la forme canonique du libellé d'un code Preventiel alors le rapprochement est effectué.\r\n" +
                "Forme canonique : \r\n" +
                "- Changement de caractère, ex : é -> e \r\n" +
                "- Suppression de caractère non significatif, ex : '(' -> '' ";

            myform.créerCodeInactifGlobalToolStripMenuItem.ToolTipText = "Permet de créer en inactif tous les codes non traités, de tous les modules, en reprennant même code, même libellé.";

            myform.creerCodeInactifParModuleToolStripMenuItem.ToolTipText = "Permet de créer en inactif tous les codes non traités, du module actuel, en reprennant même code, même libellé.";

            myform.nePasReprendreGlobalToolStripMenuItem.ToolTipText = "Permet de ne pas reprendre tous les codes non traités, de tous les modules.";

            myform.nePasReprendreModuleToolStripMenuItem.ToolTipText = "Permet de ne pas reprendre tous les codes non traités, du module actuel.";

            myform.supprimerRecodageGlobalToolStripMenuItem.ToolTipText = "Permet de supprimer tous les recodages de tous les modules.";

            myform.suppressionRecodageParModuleToolStripMenuItem.ToolTipText = "Permet de supprimer tous les recodages du module actuel.";
        }

        /// <summary>
        /// Initialise le ComBoBox qui permet de filtrer par module.
        /// </summary>
        /// <param name="myform"></param>
        public void InitComboBoxFiltre(Form1 myform)
        {
            myform.comboBox_filtre.DataSource = new BindingSource(VariablePartage.ComboBoxFiltre, null);
            myform.comboBox_filtre.DisplayMember = "NomRef";
            myform.comboBox_filtre.ValueMember = "Cpl";
            //myform.progressBar_admin.Value++;
            myform.comboBox_filtre.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// Inactive une liste de contrôle sauf le controle passé en paramètre.
        /// </summary>
        /// <param name="button"></param>
        /// <param name="myform"></param>
        public void InactiveControlExceptParamButton(Button button , Form1 myform)
        {
            if (button.Name != myform.comboBox_filtre.Name)
            {
                myform.comboBox_filtre.Enabled = false;
            }
            if (button.Name != myform.dataGridView_saisie.Name)
            {
                myform.dataGridView_saisie.Enabled = false;
            }
            if (button.Name != myform.dataGridView_ref.Name)
            {
                myform.dataGridView_ref.Enabled = false;
            }
            if (button.Name != myform.button_del_filter_search.Name)
            {
                myform.button_del_filter_search.Enabled = false;
            }
            if (button.Name != myform.textBox_search.Name)
            {
                myform.textBox_search.Enabled = false;
            }
            if (button.Name != myform.checkBox_nontraite.Name)
            {
                myform.checkBox_nontraite.Enabled = false;
            }
            if (button.Name != myform.fichierToolStripMenuItem.Name)
            {
                myform.fichierToolStripMenuItem.Enabled = false;
            }
            if (button.Name != myform.textBox_search_corr.Name)
            {
                myform.textBox_search_corr.Enabled = false;
            }
            if (button.Name != myform.button_delete_filter_corr.Name)
            {
                myform.button_delete_filter_corr.Enabled = false;
            }
        }

        /// <summary>
        /// Reactive les contrôles du formulaire de base de l'application.
        /// </summary>
        /// <param name="myform"></param>
        public void ReactiveButton(Form1 myform)
        {
            myform.dataGridView_saisie.Enabled = true;
            myform.dataGridView_ref.Enabled = true;
            myform.comboBox_filtre.Enabled = true;
            myform.button_del_filter_search.Enabled = true;
            myform.textBox_search.Enabled = true;
            myform.textBox_search_referentiel_code.Enabled = true;
            myform.textBox_search_saisie_code.Enabled = true;
            myform.checkBox_nontraite.Enabled = true;
            myform.fichierToolStripMenuItem.Enabled = true;
            myform.textBox_search_corr.Enabled = true;
            myform.button_delete_filter_corr.Enabled = true;
        }

        /// <summary>
        /// Affiche une boite de dialogue permettant d'ouvrir la base de recodage.
        /// </summary>
        /// <param name="myform"></param>
        /// <param name="myformchargement"></param>
        public void MenuOuvrir(Form1 myform, Chargement myformchargement)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Database Files|*.db";
            openFileDialog1.Title = "Sélectionner la base recodage du client";

            // Show the Dialog.  
            // If the user clicked OK in the dialog and  
            // a .CUR file was selected, open it.  
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                VariablePartage.CheminBaseClient = openFileDialog1.FileName;
                myformchargement.Show();
                ChargementTablesEnMemoire.ChargementTableEnMemoire(VariablePartage.CheminBaseClient,myform, myformchargement);
            }
        }

        /// <summary>
        /// Initialise la variable globale ListeNomRef_admin en lui affectant la liste des NomRef de type administratif
        /// </summary>
        public void InitListeNomRef_admin()
        {
            VariablePartage.ListeNomRef_admin = new List<string>(new string[] { "Civilite", "CodeMotifRadiation", "CodeMotifSuspension", "ContratTravail", "FormeJuridique", "MotifSortie", "OrgaSanteTravail", "PersonnelMedical", "PosteDeTravail", "SituationFamille","Entreprise","Etablissement","Qualification", "RythmeTravail","Emploi","MotifSortie","Secteur" });
        }

        /// <summary>
        /// Enregistre la base de données SQLite précédemment chargée et effectue un calcul de cohérence du recodage permetant d'affecter les codes couleurs corrects.
        /// </summary>
        /// <param name="myform"></param>
        /// <param name="myformchargement"></param>
        public void EnregistrerBaseSQLITE(Form1 myform, Chargement myformchargement)
        {
            // Displays a SaveFileDialog so the user can save the Image  
            // assigned to Button2.  
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Database Files|*.db";
            saveFileDialog1.Title = "Sauvegarder la base de recodage";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.  
            if (saveFileDialog1.FileName != "")
            {
                File.Copy(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\PREVTGXV7.db", saveFileDialog1.FileName, true);
                VariablePartage.CheminBaseClient = saveFileDialog1.FileName;
                myformchargement.Show();
                ChargementTablesEnMemoire.ChargementTableEnMemoire(VariablePartage.CheminBaseClient, myform, myformchargement);
                myform.label_client_en_cour.Text = VariablePartage.ClientEnCours;
                //File.Delete(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\PREVTGXV7.db");
            }
        }

        /// <summary>
        /// Permet de rechercher dans le DataGridView référentiel des items par code. 
        /// </summary>
        /// <param name="dataGridView_saisie"></param>
        /// <param name="dataGridView_ref"></param>
        /// <param name="myform"></param>
        public void RechercheRefCode(DataGridView dataGridView_saisie, DataGridView dataGridView_ref, Form1 myform)
        {
            if (dataGridView_saisie.CurrentCell != null && dataGridView_saisie.CurrentCell.RowIndex != -1
            && dataGridView_saisie.CurrentRow.Cells[16].Value.ToString() != "50" && dataGridView_saisie.CurrentRow.Cells[16].Value.ToString() != "135"
                && dataGridView_saisie.CurrentRow.Cells[16].Value.ToString() != "60" && dataGridView_saisie.CurrentRow.Cells[16].Value.ToString() != "141")
            {
                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);
                List<ReferentielBLL> TableRefLevenshtein = new List<ReferentielBLL>();
                if (dataGridView_saisie.CurrentRow.Cells[2].Value != null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, dataGridView_saisie.CurrentRow.Cells[2].Value.ToString());
                }
                else if (dataGridView_saisie.CurrentRow.Cells[2].Value == null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, "");
                }

                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltreReferentielParCodeRecherche(TableRefLevenshtein, myform.textBox_search_referentiel_code.Text);
                var source_ref = new BindingSource();
                source_ref.DataSource = VariablePartage.TableReferentielFiltre;
                dataGridView_ref.DataSource = source_ref;
                InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
                InitColorReferentiel(dataGridView_ref);

            }
            else if (dataGridView_saisie.CurrentCell != null && dataGridView_saisie.CurrentCell.RowIndex != -1
            && dataGridView_saisie.CurrentRow.Cells[16].Value.ToString() == "50" && dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Contains("#"))
            {
                string[] examen_ancien_code = dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Split('#');
                CorrespondanceBLL ligne_exam_nouveau_code = CorrObjectBLL.RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondanceFiltre, examen_ancien_code[0], "135|0|0|NOMEN");
                List<ReferentielBLL> TableRefResultatExamen = new List<ReferentielBLL>();
                TableRefResultatExamen = RefObjectBLL.FiltrerListeResultatExamenReferentielParExamen(VariablePartage.TableReferentiel, ligne_exam_nouveau_code.Nouveau_Code);
                List<ReferentielBLL> TableRefLevenshtein = new List<ReferentielBLL>();
                if (dataGridView_saisie.CurrentRow.Cells[2].Value != null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(TableRefResultatExamen, dataGridView_saisie.CurrentRow.Cells[2].Value.ToString());
                }
                else if (dataGridView_saisie.CurrentRow.Cells[2].Value == null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(TableRefResultatExamen, "");
                }
                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltreReferentielParCodeRecherche(TableRefLevenshtein, myform.textBox_search_referentiel_code.Text);
                var source_ref = new BindingSource();
                source_ref.DataSource = VariablePartage.TableReferentielFiltre;
                dataGridView_ref.DataSource = source_ref;
                InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
                InitColorReferentiel(dataGridView_ref);
            }
            else if (dataGridView_saisie.CurrentCell != null && dataGridView_saisie.CurrentCell.RowIndex != -1
            && dataGridView_saisie.CurrentRow.Cells[16].Value.ToString() == "135")
            {
                //ReferentielDAL Ref = new ReferentielDAL();
                //Console.WriteLine("(string)comboBox_filtre.SelectedValue : " + (string)comboBox_filtre.SelectedValue);
                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);

                //Supprime les resultat examen du referentiel
                VariablePartage.TableReferentielFiltre = RefObjectBLL.SupprimeResultatExamenReferentiel(VariablePartage.TableReferentielFiltre);

                List<ReferentielBLL> TableRefLevenshtein = new List<ReferentielBLL>();
                if (dataGridView_saisie.CurrentRow.Cells[2].Value != null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, dataGridView_saisie.CurrentRow.Cells[2].Value.ToString());
                }
                else if (dataGridView_saisie.CurrentRow.Cells[2].Value == null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, "");
                }
                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltreReferentielParCodeRecherche(TableRefLevenshtein, myform.textBox_search_referentiel_code.Text);
                var source_ref = new BindingSource();
                source_ref.DataSource = VariablePartage.TableReferentielFiltre;
                dataGridView_ref.DataSource = source_ref;
                InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
                InitColorReferentiel(dataGridView_ref);
            }
            else if (dataGridView_saisie.CurrentCell != null && dataGridView_saisie.CurrentCell.RowIndex != -1
            && dataGridView_saisie.CurrentRow.Cells[16].Value.ToString() == "60" && dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Contains("#"))
            {
                string[] vaccin_ancien_code = dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Split('#');
                CorrespondanceBLL ligne_vacin_nouveau_code = CorrObjectBLL.RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondanceFiltre, vaccin_ancien_code[0], "141|0|0|NOMEN");
                List<ReferentielBLL> TableProtocoleVaccin = new List<ReferentielBLL>();
                TableProtocoleVaccin = RefObjectBLL.FiltrerListeResultatExamenReferentielParExamen(VariablePartage.TableReferentiel, ligne_vacin_nouveau_code.Nouveau_Code);
                List<ReferentielBLL> TableRefLevenshtein = new List<ReferentielBLL>();
                if (dataGridView_saisie.CurrentRow.Cells[2].Value != null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(TableProtocoleVaccin, dataGridView_saisie.CurrentRow.Cells[2].Value.ToString());
                }
                else if (dataGridView_saisie.CurrentRow.Cells[2].Value == null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(TableProtocoleVaccin, "");
                }
                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltreReferentielParCodeRecherche(TableRefLevenshtein, myform.textBox_search_referentiel_code.Text);
                var source_ref = new BindingSource();
                source_ref.DataSource = VariablePartage.TableReferentielFiltre;
                dataGridView_ref.DataSource = source_ref;
                InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
                InitColorReferentiel(dataGridView_ref);
            }
            else if (dataGridView_saisie.CurrentCell != null && dataGridView_saisie.CurrentCell.RowIndex != -1
            && dataGridView_saisie.CurrentRow.Cells[16].Value.ToString() == "141")
            {
                //ReferentielDAL Ref = new ReferentielDAL();
                //Console.WriteLine("(string)comboBox_filtre.SelectedValue : " + (string)comboBox_filtre.SelectedValue);
                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);
                VariablePartage.TableReferentielFiltre = RefObjectBLL.SupprimeProtocoleVaccinal(VariablePartage.TableReferentielFiltre);
                List<ReferentielBLL> TableRefLevenshtein = new List<ReferentielBLL>();
                if (dataGridView_saisie.CurrentRow.Cells[2].Value != null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, dataGridView_saisie.CurrentRow.Cells[2].Value.ToString());
                }
                else if (dataGridView_saisie.CurrentRow.Cells[2].Value == null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, "");
                }
                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltreReferentielParCodeRecherche(TableRefLevenshtein, myform.textBox_search_referentiel_code.Text);
                var source_ref = new BindingSource();
                source_ref.DataSource = VariablePartage.TableReferentielFiltre;
                dataGridView_ref.DataSource = source_ref;
                InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
                InitColorReferentiel(dataGridView_ref);
            }
            else
            {
                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);
                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltreReferentielParCodeRecherche(VariablePartage.TableReferentielFiltre, myform.textBox_search_referentiel_code.Text);
                var source_ref = new BindingSource();
                source_ref.DataSource = VariablePartage.TableReferentielFiltre;
                dataGridView_ref.DataSource = source_ref;
                InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
                InitColorReferentiel(dataGridView_ref);
            }
        }

        /// <summary>
        /// Permet de rechercher dans le DataGridView référentiel des items par libellé. 
        /// </summary>
        /// <param name="dataGridView_saisie"></param>
        /// <param name="dataGridView_ref"></param>
        /// <param name="myform"></param>
        public void RechercheRef(DataGridView dataGridView_saisie, DataGridView dataGridView_ref, Form1 myform)
        {
            if (dataGridView_saisie.CurrentCell != null && dataGridView_saisie.CurrentCell.RowIndex != -1
            && dataGridView_saisie.CurrentRow.Cells[16].Value.ToString() != "50" && dataGridView_saisie.CurrentRow.Cells[16].Value.ToString() != "135"
                && dataGridView_saisie.CurrentRow.Cells[16].Value.ToString() != "60" && dataGridView_saisie.CurrentRow.Cells[16].Value.ToString() != "141")
            {
                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);
                List<ReferentielBLL> TableRefLevenshtein = new List<ReferentielBLL>();
                if (dataGridView_saisie.CurrentRow.Cells[2].Value != null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, dataGridView_saisie.CurrentRow.Cells[2].Value.ToString());
                }
                else if (dataGridView_saisie.CurrentRow.Cells[2].Value == null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, "");
                }

                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltreReferentielParChaineRecherche(TableRefLevenshtein, myform.textBox_search.Text);
                var source_ref = new BindingSource();
                source_ref.DataSource = VariablePartage.TableReferentielFiltre;
                dataGridView_ref.DataSource = source_ref;
                InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
                InitColorReferentiel(dataGridView_ref);

            }
            else if (dataGridView_saisie.CurrentCell != null && dataGridView_saisie.CurrentCell.RowIndex != -1
            && dataGridView_saisie.CurrentRow.Cells[16].Value.ToString() == "50" && dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Contains("#"))
            {
                string[] examen_ancien_code = dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Split('#');
                CorrespondanceBLL ligne_exam_nouveau_code = CorrObjectBLL.RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondanceFiltre, examen_ancien_code[0], "135|0|0|NOMEN");
                List<ReferentielBLL> TableRefResultatExamen = new List<ReferentielBLL>();
                TableRefResultatExamen = RefObjectBLL.FiltrerListeResultatExamenReferentielParExamen(VariablePartage.TableReferentiel, ligne_exam_nouveau_code.Nouveau_Code);
                List<ReferentielBLL> TableRefLevenshtein = new List<ReferentielBLL>();
                if (dataGridView_saisie.CurrentRow.Cells[2].Value != null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(TableRefResultatExamen, dataGridView_saisie.CurrentRow.Cells[2].Value.ToString());
                }
                else if (dataGridView_saisie.CurrentRow.Cells[2].Value == null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(TableRefResultatExamen, "");
                }
                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltreReferentielParChaineRecherche(TableRefLevenshtein, myform.textBox_search.Text);
                var source_ref = new BindingSource();
                source_ref.DataSource = VariablePartage.TableReferentielFiltre;
                dataGridView_ref.DataSource = source_ref;
                InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
                InitColorReferentiel(dataGridView_ref);
            }
            else if (dataGridView_saisie.CurrentCell != null && dataGridView_saisie.CurrentCell.RowIndex != -1
            && dataGridView_saisie.CurrentRow.Cells[16].Value.ToString() == "135")
            {
                //ReferentielDAL Ref = new ReferentielDAL();
                //Console.WriteLine("(string)comboBox_filtre.SelectedValue : " + (string)comboBox_filtre.SelectedValue);
                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);

                //Supprime les resultat examen du referentiel
                VariablePartage.TableReferentielFiltre = RefObjectBLL.SupprimeResultatExamenReferentiel(VariablePartage.TableReferentielFiltre);

                List<ReferentielBLL> TableRefLevenshtein = new List<ReferentielBLL>();
                if (dataGridView_saisie.CurrentRow.Cells[2].Value != null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, dataGridView_saisie.CurrentRow.Cells[2].Value.ToString());
                }
                else if (dataGridView_saisie.CurrentRow.Cells[2].Value == null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, "");
                }
                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltreReferentielParChaineRecherche(TableRefLevenshtein, myform.textBox_search.Text);
                var source_ref = new BindingSource();
                source_ref.DataSource = VariablePartage.TableReferentielFiltre;
                dataGridView_ref.DataSource = source_ref;
                InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
                InitColorReferentiel(dataGridView_ref);
            }
            else if (dataGridView_saisie.CurrentCell != null && dataGridView_saisie.CurrentCell.RowIndex != -1
            && dataGridView_saisie.CurrentRow.Cells[16].Value.ToString() == "60" && dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Contains("#"))
            {
                string[] vaccin_ancien_code = dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Split('#');
                CorrespondanceBLL ligne_vacin_nouveau_code = CorrObjectBLL.RetourneCorrespondanceNouveauCode(VariablePartage.TableCorrespondanceFiltre, vaccin_ancien_code[0], "141|0|0|NOMEN");
                List<ReferentielBLL> TableProtocoleVaccin = new List<ReferentielBLL>();
                TableProtocoleVaccin = RefObjectBLL.FiltrerListeResultatExamenReferentielParExamen(VariablePartage.TableReferentiel, ligne_vacin_nouveau_code.Nouveau_Code);
                List<ReferentielBLL> TableRefLevenshtein = new List<ReferentielBLL>();
                if (dataGridView_saisie.CurrentRow.Cells[2].Value != null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(TableProtocoleVaccin, dataGridView_saisie.CurrentRow.Cells[2].Value.ToString());
                }
                else if (dataGridView_saisie.CurrentRow.Cells[2].Value == null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(TableProtocoleVaccin, "");
                }
                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltreReferentielParChaineRecherche(TableRefLevenshtein, myform.textBox_search.Text);
                var source_ref = new BindingSource();
                source_ref.DataSource = VariablePartage.TableReferentielFiltre;
                dataGridView_ref.DataSource = source_ref;
                InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
                InitColorReferentiel(dataGridView_ref);
            }
            else if (dataGridView_saisie.CurrentCell != null && dataGridView_saisie.CurrentCell.RowIndex != -1
            && dataGridView_saisie.CurrentRow.Cells[16].Value.ToString() == "141")
            {
                //ReferentielDAL Ref = new ReferentielDAL();
                //Console.WriteLine("(string)comboBox_filtre.SelectedValue : " + (string)comboBox_filtre.SelectedValue);
                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);
                VariablePartage.TableReferentielFiltre = RefObjectBLL.SupprimeProtocoleVaccinal(VariablePartage.TableReferentielFiltre);
                List <ReferentielBLL> TableRefLevenshtein = new List<ReferentielBLL>();
                if (dataGridView_saisie.CurrentRow.Cells[2].Value != null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, dataGridView_saisie.CurrentRow.Cells[2].Value.ToString());
                }
                else if (dataGridView_saisie.CurrentRow.Cells[2].Value == null)
                {
                    TableRefLevenshtein = RefObjectBLL.TrierListeParLevenshtein(VariablePartage.TableReferentielFiltre, "");
                }
                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltreReferentielParChaineRecherche(TableRefLevenshtein, myform.textBox_search.Text);
                var source_ref = new BindingSource();
                source_ref.DataSource = VariablePartage.TableReferentielFiltre;
                dataGridView_ref.DataSource = source_ref;
                InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
                InitColorReferentiel(dataGridView_ref);
            }
            else
            {
                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltrerListeReferentiel_parCPL(VariablePartage.TableReferentiel, (string)myform.comboBox_filtre.SelectedValue);
                VariablePartage.TableReferentielFiltre = RefObjectBLL.FiltreReferentielParChaineRecherche(VariablePartage.TableReferentielFiltre, myform.textBox_search.Text);
                var source_ref = new BindingSource();
                source_ref.DataSource = VariablePartage.TableReferentielFiltre;
                dataGridView_ref.DataSource = source_ref;
                InitStructReferentiel(dataGridView_ref, dataGridView_saisie);
                InitColorReferentiel(dataGridView_ref);
            }
        }

        public void AfficheSeulementNonTraite_search_libellé(Form1 myform)
        {
            myform.textBox_search_saisie_code.Text = "";
            if (myform.checkBox_nontraite.Checked)
            {
                VariablePartage.TableCorrespondanceFiltre = CorrObjectBLL.FiltrerListeCorrespondance_parCPL(VariablePartage.TableCorrespondance, (string)myform.comboBox_filtre.SelectedValue);
                VariablePartage.TableCorrespondanceFiltre = CorrObjectBLL.FiltreCorrespondanceParChaineRecherche(VariablePartage.TableCorrespondanceFiltre, myform.textBox_search_corr.Text);
                VariablePartage.TableCorrespondanceFiltre = CorrObjectBLL.RetourneSeulementNonTraite(VariablePartage.TableCorrespondanceFiltre);
                var source_ref = new BindingSource();
                source_ref.DataSource = VariablePartage.TableCorrespondanceFiltre;
                myform.dataGridView_saisie.DataSource = source_ref;
                InitStructSaisie(myform.dataGridView_saisie, myform);
                InitColorSaisie(myform.dataGridView_saisie);
            }
            else
            {
                VariablePartage.TableCorrespondanceFiltre = CorrObjectBLL.FiltrerListeCorrespondance_parCPL(VariablePartage.TableCorrespondance, (string)myform.comboBox_filtre.SelectedValue);
                VariablePartage.TableCorrespondanceFiltre = CorrObjectBLL.FiltreCorrespondanceParChaineRecherche(VariablePartage.TableCorrespondanceFiltre, myform.textBox_search_corr.Text);
                var source = new BindingSource();
                source.DataSource = VariablePartage.TableCorrespondanceFiltre;
                myform.dataGridView_saisie.DataSource = source;
                InitStructSaisie(myform.dataGridView_saisie, myform);
                InitColorSaisie(myform.dataGridView_saisie);
            }
        }

        public void AfficheSeulementNonTraite_search_code(Form1 myform)
        {
            myform.textBox_search_corr.Text = "";
            if (myform.checkBox_nontraite.Checked)
            {
                VariablePartage.TableCorrespondanceFiltre = CorrObjectBLL.FiltrerListeCorrespondance_parCPL(VariablePartage.TableCorrespondance, (string)myform.comboBox_filtre.SelectedValue);
                VariablePartage.TableCorrespondanceFiltre = CorrObjectBLL.FiltreCorrespondanceParCodeRecherche(VariablePartage.TableCorrespondanceFiltre, myform.textBox_search_saisie_code.Text);
                VariablePartage.TableCorrespondanceFiltre = CorrObjectBLL.RetourneSeulementNonTraite(VariablePartage.TableCorrespondanceFiltre);
                var source_ref = new BindingSource();
                source_ref.DataSource = VariablePartage.TableCorrespondanceFiltre;
                myform.dataGridView_saisie.DataSource = source_ref;
                InitStructSaisie(myform.dataGridView_saisie, myform);
                InitColorSaisie(myform.dataGridView_saisie);
            }
            else
            {
                VariablePartage.TableCorrespondanceFiltre = CorrObjectBLL.FiltrerListeCorrespondance_parCPL(VariablePartage.TableCorrespondance, (string)myform.comboBox_filtre.SelectedValue);
                VariablePartage.TableCorrespondanceFiltre = CorrObjectBLL.FiltreCorrespondanceParCodeRecherche(VariablePartage.TableCorrespondanceFiltre, myform.textBox_search_saisie_code.Text);
                var source = new BindingSource();
                source.DataSource = VariablePartage.TableCorrespondanceFiltre;
                myform.dataGridView_saisie.DataSource = source;
                InitStructSaisie(myform.dataGridView_saisie, myform);
                InitColorSaisie(myform.dataGridView_saisie);
            }
        }

        public void MenuQuitter()
        {
            if (System.Windows.Forms.Application.MessageLoop)
            {
                // WinForms app
                System.Windows.Forms.Application.Exit();
            }
            else
            {
                // Console app
                System.Environment.Exit(1);
            }
        }

        public void ColorComboBox(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            // Get the item text    
            string text = ((ComboBox)sender).Items[e.Index].ToString();
            //(string)comboBox_filtre.SelectedValue
            // Determine the forecolor based on whether or not the item is selected    
            Brush brush;

            //Filtre la table de corresp globale par CPL
            List<CorrespondanceBLL> CorrModule = new List<CorrespondanceBLL>();
            CorrModule = CorrObjectBLL.FiltrerListeCorrespondance_parNOMREF(VariablePartage.TableCorrespondance, text);

            if ((int)CorrObjectBLL.RetourneEtatAvancementRecodage(CorrModule) == 100)// compare  date with your list.  
            {
                brush = Brushes.Green;
            }
            else if (text == "-----Administratif-----" || text == "-------Medical---------")
            {
                brush = Brushes.Black;
            }
            else
            {
                brush = Brushes.Red;
            }

            // Draw the text    
            e.Graphics.DrawString(text, ((Control)sender).Font, brush, e.Bounds.X, e.Bounds.Y);
        }

        public void EffacerFiltreReferentiel(Form1 myform)
        {
            myform.textBox_search_referentiel_code.Text = "";
            myform.textBox_search.Text = "";
            myform.textBox_search.Focus();
        }

        

        //public void AccesInterfaceAdmin(Form1 myform)
        //{
        //    if (myform.button_acces_admin.Text == "Accès interface Admin")
        //    {
        //        myform.textBox_pass_admin.Visible = true;
        //        myform.button_okmdpadmin.Visible = true;
        //        myform.button_acces_admin.Text = "Fermer interface Admin";
        //    }
        //    else if (myform.button_acces_admin.Text == "Fermer interface Admin")
        //    {
        //        myform.textBox_pass_admin.Text = "";
        //        myform.textBox_pass_admin.Visible = false;
        //        myform.button_okmdpadmin.Visible = false;
        //        myform.groupBox_admin.Visible = false;
        //        myform.button_acces_admin.Text = "Accès interface Admin";
        //    }
        //}

        public void ExporteCorrespondance(InterfaceAdmin myform, Chargement myformchargement)
        {
            CorrespondanceDAL CorrObjectDAL = new CorrespondanceDAL();
            myformchargement.progressBar_chargement.Maximum = VariablePartage.TableCorrespondance.Count;
            //InactiveControlExceptParamButton(myform.button_exportCorresp, myform);
            if (InfoSqlServer.SQLServerConnectionString != null)
            {
                Thread export = new Thread(CorrObjectDAL.ExporteCorrespondance);
                export.Start(myformchargement);
            }
            else
            {
                MessageBox.Show("Veuillez définir la base SQL server export via Param Base");
                //ReactiveButton(myform);
            }
        }

        public void RaccourciClavierSaisie(object sender, KeyEventArgs e, Form1 myform)
        {
            ActionRecodage ActionRecodage = new ActionRecodage();
            if (e.KeyCode == Keys.Delete)
            {
                ActionRecodage.DeleteButton(myform.dataGridView_saisie, myform.dataGridView_ref, myform);
            }
            if (e.KeyCode == Keys.Space)
            {
                ActionRecodage.AffecteCode(myform.dataGridView_saisie, myform.dataGridView_ref, myform, (string)myform.comboBox_filtre.SelectedValue);
            }
            if (e.KeyCode == Keys.N)
            {
                ActionRecodage.NePasReprendre(myform.dataGridView_saisie, myform.dataGridView_ref, myform);
            }
            if (e.KeyCode == Keys.C)
            {
                ActionRecodage.CreerCodeMemeCodeMemeLibelle(myform.dataGridView_saisie, myform.dataGridView_ref, myform, false);
            }
            if (e.KeyCode == Keys.F2)
            {
                AfficherCreationCode_Datagrid(myform);
            }
            if (e.KeyCode == Keys.A)
            {
                ActionRecodage.CreerCodeMemeCodeMemeLibelle(myform.dataGridView_saisie, myform.dataGridView_ref, myform, true);
            }
        }

        

        public void AfficherCreationCode_Datagrid(Form1 myform)
        {
            if (myform.dataGridView_saisie.CurrentCell != null && myform.dataGridView_saisie.CurrentCell.RowIndex != -1)
            {
                if(myform.dataGridView_saisie.CurrentCell.ColumnIndex == 4 && myform.dataGridView_saisie.SelectedCells.Count == 1)
                {

                    InterfaceCreerCodeAvance interfaceCreerCodeAvance = new InterfaceCreerCodeAvance(myform);
                    try
                    {

                        interfaceCreerCodeAvance.textBox_ancien_code.Text = myform.dataGridView_saisie.Rows[myform.dataGridView_saisie.CurrentRow.Index].Cells[1].Value.ToString();
                        interfaceCreerCodeAvance.textBox_ancien_code.ReadOnly = true;
                        interfaceCreerCodeAvance.textBox_libelle_ancien_code.Text = myform.dataGridView_saisie.Rows[myform.dataGridView_saisie.CurrentRow.Index].Cells[2].Value.ToString();
                        interfaceCreerCodeAvance.textBox_libelle_ancien_code.ReadOnly = true;
                        interfaceCreerCodeAvance.textBox_nouveau_code.Text = myform.dataGridView_saisie.Rows[myform.dataGridView_saisie.CurrentRow.Index].Cells[1].Value.ToString();
                        interfaceCreerCodeAvance.textBox_libelle_nouveau_code.Text = myform.dataGridView_saisie.Rows[myform.dataGridView_saisie.CurrentRow.Index].Cells[2].Value.ToString();
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Erreur : " + e.Message);
                    }
                    interfaceCreerCodeAvance.ShowDialog(myform);
                }
                else
                {
                    MessageBox.Show("Seule la cellule 'Nouveau_code' est selectionnable, une cellule à la fois.");
                }

            }
        }

       

        /// <summary>
        /// Ce delegate me permet de communiquer le pourcentage d'avancement du recodage.
        /// </summary>
        /// <param name="valeur"></param>
        public delegate void MontrerAvancement(float valeur, Form1 myform);

        /// <summary>
        /// Cette méthode me permet d'appeler la méthode Avancement_rapprochement() 
        /// via le delegate MontrerAvancement() 
        /// Ce qui revient à afficher l'avancement du recodage, du module en cours, via un pourcentage.
        /// </summary>
        /// <param name="value"></param>
        private void UpdateAvancement_rapprochement(float value,Form1 myform)
        {
            try
            {
                //On invoque le delegate pour qu'il effectue la tâche sur le temps
                //de l'autre thread.
                myform.Invoke((MontrerAvancement)Avancement_rapprochement, value, myform);
            }
            catch (Exception ex) { return; }
        }

        /// <summary>
        /// Met à jour le Text du Label 'label_avancement_module_pourcentage' 
        /// à partir d'un paramètre de type float.
        /// </summary>
        /// <param name="valeur"></param>
        public void Avancement_rapprochement(float valeur,Form1 myform)
        {
            //On met la valeur dans le contrôle Windows Forms.
            myform.label_avancement_module_pourcentage.Text = valeur.ToString() + "%";
        }

        /// <summary>
        /// Cette méthode me permet d'appeler la méthode Avancement_rapprochement_global() 
        /// via le delegate MontrerAvancement() 
        /// Ce qui revient à afficher l'avancement du recodage global via un pourcentage.
        /// </summary>
        /// <param name="value"></param>
        private void UpdateAvancement_rapprochement_global(float value, Form1 myform)
        {
            try
            {
                //On invoque le delegate pour qu'il effectue la tâche sur le temps
                //de l'autre thread.
                myform.Invoke((MontrerAvancement)Avancement_rapprochement_global, value, myform);
            }
            catch (Exception ex) { return; }
        }

        /// <summary>
        /// Met à jour le Label 'label_avancement_recodage_global' 
        /// à partir d'un paramètre de type float.
        /// </summary>
        /// <param name="valeur"></param>
        public void Avancement_rapprochement_global(float valeur, Form1 myform)
        {
            //On met la valeur dans le contrôle Windows Forms.
            myform.label_avancement_recodage_global.Text = valeur.ToString() + "%";
        }


        public void Avancement_rapprochement_thread(object args)
        {
            Form1 myform = (Form1)args;
            while (1 == 1)
            {
                UpdateAvancement_rapprochement(CorrObjectBLL.RetourneEtatAvancementRecodage(VariablePartage.TableCorrespondanceFiltre), myform);
                Thread.Sleep(200);
            }
        }

        public void Avancement_rapprochement_global_thread(object args)
        {
            Form1 myform = (Form1)args;
            while (1 == 1)
            {
                UpdateAvancement_rapprochement_global(CorrObjectBLL.RetourneEtatAvancementRecodage(VariablePartage.TableCorrespondance), myform);
                Thread.Sleep(200);
            }
        }

    }
}
