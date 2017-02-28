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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Employe));
            this.grp_employe = new System.Windows.Forms.GroupBox();
            this.dgv_employe = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supp_ = new System.Windows.Forms.DataGridViewLinkColumn();
            this.grp_empreinte = new System.Windows.Forms.GroupBox();
            this.dgv_empreinte = new System.Windows.Forms.DataGridView();
            this.finger = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.main = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.doigt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.supp = new System.Windows.Forms.DataGridViewLinkColumn();
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
            this.txt_names = new System.Windows.Forms.TextBox();
            this.box_connect = new System.Windows.Forms.PictureBox();
            this.btn_change_fct = new System.Windows.Forms.Button();
            this.grp_employe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_employe)).BeginInit();
            this.grp_empreinte.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_empreinte)).BeginInit();
            this.grp_infos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.box_identity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.box_connect)).BeginInit();
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
            this.supp_});
            this.dgv_employe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_employe.Location = new System.Drawing.Point(3, 16);
            this.dgv_employe.Name = "dgv_employe";
            this.dgv_employe.ReadOnly = true;
            this.dgv_employe.RowHeadersVisible = false;
            this.dgv_employe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_employe.Size = new System.Drawing.Size(314, 437);
            this.dgv_employe.TabIndex = 1;
            this.dgv_employe.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_employe_CellContentClick);
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
            // grp_empreinte
            // 
            this.grp_empreinte.Controls.Add(this.dgv_empreinte);
            this.grp_empreinte.Location = new System.Drawing.Point(323, 231);
            this.grp_empreinte.Name = "grp_empreinte";
            this.grp_empreinte.Size = new System.Drawing.Size(524, 192);
            this.grp_empreinte.TabIndex = 1;
            this.grp_empreinte.TabStop = false;
            this.grp_empreinte.Text = "Gestion Empreinte";
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
            this.dgv_empreinte.Location = new System.Drawing.Point(3, 16);
            this.dgv_empreinte.Name = "dgv_empreinte";
            this.dgv_empreinte.ReadOnly = true;
            this.dgv_empreinte.Size = new System.Drawing.Size(518, 173);
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
            this.grp_infos.Controls.Add(this.txt_names);
            this.grp_infos.Location = new System.Drawing.Point(323, 0);
            this.grp_infos.Name = "grp_infos";
            this.grp_infos.Size = new System.Drawing.Size(524, 225);
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
            this.chk_actif.Location = new System.Drawing.Point(123, 192);
            this.chk_actif.Name = "chk_actif";
            this.chk_actif.Size = new System.Drawing.Size(15, 14);
            this.chk_actif.TabIndex = 3;
            this.chk_actif.UseVisualStyleBackColor = true;
            // 
            // btn_update
            // 
            this.btn_update.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_update.Location = new System.Drawing.Point(433, 192);
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
            this.label4.Location = new System.Drawing.Point(16, 193);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Actif ";
            // 
            // lb_privilege
            // 
            this.lb_privilege.AutoSize = true;
            this.lb_privilege.Location = new System.Drawing.Point(16, 124);
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
            this.label1.Location = new System.Drawing.Point(16, 159);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Agence";
            // 
            // lb_password
            // 
            this.lb_password.AutoSize = true;
            this.lb_password.Location = new System.Drawing.Point(16, 91);
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
            this.lb_name.Location = new System.Drawing.Point(16, 59);
            this.lb_name.Name = "lb_name";
            this.lb_name.Size = new System.Drawing.Size(93, 13);
            this.lb_name.TabIndex = 1;
            this.lb_name.Text = "Noms et Prénoms ";
            // 
            // txt_agence
            // 
            this.txt_agence.Location = new System.Drawing.Point(123, 156);
            this.txt_agence.Name = "txt_agence";
            this.txt_agence.Size = new System.Drawing.Size(240, 20);
            this.txt_agence.TabIndex = 0;
            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(123, 88);
            this.txt_password.Name = "txt_password";
            this.txt_password.PasswordChar = '*';
            this.txt_password.Size = new System.Drawing.Size(188, 20);
            this.txt_password.TabIndex = 0;
            // 
            // txt_privilege
            // 
            this.txt_privilege.Location = new System.Drawing.Point(123, 121);
            this.txt_privilege.Name = "txt_privilege";
            this.txt_privilege.ReadOnly = true;
            this.txt_privilege.Size = new System.Drawing.Size(34, 20);
            this.txt_privilege.TabIndex = 0;
            // 
            // txt_names
            // 
            this.txt_names.Location = new System.Drawing.Point(123, 56);
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
            ((System.ComponentModel.ISupportInitialize)(this.dgv_empreinte)).EndInit();
            this.grp_infos.ResumeLayout(false);
            this.grp_infos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.box_identity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.box_connect)).EndInit();
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
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn nom;
        private System.Windows.Forms.DataGridViewLinkColumn supp_;
        private System.Windows.Forms.PictureBox box_connect;
        private System.Windows.Forms.PictureBox box_identity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_agence;
        private System.Windows.Forms.Button btn_change_fct;
    }
}