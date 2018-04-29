using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using RecodageList.DAL;
using RecodageList.Fonction;

namespace RecodageList
{
    public partial class Form1 : Form
    {
        
        //delegate void ControlArgReturningVoidDelegate(Control button);
        static Referentiels referentiel = new Referentiels();
        static List<Referentiel> TableRef = referentiel.ObtenirListeReferentiel_SQLITE();

        static Correspondances Corr = new Correspondances();
        static List<Correspondance> TableCorresp = Corr.ObtenirListeCorrespondance_SQLITE(TableRef);
        //static List<Correspondance> TableCorresp = new List<Correspondance>();
        static List<Correspondance> TableCorrespFiltered = Corr.ObtenirListeCorrespondance_SQLITE(TableRef);

        //static CorrespondancesAffichages CorrAffichage = new CorrespondancesAffichages();
        //static List<CorrespondanceAffichage> TableCorrespAffichage = CorrAffichage.ObtenirListeCorrespondanceAffichage(TableCorresp);

        
        //static ReferentielsAffichages RefAffichage = new ReferentielsAffichages();
        //static List<ReferentielAffichage> TableRefAffichage = RefAffichage.ObtenirListeReferentielAffichage(TableRef);

        static List<Referentiel> RefFiltered = new List<Referentiel>();

        static ComboBoxFiltres combobox_object = new ComboBoxFiltres();
        static List<ComboBoxFiltre> comboboxfiltre = combobox_object.ObtenirComboBoxFiltre();

        public Form1()
        {
            InitializeComponent();
            //InitDatagridSaisie();
            //InitDatagridReferentiel();
            InitComboBoxFiltre();
            //TestCanonique();
            
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
            source.DataSource = TableCorresp;
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
            //dataGridView_saisie.Columns["Canonical"].Visible = false;
            //dataGridView_saisie.Columns["FlagReferentiel"].Visible = false;

            dataGridView_saisie.Columns["Ancien_Code"].HeaderText = "Code Source";
            dataGridView_saisie.Columns["Libelle_Ancien_Code"].HeaderText = "Intitulé Source";
            dataGridView_saisie.Columns["Nouveau_Code"].HeaderText = "Code Référentiel";
            dataGridView_saisie.Columns["Libelle_Nouveau_Code"].HeaderText = "Intitulé Référentiel";
            dataGridView_saisie.Columns["NouveauCodeInactif"].HeaderText = "Inactif";
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
            source.DataSource = TableRef;
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

            dataGridView_ref.Columns["Code"].HeaderText = "Code Référentiel";
            dataGridView_ref.Columns["Lib"].HeaderText = "Intitulé Référentiel";
            dataGridView_ref.Columns["InActif"].HeaderText = "Inactif";
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
            comboBox_filtre.DataSource = new BindingSource(comboboxfiltre, null);
            comboBox_filtre.DisplayMember = "NomRef";
            comboBox_filtre.ValueMember = "Cpl";
            //MessageBox.Show();
        }
        
        public void TestCanonique()
        {
            Fonc fonc = new Fonc();
            Console.WriteLine(fonc.CanonicalString("j'ai testé comme un fouuuu"));
            Console.WriteLine(TableCorresp[0].Canonical);
            Console.WriteLine(TableCorresp[1].Canonical);
            Console.WriteLine(TableCorresp[2].Canonical);
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
            TableCorresp = Corresp.RetourneListeCorrespondanceUpdater(TableCorresp, ligne, ligne.NomRef);

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

            Correspondances Corr = new Correspondances();
            //Console.WriteLine(dataGridView_saisie.DataMember);
            //List<Correspondance> TableCorresp = Corr.ObtenirListeCorrespondance_SQLITE();
            //List<Correspondance> CorrFiltered = new List<Correspondance>();
            Console.WriteLine(comboBox_filtre.ValueMember);
            Console.WriteLine("(string)comboBox_filtre.SelectedValue : " + (string)comboBox_filtre.SelectedValue);

            //Filtre la table de corresp globale par CPL
            TableCorrespFiltered = Corr.FiltrerListeCorrespondance_parCPL(TableCorresp, (string)comboBox_filtre.SelectedValue);

            //Créé une nouvelle liste de type affichage qui va etre affectée de la liste ci-dessus (liste globale filtrée).
            //CorrespondancesAffichages CorrAffichage = new CorrespondancesAffichages();
            //List<CorrespondanceAffichage> TableCorrespAffichage = CorrAffichage.ObtenirListeCorrespondanceAffichage(TableCorrespFiltered);

            //Réaffecte le datagrid à la liste filtrée 
            var source = new BindingSource();
            source.DataSource = TableCorrespFiltered;
            dataGridView_saisie.DataSource = source;
            InitStructSaisie();
            InitColorSaisie();

            Referentiels Ref = new Referentiels();
            //List<Referentiel> TableRef = Ref.ObtenirListeReferentiel_SQLITE();
            Console.WriteLine("(string)comboBox_filtre.SelectedValue : " + (string)comboBox_filtre.SelectedValue);
            RefFiltered = Ref.FiltrerListeReferentiel_parCPL(TableRef, (string)comboBox_filtre.SelectedValue);
            //ReferentielsAffichages RefAffichage = new ReferentielsAffichages();
            //List<ReferentielAffichage> TableRefAffichage = RefAffichage.ObtenirListeReferentielAffichage(RefFiltered);
            var source_ref = new BindingSource();
            source_ref.DataSource = RefFiltered;
            dataGridView_ref.DataSource = source_ref;
            InitStructReferentiel();

        }

  

        private void button_deletefilter_Click(object sender, EventArgs e)
        {
            InitDatagridSaisie();
            InitDatagridReferentiel();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
                            TableRefLevenshtein = RefObject.TrierListeParLevenshtein(RefFiltered, dataGridView_saisie.CurrentRow.Cells[2].Value.ToString());
                        }
                        else if(dataGridView_saisie.CurrentRow.Cells[2].Value == null)
                        {
                            TableRefLevenshtein = RefObject.TrierListeParLevenshtein(RefFiltered, "");
                        }
                        var source_ref = new BindingSource();
                        source_ref.DataSource = TableRefLevenshtein;
                        dataGridView_ref.DataSource = source_ref;
                    }
                    else if (dataGridView_saisie.CurrentRow.Cells[16].Value.ToString() == "50" && dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Contains("#"))
                    {
                        string[] examen_ancien_code = dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Split('#');
                        Correspondances CorrObject = new Correspondances();
                        Correspondance ligne_exam_nouveau_code = CorrObject.RetourneCorrespondanceNouveauCode(TableCorrespFiltered, examen_ancien_code[0], "135|0|0|NOMEN");
                        Referentiels RefObject = new Referentiels();
                        List<Referentiel> TableRefResultatExamen = new List<Referentiel>();
                        TableRefResultatExamen = RefObject.FiltrerListeResultatExamenReferentielParExamen(TableRef, ligne_exam_nouveau_code.Nouveau_Code);
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
                        RefFiltered = Ref.FiltrerListeReferentiel_parCPL(TableRef, (string)comboBox_filtre.SelectedValue);

                        //Supprime les resultat examen du referentiel
                        RefFiltered = Ref.SupprimeResultatExamenReferentiel(RefFiltered);

                        //ReferentielsAffichages RefAffichage = new ReferentielsAffichages();
                        //List<ReferentielAffichage> TableRefAffichage = RefAffichage.ObtenirListeReferentielAffichage(RefFiltered);
                        List<Referentiel> TableRefLevenshtein = new List<Referentiel>();
                        if (dataGridView_saisie.CurrentRow.Cells[2].Value != null)
                        {
                            TableRefLevenshtein = Ref.TrierListeParLevenshtein(RefFiltered, dataGridView_saisie.CurrentRow.Cells[2].Value.ToString());
                        }
                        else if (dataGridView_saisie.CurrentRow.Cells[2].Value == null)
                        {
                            TableRefLevenshtein = Ref.TrierListeParLevenshtein(RefFiltered, "");
                        }
                        var source_ref = new BindingSource();
                        source_ref.DataSource = RefFiltered;
                        dataGridView_ref.DataSource = source_ref;
                        InitStructReferentiel();
                    }
                    else if (dataGridView_saisie.CurrentRow.Cells[16].Value.ToString() == "60" && dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Contains("#"))
                    {
                        string[] vaccin_ancien_code = dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Split('#');
                        Correspondances CorrObject = new Correspondances();
                        Correspondance ligne_vacin_nouveau_code = CorrObject.RetourneCorrespondanceNouveauCode(TableCorrespFiltered, vaccin_ancien_code[0], "141|0|0|NOMEN");
                        Referentiels RefObject = new Referentiels();
                        List<Referentiel> TableProtocoleVaccin = new List<Referentiel>();
                        TableProtocoleVaccin = RefObject.FiltrerListeResultatExamenReferentielParExamen(TableRef, ligne_vacin_nouveau_code.Nouveau_Code);
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
                        RefFiltered = Ref.FiltrerListeReferentiel_parCPL(TableRef, (string)comboBox_filtre.SelectedValue);
                        //ReferentielsAffichages RefAffichage = new ReferentielsAffichages();
                        //List<ReferentielAffichage> TableRefAffichage = RefAffichage.ObtenirListeReferentielAffichage(RefFiltered);
                        List<Referentiel> TableRefLevenshtein = new List<Referentiel>();
                        if (dataGridView_saisie.CurrentRow.Cells[2].Value != null)
                        {
                            TableRefLevenshtein = Ref.TrierListeParLevenshtein(RefFiltered, dataGridView_saisie.CurrentRow.Cells[2].Value.ToString());
                        }
                        else if (dataGridView_saisie.CurrentRow.Cells[2].Value == null)
                        {
                            TableRefLevenshtein = Ref.TrierListeParLevenshtein(RefFiltered, "");
                        }
                        var source_ref = new BindingSource();
                        source_ref.DataSource = RefFiltered;
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
                TableCorresp = Corresp.RetourneListeCorrespondanceUpdater(TableCorresp, ligne, dataGridView_saisie.CurrentRow.Cells[15].Value.ToString());

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
            TableCorresp = Corresp.RetourneListeCorrespondanceUpdater(TableCorresp, ligne, ligne.NomRef);

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

       

        private void button_rapprochementmodule_Click(object sender, EventArgs e)
        {

            /*
            Thread backgroundThread = new Thread(
                new ThreadStart(() =>
                {
                    progressBar1.BeginInvoke(new Action(() => progressBar1.Maximum = TableCorrespFiltered.Count));
                    Fonc fonc = new Fonc();
                    Correspondances ObjCorr = new Correspondances();
                    for (int i = 0; i < TableCorrespFiltered.Count; i++)
                    {
                        if (TableCorrespFiltered[i].Nouveau_Code == "" || TableCorrespFiltered[i].Nouveau_Code == null)
                        {
                            for (int j = 0; j < RefFiltered.Count; j++)
                            {
                                if (TableCorrespFiltered[i].Canonical == fonc.CanonicalString(RefFiltered[j].Lib)
                                    && TableCorrespFiltered[i].Cpl == RefFiltered[j].Cpl
                                    && TableCorrespFiltered[i].Cpl1 == RefFiltered[j].Cpl1
                                    && TableCorrespFiltered[i].Cpl2 == RefFiltered[j].Cpl2
                                    && TableCorrespFiltered[i].TypeRef == RefFiltered[j].Type)
                                {
                                    TableCorrespFiltered[i].Nouveau_Code = RefFiltered[j].Code;
                                    TableCorrespFiltered[i].Libelle_Nouveau_Code = RefFiltered[j].Lib;
                                    if(RefFiltered[j].InActif==true)
                                    {
                                        TableCorrespFiltered[i].NouveauCodeInactif = true;
                                    }
                                    else
                                    {
                                        TableCorrespFiltered[i].NouveauCodeInactif = false;
                                    }
                                    TableCorrespFiltered[i].FlagReferentiel = 1;
                                    Corr.UpdateSQLITE_TBCorrespondance(TableCorrespFiltered[i]);
                                    Corr.UpdateSQLITE_TBCorrespondance_FlagPreventiel(TableCorrespFiltered[i]);
                                    dataGridView_saisie.BeginInvoke(new Action(() => dataGridView_saisie.Rows[i].Cells[4].Value = TableCorrespFiltered[i].Nouveau_Code));
                                    dataGridView_saisie.BeginInvoke(new Action(() => dataGridView_saisie.Rows[i].Cells[5].Value = TableCorrespFiltered[i].Libelle_Nouveau_Code));
                                    dataGridView_saisie.BeginInvoke(new Action(() => dataGridView_saisie.Rows[i].Cells[12].Value = TableCorrespFiltered[i].NouveauCodeInactif));
                                    dataGridView_saisie.BeginInvoke(new Action(() => dataGridView_saisie.Rows[i].Cells[20].Value = TableCorrespFiltered[i].FlagReferentiel));
                                }
                            }
                        }
                        progressBar1.BeginInvoke(new Action(() => progressBar1.Value = i));
                    }

                    var source = new BindingSource();
                    source.DataSource = TableCorrespFiltered;

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

          


            int nbItemRapprochés = 0;
            
            Fonc fonc = new Fonc();
            Correspondances ObjCorr = new Correspondances();
            /*
            string[] examen_ancien_code = dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Split('#');
            Correspondances CorrObject = new Correspondances();
            Correspondance ligne_exam_nouveau_code = CorrObject.RetourneCorrespondanceNouveauCode(TableCorrespFiltered, examen_ancien_code[0], "135|0|0|NOMEN");
            Referentiels RefObject = new Referentiels();
            List<Referentiel> TableRefResultatExamen = new List<Referentiel>();
            TableRefResultatExamen = RefObject.FiltrerListeResultatExamenReferentielParExamen(TableRef, ligne_exam_nouveau_code.Nouveau_Code);
            */
            //TableCorrespFiltered = TableCorresp;
            progressBar1.Maximum = TableCorrespFiltered.Count - 1;
            if ((string)comboBox_filtre.SelectedValue=="135|0|0|NOMEN")
            {
                //string[] examen_ancien_code = dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Split('#');
                
                for (int i = 0; i < TableCorrespFiltered.Count; i++)
                {
                    if (TableCorrespFiltered[i].Ancien_Code.Contains("1128"))
                        Console.WriteLine("TableCorrespFiltered[i].Ancien_Code : "+ TableCorrespFiltered[i].Ancien_Code);
                    if (TableCorrespFiltered[i].Nouveau_Code == "" || TableCorrespFiltered[i].Nouveau_Code == null)
                    {
                        for (int j = 0; j < RefFiltered.Count; j++)
                        {
                            if (TableCorrespFiltered[i].Ancien_Code.ToString().Contains("1128#") && RefFiltered[j].Cpl == "50")
                            {
                                /*
                                Console.WriteLine("TableCorrespFiltered[i].Canonical : " + TableCorrespFiltered[i].Canonical);
                                Console.WriteLine("fonc.CanonicalString(RefFiltered[j].Lib) : " + fonc.CanonicalString(RefFiltered[j].Lib));
                                Console.WriteLine("TableCorrespFiltered[i].Cpl : " + TableCorrespFiltered[i].Cpl);
                                Console.WriteLine("RefFiltered[j].Cpl : " + RefFiltered[j].Cpl);
                                Console.WriteLine("TableCorrespFiltered[i].TypeRef : " + TableCorrespFiltered[i].TypeRef);
                                Console.WriteLine("RefFiltered[j].Type : " + RefFiltered[j].Type);
                                Console.WriteLine("TableCorrespFiltered[i].Ancien_Code.ToString().Contains('#') : " + TableCorrespFiltered[i].Ancien_Code.ToString().Contains("#"));
                                //Console.WriteLine("TableCorrespFiltered[i].Canonical : " + TableCorrespFiltered[i].Canonical);
                                */
                            }

                            if (TableCorrespFiltered[i].Canonical == fonc.CanonicalString(RefFiltered[j].Lib)
                                && TableCorrespFiltered[i].Cpl == RefFiltered[j].Cpl
                                && TableCorrespFiltered[i].Cpl1 == RefFiltered[j].Cpl1
                                && TableCorrespFiltered[i].Cpl2 == RefFiltered[j].Cpl2
                                && TableCorrespFiltered[i].TypeRef == RefFiltered[j].Type
                                && TableCorrespFiltered[i].Cpl == "135"
                                && RefFiltered[j].Cpl == "135"
                                && TableCorrespFiltered[i].TypeRef == "NOMEN"
                                && RefFiltered[j].Type == "NOMEN")
                            {
                                TableCorrespFiltered[i].Nouveau_Code = RefFiltered[j].Code;
                                TableCorrespFiltered[i].Libelle_Nouveau_Code = RefFiltered[j].Lib;
                                if (RefFiltered[j].InActif == true)
                                {
                                    TableCorrespFiltered[i].NouveauCodeInactif = true;
                                }
                                else
                                {
                                    TableCorrespFiltered[i].NouveauCodeInactif = false;
                                }
                                TableCorrespFiltered[i].FlagReferentiel = 1;
                                Corr.UpdateSQLITE_TBCorrespondance(TableCorrespFiltered[i]);
                                Corr.UpdateSQLITE_TBCorrespondance_FlagPreventiel(TableCorrespFiltered[i]);
                                dataGridView_saisie.Rows[i].Cells[4].Value = TableCorrespFiltered[i].Nouveau_Code;
                                dataGridView_saisie.Rows[i].Cells[5].Value = TableCorrespFiltered[i].Libelle_Nouveau_Code;
                                dataGridView_saisie.Rows[i].Cells[12].Value = TableCorrespFiltered[i].NouveauCodeInactif;
                                dataGridView_saisie.Rows[i].Cells[20].Value = TableCorrespFiltered[i].FlagReferentiel;
                                nbItemRapprochés++;
                            }
                            else if (TableCorrespFiltered[i].Canonical == fonc.CanonicalString(RefFiltered[j].Lib)
                                && TableCorrespFiltered[i].Cpl == RefFiltered[j].Cpl
                                //&& TableCorrespFiltered[i].Cpl1 == RefFiltered[j].Cpl1
                                //&& TableCorrespFiltered[i].Cpl2 == RefFiltered[j].Cpl2
                                && TableCorrespFiltered[i].TypeRef == RefFiltered[j].Type
                                && TableCorrespFiltered[i].Cpl == "50"
                                && RefFiltered[j].Cpl == "50"
                                && TableCorrespFiltered[i].TypeRef == "CTRL"
                                && RefFiltered[j].Type == "CTRL"
                                && TableCorrespFiltered[i].Ancien_Code.ToString().Contains("#"))
                            {
                                string[] examen_ancien_code = TableCorrespFiltered[i].Ancien_Code.ToString().Split('#');
                                Correspondances CorrObject = new Correspondances();
                                Correspondance ligne_exam_nouveau_code = CorrObject.RetourneCorrespondanceNouveauCode(TableCorrespFiltered, examen_ancien_code[0], "135|0|0|NOMEN");
                                Referentiels RefObject = new Referentiels();
                                List<Referentiel> TableRefResultatExamen = new List<Referentiel>();
                                TableRefResultatExamen = RefObject.FiltrerListeResultatExamenReferentielParExamen(TableRef, ligne_exam_nouveau_code.Nouveau_Code);
                                for(int k=0;k<TableRefResultatExamen.Count;k++)
                                {
                                    if(fonc.CanonicalString(TableRefResultatExamen[k].Lib)==TableCorrespFiltered[i].Canonical)
                                    {
                                        TableCorrespFiltered[i].Nouveau_Code = TableRefResultatExamen[k].Code;
                                        TableCorrespFiltered[i].Libelle_Nouveau_Code = TableRefResultatExamen[k].Lib;
                                        if (TableRefResultatExamen[k].InActif == true)
                                        {
                                            TableCorrespFiltered[i].NouveauCodeInactif = true;
                                        }
                                        else
                                        {
                                            TableCorrespFiltered[i].NouveauCodeInactif = false;
                                        }
                                        TableCorrespFiltered[i].FlagReferentiel = 1;
                                        Corr.UpdateSQLITE_TBCorrespondance(TableCorrespFiltered[i]);
                                        Corr.UpdateSQLITE_TBCorrespondance_FlagPreventiel(TableCorrespFiltered[i]);
                                        dataGridView_saisie.Rows[i].Cells[4].Value = TableCorrespFiltered[i].Nouveau_Code;
                                        dataGridView_saisie.Rows[i].Cells[5].Value = TableCorrespFiltered[i].Libelle_Nouveau_Code;
                                        dataGridView_saisie.Rows[i].Cells[12].Value = TableCorrespFiltered[i].NouveauCodeInactif;
                                        dataGridView_saisie.Rows[i].Cells[20].Value = TableCorrespFiltered[i].FlagReferentiel;
                                        nbItemRapprochés++;
                                    }
                                }
                            }
                        }
                    }
                    progressBar1.Value = i;
                    Console.WriteLine("Avancement : " + i + "/" + TableCorrespFiltered.Count);
                }
            }
            else if((string)comboBox_filtre.SelectedValue == "141|0|0|NOMEN")
            {
                //string[] examen_ancien_code = dataGridView_saisie.CurrentRow.Cells[1].Value.ToString().Split('#');

                for (int i = 0; i < TableCorrespFiltered.Count; i++)
                {
                    if (TableCorrespFiltered[i].Ancien_Code.Contains("1128"))
                        Console.WriteLine("TableCorrespFiltered[i].Ancien_Code : " + TableCorrespFiltered[i].Ancien_Code);
                    if (TableCorrespFiltered[i].Nouveau_Code == "" || TableCorrespFiltered[i].Nouveau_Code == null)
                    {
                        for (int j = 0; j < RefFiltered.Count; j++)
                        {
                            if (TableCorrespFiltered[i].Ancien_Code.ToString().Contains("1128#") && RefFiltered[j].Cpl == "50")
                            {
                                /*
                                Console.WriteLine("TableCorrespFiltered[i].Canonical : " + TableCorrespFiltered[i].Canonical);
                                Console.WriteLine("fonc.CanonicalString(RefFiltered[j].Lib) : " + fonc.CanonicalString(RefFiltered[j].Lib));
                                Console.WriteLine("TableCorrespFiltered[i].Cpl : " + TableCorrespFiltered[i].Cpl);
                                Console.WriteLine("RefFiltered[j].Cpl : " + RefFiltered[j].Cpl);
                                Console.WriteLine("TableCorrespFiltered[i].TypeRef : " + TableCorrespFiltered[i].TypeRef);
                                Console.WriteLine("RefFiltered[j].Type : " + RefFiltered[j].Type);
                                Console.WriteLine("TableCorrespFiltered[i].Ancien_Code.ToString().Contains('#') : " + TableCorrespFiltered[i].Ancien_Code.ToString().Contains("#"));
                                //Console.WriteLine("TableCorrespFiltered[i].Canonical : " + TableCorrespFiltered[i].Canonical);
                                */
                            }

                            if (TableCorrespFiltered[i].Canonical == fonc.CanonicalString(RefFiltered[j].Lib)
                                && TableCorrespFiltered[i].Cpl == RefFiltered[j].Cpl
                                && TableCorrespFiltered[i].Cpl1 == RefFiltered[j].Cpl1
                                && TableCorrespFiltered[i].Cpl2 == RefFiltered[j].Cpl2
                                && TableCorrespFiltered[i].TypeRef == RefFiltered[j].Type
                                && TableCorrespFiltered[i].Cpl == "141"
                                && RefFiltered[j].Cpl == "141"
                                && TableCorrespFiltered[i].TypeRef == "NOMEN"
                                && RefFiltered[j].Type == "NOMEN")
                            {
                                TableCorrespFiltered[i].Nouveau_Code = RefFiltered[j].Code;
                                TableCorrespFiltered[i].Libelle_Nouveau_Code = RefFiltered[j].Lib;
                                if (RefFiltered[j].InActif == true)
                                {
                                    TableCorrespFiltered[i].NouveauCodeInactif = true;
                                }
                                else
                                {
                                    TableCorrespFiltered[i].NouveauCodeInactif = false;
                                }
                                TableCorrespFiltered[i].FlagReferentiel = 1;
                                Corr.UpdateSQLITE_TBCorrespondance(TableCorrespFiltered[i]);
                                Corr.UpdateSQLITE_TBCorrespondance_FlagPreventiel(TableCorrespFiltered[i]);
                                dataGridView_saisie.Rows[i].Cells[4].Value = TableCorrespFiltered[i].Nouveau_Code;
                                dataGridView_saisie.Rows[i].Cells[5].Value = TableCorrespFiltered[i].Libelle_Nouveau_Code;
                                dataGridView_saisie.Rows[i].Cells[12].Value = TableCorrespFiltered[i].NouveauCodeInactif;
                                dataGridView_saisie.Rows[i].Cells[20].Value = TableCorrespFiltered[i].FlagReferentiel;
                                nbItemRapprochés++;
                            }
                            else if (TableCorrespFiltered[i].Canonical == fonc.CanonicalString(RefFiltered[j].Lib)
                                && TableCorrespFiltered[i].Cpl == RefFiltered[j].Cpl
                                //&& TableCorrespFiltered[i].Cpl1 == RefFiltered[j].Cpl1
                                //&& TableCorrespFiltered[i].Cpl2 == RefFiltered[j].Cpl2
                                && TableCorrespFiltered[i].TypeRef == RefFiltered[j].Type
                                && TableCorrespFiltered[i].Cpl == "60"
                                && RefFiltered[j].Cpl == "60"
                                && TableCorrespFiltered[i].TypeRef == "CTRL"
                                && RefFiltered[j].Type == "CTRL"
                                && TableCorrespFiltered[i].Ancien_Code.ToString().Contains("#"))
                            {
                                string[] vaccin_ancien_code = TableCorrespFiltered[i].Ancien_Code.ToString().Split('#');
                                Correspondances CorrObject = new Correspondances();
                                Correspondance ligne_vaccin_nouveau_code = CorrObject.RetourneCorrespondanceNouveauCode(TableCorrespFiltered, vaccin_ancien_code[0], "141|0|0|NOMEN");
                                Referentiels RefObject = new Referentiels();
                                List<Referentiel> TableProtocoleVaccinal = new List<Referentiel>();
                                TableProtocoleVaccinal = RefObject.FiltrerListeResultatExamenReferentielParExamen(TableRef, ligne_vaccin_nouveau_code.Nouveau_Code);
                                for (int k = 0; k < TableProtocoleVaccinal.Count; k++)
                                {
                                    if (fonc.CanonicalString(TableProtocoleVaccinal[k].Lib) == TableCorrespFiltered[i].Canonical)
                                    {
                                        TableCorrespFiltered[i].Nouveau_Code = TableProtocoleVaccinal[k].Code;
                                        TableCorrespFiltered[i].Libelle_Nouveau_Code = TableProtocoleVaccinal[k].Lib;
                                        if (TableProtocoleVaccinal[k].InActif == true)
                                        {
                                            TableCorrespFiltered[i].NouveauCodeInactif = true;
                                        }
                                        else
                                        {
                                            TableCorrespFiltered[i].NouveauCodeInactif = false;
                                        }
                                        TableCorrespFiltered[i].FlagReferentiel = 1;
                                        Corr.UpdateSQLITE_TBCorrespondance(TableCorrespFiltered[i]);
                                        Corr.UpdateSQLITE_TBCorrespondance_FlagPreventiel(TableCorrespFiltered[i]);
                                        dataGridView_saisie.Rows[i].Cells[4].Value = TableCorrespFiltered[i].Nouveau_Code;
                                        dataGridView_saisie.Rows[i].Cells[5].Value = TableCorrespFiltered[i].Libelle_Nouveau_Code;
                                        dataGridView_saisie.Rows[i].Cells[12].Value = TableCorrespFiltered[i].NouveauCodeInactif;
                                        dataGridView_saisie.Rows[i].Cells[20].Value = TableCorrespFiltered[i].FlagReferentiel;
                                        nbItemRapprochés++;
                                    }
                                }
                            }
                        }
                    }
                    progressBar1.Value = i;
                    Console.WriteLine("Avancement : " + i + "/" + TableCorrespFiltered.Count);
                }
            }
            else
            {
                for (int i = 0; i < TableCorrespFiltered.Count; i++)
                {
                    if (TableCorrespFiltered[i].Nouveau_Code == "" || TableCorrespFiltered[i].Nouveau_Code == null)
                    {
                        for (int j = 0; j < RefFiltered.Count; j++)
                        {
                            if (TableCorrespFiltered[i].Canonical == fonc.CanonicalString(RefFiltered[j].Lib)
                                && TableCorrespFiltered[i].Cpl == RefFiltered[j].Cpl
                                && TableCorrespFiltered[i].Cpl1 == RefFiltered[j].Cpl1
                                && TableCorrespFiltered[i].Cpl2 == RefFiltered[j].Cpl2
                                && TableCorrespFiltered[i].TypeRef == RefFiltered[j].Type)
                            {
                                TableCorrespFiltered[i].Nouveau_Code = RefFiltered[j].Code;
                                TableCorrespFiltered[i].Libelle_Nouveau_Code = RefFiltered[j].Lib;
                                if (RefFiltered[j].InActif == true)
                                {
                                    TableCorrespFiltered[i].NouveauCodeInactif = true;
                                }
                                else
                                {
                                    TableCorrespFiltered[i].NouveauCodeInactif = false;
                                }
                                TableCorrespFiltered[i].FlagReferentiel = 1;
                                Corr.UpdateSQLITE_TBCorrespondance(TableCorrespFiltered[i]);
                                Corr.UpdateSQLITE_TBCorrespondance_FlagPreventiel(TableCorrespFiltered[i]);
                                dataGridView_saisie.Rows[i].Cells[4].Value = TableCorrespFiltered[i].Nouveau_Code;
                                dataGridView_saisie.Rows[i].Cells[5].Value = TableCorrespFiltered[i].Libelle_Nouveau_Code;
                                dataGridView_saisie.Rows[i].Cells[12].Value = TableCorrespFiltered[i].NouveauCodeInactif;
                                dataGridView_saisie.Rows[i].Cells[20].Value = TableCorrespFiltered[i].FlagReferentiel;
                                nbItemRapprochés++;
                            }
                        }
                    }
                    progressBar1.Value = i;
                    Console.WriteLine("Avancement : " + i + "/" + TableCorrespFiltered.Count);
                }
            }

            var source = new BindingSource();
            source.DataSource = TableCorrespFiltered;

            //dataGridView_saisie.BeginInvoke(new Action(() => dataGridView_saisie.DataSource = source));

            dataGridView_saisie.DataSource = source;

            InitStructSaisie();
            InitColorSaisie();
            MessageBox.Show("Rapprochement automatique terminé. \r\n" + nbItemRapprochés.ToString() + " code(s) rapproché(s) automatiquement.");
            progressBar1.Value = 0;
            button_deleteRecodage.Enabled = true;
            button_afficherCreationCode.Enabled = true;
            button_affecter.Enabled = true;
        }
    }
}
