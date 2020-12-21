namespace ZK_Lymytz.IHM
{
    partial class Form_Employe
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Employe));
            this.grp_employe = new System.Windows.Forms.GroupBox();
            this.dgv_employe = new System.Windows.Forms.DataGridView();
            this.grp_empreinte = new System.Windows.Forms.GroupBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tab_digital = new System.Windows.Forms.TabPage();
            this.dgv_empreinte = new System.Windows.Forms.DataGridView();
            this.finger = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.main = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doigt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supp = new System.Windows.Forms.DataGridViewLinkColumn();
            this.tab_facial = new System.Windows.Forms.TabPage();
            this.grp_infos = new System.Windows.Forms.GroupBox();
            this.box_identity = new System.Windows.Forms.PictureBox();
            this.chk_actif = new System.Windows.Forms.CheckBox();
            this.btn_update = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lb_privilege = new System.Windows.Forms.Label();
            this.lb_id = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_password = new System.Windows.Forms.Label();
            this.txt_id = new System.Windows.Forms.TextBox();
            this.lb_name = new System.Windows.Forms.Label();
            this.txt_agence = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.txt_privilege = new System.Windows.Forms.TextBox();
            this.lb_nom_prenom = new System.Windows.Forms.TextBox();
            this.txt_names = new System.Windows.Forms.TextBox();
            this.box_connect = new System.Windows.Forms.PictureBox();
            this.btn_change_fct = new System.Windows.Forms.Button();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.icon = new System.Windows.Forms.DataGridViewImageColumn();
            this.supp_ = new System.Windows.Forms.DataGridViewLinkColumn();
            this.context_employe = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_defined_niveau = new System.Windows.Forms.ToolStripMenuItem();
            this.grp_employe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_employe)).BeginInit();
            this.grp_empreinte.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tab_digital.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_empreinte)).BeginInit();
            this.grp_infos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.box_identity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.box_connect)).BeginInit();
            this.context_employe.SuspendLayout();
            this.SuspendLayout();
            // 
            // grp_employe
            // 
            this.grp_employe.Controls.Add(this.dgv_employe);
            this.grp_employe.Dock = System.Windows.Forms.DockStyle.Left;
            this.grp_employe.Location = new System.Drawing.Point(0, 0);
            this.grp_employe.Name = "grp_employe";
            this.grp_employe.Size = new System.Drawing.Size(320, 456);
            this.grp_employe.TabIndex = 0;
            this.grp_employe.TabStop = false;
            this.grp_employe.Text = "Employes";
            // 
            // dgv_employe
            // 
            this.dgv_employe.AllowUserToAddRows = false;
            this.dgv_employe.AllowUserToDeleteRows = false;
            this.dgv_employe.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_employe.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgv_employe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_employe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.nom,
            this.icon,
            this.supp_});
            this.dgv_employe.ContextMenuStrip = this.context_employe;
            this.dgv_employe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_employe.Location = new System.Drawing.Point(3, 16);
            this.dgv_employe.Name = "dgv_employe";
            this.dgv_employe.ReadOnly = true;
            this.dgv_employe.RowHeadersVisible = false;
            this.dgv_employe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_employe.Size = new System.Drawing.Size(314, 437);
            this.dgv_employe.TabIndex = 1;
            this.dgv_employe.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_employe_CellContentClick);
            this.dgv_employe.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgv_employe_MouseDown);
            // 
            // grp_empreinte
            // 
            this.grp_empreinte.Controls.Add(this.tabControl);
            this.grp_empreinte.Location = new System.Drawing.Point(323, 187);
            this.grp_empreinte.Name = "grp_empreinte";
            this.grp_empreinte.Size = new System.Drawing.Size(524, 236);
            this.grp_empreinte.TabIndex = 1;
            this.grp_empreinte.TabStop = false;
            this.grp_empreinte.Text = "Gestion Empreinte";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tab_digital);
            this.tabControl.Controls.Add(this.tab_facial);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(3, 16);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(518, 217);
            this.tabControl.TabIndex = 6;
            // 
            // tab_digital
            // 
            this.tab_digital.Controls.Add(this.dgv_empreinte);
            this.tab_digital.Location = new System.Drawing.Point(4, 22);
            this.tab_digital.Name = "tab_digital";
            this.tab_digital.Padding = new System.Windows.Forms.Padding(3);
            this.tab_digital.Size = new System.Drawing.Size(510, 191);
            this.tab_digital.TabIndex = 0;
            this.tab_digital.Text = "Digitale";
            this.tab_digital.UseVisualStyleBackColor = true;
            // 
            // dgv_empreinte
            // 
            this.dgv_empreinte.AllowUserToAddRows = false;
            this.dgv_empreinte.AllowUserToDeleteRows = false;
            this.dgv_empreinte.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_empreinte.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgv_empreinte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_empreinte.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.finger,
            this.main,
            this.doigt,
            this.flag,
            this.supp});
            this.dgv_empreinte.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_empreinte.Location = new System.Drawing.Point(3, 3);
            this.dgv_empreinte.Name = "dgv_empreinte";
            this.dgv_empreinte.ReadOnly = true;
            this.dgv_empreinte.Size = new System.Drawing.Size(504, 185);
            this.dgv_empreinte.TabIndex = 0;
            this.dgv_empreinte.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_empreinte_CellContentClick);
            // 
            // finger
            // 
            this.finger.HeaderText = "Finger";
            this.finger.Name = "finger";
            this.finger.ReadOnly = true;
            this.finger.Visible = false;
            // 
            // main
            // 
            this.main.FillWeight = 156.6338F;
            this.main.HeaderText = "Main";
            this.main.Name = "main";
            this.main.ReadOnly = true;
            // 
            // doigt
            // 
            this.doigt.FillWeight = 156.6338F;
            this.doigt.HeaderText = "Doigt";
            this.doigt.Name = "doigt";
            this.doigt.ReadOnly = true;
            // 
            // flag
            // 
            this.flag.FillWeight = 56.27561F;
            this.flag.HeaderText = "Flag";
            this.flag.Name = "flag";
            this.flag.ReadOnly = true;
            // 
            // supp
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle2.NullValue = "X";
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.supp.DefaultCellStyle = dataGridViewCellStyle2;
            this.supp.FillWeight = 30.45685F;
            this.supp.HeaderText = "";
            this.supp.LinkColor = System.Drawing.Color.Red;
            this.supp.Name = "supp";
            this.supp.ReadOnly = true;
            // 
            // tab_facial
            // 
            this.tab_facial.Location = new System.Drawing.Point(4, 22);
            this.tab_facial.Name = "tab_facial";
            this.tab_facial.Padding = new System.Windows.Forms.Padding(3);
            this.tab_facial.Size = new System.Drawing.Size(510, 191);
            this.tab_facial.TabIndex = 1;
            this.tab_facial.Text = "Faciale";
            this.tab_facial.UseVisualStyleBackColor = true;
            // 
            // grp_infos
            // 
            this.grp_infos.Controls.Add(this.box_identity);
            this.grp_infos.Controls.Add(this.chk_actif);
            this.grp_infos.Controls.Add(this.btn_update);
            this.grp_infos.Controls.Add(this.label4);
            this.grp_infos.Controls.Add(this.lb_privilege);
            this.grp_infos.Controls.Add(this.lb_id);
            this.grp_infos.Controls.Add(this.label1);
            this.grp_infos.Controls.Add(this.lb_password);
            this.grp_infos.Controls.Add(this.txt_id);
            this.grp_infos.Controls.Add(this.lb_name);
            this.grp_infos.Controls.Add(this.txt_agence);
            this.grp_infos.Controls.Add(this.txt_password);
            this.grp_infos.Controls.Add(this.txt_privilege);
            this.grp_infos.Controls.Add(this.lb_nom_prenom);
            this.grp_infos.Controls.Add(this.txt_names);
            this.grp_infos.Location = new System.Drawing.Point(323, 0);
            this.grp_infos.Name = "grp_infos";
            this.grp_infos.Size = new System.Drawing.Size(524, 181);
            this.grp_infos.TabIndex = 1;
            this.grp_infos.TabStop = false;
            this.grp_infos.Text = "Gestion Information";
            // 
            // box_identity
            // 
            this.box_identity.BackColor = System.Drawing.SystemColors.Window;
            this.box_identity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.box_identity.Image = global::ZK_Lymytz.Properties.Resources.contact;
            this.box_identity.Location = new System.Drawing.Point(396, 16);
            this.box_identity.Name = "box_identity";
            this.box_identity.Size = new System.Drawing.Size(122, 121);
            this.box_identity.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.box_identity.TabIndex = 4;
            this.box_identity.TabStop = false;
            // 
            // chk_actif
            // 
            this.chk_actif.AutoSize = true;
            this.chk_actif.Location = new System.Drawing.Point(123, 152);
            this.chk_actif.Name = "chk_actif";
            this.chk_actif.Size = new System.Drawing.Size(15, 14);
            this.chk_actif.TabIndex = 3;
            this.chk_actif.UseVisualStyleBackColor = true;
            // 
            // btn_update
            // 
            this.btn_update.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_update.Location = new System.Drawing.Point(433, 146);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(85, 27);
            this.btn_update.TabIndex = 2;
            this.btn_update.Text = "Modifier";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Actif ";
            // 
            // lb_privilege
            // 
            this.lb_privilege.AutoSize = true;
            this.lb_privilege.Location = new System.Drawing.Point(16, 51);
            this.lb_privilege.Name = "lb_privilege";
            this.lb_privilege.Size = new System.Drawing.Size(47, 13);
            this.lb_privilege.TabIndex = 1;
            this.lb_privilege.Text = "Privilège";
            // 
            // lb_id
            // 
            this.lb_id.AutoSize = true;
            this.lb_id.Location = new System.Drawing.Point(16, 25);
            this.lb_id.Name = "lb_id";
            this.lb_id.Size = new System.Drawing.Size(18, 13);
            this.lb_id.TabIndex = 1;
            this.lb_id.Text = "ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Agence";
            // 
            // lb_password
            // 
            this.lb_password.AutoSize = true;
            this.lb_password.Location = new System.Drawing.Point(16, 103);
            this.lb_password.Name = "lb_password";
            this.lb_password.Size = new System.Drawing.Size(71, 13);
            this.lb_password.TabIndex = 1;
            this.lb_password.Text = "Mot de passe";
            // 
            // txt_id
            // 
            this.txt_id.Location = new System.Drawing.Point(123, 22);
            this.txt_id.Name = "txt_id";
            this.txt_id.ReadOnly = true;
            this.txt_id.Size = new System.Drawing.Size(100, 20);
            this.txt_id.TabIndex = 0;
            // 
            // lb_name
            // 
            this.lb_name.AutoSize = true;
            this.lb_name.Location = new System.Drawing.Point(16, 77);
            this.lb_name.Name = "lb_name";
            this.lb_name.Size = new System.Drawing.Size(93, 13);
            this.lb_name.TabIndex = 1;
            this.lb_name.Text = "Noms et Prénoms ";
            // 
            // txt_agence
            // 
            this.txt_agence.Location = new System.Drawing.Point(123, 126);
            this.txt_agence.Name = "txt_agence";
            this.txt_agence.Size = new System.Drawing.Size(240, 20);
            this.txt_agence.TabIndex = 0;
            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(123, 100);
            this.txt_password.Name = "txt_password";
            this.txt_password.PasswordChar = '*';
            this.txt_password.Size = new System.Drawing.Size(188, 20);
            this.txt_password.TabIndex = 0;
            // 
            // txt_privilege
            // 
            this.txt_privilege.Location = new System.Drawing.Point(123, 48);
            this.txt_privilege.Name = "txt_privilege";
            this.txt_privilege.ReadOnly = true;
            this.txt_privilege.Size = new System.Drawing.Size(34, 20);
            this.txt_privilege.TabIndex = 0;
            // 
            // lb_nom_prenom
            // 
            this.lb_nom_prenom.Location = new System.Drawing.Point(163, 48);
            this.lb_nom_prenom.Name = "lb_nom_prenom";
            this.lb_nom_prenom.ReadOnly = true;
            this.lb_nom_prenom.Size = new System.Drawing.Size(200, 20);
            this.lb_nom_prenom.TabIndex = 0;
            this.lb_nom_prenom.Text = "---";
            this.lb_nom_prenom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_names
            // 
            this.txt_names.Location = new System.Drawing.Point(123, 74);
            this.txt_names.Name = "txt_names";
            this.txt_names.Size = new System.Drawing.Size(240, 20);
            this.txt_names.TabIndex = 0;
            // 
            // box_connect
            // 
            this.box_connect.Image = global::ZK_Lymytz.Properties.Resources.unconnecte;
            this.box_connect.Location = new System.Drawing.Point(827, 433);
            this.box_connect.Name = "box_connect";
            this.box_connect.Size = new System.Drawing.Size(20, 20);
            this.box_connect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.box_connect.TabIndex = 5;
            this.box_connect.TabStop = false;
            // 
            // btn_change_fct
            // 
            this.btn_change_fct.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_change_fct.Location = new System.Drawing.Point(325, 426);
            this.btn_change_fct.Name = "btn_change_fct";
            this.btn_change_fct.Size = new System.Drawing.Size(155, 27);
            this.btn_change_fct.TabIndex = 2;
            this.btn_change_fct.Text = "Passer en mode ";
            this.btn_change_fct.UseVisualStyleBackColor = true;
            this.btn_change_fct.Click += new System.EventHandler(this.btn_change_fct_Click);
            // 
            // id
            // 
            this.id.FillWeight = 68.41196F;
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            // 
            // nom
            // 
            this.nom.FillWeight = 201.1312F;
            this.nom.HeaderText = "Noms & Prénoms";
            this.nom.Name = "nom";
            this.nom.ReadOnly = true;
            // 
            // icon
            // 
            this.icon.FillWeight = 30F;
            this.icon.HeaderText = "";
            this.icon.Name = "icon";
            this.icon.ReadOnly = true;
            // 
            // supp_
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle1.NullValue = "X";
            this.supp_.DefaultCellStyle = dataGridViewCellStyle1;
            this.supp_.FillWeight = 30.45686F;
            this.supp_.HeaderText = "";
            this.supp_.LinkColor = System.Drawing.Color.Red;
            this.supp_.Name = "supp_";
            this.supp_.ReadOnly = true;
            // 
            // context_employe
            // 
            this.context_employe.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_defined_niveau});
            this.context_employe.Name = "context_employe";
            this.context_employe.Size = new System.Drawing.Size(192, 26);
            // 
            // tsmi_defined_niveau
            // 
            this.tsmi_defined_niveau.Image = global::ZK_Lymytz.Properties.Resources.administrateur;
            this.tsmi_defined_niveau.Name = "tsmi_defined_niveau";
            this.tsmi_defined_niveau.Size = new System.Drawing.Size(191, 22);
            this.tsmi_defined_niveau.Text = "Définir Administrateur";
            this.tsmi_defined_niveau.Click += new System.EventHandler(this.tsmi_defined_niveau_Click);
            // 
            // Form_Employe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 456);
            this.Controls.Add(this.box_connect);
            this.Controls.Add(this.grp_infos);
            this.Controls.Add(this.btn_change_fct);
            this.Controls.Add(this.grp_empreinte);
            this.Controls.Add(this.grp_employe);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(866, 495);
            this.MinimumSize = new System.Drawing.Size(866, 495);
            this.Name = "Form_Employe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion Employe";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Employe_FormClosed);
            this.Load += new System.EventHandler(this.Form_Employe_Load);
            this.grp_employe.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_employe)).EndInit();
            this.grp_empreinte.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tab_digital.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_empreinte)).EndInit();
            this.grp_infos.ResumeLayout(false);
            this.grp_infos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.box_identity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.box_connect)).EndInit();
            this.context_employe.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_employe;
        private System.Windows.Forms.DataGridView dgv_employe;
        private System.Windows.Forms.GroupBox grp_empreinte;
        private System.Windows.Forms.GroupBox grp_infos;
        private System.Windows.Forms.DataGridView dgv_empreinte;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.TextBox txt_privilege;
        private System.Windows.Forms.TextBox txt_names;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lb_privilege;
        private System.Windows.Forms.Label lb_password;
        private System.Windows.Forms.Label lb_name;
        private System.Windows.Forms.Label lb_id;
        private System.Windows.Forms.TextBox txt_id;
        private System.Windows.Forms.CheckBox chk_actif;
        private System.Windows.Forms.DataGridViewTextBoxColumn finger;
        private System.Windows.Forms.DataGridViewTextBoxColumn main;
        private System.Windows.Forms.DataGridViewTextBoxColumn doigt;
        private System.Windows.Forms.DataGridViewTextBoxColumn flag;
        private System.Windows.Forms.DataGridViewLinkColumn supp;
        private System.Windows.Forms.PictureBox box_connect;
        private System.Windows.Forms.PictureBox box_identity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_agence;
        private System.Windows.Forms.Button btn_change_fct;
        private System.Windows.Forms.TextBox lb_nom_prenom;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tab_digital;
        private System.Windows.Forms.TabPage tab_facial;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn nom;
        private System.Windows.Forms.DataGridViewImageColumn icon;
        private System.Windows.Forms.DataGridViewLinkColumn supp_;
        private System.Windows.Forms.ContextMenuStrip context_employe;
        private System.Windows.Forms.ToolStripMenuItem tsmi_defined_niveau;
    }
}