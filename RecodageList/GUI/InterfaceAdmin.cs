using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Converter;
using RecodageList.BLL;

namespace RecodageList.GUI
{
    public partial class InterfaceAdmin : Form
    {
        public InterfaceAdmin()
        {
            InitializeComponent();
        }

        private void button_parambase_Click(object sender, EventArgs e)
        {
            new MainForm().Show();
        }

        private void button_save_for_client_Click(object sender, EventArgs e)
        {
            GUIFonction GUI = new GUIFonction();
            Form1 myform = new Form1();
            Chargement myformchargement = new Chargement();
            //myformchargement.label_trt_encours.Text = "Enregistrement de la base pour client";
            //myformchargement.Show();
            GUI.EnregistrerBaseSQLITE(myform, myformchargement);
        }

        private void button_exportCorresp_Click(object sender, EventArgs e)
        {
            GUIFonction GUI = new GUIFonction();
            Chargement myformchargement = new Chargement();
            myformchargement.label_trt_encours.Text = "Exportation vers SQLServer";
            myformchargement.Show();
            GUI.ExporteCorrespondance(this, myformchargement);
        }
    }
}
