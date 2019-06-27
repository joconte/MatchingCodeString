using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RecodageList.BLL;

namespace RecodageList.GUI
{
    public partial class RattachementEtablissementEntreprise : Form
    {
        public Form1 form1 { get; set; }

        public RattachementEtablissementEntreprise()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void RattachementEtablissementEntreprise_Load(object sender, EventArgs e)
        {
            ComboBox_EntrepriseBLL comboBox_EntrepriseBLL = new ComboBox_EntrepriseBLL();
            comboBox_EntrepriseBLL.myformrattachement = this;
            comboBox_EntrepriseBLL.InitComboBoxRattachementEntEtab(VariablePartage.TableReferentiel);
        }

        private void RattachementEtablissementEntreprise_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Annuler();
        }

        private void button_annuler_ent_etab_Click(object sender, EventArgs e)
        {
            Annuler();
        }

        public void Annuler()
        {
            ActionRecodage actionRecodage = new ActionRecodage();
            actionRecodage.DesaffecteCode(form1.dataGridView_saisie, form1.dataGridView_ref, form1);
            Close();
        }

        private void button_save_ent_etab_Click(object sender, EventArgs e)
        {
            Valider();
        }

        public void Valider()
        {
            if(radioButton_existante.Checked)
            {
                ComboBox_EntrepriseBLL comboBox_EntrepriseBLL = new ComboBox_EntrepriseBLL();
                comboBox_EntrepriseBLL.myformrattachement = this;
                CorrespondanceBLL ligne_selectionne_datagrid_ent_etab = new CorrespondanceBLL();
                ligne_selectionne_datagrid_ent_etab = ligne_selectionne_datagrid_ent_etab.RetourneLigneCorrespondanceDatagrid(form1.dataGridView_saisie, form1.dataGridView_saisie.CurrentCell);
                CorrespondanceBLL ligne_selectionne_objet_ent_etab = new CorrespondanceBLL();
                ligne_selectionne_objet_ent_etab = ligne_selectionne_objet_ent_etab.TrouveLigneCorrespondance_ByLigne(ligne_selectionne_datagrid_ent_etab, VariablePartage.TableCorrespondanceFiltre);
                comboBox_EntrepriseBLL.AjouteLigneEntEtab_ByComboBoxEntEtab(form1, this, ligne_selectionne_objet_ent_etab);
                Close();
            }
            else if (radioButton_creer_ent.Checked)
            {
                ComboBox_EntrepriseBLL comboBox_EntrepriseBLL = new ComboBox_EntrepriseBLL();
                comboBox_EntrepriseBLL.myformrattachement = this;
                CorrespondanceBLL ligne_selectionne_datagrid_ent_etab = new CorrespondanceBLL();
                ligne_selectionne_datagrid_ent_etab = ligne_selectionne_datagrid_ent_etab.RetourneLigneCorrespondanceDatagrid(form1.dataGridView_saisie, form1.dataGridView_saisie.CurrentCell);
                CorrespondanceBLL ligne_selectionne_objet_ent_etab = new CorrespondanceBLL();
                ligne_selectionne_objet_ent_etab = ligne_selectionne_objet_ent_etab.TrouveLigneCorrespondance_ByLigne(ligne_selectionne_datagrid_ent_etab, VariablePartage.TableCorrespondanceFiltre);
                comboBox_EntrepriseBLL.AjoutLigneEntEtab_ByCreerEnt(form1, this, ligne_selectionne_objet_ent_etab);
                Close();
            }
        }

        public void AlgoCalculInterface()
        {
            if(radioButton_existante.Checked)
            {
                comboBox_entreprise_existante.Enabled = true;
                textBox_code_entreprise_a_creer.Enabled = false;
                textBox_libelle_ent_a_creer.Enabled = false;
            }
            else if (radioButton_creer_ent.Checked)
            {
                textBox_code_entreprise_a_creer.Enabled = true;
                textBox_libelle_ent_a_creer.Enabled = true;
                comboBox_entreprise_existante.Enabled = false;
            }
        }

        private void radioButton_existante_CheckedChanged(object sender, EventArgs e)
        {
            AlgoCalculInterface();
        }

        private void radioButton_creer_ent_CheckedChanged(object sender, EventArgs e)
        {
            AlgoCalculInterface();
        }

        private void comboBox_entreprise_existante_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Item selectionné : " + (comboBox_entreprise_existante.SelectedItem as ComboBox_EntrepriseBLL).Code.ToString() + " - " 
                + (comboBox_entreprise_existante.SelectedItem as ComboBox_EntrepriseBLL).Lib.ToString()) ;
        }
    }
}
