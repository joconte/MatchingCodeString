namespace RecodageList
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView_saisie = new System.Windows.Forms.DataGridView();
            this.button_affecter = new System.Windows.Forms.Button();
            this.comboBox_filtre = new System.Windows.Forms.ComboBox();
            this.button_filter = new System.Windows.Forms.Button();
            this.dataGridView_ref = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button_afficherCreationCode = new System.Windows.Forms.Button();
            this.textBox_codeacreer = new System.Windows.Forms.TextBox();
            this.textBox_libellecodeacreer = new System.Windows.Forms.TextBox();
            this.checkBox_codeacreer_actif_inactif = new System.Windows.Forms.CheckBox();
            this.button_creercode = new System.Windows.Forms.Button();
            this.button_deleteRecodage = new System.Windows.Forms.Button();
            this.button_rapprochementmodule = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button_parambase = new System.Windows.Forms.Button();
            this.button_chargement = new System.Windows.Forms.Button();
            this.progressBar_chargementlogiciel = new System.Windows.Forms.ProgressBar();
            this.button_testsqlserver = new System.Windows.Forms.Button();
            this.button_exportCorresp = new System.Windows.Forms.Button();
            this.button_rapprochement_global = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_saisie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ref)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_saisie
            // 
            this.dataGridView_saisie.AllowUserToAddRows = false;
            this.dataGridView_saisie.AllowUserToDeleteRows = false;
            this.dataGridView_saisie.AllowUserToResizeRows = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            this.dataGridView_saisie.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView_saisie.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_saisie.Location = new System.Drawing.Point(62, 42);
            this.dataGridView_saisie.Name = "dataGridView_saisie";
            this.dataGridView_saisie.ReadOnly = true;
            this.dataGridView_saisie.RowHeadersVisible = false;
            this.dataGridView_saisie.Size = new System.Drawing.Size(1053, 324);
            this.dataGridView_saisie.TabIndex = 0;
            this.dataGridView_saisie.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView_saisie.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_saisie_CellClick);
            this.dataGridView_saisie.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView_saisie_KeyDown);
            // 
            // button_affecter
            // 
            this.button_affecter.Enabled = false;
            this.button_affecter.Location = new System.Drawing.Point(1121, 420);
            this.button_affecter.Name = "button_affecter";
            this.button_affecter.Size = new System.Drawing.Size(75, 23);
            this.button_affecter.TabIndex = 1;
            this.button_affecter.Text = "Affecter code";
            this.button_affecter.UseVisualStyleBackColor = true;
            this.button_affecter.Click += new System.EventHandler(this.button_save_Click);
            // 
            // comboBox_filtre
            // 
            this.comboBox_filtre.FormattingEnabled = true;
            this.comboBox_filtre.Items.AddRange(new object[] {
            "123",
            "124",
            "142"});
            this.comboBox_filtre.Location = new System.Drawing.Point(60, 13);
            this.comboBox_filtre.Name = "comboBox_filtre";
            this.comboBox_filtre.Size = new System.Drawing.Size(121, 21);
            this.comboBox_filtre.TabIndex = 4;
            this.comboBox_filtre.SelectedValueChanged += new System.EventHandler(this.comboBox_filtre_SelectedValueChanged);
            // 
            // button_filter
            // 
            this.button_filter.Location = new System.Drawing.Point(187, 13);
            this.button_filter.Name = "button_filter";
            this.button_filter.Size = new System.Drawing.Size(75, 23);
            this.button_filter.TabIndex = 5;
            this.button_filter.Text = "Filtrer";
            this.button_filter.UseVisualStyleBackColor = true;
            this.button_filter.Click += new System.EventHandler(this.button_filter_Click);
            // 
            // dataGridView_ref
            // 
            this.dataGridView_ref.AllowUserToAddRows = false;
            this.dataGridView_ref.AllowUserToDeleteRows = false;
            this.dataGridView_ref.AllowUserToResizeRows = false;
            this.dataGridView_ref.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_ref.Location = new System.Drawing.Point(62, 390);
            this.dataGridView_ref.Name = "dataGridView_ref";
            this.dataGridView_ref.ReadOnly = true;
            this.dataGridView_ref.RowHeadersVisible = false;
            this.dataGridView_ref.Size = new System.Drawing.Size(1053, 329);
            this.dataGridView_ref.TabIndex = 6;
            this.dataGridView_ref.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_ref_CellContentClick);
            this.dataGridView_ref.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_ref_CellMouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 189);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Saisie";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-2, 549);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Référentiel";
            // 
            // button_afficherCreationCode
            // 
            this.button_afficherCreationCode.Enabled = false;
            this.button_afficherCreationCode.Location = new System.Drawing.Point(1174, 101);
            this.button_afficherCreationCode.Name = "button_afficherCreationCode";
            this.button_afficherCreationCode.Size = new System.Drawing.Size(96, 23);
            this.button_afficherCreationCode.TabIndex = 10;
            this.button_afficherCreationCode.Text = "Création code";
            this.button_afficherCreationCode.UseVisualStyleBackColor = true;
            this.button_afficherCreationCode.Click += new System.EventHandler(this.button_afficherCreationCode_Click);
            // 
            // textBox_codeacreer
            // 
            this.textBox_codeacreer.Enabled = false;
            this.textBox_codeacreer.Location = new System.Drawing.Point(1174, 146);
            this.textBox_codeacreer.Name = "textBox_codeacreer";
            this.textBox_codeacreer.Size = new System.Drawing.Size(134, 20);
            this.textBox_codeacreer.TabIndex = 11;
            // 
            // textBox_libellecodeacreer
            // 
            this.textBox_libellecodeacreer.Enabled = false;
            this.textBox_libellecodeacreer.Location = new System.Drawing.Point(1174, 172);
            this.textBox_libellecodeacreer.Name = "textBox_libellecodeacreer";
            this.textBox_libellecodeacreer.Size = new System.Drawing.Size(134, 20);
            this.textBox_libellecodeacreer.TabIndex = 12;
            // 
            // checkBox_codeacreer_actif_inactif
            // 
            this.checkBox_codeacreer_actif_inactif.AutoSize = true;
            this.checkBox_codeacreer_actif_inactif.Enabled = false;
            this.checkBox_codeacreer_actif_inactif.Location = new System.Drawing.Point(1174, 198);
            this.checkBox_codeacreer_actif_inactif.Name = "checkBox_codeacreer_actif_inactif";
            this.checkBox_codeacreer_actif_inactif.Size = new System.Drawing.Size(55, 17);
            this.checkBox_codeacreer_actif_inactif.TabIndex = 13;
            this.checkBox_codeacreer_actif_inactif.Text = "Inactif";
            this.checkBox_codeacreer_actif_inactif.UseVisualStyleBackColor = true;
            // 
            // button_creercode
            // 
            this.button_creercode.Enabled = false;
            this.button_creercode.Location = new System.Drawing.Point(1174, 221);
            this.button_creercode.Name = "button_creercode";
            this.button_creercode.Size = new System.Drawing.Size(75, 23);
            this.button_creercode.TabIndex = 14;
            this.button_creercode.Text = "Créer Code";
            this.button_creercode.UseVisualStyleBackColor = true;
            this.button_creercode.Click += new System.EventHandler(this.button_creercode_Click);
            // 
            // button_deleteRecodage
            // 
            this.button_deleteRecodage.Enabled = false;
            this.button_deleteRecodage.Location = new System.Drawing.Point(1174, 57);
            this.button_deleteRecodage.Name = "button_deleteRecodage";
            this.button_deleteRecodage.Size = new System.Drawing.Size(131, 23);
            this.button_deleteRecodage.TabIndex = 15;
            this.button_deleteRecodage.Text = "Supprimer recodage";
            this.button_deleteRecodage.UseVisualStyleBackColor = true;
            this.button_deleteRecodage.Click += new System.EventHandler(this.button_deleteRecodage_Click);
            // 
            // button_rapprochementmodule
            // 
            this.button_rapprochementmodule.Location = new System.Drawing.Point(1155, 267);
            this.button_rapprochementmodule.Name = "button_rapprochementmodule";
            this.button_rapprochementmodule.Size = new System.Drawing.Size(149, 43);
            this.button_rapprochementmodule.TabIndex = 16;
            this.button_rapprochementmodule.Text = "Rapprochement automatique par module";
            this.button_rapprochementmodule.UseVisualStyleBackColor = true;
            this.button_rapprochementmodule.Click += new System.EventHandler(this.button_rapprochementmodule_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(1180, 326);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.TabIndex = 17;
            // 
            // button_parambase
            // 
            this.button_parambase.Location = new System.Drawing.Point(1171, 487);
            this.button_parambase.Name = "button_parambase";
            this.button_parambase.Size = new System.Drawing.Size(75, 23);
            this.button_parambase.TabIndex = 18;
            this.button_parambase.Text = "Param base";
            this.button_parambase.UseVisualStyleBackColor = true;
            this.button_parambase.Click += new System.EventHandler(this.button_parambase_Click);
            // 
            // button_chargement
            // 
            this.button_chargement.Location = new System.Drawing.Point(1171, 530);
            this.button_chargement.Name = "button_chargement";
            this.button_chargement.Size = new System.Drawing.Size(133, 46);
            this.button_chargement.TabIndex = 19;
            this.button_chargement.Text = "Chargement base dans le logiciel";
            this.button_chargement.UseVisualStyleBackColor = true;
            this.button_chargement.Click += new System.EventHandler(this.button_chargement_Click);
            // 
            // progressBar_chargementlogiciel
            // 
            this.progressBar_chargementlogiciel.Location = new System.Drawing.Point(1180, 582);
            this.progressBar_chargementlogiciel.Name = "progressBar_chargementlogiciel";
            this.progressBar_chargementlogiciel.Size = new System.Drawing.Size(100, 23);
            this.progressBar_chargementlogiciel.TabIndex = 20;
            // 
            // button_testsqlserver
            // 
            this.button_testsqlserver.Location = new System.Drawing.Point(1144, 747);
            this.button_testsqlserver.Name = "button_testsqlserver";
            this.button_testsqlserver.Size = new System.Drawing.Size(144, 23);
            this.button_testsqlserver.TabIndex = 21;
            this.button_testsqlserver.Text = "Test connexion SQL Server";
            this.button_testsqlserver.UseVisualStyleBackColor = true;
            this.button_testsqlserver.Click += new System.EventHandler(this.button_testsqlserver_Click);
            // 
            // button_exportCorresp
            // 
            this.button_exportCorresp.Location = new System.Drawing.Point(1174, 627);
            this.button_exportCorresp.Name = "button_exportCorresp";
            this.button_exportCorresp.Size = new System.Drawing.Size(130, 45);
            this.button_exportCorresp.TabIndex = 22;
            this.button_exportCorresp.Text = "Exportation vers table de travail reprise";
            this.button_exportCorresp.UseVisualStyleBackColor = true;
            this.button_exportCorresp.Click += new System.EventHandler(this.button_exportCorresp_Click);
            // 
            // button_rapprochement_global
            // 
            this.button_rapprochement_global.Location = new System.Drawing.Point(1155, 355);
            this.button_rapprochement_global.Name = "button_rapprochement_global";
            this.button_rapprochement_global.Size = new System.Drawing.Size(149, 43);
            this.button_rapprochement_global.TabIndex = 23;
            this.button_rapprochement_global.Text = "Rapprochement automatique global";
            this.button_rapprochement_global.UseVisualStyleBackColor = true;
            this.button_rapprochement_global.Click += new System.EventHandler(this.button_rapprochement_global_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1355, 836);
            this.Controls.Add(this.button_rapprochement_global);
            this.Controls.Add(this.button_exportCorresp);
            this.Controls.Add(this.button_testsqlserver);
            this.Controls.Add(this.progressBar_chargementlogiciel);
            this.Controls.Add(this.button_chargement);
            this.Controls.Add(this.button_parambase);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button_rapprochementmodule);
            this.Controls.Add(this.button_deleteRecodage);
            this.Controls.Add(this.button_creercode);
            this.Controls.Add(this.checkBox_codeacreer_actif_inactif);
            this.Controls.Add(this.textBox_libellecodeacreer);
            this.Controls.Add(this.textBox_codeacreer);
            this.Controls.Add(this.button_afficherCreationCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView_ref);
            this.Controls.Add(this.button_filter);
            this.Controls.Add(this.comboBox_filtre);
            this.Controls.Add(this.button_affecter);
            this.Controls.Add(this.dataGridView_saisie);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_saisie)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ref)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_saisie;
        private System.Windows.Forms.Button button_affecter;
        private System.Windows.Forms.ComboBox comboBox_filtre;
        private System.Windows.Forms.Button button_filter;
        private System.Windows.Forms.DataGridView dataGridView_ref;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_afficherCreationCode;
        private System.Windows.Forms.TextBox textBox_codeacreer;
        private System.Windows.Forms.TextBox textBox_libellecodeacreer;
        private System.Windows.Forms.CheckBox checkBox_codeacreer_actif_inactif;
        private System.Windows.Forms.Button button_creercode;
        private System.Windows.Forms.Button button_deleteRecodage;
        private System.Windows.Forms.Button button_rapprochementmodule;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button_parambase;
        private System.Windows.Forms.Button button_chargement;
        private System.Windows.Forms.ProgressBar progressBar_chargementlogiciel;
        private System.Windows.Forms.Button button_testsqlserver;
        private System.Windows.Forms.Button button_exportCorresp;
        private System.Windows.Forms.Button button_rapprochement_global;
    }
}

