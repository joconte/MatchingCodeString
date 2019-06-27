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
    public partial class InterfaceAdmin_pass : Form
    {
        public InterfaceAdmin_pass()
        {
            InitializeComponent();
        }

        private void textBox_pass_admin_TextChanged(object sender, EventArgs e)
        {

        }

        private void button_okmdpadmin_Click(object sender, EventArgs e)
        {
            Enter_OK_Admin(this);
        }

        public void Enter_OK_Admin(InterfaceAdmin_pass myform)
        {
            string PassAdmin = "TeamReprise";
            if (myform.textBox_pass_admin.Text == PassAdmin)
            {
                InterfaceAdmin interfaceAdmin = new InterfaceAdmin();
                interfaceAdmin.ShowDialog();
                myform.Close();
            }
            else
            {
                MessageBox.Show("Mot de passe incorrect.");
            }
        }


        private void textBox_pass_admin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Enter_OK_Admin(this);
            }
        }

        private void InterfaceAdmin_pass_Load(object sender, EventArgs e)
        {

        }
    }
}
