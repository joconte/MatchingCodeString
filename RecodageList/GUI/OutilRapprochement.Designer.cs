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
        public void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dataGridView_saisie = new System.Windows.Forms.DataGridView();
            this.comboBox_filtre = new System.Windows.Forms.ComboBox();
            this.dataGridView_ref = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pictureBox_bottom = new System.Windows.Forms.PictureBox();
            this.textBox_search = new System.Windows.Forms.TextBox();
            this.button_del_filter_search = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label_avancement_module_pourcentage = new System.Windows.Forms.Label();
            this.label_avancement_recodage_global = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox_nontraite = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ouvrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsParModuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rapprochementAutomatiqueParModuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creerCodeActifParModuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.creerCodeInactifParModuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nePasReprendreModuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.suppressionRecodageParModuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsGlobalesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rapprochementAutomatiqueGlobalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.créerCodeActifGlobalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.créerCodeInactifGlobalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nePasReprendreGlobalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.supprimerRecodageGlobalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paramétrageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accèsInterfaceAdminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.button_delete_filter_corr = new System.Windows.Forms.Button();
            this.textBox_search_corr = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label_client_en_cour = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_search_referentiel_code = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_search_saisie_code = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_saisie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ref)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_bottom)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView_saisie
            // 
            this.dataGridView_saisie.AllowUserToAddRows = false;
            this.dataGridView_saisie.AllowUserToDeleteRows = false;
            this.dataGridView_saisie.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dataGridView_saisie.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView_saisie.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_saisie.Location = new System.Drawing.Point(12, 72);
            this.dataGridView_saisie.Name = "dataGridView_saisie";
            this.dataGridView_saisie.ReadOnly = true;
            this.dataGridView_saisie.RowHeadersVisible = false;
            this.dataGridView_saisie.Size = new System.Drawing.Size(1325, 300);
            this.dataGridView_saisie.TabIndex = 0;
            this.dataGridView_saisie.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_saisie_CellClick_1);
            this.dataGridView_saisie.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_saisie_CellContentClick);
            this.dataGridView_saisie.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_saisie_CellClick);
            this.dataGridView_saisie.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_saisie_CellMouseClick);
            this.dataGridView_saisie.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridView_saisie_Scroll);
            this.dataGridView_saisie.Click += new System.EventHandler(this.dataGridView_saisie_Click);
            this.dataGridView_saisie.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView_saisie_KeyDown);
            this.dataGridView_saisie.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView_saisie_MouseClick);
            // 
            // comboBox_filtre
            // 
            this.comboBox_filtre.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBox_filtre.Enabled = false;
            this.comboBox_filtre.FormattingEnabled = true;
            this.comboBox_filtre.Items.AddRange(new object[] {
            "123",
            "124",
            "142"});
            this.comboBox_filtre.Location = new System.Drawing.Point(12, 31);
            this.comboBox_filtre.Name = "comboBox_filtre";
            this.comboBox_filtre.Size = new System.Drawing.Size(121, 21);
            this.comboBox_filtre.TabIndex = 4;
            this.comboBox_filtre.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox_filtre_DrawItem);
            this.comboBox_filtre.SelectedIndexChanged += new System.EventHandler(this.comboBox_filtre_SelectedIndexChanged);
            this.comboBox_filtre.SelectionChangeCommitted += new System.EventHandler(this.comboBox_filtre_SelectionChangeCommitted);
            // 
            // dataGridView_ref
            // 
            this.dataGridView_ref.AllowUserToAddRows = false;
            this.dataGridView_ref.AllowUserToDeleteRows = false;
            this.dataGridView_ref.AllowUserToResizeRows = false;
            this.dataGridView_ref.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_ref.Location = new System.Drawing.Point(12, 415);
            this.dataGridView_ref.Name = "dataGridView_ref";
            this.dataGridView_ref.ReadOnly = true;
            this.dataGridView_ref.RowHeadersVisible = false;
            this.dataGridView_ref.Size = new System.Drawing.Size(1325, 300);
            this.dataGridView_ref.TabIndex = 6;
            this.dataGridView_ref.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_ref_CellContentClick);
            this.dataGridView_ref.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_ref_CellDoubleClick);
            this.dataGridView_ref.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView_ref_CellFormatting);
            this.dataGridView_ref.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridView_ref_Scroll);
            this.dataGridView_ref.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView_ref_KeyDown);
            this.dataGridView_ref.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView_ref_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Saisie";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 399);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Référentiel";
            // 
            // pictureBox_bottom
            // 
            this.pictureBox_bottom.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox_bottom.Image")));
            this.pictureBox_bottom.Location = new System.Drawing.Point(1037, 742);
            this.pictureBox_bottom.Name = "pictureBox_bottom";
            this.pictureBox_bottom.Size = new System.Drawing.Size(274, 68);
            this.pictureBox_bottom.TabIndex = 30;
            this.pictureBox_bottom.TabStop = false;
            // 
            // textBox_search
            // 
            this.textBox_search.Enabled = false;
            this.textBox_search.Location = new System.Drawing.Point(1068, 392);
            this.textBox_search.Name = "textBox_search";
            this.textBox_search.Size = new System.Drawing.Size(169, 20);
            this.textBox_search.TabIndex = 31;
            this.textBox_search.TextChanged += new System.EventHandler(this.textBox_search_TextChanged);
            // 
            // button_del_filter_search
            // 
            this.button_del_filter_search.Enabled = false;
            this.button_del_filter_search.Location = new System.Drawing.Point(1243, 390);
            this.button_del_filter_search.Name = "button_del_filter_search";
            this.button_del_filter_search.Size = new System.Drawing.Size(93, 23);
            this.button_del_filter_search.TabIndex = 33;
            this.button_del_filter_search.Text = "Effacer le filtre";
            this.button_del_filter_search.UseVisualStyleBackColor = true;
            this.button_del_filter_search.Click += new System.EventHandler(this.button_del_filter_search_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(973, 395);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 34;
            this.label3.Text = "Recherche libellé";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(320, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 38;
            this.label4.Text = "Recodage module :";
            // 
            // label_avancement_module_pourcentage
            // 
            this.label_avancement_module_pourcentage.AutoSize = true;
            this.label_avancement_module_pourcentage.Location = new System.Drawing.Point(426, 46);
            this.label_avancement_module_pourcentage.Name = "label_avancement_module_pourcentage";
            this.label_avancement_module_pourcentage.Size = new System.Drawing.Size(16, 13);
            this.label_avancement_module_pourcentage.TabIndex = 39;
            this.label_avancement_module_pourcentage.Text = "...";
            // 
            // label_avancement_recodage_global
            // 
            this.label_avancement_recodage_global.AutoSize = true;
            this.label_avancement_recodage_global.Location = new System.Drawing.Point(256, 46);
            this.label_avancement_recodage_global.Name = "label_avancement_recodage_global";
            this.label_avancement_recodage_global.Size = new System.Drawing.Size(16, 13);
            this.label_avancement_recodage_global.TabIndex = 41;
            this.label_avancement_recodage_global.Text = "...";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(156, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 40;
            this.label6.Text = "Recodage global :";
            // 
            // checkBox_nontraite
            // 
            this.checkBox_nontraite.AutoSize = true;
            this.checkBox_nontraite.Enabled = false;
            this.checkBox_nontraite.Location = new System.Drawing.Point(482, 46);
            this.checkBox_nontraite.Name = "checkBox_nontraite";
            this.checkBox_nontraite.Size = new System.Drawing.Size(181, 17);
            this.checkBox_nontraite.TabIndex = 42;
            this.checkBox_nontraite.Text = "Afficher seulement les non traités";
            this.checkBox_nontraite.UseVisualStyleBackColor = true;
            this.checkBox_nontraite.CheckedChanged += new System.EventHandler(this.checkBox_nontraite_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fichierToolStripMenuItem,
            this.actionsParModuleToolStripMenuItem,
            this.actionsGlobalesToolStripMenuItem,
            this.paramétrageToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1349, 24);
            this.menuStrip1.TabIndex = 45;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fichierToolStripMenuItem
            // 
            this.fichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ouvrirToolStripMenuItem,
            this.quitterToolStripMenuItem});
            this.fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
            this.fichierToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.fichierToolStripMenuItem.Text = "Fichier";
            // 
            // ouvrirToolStripMenuItem
            // 
            this.ouvrirToolStripMenuItem.Name = "ouvrirToolStripMenuItem";
            this.ouvrirToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.ouvrirToolStripMenuItem.Text = "Ouvrir";
            this.ouvrirToolStripMenuItem.Click += new System.EventHandler(this.ouvrirToolStripMenuItem_Click);
            // 
            // quitterToolStripMenuItem
            // 
            this.quitterToolStripMenuItem.Name = "quitterToolStripMenuItem";
            this.quitterToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.quitterToolStripMenuItem.Text = "Quitter";
            this.quitterToolStripMenuItem.Click += new System.EventHandler(this.quitterToolStripMenuItem_Click);
            // 
            // actionsParModuleToolStripMenuItem
            // 
            this.actionsParModuleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rapprochementAutomatiqueParModuleToolStripMenuItem,
            this.creerCodeActifParModuleToolStripMenuItem,
            this.creerCodeInactifParModuleToolStripMenuItem,
            this.nePasReprendreModuleToolStripMenuItem,
            this.suppressionRecodageParModuleToolStripMenuItem});
            this.actionsParModuleToolStripMenuItem.Name = "actionsParModuleToolStripMenuItem";
            this.actionsParModuleToolStripMenuItem.Size = new System.Drawing.Size(123, 20);
            this.actionsParModuleToolStripMenuItem.Text = "Actions par module";
            // 
            // rapprochementAutomatiqueParModuleToolStripMenuItem
            // 
            this.rapprochementAutomatiqueParModuleToolStripMenuItem.Name = "rapprochementAutomatiqueParModuleToolStripMenuItem";
            this.rapprochementAutomatiqueParModuleToolStripMenuItem.Size = new System.Drawing.Size(294, 22);
            this.rapprochementAutomatiqueParModuleToolStripMenuItem.Text = "Rapprochement automatique par module";
            this.rapprochementAutomatiqueParModuleToolStripMenuItem.Click += new System.EventHandler(this.rapprochementAutomatiqueParModuleToolStripMenuItem_Click);
            // 
            // creerCodeActifParModuleToolStripMenuItem
            // 
            this.creerCodeActifParModuleToolStripMenuItem.Name = "creerCodeActifParModuleToolStripMenuItem";
            this.creerCodeActifParModuleToolStripMenuItem.Size = new System.Drawing.Size(294, 22);
            this.creerCodeActifParModuleToolStripMenuItem.Text = "Creer code actif par module";
            this.creerCodeActifParModuleToolStripMenuItem.Click += new System.EventHandler(this.creerCodeActifParModuleToolStripMenuItem_Click);
            // 
            // creerCodeInactifParModuleToolStripMenuItem
            // 
            this.creerCodeInactifParModuleToolStripMenuItem.Name = "creerCodeInactifParModuleToolStripMenuItem";
            this.creerCodeInactifParModuleToolStripMenuItem.Size = new System.Drawing.Size(294, 22);
            this.creerCodeInactifParModuleToolStripMenuItem.Text = "Creer code inactif par module";
            this.creerCodeInactifParModuleToolStripMenuItem.Click += new System.EventHandler(this.creerCodeInactifParModuleToolStripMenuItem_Click);
            // 
            // nePasReprendreModuleToolStripMenuItem
            // 
            this.nePasReprendreModuleToolStripMenuItem.Name = "nePasReprendreModuleToolStripMenuItem";
            this.nePasReprendreModuleToolStripMenuItem.Size = new System.Drawing.Size(294, 22);
            this.nePasReprendreModuleToolStripMenuItem.Text = "Ne pas reprendre par module";
            this.nePasReprendreModuleToolStripMenuItem.Click += new System.EventHandler(this.nePasReprendreModuleToolStripMenuItem_Click);
            // 
            // suppressionRecodageParModuleToolStripMenuItem
            // 
            this.suppressionRecodageParModuleToolStripMenuItem.Name = "suppressionRecodageParModuleToolStripMenuItem";
            this.suppressionRecodageParModuleToolStripMenuItem.Size = new System.Drawing.Size(294, 22);
            this.suppressionRecodageParModuleToolStripMenuItem.Text = "Suppression recodage par module";
            this.suppressionRecodageParModuleToolStripMenuItem.Click += new System.EventHandler(this.suppressionRecodageParModuleToolStripMenuItem_Click);
            // 
            // actionsGlobalesToolStripMenuItem
            // 
            this.actionsGlobalesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rapprochementAutomatiqueGlobalToolStripMenuItem,
            this.créerCodeActifGlobalToolStripMenuItem,
            this.créerCodeInactifGlobalToolStripMenuItem,
            this.nePasReprendreGlobalToolStripMenuItem,
            this.supprimerRecodageGlobalToolStripMenuItem});
            this.actionsGlobalesToolStripMenuItem.Name = "actionsGlobalesToolStripMenuItem";
            this.actionsGlobalesToolStripMenuItem.Size = new System.Drawing.Size(106, 20);
            this.actionsGlobalesToolStripMenuItem.Text = "Actions globales";
            // 
            // rapprochementAutomatiqueGlobalToolStripMenuItem
            // 
            this.rapprochementAutomatiqueGlobalToolStripMenuItem.Name = "rapprochementAutomatiqueGlobalToolStripMenuItem";
            this.rapprochementAutomatiqueGlobalToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
            this.rapprochementAutomatiqueGlobalToolStripMenuItem.Text = "Rapprochement automatique global";
            this.rapprochementAutomatiqueGlobalToolStripMenuItem.Click += new System.EventHandler(this.rapprochementAutomatiqueGlobalToolStripMenuItem_Click);
            // 
            // créerCodeActifGlobalToolStripMenuItem
            // 
            this.créerCodeActifGlobalToolStripMenuItem.Name = "créerCodeActifGlobalToolStripMenuItem";
            this.créerCodeActifGlobalToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
            this.créerCodeActifGlobalToolStripMenuItem.Text = "Créer code actif global";
            this.créerCodeActifGlobalToolStripMenuItem.Click += new System.EventHandler(this.créerCodeActifGlobalToolStripMenuItem_Click);
            // 
            // créerCodeInactifGlobalToolStripMenuItem
            // 
            this.créerCodeInactifGlobalToolStripMenuItem.Name = "créerCodeInactifGlobalToolStripMenuItem";
            this.créerCodeInactifGlobalToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
            this.créerCodeInactifGlobalToolStripMenuItem.Text = "Créer code inactif global";
            this.créerCodeInactifGlobalToolStripMenuItem.Click += new System.EventHandler(this.créerCodeInactifGlobalToolStripMenuItem_Click);
            // 
            // nePasReprendreGlobalToolStripMenuItem
            // 
            this.nePasReprendreGlobalToolStripMenuItem.Name = "nePasReprendreGlobalToolStripMenuItem";
            this.nePasReprendreGlobalToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
            this.nePasReprendreGlobalToolStripMenuItem.Text = "Ne pas reprendre global";
            this.nePasReprendreGlobalToolStripMenuItem.Click += new System.EventHandler(this.nePasReprendreGlobalToolStripMenuItem_Click);
            // 
            // supprimerRecodageGlobalToolStripMenuItem
            // 
            this.supprimerRecodageGlobalToolStripMenuItem.Name = "supprimerRecodageGlobalToolStripMenuItem";
            this.supprimerRecodageGlobalToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
            this.supprimerRecodageGlobalToolStripMenuItem.Text = "Supprimer recodage global";
            this.supprimerRecodageGlobalToolStripMenuItem.Click += new System.EventHandler(this.supprimerRecodageGlobalToolStripMenuItem_Click);
            // 
            // paramétrageToolStripMenuItem
            // 
            this.paramétrageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accèsInterfaceAdminToolStripMenuItem});
            this.paramétrageToolStripMenuItem.Name = "paramétrageToolStripMenuItem";
            this.paramétrageToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.paramétrageToolStripMenuItem.Text = "Paramétrage";
            // 
            // accèsInterfaceAdminToolStripMenuItem
            // 
            this.accèsInterfaceAdminToolStripMenuItem.Name = "accèsInterfaceAdminToolStripMenuItem";
            this.accèsInterfaceAdminToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.accèsInterfaceAdminToolStripMenuItem.Text = "Accès interface admin";
            this.accèsInterfaceAdminToolStripMenuItem.Click += new System.EventHandler(this.accèsInterfaceAdminToolStripMenuItem_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(973, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 48;
            this.label5.Text = "Recherche libellé";
            // 
            // button_delete_filter_corr
            // 
            this.button_delete_filter_corr.Enabled = false;
            this.button_delete_filter_corr.Location = new System.Drawing.Point(1243, 46);
            this.button_delete_filter_corr.Name = "button_delete_filter_corr";
            this.button_delete_filter_corr.Size = new System.Drawing.Size(93, 23);
            this.button_delete_filter_corr.TabIndex = 47;
            this.button_delete_filter_corr.Text = "Effacer le filtre";
            this.button_delete_filter_corr.UseVisualStyleBackColor = true;
            this.button_delete_filter_corr.Click += new System.EventHandler(this.button_delete_filter_corr_Click);
            // 
            // textBox_search_corr
            // 
            this.textBox_search_corr.Enabled = false;
            this.textBox_search_corr.Location = new System.Drawing.Point(1068, 48);
            this.textBox_search_corr.Name = "textBox_search_corr";
            this.textBox_search_corr.Size = new System.Drawing.Size(169, 20);
            this.textBox_search_corr.TabIndex = 46;
            this.textBox_search_corr.TextChanged += new System.EventHandler(this.textBox_search_corr_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 760);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 13);
            this.label7.TabIndex = 49;
            this.label7.Text = "Recodage en cours : ";
            // 
            // label_client_en_cour
            // 
            this.label_client_en_cour.AutoSize = true;
            this.label_client_en_cour.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_client_en_cour.Location = new System.Drawing.Point(139, 759);
            this.label_client_en_cour.Name = "label_client_en_cour";
            this.label_client_en_cour.Size = new System.Drawing.Size(24, 20);
            this.label_client_en_cour.TabIndex = 50;
            this.label_client_en_cour.Text = "...";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(692, 394);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 13);
            this.label8.TabIndex = 52;
            this.label8.Text = "Recherche code";
            // 
            // textBox_search_referentiel_code
            // 
            this.textBox_search_referentiel_code.Enabled = false;
            this.textBox_search_referentiel_code.Location = new System.Drawing.Point(785, 392);
            this.textBox_search_referentiel_code.Name = "textBox_search_referentiel_code";
            this.textBox_search_referentiel_code.Size = new System.Drawing.Size(169, 20);
            this.textBox_search_referentiel_code.TabIndex = 51;
            this.textBox_search_referentiel_code.TextChanged += new System.EventHandler(this.textBox_search_referentiel_code_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(692, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 13);
            this.label9.TabIndex = 54;
            this.label9.Text = "Recherche code";
            // 
            // textBox_search_saisie_code
            // 
            this.textBox_search_saisie_code.Enabled = false;
            this.textBox_search_saisie_code.Location = new System.Drawing.Point(785, 48);
            this.textBox_search_saisie_code.Name = "textBox_search_saisie_code";
            this.textBox_search_saisie_code.Size = new System.Drawing.Size(169, 20);
            this.textBox_search_saisie_code.TabIndex = 53;
            this.textBox_search_saisie_code.TextChanged += new System.EventHandler(this.textBox_search_saisie_code_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1349, 822);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBox_search_saisie_code);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox_search_referentiel_code);
            this.Controls.Add(this.label_client_en_cour);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button_delete_filter_corr);
            this.Controls.Add(this.textBox_search_corr);
            this.Controls.Add(this.checkBox_nontraite);
            this.Controls.Add(this.label_avancement_recodage_global);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label_avancement_module_pourcentage);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button_del_filter_search);
            this.Controls.Add(this.textBox_search);
            this.Controls.Add(this.pictureBox_bottom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView_ref);
            this.Controls.Add(this.comboBox_filtre);
            this.Controls.Add(this.dataGridView_saisie);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Outil Rapprochement";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_saisie)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ref)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_bottom)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView dataGridView_saisie;
        public System.Windows.Forms.ComboBox comboBox_filtre;
        public System.Windows.Forms.DataGridView dataGridView_ref;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.ComponentModel.BackgroundWorker backgroundWorker1;
        public System.Windows.Forms.PictureBox pictureBox_bottom;
        public System.Windows.Forms.TextBox textBox_search;
        public System.Windows.Forms.Button button_del_filter_search;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label_avancement_module_pourcentage;
        public System.Windows.Forms.Label label_avancement_recodage_global;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.CheckBox checkBox_nontraite;
        public System.Windows.Forms.MenuStrip menuStrip1;
        public System.Windows.Forms.ToolStripMenuItem fichierToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem ouvrirToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem quitterToolStripMenuItem;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Button button_delete_filter_corr;
        public System.Windows.Forms.TextBox textBox_search_corr;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label label_client_en_cour;
        private System.Windows.Forms.ToolStripMenuItem actionsParModuleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionsGlobalesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paramétrageToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem rapprochementAutomatiqueParModuleToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem creerCodeInactifParModuleToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem nePasReprendreModuleToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem suppressionRecodageParModuleToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem rapprochementAutomatiqueGlobalToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem créerCodeInactifGlobalToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem nePasReprendreGlobalToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem supprimerRecodageGlobalToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem accèsInterfaceAdminToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem creerCodeActifParModuleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem créerCodeActifGlobalToolStripMenuItem;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox textBox_search_referentiel_code;
        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.TextBox textBox_search_saisie_code;
    }
}

