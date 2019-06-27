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
    public partial class InterfaceCreerCodeAvance : Form
    {
        public Form1 myform;

        public InterfaceCreerCodeAvance(Form1 _myform)
        {
            myform = _myform;
            InitializeComponent();
        }

        public InterfaceCreerCodeAvance()
        {
            InitializeComponent();
        }

        private void button_annuler_nouveau_code_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_save_nouveau_code_Click(object sender, EventArgs e)
        {
            Valider();
        }

        public void Valider()
        {
            ActionRecodage actionRecodage = new ActionRecodage();
            //Form1 myform = new Form1();
            bool codeactif;
            if(checkBox_inactif.Checked ==true)
            {
                codeactif = false;
            }
            else
            {
                codeactif = true;
            }
            actionRecodage.CreerCodeInterfaceAvancee(myform.dataGridView_saisie, myform.dataGridView_ref, myform, textBox_nouveau_code.Text, textBox_libelle_nouveau_code.Text,codeactif);
            this.Close();
        }
    }
}
