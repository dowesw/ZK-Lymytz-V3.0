namespace ZK_Lymytz.IHM
{
    partial class Form_Archive_Serveur
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Archive_Serveur));
            this.grp_log = new System.Windows.Forms.GroupBox();
            this.dgv_log = new System.Windows.Forms.DataGridView();
            this.pos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.heure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grp_backup = new System.Windows.Forms.GroupBox();
            this.dgv_backup = new System.Windows.Forms.DataGridView();
            this.fileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_current = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pbar_statut = new System.Windows.Forms.ProgressBar();
            this.grp_log.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_log)).BeginInit();
            this.grp_backup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_backup)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // grp_log
            // 
            this.grp_log.Controls.Add(this.dgv_log);
            this.grp_log.Location = new System.Drawing.Point(186, 2);
            this.grp_log.Name = "grp_log";
            this.grp_log.Size = new System.Drawing.Size(698, 421);
            this.grp_log.TabIndex = 3;
            this.grp_log.TabStop = false;
            this.grp_log.Text = "Liste Logs";
            // 
            // dgv_log
            // 
            this.dgv_log.AllowUserToAddRows = false;
            this.dgv_log.AllowUserToDeleteRows = false;
            this.dgv_log.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_log.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgv_log.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_log.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pos,
            this.num,
            this.name,
            this.date,
            this.heure});
            this.dgv_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_log.Location = new System.Drawing.Point(3, 16);
            this.dgv_log.Name = "dgv_log";
            this.dgv_log.ReadOnly = true;
            this.dgv_log.RowHeadersVisible = false;
            this.dgv_log.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_log.Size = new System.Drawing.Size(692, 402);
            this.dgv_log.TabIndex = 0;
            // 
            // pos
            // 
            this.pos.FillWeight = 50.76142F;
            this.pos.HeaderText = "";
            this.pos.Name = "pos";
            this.pos.ReadOnly = true;
            // 
            // num
            // 
            this.num.FillWeight = 48.00278F;
            this.num.HeaderText = "N°";
            this.num.Name = "num";
            this.num.ReadOnly = true;
            // 
            // name
            // 
            this.name.FillWeight = 214.5677F;
            this.name.HeaderText = "Noms & Prénoms";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // date
            // 
            this.date.FillWeight = 93.58389F;
            this.date.HeaderText = "Date";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            // 
            // heure
            // 
            this.heure.FillWeight = 93.08425F;
            this.heure.HeaderText = "Heure";
            this.heure.Name = "heure";
            this.heure.ReadOnly = true;
            // 
            // grp_backup
            // 
            this.grp_backup.Controls.Add(this.dgv_backup);
            this.grp_backup.Location = new System.Drawing.Point(4, 27);
            this.grp_backup.Name = "grp_backup";
            this.grp_backup.Size = new System.Drawing.Size(179, 366);
            this.grp_backup.TabIndex = 1;
            this.grp_backup.TabStop = false;
            this.grp_backup.Text = "Liste Sauvegarde";
            // 
            // dgv_backup
            // 
            this.dgv_backup.AllowUserToAddRows = false;
            this.dgv_backup.AllowUserToDeleteRows = false;
            this.dgv_backup.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_backup.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgv_backup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_backup.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fileName});
            this.dgv_backup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_backup.Location = new System.Drawing.Point(3, 16);
            this.dgv_backup.Name = "dgv_backup";
            this.dgv_backup.ReadOnly = true;
            this.dgv_backup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_backup.Size = new System.Drawing.Size(173, 347);
            this.dgv_backup.TabIndex = 1;
            this.dgv_backup.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_backup_CellContentClick);
            // 
            // fileName
            // 
            this.fileName.HeaderText = "Nom Fichier";
            this.fileName.Name = "fileName";
            this.fileName.ReadOnly = true;
            // 
            // btn_current
            // 
            this.btn_current.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_current.Location = new System.Drawing.Point(4, 2);
            this.btn_current.Name = "btn_current";
            this.btn_current.Size = new System.Drawing.Size(179, 25);
            this.btn_current.TabIndex = 1;
            this.btn_current.Text = "Afficher Log Courant";
            this.btn_current.UseVisualStyleBackColor = true;
            this.btn_current.Click += new System.EventHandler(this.btn_current_Click);
            // 
            // btn_save
            // 
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.Location = new System.Drawing.Point(4, 397);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(179, 26);
            this.btn_save.TabIndex = 1;
            this.btn_save.Text = "Sauvegarder";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pbar_statut);
            this.panel4.Location = new System.Drawing.Point(2, 429);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(882, 22);
            this.panel4.TabIndex = 7;
            // 
            // pbar_statut
            // 
            this.pbar_statut.Location = new System.Drawing.Point(3, 6);
            this.pbar_statut.Maximum = 10000;
            this.pbar_statut.Name = "pbar_statut";
            this.pbar_statut.Size = new System.Drawing.Size(876, 10);
            this.pbar_statut.TabIndex = 0;
            // 
            // Form_Archive_Serveur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 456);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_current);
            this.Controls.Add(this.grp_backup);
            this.Controls.Add(this.grp_log);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(901, 495);
            this.MinimumSize = new System.Drawing.Size(901, 495);
            this.Name = "Form_Archive_Serveur";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Archive Serveur";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Archive_Serveur_FormClosed);
            this.Load += new System.EventHandler(this.Form_Archive_Serveur_Load);
            this.grp_log.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_log)).EndInit();
            this.grp_backup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_backup)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_log;
        private System.Windows.Forms.DataGridView dgv_log;
        private System.Windows.Forms.DataGridViewTextBoxColumn pos;
        private System.Windows.Forms.DataGridViewTextBoxColumn num;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn heure;
        private System.Windows.Forms.GroupBox grp_backup;
        private System.Windows.Forms.Button btn_current;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.DataGridView dgv_backup;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileName;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ProgressBar pbar_statut;
    }
}