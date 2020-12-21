namespace ZK_Lymytz.IHM
{
    partial class Form_Users
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Users));
            this.btn_save = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_domain = new System.Windows.Forms.TextBox();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.txt_password_pc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_password_log = new System.Windows.Forms.TextBox();
            this.btn_pass = new System.Windows.Forms.Button();
            this.grp_local_users = new System.Windows.Forms.GroupBox();
            this.grp_users_distant = new System.Windows.Forms.GroupBox();
            this.com_agence = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.com_societe = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_login = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.grp_local_users.SuspendLayout();
            this.grp_users_distant.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_save
            // 
            this.btn_save.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.Location = new System.Drawing.Point(346, 271);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(87, 24);
            this.btn_save.TabIndex = 0;
            this.btn_save.Text = "Se Connecter";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nom : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password Ordinateur : ";
            // 
            // txt_domain
            // 
            this.txt_domain.Location = new System.Drawing.Point(140, 18);
            this.txt_domain.Name = "txt_domain";
            this.txt_domain.ReadOnly = true;
            this.txt_domain.Size = new System.Drawing.Size(100, 20);
            this.txt_domain.TabIndex = 3;
            this.txt_domain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(250, 18);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(174, 20);
            this.txt_name.TabIndex = 5;
            // 
            // txt_password_pc
            // 
            this.txt_password_pc.Location = new System.Drawing.Point(140, 49);
            this.txt_password_pc.Name = "txt_password_pc";
            this.txt_password_pc.Size = new System.Drawing.Size(284, 20);
            this.txt_password_pc.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(239, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "\\";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Password Logiciel : ";
            // 
            // txt_password_log
            // 
            this.txt_password_log.Location = new System.Drawing.Point(140, 80);
            this.txt_password_log.Name = "txt_password_log";
            this.txt_password_log.Size = new System.Drawing.Size(284, 20);
            this.txt_password_log.TabIndex = 7;
            // 
            // btn_pass
            // 
            this.btn_pass.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_pass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_pass.Location = new System.Drawing.Point(253, 271);
            this.btn_pass.Name = "btn_pass";
            this.btn_pass.Size = new System.Drawing.Size(87, 24);
            this.btn_pass.TabIndex = 7;
            this.btn_pass.Text = "Sortir";
            this.btn_pass.UseVisualStyleBackColor = true;
            this.btn_pass.Click += new System.EventHandler(this.btn_pass_Click);
            // 
            // grp_local_users
            // 
            this.grp_local_users.Controls.Add(this.txt_password_log);
            this.grp_local_users.Controls.Add(this.label1);
            this.grp_local_users.Controls.Add(this.label3);
            this.grp_local_users.Controls.Add(this.txt_password_pc);
            this.grp_local_users.Controls.Add(this.label2);
            this.grp_local_users.Controls.Add(this.txt_name);
            this.grp_local_users.Controls.Add(this.txt_domain);
            this.grp_local_users.Controls.Add(this.label4);
            this.grp_local_users.Location = new System.Drawing.Point(3, 141);
            this.grp_local_users.Name = "grp_local_users";
            this.grp_local_users.Size = new System.Drawing.Size(430, 113);
            this.grp_local_users.TabIndex = 8;
            this.grp_local_users.TabStop = false;
            this.grp_local_users.Text = "Utilisateur Local";
            // 
            // grp_users_distant
            // 
            this.grp_users_distant.Controls.Add(this.com_agence);
            this.grp_users_distant.Controls.Add(this.label8);
            this.grp_users_distant.Controls.Add(this.com_societe);
            this.grp_users_distant.Controls.Add(this.label7);
            this.grp_users_distant.Controls.Add(this.txt_login);
            this.grp_users_distant.Controls.Add(this.label5);
            this.grp_users_distant.Location = new System.Drawing.Point(3, 2);
            this.grp_users_distant.Name = "grp_users_distant";
            this.grp_users_distant.Size = new System.Drawing.Size(430, 115);
            this.grp_users_distant.TabIndex = 9;
            this.grp_users_distant.TabStop = false;
            this.grp_users_distant.Text = "Connexion";
            // 
            // com_agence
            // 
            this.com_agence.Enabled = false;
            this.com_agence.FormattingEnabled = true;
            this.com_agence.Location = new System.Drawing.Point(140, 84);
            this.com_agence.Name = "com_agence";
            this.com_agence.Size = new System.Drawing.Size(284, 21);
            this.com_agence.TabIndex = 4;
            this.com_agence.SelectedIndexChanged += new System.EventHandler(this.com_agence_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 87);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Agence : ";
            // 
            // com_societe
            // 
            this.com_societe.Enabled = false;
            this.com_societe.FormattingEnabled = true;
            this.com_societe.Location = new System.Drawing.Point(140, 51);
            this.com_societe.Name = "com_societe";
            this.com_societe.Size = new System.Drawing.Size(284, 21);
            this.com_societe.TabIndex = 3;
            this.com_societe.SelectedIndexChanged += new System.EventHandler(this.com_societe_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Socièté : ";
            // 
            // txt_login
            // 
            this.txt_login.Location = new System.Drawing.Point(140, 19);
            this.txt_login.Name = "txt_login";
            this.txt_login.Size = new System.Drawing.Size(284, 20);
            this.txt_login.TabIndex = 1;
            this.txt_login.Leave += new System.EventHandler(this.txt_login_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Identifiant : ";
            // 
            // Form_Users
            // 
            this.AcceptButton = this.btn_save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_pass;
            this.ClientSize = new System.Drawing.Size(437, 304);
            this.Controls.Add(this.grp_users_distant);
            this.Controls.Add(this.grp_local_users);
            this.Controls.Add(this.btn_pass);
            this.Controls.Add(this.btn_save);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(453, 343);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(453, 343);
            this.Name = "Form_Users";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Utilisateur";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Users_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Users_FormClosed);
            this.Load += new System.EventHandler(this.Form_Users_Load);
            this.grp_local_users.ResumeLayout(false);
            this.grp_local_users.PerformLayout();
            this.grp_users_distant.ResumeLayout(false);
            this.grp_users_distant.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_domain;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.TextBox txt_password_pc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_password_log;
        private System.Windows.Forms.Button btn_pass;
        private System.Windows.Forms.GroupBox grp_local_users;
        private System.Windows.Forms.GroupBox grp_users_distant;
        private System.Windows.Forms.TextBox txt_login;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox com_societe;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox com_agence;
        private System.Windows.Forms.Label label8;
    }
}