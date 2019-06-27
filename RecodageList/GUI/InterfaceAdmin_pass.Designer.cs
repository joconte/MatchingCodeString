namespace RecodageList.GUI
{
    partial class InterfaceAdmin_pass
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_okmdpadmin = new System.Windows.Forms.Button();
            this.textBox_pass_admin = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_okmdpadmin
            // 
            this.button_okmdpadmin.Location = new System.Drawing.Point(116, 28);
            this.button_okmdpadmin.Name = "button_okmdpadmin";
            this.button_okmdpadmin.Size = new System.Drawing.Size(35, 23);
            this.button_okmdpadmin.TabIndex = 29;
            this.button_okmdpadmin.Text = "OK";
            this.button_okmdpadmin.UseVisualStyleBackColor = true;
            this.button_okmdpadmin.Click += new System.EventHandler(this.button_okmdpadmin_Click);
            // 
            // textBox_pass_admin
            // 
            this.textBox_pass_admin.Location = new System.Drawing.Point(15, 28);
            this.textBox_pass_admin.Name = "textBox_pass_admin";
            this.textBox_pass_admin.PasswordChar = '*';
            this.textBox_pass_admin.Size = new System.Drawing.Size(95, 20);
            this.textBox_pass_admin.TabIndex = 28;
            this.textBox_pass_admin.TextChanged += new System.EventHandler(this.textBox_pass_admin_TextChanged);
            this.textBox_pass_admin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_pass_admin_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Veuillez entrer le mot de passe admin";
            // 
            // InterfaceAdmin_pass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(199, 61);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_okmdpadmin);
            this.Controls.Add(this.textBox_pass_admin);
            this.Name = "InterfaceAdmin_pass";
            this.Text = "InterfaceAdmin_pass";
            this.Load += new System.EventHandler(this.InterfaceAdmin_pass_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button button_okmdpadmin;
        public System.Windows.Forms.TextBox textBox_pass_admin;
        private System.Windows.Forms.Label label1;
    }
}