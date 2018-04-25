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
            this.dataGridView_saisie = new System.Windows.Forms.DataGridView();
            this.button_save = new System.Windows.Forms.Button();
            this.button_addlambda = new System.Windows.Forms.Button();
            this.button_clearTable = new System.Windows.Forms.Button();
            this.comboBox_filtre = new System.Windows.Forms.ComboBox();
            this.button_filter = new System.Windows.Forms.Button();
            this.dataGridView_ref = new System.Windows.Forms.DataGridView();
            this.button_deletefilter = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_saisie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ref)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_saisie
            // 
            this.dataGridView_saisie.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_saisie.Location = new System.Drawing.Point(62, 42);
            this.dataGridView_saisie.Name = "dataGridView_saisie";
            this.dataGridView_saisie.Size = new System.Drawing.Size(669, 278);
            this.dataGridView_saisie.TabIndex = 0;
            this.dataGridView_saisie.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(727, 371);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(75, 23);
            this.button_save.TabIndex = 1;
            this.button_save.Text = "Enregistrer";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // button_addlambda
            // 
            this.button_addlambda.Location = new System.Drawing.Point(391, 394);
            this.button_addlambda.Name = "button_addlambda";
            this.button_addlambda.Size = new System.Drawing.Size(75, 23);
            this.button_addlambda.TabIndex = 2;
            this.button_addlambda.Text = "Ajouter Corresp lambda";
            this.button_addlambda.UseVisualStyleBackColor = true;
            this.button_addlambda.Click += new System.EventHandler(this.button_addlambda_Click);
            // 
            // button_clearTable
            // 
            this.button_clearTable.Location = new System.Drawing.Point(391, 440);
            this.button_clearTable.Name = "button_clearTable";
            this.button_clearTable.Size = new System.Drawing.Size(149, 23);
            this.button_clearTable.TabIndex = 3;
            this.button_clearTable.Text = "Supprimer toutes les lignes";
            this.button_clearTable.UseVisualStyleBackColor = true;
            this.button_clearTable.Click += new System.EventHandler(this.button_clearTable_Click);
            // 
            // comboBox_filtre
            // 
            this.comboBox_filtre.FormattingEnabled = true;
            this.comboBox_filtre.Items.AddRange(new object[] {
            "124",
            "142",
            "123"});
            this.comboBox_filtre.Location = new System.Drawing.Point(96, 385);
            this.comboBox_filtre.Name = "comboBox_filtre";
            this.comboBox_filtre.Size = new System.Drawing.Size(121, 21);
            this.comboBox_filtre.TabIndex = 4;
            // 
            // button_filter
            // 
            this.button_filter.Location = new System.Drawing.Point(223, 385);
            this.button_filter.Name = "button_filter";
            this.button_filter.Size = new System.Drawing.Size(75, 23);
            this.button_filter.TabIndex = 5;
            this.button_filter.Text = "Filtrer";
            this.button_filter.UseVisualStyleBackColor = true;
            this.button_filter.Click += new System.EventHandler(this.button_filter_Click);
            // 
            // dataGridView_ref
            // 
            this.dataGridView_ref.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_ref.Location = new System.Drawing.Point(865, 45);
            this.dataGridView_ref.Name = "dataGridView_ref";
            this.dataGridView_ref.Size = new System.Drawing.Size(464, 554);
            this.dataGridView_ref.TabIndex = 6;
            // 
            // button_deletefilter
            // 
            this.button_deletefilter.Location = new System.Drawing.Point(223, 414);
            this.button_deletefilter.Name = "button_deletefilter";
            this.button_deletefilter.Size = new System.Drawing.Size(109, 23);
            this.button_deletefilter.TabIndex = 7;
            this.button_deletefilter.Text = "Supprimer filtre";
            this.button_deletefilter.UseVisualStyleBackColor = true;
            this.button_deletefilter.Click += new System.EventHandler(this.button_deletefilter_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1349, 629);
            this.Controls.Add(this.button_deletefilter);
            this.Controls.Add(this.dataGridView_ref);
            this.Controls.Add(this.button_filter);
            this.Controls.Add(this.comboBox_filtre);
            this.Controls.Add(this.button_clearTable);
            this.Controls.Add(this.button_addlambda);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.dataGridView_saisie);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_saisie)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ref)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_saisie;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_addlambda;
        private System.Windows.Forms.Button button_clearTable;
        private System.Windows.Forms.ComboBox comboBox_filtre;
        private System.Windows.Forms.Button button_filter;
        private System.Windows.Forms.DataGridView dataGridView_ref;
        private System.Windows.Forms.Button button_deletefilter;
    }
}

