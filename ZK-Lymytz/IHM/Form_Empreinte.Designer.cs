namespace ZK_Lymytz.IHM
{
    partial class Form_Empreinte
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Empreinte));
            this.panel1 = new System.Windows.Forms.Panel();
            this.chk_via_serveur = new System.Windows.Forms.CheckBox();
            this.grp_source = new System.Windows.Forms.GroupBox();
            this.dgv_pointeuse = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pointeuse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_add_serveur = new System.Windows.Forms.Button();
            this.btn_synchro = new System.Windows.Forms.Button();
            this.grp_destinataire = new System.Windows.Forms.GroupBox();
            this.btn_distant = new System.Windows.Forms.Button();
            this.dgv_destination = new System.Windows.Forms.DataGridView();
            this.id_d = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chech = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chk_not_in = new System.Windows.Forms.CheckBox();
            this.com_employe = new System.Windows.Forms.ComboBox();
            this.chk_all = new System.Windows.Forms.CheckBox();
            this.grp_template = new System.Windows.Forms.GroupBox();
            this.dgv_empreinte = new System.Windows.Forms.DataGridView();
            this.id_em = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.check_ = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_e = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.main = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doigt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.context_empreinte = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.supprimerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.chargerTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_infos = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_empreinte = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_faciale = new System.Windows.Forms.ToolStripMenuItem();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pbar_statut = new System.Windows.Forms.ProgressBar();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tsmi_recuperer_infos = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.grp_source.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pointeuse)).BeginInit();
            this.panel2.SuspendLayout();
            this.grp_destinataire.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_destination)).BeginInit();
            this.panel3.SuspendLayout();
            this.grp_template.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_empreinte)).BeginInit();
            this.context_empreinte.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chk_via_serveur);
            this.panel1.Controls.Add(this.grp_source);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(197, 447);
            this.panel1.TabIndex = 0;
            // 
            // chk_via_serveur
            // 
            this.chk_via_serveur.AutoSize = true;
            this.chk_via_serveur.Location = new System.Drawing.Point(4, 8);
            this.chk_via_serveur.Name = "chk_via_serveur";
            this.chk_via_serveur.Size = new System.Drawing.Size(112, 17);
            this.chk_via_serveur.TabIndex = 0;
            this.chk_via_serveur.Text = "A partir du serveur";
            this.chk_via_serveur.UseVisualStyleBackColor = true;
            this.chk_via_serveur.CheckedChanged += new System.EventHandler(this.chk_via_serveur_CheckedChanged);
            // 
            // grp_source
            // 
            this.grp_source.Controls.Add(this.dgv_pointeuse);
            this.grp_source.Location = new System.Drawing.Point(4, 31);
            this.grp_source.Name = "grp_source";
            this.grp_source.Size = new System.Drawing.Size(191, 413);
            this.grp_source.TabIndex = 0;
            this.grp_source.TabStop = false;
            this.grp_source.Text = "Source";
            // 
            // dgv_pointeuse
            // 
            this.dgv_pointeuse.AllowUserToAddRows = false;
            this.dgv_pointeuse.AllowUserToDeleteRows = false;
            this.dgv_pointeuse.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_pointeuse.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgv_pointeuse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_pointeuse.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.pointeuse});
            this.dgv_pointeuse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_pointeuse.Location = new System.Drawing.Point(3, 16);
            this.dgv_pointeuse.MultiSelect = false;
            this.dgv_pointeuse.Name = "dgv_pointeuse";
            this.dgv_pointeuse.ReadOnly = true;
            this.dgv_pointeuse.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_pointeuse.Size = new System.Drawing.Size(185, 394);
            this.dgv_pointeuse.TabIndex = 0;
            this.dgv_pointeuse.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_pointeuse_CellContentClick);
            // 
            // id
            // 
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // pointeuse
            // 
            this.pointeuse.HeaderText = "Pointeuse";
            this.pointeuse.Name = "pointeuse";
            this.pointeuse.ReadOnly = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_add_serveur);
            this.panel2.Controls.Add(this.btn_synchro);
            this.panel2.Controls.Add(this.grp_destinataire);
            this.panel2.Location = new System.Drawing.Point(714, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(219, 447);
            this.panel2.TabIndex = 0;
            // 
            // btn_add_serveur
            // 
            this.btn_add_serveur.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_add_serveur.Image = global::ZK_Lymytz.Properties.Resources._in;
            this.btn_add_serveur.Location = new System.Drawing.Point(6, 5);
            this.btn_add_serveur.Name = "btn_add_serveur";
            this.btn_add_serveur.Size = new System.Drawing.Size(35, 26);
            this.btn_add_serveur.TabIndex = 2;
            this.toolTip.SetToolTip(this.btn_add_serveur, "Télécharger les empreintes de la pointeuse vers la base de donnée");
            this.btn_add_serveur.UseVisualStyleBackColor = true;
            this.btn_add_serveur.Click += new System.EventHandler(this.btn_add_serveur_Click);
            // 
            // btn_synchro
            // 
            this.btn_synchro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_synchro.Image = global::ZK_Lymytz.Properties.Resources.next;
            this.btn_synchro.Location = new System.Drawing.Point(180, 4);
            this.btn_synchro.Name = "btn_synchro";
            this.btn_synchro.Size = new System.Drawing.Size(33, 28);
            this.btn_synchro.TabIndex = 1;
            this.toolTip.SetToolTip(this.btn_synchro, "Ajouter les empreintes dans la pointeuse");
            this.btn_synchro.UseVisualStyleBackColor = true;
            this.btn_synchro.Click += new System.EventHandler(this.btn_synchro_Click);
            // 
            // grp_destinataire
            // 
            this.grp_destinataire.Controls.Add(this.btn_distant);
            this.grp_destinataire.Controls.Add(this.dgv_destination);
            this.grp_destinataire.Location = new System.Drawing.Point(3, 31);
            this.grp_destinataire.Name = "grp_destinataire";
            this.grp_destinataire.Size = new System.Drawing.Size(213, 416);
            this.grp_destinataire.TabIndex = 0;
            this.grp_destinataire.TabStop = false;
            this.grp_destinataire.Text = "Destinataire";
            // 
            // btn_distant
            // 
            this.btn_distant.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_distant.Location = new System.Drawing.Point(3, 383);
            this.btn_distant.Name = "btn_distant";
            this.btn_distant.Size = new System.Drawing.Size(207, 28);
            this.btn_distant.TabIndex = 1;
            this.btn_distant.Text = "Serveur Distant";
            this.btn_distant.UseVisualStyleBackColor = true;
            this.btn_distant.Click += new System.EventHandler(this.btn_distant_Click);
            // 
            // dgv_destination
            // 
            this.dgv_destination.AllowUserToAddRows = false;
            this.dgv_destination.AllowUserToDeleteRows = false;
            this.dgv_destination.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_destination.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgv_destination.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_destination.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_d,
            this.chech,
            this.dataGridViewTextBoxColumn2});
            this.dgv_destination.Location = new System.Drawing.Point(3, 16);
            this.dgv_destination.MultiSelect = false;
            this.dgv_destination.Name = "dgv_destination";
            this.dgv_destination.ReadOnly = true;
            this.dgv_destination.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_destination.Size = new System.Drawing.Size(207, 361);
            this.dgv_destination.TabIndex = 0;
            this.dgv_destination.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_destination_CellContentClick);
            // 
            // id_d
            // 
            this.id_d.HeaderText = "ID";
            this.id_d.Name = "id_d";
            this.id_d.ReadOnly = true;
            this.id_d.Visible = false;
            // 
            // chech
            // 
            this.chech.FillWeight = 30.45685F;
            this.chech.HeaderText = "";
            this.chech.Name = "chech";
            this.chech.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.FillWeight = 169.5432F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Pointeuse";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chk_not_in);
            this.panel3.Controls.Add(this.com_employe);
            this.panel3.Controls.Add(this.chk_all);
            this.panel3.Controls.Add(this.grp_template);
            this.panel3.Controls.Add(this.menuStrip);
            this.panel3.Location = new System.Drawing.Point(197, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(517, 447);
            this.panel3.TabIndex = 1;
            // 
            // chk_not_in
            // 
            this.chk_not_in.AutoSize = true;
            this.chk_not_in.Location = new System.Drawing.Point(412, 8);
            this.chk_not_in.Name = "chk_not_in";
            this.chk_not_in.Size = new System.Drawing.Size(96, 17);
            this.chk_not_in.TabIndex = 4;
            this.chk_not_in.Text = "Non disponible";
            this.chk_not_in.UseVisualStyleBackColor = true;
            this.chk_not_in.CheckedChanged += new System.EventHandler(this.chk_not_in_CheckedChanged);
            // 
            // com_employe
            // 
            this.com_employe.FormattingEnabled = true;
            this.com_employe.Location = new System.Drawing.Point(63, 6);
            this.com_employe.Name = "com_employe";
            this.com_employe.Size = new System.Drawing.Size(177, 21);
            this.com_employe.TabIndex = 2;
            this.com_employe.SelectedIndexChanged += new System.EventHandler(this.com_employe_SelectedIndexChanged);
            // 
            // chk_all
            // 
            this.chk_all.AutoSize = true;
            this.chk_all.Location = new System.Drawing.Point(9, 8);
            this.chk_all.Name = "chk_all";
            this.chk_all.Size = new System.Drawing.Size(48, 17);
            this.chk_all.TabIndex = 0;
            this.chk_all.Text = "Tout";
            this.chk_all.UseVisualStyleBackColor = true;
            this.chk_all.CheckedChanged += new System.EventHandler(this.chk_all_CheckedChanged);
            // 
            // grp_template
            // 
            this.grp_template.Controls.Add(this.dgv_empreinte);
            this.grp_template.Location = new System.Drawing.Point(6, 31);
            this.grp_template.Name = "grp_template";
            this.grp_template.Size = new System.Drawing.Size(502, 413);
            this.grp_template.TabIndex = 0;
            this.grp_template.TabStop = false;
            this.grp_template.Text = "Template";
            // 
            // dgv_empreinte
            // 
            this.dgv_empreinte.AllowUserToAddRows = false;
            this.dgv_empreinte.AllowUserToDeleteRows = false;
            this.dgv_empreinte.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_empreinte.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgv_empreinte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_empreinte.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_em,
            this.check_,
            this.num,
            this.id_e,
            this.nom,
            this.main,
            this.doigt});
            this.dgv_empreinte.ContextMenuStrip = this.context_empreinte;
            this.dgv_empreinte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_empreinte.Location = new System.Drawing.Point(3, 16);
            this.dgv_empreinte.MultiSelect = false;
            this.dgv_empreinte.Name = "dgv_empreinte";
            this.dgv_empreinte.ReadOnly = true;
            this.dgv_empreinte.RowHeadersVisible = false;
            this.dgv_empreinte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_empreinte.Size = new System.Drawing.Size(496, 394);
            this.dgv_empreinte.TabIndex = 0;
            this.dgv_empreinte.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_empreinte_CellContentClick);
            this.dgv_empreinte.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgv_empreinte_MouseDown);
            // 
            // id_em
            // 
            this.id_em.HeaderText = "";
            this.id_em.Name = "id_em";
            this.id_em.ReadOnly = true;
            this.id_em.Visible = false;
            // 
            // check_
            // 
            this.check_.FillWeight = 42.37402F;
            this.check_.HeaderText = "";
            this.check_.Name = "check_";
            this.check_.ReadOnly = true;
            // 
            // num
            // 
            this.num.FillWeight = 50F;
            this.num.HeaderText = "N°";
            this.num.Name = "num";
            this.num.ReadOnly = true;
            // 
            // id_e
            // 
            this.id_e.FillWeight = 50F;
            this.id_e.HeaderText = "ID";
            this.id_e.Name = "id_e";
            this.id_e.ReadOnly = true;
            // 
            // nom
            // 
            this.nom.FillWeight = 258.5975F;
            this.nom.HeaderText = "Noms & Prénoms";
            this.nom.Name = "nom";
            this.nom.ReadOnly = true;
            // 
            // main
            // 
            this.main.FillWeight = 100.0459F;
            this.main.HeaderText = "Main";
            this.main.Name = "main";
            this.main.ReadOnly = true;
            // 
            // doigt
            // 
            this.doigt.FillWeight = 109.3222F;
            this.doigt.HeaderText = "Doigt";
            this.doigt.Name = "doigt";
            this.doigt.ReadOnly = true;
            // 
            // context_empreinte
            // 
            this.context_empreinte.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.supprimerToolStripMenuItem,
            this.tsmi_recuperer_infos});
            this.context_empreinte.Name = "context_empreinte";
            this.context_empreinte.Size = new System.Drawing.Size(194, 70);
            // 
            // supprimerToolStripMenuItem
            // 
            this.supprimerToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.delete;
            this.supprimerToolStripMenuItem.Name = "supprimerToolStripMenuItem";
            this.supprimerToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.supprimerToolStripMenuItem.Text = "Supprimer";
            this.supprimerToolStripMenuItem.Click += new System.EventHandler(this.supprimerToolStripMenuItem_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chargerTemplateToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(243, 7);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(122, 24);
            this.menuStrip.TabIndex = 3;
            this.menuStrip.Text = "menuStrip1";
            // 
            // chargerTemplateToolStripMenuItem
            // 
            this.chargerTemplateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_infos,
            this.tsmi_empreinte,
            this.tsmi_faciale});
            this.chargerTemplateToolStripMenuItem.Name = "chargerTemplateToolStripMenuItem";
            this.chargerTemplateToolStripMenuItem.Size = new System.Drawing.Size(114, 20);
            this.chargerTemplateToolStripMenuItem.Text = "Charger Template";
            // 
            // tsmi_infos
            // 
            this.tsmi_infos.Image = global::ZK_Lymytz.Properties.Resources.document;
            this.tsmi_infos.Name = "tsmi_infos";
            this.tsmi_infos.Size = new System.Drawing.Size(152, 22);
            this.tsmi_infos.Text = "Informations";
            this.tsmi_infos.Visible = false;
            this.tsmi_infos.Click += new System.EventHandler(this.tsmi_infos_Click);
            // 
            // tsmi_empreinte
            // 
            this.tsmi_empreinte.Image = global::ZK_Lymytz.Properties.Resources.empreinte_mini;
            this.tsmi_empreinte.Name = "tsmi_empreinte";
            this.tsmi_empreinte.Size = new System.Drawing.Size(152, 22);
            this.tsmi_empreinte.Text = "Digitale";
            this.tsmi_empreinte.Click += new System.EventHandler(this.tsmi_empreinte_Click);
            // 
            // tsmi_faciale
            // 
            this.tsmi_faciale.Image = global::ZK_Lymytz.Properties.Resources.edit_user;
            this.tsmi_faciale.Name = "tsmi_faciale";
            this.tsmi_faciale.Size = new System.Drawing.Size(152, 22);
            this.tsmi_faciale.Text = "Faciale";
            this.tsmi_faciale.Click += new System.EventHandler(this.tsmi_faciale_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pbar_statut);
            this.panel4.Location = new System.Drawing.Point(0, 453);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(932, 22);
            this.panel4.TabIndex = 5;
            // 
            // pbar_statut
            // 
            this.pbar_statut.Location = new System.Drawing.Point(3, 6);
            this.pbar_statut.Maximum = 10000;
            this.pbar_statut.Name = "pbar_statut";
            this.pbar_statut.Size = new System.Drawing.Size(928, 10);
            this.pbar_statut.TabIndex = 0;
            // 
            // tsmi_recuperer_infos
            // 
            this.tsmi_recuperer_infos.Image = global::ZK_Lymytz.Properties.Resources.document;
            this.tsmi_recuperer_infos.Name = "tsmi_recuperer_infos";
            this.tsmi_recuperer_infos.Size = new System.Drawing.Size(193, 22);
            this.tsmi_recuperer_infos.Text = "Recuperer Information";
            this.tsmi_recuperer_infos.Click += new System.EventHandler(this.tsmi_recuperer_infos_Click);
            // 
            // Form_Empreinte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 477);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(949, 516);
            this.MinimumSize = new System.Drawing.Size(949, 516);
            this.Name = "Form_Empreinte";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion Empreinte";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Empreinte_FormClosed);
            this.Load += new System.EventHandler(this.Form_Empreinte_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grp_source.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pointeuse)).EndInit();
            this.panel2.ResumeLayout(false);
            this.grp_destinataire.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_destination)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.grp_template.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_empreinte)).EndInit();
            this.context_empreinte.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox grp_source;
        private System.Windows.Forms.CheckBox chk_via_serveur;
        private System.Windows.Forms.DataGridView dgv_pointeuse;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn pointeuse;
        private System.Windows.Forms.DataGridView dgv_destination;
        private System.Windows.Forms.GroupBox grp_destinataire;
        private System.Windows.Forms.GroupBox grp_template;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_d;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chech;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridView dgv_empreinte;
        private System.Windows.Forms.CheckBox chk_all;
        private System.Windows.Forms.ComboBox com_employe;
        private System.Windows.Forms.Button btn_synchro;
        private System.Windows.Forms.Button btn_add_serveur;
        private System.Windows.Forms.Button btn_distant;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ProgressBar pbar_statut;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem chargerTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_empreinte;
        private System.Windows.Forms.ToolStripMenuItem tsmi_faciale;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_em;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check_;
        private System.Windows.Forms.DataGridViewTextBoxColumn num;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_e;
        private System.Windows.Forms.DataGridViewTextBoxColumn nom;
        private System.Windows.Forms.DataGridViewTextBoxColumn main;
        private System.Windows.Forms.DataGridViewTextBoxColumn doigt;
        private System.Windows.Forms.CheckBox chk_not_in;
        private System.Windows.Forms.ContextMenuStrip context_empreinte;
        private System.Windows.Forms.ToolStripMenuItem supprimerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi_infos;
        private System.Windows.Forms.ToolStripMenuItem tsmi_recuperer_infos;
    }
}