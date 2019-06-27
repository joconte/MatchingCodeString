namespace RecodageList.GUI
{
    partial class InterfaceCreerCodeAvance
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_ancien_code = new System.Windows.Forms.TextBox();
            this.textBox_libelle_ancien_code = new System.Windows.Forms.TextBox();
            this.textBox_nouveau_code = new System.Windows.Forms.TextBox();
            this.textBox_libelle_nouveau_code = new System.Windows.Forms.TextBox();
            this.button_annuler_nouveau_code = new System.Windows.Forms.Button();
            this.button_save_nouveau_code = new System.Windows.Forms.Button();
            this.checkBox_inactif = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Code Source";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Intitulé Source";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(359, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nouveau Code";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(465, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Nouveau Libellé";
            // 
            // textBox_ancien_code
            // 
            this.textBox_ancien_code.Location = new System.Drawing.Point(12, 29);
            this.textBox_ancien_code.Name = "textBox_ancien_code";
            this.textBox_ancien_code.Size = new System.Drawing.Size(100, 20);
            this.textBox_ancien_code.TabIndex = 4;
            // 
            // textBox_libelle_ancien_code
            // 
            this.textBox_libelle_ancien_code.Location = new System.Drawing.Point(118, 28);
            this.textBox_libelle_ancien_code.Name = "textBox_libelle_ancien_code";
            this.textBox_libelle_ancien_code.Size = new System.Drawing.Size(238, 20);
            this.textBox_libelle_ancien_code.TabIndex = 5;
            // 
            // textBox_nouveau_code
            // 
            this.textBox_nouveau_code.Location = new System.Drawing.Point(362, 28);
            this.textBox_nouveau_code.Name = "textBox_nouveau_code";
            this.textBox_nouveau_code.Size = new System.Drawing.Size(100, 20);
            this.textBox_nouveau_code.TabIndex = 6;
            // 
            // textBox_libelle_nouveau_code
            // 
            this.textBox_libelle_nouveau_code.Location = new System.Drawing.Point(468, 29);
            this.textBox_libelle_nouveau_code.Name = "textBox_libelle_nouveau_code";
            this.textBox_libelle_nouveau_code.Size = new System.Drawing.Size(306, 20);
            this.textBox_libelle_nouveau_code.TabIndex = 7;
            // 
            // button_annuler_nouveau_code
            // 
            this.button_annuler_nouveau_code.Location = new System.Drawing.Point(677, 56);
            this.button_annuler_nouveau_code.Name = "button_annuler_nouveau_code";
            this.button_annuler_nouveau_code.Size = new System.Drawing.Size(75, 23);
            this.button_annuler_nouveau_code.TabIndex = 8;
            this.button_annuler_nouveau_code.Text = "Annuler";
            this.button_annuler_nouveau_code.UseVisualStyleBackColor = true;
            this.button_annuler_nouveau_code.Click += new System.EventHandler(this.button_annuler_nouveau_code_Click);
            // 
            // button_save_nouveau_code
            // 
            this.button_save_nouveau_code.Location = new System.Drawing.Point(758, 56);
            this.button_save_nouveau_code.Name = "button_save_nouveau_code";
            this.button_save_nouveau_code.Size = new System.Drawing.Size(75, 23);
            this.button_save_nouveau_code.TabIndex = 9;
            this.button_save_nouveau_code.Text = "Valider";
            this.button_save_nouveau_code.UseVisualStyleBackColor = true;
            this.button_save_nouveau_code.Click += new System.EventHandler(this.button_save_nouveau_code_Click);
            // 
            // checkBox_inactif
            // 
            this.checkBox_inactif.AutoSize = true;
            this.checkBox_inactif.Location = new System.Drawing.Point(782, 32);
            this.checkBox_inactif.Name = "checkBox_inactif";
            this.checkBox_inactif.Size = new System.Drawing.Size(55, 17);
            this.checkBox_inactif.TabIndex = 11;
            this.checkBox_inactif.Text = "Inactif";
            this.checkBox_inactif.UseVisualStyleBackColor = true;
            // 
            // InterfaceCreerCodeAvance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 91);
            this.Controls.Add(this.checkBox_inactif);
            this.Controls.Add(this.button_save_nouveau_code);
            this.Controls.Add(this.button_annuler_nouveau_code);
            this.Controls.Add(this.textBox_libelle_nouveau_code);
            this.Controls.Add(this.textBox_nouveau_code);
            this.Controls.Add(this.textBox_libelle_ancien_code);
            this.Controls.Add(this.textBox_ancien_code);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "InterfaceCreerCodeAvance";
            this.Text = "InterfaceCreerCodeAvance";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox textBox_ancien_code;
        public System.Windows.Forms.TextBox textBox_libelle_ancien_code;
        public System.Windows.Forms.TextBox textBox_nouveau_code;
        public System.Windows.Forms.TextBox textBox_libelle_nouveau_code;
        public System.Windows.Forms.Button button_annuler_nouveau_code;
        public System.Windows.Forms.Button button_save_nouveau_code;
        public System.Windows.Forms.CheckBox checkBox_inactif;
    }
}