namespace RecodageList.GUI
{
    partial class InterfaceAdmin
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
            this.groupBox_admin = new System.Windows.Forms.GroupBox();
            this.button_parambase = new System.Windows.Forms.Button();
            this.button_chargement = new System.Windows.Forms.Button();
            this.button_save_for_client = new System.Windows.Forms.Button();
            this.button_exportCorresp = new System.Windows.Forms.Button();
            this.groupBox_admin.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_admin
            // 
            this.groupBox_admin.Controls.Add(this.button_parambase);
            this.groupBox_admin.Controls.Add(this.button_chargement);
            this.groupBox_admin.Controls.Add(this.button_save_for_client);
            this.groupBox_admin.Controls.Add(this.button_exportCorresp);
            this.groupBox_admin.Location = new System.Drawing.Point(12, 12);
            this.groupBox_admin.Name = "groupBox_admin";
            this.groupBox_admin.Size = new System.Drawing.Size(329, 116);
            this.groupBox_admin.TabIndex = 25;
            this.groupBox_admin.TabStop = false;
            this.groupBox_admin.Text = "Interface Admin";
            // 
            // button_parambase
            // 
            this.button_parambase.Location = new System.Drawing.Point(10, 28);
            this.button_parambase.Name = "button_parambase";
            this.button_parambase.Size = new System.Drawing.Size(148, 23);
            this.button_parambase.TabIndex = 18;
            this.button_parambase.Text = "Param base";
            this.button_parambase.UseVisualStyleBackColor = true;
            this.button_parambase.Click += new System.EventHandler(this.button_parambase_Click);
            // 
            // button_chargement
            // 
            this.button_chargement.Location = new System.Drawing.Point(10, 57);
            this.button_chargement.Name = "button_chargement";
            this.button_chargement.Size = new System.Drawing.Size(148, 46);
            this.button_chargement.TabIndex = 19;
            this.button_chargement.Text = "Chargement base dans le logiciel";
            this.button_chargement.UseVisualStyleBackColor = true;
            // 
            // button_save_for_client
            // 
            this.button_save_for_client.Location = new System.Drawing.Point(165, 28);
            this.button_save_for_client.Name = "button_save_for_client";
            this.button_save_for_client.Size = new System.Drawing.Size(149, 23);
            this.button_save_for_client.TabIndex = 21;
            this.button_save_for_client.Text = "Enregistrer base pour client";
            this.button_save_for_client.UseVisualStyleBackColor = true;
            this.button_save_for_client.Click += new System.EventHandler(this.button_save_for_client_Click);
            // 
            // button_exportCorresp
            // 
            this.button_exportCorresp.Location = new System.Drawing.Point(165, 57);
            this.button_exportCorresp.Name = "button_exportCorresp";
            this.button_exportCorresp.Size = new System.Drawing.Size(149, 45);
            this.button_exportCorresp.TabIndex = 22;
            this.button_exportCorresp.Text = "Exportation vers table de travail reprise";
            this.button_exportCorresp.UseVisualStyleBackColor = true;
            this.button_exportCorresp.Click += new System.EventHandler(this.button_exportCorresp_Click);
            // 
            // InterfaceAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 138);
            this.Controls.Add(this.groupBox_admin);
            this.Name = "InterfaceAdmin";
            this.Text = "InterfaceAdmin";
            this.groupBox_admin.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox groupBox_admin;
        public System.Windows.Forms.Button button_parambase;
        public System.Windows.Forms.Button button_chargement;
        public System.Windows.Forms.Button button_save_for_client;
        public System.Windows.Forms.Button button_exportCorresp;
    }
}