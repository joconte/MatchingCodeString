namespace RecodageList.GUI
{
    partial class RattachementEtablissementEntreprise
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
            this.radioButton_creer_ent = new System.Windows.Forms.RadioButton();
            this.radioButton_existante = new System.Windows.Forms.RadioButton();
            this.comboBox_entreprise_existante = new System.Windows.Forms.ComboBox();
            this.textBox_code_entreprise_a_creer = new System.Windows.Forms.TextBox();
            this.button_save_ent_etab = new System.Windows.Forms.Button();
            this.button_annuler_ent_etab = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_libelle_ent_a_creer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(422, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vous venez de créer un établissement, vous devez le rattacher à une entreprise. \r" +
    "\nVous pouvez le rattacher à une entreprise déjà existante ou bien décider d\'en c" +
    "réer une.";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // radioButton_creer_ent
            // 
            this.radioButton_creer_ent.AutoSize = true;
            this.radioButton_creer_ent.Location = new System.Drawing.Point(12, 114);
            this.radioButton_creer_ent.Name = "radioButton_creer_ent";
            this.radioButton_creer_ent.Size = new System.Drawing.Size(99, 17);
            this.radioButton_creer_ent.TabIndex = 1;
            this.radioButton_creer_ent.TabStop = true;
            this.radioButton_creer_ent.Text = "Créer entreprise";
            this.radioButton_creer_ent.UseVisualStyleBackColor = true;
            this.radioButton_creer_ent.CheckedChanged += new System.EventHandler(this.radioButton_creer_ent_CheckedChanged);
            // 
            // radioButton_existante
            // 
            this.radioButton_existante.AutoSize = true;
            this.radioButton_existante.Location = new System.Drawing.Point(12, 69);
            this.radioButton_existante.Name = "radioButton_existante";
            this.radioButton_existante.Size = new System.Drawing.Size(117, 17);
            this.radioButton_existante.TabIndex = 2;
            this.radioButton_existante.TabStop = true;
            this.radioButton_existante.Text = "Entreprise existante";
            this.radioButton_existante.UseVisualStyleBackColor = true;
            this.radioButton_existante.CheckedChanged += new System.EventHandler(this.radioButton_existante_CheckedChanged);
            // 
            // comboBox_entreprise_existante
            // 
            this.comboBox_entreprise_existante.FormattingEnabled = true;
            this.comboBox_entreprise_existante.Location = new System.Drawing.Point(135, 69);
            this.comboBox_entreprise_existante.Name = "comboBox_entreprise_existante";
            this.comboBox_entreprise_existante.Size = new System.Drawing.Size(299, 21);
            this.comboBox_entreprise_existante.TabIndex = 3;
            this.comboBox_entreprise_existante.SelectedIndexChanged += new System.EventHandler(this.comboBox_entreprise_existante_SelectedIndexChanged);
            // 
            // textBox_code_entreprise_a_creer
            // 
            this.textBox_code_entreprise_a_creer.Location = new System.Drawing.Point(135, 132);
            this.textBox_code_entreprise_a_creer.Name = "textBox_code_entreprise_a_creer";
            this.textBox_code_entreprise_a_creer.Size = new System.Drawing.Size(72, 20);
            this.textBox_code_entreprise_a_creer.TabIndex = 4;
            // 
            // button_save_ent_etab
            // 
            this.button_save_ent_etab.Location = new System.Drawing.Point(360, 165);
            this.button_save_ent_etab.Name = "button_save_ent_etab";
            this.button_save_ent_etab.Size = new System.Drawing.Size(75, 23);
            this.button_save_ent_etab.TabIndex = 5;
            this.button_save_ent_etab.Text = "Valider";
            this.button_save_ent_etab.UseVisualStyleBackColor = true;
            this.button_save_ent_etab.Click += new System.EventHandler(this.button_save_ent_etab_Click);
            // 
            // button_annuler_ent_etab
            // 
            this.button_annuler_ent_etab.Location = new System.Drawing.Point(279, 165);
            this.button_annuler_ent_etab.Name = "button_annuler_ent_etab";
            this.button_annuler_ent_etab.Size = new System.Drawing.Size(75, 23);
            this.button_annuler_ent_etab.TabIndex = 6;
            this.button_annuler_ent_etab.Text = "Annuler";
            this.button_annuler_ent_etab.UseVisualStyleBackColor = true;
            this.button_annuler_ent_etab.Click += new System.EventHandler(this.button_annuler_ent_etab_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(132, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Code a créer";
            // 
            // textBox_libelle_ent_a_creer
            // 
            this.textBox_libelle_ent_a_creer.Location = new System.Drawing.Point(213, 132);
            this.textBox_libelle_ent_a_creer.Name = "textBox_libelle_ent_a_creer";
            this.textBox_libelle_ent_a_creer.Size = new System.Drawing.Size(221, 20);
            this.textBox_libelle_ent_a_creer.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(210, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Libellé à créer";
            // 
            // RattachementEtablissementEntreprise
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 200);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_libelle_ent_a_creer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_annuler_ent_etab);
            this.Controls.Add(this.button_save_ent_etab);
            this.Controls.Add(this.textBox_code_entreprise_a_creer);
            this.Controls.Add(this.comboBox_entreprise_existante);
            this.Controls.Add(this.radioButton_existante);
            this.Controls.Add(this.radioButton_creer_ent);
            this.Controls.Add(this.label1);
            this.Name = "RattachementEtablissementEntreprise";
            this.Text = "RattachementEtablissementEntreprise";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RattachementEtablissementEntreprise_FormClosing);
            this.Load += new System.EventHandler(this.RattachementEtablissementEntreprise_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton_creer_ent;
        private System.Windows.Forms.RadioButton radioButton_existante;
        public System.Windows.Forms.ComboBox comboBox_entreprise_existante;
        public System.Windows.Forms.TextBox textBox_code_entreprise_a_creer;
        private System.Windows.Forms.Button button_save_ent_etab;
        private System.Windows.Forms.Button button_annuler_ent_etab;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBox_libelle_ent_a_creer;
        private System.Windows.Forms.Label label3;
    }
}