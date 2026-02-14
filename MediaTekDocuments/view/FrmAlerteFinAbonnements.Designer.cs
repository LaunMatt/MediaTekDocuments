namespace MediaTekDocuments.view
{
    partial class FrmAlerteFinAbonnements
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
            this.label6 = new System.Windows.Forms.Label();
            this.dgvAlerteFinAbonnementsListe = new System.Windows.Forms.DataGridView();
            this.btnAlerteFinAbonnementsFermer = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlerteFinAbonnementsListe)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(404, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Liste des revues dont un abonnement se finit dans moins de 30 jours :";
            // 
            // dgvAlerteFinAbonnementsListe
            // 
            this.dgvAlerteFinAbonnementsListe.AllowUserToAddRows = false;
            this.dgvAlerteFinAbonnementsListe.AllowUserToDeleteRows = false;
            this.dgvAlerteFinAbonnementsListe.AllowUserToResizeColumns = false;
            this.dgvAlerteFinAbonnementsListe.AllowUserToResizeRows = false;
            this.dgvAlerteFinAbonnementsListe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlerteFinAbonnementsListe.Location = new System.Drawing.Point(15, 34);
            this.dgvAlerteFinAbonnementsListe.MultiSelect = false;
            this.dgvAlerteFinAbonnementsListe.Name = "dgvAlerteFinAbonnementsListe";
            this.dgvAlerteFinAbonnementsListe.ReadOnly = true;
            this.dgvAlerteFinAbonnementsListe.RowHeadersVisible = false;
            this.dgvAlerteFinAbonnementsListe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAlerteFinAbonnementsListe.Size = new System.Drawing.Size(398, 132);
            this.dgvAlerteFinAbonnementsListe.TabIndex = 5;
            this.dgvAlerteFinAbonnementsListe.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvAlerteFinAbonnementsListe_ColumnHeaderMouseClick);
            // 
            // btnAlerteFinAbonnementsFermer
            // 
            this.btnAlerteFinAbonnementsFermer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlerteFinAbonnementsFermer.Location = new System.Drawing.Point(15, 182);
            this.btnAlerteFinAbonnementsFermer.Name = "btnAlerteFinAbonnementsFermer";
            this.btnAlerteFinAbonnementsFermer.Size = new System.Drawing.Size(398, 22);
            this.btnAlerteFinAbonnementsFermer.TabIndex = 46;
            this.btnAlerteFinAbonnementsFermer.Text = "Fermer cette fenêtre";
            this.btnAlerteFinAbonnementsFermer.UseVisualStyleBackColor = true;
            this.btnAlerteFinAbonnementsFermer.Click += new System.EventHandler(this.btnAlerteFinAbonnementsFermer_Click);
            // 
            // FrmAlerteFinAbonnements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 216);
            this.Controls.Add(this.btnAlerteFinAbonnementsFermer);
            this.Controls.Add(this.dgvAlerteFinAbonnementsListe);
            this.Controls.Add(this.label6);
            this.Name = "FrmAlerteFinAbonnements";
            this.Text = "Abonnements finissant dans moins de 30 jours";
            this.Load += new System.EventHandler(this.FrmAlerteFinAbonnements_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlerteFinAbonnementsListe)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvAlerteFinAbonnementsListe;
        private System.Windows.Forms.Button btnAlerteFinAbonnementsFermer;
    }
}