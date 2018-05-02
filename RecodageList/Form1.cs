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
using RecodageList.Fonction;
using Converter;
using DbAccess;
using System.IO;

namespace RecodageList
{
    public partial class Form1 : Form
    {

        //delegate void ControlArgReturningVoidDelegate(Control button);
        /*
        static Referentiels referentiel = new Referentiels();
        static List<Referentiel> TableRef = referentiel.ObtenirListeReferentiel_SQLITE();

        static Correspondances Corr = new Correspondances();
        static List<Correspondance> TableCorresp = Corr.ObtenirListeCorrespondance_SQLITE(TableRef);
        //static List<Correspondance> TableCorresp = new List<Correspondance>();
        static List<Correspondance> Init.TableCorrespondanceFiltre = Corr.ObtenirListeCorrespondance_SQLITE(TableRef);

        //static CorrespondancesAffichages CorrAffichage = new CorrespondancesAffichages();
        //static List<CorrespondanceAffichage> TableCorrespAffichage = CorrAffichage.ObtenirListeCorrespondanceAffichage(TableCorresp);

        
        //static ReferentielsAffichages RefAffichage = new ReferentielsAffichages();
        //static List<ReferentielAffichage> TableRefAffichage = RefAffichage.ObtenirListeReferentielAffichage(TableRef);

        static List<Referentiel> Init.TableReferentielFiltre = new List<Referentiel>();

        static ComboBoxFiltres combobox_object = new ComboBoxFiltres();
        static List<ComboBoxFiltre> comboboxfiltre = combobox_object.ObtenirComboBoxFiltre();
        */
        public void InitTable()
        {
            /*
            if(InfoSqlServer.SQLServerConnectionString!=null)
            {
                Console.WriteLine("InfoSqlServer.SQLServerConnectionString : " + InfoSqlServer.SQLServerConnectionString);
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Remove("SQLServerConnectionString");
                config.AppSettings.Settings.Add("SQLServerConnectionString", InfoSqlServer.SQLServerConnectionString);
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            */

            if(File.Exists(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\PREVTGXV7.db"))
            {
                progressBar_admin.Maximum = 6;
                progressBar_admin.Value = 0;
                Referentiels RefObject = new Referentiels();
                Init.TableReferentiel = RefObject.ObtenirListeReferentiel_SQLITE();
                progressBar_admin.Value++;
                Init.TableReferentielFiltre = new List<Referentiel>();
                progressBar_admin.Value++;
                Correspondances CorrObject = new Correspondances();
                Init.TableCorrespondance = CorrObject.ObtenirListeCorrespondance_SQLITE(Init.TableReferentiel);
                progressBar_admin.Value++;
                Init.TableCorrespondanceFiltre = new List<Correspondance>();
                progressBar_admin.Value++;
                ComboBoxFiltres ComboObject = new ComboBoxFiltres();
                Init.ComboBoxFiltre = ComboObject.ObtenirComboBoxFiltre();
                progressBar_admin.Value++;
            }

        }
        Correspondances CorrObject = new Correspondances();
        public Form1()
        {
            InitializeComponent();
            
            CorrObject.UpdateProgress_admin += UpdateProgress_admin;
            CorrObject.UpdateProgress_admin_Terminate += UpdateProgress_admin_Teminate;
            //InitTable();
            //InitDatagridSaisie();
            //InitDatagridReferentiel();
            //InitComboBoxFiltre();
            //TestCanonique();

        }

        //On crée notre delagate.
        public delegate void MontrerProgres(int valeur);
        public delegate void MontrerProgresTerminate(bool terminate);
        public delegate void MontrerProgresTerminate_rapprochement(int nbItemRapprochés);


        private void UpdateProgress_rapprochement(int value)
        {
            try
            {
                //On invoque le delegate pour qu'il effectue la tâche sur le temps
                //de l'autre thread.
                Invoke((MontrerProgres)Progres_rapprochement, value);
            }
            catch (Exception ex) { return; }

            //progressBar_admin.Value = value;

        }

        private void UpdateProgress_rapprochement_terminate(int nbItemRapprochés)
        {
            try
            {
                //On invoque le delegate pour qu'il effectue la tâche sur le temps
                //de l'autre thread.
                Invoke((MontrerProgresTerminate_rapprochement)Progres_rapprochement_terminate, nbItemRapprochés);
            }
            catch (Exception ex) { return; }

            //progressBar_admin.Value = value;

        }

        private void UpdateProgress_rapprochement_module_terminate(int nbItemRapprochés)
        {
            try
            {
                //On invoque le delegate pour qu'il effectue la tâche sur le temps
                //de l'autre thread.
                Invoke((MontrerProgresTerminate_rapprochement)Progres_rapprochement_module_terminate, nbItemRapprochés);
            }
            catch (Exception ex) { return; }

            //progressBar_admin.Value = value;

        }

        private void UpdateProgress_admin(int value)
        {
            try
            {
                //On invoque le delegate pour qu'il effectue la tâche sur le temps
                //de l'autre thread.
                Invoke((MontrerProgres)Progres_admin, value);
            }
            catch (Exception ex) { return; }

            //progressBar_admin.Value = value;

        }


        private void UpdateProgress_admin_Teminate(bool terminate)
        {
            try
            {
                Invoke((MontrerProgresTerminate)ProgresTerminate, terminate);
                Invoke((MontrerProgres)Progres_admin, 0);
            }
            catch(Exception ex) { return; }
        }

        public void Progres_rapprochement(int valeur)
        {
            //On met la valeur dans le contrôle Windows Forms.
            progressBar_rapprochement.Value = valeur;
        }
        public void Progres_admin(int valeur)
        {
            //On met la valeur dans le contrôle Windows Forms.
            progressBar_admin.Value = valeur;
        }
        public void ProgresTerminate(bool terminate)
        {
            //On met la valeur dans le contrôle Windows Forms.
            if(terminate)
            {
                MessageBox.Show("Export terminé");
            }
        }
        public void Progres_rapprochement_terminate(int nbItemRapprochés)
        {
            //On met la valeur dans le contrôle Windows Forms.

                var source = new BindingSource();
                source.DataSource = Init.TableCorrespondance;

                //dataGridView_saisie.BeginInvoke(new Action(() => dataGridView_saisie.DataSource = source));

                dataGridView_saisie.DataSource = source;

                InitStructSaisie();
                InitColorSaisie();
                MessageBox.Show("Rapprochement automatique terminé. \r\n" + nbItemRapprochés.ToString() + " code(s) rapproché(s) automatiquement.");
                //UpdateProgress_rapprochement_terminate(nbItemRapprochés);
                //progressBar_rapprochement.Value = 0;
                button_deleteRecodage.Enabled = true;
                button_afficherCreationCode.Enabled = true;
                button_affecter.Enabled = true;
            progressBar_rapprochement.Value = 0;
                //MessageBox.Show("Export terminé");
        }

        public void Progres_rapprochement_module_terminate(int nbItemRapprochés)
        {
            //On met la valeur dans le contrôle Windows Forms.


            var source = new BindingSource();
            source.DataSource = Init.TableCorrespondanceFiltre;

            //dataGridView_saisie.BeginInvoke(new Action(() => dataGridView_saisie.DataSource = source));

            dataGridView_saisie.DataSource = source;

            InitStructSaisie();
            InitColorSaisie();
            MessageBox.Show("Rapprochement automatique terminé. \r\n" + nbItemRapprochés.ToString() + " code(s) rapproché(s) automatiquement.");
            progressBar_rapprochement.Value = 0;
            button_deleteRecodage.Enabled = true;
            button_afficherCreationCode.Enabled = true;
            button_affecter.Enabled = true;
        }

        /*
        public void InitFlagPreventielTbCorrespondance()
        {
            Correspondances corr = new Correspondances();
            corr.AddFlagPreventielToTbCorrespondance();
        }
        */
        public void InitDatagridSaisie()
        {
            var source = new BindingSource();
            source.DataSource = Init.TableCorrespondance;
            dataGridView_saisie.DataSource = source;



            //dataGridView_saisie.DataSource = source;

            //dataGridView_saisie.Columns["Ancien_Code"].Name = "Code Source";

            //dataGridView_saisie.Rows[4].DefaultCellStyle.BackColor = Color.Red;
            InitStructSaisie();
            InitColorSaisie();


            /*
            dataGridView_saisie.Columns.Remove("TypeItem");
            dataGridView_saisie.Columns.Remove("AncienCodeActif");
            dataGridView_saisie.Columns.Remove("Code_utilise");
            dataGridView_saisie.Columns.Remove("NomSchema");
            dataGridView_saisie.Columns.Remove("DateRecensement");
            dataGridView_saisie.Columns.Remove("DateMAJ");
            dataGridView_saisie.Columns.Remove("TypeRecodage");
            dataGridView_saisie.Columns.Remove("UtilisateurCreation");
            dataGridView_saisie.Columns.Remove("TypeRef");
            dataGridView_saisie.Columns.Remove("NomRef");
            dataGridView_saisie.Columns.Remove("Cpl");
            dataGridView_saisie.Columns.Remove("Cpl1");
            dataGridView_saisie.Columns.Remove("Cpl2");
            dataGridView_saisie.Columns.Remove("Canonical");
            */

        }

        public void InitStructSaisie()
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

            dataGridView_saisie.Columns["Ancien_Code"].HeaderText = "Code Source";
            dataGridView_saisie.Columns["Libelle_Ancien_Code"].HeaderText = "Intitulé Source";
            dataGridView_saisie.Columns["Nouveau_Code"].HeaderText = "Code Référentiel";
            dataGridView_saisie.Columns["Libelle_Nouveau_Code"].HeaderText = "Intitulé Référentiel";
            dataGridView_saisie.Columns["NouveauCodeInactif"].HeaderText = "Inactif";

            //dataGridView_saisie.AutoResizeColumns();

            // Configure the details DataGridView so that its columns automatically
            // adjust their widths when the data changes.
            //dataGridView_saisie.AutoSizeColumnsMode =
              //  DataGridViewAutoSizeColumnsMode.AllCells;

           //if(dataGridView_saisie.Columns["Libelle_Ancien_Code"].Width > 300)
            //{
                dataGridView_saisie.Columns["Libelle_Ancien_Code"].Width = 300;
            //}
            //if (dataGridView_saisie.Columns["Libelle_Nouveau_Code"].Width > 300)
            //{
                dataGridView_saisie.Columns["Libelle_Nouveau_Code"].Width = 300;
            //}
        }
        /*
        public void InitColorSaisie()
        {
            foreach (DataGridViewRow row in dataGridView_saisie.Rows)
            {
                if(row.Index!=1)
                {
                    //MessageBox.Show("Dans init color dataGridView_saisie.CurrentRow.Cells[20].Value.ToString() : " + dataGridView_saisie.CurrentRow.Cells[20].Value.ToString());
                    MessageBox.Show("Dans init color row.Cells[20].Value : rowindex = " + row.Index + " value flag = " + row.Cells[20].Value.ToString());
                    Console.WriteLine("row.Cells[20].Value : rowindex = " + row.Index + " value flag = " + row.Cells[20].Value);
                    if (Convert.ToInt32(row.Cells[20].Value) == 1 && row.Cells[12].Value.Equals(false))
                    {
                        DataGridViewCellStyle style = new DataGridViewCellStyle();
                        style.Font = new Font(dataGridView_saisie.Font, FontStyle.Regular);
                        row.DefaultCellStyle = style;
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    if (Convert.ToInt32(row.Cells[20].Value) == 1 && row.Cells[12].Value.Equals(true))
                    {
                        DataGridViewCellStyle style = new DataGridViewCellStyle();
                        style.Font = new Font(dataGridView_saisie.Font, FontStyle.Italic);
                        row.DefaultCellStyle = style;
                        row.DefaultCellStyle.BackColor = Color.Gray;
                    }
                    if (Convert.ToInt32(row.Cells[20].Value) == 2 && row.Cells[12].Value.Equals(false))
                    {
                        DataGridViewCellStyle style = new DataGridViewCellStyle();
                        style.Font = new Font(dataGridView_saisie.Font, FontStyle.Regular);
                        row.DefaultCellStyle = style;
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    if (Convert.ToInt32(row.Cells[20].Value) == 2 && row.Cells[12].Value.Equals(true))
                    {
                        DataGridViewCellStyle style = new DataGridViewCellStyle();
                        style.Font = new Font(dataGridView_saisie.Font, FontStyle.Italic);
                        row.DefaultCellStyle = style;
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    if (Convert.ToInt32(row.Cells[20].Value) == 0)
                    {
                        DataGridViewCellStyle style = new DataGridViewCellStyle();
                        style.Font = new Font(dataGridView_saisie.Font, FontStyle.Regular);
                        row.DefaultCellStyle = style;
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
        }*/

        public void InitColorSaisie()
        {
            for(int i=0;i< dataGridView_saisie.RowCount;i++)
            {
                //MessageBox.Show("Dans init color dataGridView_saisie.CurrentRow.Cells[20].Value.ToString() : " + dataGridView_saisie.CurrentRow.Cells[20].Value.ToString());
                //MessageBox.Show("Dans init color dataGridView_saisie.Rows[i].Value : rowindex = " + dataGridView_saisie.Rows[i].Index + " value flag = " + dataGridView_saisie.Rows[i].Cells[20].Value.ToString());
                if (Convert.ToInt32(dataGridView_saisie.Rows[i].Cells[20].Value) == 1 && dataGridView_saisie.Rows[i].Cells[12].Value.Equals(false))
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(dataGridView_saisie.Font, FontStyle.Regular);
                    dataGridView_saisie.Rows[i].DefaultCellStyle = style;
                    dataGridView_saisie.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                if (Convert.ToInt32(dataGridView_saisie.Rows[i].Cells[20].Value) == 1 && dataGridView_saisie.Rows[i].Cells[12].Value.Equals(true))
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(dataGridView_saisie.Font, FontStyle.Italic);
                    dataGridView_saisie.Rows[i].DefaultCellStyle = style;
                    dataGridView_saisie.Rows[i].DefaultCellStyle.BackColor = Color.Gray;
                }
                if (Convert.ToInt32(dataGridView_saisie.Rows[i].Cells[20].Value) == 2 && dataGridView_saisie.Rows[i].Cells[12].Value.Equals(false))
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(dataGridView_saisie.Font, FontStyle.Regular);
                    dataGridView_saisie.Rows[i].DefaultCellStyle = style;
                    dataGridView_saisie.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
                if (Convert.ToInt32(dataGridView_saisie.Rows[i].Cells[20].Value) == 2 && dataGridView_saisie.Rows[i].Cells[12].Value.Equals(true))
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(dataGridView_saisie.Font, FontStyle.Italic);
                    dataGridView_saisie.Rows[i].DefaultCellStyle = style;
                    dataGridView_saisie.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
                if (Convert.ToInt32(dataGridView_saisie.Rows[i].Cells[20].Value) == 0)
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.Font = new Font(dataGridView_saisie.Font, FontStyle.Regular);
                    dataGridView_saisie.Rows[i].DefaultCellStyle = style;
                    dataGridView_saisie.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
            }
                
            
        }

        public void InitDatagridReferentiel()
        {
            var source = new BindingSource();
            source.DataSource = Init.TableReferentiel;
            dataGridView_ref.DataSource = source;

            InitStructReferentiel();
            /*
            dataGridView_ref.Columns["Type"].Visible = false;
            dataGridView_ref.Columns["TypeItem"].Visible = false;
            dataGridView_ref.Columns["CodeOrigine"].Visible = false;
            //dataGridView_ref.Columns["InActif"].Visible = false;
            dataGridView_ref.Columns["Cpl"].Visible = false;
            dataGridView_ref.Columns["Cpl1"].Visible = false;
            dataGridView_ref.Columns["Cpl2"].Visible = false;
            dataGridView_ref.Columns["Cpl3"].Visible = false;
            dataGridView_ref.Columns["DateFinValidite"].Visible = false;

            dataGridView_ref.Columns["Code"].HeaderText = "Code Référentiel";
            dataGridView_ref.Columns["Lib"].HeaderText = "Intitulé Référentiel";
            dataGridView_ref.Columns["InActif"].HeaderText = "Inactif";
            */
            /*
            dataGridView_ref.Columns.Remove("Type");
            dataGridView_ref.Columns.Remove("TypeItem");
            dataGridView_ref.Columns.Remove("CodeOrigine");
            dataGridView_ref.Columns.Remove("InActif");
            dataGridView_ref.Columns.Remove("Cpl");
            dataGridView_ref.Columns.Remove("Cpl1");
            dataGridView_ref.Columns.Remove("Cpl2");
            dataGridView_ref.Columns.Remove("Cpl3");
            dataGridView_ref.Columns.Remove("DateFinValidite");
            */

            //dataGridView_ref.Columns["IndiceLevenshtein"].Visible = false;
        }

        public void InitStructReferentiel()
        {
            dataGridView_ref.Columns["Type"].Visible = false;
            dataGridView_ref.Columns["TypeItem"].Visible = false;
            dataGridView_ref.Columns["CodeOrigine"].Visible = false;
            //dataGridView_ref.Columns["InActif"].Visible = false;
            dataGridView_ref.Columns["Cpl"].Visible = false;
            dataGridView_ref.Columns["Cpl1"].Visible = false;
            dataGridView_ref.Columns["Cpl2"].Visible = false;
            dataGridView_ref.Columns["Cpl3"].Visible = false;
            dataGridView_ref.Columns["DateFinValidite"].Visible = false;
            dataGridView_ref.Columns["Canonical"].Visible = false;

            dataGridView_ref.Columns["Code"].HeaderText = "Code Référentiel";
            dataGridView_ref.Columns["Lib"].HeaderText = "Intitulé Référentiel";
            dataGridView_ref.Columns["InActif"].HeaderText = "Inactif";

            //dataGridView_ref.AutoResizeColumns();

            // Configure the details DataGridView so that its columns automatically
            // adjust their widths when the data changes.
            //dataGridView_ref.AutoSizeColumnsMode =
                //DataGridViewAutoSizeColumnsMode.AllCells;
            //if (dataGridView_ref.Columns["Lib"].Width > 300)
            //{
                dataGridView_ref.Columns["Lib"].Width = dataGridView_saisie.Columns["Libelle_Ancien_Code"].Width + dataGridView_saisie.Columns["Nouveau_Code"].Width + dataGridView_saisie.Columns["Libelle_Nouveau_Code"].Width;
            //}


        }
        /*
        public void CreationAffichageThread()
        {
            while (1==1)
            {
                if(dataGridView_saisie.CurrentCell.ColumnIndex.Equals(4) && dataGridView_saisie.CurrentCell.RowIndex != -1 && dataGridView_saisie.CurrentCell!= null)
                {
                    button_afficherCreationCode.Enabled = true;
                }
                else
                {
                    button_afficherCreationCode.Enabled = false;
                }
            }
        }
        */


        public void InitComboBoxFiltre()
        {
            /*
            for(int i=0;i<comboboxfiltre.Count;i++)
            {
                comboBox_filtre.Items.Add(comboboxfiltre[i]);
            }
            */
            comboBox_filtre.DataSource = new BindingSource(Init.ComboBoxFiltre, null);
            comboBox_filtre.DisplayMember = "NomRef";
            comboBox_filtre.ValueMember = "Cpl";
            progressBar_admin.Value++;
            //MessageBox.Show();
        }
        
        public void TestCanonique()
        {
            Fonc fonc = new Fonc();
            Console.WriteLine(fonc.CanonicalString("j'ai testé comme un fouuuu"));
            Console.WriteLine(Init.TableCorrespondance[0].Canonical);
            Console.WriteLine(Init.TableCorrespondance[1].Canonical);
            Console.WriteLine(Init.TableCorrespondance[2].Canonical);
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// Affecte le nouveau code et le nouveau libellé referentiel dans la table de correspondance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_save_Click(object sender, EventArgs e)
        {
            //Fait l'affectation du nouveau code graphiquement
            dataGridView_saisie.CurrentRow.Cells[4].Value = dataGridView_ref.CurrentRow.Cells[2].Value.ToString();
            dataGridView_saisie.CurrentRow.Cells[5].Value = dataGridView_ref.CurrentRow.Cells[3].Value.ToString();
            dataGridView_saisie.CurrentRow.Cells[12].Value = dataGridView_ref.CurrentRow.Cells[5].Value;
            dataGridView_saisie.CurrentRow.Cells[20].Value = 1;


            //Fait l'affectation du nouveau code en base
            Correspondances Corresp = new Correspondances();
            Correspondance ligne = new Correspondance();
            ligne.Ancien_Code = dataGridView_saisie.CurrentRow.Cells[1].Value.ToString();
            //ligne.Libelle_Ancien_Code = dataGridView_saisie.CurrentRow.Cells[2].Value.ToString();
            ligne.Nouveau_Code = dataGridView_saisie.CurrentRow.Cells[4].Value.ToString();
            ligne.Libelle_Nouveau_Code = dataGridView_saisie.CurrentRow.Cells[5].Value.ToString();
            ligne.NomRef = dataGridView_saisie.CurrentRow.Cells[15].Value.ToString();
            ligne.NouveauCodeInactif = (bool)dataGridView_saisie.CurrentRow.Cells[12].Value;
            ligne.FlagReferentiel = 1;

            Corresp.UpdateSQLITE_TBCorrespondance(ligne);
            Corresp.UpdateSQLITE_TBCorrespondance_FlagPreventiel(ligne);


            //CorrespondancesAffichages ObjectCorrAffichage = new CorrespondancesAffichages();
            //TableCorrespAffichage = ObjectCorrAffichage.RetourneListeCorrespondanceAffichageUpdater(TableCorrespAffichage, ligne, comboBox_filtre.SelectedItem.ToString());
            Init.TableCorrespondance = Corresp.RetourneListeCorrespondanceUpdater(Init.TableCorrespondance, ligne, ligne.NomRef);

            //var source = new BindingSource();
            //source.DataSource = TableCorresp;
            //dataGridView_saisie.DataSource = source;
            //dataGridView_saisie.CurrentCell = activecell;

            InitColorSaisie();
            //dataGridView_ref.CurrentRow.Cells[0].Value.ToString()
        }

        private void button_addlambda_Click(object sender, EventArgs e)
        {
            Correspondances Corr = new Correspondances();
            List<Correspondance> TableCorresp = Corr.ObtenirListeCorrespondance_lambda();
            Corr.InsertIntoSQLITE_TBCorrespondance(TableCorresp);
            InitDatagridSaisie();
        }

        private void button_clearTable_Click(object sender, EventArgs e)
        {
            Correspondances Corr = new Correspondances();
            Corr.ClearTable();
            InitDatagridSaisie();
        }

        /// <summary>
        /// Bouton filtre, permet de filtrer la table de correspondance par module
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_filter_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("comboBox_filtre.SelectedText : " + comboBox_filtre.SelectedText);
            //Console.WriteLine("comboBox_filtre.comboBox_filtre.SelectedItem.ToString() : " + comboBox_filtre.SelectedItem.ToString());

            if((string)comboBox_filtre.SelectedValue!=null)
            {
                Correspondances Corr = new Correspondances();
                //Console.WriteLine(dataGridView_saisie.DataMember);
                //List<Correspondance> TableCorresp = Corr.ObtenirListeCorrespondance_SQLITE();
                //List<Correspondance> CorrFiltered = new List<Correspondance>();
                Console.WriteLine(comboBox_filtre.ValueMember);
                Console.WriteLine("(string)comboBox_filtre.SelectedValue : " + (string)comboBox_filtre.SelectedValue);

                //Filtre la table de corresp globale par CPL
                Init.TableCorrespondanceFiltre = Corr.FiltrerListeCorrespondance_parCPL(Init.TableCorrespondance, (string)comboBox_filtre.SelectedValue);

                //Créé une nouvelle liste de type affichage qui va etre affectée de la liste ci-dessus (liste globale filtrée).
                //CorrespondancesAffichages CorrAffichage = new CorrespondancesAffichages();
                //List<CorrespondanceAffichage> TableCorrespAffichage = CorrAffichage.ObtenirListeCorrespondanceAffichage(Init.TableCorrespondanceFiltre);

                //Réaffecte le datagrid à la liste filtrée 
                var source = new BindingSource();
                source.DataSource = Init.TableCorrespondanceFiltre;
                dataGridView_saisie.DataSource = source;
                InitStructSaisie();
                InitColorSaisie();

                Referentiels Ref = new Referentiels();
                //List<Referentiel> TableRef = Ref.ObtenirListeReferentiel_SQLITE();
                Console.WriteLine("(string)comboBox_filtre.SelectedValue : " + (string)comboBox_filtre.SelectedValue);
                Init.TableReferentielFiltre = Ref.FiltrerListeReferentiel_parCPL(Init.TableReferentiel, (string)comboBox_filtre.SelectedValue);
                //ReferentielsAffichages RefAffichage = new ReferentielsAffichages();
                //List<ReferentielAffichage> TableRefAffichage = RefAffichage.ObtenirListeReferentielAffichage(Init.TableReferentielFiltre);
                var source_ref = new BindingSource();
                source_ref.DataSource = Init.TableReferentielFiltre;
                dataGridView_ref.DataSource = source_ref;
                InitStructReferentiel();
            }
        }

  

        private void button_deletefilter_Click(object sender, EventArgs e)
        {
            InitDatagridSaisie();
            InitDatagridReferentiel();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ChargementTableEnMemoire();
            //InitDatagridSaisie();
            //Thread th = new Thread(CreationAffichageThread);
            //th.Start();
            //dataGridView_saisie
        }

        /// <summary>
        /// Est appelé lors que l'utilisateur clique sur une cellule nouveau code du tableau saisie
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_saisie_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_saisie.CurrentCell.ColumnIndex.Equals(4) && e.RowIndex != -1)
            {
                button_deleteRecodage.Enabled = true;
                button_affecter.Enabled = true;
                button_afficherCreationCode.Enabled = true;
                button_creercode.Enabled = false;
                textBox_codeacreer.Text = "";
                textBox_libellecodeacreer.Text = "";
                textBox_codeacreer.Enabled = false;
                textBox_libellecodeacreer.Enabled = false;
                checkBox_codeacreer_actif_inactif.Enabled = false;
                if (dataGridView_saisie.CurrentCell != null /*&& dataGridView_saisie.CurrentRow.Cells[2].Value != null && dataGridView_saisie.CurrentCell.Value != null*/)
                //MessageBox.Show(dataGridView_saisie.CurrentCell.Value.ToString());
                {
                    if(dataGridView_saisie.CurrentRow.Cells[16].Value.ToString()!="50" && dataGridView_saisie.CurrentRow.Cells[16].Value.ToString() != "135"
                        && dataGridView_saisie.CurrentRow.Cells[16].Value.ToString() != "60" && dataGridView_saisie.CurrentRow.Cells[16].Value.ToString() != "141")
                    {
                        Fonc fonc = new Fonc();
                        Referentiels RefObject = new Referentiels();
                        List<Referentiel> TableRefLevenshtein = new List<Referentiel>();
                        //MessageBox.Show(dataGridView_saisie.CurrentRow.Cells[3].Value.ToString());
                        if (dataGridView_saisie.CurrentRow.Cells[2].Value!=null)
                        {
                            TableRefLevenshtein = RefObject.TrierListeParLevenshtein(Init.TableReferentielFiltre, dataGridView_saisie.CurrentRow.Cells[2].Value.ToString());
                        }
                        else if(dataGridView_saisie.CurrentRow.Cells[2].Value == null)
                        {
                            TableRefLevenshtein = RefObject.TrierListeParLevenshtein(Init.TableReferentielFiltre, "");
                        }
                        var source_ref = new BindingSource();
                        source_ref.DataSource = TableRefLevenshtein;
                        dataGridView_ref.DataSource = source_ref;
                    }
                    else if (dataGridView_saisie.CurrentRow.Cells[16].Value.ToString() == "50" && dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Contains("#"))
                    {
                        string[] examen_ancien_code = dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Split('#');
                        Correspondances CorrObject = new Correspondances();
                        Correspondance ligne_exam_nouveau_code = CorrObject.RetourneCorrespondanceNouveauCode(Init.TableCorrespondanceFiltre, examen_ancien_code[0], "135|0|0|NOMEN");
                        Referentiels RefObject = new Referentiels();
                        List<Referentiel> TableRefResultatExamen = new List<Referentiel>();
                        TableRefResultatExamen = RefObject.FiltrerListeResultatExamenReferentielParExamen(Init.TableReferentiel, ligne_exam_nouveau_code.Nouveau_Code);
                        List<Referentiel> TableRefLevenshtein = new List<Referentiel>();
                        if (dataGridView_saisie.CurrentRow.Cells[2].Value != null)
                        {
                            TableRefLevenshtein = RefObject.TrierListeParLevenshtein(TableRefResultatExamen, dataGridView_saisie.CurrentRow.Cells[2].Value.ToString());
                        }
                        else if (dataGridView_saisie.CurrentRow.Cells[2].Value == null)
                        {
                            TableRefLevenshtein = RefObject.TrierListeParLevenshtein(TableRefResultatExamen, "");
                        }
                        var source_ref = new BindingSource();
                        source_ref.DataSource = TableRefLevenshtein;
                        dataGridView_ref.DataSource = source_ref;
                    }
                    else if (dataGridView_saisie.CurrentRow.Cells[16].Value.ToString() == "135")
                    {
                        Referentiels Ref = new Referentiels();
                        //List<Referentiel> TableRef = Ref.ObtenirListeReferentiel_SQLITE();
                        Console.WriteLine("(string)comboBox_filtre.SelectedValue : " + (string)comboBox_filtre.SelectedValue);
                        Init.TableReferentielFiltre = Ref.FiltrerListeReferentiel_parCPL(Init.TableReferentiel, (string)comboBox_filtre.SelectedValue);

                        //Supprime les resultat examen du referentiel
                        Init.TableReferentielFiltre = Ref.SupprimeResultatExamenReferentiel(Init.TableReferentielFiltre);

                        //ReferentielsAffichages RefAffichage = new ReferentielsAffichages();
                        //List<ReferentielAffichage> TableRefAffichage = RefAffichage.ObtenirListeReferentielAffichage(Init.TableReferentielFiltre);
                        List<Referentiel> TableRefLevenshtein = new List<Referentiel>();
                        if (dataGridView_saisie.CurrentRow.Cells[2].Value != null)
                        {
                            TableRefLevenshtein = Ref.TrierListeParLevenshtein(Init.TableReferentielFiltre, dataGridView_saisie.CurrentRow.Cells[2].Value.ToString());
                        }
                        else if (dataGridView_saisie.CurrentRow.Cells[2].Value == null)
                        {
                            TableRefLevenshtein = Ref.TrierListeParLevenshtein(Init.TableReferentielFiltre, "");
                        }
                        var source_ref = new BindingSource();
                        source_ref.DataSource = Init.TableReferentielFiltre;
                        dataGridView_ref.DataSource = source_ref;
                        InitStructReferentiel();
                    }
                    else if (dataGridView_saisie.CurrentRow.Cells[16].Value.ToString() == "60" && dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Contains("#"))
                    {
                        string[] vaccin_ancien_code = dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Split('#');
                        Correspondances CorrObject = new Correspondances();
                        Correspondance ligne_vacin_nouveau_code = CorrObject.RetourneCorrespondanceNouveauCode(Init.TableCorrespondanceFiltre, vaccin_ancien_code[0], "141|0|0|NOMEN");
                        Referentiels RefObject = new Referentiels();
                        List<Referentiel> TableProtocoleVaccin = new List<Referentiel>();
                        TableProtocoleVaccin = RefObject.FiltrerListeResultatExamenReferentielParExamen(Init.TableReferentiel, ligne_vacin_nouveau_code.Nouveau_Code);
                        List<Referentiel> TableRefLevenshtein = new List<Referentiel>();
                        if (dataGridView_saisie.CurrentRow.Cells[2].Value != null)
                        {
                            TableRefLevenshtein = RefObject.TrierListeParLevenshtein(TableProtocoleVaccin, dataGridView_saisie.CurrentRow.Cells[2].Value.ToString());
                        }
                        else if (dataGridView_saisie.CurrentRow.Cells[2].Value == null)
                        {
                            TableRefLevenshtein = RefObject.TrierListeParLevenshtein(TableProtocoleVaccin, "");
                        }
                        var source_ref = new BindingSource();
                        source_ref.DataSource = TableRefLevenshtein;
                        dataGridView_ref.DataSource = source_ref;
                    }
                    else if (dataGridView_saisie.CurrentRow.Cells[16].Value.ToString() == "141")
                    {
                        Referentiels Ref = new Referentiels();
                        //List<Referentiel> TableRef = Ref.ObtenirListeReferentiel_SQLITE();
                        Console.WriteLine("(string)comboBox_filtre.SelectedValue : " + (string)comboBox_filtre.SelectedValue);
                        Init.TableReferentielFiltre = Ref.FiltrerListeReferentiel_parCPL(Init.TableReferentiel, (string)comboBox_filtre.SelectedValue);
                        //ReferentielsAffichages RefAffichage = new ReferentielsAffichages();
                        //List<ReferentielAffichage> TableRefAffichage = RefAffichage.ObtenirListeReferentielAffichage(Init.TableReferentielFiltre);
                        List<Referentiel> TableRefLevenshtein = new List<Referentiel>();
                        if (dataGridView_saisie.CurrentRow.Cells[2].Value != null)
                        {
                            TableRefLevenshtein = Ref.TrierListeParLevenshtein(Init.TableReferentielFiltre, dataGridView_saisie.CurrentRow.Cells[2].Value.ToString());
                        }
                        else if (dataGridView_saisie.CurrentRow.Cells[2].Value == null)
                        {
                            TableRefLevenshtein = Ref.TrierListeParLevenshtein(Init.TableReferentielFiltre, "");
                        }
                        var source_ref = new BindingSource();
                        source_ref.DataSource = Init.TableReferentielFiltre;
                        dataGridView_ref.DataSource = source_ref;
                        InitStructReferentiel();
                    }
                }
            }
            else
            {

                button_deleteRecodage.Enabled = false;
                button_affecter.Enabled = false;
                button_afficherCreationCode.Enabled = false;
                button_creercode.Enabled = false;
                textBox_codeacreer.Text = "";
                textBox_libellecodeacreer.Text = "";
                textBox_codeacreer.Enabled = false;
                textBox_libellecodeacreer.Enabled = false;
                checkBox_codeacreer_actif_inactif.Enabled = false;
            }
        }

        private void dataGridView_ref_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView_ref_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        /// <summary>
        /// Est appelé lors de l'appuie sur le bouton Affichage Creation
        /// Permet d'activer le module de creation de code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_afficherCreationCode_Click(object sender, EventArgs e)
        {
            if (button_afficherCreationCode.Text == "Création code")
            {
                button_afficherCreationCode.Text = "Annuler";
                button_affecter.Enabled = false;
                textBox_codeacreer.Enabled = true;
                textBox_libellecodeacreer.Enabled = true;
                checkBox_codeacreer_actif_inactif.Enabled = true;
                button_creercode.Enabled = true;
                button_deleteRecodage.Enabled = false;
                button_rapprochementmodule.Enabled = false;

                textBox_codeacreer.Text = dataGridView_saisie.CurrentRow.Cells[1].Value.ToString();
                if (dataGridView_saisie.CurrentRow.Cells[2].Value != null)
                {
                    textBox_libellecodeacreer.Text = dataGridView_saisie.CurrentRow.Cells[2].Value.ToString();
                }
                else
                {
                    textBox_libellecodeacreer.Text = "";
                }
                checkBox_codeacreer_actif_inactif.Checked = true;
            }
            else if(button_afficherCreationCode.Text == "Annuler")
            {
                button_afficherCreationCode.Text = "Création code";
                button_affecter.Enabled = true;
                button_afficherCreationCode.Enabled = true;
                button_creercode.Enabled = false;
                textBox_codeacreer.Text = "";
                textBox_libellecodeacreer.Text = "";
                textBox_codeacreer.Enabled = false;
                textBox_libellecodeacreer.Enabled = false;
                checkBox_codeacreer_actif_inactif.Enabled = false;
                button_deleteRecodage.Enabled = true;
                button_rapprochementmodule.Enabled = true;
            }
                
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
            if(textBox_codeacreer.Text!="" && textBox_libellecodeacreer.Text!="")
            {
                dataGridView_saisie.CurrentRow.Cells[4].Value = textBox_codeacreer.Text;
                dataGridView_saisie.CurrentRow.Cells[5].Value = textBox_libellecodeacreer.Text;
                dataGridView_saisie.CurrentRow.Cells[12].Value = checkBox_codeacreer_actif_inactif.Checked;
                dataGridView_saisie.CurrentRow.Cells[20].Value = 2;

                //Fait l'affectation du nouveau code en base
                Correspondances Corresp = new Correspondances();
                Correspondance ligne = new Correspondance();
                ligne.Ancien_Code = dataGridView_saisie.CurrentRow.Cells[1].Value.ToString();
                //ligne.Libelle_Ancien_Code = dataGridView_saisie.CurrentRow.Cells[2].Value.ToString();
                ligne.Nouveau_Code = dataGridView_saisie.CurrentRow.Cells[4].Value.ToString();
                ligne.Libelle_Nouveau_Code = dataGridView_saisie.CurrentRow.Cells[5].Value.ToString();
                ligne.NomRef = dataGridView_saisie.CurrentRow.Cells[15].Value.ToString();
                //ligne.NomRef = comboBox_filtre.SelectedItem.ToString();
                ligne.FlagReferentiel = 2;
                ligne.NouveauCodeInactif = (bool)dataGridView_saisie.CurrentRow.Cells[12].Value;

                Corresp.UpdateSQLITE_TBCorrespondance(ligne);
                Corresp.UpdateSQLITE_TBCorrespondance_FlagPreventiel(ligne);


                //CorrespondancesAffichages ObjectCorrAffichage = new CorrespondancesAffichages();
                //TableCorrespAffichage = ObjectCorrAffichage.RetourneListeCorrespondanceAffichageUpdater(TableCorrespAffichage, ligne, comboBox_filtre.SelectedItem.ToString());
                Init.TableCorrespondance = Corresp.RetourneListeCorrespondanceUpdater(Init.TableCorrespondance, ligne, dataGridView_saisie.CurrentRow.Cells[15].Value.ToString());

                //var source = new BindingSource();
                //source.DataSource = TableCorresp;
                //dataGridView_saisie.DataSource = source;
                //dataGridView_saisie.CurrentCell = activecell;

                button_affecter.Enabled = true;
                button_afficherCreationCode.Enabled = true;
                button_creercode.Enabled = false;
                textBox_codeacreer.Text = "";
                textBox_libellecodeacreer.Text = "";
                textBox_codeacreer.Enabled = false;
                textBox_libellecodeacreer.Enabled = false;
                checkBox_codeacreer_actif_inactif.Enabled = false;
                button_afficherCreationCode.Text = "Création code";
                button_rapprochementmodule.Enabled = true;
                //MessageBox.Show("Avant init color : " + dataGridView_saisie.CurrentRow.Cells[20].Value.ToString());
                //InitColorSaisie();
                InitColorSaisie();
                //MessageBox.Show("Apres init color : " + dataGridView_saisie.CurrentRow.Cells[20].Value.ToString());
            }
            else
            {
                MessageBox.Show("Code et Libellé obligatoire.");
            }
        }

        private void comboBox_filtre_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void button_deleteRecodage_Click(object sender, EventArgs e)
        {
            DeleteButton();
        }

        public void DeleteButton()
        {

            dataGridView_saisie.CurrentRow.Cells[4].Value = "";
            dataGridView_saisie.CurrentRow.Cells[5].Value = "";
            dataGridView_saisie.CurrentRow.Cells[12].Value = false;
            dataGridView_saisie.CurrentRow.Cells[20].Value = 0;

            //Fait l'affectation du nouveau code en base
            Correspondances Corresp = new Correspondances();
            Correspondance ligne = new Correspondance();
            ligne.Ancien_Code = dataGridView_saisie.CurrentRow.Cells[1].Value.ToString();
            //ligne.Libelle_Ancien_Code = dataGridView_saisie.CurrentRow.Cells[2].Value.ToString();
            ligne.Nouveau_Code = "";
            ligne.Libelle_Nouveau_Code = "";
            ligne.NomRef = dataGridView_saisie.CurrentRow.Cells[15].Value.ToString();
            ligne.FlagReferentiel = 0;
            ligne.NouveauCodeInactif = false;

            Corresp.UpdateSQLITE_TBCorrespondance(ligne);
            Corresp.UpdateSQLITE_TBCorrespondance_FlagPreventiel(ligne);


            //CorrespondancesAffichages ObjectCorrAffichage = new CorrespondancesAffichages();
            //TableCorrespAffichage = ObjectCorrAffichage.RetourneListeCorrespondanceAffichageUpdater(TableCorrespAffichage, ligne, comboBox_filtre.SelectedItem.ToString());
            Init.TableCorrespondance = Corresp.RetourneListeCorrespondanceUpdater(Init.TableCorrespondance, ligne, ligne.NomRef);

            //var source = new BindingSource();
            //source.DataSource = TableCorresp;
            //dataGridView_saisie.DataSource = source;
            //dataGridView_saisie.CurrentCell = activecell;

            button_affecter.Enabled = true;
            button_afficherCreationCode.Enabled = true;
            button_creercode.Enabled = false;
            textBox_codeacreer.Text = "";
            textBox_libellecodeacreer.Text = "";
            textBox_codeacreer.Enabled = false;
            textBox_libellecodeacreer.Enabled = false;
            checkBox_codeacreer_actif_inactif.Enabled = false;

            InitColorSaisie();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void dataGridView_saisie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteButton();
            }
        }

       public void RapprochementModule(object clefValeur)
        {


            int nbItemRapprochés = 0;

            Fonc fonc = new Fonc();
            Correspondances ObjCorr = new Correspondances();
            /*
            string[] examen_ancien_code = dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Split('#');
            Correspondances CorrObject = new Correspondances();
            Correspondance ligne_exam_nouveau_code = CorrObject.RetourneCorrespondanceNouveauCode(Init.TableCorrespondanceFiltre, examen_ancien_code[0], "135|0|0|NOMEN");
            Referentiels RefObject = new Referentiels();
            List<Referentiel> TableRefResultatExamen = new List<Referentiel>();
            TableRefResultatExamen = RefObject.FiltrerListeResultatExamenReferentielParExamen(TableRef, ligne_exam_nouveau_code.Nouveau_Code);
            */
            //Init.TableCorrespondanceFiltre = TableCorresp;
            
            if ((string)clefValeur == "135|0|0|NOMEN")
            {
                //string[] examen_ancien_code = dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Split('#');

                for (int i = 0; i < Init.TableCorrespondanceFiltre.Count; i++)
                {
                    if (Init.TableCorrespondanceFiltre[i].Ancien_Code.Contains("1128"))
                        Console.WriteLine("Init.TableCorrespondanceFiltre[i].Ancien_Code : " + Init.TableCorrespondanceFiltre[i].Ancien_Code);
                    if (Init.TableCorrespondanceFiltre[i].Nouveau_Code == "" || Init.TableCorrespondanceFiltre[i].Nouveau_Code == null)
                    {
                        for (int j = 0; j < Init.TableReferentielFiltre.Count; j++)
                        {
                            if (Init.TableCorrespondanceFiltre[i].Ancien_Code.ToString().Contains("1128#") && Init.TableReferentielFiltre[j].Cpl == "50")
                            {
                                /*
                                Console.WriteLine("Init.TableCorrespondanceFiltre[i].Canonical : " + Init.TableCorrespondanceFiltre[i].Canonical);
                                Console.WriteLine("fonc.CanonicalString(Init.TableReferentielFiltre[j].Lib) : " + fonc.CanonicalString(Init.TableReferentielFiltre[j].Lib));
                                Console.WriteLine("Init.TableCorrespondanceFiltre[i].Cpl : " + Init.TableCorrespondanceFiltre[i].Cpl);
                                Console.WriteLine("Init.TableReferentielFiltre[j].Cpl : " + Init.TableReferentielFiltre[j].Cpl);
                                Console.WriteLine("Init.TableCorrespondanceFiltre[i].TypeRef : " + Init.TableCorrespondanceFiltre[i].TypeRef);
                                Console.WriteLine("Init.TableReferentielFiltre[j].Type : " + Init.TableReferentielFiltre[j].Type);
                                Console.WriteLine("Init.TableCorrespondanceFiltre[i].Ancien_Code.ToString().Contains('#') : " + Init.TableCorrespondanceFiltre[i].Ancien_Code.ToString().Contains("#"));
                                //Console.WriteLine("Init.TableCorrespondanceFiltre[i].Canonical : " + Init.TableCorrespondanceFiltre[i].Canonical);
                                */
                            }

                            if (Init.TableCorrespondanceFiltre[i].Canonical == Init.TableReferentielFiltre[j].Canonical
                                && Init.TableCorrespondanceFiltre[i].Cpl == Init.TableReferentielFiltre[j].Cpl
                                && Init.TableCorrespondanceFiltre[i].Cpl1 == Init.TableReferentielFiltre[j].Cpl1
                                && Init.TableCorrespondanceFiltre[i].Cpl2 == Init.TableReferentielFiltre[j].Cpl2
                                && Init.TableCorrespondanceFiltre[i].TypeRef == Init.TableReferentielFiltre[j].Type
                                && Init.TableCorrespondanceFiltre[i].Cpl == "135"
                                && Init.TableReferentielFiltre[j].Cpl == "135"
                                && Init.TableCorrespondanceFiltre[i].TypeRef == "NOMEN"
                                && Init.TableReferentielFiltre[j].Type == "NOMEN")
                            {
                                Init.TableCorrespondanceFiltre[i].Nouveau_Code = Init.TableReferentielFiltre[j].Code;
                                Init.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code = Init.TableReferentielFiltre[j].Lib;
                                if (Init.TableReferentielFiltre[j].InActif == true)
                                {
                                    Init.TableCorrespondanceFiltre[i].NouveauCodeInactif = true;
                                }
                                else
                                {
                                    Init.TableCorrespondanceFiltre[i].NouveauCodeInactif = false;
                                }
                                Init.TableCorrespondanceFiltre[i].FlagReferentiel = 1;
                                ObjCorr.UpdateSQLITE_TBCorrespondance(Init.TableCorrespondanceFiltre[i]);
                                ObjCorr.UpdateSQLITE_TBCorrespondance_FlagPreventiel(Init.TableCorrespondanceFiltre[i]);
                                /*
                                dataGridView_saisie.Rows[i].Cells[4].Value = Init.TableCorrespondanceFiltre[i].Nouveau_Code;
                                dataGridView_saisie.Rows[i].Cells[5].Value = Init.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code;
                                dataGridView_saisie.Rows[i].Cells[12].Value = Init.TableCorrespondanceFiltre[i].NouveauCodeInactif;
                                dataGridView_saisie.Rows[i].Cells[20].Value = Init.TableCorrespondanceFiltre[i].FlagReferentiel;
                                */
                                nbItemRapprochés++;
                            }
                            else if (Init.TableCorrespondanceFiltre[i].Canonical == Init.TableReferentielFiltre[j].Canonical
                                && Init.TableCorrespondanceFiltre[i].Cpl == Init.TableReferentielFiltre[j].Cpl
                                //&& Init.TableCorrespondanceFiltre[i].Cpl1 == Init.TableReferentielFiltre[j].Cpl1
                                //&& Init.TableCorrespondanceFiltre[i].Cpl2 == Init.TableReferentielFiltre[j].Cpl2
                                && Init.TableCorrespondanceFiltre[i].TypeRef == Init.TableReferentielFiltre[j].Type
                                && Init.TableCorrespondanceFiltre[i].Cpl == "50"
                                && Init.TableReferentielFiltre[j].Cpl == "50"
                                && Init.TableCorrespondanceFiltre[i].TypeRef == "CTRL"
                                && Init.TableReferentielFiltre[j].Type == "CTRL"
                                && Init.TableCorrespondanceFiltre[i].Ancien_Code.ToString().Contains("#"))
                            {
                                string[] examen_ancien_code = Init.TableCorrespondanceFiltre[i].Ancien_Code.ToString().Split('#');
                                Correspondances CorrObject = new Correspondances();
                                Correspondance ligne_exam_nouveau_code = CorrObject.RetourneCorrespondanceNouveauCode(Init.TableCorrespondanceFiltre, examen_ancien_code[0], "135|0|0|NOMEN");
                                Referentiels RefObject = new Referentiels();
                                List<Referentiel> TableRefResultatExamen = new List<Referentiel>();
                                TableRefResultatExamen = RefObject.FiltrerListeResultatExamenReferentielParExamen(Init.TableReferentiel, ligne_exam_nouveau_code.Nouveau_Code);
                                for (int k = 0; k < TableRefResultatExamen.Count; k++)
                                {
                                    if (TableRefResultatExamen[k].Canonical == Init.TableCorrespondanceFiltre[i].Canonical)
                                    {
                                        Init.TableCorrespondanceFiltre[i].Nouveau_Code = TableRefResultatExamen[k].Code;
                                        Init.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code = TableRefResultatExamen[k].Lib;
                                        if (TableRefResultatExamen[k].InActif == true)
                                        {
                                            Init.TableCorrespondanceFiltre[i].NouveauCodeInactif = true;
                                        }
                                        else
                                        {
                                            Init.TableCorrespondanceFiltre[i].NouveauCodeInactif = false;
                                        }
                                        Init.TableCorrespondanceFiltre[i].FlagReferentiel = 1;
                                        ObjCorr.UpdateSQLITE_TBCorrespondance(Init.TableCorrespondanceFiltre[i]);
                                        ObjCorr.UpdateSQLITE_TBCorrespondance_FlagPreventiel(Init.TableCorrespondanceFiltre[i]);
                                        /*
                                        dataGridView_saisie.Rows[i].Cells[4].Value = Init.TableCorrespondanceFiltre[i].Nouveau_Code;
                                        dataGridView_saisie.Rows[i].Cells[5].Value = Init.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code;
                                        dataGridView_saisie.Rows[i].Cells[12].Value = Init.TableCorrespondanceFiltre[i].NouveauCodeInactif;
                                        dataGridView_saisie.Rows[i].Cells[20].Value = Init.TableCorrespondanceFiltre[i].FlagReferentiel;
                                        */
                                        nbItemRapprochés++;
                                    }
                                }
                            }
                        }
                    }
                    UpdateProgress_rapprochement(i);
                    //progressBar_rapprochement.Value = i;
                    Console.WriteLine("Avancement : " + i + "/" + Init.TableCorrespondanceFiltre.Count);
                }
            }
            else if ((string)clefValeur == "141|0|0|NOMEN")
            {
                //string[] examen_ancien_code = dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Split('#');

                for (int i = 0; i < Init.TableCorrespondanceFiltre.Count; i++)
                {
                    if (Init.TableCorrespondanceFiltre[i].Ancien_Code.Contains("1128"))
                        Console.WriteLine("Init.TableCorrespondanceFiltre[i].Ancien_Code : " + Init.TableCorrespondanceFiltre[i].Ancien_Code);
                    if (Init.TableCorrespondanceFiltre[i].Nouveau_Code == "" || Init.TableCorrespondanceFiltre[i].Nouveau_Code == null)
                    {
                        for (int j = 0; j < Init.TableReferentielFiltre.Count; j++)
                        {
                            if (Init.TableCorrespondanceFiltre[i].Ancien_Code.ToString().Contains("1128#") && Init.TableReferentielFiltre[j].Cpl == "50")
                            {
                                /*
                                Console.WriteLine("Init.TableCorrespondanceFiltre[i].Canonical : " + Init.TableCorrespondanceFiltre[i].Canonical);
                                Console.WriteLine("fonc.CanonicalString(Init.TableReferentielFiltre[j].Lib) : " + fonc.CanonicalString(Init.TableReferentielFiltre[j].Lib));
                                Console.WriteLine("Init.TableCorrespondanceFiltre[i].Cpl : " + Init.TableCorrespondanceFiltre[i].Cpl);
                                Console.WriteLine("Init.TableReferentielFiltre[j].Cpl : " + Init.TableReferentielFiltre[j].Cpl);
                                Console.WriteLine("Init.TableCorrespondanceFiltre[i].TypeRef : " + Init.TableCorrespondanceFiltre[i].TypeRef);
                                Console.WriteLine("Init.TableReferentielFiltre[j].Type : " + Init.TableReferentielFiltre[j].Type);
                                Console.WriteLine("Init.TableCorrespondanceFiltre[i].Ancien_Code.ToString().Contains('#') : " + Init.TableCorrespondanceFiltre[i].Ancien_Code.ToString().Contains("#"));
                                //Console.WriteLine("Init.TableCorrespondanceFiltre[i].Canonical : " + Init.TableCorrespondanceFiltre[i].Canonical);
                                */
                            }

                            if (Init.TableCorrespondanceFiltre[i].Canonical == Init.TableReferentielFiltre[j].Canonical
                                && Init.TableCorrespondanceFiltre[i].Cpl == Init.TableReferentielFiltre[j].Cpl
                                && Init.TableCorrespondanceFiltre[i].Cpl1 == Init.TableReferentielFiltre[j].Cpl1
                                && Init.TableCorrespondanceFiltre[i].Cpl2 == Init.TableReferentielFiltre[j].Cpl2
                                && Init.TableCorrespondanceFiltre[i].TypeRef == Init.TableReferentielFiltre[j].Type
                                && Init.TableCorrespondanceFiltre[i].Cpl == "141"
                                && Init.TableReferentielFiltre[j].Cpl == "141"
                                && Init.TableCorrespondanceFiltre[i].TypeRef == "NOMEN"
                                && Init.TableReferentielFiltre[j].Type == "NOMEN")
                            {
                                Init.TableCorrespondanceFiltre[i].Nouveau_Code = Init.TableReferentielFiltre[j].Code;
                                Init.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code = Init.TableReferentielFiltre[j].Lib;
                                if (Init.TableReferentielFiltre[j].InActif == true)
                                {
                                    Init.TableCorrespondanceFiltre[i].NouveauCodeInactif = true;
                                }
                                else
                                {
                                    Init.TableCorrespondanceFiltre[i].NouveauCodeInactif = false;
                                }
                                Init.TableCorrespondanceFiltre[i].FlagReferentiel = 1;
                                ObjCorr.UpdateSQLITE_TBCorrespondance(Init.TableCorrespondanceFiltre[i]);
                                ObjCorr.UpdateSQLITE_TBCorrespondance_FlagPreventiel(Init.TableCorrespondanceFiltre[i]);
                                /*
                                dataGridView_saisie.Rows[i].Cells[4].Value = Init.TableCorrespondanceFiltre[i].Nouveau_Code;
                                dataGridView_saisie.Rows[i].Cells[5].Value = Init.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code;
                                dataGridView_saisie.Rows[i].Cells[12].Value = Init.TableCorrespondanceFiltre[i].NouveauCodeInactif;
                                dataGridView_saisie.Rows[i].Cells[20].Value = Init.TableCorrespondanceFiltre[i].FlagReferentiel;
                                */
                                nbItemRapprochés++;
                            }
                            else if (Init.TableCorrespondanceFiltre[i].Canonical == Init.TableReferentielFiltre[j].Canonical
                                && Init.TableCorrespondanceFiltre[i].Cpl == Init.TableReferentielFiltre[j].Cpl
                                //&& Init.TableCorrespondanceFiltre[i].Cpl1 == Init.TableReferentielFiltre[j].Cpl1
                                //&& Init.TableCorrespondanceFiltre[i].Cpl2 == Init.TableReferentielFiltre[j].Cpl2
                                && Init.TableCorrespondanceFiltre[i].TypeRef == Init.TableReferentielFiltre[j].Type
                                && Init.TableCorrespondanceFiltre[i].Cpl == "60"
                                && Init.TableReferentielFiltre[j].Cpl == "60"
                                && Init.TableCorrespondanceFiltre[i].TypeRef == "CTRL"
                                && Init.TableReferentielFiltre[j].Type == "CTRL"
                                && Init.TableCorrespondanceFiltre[i].Ancien_Code.ToString().Contains("#"))
                            {
                                string[] vaccin_ancien_code = Init.TableCorrespondanceFiltre[i].Ancien_Code.ToString().Split('#');
                                Correspondances CorrObject = new Correspondances();
                                Correspondance ligne_vaccin_nouveau_code = CorrObject.RetourneCorrespondanceNouveauCode(Init.TableCorrespondanceFiltre, vaccin_ancien_code[0], "141|0|0|NOMEN");
                                Referentiels RefObject = new Referentiels();
                                List<Referentiel> TableProtocoleVaccinal = new List<Referentiel>();
                                TableProtocoleVaccinal = RefObject.FiltrerListeResultatExamenReferentielParExamen(Init.TableReferentiel, ligne_vaccin_nouveau_code.Nouveau_Code);
                                for (int k = 0; k < TableProtocoleVaccinal.Count; k++)
                                {
                                    if (TableProtocoleVaccinal[k].Canonical == Init.TableCorrespondanceFiltre[i].Canonical)
                                    {
                                        Init.TableCorrespondanceFiltre[i].Nouveau_Code = TableProtocoleVaccinal[k].Code;
                                        Init.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code = TableProtocoleVaccinal[k].Lib;
                                        if (TableProtocoleVaccinal[k].InActif == true)
                                        {
                                            Init.TableCorrespondanceFiltre[i].NouveauCodeInactif = true;
                                        }
                                        else
                                        {
                                            Init.TableCorrespondanceFiltre[i].NouveauCodeInactif = false;
                                        }
                                        Init.TableCorrespondanceFiltre[i].FlagReferentiel = 1;
                                        ObjCorr.UpdateSQLITE_TBCorrespondance(Init.TableCorrespondanceFiltre[i]);
                                        ObjCorr.UpdateSQLITE_TBCorrespondance_FlagPreventiel(Init.TableCorrespondanceFiltre[i]);
                                        /*
                                        dataGridView_saisie.Rows[i].Cells[4].Value = Init.TableCorrespondanceFiltre[i].Nouveau_Code;
                                        dataGridView_saisie.Rows[i].Cells[5].Value = Init.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code;
                                        dataGridView_saisie.Rows[i].Cells[12].Value = Init.TableCorrespondanceFiltre[i].NouveauCodeInactif;
                                        dataGridView_saisie.Rows[i].Cells[20].Value = Init.TableCorrespondanceFiltre[i].FlagReferentiel;
                                        */
                                        nbItemRapprochés++;
                                    }
                                }
                            }
                        }
                    }
                    UpdateProgress_rapprochement(i);
                    //progressBar_rapprochement.Value = i;
                    Console.WriteLine("Avancement : " + i + "/" + Init.TableCorrespondanceFiltre.Count);
                }
            }
            else
            {
                for (int i = 0; i < Init.TableCorrespondanceFiltre.Count; i++)
                {
                    if (Init.TableCorrespondanceFiltre[i].Nouveau_Code == "" || Init.TableCorrespondanceFiltre[i].Nouveau_Code == null)
                    {
                        for (int j = 0; j < Init.TableReferentielFiltre.Count; j++)
                        {
                            if (Init.TableCorrespondanceFiltre[i].Canonical == Init.TableReferentielFiltre[j].Canonical
                                && Init.TableCorrespondanceFiltre[i].Cpl == Init.TableReferentielFiltre[j].Cpl
                                && Init.TableCorrespondanceFiltre[i].Cpl1 == Init.TableReferentielFiltre[j].Cpl1
                                && Init.TableCorrespondanceFiltre[i].Cpl2 == Init.TableReferentielFiltre[j].Cpl2
                                && Init.TableCorrespondanceFiltre[i].TypeRef == Init.TableReferentielFiltre[j].Type)
                            {
                                Init.TableCorrespondanceFiltre[i].Nouveau_Code = Init.TableReferentielFiltre[j].Code;
                                Init.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code = Init.TableReferentielFiltre[j].Lib;
                                if (Init.TableReferentielFiltre[j].InActif == true)
                                {
                                    Init.TableCorrespondanceFiltre[i].NouveauCodeInactif = true;
                                }
                                else
                                {
                                    Init.TableCorrespondanceFiltre[i].NouveauCodeInactif = false;
                                }
                                Init.TableCorrespondanceFiltre[i].FlagReferentiel = 1;
                                ObjCorr.UpdateSQLITE_TBCorrespondance(Init.TableCorrespondanceFiltre[i]);
                                ObjCorr.UpdateSQLITE_TBCorrespondance_FlagPreventiel(Init.TableCorrespondanceFiltre[i]);
                                /*
                                dataGridView_saisie.Rows[i].Cells[4].Value = Init.TableCorrespondanceFiltre[i].Nouveau_Code;
                                dataGridView_saisie.Rows[i].Cells[5].Value = Init.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code;
                                dataGridView_saisie.Rows[i].Cells[12].Value = Init.TableCorrespondanceFiltre[i].NouveauCodeInactif;
                                dataGridView_saisie.Rows[i].Cells[20].Value = Init.TableCorrespondanceFiltre[i].FlagReferentiel;
                                */
                                nbItemRapprochés++;
                            }
                        }
                    }
                    UpdateProgress_rapprochement(i);
                    //progressBar_rapprochement.Value = i;
                    Console.WriteLine("Avancement : " + i + "/" + Init.TableCorrespondanceFiltre.Count);
                }
            }
            UpdateProgress_rapprochement_module_terminate(nbItemRapprochés);
        }

        private void button_rapprochementmodule_Click(object sender, EventArgs e)
        {
            progressBar_rapprochement.Maximum = Init.TableCorrespondanceFiltre.Count - 1;
            Thread rapprochement = new Thread(new ParameterizedThreadStart(RapprochementModule));
            rapprochement.Start((string)comboBox_filtre.SelectedValue);
            /*
            Thread backgroundThread = new Thread(
                new ThreadStart(() =>
                {
                    progressBar1.BeginInvoke(new Action(() => progressBar1.Maximum = Init.TableCorrespondanceFiltre.Count));
                    Fonc fonc = new Fonc();
                    Correspondances ObjCorr = new Correspondances();
                    for (int i = 0; i < Init.TableCorrespondanceFiltre.Count; i++)
                    {
                        if (Init.TableCorrespondanceFiltre[i].Nouveau_Code == "" || Init.TableCorrespondanceFiltre[i].Nouveau_Code == null)
                        {
                            for (int j = 0; j < Init.TableReferentielFiltre.Count; j++)
                            {
                                if (Init.TableCorrespondanceFiltre[i].Canonical == fonc.CanonicalString(Init.TableReferentielFiltre[j].Lib)
                                    && Init.TableCorrespondanceFiltre[i].Cpl == Init.TableReferentielFiltre[j].Cpl
                                    && Init.TableCorrespondanceFiltre[i].Cpl1 == Init.TableReferentielFiltre[j].Cpl1
                                    && Init.TableCorrespondanceFiltre[i].Cpl2 == Init.TableReferentielFiltre[j].Cpl2
                                    && Init.TableCorrespondanceFiltre[i].TypeRef == Init.TableReferentielFiltre[j].Type)
                                {
                                    Init.TableCorrespondanceFiltre[i].Nouveau_Code = Init.TableReferentielFiltre[j].Code;
                                    Init.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code = Init.TableReferentielFiltre[j].Lib;
                                    if(Init.TableReferentielFiltre[j].InActif==true)
                                    {
                                        Init.TableCorrespondanceFiltre[i].NouveauCodeInactif = true;
                                    }
                                    else
                                    {
                                        Init.TableCorrespondanceFiltre[i].NouveauCodeInactif = false;
                                    }
                                    Init.TableCorrespondanceFiltre[i].FlagReferentiel = 1;
                                    Corr.UpdateSQLITE_TBCorrespondance(Init.TableCorrespondanceFiltre[i]);
                                    Corr.UpdateSQLITE_TBCorrespondance_FlagPreventiel(Init.TableCorrespondanceFiltre[i]);
                                    dataGridView_saisie.BeginInvoke(new Action(() => dataGridView_saisie.Rows[i].Cells[4].Value = Init.TableCorrespondanceFiltre[i].Nouveau_Code));
                                    dataGridView_saisie.BeginInvoke(new Action(() => dataGridView_saisie.Rows[i].Cells[5].Value = Init.TableCorrespondanceFiltre[i].Libelle_Nouveau_Code));
                                    dataGridView_saisie.BeginInvoke(new Action(() => dataGridView_saisie.Rows[i].Cells[12].Value = Init.TableCorrespondanceFiltre[i].NouveauCodeInactif));
                                    dataGridView_saisie.BeginInvoke(new Action(() => dataGridView_saisie.Rows[i].Cells[20].Value = Init.TableCorrespondanceFiltre[i].FlagReferentiel));
                                }
                            }
                        }
                        progressBar1.BeginInvoke(new Action(() => progressBar1.Value = i));
                    }

                    var source = new BindingSource();
                    source.DataSource = Init.TableCorrespondanceFiltre;

                    dataGridView_saisie.BeginInvoke(new Action(() => dataGridView_saisie.DataSource = source));

                    //dataGridView_saisie.DataSource = source;

                    InitStructSaisie();
                    InitColorSaisie();
                    MessageBox.Show("Thread completed!");
                    if (progressBar1.InvokeRequired)
                        progressBar1.BeginInvoke(new Action(() => progressBar1.Value = 0));
                }
            ));

            // Start the background process thread
            backgroundThread.Start();
            //backgroundThread.Join();
            */



        }

        private void button_parambase_Click(object sender, EventArgs e)
        {
            new MainForm().Show();
        }

        private void button_chargement_Click(object sender, EventArgs e)
        {
            ChargementTableEnMemoire();
            MessageBox.Show("Chargement terminé.");
            progressBar_admin.Value = 0;
        }

        public void ChargementTableEnMemoire()
        {
            if (File.Exists(System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\PREVTGXV7.db"))
            {
                InitTable();
                InitComboBoxFiltre();
                progressBar_admin.Value = 0;


            }
        }

        private void button_testsqlserver_Click(object sender, EventArgs e)
        {
            ConnexionSQLServer con = new ConnexionSQLServer();
            string test = "update TB$S_CorrespondanceItem set Occurrence = 3 where NomRef = 'AMT' and Ancien_Code = '17|TT09'";
            con.ExecuteQuery(test);
        }

        
        private void button_exportCorresp_Click(object sender, EventArgs e)
        {
            progressBar_admin.Maximum = Init.TableCorrespondance.Count;
            
            if(InfoSqlServer.SQLServerConnectionString != null)
            {
                Thread export = new Thread(CorrObject.ExporteCorrespondance);
                export.Start();
                //MessageBox.Show("Export terminé");
                //progressBar_admin.Value = 0;
            }
            else
            {
                MessageBox.Show("Veuillez définir la base SQL server export via Param Base");
            }
            
        }

        public void RapprochementGlobal(object clefValeur)
        {

            int nbItemRapprochés = 0;

            //Fonc fonc = new Fonc();
            Correspondances ObjCorr = new Correspondances();
            /*
            string[] examen_ancien_code = dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Split('#');
            Correspondances CorrObject = new Correspondances();
            Correspondance ligne_exam_nouveau_code = CorrObject.RetourneCorrespondanceNouveauCode(Init.TableCorrespondance, examen_ancien_code[0], "135|0|0|NOMEN");
            Referentiels RefObject = new Referentiels();
            List<Referentiel> TableRefResultatExamen = new List<Referentiel>();
            TableRefResultatExamen = RefObject.FiltrerListeResultatExamenReferentielParExamen(TableRef, ligne_exam_nouveau_code.Nouveau_Code);
            */
            //Init.TableCorrespondance = TableCorresp;
            
            if ((string)clefValeur == "135|0|0|NOMEN")
            {
                //string[] examen_ancien_code = dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Split('#');

                for (int i = 0; i < Init.TableCorrespondance.Count; i++)
                {
                    if (Init.TableCorrespondance[i].Ancien_Code.Contains("1128"))
                        Console.WriteLine("Init.TableCorrespondance[i].Ancien_Code : " + Init.TableCorrespondance[i].Ancien_Code);
                    if (Init.TableCorrespondance[i].Nouveau_Code == "" || Init.TableCorrespondance[i].Nouveau_Code == null)
                    {
                        for (int j = 0; j < Init.TableReferentiel.Count; j++)
                        {
                            if (Init.TableCorrespondance[i].Ancien_Code.ToString().Contains("1128#") && Init.TableReferentiel[j].Cpl == "50")
                            {
                                /*
                                Console.WriteLine("Init.TableCorrespondance[i].Canonical : " + Init.TableCorrespondance[i].Canonical);
                                Console.WriteLine("fonc.CanonicalString(Init.TableReferentiel[j].Lib) : " + fonc.CanonicalString(Init.TableReferentiel[j].Lib));
                                Console.WriteLine("Init.TableCorrespondance[i].Cpl : " + Init.TableCorrespondance[i].Cpl);
                                Console.WriteLine("Init.TableReferentiel[j].Cpl : " + Init.TableReferentiel[j].Cpl);
                                Console.WriteLine("Init.TableCorrespondance[i].TypeRef : " + Init.TableCorrespondance[i].TypeRef);
                                Console.WriteLine("Init.TableReferentiel[j].Type : " + Init.TableReferentiel[j].Type);
                                Console.WriteLine("Init.TableCorrespondance[i].Ancien_Code.ToString().Contains('#') : " + Init.TableCorrespondance[i].Ancien_Code.ToString().Contains("#"));
                                //Console.WriteLine("Init.TableCorrespondance[i].Canonical : " + Init.TableCorrespondance[i].Canonical);
                                */
                            }

                            if (Init.TableCorrespondance[i].Canonical != "" && Init.TableReferentiel[i].Canonical != ""
                                && Init.TableCorrespondance[i].Canonical == Init.TableReferentiel[j].Canonical
                                && Init.TableCorrespondance[i].Cpl == Init.TableReferentiel[j].Cpl
                                && Init.TableCorrespondance[i].Cpl1 == Init.TableReferentiel[j].Cpl1
                                && Init.TableCorrespondance[i].Cpl2 == Init.TableReferentiel[j].Cpl2
                                && Init.TableCorrespondance[i].TypeRef == Init.TableReferentiel[j].Type
                                && Init.TableCorrespondance[i].Cpl == "135"
                                && Init.TableReferentiel[j].Cpl == "135"
                                && Init.TableCorrespondance[i].TypeRef == "NOMEN"
                                && Init.TableReferentiel[j].Type == "NOMEN")
                            {
                                Init.TableCorrespondance[i].Nouveau_Code = Init.TableReferentiel[j].Code;
                                Init.TableCorrespondance[i].Libelle_Nouveau_Code = Init.TableReferentiel[j].Lib;
                                if (Init.TableReferentiel[j].InActif == true)
                                {
                                    Init.TableCorrespondance[i].NouveauCodeInactif = true;
                                }
                                else
                                {
                                    Init.TableCorrespondance[i].NouveauCodeInactif = false;
                                }
                                Init.TableCorrespondance[i].FlagReferentiel = 1;
                                ObjCorr.UpdateSQLITE_TBCorrespondance(Init.TableCorrespondance[i]);
                                ObjCorr.UpdateSQLITE_TBCorrespondance_FlagPreventiel(Init.TableCorrespondance[i]);
                                /*
                                dataGridView_saisie.Rows[i].Cells[4].Value = Init.TableCorrespondance[i].Nouveau_Code;
                                dataGridView_saisie.Rows[i].Cells[5].Value = Init.TableCorrespondance[i].Libelle_Nouveau_Code;
                                dataGridView_saisie.Rows[i].Cells[12].Value = Init.TableCorrespondance[i].NouveauCodeInactif;
                                dataGridView_saisie.Rows[i].Cells[20].Value = Init.TableCorrespondance[i].FlagReferentiel;
                                */
                                nbItemRapprochés++;
                            }
                            else if (Init.TableCorrespondance[i].Canonical != "" && Init.TableReferentiel[i].Canonical != ""
                                && Init.TableCorrespondance[i].Canonical == Init.TableReferentiel[j].Canonical
                                && Init.TableCorrespondance[i].Cpl == Init.TableReferentiel[j].Cpl
                                //&& Init.TableCorrespondance[i].Cpl1 == Init.TableReferentiel[j].Cpl1
                                //&& Init.TableCorrespondance[i].Cpl2 == Init.TableReferentiel[j].Cpl2
                                && Init.TableCorrespondance[i].TypeRef == Init.TableReferentiel[j].Type
                                && Init.TableCorrespondance[i].Cpl == "50"
                                && Init.TableReferentiel[j].Cpl == "50"
                                && Init.TableCorrespondance[i].TypeRef == "CTRL"
                                && Init.TableReferentiel[j].Type == "CTRL"
                                && Init.TableCorrespondance[i].Ancien_Code.ToString().Contains("#"))
                            {
                                string[] examen_ancien_code = Init.TableCorrespondance[i].Ancien_Code.ToString().Split('#');
                                Correspondances CorrObject = new Correspondances();
                                Correspondance ligne_exam_nouveau_code = CorrObject.RetourneCorrespondanceNouveauCode(Init.TableCorrespondance, examen_ancien_code[0], "135|0|0|NOMEN");
                                Referentiels RefObject = new Referentiels();
                                List<Referentiel> TableRefResultatExamen = new List<Referentiel>();
                                TableRefResultatExamen = RefObject.FiltrerListeResultatExamenReferentielParExamen(Init.TableReferentiel, ligne_exam_nouveau_code.Nouveau_Code);
                                for (int k = 0; k < TableRefResultatExamen.Count; k++)
                                {
                                    if (Init.TableCorrespondance[i].Canonical != "" && TableRefResultatExamen[k].Canonical != ""
                                && TableRefResultatExamen[k].Canonical == Init.TableCorrespondance[i].Canonical)
                                    {
                                        Init.TableCorrespondance[i].Nouveau_Code = TableRefResultatExamen[k].Code;
                                        Init.TableCorrespondance[i].Libelle_Nouveau_Code = TableRefResultatExamen[k].Lib;
                                        if (TableRefResultatExamen[k].InActif == true)
                                        {
                                            Init.TableCorrespondance[i].NouveauCodeInactif = true;
                                        }
                                        else
                                        {
                                            Init.TableCorrespondance[i].NouveauCodeInactif = false;
                                        }
                                        Init.TableCorrespondance[i].FlagReferentiel = 1;
                                        ObjCorr.UpdateSQLITE_TBCorrespondance(Init.TableCorrespondance[i]);
                                        ObjCorr.UpdateSQLITE_TBCorrespondance_FlagPreventiel(Init.TableCorrespondance[i]);
                                        /*
                                        dataGridView_saisie.Rows[i].Cells[4].Value = Init.TableCorrespondance[i].Nouveau_Code;
                                        dataGridView_saisie.Rows[i].Cells[5].Value = Init.TableCorrespondance[i].Libelle_Nouveau_Code;
                                        dataGridView_saisie.Rows[i].Cells[12].Value = Init.TableCorrespondance[i].NouveauCodeInactif;
                                        dataGridView_saisie.Rows[i].Cells[20].Value = Init.TableCorrespondance[i].FlagReferentiel;
                                        */
                                        nbItemRapprochés++;
                                    }
                                }
                            }
                        }
                    }
                    //progressBar_rapprochement.Value = i;
                    UpdateProgress_rapprochement(i);
                    Console.WriteLine("Avancement : " + i + "/" + Init.TableCorrespondance.Count);
                }
            }
            else if ((string)clefValeur == "141|0|0|NOMEN")
            {
                //string[] examen_ancien_code = dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Split('#');

                for (int i = 0; i < Init.TableCorrespondance.Count; i++)
                {
                    if (Init.TableCorrespondance[i].Ancien_Code.Contains("1128"))
                        Console.WriteLine("Init.TableCorrespondance[i].Ancien_Code : " + Init.TableCorrespondance[i].Ancien_Code);
                    if (Init.TableCorrespondance[i].Nouveau_Code == "" || Init.TableCorrespondance[i].Nouveau_Code == null)
                    {
                        for (int j = 0; j < Init.TableReferentiel.Count; j++)
                        {
                            if (Init.TableCorrespondance[i].Ancien_Code.ToString().Contains("1128#") && Init.TableReferentiel[j].Cpl == "50")
                            {
                                /*
                                Console.WriteLine("Init.TableCorrespondance[i].Canonical : " + Init.TableCorrespondance[i].Canonical);
                                Console.WriteLine("fonc.CanonicalString(Init.TableReferentiel[j].Lib) : " + fonc.CanonicalString(Init.TableReferentiel[j].Lib));
                                Console.WriteLine("Init.TableCorrespondance[i].Cpl : " + Init.TableCorrespondance[i].Cpl);
                                Console.WriteLine("Init.TableReferentiel[j].Cpl : " + Init.TableReferentiel[j].Cpl);
                                Console.WriteLine("Init.TableCorrespondance[i].TypeRef : " + Init.TableCorrespondance[i].TypeRef);
                                Console.WriteLine("Init.TableReferentiel[j].Type : " + Init.TableReferentiel[j].Type);
                                Console.WriteLine("Init.TableCorrespondance[i].Ancien_Code.ToString().Contains('#') : " + Init.TableCorrespondance[i].Ancien_Code.ToString().Contains("#"));
                                //Console.WriteLine("Init.TableCorrespondance[i].Canonical : " + Init.TableCorrespondance[i].Canonical);
                                */
                            }

                            if (Init.TableCorrespondance[i].Canonical != "" && Init.TableReferentiel[i].Canonical != ""
                                && Init.TableCorrespondance[i].Canonical == Init.TableReferentiel[j].Canonical
                                && Init.TableCorrespondance[i].Cpl == Init.TableReferentiel[j].Cpl
                                && Init.TableCorrespondance[i].Cpl1 == Init.TableReferentiel[j].Cpl1
                                && Init.TableCorrespondance[i].Cpl2 == Init.TableReferentiel[j].Cpl2
                                && Init.TableCorrespondance[i].TypeRef == Init.TableReferentiel[j].Type
                                && Init.TableCorrespondance[i].Cpl == "141"
                                && Init.TableReferentiel[j].Cpl == "141"
                                && Init.TableCorrespondance[i].TypeRef == "NOMEN"
                                && Init.TableReferentiel[j].Type == "NOMEN")
                            {
                                Init.TableCorrespondance[i].Nouveau_Code = Init.TableReferentiel[j].Code;
                                Init.TableCorrespondance[i].Libelle_Nouveau_Code = Init.TableReferentiel[j].Lib;
                                if (Init.TableReferentiel[j].InActif == true)
                                {
                                    Init.TableCorrespondance[i].NouveauCodeInactif = true;
                                }
                                else
                                {
                                    Init.TableCorrespondance[i].NouveauCodeInactif = false;
                                }
                                Init.TableCorrespondance[i].FlagReferentiel = 1;
                                ObjCorr.UpdateSQLITE_TBCorrespondance(Init.TableCorrespondance[i]);
                                ObjCorr.UpdateSQLITE_TBCorrespondance_FlagPreventiel(Init.TableCorrespondance[i]);
                                /*
                                dataGridView_saisie.Rows[i].Cells[4].Value = Init.TableCorrespondance[i].Nouveau_Code;
                                dataGridView_saisie.Rows[i].Cells[5].Value = Init.TableCorrespondance[i].Libelle_Nouveau_Code;
                                dataGridView_saisie.Rows[i].Cells[12].Value = Init.TableCorrespondance[i].NouveauCodeInactif;
                                dataGridView_saisie.Rows[i].Cells[20].Value = Init.TableCorrespondance[i].FlagReferentiel;
                                */
                                nbItemRapprochés++;
                            }
                            else if (Init.TableCorrespondance[i].Canonical != "" && Init.TableReferentiel[i].Canonical != ""
                                && Init.TableCorrespondance[i].Canonical == Init.TableReferentiel[j].Canonical
                                && Init.TableCorrespondance[i].Cpl == Init.TableReferentiel[j].Cpl
                                //&& Init.TableCorrespondance[i].Cpl1 == Init.TableReferentiel[j].Cpl1
                                //&& Init.TableCorrespondance[i].Cpl2 == Init.TableReferentiel[j].Cpl2
                                && Init.TableCorrespondance[i].TypeRef == Init.TableReferentiel[j].Type
                                && Init.TableCorrespondance[i].Cpl == "60"
                                && Init.TableReferentiel[j].Cpl == "60"
                                && Init.TableCorrespondance[i].TypeRef == "CTRL"
                                && Init.TableReferentiel[j].Type == "CTRL"
                                && Init.TableCorrespondance[i].Ancien_Code.ToString().Contains("#"))
                            {
                                string[] vaccin_ancien_code = Init.TableCorrespondance[i].Ancien_Code.ToString().Split('#');
                                Correspondances CorrObject = new Correspondances();
                                Correspondance ligne_vaccin_nouveau_code = CorrObject.RetourneCorrespondanceNouveauCode(Init.TableCorrespondance, vaccin_ancien_code[0], "141|0|0|NOMEN");
                                Referentiels RefObject = new Referentiels();
                                List<Referentiel> TableProtocoleVaccinal = new List<Referentiel>();
                                TableProtocoleVaccinal = RefObject.FiltrerListeResultatExamenReferentielParExamen(Init.TableReferentiel, ligne_vaccin_nouveau_code.Nouveau_Code);
                                for (int k = 0; k < TableProtocoleVaccinal.Count; k++)
                                {
                                    if (Init.TableCorrespondance[i].Canonical != "" && TableProtocoleVaccinal[k].Canonical != ""
                                && TableProtocoleVaccinal[k].Canonical == Init.TableCorrespondance[i].Canonical)
                                    {
                                        Init.TableCorrespondance[i].Nouveau_Code = TableProtocoleVaccinal[k].Code;
                                        Init.TableCorrespondance[i].Libelle_Nouveau_Code = TableProtocoleVaccinal[k].Lib;
                                        if (TableProtocoleVaccinal[k].InActif == true)
                                        {
                                            Init.TableCorrespondance[i].NouveauCodeInactif = true;
                                        }
                                        else
                                        {
                                            Init.TableCorrespondance[i].NouveauCodeInactif = false;
                                        }
                                        Init.TableCorrespondance[i].FlagReferentiel = 1;
                                        ObjCorr.UpdateSQLITE_TBCorrespondance(Init.TableCorrespondance[i]);
                                        ObjCorr.UpdateSQLITE_TBCorrespondance_FlagPreventiel(Init.TableCorrespondance[i]);
                                        /*
                                        dataGridView_saisie.Rows[i].Cells[4].Value = Init.TableCorrespondance[i].Nouveau_Code;
                                        dataGridView_saisie.Rows[i].Cells[5].Value = Init.TableCorrespondance[i].Libelle_Nouveau_Code;
                                        dataGridView_saisie.Rows[i].Cells[12].Value = Init.TableCorrespondance[i].NouveauCodeInactif;
                                        dataGridView_saisie.Rows[i].Cells[20].Value = Init.TableCorrespondance[i].FlagReferentiel;
                                        */
                                        nbItemRapprochés++;
                                    }
                                }
                            }
                        }
                    }
                    //progressBar_rapprochement.Value = i;
                    UpdateProgress_rapprochement(i);
                    Console.WriteLine("Avancement : " + i + "/" + Init.TableCorrespondance.Count);
                }
            }
            else
            {
                for (int i = 0; i < Init.TableCorrespondance.Count; i++)
                {
                    if (Init.TableCorrespondance[i].Nouveau_Code == "" || Init.TableCorrespondance[i].Nouveau_Code == null)
                    {
                        for (int j = 0; j < Init.TableReferentiel.Count; j++)
                        {
                            if (Init.TableCorrespondance[i].Canonical != "" && Init.TableReferentiel[i].Canonical != ""
                                && Init.TableCorrespondance[i].Canonical == Init.TableReferentiel[j].Canonical
                                && Init.TableCorrespondance[i].Cpl == Init.TableReferentiel[j].Cpl
                                && Init.TableCorrespondance[i].Cpl1 == Init.TableReferentiel[j].Cpl1
                                && Init.TableCorrespondance[i].Cpl2 == Init.TableReferentiel[j].Cpl2
                                && Init.TableCorrespondance[i].TypeRef == Init.TableReferentiel[j].Type)
                            {
                                Init.TableCorrespondance[i].Nouveau_Code = Init.TableReferentiel[j].Code;
                                Init.TableCorrespondance[i].Libelle_Nouveau_Code = Init.TableReferentiel[j].Lib;
                                if (Init.TableReferentiel[j].InActif == true)
                                {
                                    Init.TableCorrespondance[i].NouveauCodeInactif = true;
                                }
                                else
                                {
                                    Init.TableCorrespondance[i].NouveauCodeInactif = false;
                                }
                                Init.TableCorrespondance[i].FlagReferentiel = 1;
                                ObjCorr.UpdateSQLITE_TBCorrespondance(Init.TableCorrespondance[i]);
                                ObjCorr.UpdateSQLITE_TBCorrespondance_FlagPreventiel(Init.TableCorrespondance[i]);
                                /*
                                dataGridView_saisie.Rows[i].Cells[4].Value = Init.TableCorrespondance[i].Nouveau_Code;
                                dataGridView_saisie.Rows[i].Cells[5].Value = Init.TableCorrespondance[i].Libelle_Nouveau_Code;
                                dataGridView_saisie.Rows[i].Cells[12].Value = Init.TableCorrespondance[i].NouveauCodeInactif;
                                dataGridView_saisie.Rows[i].Cells[20].Value = Init.TableCorrespondance[i].FlagReferentiel;
                                */
                                nbItemRapprochés++;
                            }
                        }
                    }
                    //progressBar_rapprochement.Value = i;
                    UpdateProgress_rapprochement(i);
                    Console.WriteLine("Avancement : " + i + "/" + Init.TableCorrespondance.Count);
                }
            }

            UpdateProgress_rapprochement_terminate(nbItemRapprochés);
            /*
            var source = new BindingSource();
            source.DataSource = Init.TableCorrespondance;

            //dataGridView_saisie.BeginInvoke(new Action(() => dataGridView_saisie.DataSource = source));

            dataGridView_saisie.DataSource = source;

            InitStructSaisie();
            InitColorSaisie();
            MessageBox.Show("Rapprochement automatique terminé. \r\n" + nbItemRapprochés.ToString() + " code(s) rapproché(s) automatiquement.");
            UpdateProgress_rapprochement_terminate(nbItemRapprochés);
            //progressBar_rapprochement.Value = 0;
            button_deleteRecodage.Enabled = true;
            button_afficherCreationCode.Enabled = true;
            button_affecter.Enabled = true;*/

        }


        private void button_rapprochement_global_Click(object sender, EventArgs e)
        {
            progressBar_rapprochement.Maximum = Init.TableCorrespondance.Count - 1;
            Thread rapprochement = new Thread(new ParameterizedThreadStart(RapprochementGlobal));
            rapprochement.Start((string)comboBox_filtre.SelectedValue);
        }

        private void button_acces_admin_Click(object sender, EventArgs e)
        {
            if(button_acces_admin.Text == "Accès interface Admin")
            {
                textBox_pass_admin.Visible = true;
                button_okmdpadmin.Visible = true;
                //groupBox_admin.Visible = true;
                button_acces_admin.Text = "Fermer interface Admin";
            }
            else if (button_acces_admin.Text == "Fermer interface Admin")
            {
                textBox_pass_admin.Text = "";
                textBox_pass_admin.Visible = false;
                button_okmdpadmin.Visible = false;
                groupBox_admin.Visible = false;
                button_acces_admin.Text = "Accès interface Admin";
            }
        }

        private void button_okmdpadmin_Click(object sender, EventArgs e)
        {
            Enter_OK_Admin();
        }

        public void Enter_OK_Admin()
        {
            string PassAdmin = "TeamReprise";
            if (textBox_pass_admin.Text == PassAdmin)
            {
                groupBox_admin.Visible = true;
            }
            else
            {
                MessageBox.Show("Mot de passe incorrect.");
            }
        }

        private void textBox_pass_admin_Enter(object sender, EventArgs e)
        {
            //Enter_OK_Admin();
        }

        private void textBox_pass_admin_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox_pass_admin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                Enter_OK_Admin();
            }
        }
    }
}
