namespace RecodageList.GUI
{
    partial class Chargement
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
            this.progressBar_chargement = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label_trt_encours = new System.Windows.Forms.Label();
            this.button_annuler_chargement = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBar_chargement
            // 
            this.progressBar_chargement.Location = new System.Drawing.Point(12, 138);
            this.progressBar_chargement.Name = "progressBar_chargement";
            this.progressBar_chargement.Size = new System.Drawing.Size(421, 38);
            this.progressBar_chargement.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Traitement en cours :";
            // 
            // label_trt_encours
            // 
            this.label_trt_encours.AutoSize = true;
            this.label_trt_encours.Location = new System.Drawing.Point(12, 34);
            this.label_trt_encours.Name = "label_trt_encours";
            this.label_trt_encours.Size = new System.Drawing.Size(16, 13);
            this.label_trt_encours.TabIndex = 2;
            this.label_trt_encours.Text = "...";
            // 
            // button_annuler_chargement
            // 
            this.button_annuler_chargement.Location = new System.Drawing.Point(439, 138);
            this.button_annuler_chargement.Name = "button_annuler_chargement";
            this.button_annuler_chargement.Size = new System.Drawing.Size(75, 38);
            this.button_annuler_chargement.TabIndex = 3;
            this.button_annuler_chargement.Text = "Annuler";
            this.button_annuler_chargement.UseVisualStyleBackColor = true;
            this.button_annuler_chargement.Click += new System.EventHandler(this.button_annuler_chargement_Click);
            // 
            // Chargement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 192);
            this.ControlBox = false;
            this.Controls.Add(this.button_annuler_chargement);
            this.Controls.Add(this.label_trt_encours);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar_chargement);
            this.Name = "Chargement";
            this.Text = "Chargement";
            this.Load += new System.EventHandler(this.Chargement_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ProgressBar progressBar_chargement;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label_trt_encours;
        public System.Windows.Forms.Button button_annuler_chargement;
    }
}