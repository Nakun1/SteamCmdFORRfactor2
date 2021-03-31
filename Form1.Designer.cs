namespace InstallPackageRF2
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnUpdateRF2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpenWorkshop = new System.Windows.Forms.Button();
            this.btnDlMod = new System.Windows.Forms.Button();
            this.txtIdMod = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnUpdateRF2
            // 
            this.btnUpdateRF2.Location = new System.Drawing.Point(68, 20);
            this.btnUpdateRF2.Name = "btnUpdateRF2";
            this.btnUpdateRF2.Size = new System.Drawing.Size(133, 23);
            this.btnUpdateRF2.TabIndex = 0;
            this.btnUpdateRF2.Text = "Mise à jour RF2";
            this.btnUpdateRF2.UseVisualStyleBackColor = true;
            this.btnUpdateRF2.Click += new System.EventHandler(this.btnUpdateRF2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(213, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Met à jour le serveur si nouvelle version d\'RF2";
            // 
            // btnOpenWorkshop
            // 
            this.btnOpenWorkshop.Location = new System.Drawing.Point(68, 50);
            this.btnOpenWorkshop.Name = "btnOpenWorkshop";
            this.btnOpenWorkshop.Size = new System.Drawing.Size(133, 23);
            this.btnOpenWorkshop.TabIndex = 2;
            this.btnOpenWorkshop.Text = "Ouvrir Steam Workshop";
            this.btnOpenWorkshop.UseVisualStyleBackColor = true;
            this.btnOpenWorkshop.Click += new System.EventHandler(this.btnOpenWorkshop_Click);
            // 
            // btnDlMod
            // 
            this.btnDlMod.Location = new System.Drawing.Point(252, 181);
            this.btnDlMod.Name = "btnDlMod";
            this.btnDlMod.Size = new System.Drawing.Size(133, 23);
            this.btnDlMod.TabIndex = 3;
            this.btnDlMod.Text = "Télécharger le Mod";
            this.btnDlMod.UseVisualStyleBackColor = true;
            this.btnDlMod.Click += new System.EventHandler(this.btnDlMod_Click);
            // 
            // txtIdMod
            // 
            this.txtIdMod.Location = new System.Drawing.Point(216, 155);
            this.txtIdMod.Name = "txtIdMod";
            this.txtIdMod.Size = new System.Drawing.Size(169, 20);
            this.txtIdMod.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "ID du mod à télécharger :";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 224);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtIdMod);
            this.Controls.Add(this.btnDlMod);
            this.Controls.Add(this.btnOpenWorkshop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUpdateRF2);
            this.Name = "Form1";
            this.Text = "Prouuuuuuuuuuuuuut";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUpdateRF2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpenWorkshop;
        private System.Windows.Forms.Button btnDlMod;
        private System.Windows.Forms.TextBox txtIdMod;
        private System.Windows.Forms.Label label2;
    }
}

