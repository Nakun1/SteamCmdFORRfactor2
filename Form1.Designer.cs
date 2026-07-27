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
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
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
            this.groupBoxGame = new System.Windows.Forms.GroupBox();
            this.rbAMS2 = new System.Windows.Forms.RadioButton();
            this.rbRFactor2 = new System.Windows.Forms.RadioButton();
            this.btnUpdateServer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpenWorkshop = new System.Windows.Forms.Button();
            this.btnDlMod = new System.Windows.Forms.Button();
            this.txtIdMod = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxGame.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBoxGame
            //
            this.groupBoxGame.Controls.Add(this.rbAMS2);
            this.groupBoxGame.Controls.Add(this.rbRFactor2);
            this.groupBoxGame.Location = new System.Drawing.Point(12, 9);
            this.groupBoxGame.Name = "groupBoxGame";
            this.groupBoxGame.Size = new System.Drawing.Size(421, 45);
            this.groupBoxGame.TabIndex = 0;
            this.groupBoxGame.TabStop = false;
            this.groupBoxGame.Text = "Serveur";
            //
            // rbAMS2
            //
            this.rbAMS2.AutoSize = true;
            this.rbAMS2.Location = new System.Drawing.Point(150, 18);
            this.rbAMS2.Name = "rbAMS2";
            this.rbAMS2.Size = new System.Drawing.Size(115, 17);
            this.rbAMS2.TabIndex = 1;
            this.rbAMS2.Text = "Automobilista 2";
            this.rbAMS2.UseVisualStyleBackColor = true;
            this.rbAMS2.CheckedChanged += new System.EventHandler(this.rbGame_CheckedChanged);
            //
            // rbRFactor2
            //
            this.rbRFactor2.AutoSize = true;
            this.rbRFactor2.Checked = true;
            this.rbRFactor2.Location = new System.Drawing.Point(15, 18);
            this.rbRFactor2.Name = "rbRFactor2";
            this.rbRFactor2.Size = new System.Drawing.Size(70, 17);
            this.rbRFactor2.TabIndex = 0;
            this.rbRFactor2.TabStop = true;
            this.rbRFactor2.Text = "rFactor 2";
            this.rbRFactor2.UseVisualStyleBackColor = true;
            this.rbRFactor2.CheckedChanged += new System.EventHandler(this.rbGame_CheckedChanged);
            //
            // btnUpdateServer
            //
            this.btnUpdateServer.Location = new System.Drawing.Point(68, 70);
            this.btnUpdateServer.Name = "btnUpdateServer";
            this.btnUpdateServer.Size = new System.Drawing.Size(133, 23);
            this.btnUpdateServer.TabIndex = 2;
            this.btnUpdateServer.Text = "Mise à jour";
            this.btnUpdateServer.UseVisualStyleBackColor = true;
            this.btnUpdateServer.Click += new System.EventHandler(this.btnUpdateServer_Click);
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(213, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Met à jour le serveur si nouvelle version";
            //
            // btnOpenWorkshop
            //
            this.btnOpenWorkshop.Location = new System.Drawing.Point(68, 100);
            this.btnOpenWorkshop.Name = "btnOpenWorkshop";
            this.btnOpenWorkshop.Size = new System.Drawing.Size(133, 23);
            this.btnOpenWorkshop.TabIndex = 4;
            this.btnOpenWorkshop.Text = "Ouvrir Steam Workshop";
            this.btnOpenWorkshop.UseVisualStyleBackColor = true;
            this.btnOpenWorkshop.Click += new System.EventHandler(this.btnOpenWorkshop_Click);
            //
            // btnDlMod
            //
            this.btnDlMod.Location = new System.Drawing.Point(252, 231);
            this.btnDlMod.Name = "btnDlMod";
            this.btnDlMod.Size = new System.Drawing.Size(133, 23);
            this.btnDlMod.TabIndex = 5;
            this.btnDlMod.Text = "Télécharger le Mod";
            this.btnDlMod.UseVisualStyleBackColor = true;
            this.btnDlMod.Click += new System.EventHandler(this.btnDlMod_Click);
            //
            // txtIdMod
            //
            this.txtIdMod.Location = new System.Drawing.Point(216, 205);
            this.txtIdMod.Name = "txtIdMod";
            this.txtIdMod.Size = new System.Drawing.Size(169, 20);
            this.txtIdMod.TabIndex = 6;
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 208);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "ID du mod à télécharger :";
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 274);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtIdMod);
            this.Controls.Add(this.btnDlMod);
            this.Controls.Add(this.btnOpenWorkshop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUpdateServer);
            this.Controls.Add(this.groupBoxGame);
            this.Name = "Form1";
            this.Text = "Prouuuuuuuuuuuuuut";
            this.groupBoxGame.ResumeLayout(false);
            this.groupBoxGame.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxGame;
        private System.Windows.Forms.RadioButton rbAMS2;
        private System.Windows.Forms.RadioButton rbRFactor2;
        private System.Windows.Forms.Button btnUpdateServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpenWorkshop;
        private System.Windows.Forms.Button btnDlMod;
        private System.Windows.Forms.TextBox txtIdMod;
        private System.Windows.Forms.Label label2;
    }
}
