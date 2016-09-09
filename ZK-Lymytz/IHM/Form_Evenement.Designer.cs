namespace ZK_Lymytz.IHM
{
    partial class Form_Evenement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Evenement));
            this.grp_pointeuse = new System.Windows.Forms.GroupBox();
            this.dgv_pointeuse = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pointeuse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grp_log = new System.Windows.Forms.GroupBox();
            this.dgv_log = new System.Windows.Forms.DataGridView();
            this.pos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.heure = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_load = new System.Windows.Forms.Button();
            this.btn_synchro = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbar_statut = new System.Windows.Forms.ProgressBar();
            this.chk_filter = new System.Windows.Forms.CheckBox();
            this.grp_filter = new System.Windows.Forms.GroupBox();
            this.txt_marge_heure_fin = new System.Windows.Forms.NumericUpDown();
            this.txt_marge_heure_debut = new System.Windows.Forms.NumericUpDown();
            this.chk_date = new System.Windows.Forms.CheckBox();
            this.chk_employe = new System.Windows.Forms.CheckBox();
            this.txt_id = new System.Windows.Forms.TextBox();
            this.grp_planning = new System.Windows.Forms.GroupBox();
            this.txt_fin = new System.Windows.Forms.TextBox();
            this.txt_debut = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtp_heure_fin = new System.Windows.Forms.DateTimePicker();
            this.dtp_heure_debut = new System.Windows.Forms.DateTimePicker();
            this.dtp_date_fin = new System.Windows.Forms.DateTimePicker();
            this.dtp_date_debut = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.com_employe = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab_app = new System.Windows.Forms.TabPage();
            this.tab_filtre = new System.Windows.Forms.TabPage();
            this.tab_excluse = new System.Windows.Forms.TabPage();
            this.chk_exclus = new System.Windows.Forms.CheckBox();
            this.grp_date_exclus = new System.Windows.Forms.GroupBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tab_date_exclus = new System.Windows.Forms.TabPage();
            this.mc_date_exclus = new System.Windows.Forms.MonthCalendar();
            this.tab_list_exclu = new System.Windows.Forms.TabPage();
            this.txt_list_date_exclus = new System.Windows.Forms.RichTextBox();
            this.context_list_date_exclu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.effacerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grp_employe_exclu = new System.Windows.Forms.GroupBox();
            this.dgv_employe = new System.Windows.Forms.DataGridView();
            this.idEmp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.check = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.nomEmpl = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tt_message = new System.Windows.Forms.ToolTip(this.components);
            this.grp_pointeuse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pointeuse)).BeginInit();
            this.grp_log.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_log)).BeginInit();
            this.panel1.SuspendLayout();
            this.grp_filter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_marge_heure_fin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_marge_heure_debut)).BeginInit();
            this.grp_planning.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tab_app.SuspendLayout();
            this.tab_filtre.SuspendLayout();
            this.tab_excluse.SuspendLayout();
            this.grp_date_exclus.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tab_date_exclus.SuspendLayout();
            this.tab_list_exclu.SuspendLayout();
            this.context_list_date_exclu.SuspendLayout();
            this.grp_employe_exclu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_employe)).BeginInit();
            this.SuspendLayout();
            // 
            // grp_pointeuse
            // 
            this.grp_pointeuse.Controls.Add(this.dgv_pointeuse);
            this.grp_pointeuse.Location = new System.Drawing.Point(3, 6);
            this.grp_pointeuse.Name = "grp_pointeuse";
            this.grp_pointeuse.Size = new System.Drawing.Size(264, 390);
            this.grp_pointeuse.TabIndex = 0;
            this.grp_pointeuse.TabStop = false;
            this.grp_pointeuse.Text = "Pointeuses";
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
            this.dgv_pointeuse.Name = "dgv_pointeuse";
            this.dgv_pointeuse.ReadOnly = true;
            this.dgv_pointeuse.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_pointeuse.Size = new System.Drawing.Size(258, 371);
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
            // grp_log
            // 
            this.grp_log.Controls.Add(this.dgv_log);
            this.grp_log.Location = new System.Drawing.Point(305, -2);
            this.grp_log.Name = "grp_log";
            this.grp_log.Size = new System.Drawing.Size(650, 381);
            this.grp_log.TabIndex = 1;
            this.grp_log.TabStop = false;
            this.grp_log.Text = "Historique";
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
            this.dgv_log.Size = new System.Drawing.Size(644, 362);
            this.dgv_log.TabIndex = 1;
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
            // btn_load
            // 
            this.btn_load.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_load.Location = new System.Drawing.Point(308, 382);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(200, 26);
            this.btn_load.TabIndex = 2;
            this.btn_load.Text = "Charger Logs";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // btn_synchro
            // 
            this.btn_synchro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_synchro.Location = new System.Drawing.Point(793, 381);
            this.btn_synchro.Name = "btn_synchro";
            this.btn_synchro.Size = new System.Drawing.Size(156, 26);
            this.btn_synchro.TabIndex = 3;
            this.btn_synchro.Text = "Synchroniser avec serveur";
            this.btn_synchro.UseVisualStyleBackColor = true;
            this.btn_synchro.Click += new System.EventHandler(this.btn_synchro_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pbar_statut);
            this.panel1.Location = new System.Drawing.Point(4, 414);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(950, 19);
            this.panel1.TabIndex = 4;
            // 
            // pbar_statut
            // 
            this.pbar_statut.Location = new System.Drawing.Point(3, 4);
            this.pbar_statut.Maximum = 10000;
            this.pbar_statut.Name = "pbar_statut";
            this.pbar_statut.Size = new System.Drawing.Size(945, 10);
            this.pbar_statut.TabIndex = 0;
            // 
            // chk_filter
            // 
            this.chk_filter.AutoSize = true;
            this.chk_filter.Location = new System.Drawing.Point(5, 2);
            this.chk_filter.Name = "chk_filter";
            this.chk_filter.Size = new System.Drawing.Size(101, 17);
            this.chk_filter.TabIndex = 8;
            this.chk_filter.Text = "Filtrer Historique";
            this.chk_filter.UseVisualStyleBackColor = true;
            this.chk_filter.CheckedChanged += new System.EventHandler(this.chk_filter_CheckedChanged);
            // 
            // grp_filter
            // 
            this.grp_filter.Controls.Add(this.txt_marge_heure_fin);
            this.grp_filter.Controls.Add(this.txt_marge_heure_debut);
            this.grp_filter.Controls.Add(this.chk_date);
            this.grp_filter.Controls.Add(this.chk_employe);
            this.grp_filter.Controls.Add(this.txt_id);
            this.grp_filter.Controls.Add(this.grp_planning);
            this.grp_filter.Controls.Add(this.dtp_heure_fin);
            this.grp_filter.Controls.Add(this.dtp_heure_debut);
            this.grp_filter.Controls.Add(this.dtp_date_fin);
            this.grp_filter.Controls.Add(this.dtp_date_debut);
            this.grp_filter.Controls.Add(this.label3);
            this.grp_filter.Controls.Add(this.label2);
            this.grp_filter.Controls.Add(this.com_employe);
            this.grp_filter.Enabled = false;
            this.grp_filter.Location = new System.Drawing.Point(5, 25);
            this.grp_filter.Name = "grp_filter";
            this.grp_filter.Size = new System.Drawing.Size(263, 371);
            this.grp_filter.TabIndex = 7;
            this.grp_filter.TabStop = false;
            this.grp_filter.Text = "Elément Filtagre";
            // 
            // txt_marge_heure_fin
            // 
            this.txt_marge_heure_fin.Enabled = false;
            this.txt_marge_heure_fin.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.txt_marge_heure_fin.Location = new System.Drawing.Point(216, 173);
            this.txt_marge_heure_fin.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.txt_marge_heure_fin.Name = "txt_marge_heure_fin";
            this.txt_marge_heure_fin.Size = new System.Drawing.Size(41, 20);
            this.txt_marge_heure_fin.TabIndex = 8;
            this.tt_message.SetToolTip(this.txt_marge_heure_fin, "marge supérieur sur l\'heure de fin");
            // 
            // txt_marge_heure_debut
            // 
            this.txt_marge_heure_debut.AccessibleDescription = "";
            this.txt_marge_heure_debut.Enabled = false;
            this.txt_marge_heure_debut.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.txt_marge_heure_debut.Location = new System.Drawing.Point(216, 125);
            this.txt_marge_heure_debut.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.txt_marge_heure_debut.Name = "txt_marge_heure_debut";
            this.txt_marge_heure_debut.Size = new System.Drawing.Size(41, 20);
            this.txt_marge_heure_debut.TabIndex = 8;
            this.tt_message.SetToolTip(this.txt_marge_heure_debut, "marge inférieur sur l\'heure de début");
            // 
            // chk_date
            // 
            this.chk_date.AutoSize = true;
            this.chk_date.Location = new System.Drawing.Point(10, 88);
            this.chk_date.Name = "chk_date";
            this.chk_date.Size = new System.Drawing.Size(54, 17);
            this.chk_date.TabIndex = 6;
            this.chk_date.Text = "Dates";
            this.chk_date.UseVisualStyleBackColor = true;
            this.chk_date.CheckedChanged += new System.EventHandler(this.chk_date_CheckedChanged);
            // 
            // chk_employe
            // 
            this.chk_employe.AutoSize = true;
            this.chk_employe.Location = new System.Drawing.Point(10, 24);
            this.chk_employe.Name = "chk_employe";
            this.chk_employe.Size = new System.Drawing.Size(66, 17);
            this.chk_employe.TabIndex = 6;
            this.chk_employe.Text = "Employé";
            this.chk_employe.UseVisualStyleBackColor = true;
            this.chk_employe.CheckedChanged += new System.EventHandler(this.chk_employe_CheckedChanged);
            // 
            // txt_id
            // 
            this.txt_id.Location = new System.Drawing.Point(3, 46);
            this.txt_id.Name = "txt_id";
            this.txt_id.ReadOnly = true;
            this.txt_id.Size = new System.Drawing.Size(60, 20);
            this.txt_id.TabIndex = 7;
            this.txt_id.Text = "0";
            this.txt_id.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grp_planning
            // 
            this.grp_planning.Controls.Add(this.txt_fin);
            this.grp_planning.Controls.Add(this.txt_debut);
            this.grp_planning.Controls.Add(this.label5);
            this.grp_planning.Controls.Add(this.label4);
            this.grp_planning.Location = new System.Drawing.Point(3, 206);
            this.grp_planning.Name = "grp_planning";
            this.grp_planning.Size = new System.Drawing.Size(254, 134);
            this.grp_planning.TabIndex = 6;
            this.grp_planning.TabStop = false;
            this.grp_planning.Text = "Planning";
            // 
            // txt_fin
            // 
            this.txt_fin.Location = new System.Drawing.Point(16, 99);
            this.txt_fin.Name = "txt_fin";
            this.txt_fin.ReadOnly = true;
            this.txt_fin.Size = new System.Drawing.Size(148, 20);
            this.txt_fin.TabIndex = 5;
            // 
            // txt_debut
            // 
            this.txt_debut.Location = new System.Drawing.Point(16, 47);
            this.txt_debut.Name = "txt_debut";
            this.txt_debut.ReadOnly = true;
            this.txt_debut.Size = new System.Drawing.Size(148, 20);
            this.txt_debut.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Heure Fin";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Heure Début";
            // 
            // dtp_heure_fin
            // 
            this.dtp_heure_fin.Enabled = false;
            this.dtp_heure_fin.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_heure_fin.Location = new System.Drawing.Point(124, 173);
            this.dtp_heure_fin.Name = "dtp_heure_fin";
            this.dtp_heure_fin.ShowUpDown = true;
            this.dtp_heure_fin.Size = new System.Drawing.Size(86, 20);
            this.dtp_heure_fin.TabIndex = 3;
            this.dtp_heure_fin.Value = new System.DateTime(2016, 9, 6, 0, 0, 0, 0);
            this.dtp_heure_fin.ValueChanged += new System.EventHandler(this.dtp_heure_fin_ValueChanged);
            // 
            // dtp_heure_debut
            // 
            this.dtp_heure_debut.CustomFormat = "";
            this.dtp_heure_debut.Enabled = false;
            this.dtp_heure_debut.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_heure_debut.Location = new System.Drawing.Point(124, 125);
            this.dtp_heure_debut.Name = "dtp_heure_debut";
            this.dtp_heure_debut.ShowUpDown = true;
            this.dtp_heure_debut.Size = new System.Drawing.Size(86, 20);
            this.dtp_heure_debut.TabIndex = 2;
            this.dtp_heure_debut.Value = new System.DateTime(2016, 9, 6, 0, 0, 0, 0);
            this.dtp_heure_debut.ValueChanged += new System.EventHandler(this.dtp_heure_debut_ValueChanged);
            // 
            // dtp_date_fin
            // 
            this.dtp_date_fin.Enabled = false;
            this.dtp_date_fin.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_date_fin.Location = new System.Drawing.Point(16, 173);
            this.dtp_date_fin.Name = "dtp_date_fin";
            this.dtp_date_fin.Size = new System.Drawing.Size(102, 20);
            this.dtp_date_fin.TabIndex = 3;
            this.dtp_date_fin.ValueChanged += new System.EventHandler(this.dtp_fin_ValueChanged);
            // 
            // dtp_date_debut
            // 
            this.dtp_date_debut.Enabled = false;
            this.dtp_date_debut.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_date_debut.Location = new System.Drawing.Point(16, 125);
            this.dtp_date_debut.Name = "dtp_date_debut";
            this.dtp_date_debut.Size = new System.Drawing.Size(102, 20);
            this.dtp_date_debut.TabIndex = 2;
            this.dtp_date_debut.ValueChanged += new System.EventHandler(this.dtp_debut_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Au";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Du";
            // 
            // com_employe
            // 
            this.com_employe.Enabled = false;
            this.com_employe.FormattingEnabled = true;
            this.com_employe.Location = new System.Drawing.Point(65, 45);
            this.com_employe.Name = "com_employe";
            this.com_employe.Size = new System.Drawing.Size(192, 21);
            this.com_employe.TabIndex = 0;
            this.com_employe.SelectedIndexChanged += new System.EventHandler(this.com_employe_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl1.Controls.Add(this.tab_app);
            this.tabControl1.Controls.Add(this.tab_filtre);
            this.tabControl1.Controls.Add(this.tab_excluse);
            this.tabControl1.Location = new System.Drawing.Point(4, 1);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(298, 410);
            this.tabControl1.TabIndex = 1;
            // 
            // tab_app
            // 
            this.tab_app.Controls.Add(this.grp_pointeuse);
            this.tab_app.Location = new System.Drawing.Point(23, 4);
            this.tab_app.Name = "tab_app";
            this.tab_app.Padding = new System.Windows.Forms.Padding(3);
            this.tab_app.Size = new System.Drawing.Size(271, 402);
            this.tab_app.TabIndex = 0;
            this.tab_app.Text = "Appareils";
            this.tab_app.UseVisualStyleBackColor = true;
            // 
            // tab_filtre
            // 
            this.tab_filtre.Controls.Add(this.grp_filter);
            this.tab_filtre.Controls.Add(this.chk_filter);
            this.tab_filtre.Location = new System.Drawing.Point(23, 4);
            this.tab_filtre.Name = "tab_filtre";
            this.tab_filtre.Padding = new System.Windows.Forms.Padding(3);
            this.tab_filtre.Size = new System.Drawing.Size(271, 402);
            this.tab_filtre.TabIndex = 1;
            this.tab_filtre.Text = "Filtrage";
            this.tab_filtre.UseVisualStyleBackColor = true;
            // 
            // tab_excluse
            // 
            this.tab_excluse.Controls.Add(this.chk_exclus);
            this.tab_excluse.Controls.Add(this.grp_date_exclus);
            this.tab_excluse.Controls.Add(this.grp_employe_exclu);
            this.tab_excluse.Location = new System.Drawing.Point(23, 4);
            this.tab_excluse.Name = "tab_excluse";
            this.tab_excluse.Padding = new System.Windows.Forms.Padding(3);
            this.tab_excluse.Size = new System.Drawing.Size(271, 402);
            this.tab_excluse.TabIndex = 2;
            this.tab_excluse.Text = "Exclusion";
            this.tab_excluse.UseVisualStyleBackColor = true;
            // 
            // chk_exclus
            // 
            this.chk_exclus.AutoSize = true;
            this.chk_exclus.Location = new System.Drawing.Point(5, 2);
            this.chk_exclus.Name = "chk_exclus";
            this.chk_exclus.Size = new System.Drawing.Size(117, 17);
            this.chk_exclus.TabIndex = 9;
            this.chk_exclus.Text = "Exclusion Eléments";
            this.chk_exclus.UseVisualStyleBackColor = true;
            this.chk_exclus.CheckedChanged += new System.EventHandler(this.chk_exclus_CheckedChanged);
            // 
            // grp_date_exclus
            // 
            this.grp_date_exclus.Controls.Add(this.tabControl2);
            this.grp_date_exclus.Enabled = false;
            this.grp_date_exclus.Location = new System.Drawing.Point(3, 218);
            this.grp_date_exclus.Name = "grp_date_exclus";
            this.grp_date_exclus.Size = new System.Drawing.Size(265, 182);
            this.grp_date_exclus.TabIndex = 2;
            this.grp_date_exclus.TabStop = false;
            this.grp_date_exclus.Text = "Dates Exclu";
            // 
            // tabControl2
            // 
            this.tabControl2.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl2.Controls.Add(this.tab_date_exclus);
            this.tabControl2.Controls.Add(this.tab_list_exclu);
            this.tabControl2.Location = new System.Drawing.Point(2, 14);
            this.tabControl2.Multiline = true;
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(266, 174);
            this.tabControl2.TabIndex = 0;
            // 
            // tab_date_exclus
            // 
            this.tab_date_exclus.Controls.Add(this.mc_date_exclus);
            this.tab_date_exclus.Location = new System.Drawing.Point(23, 4);
            this.tab_date_exclus.Name = "tab_date_exclus";
            this.tab_date_exclus.Padding = new System.Windows.Forms.Padding(3);
            this.tab_date_exclus.Size = new System.Drawing.Size(239, 166);
            this.tab_date_exclus.TabIndex = 0;
            this.tab_date_exclus.Text = "Calendrier";
            this.tab_date_exclus.UseVisualStyleBackColor = true;
            // 
            // mc_date_exclus
            // 
            this.mc_date_exclus.Location = new System.Drawing.Point(4, 2);
            this.mc_date_exclus.Name = "mc_date_exclus";
            this.mc_date_exclus.TabIndex = 1;
            this.mc_date_exclus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mc_date_exclus_MouseDown);
            // 
            // tab_list_exclu
            // 
            this.tab_list_exclu.Controls.Add(this.txt_list_date_exclus);
            this.tab_list_exclu.Location = new System.Drawing.Point(23, 4);
            this.tab_list_exclu.Name = "tab_list_exclu";
            this.tab_list_exclu.Padding = new System.Windows.Forms.Padding(3);
            this.tab_list_exclu.Size = new System.Drawing.Size(239, 166);
            this.tab_list_exclu.TabIndex = 1;
            this.tab_list_exclu.Text = "Liste";
            this.tab_list_exclu.UseVisualStyleBackColor = true;
            // 
            // txt_list_date_exclus
            // 
            this.txt_list_date_exclus.ContextMenuStrip = this.context_list_date_exclu;
            this.txt_list_date_exclus.Location = new System.Drawing.Point(3, 3);
            this.txt_list_date_exclus.Name = "txt_list_date_exclus";
            this.txt_list_date_exclus.Size = new System.Drawing.Size(233, 158);
            this.txt_list_date_exclus.TabIndex = 0;
            this.txt_list_date_exclus.Text = "";
            // 
            // context_list_date_exclu
            // 
            this.context_list_date_exclu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.effacerToolStripMenuItem});
            this.context_list_date_exclu.Name = "context_list_date_exclu";
            this.context_list_date_exclu.Size = new System.Drawing.Size(111, 26);
            // 
            // effacerToolStripMenuItem
            // 
            this.effacerToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.clean;
            this.effacerToolStripMenuItem.Name = "effacerToolStripMenuItem";
            this.effacerToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.effacerToolStripMenuItem.Text = "Effacer";
            this.effacerToolStripMenuItem.Click += new System.EventHandler(this.effacerToolStripMenuItem_Click);
            // 
            // grp_employe_exclu
            // 
            this.grp_employe_exclu.Controls.Add(this.dgv_employe);
            this.grp_employe_exclu.Enabled = false;
            this.grp_employe_exclu.Location = new System.Drawing.Point(3, 18);
            this.grp_employe_exclu.Name = "grp_employe_exclu";
            this.grp_employe_exclu.Size = new System.Drawing.Size(265, 197);
            this.grp_employe_exclu.TabIndex = 1;
            this.grp_employe_exclu.TabStop = false;
            this.grp_employe_exclu.Text = "Employé Exclu";
            // 
            // dgv_employe
            // 
            this.dgv_employe.AllowUserToAddRows = false;
            this.dgv_employe.AllowUserToDeleteRows = false;
            this.dgv_employe.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_employe.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgv_employe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_employe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idEmp,
            this.check,
            this.nomEmpl});
            this.dgv_employe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_employe.Location = new System.Drawing.Point(3, 16);
            this.dgv_employe.Name = "dgv_employe";
            this.dgv_employe.ReadOnly = true;
            this.dgv_employe.RowHeadersVisible = false;
            this.dgv_employe.Size = new System.Drawing.Size(259, 178);
            this.dgv_employe.TabIndex = 0;
            this.dgv_employe.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_employe_CellContentClick);
            // 
            // idEmp
            // 
            this.idEmp.HeaderText = "ID";
            this.idEmp.Name = "idEmp";
            this.idEmp.ReadOnly = true;
            this.idEmp.Visible = false;
            // 
            // check
            // 
            this.check.FillWeight = 20.30457F;
            this.check.HeaderText = "";
            this.check.Name = "check";
            this.check.ReadOnly = true;
            // 
            // nomEmpl
            // 
            this.nomEmpl.FillWeight = 179.6954F;
            this.nomEmpl.HeaderText = "Noms & Prénoms";
            this.nomEmpl.Name = "nomEmpl";
            this.nomEmpl.ReadOnly = true;
            // 
            // tt_message
            // 
            this.tt_message.ToolTipTitle = "Marges";
            // 
            // Form_Evenement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 437);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_synchro);
            this.Controls.Add(this.btn_load);
            this.Controls.Add(this.grp_log);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(975, 476);
            this.MinimumSize = new System.Drawing.Size(975, 476);
            this.Name = "Form_Evenement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Entrée/Sortie";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Evenement_FormClosed);
            this.Load += new System.EventHandler(this.Form_Evenement_Load);
            this.grp_pointeuse.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pointeuse)).EndInit();
            this.grp_log.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_log)).EndInit();
            this.panel1.ResumeLayout(false);
            this.grp_filter.ResumeLayout(false);
            this.grp_filter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_marge_heure_fin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_marge_heure_debut)).EndInit();
            this.grp_planning.ResumeLayout(false);
            this.grp_planning.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tab_app.ResumeLayout(false);
            this.tab_filtre.ResumeLayout(false);
            this.tab_filtre.PerformLayout();
            this.tab_excluse.ResumeLayout(false);
            this.tab_excluse.PerformLayout();
            this.grp_date_exclus.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tab_date_exclus.ResumeLayout(false);
            this.tab_list_exclu.ResumeLayout(false);
            this.context_list_date_exclu.ResumeLayout(false);
            this.grp_employe_exclu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_employe)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_pointeuse;
        private System.Windows.Forms.GroupBox grp_log;
        private System.Windows.Forms.DataGridView dgv_pointeuse;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn pointeuse;
        private System.Windows.Forms.DataGridView dgv_log;
        private System.Windows.Forms.DataGridViewTextBoxColumn pos;
        private System.Windows.Forms.DataGridViewTextBoxColumn num;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn heure;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.Button btn_synchro;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ProgressBar pbar_statut;
        private System.Windows.Forms.CheckBox chk_filter;
        private System.Windows.Forms.GroupBox grp_filter;
        private System.Windows.Forms.CheckBox chk_date;
        private System.Windows.Forms.CheckBox chk_employe;
        private System.Windows.Forms.TextBox txt_id;
        private System.Windows.Forms.GroupBox grp_planning;
        private System.Windows.Forms.TextBox txt_fin;
        private System.Windows.Forms.TextBox txt_debut;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtp_date_fin;
        private System.Windows.Forms.DateTimePicker dtp_date_debut;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox com_employe;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab_app;
        private System.Windows.Forms.TabPage tab_filtre;
        private System.Windows.Forms.TabPage tab_excluse;
        private System.Windows.Forms.GroupBox grp_date_exclus;
        private System.Windows.Forms.GroupBox grp_employe_exclu;
        private System.Windows.Forms.DataGridView dgv_employe;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tab_date_exclus;
        private System.Windows.Forms.MonthCalendar mc_date_exclus;
        private System.Windows.Forms.TabPage tab_list_exclu;
        private System.Windows.Forms.RichTextBox txt_list_date_exclus;
        private System.Windows.Forms.ContextMenuStrip context_list_date_exclu;
        private System.Windows.Forms.ToolStripMenuItem effacerToolStripMenuItem;
        private System.Windows.Forms.CheckBox chk_exclus;
        private System.Windows.Forms.DataGridViewTextBoxColumn idEmp;
        private System.Windows.Forms.DataGridViewCheckBoxColumn check;
        private System.Windows.Forms.DataGridViewTextBoxColumn nomEmpl;
        private System.Windows.Forms.DateTimePicker dtp_heure_fin;
        private System.Windows.Forms.DateTimePicker dtp_heure_debut;
        private System.Windows.Forms.NumericUpDown txt_marge_heure_fin;
        private System.Windows.Forms.NumericUpDown txt_marge_heure_debut;
        private System.Windows.Forms.ToolTip tt_message;
    }
}