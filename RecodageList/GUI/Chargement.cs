using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using RecodageList.BLL;

namespace RecodageList.GUI
{
    public partial class Chargement : Form
    {
        public Chargement()
        {
            InitializeComponent();
        }

        private void Chargement_Load(object sender, EventArgs e)
        {
            
        }

        private void button_annuler_chargement_Click(object sender, EventArgs e)
        {
            VariablePartage.ThreadStop = true;
        }
    }
}
