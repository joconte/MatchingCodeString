using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RecodageList.DAL;

namespace RecodageList
{
    public partial class Form1 : Form
    {

        static Correspondances Corr = new Correspondances();
        static List<Correspondance> TableCorresp = Corr.ObtenirListeCorrespondance_SQLITE();
        static CorrespondancesAffichages CorrAffichage = new CorrespondancesAffichages();
        static List<CorrespondanceAffichage> TableCorrespAffichage = CorrAffichage.ObtenirListeCorrespondanceAffichage(TableCorresp);

        static Referentiels referentiel = new Referentiels();
        static List<Referentiel> TableRef = referentiel.ObtenirListeReferentiel_SQLITE();
        static ReferentielsAffichages RefAffichage = new ReferentielsAffichages();
        static List<ReferentielAffichage> TableRefAffichage = RefAffichage.ObtenirListeReferentielAffichage(TableRef);

        static ComboBoxFiltres combobox_object = new ComboBoxFiltres();
        static List<ComboBoxFiltre> comboboxfiltre = combobox_object.ObtenirComboBoxFiltre();

        public Form1()
        {
            InitializeComponent();
            InitDatagridSaisie();
            InitDatagridReferentiel();
            InitComboBoxFiltre();
        }

        public void InitDatagridSaisie()
        {
            var source = new BindingSource();
            source.DataSource = TableCorrespAffichage;
            dataGridView_saisie.DataSource = source;
        }

        public void InitDatagridReferentiel()
        {
            var source = new BindingSource();
            source.DataSource = TableRefAffichage;
            dataGridView_ref.DataSource = source;
        }

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
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button_save_Click(object sender, EventArgs e)
        {
            
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

        private void button_filter_Click(object sender, EventArgs e)
        {
            Correspondances Corr = new Correspondances();
            Console.WriteLine(dataGridView_saisie.DataMember);
            //List<Correspondance> TableCorresp = Corr.ObtenirListeCorrespondance_SQLITE();
            List<Correspondance> CorrFiltered = new List<Correspondance>();
            Console.WriteLine(comboBox_filtre.ValueMember);
            CorrFiltered = Corr.FiltrerListeCorrespondance_parCPL(TableCorresp, (string)comboBox_filtre.SelectedValue);
            CorrespondancesAffichages CorrAffichage = new CorrespondancesAffichages();
            List<CorrespondanceAffichage> TableCorrespAffichage = CorrAffichage.ObtenirListeCorrespondanceAffichage(CorrFiltered);
            var source = new BindingSource();
            source.DataSource = TableCorrespAffichage;
            dataGridView_saisie.DataSource = source;

            Referentiels Ref = new Referentiels();
            //List<Referentiel> TableRef = Ref.ObtenirListeReferentiel_SQLITE();
            List<Referentiel> RefFiltered = new List<Referentiel>();
            RefFiltered = Ref.FiltrerListeReferentiel_parCPL(TableRef, (string)comboBox_filtre.SelectedValue);
            ReferentielsAffichages RefAffichage = new ReferentielsAffichages();
            List<ReferentielAffichage> TableRefAffichage = RefAffichage.ObtenirListeReferentielAffichage(RefFiltered);
            var source_ref = new BindingSource();
            source_ref.DataSource = TableRefAffichage;
            dataGridView_ref.DataSource = source_ref;
        }

        private void button_deletefilter_Click(object sender, EventArgs e)
        {
            InitDatagridSaisie();
            InitDatagridReferentiel();
        }
    }
}
