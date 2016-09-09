namespace ZK_Lymytz.IHM
{
    partial class Form_Archive_Pointeuse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Archive_Pointeuse));
            this.panel_pointeuse = new System.Windows.Forms.Panel();
            this.dgv_pointeuse = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.adresse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grp_backup = new System.Windows.Forms.GroupBox();
            this.dgv_backup = new System.Windows.Forms.DataGridView();
            this.cpt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateSave = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chemin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grp_log = new System.Windows.Forms.GroupBox();
            this.dgv_log = new System.Windows.Forms.DataGridView();
            this.pos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.heure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grp_action = new System.Windows.Forms.GroupBox();
            this.btn_del_doublon = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_load_by_file = new System.Windows.Forms.Button();
            this.btn_load_by_appareil = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pbar_statut = new System.Windows.Forms.ProgressBar();
            this.panel_pointeuse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pointeuse)).BeginInit();
            this.grp_backup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_backup)).BeginInit();
            this.grp_log.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_log)).BeginInit();
            this.grp_action.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_pointeuse
            // 
            this.panel_pointeuse.Controls.Add(this.dgv_pointeuse);
            this.panel_pointeuse.Location = new System.Drawing.Point(0, 0);
            this.panel_pointeuse.Name = "panel_pointeuse";
            this.panel_pointeuse.Size = new System.Drawing.Size(200, 503);
            this.panel_pointeuse.TabIndex = 0;
            // 
            // dgv_pointeuse
            // 
            this.dgv_pointeuse.AllowUserToAddRows = false;
            this.dgv_pointeuse.AllowUserToDeleteRows = false;
            this.dgv_pointeuse.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_pointeuse.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgv_pointeuse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_pointeuse.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.adresse});
            this.dgv_pointeuse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_pointeuse.Location = new System.Drawing.Point(0, 0);
            this.dgv_pointeuse.Name = "dgv_pointeuse";
            this.dgv_pointeuse.ReadOnly = true;
            this.dgv_pointeuse.Size = new System.Drawing.Size(200, 503);
            this.dgv_pointeuse.TabIndex = 1;
            this.dgv_pointeuse.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_pointeuse_CellContentClick);
            // 
            // ID
            // 
            this.ID.HeaderText = "id";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // adresse
            // 
            this.adresse.HeaderText = "Pointeuse";
            this.adresse.Name = "adresse";
            this.adresse.ReadOnly = true;
            // 
            // grp_backup
            // 
            this.grp_backup.Controls.Add(this.dgv_backup);
            this.grp_backup.Location = new System.Drawing.Point(206, 224);
            this.grp_backup.Name = "grp_backup";
            this.grp_backup.Size = new System.Drawing.Size(698, 227);
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
            this.cpt,
            this.fileName,
            this.dateSave,
            this.chemin});
            this.dgv_backup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_backup.Location = new System.Drawing.Point(3, 16);
            this.dgv_backup.Name = "dgv_backup";
            this.dgv_backup.ReadOnly = true;
            this.dgv_backup.RowHeadersVisible = false;
            this.dgv_backup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_backup.Size = new System.Drawing.Size(692, 208);
            this.dgv_backup.TabIndex = 0;
            this.dgv_backup.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_backup_CellContentClick);
            // 
            // cpt
            // 
            this.cpt.FillWeight = 40.60914F;
            this.cpt.HeaderText = "";
            this.cpt.Name = "cpt";
            this.cpt.ReadOnly = true;
            // 
            // fileName
            // 
            this.fileName.FillWeight = 87.867F;
            this.fileName.HeaderText = "Noms Fichier";
            this.fileName.Name = "fileName";
            this.fileName.ReadOnly = true;
            // 
            // dateSave
            // 
            this.dateSave.FillWeight = 83.94864F;
            this.dateSave.HeaderText = "Date Save";
            this.dateSave.Name = "dateSave";
            this.dateSave.ReadOnly = true;
            // 
            // chemin
            // 
            this.chemin.FillWeight = 187.5752F;
            this.chemin.HeaderText = "Chemin";
            this.chemin.Name = "chemin";
            this.chemin.ReadOnly = true;
            // 
            // grp_log
            // 
            this.grp_log.Controls.Add(this.dgv_log);
            this.grp_log.Location = new System.Drawing.Point(206, -2);
            this.grp_log.Name = "grp_log";
            this.grp_log.Size = new System.Drawing.Size(698, 226);
            this.grp_log.TabIndex = 2;
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
            this.dgv_log.Size = new System.Drawing.Size(692, 207);
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
            // grp_action
            // 
            this.grp_action.Controls.Add(this.btn_del_doublon);
            this.grp_action.Controls.Add(this.btn_save);
            this.grp_action.Controls.Add(this.btn_load_by_file);
            this.grp_action.Controls.Add(this.btn_load_by_appareil);
            this.grp_action.Location = new System.Drawing.Point(206, 449);
            this.grp_action.Name = "grp_action";
            this.grp_action.Size = new System.Drawing.Size(698, 51);
            this.grp_action.TabIndex = 3;
            this.grp_action.TabStop = false;
            this.grp_action.Text = "Actions";
            this.grp_action.Enter += new System.EventHandler(this.grp_action_Enter);
            // 
            // btn_del_doublon
            // 
            this.btn_del_doublon.Enabled = false;
            this.btn_del_doublon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_del_doublon.Location = new System.Drawing.Point(506, 18);
            this.btn_del_doublon.Name = "btn_del_doublon";
            this.btn_del_doublon.Size = new System.Drawing.Size(100, 28);
            this.btn_del_doublon.TabIndex = 1;
            this.btn_del_doublon.Text = "Effacer Doublon";
            this.btn_del_doublon.UseVisualStyleBackColor = true;
            this.btn_del_doublon.Click += new System.EventHandler(this.btn_del_doublon_Click);
            // 
            // btn_save
            // 
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.Location = new System.Drawing.Point(610, 18);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(85, 28);
            this.btn_save.TabIndex = 1;
            this.btn_save.Text = "Sauvegarder";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_load_by_file
            // 
            this.btn_load_by_file.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_load_by_file.Location = new System.Drawing.Point(247, 18);
            this.btn_load_by_file.Name = "btn_load_by_file";
            this.btn_load_by_file.Size = new System.Drawing.Size(212, 28);
            this.btn_load_by_file.TabIndex = 0;
            this.btn_load_by_file.Text = "Charger Log (Fichier : 00-00-0000.csv)";
            this.btn_load_by_file.UseVisualStyleBackColor = true;
            this.btn_load_by_file.Click += new System.EventHandler(this.btn_load_by_file_Click);
            // 
            // btn_load_by_appareil
            // 
            this.btn_load_by_appareil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_load_by_appareil.Location = new System.Drawing.Point(18, 18);
            this.btn_load_by_appareil.Name = "btn_load_by_appareil";
            this.btn_load_by_appareil.Size = new System.Drawing.Size(223, 28);
            this.btn_load_by_appareil.TabIndex = 0;
            this.btn_load_by_appareil.Text = "Charger Log (Pointeuse : 192.168.1.201)";
            this.btn_load_by_appareil.UseVisualStyleBackColor = true;
            this.btn_load_by_appareil.Click += new System.EventHandler(this.btn_load_by_appareil_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pbar_statut);
            this.panel4.Location = new System.Drawing.Point(0, 506);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(911, 22);
            this.panel4.TabIndex = 6;
            // 
            // pbar_statut
            // 
            this.pbar_statut.Location = new System.Drawing.Point(3, 6);
            this.pbar_statut.Maximum = 10000;
            this.pbar_statut.Name = "pbar_statut";
            this.pbar_statut.Size = new System.Drawing.Size(904, 10);
            this.pbar_statut.TabIndex = 0;
            // 
            // Form_Archive_Pointeuse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 532);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.grp_action);
            this.Controls.Add(this.grp_log);
            this.Controls.Add(this.grp_backup);
            this.Controls.Add(this.panel_pointeuse);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(927, 571);
            this.MinimumSize = new System.Drawing.Size(927, 571);
            this.Name = "Form_Archive_Pointeuse";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Archives Pointeuse";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Archive_Pointeuse_FormClosed);
            this.Load += new System.EventHandler(this.Form_Archive_Pointeuse_Load);
            this.panel_pointeuse.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pointeuse)).EndInit();
            this.grp_backup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_backup)).EndInit();
            this.grp_log.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_log)).EndInit();
            this.grp_action.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_pointeuse;
        private System.Windows.Forms.DataGridView dgv_pointeuse;
        private System.Windows.Forms.GroupBox grp_backup;
        private System.Windows.Forms.GroupBox grp_log;
        private System.Windows.Forms.DataGridView dgv_log;
        private System.Windows.Forms.GroupBox grp_action;
        private System.Windows.Forms.DataGridView dgv_backup;
        private System.Windows.Forms.Button btn_load_by_appareil;
        private System.Windows.Forms.Button btn_load_by_file;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.DataGridViewTextBoxColumn pos;
        private System.Windows.Forms.DataGridViewTextBoxColumn num;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn heure;
        private System.Windows.Forms.DataGridViewTextBoxColumn cpt;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn chemin;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn adresse;
        private System.Windows.Forms.Button btn_del_doublon;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ProgressBar pbar_statut;
    }
}