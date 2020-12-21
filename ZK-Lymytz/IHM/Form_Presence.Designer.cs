namespace ZK_Lymytz.IHM
{
    partial class Form_Presence
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Presence));
            this.label1 = new System.Windows.Forms.Label();
            this.lnk_today = new System.Windows.Forms.LinkLabel();
            this.dtp_date = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbtn_no = new System.Windows.Forms.RadioButton();
            this.rbtn_yes = new System.Windows.Forms.RadioButton();
            this.txt_heure_fin = new System.Windows.Forms.TextBox();
            this.txt_heure_debut = new System.Windows.Forms.TextBox();
            this.txt_date_fin = new System.Windows.Forms.TextBox();
            this.txt_date_debut = new System.Windows.Forms.TextBox();
            this.grp_total = new System.Windows.Forms.GroupBox();
            this.txt_total_suppl = new System.Windows.Forms.TextBox();
            this.txt_total_presence = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_id = new System.Windows.Forms.TextBox();
            this.txt_poste = new System.Windows.Forms.TextBox();
            this.txt_matricule = new System.Windows.Forms.TextBox();
            this.txt_prenom = new System.Windows.Forms.TextBox();
            this.txt_nom = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grp_list_time = new System.Windows.Forms.GroupBox();
            this.dgv_pointage = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cpt_po = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.heure_entree = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.heure_sortie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.duree = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valider = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.supp = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.context_pointage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.synchroniserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actualiserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reorganiserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reevaluerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fusionnerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.box_identity = new System.Windows.Forms.PictureBox();
            this.btn_next = new System.Windows.Forms.Button();
            this.btn_prec = new System.Windows.Forms.Button();
            this.txt_id_search = new System.Windows.Forms.TextBox();
            this.com_employe = new System.Windows.Forms.ComboBox();
            this.lb_pagination = new System.Windows.Forms.Label();
            this.txt_index_of = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pbar_statut = new System.Windows.Forms.ProgressBar();
            this.voirLesHeuresPrévuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.grp_total.SuspendLayout();
            this.grp_list_time.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pointage)).BeginInit();
            this.context_pointage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.box_identity)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date : ";
            // 
            // lnk_today
            // 
            this.lnk_today.AutoSize = true;
            this.lnk_today.LinkColor = System.Drawing.Color.Blue;
            this.lnk_today.Location = new System.Drawing.Point(12, 348);
            this.lnk_today.Name = "lnk_today";
            this.lnk_today.Size = new System.Drawing.Size(59, 13);
            this.lnk_today.TabIndex = 1;
            this.lnk_today.TabStop = true;
            this.lnk_today.Text = "Aujourd\'hui";
            this.lnk_today.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnk_today_LinkClicked);
            // 
            // dtp_date
            // 
            this.dtp_date.Location = new System.Drawing.Point(57, 3);
            this.dtp_date.Name = "dtp_date";
            this.dtp_date.Size = new System.Drawing.Size(191, 20);
            this.dtp_date.TabIndex = 2;
            this.dtp_date.ValueChanged += new System.EventHandler(this.dtp_date_ValueChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rbtn_no);
            this.panel1.Controls.Add(this.rbtn_yes);
            this.panel1.Controls.Add(this.txt_heure_fin);
            this.panel1.Controls.Add(this.txt_heure_debut);
            this.panel1.Controls.Add(this.txt_date_fin);
            this.panel1.Controls.Add(this.txt_date_debut);
            this.panel1.Controls.Add(this.grp_total);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txt_id);
            this.panel1.Controls.Add(this.txt_poste);
            this.panel1.Controls.Add(this.txt_matricule);
            this.panel1.Controls.Add(this.txt_prenom);
            this.panel1.Controls.Add(this.txt_nom);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.grp_list_time);
            this.panel1.Controls.Add(this.box_identity);
            this.panel1.Controls.Add(this.btn_next);
            this.panel1.Controls.Add(this.btn_prec);
            this.panel1.Location = new System.Drawing.Point(4, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(814, 314);
            this.panel1.TabIndex = 3;
            // 
            // rbtn_no
            // 
            this.rbtn_no.AutoCheck = false;
            this.rbtn_no.AutoSize = true;
            this.rbtn_no.Location = new System.Drawing.Point(571, 56);
            this.rbtn_no.Name = "rbtn_no";
            this.rbtn_no.Size = new System.Drawing.Size(45, 17);
            this.rbtn_no.TabIndex = 16;
            this.rbtn_no.TabStop = true;
            this.rbtn_no.Text = "Non";
            this.rbtn_no.UseVisualStyleBackColor = true;
            // 
            // rbtn_yes
            // 
            this.rbtn_yes.AutoCheck = false;
            this.rbtn_yes.AutoSize = true;
            this.rbtn_yes.Location = new System.Drawing.Point(521, 56);
            this.rbtn_yes.Name = "rbtn_yes";
            this.rbtn_yes.Size = new System.Drawing.Size(41, 17);
            this.rbtn_yes.TabIndex = 16;
            this.rbtn_yes.TabStop = true;
            this.rbtn_yes.Text = "Oui";
            this.rbtn_yes.UseVisualStyleBackColor = true;
            // 
            // txt_heure_fin
            // 
            this.txt_heure_fin.Location = new System.Drawing.Point(666, 27);
            this.txt_heure_fin.Name = "txt_heure_fin";
            this.txt_heure_fin.ReadOnly = true;
            this.txt_heure_fin.Size = new System.Drawing.Size(114, 20);
            this.txt_heure_fin.TabIndex = 15;
            // 
            // txt_heure_debut
            // 
            this.txt_heure_debut.Location = new System.Drawing.Point(524, 27);
            this.txt_heure_debut.Name = "txt_heure_debut";
            this.txt_heure_debut.ReadOnly = true;
            this.txt_heure_debut.Size = new System.Drawing.Size(117, 20);
            this.txt_heure_debut.TabIndex = 14;
            // 
            // txt_date_fin
            // 
            this.txt_date_fin.Location = new System.Drawing.Point(666, 3);
            this.txt_date_fin.Name = "txt_date_fin";
            this.txt_date_fin.ReadOnly = true;
            this.txt_date_fin.Size = new System.Drawing.Size(114, 20);
            this.txt_date_fin.TabIndex = 13;
            // 
            // txt_date_debut
            // 
            this.txt_date_debut.Location = new System.Drawing.Point(524, 3);
            this.txt_date_debut.Name = "txt_date_debut";
            this.txt_date_debut.ReadOnly = true;
            this.txt_date_debut.Size = new System.Drawing.Size(117, 20);
            this.txt_date_debut.TabIndex = 12;
            // 
            // grp_total
            // 
            this.grp_total.Controls.Add(this.txt_total_suppl);
            this.grp_total.Controls.Add(this.txt_total_presence);
            this.grp_total.Controls.Add(this.label9);
            this.grp_total.Controls.Add(this.label10);
            this.grp_total.Location = new System.Drawing.Point(470, 79);
            this.grp_total.Name = "grp_total";
            this.grp_total.Size = new System.Drawing.Size(310, 48);
            this.grp_total.TabIndex = 11;
            this.grp_total.TabStop = false;
            this.grp_total.Text = "Total";
            // 
            // txt_total_suppl
            // 
            this.txt_total_suppl.Location = new System.Drawing.Point(237, 17);
            this.txt_total_suppl.Name = "txt_total_suppl";
            this.txt_total_suppl.ReadOnly = true;
            this.txt_total_suppl.Size = new System.Drawing.Size(67, 20);
            this.txt_total_suppl.TabIndex = 12;
            this.txt_total_suppl.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_total_presence
            // 
            this.txt_total_presence.Location = new System.Drawing.Point(74, 17);
            this.txt_total_presence.Name = "txt_total_presence";
            this.txt_total_presence.ReadOnly = true;
            this.txt_total_presence.Size = new System.Drawing.Size(65, 20);
            this.txt_total_presence.TabIndex = 11;
            this.txt_total_presence.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = " Présence : ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(145, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Supplémentaire :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(467, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Valider :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(467, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Heures : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(467, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Période : ";
            // 
            // txt_id
            // 
            this.txt_id.Location = new System.Drawing.Point(244, 3);
            this.txt_id.Name = "txt_id";
            this.txt_id.ReadOnly = true;
            this.txt_id.Size = new System.Drawing.Size(100, 20);
            this.txt_id.TabIndex = 9;
            // 
            // txt_poste
            // 
            this.txt_poste.Location = new System.Drawing.Point(244, 105);
            this.txt_poste.Name = "txt_poste";
            this.txt_poste.ReadOnly = true;
            this.txt_poste.Size = new System.Drawing.Size(193, 20);
            this.txt_poste.TabIndex = 8;
            // 
            // txt_matricule
            // 
            this.txt_matricule.Location = new System.Drawing.Point(244, 79);
            this.txt_matricule.Name = "txt_matricule";
            this.txt_matricule.ReadOnly = true;
            this.txt_matricule.Size = new System.Drawing.Size(100, 20);
            this.txt_matricule.TabIndex = 7;
            // 
            // txt_prenom
            // 
            this.txt_prenom.Location = new System.Drawing.Point(244, 53);
            this.txt_prenom.Name = "txt_prenom";
            this.txt_prenom.ReadOnly = true;
            this.txt_prenom.Size = new System.Drawing.Size(193, 20);
            this.txt_prenom.TabIndex = 5;
            // 
            // txt_nom
            // 
            this.txt_nom.Location = new System.Drawing.Point(244, 27);
            this.txt_nom.Name = "txt_nom";
            this.txt_nom.ReadOnly = true;
            this.txt_nom.Size = new System.Drawing.Size(193, 20);
            this.txt_nom.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(165, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Poste Actuel :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(165, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Matricule : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(165, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Prénoms :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(165, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Noms : ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(644, 30);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(13, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "à";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(644, 6);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(19, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "au";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(165, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "ID : ";
            // 
            // grp_list_time
            // 
            this.grp_list_time.Controls.Add(this.dgv_pointage);
            this.grp_list_time.Location = new System.Drawing.Point(34, 124);
            this.grp_list_time.Name = "grp_list_time";
            this.grp_list_time.Size = new System.Drawing.Size(746, 186);
            this.grp_list_time.TabIndex = 2;
            this.grp_list_time.TabStop = false;
            this.grp_list_time.Text = "Heure Présence";
            // 
            // dgv_pointage
            // 
            this.dgv_pointage.AllowUserToAddRows = false;
            this.dgv_pointage.AllowUserToDeleteRows = false;
            this.dgv_pointage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_pointage.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgv_pointage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_pointage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.cpt_po,
            this.heure_entree,
            this.heure_sortie,
            this.duree,
            this.valider,
            this.supp});
            this.dgv_pointage.ContextMenuStrip = this.context_pointage;
            this.dgv_pointage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_pointage.Location = new System.Drawing.Point(3, 16);
            this.dgv_pointage.Name = "dgv_pointage";
            this.dgv_pointage.ReadOnly = true;
            this.dgv_pointage.RowHeadersVisible = false;
            this.dgv_pointage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_pointage.Size = new System.Drawing.Size(740, 167);
            this.dgv_pointage.TabIndex = 0;
            this.dgv_pointage.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_pointage_CellFormatting);
            // 
            // id
            // 
            this.id.HeaderText = "";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // cpt_po
            // 
            this.cpt_po.FillWeight = 40.90782F;
            this.cpt_po.HeaderText = "";
            this.cpt_po.Name = "cpt_po";
            this.cpt_po.ReadOnly = true;
            // 
            // heure_entree
            // 
            this.heure_entree.FillWeight = 223.9961F;
            this.heure_entree.HeaderText = "Heure Entrée";
            this.heure_entree.Name = "heure_entree";
            this.heure_entree.ReadOnly = true;
            // 
            // heure_sortie
            // 
            this.heure_sortie.FillWeight = 223.9961F;
            this.heure_sortie.HeaderText = "Heure Sortie";
            this.heure_sortie.Name = "heure_sortie";
            this.heure_sortie.ReadOnly = true;
            // 
            // duree
            // 
            this.duree.HeaderText = "Durée";
            this.duree.Name = "duree";
            this.duree.ReadOnly = true;
            // 
            // valider
            // 
            this.valider.FillWeight = 40.3619F;
            this.valider.HeaderText = "Val.";
            this.valider.Name = "valider";
            this.valider.ReadOnly = true;
            // 
            // supp
            // 
            this.supp.FillWeight = 40.28119F;
            this.supp.HeaderText = "Sup.";
            this.supp.Name = "supp";
            this.supp.ReadOnly = true;
            // 
            // context_pointage
            // 
            this.context_pointage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.synchroniserToolStripMenuItem,
            this.actualiserToolStripMenuItem,
            this.voirLesHeuresPrévuesToolStripMenuItem,
            this.reorganiserToolStripMenuItem,
            this.reevaluerToolStripMenuItem,
            this.fusionnerToolStripMenuItem});
            this.context_pointage.Name = "context_pointage";
            this.context_pointage.Size = new System.Drawing.Size(195, 158);
            // 
            // synchroniserToolStripMenuItem
            // 
            this.synchroniserToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources._in;
            this.synchroniserToolStripMenuItem.Name = "synchroniserToolStripMenuItem";
            this.synchroniserToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.synchroniserToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.synchroniserToolStripMenuItem.Text = "Synchroniser";
            this.synchroniserToolStripMenuItem.Click += new System.EventHandler(this.synchroniserToolStripMenuItem_Click);
            // 
            // actualiserToolStripMenuItem
            // 
            this.actualiserToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.connected;
            this.actualiserToolStripMenuItem.Name = "actualiserToolStripMenuItem";
            this.actualiserToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.actualiserToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.actualiserToolStripMenuItem.Text = "Actualiser";
            this.actualiserToolStripMenuItem.Click += new System.EventHandler(this.actualiserToolStripMenuItem_Click);
            // 
            // reorganiserToolStripMenuItem
            // 
            this.reorganiserToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.add;
            this.reorganiserToolStripMenuItem.Name = "reorganiserToolStripMenuItem";
            this.reorganiserToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.reorganiserToolStripMenuItem.Text = "Reorganiser";
            this.reorganiserToolStripMenuItem.Click += new System.EventHandler(this.reorganiserToolStripMenuItem_Click);
            // 
            // reevaluerToolStripMenuItem
            // 
            this.reevaluerToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.db;
            this.reevaluerToolStripMenuItem.Name = "reevaluerToolStripMenuItem";
            this.reevaluerToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.reevaluerToolStripMenuItem.Text = "Reevaluer";
            this.reevaluerToolStripMenuItem.Click += new System.EventHandler(this.reevaluerToolStripMenuItem_Click);
            // 
            // fusionnerToolStripMenuItem
            // 
            this.fusionnerToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.famille;
            this.fusionnerToolStripMenuItem.Name = "fusionnerToolStripMenuItem";
            this.fusionnerToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.fusionnerToolStripMenuItem.Text = "Fusionner";
            this.fusionnerToolStripMenuItem.Click += new System.EventHandler(this.fusionnerToolStripMenuItem_Click);
            // 
            // box_identity
            // 
            this.box_identity.BackColor = System.Drawing.SystemColors.Window;
            this.box_identity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.box_identity.Image = global::ZK_Lymytz.Properties.Resources.contact;
            this.box_identity.Location = new System.Drawing.Point(34, 5);
            this.box_identity.Name = "box_identity";
            this.box_identity.Size = new System.Drawing.Size(125, 114);
            this.box_identity.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.box_identity.TabIndex = 1;
            this.box_identity.TabStop = false;
            // 
            // btn_next
            // 
            this.btn_next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_next.Image = global::ZK_Lymytz.Properties.Resources.next;
            this.btn_next.Location = new System.Drawing.Point(786, 3);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(25, 308);
            this.btn_next.TabIndex = 0;
            this.btn_next.UseVisualStyleBackColor = true;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // btn_prec
            // 
            this.btn_prec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_prec.Image = global::ZK_Lymytz.Properties.Resources.prec;
            this.btn_prec.Location = new System.Drawing.Point(3, 3);
            this.btn_prec.Name = "btn_prec";
            this.btn_prec.Size = new System.Drawing.Size(25, 308);
            this.btn_prec.TabIndex = 0;
            this.btn_prec.UseVisualStyleBackColor = true;
            this.btn_prec.Click += new System.EventHandler(this.btn_prec_Click);
            // 
            // txt_id_search
            // 
            this.txt_id_search.Location = new System.Drawing.Point(526, 3);
            this.txt_id_search.Name = "txt_id_search";
            this.txt_id_search.Size = new System.Drawing.Size(86, 20);
            this.txt_id_search.TabIndex = 4;
            this.txt_id_search.Text = "0";
            this.txt_id_search.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_id_search.Leave += new System.EventHandler(this.txt_id_search_Leave);
            // 
            // com_employe
            // 
            this.com_employe.FormattingEnabled = true;
            this.com_employe.Location = new System.Drawing.Point(618, 2);
            this.com_employe.Name = "com_employe";
            this.com_employe.Size = new System.Drawing.Size(196, 21);
            this.com_employe.TabIndex = 5;
            this.com_employe.SelectedIndexChanged += new System.EventHandler(this.com_employe_SelectedIndexChanged);
            // 
            // lb_pagination
            // 
            this.lb_pagination.AutoSize = true;
            this.lb_pagination.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_pagination.Location = new System.Drawing.Point(741, 348);
            this.lb_pagination.Name = "lb_pagination";
            this.lb_pagination.Size = new System.Drawing.Size(34, 13);
            this.lb_pagination.TabIndex = 6;
            this.lb_pagination.Text = "1/56";
            // 
            // txt_index_of
            // 
            this.txt_index_of.Location = new System.Drawing.Point(782, 345);
            this.txt_index_of.Name = "txt_index_of";
            this.txt_index_of.Size = new System.Drawing.Size(35, 20);
            this.txt_index_of.TabIndex = 8;
            this.txt_index_of.Text = "1";
            this.txt_index_of.Leave += new System.EventHandler(this.txt_index_of_Leave);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pbar_statut);
            this.panel2.Location = new System.Drawing.Point(2, 370);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(817, 19);
            this.panel2.TabIndex = 9;
            // 
            // pbar_statut
            // 
            this.pbar_statut.Location = new System.Drawing.Point(3, 4);
            this.pbar_statut.Maximum = 10000;
            this.pbar_statut.Name = "pbar_statut";
            this.pbar_statut.Size = new System.Drawing.Size(810, 12);
            this.pbar_statut.TabIndex = 0;
            // 
            // voirLesHeuresPrévuesToolStripMenuItem
            // 
            this.voirLesHeuresPrévuesToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.vue;
            this.voirLesHeuresPrévuesToolStripMenuItem.Name = "voirLesHeuresPrévuesToolStripMenuItem";
            this.voirLesHeuresPrévuesToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.voirLesHeuresPrévuesToolStripMenuItem.Text = "Voir les heures prévues";
            this.voirLesHeuresPrévuesToolStripMenuItem.Click += new System.EventHandler(this.voirLesHeuresPrévuesToolStripMenuItem_Click);
            // 
            // Form_Presence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 391);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.txt_index_of);
            this.Controls.Add(this.lb_pagination);
            this.Controls.Add(this.com_employe);
            this.Controls.Add(this.txt_id_search);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dtp_date);
            this.Controls.Add(this.lnk_today);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(837, 430);
            this.MinimumSize = new System.Drawing.Size(837, 430);
            this.Name = "Form_Presence";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion Présence";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Presence_FormClosed);
            this.Load += new System.EventHandler(this.Form_Presence_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.grp_total.ResumeLayout(false);
            this.grp_total.PerformLayout();
            this.grp_list_time.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pointage)).EndInit();
            this.context_pointage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.box_identity)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel lnk_today;
        private System.Windows.Forms.DateTimePicker dtp_date;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_prec;
        private System.Windows.Forms.Button btn_next;
        private System.Windows.Forms.GroupBox grp_list_time;
        private System.Windows.Forms.PictureBox box_identity;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_id;
        private System.Windows.Forms.TextBox txt_poste;
        private System.Windows.Forms.TextBox txt_matricule;
        private System.Windows.Forms.TextBox txt_prenom;
        private System.Windows.Forms.TextBox txt_nom;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox grp_total;
        private System.Windows.Forms.TextBox txt_heure_fin;
        private System.Windows.Forms.TextBox txt_heure_debut;
        private System.Windows.Forms.TextBox txt_date_fin;
        private System.Windows.Forms.TextBox txt_date_debut;
        private System.Windows.Forms.TextBox txt_total_suppl;
        private System.Windows.Forms.TextBox txt_total_presence;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton rbtn_no;
        private System.Windows.Forms.RadioButton rbtn_yes;
        private System.Windows.Forms.TextBox txt_id_search;
        private System.Windows.Forms.ComboBox com_employe;
        private System.Windows.Forms.DataGridView dgv_pointage;
        private System.Windows.Forms.Label lb_pagination;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn cpt_po;
        private System.Windows.Forms.DataGridViewTextBoxColumn heure_entree;
        private System.Windows.Forms.DataGridViewTextBoxColumn heure_sortie;
        private System.Windows.Forms.DataGridViewTextBoxColumn duree;
        private System.Windows.Forms.DataGridViewCheckBoxColumn valider;
        private System.Windows.Forms.DataGridViewCheckBoxColumn supp;
        private System.Windows.Forms.ContextMenuStrip context_pointage;
        private System.Windows.Forms.ToolStripMenuItem reorganiserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fusionnerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reevaluerToolStripMenuItem;
        private System.Windows.Forms.TextBox txt_index_of;
        private System.Windows.Forms.ToolStripMenuItem actualiserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem synchroniserToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ProgressBar pbar_statut;
        private System.Windows.Forms.ToolStripMenuItem voirLesHeuresPrévuesToolStripMenuItem;
    }
}