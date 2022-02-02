namespace ZK_Lymytz.IHM
{
    partial class Form_Societe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Societe));
            this.cbox_societe = new System.Windows.Forms.ComboBox();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.lb_name = new System.Windows.Forms.Label();
            this.btn_save = new System.Windows.Forms.Button();
            this.txt_adresse = new System.Windows.Forms.TextBox();
            this.lb_adresse = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_port = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbox_type_connexion = new System.Windows.Forms.ComboBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.txt_domain = new System.Windows.Forms.TextBox();
            this.txt_users = new System.Windows.Forms.TextBox();
            this.btn_tester = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_port)).BeginInit();
            this.SuspendLayout();
            // 
            // cbox_societe
            // 
            this.cbox_societe.FormattingEnabled = true;
            this.cbox_societe.Location = new System.Drawing.Point(12, 12);
            this.cbox_societe.Name = "cbox_societe";
            this.cbox_societe.Size = new System.Drawing.Size(349, 21);
            this.cbox_societe.TabIndex = 0;
            this.cbox_societe.SelectedIndexChanged += new System.EventHandler(this.cbox_societe_SelectedIndexChanged);
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(94, 39);
            this.txt_name.Name = "txt_name";
            this.txt_name.ReadOnly = true;
            this.txt_name.Size = new System.Drawing.Size(267, 20);
            this.txt_name.TabIndex = 100;
            // 
            // lb_name
            // 
            this.lb_name.AutoSize = true;
            this.lb_name.Location = new System.Drawing.Point(9, 42);
            this.lb_name.Name = "lb_name";
            this.lb_name.Size = new System.Drawing.Size(79, 13);
            this.lb_name.TabIndex = 2;
            this.lb_name.Text = "Notre socièté : ";
            // 
            // btn_save
            // 
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.Location = new System.Drawing.Point(259, 248);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(102, 28);
            this.btn_save.TabIndex = 10;
            this.btn_save.Text = "Enregistrer";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // txt_adresse
            // 
            this.txt_adresse.Location = new System.Drawing.Point(94, 65);
            this.txt_adresse.Name = "txt_adresse";
            this.txt_adresse.Size = new System.Drawing.Size(267, 20);
            this.txt_adresse.TabIndex = 1;
            // 
            // lb_adresse
            // 
            this.lb_adresse.AutoSize = true;
            this.lb_adresse.Location = new System.Drawing.Point(9, 68);
            this.lb_adresse.Name = "lb_adresse";
            this.lb_adresse.Size = new System.Drawing.Size(82, 13);
            this.lb_adresse.TabIndex = 2;
            this.lb_adresse.Text = "Notre adresse : ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_port);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbox_type_connexion);
            this.groupBox1.Controls.Add(this.txt_password);
            this.groupBox1.Controls.Add(this.txt_domain);
            this.groupBox1.Controls.Add(this.txt_users);
            this.groupBox1.Location = new System.Drawing.Point(12, 91);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(349, 151);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Information de connexion distante";
            // 
            // txt_port
            // 
            this.txt_port.Location = new System.Drawing.Point(147, 43);
            this.txt_port.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txt_port.Name = "txt_port";
            this.txt_port.Size = new System.Drawing.Size(196, 20);
            this.txt_port.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(79, 124);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Password : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(79, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Domaine : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(79, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Users : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(79, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(79, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Type : ";
            // 
            // cbox_type_connexion
            // 
            this.cbox_type_connexion.FormattingEnabled = true;
            this.cbox_type_connexion.Items.AddRange(new object[] {
            "DESKTOP",
            "FTP",
            "SFTP"});
            this.cbox_type_connexion.Location = new System.Drawing.Point(147, 16);
            this.cbox_type_connexion.Name = "cbox_type_connexion";
            this.cbox_type_connexion.Size = new System.Drawing.Size(196, 21);
            this.cbox_type_connexion.TabIndex = 2;
            this.cbox_type_connexion.Text = "DESKTOP";
            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(147, 121);
            this.txt_password.Name = "txt_password";
            this.txt_password.Size = new System.Drawing.Size(196, 20);
            this.txt_password.TabIndex = 6;
            this.txt_password.UseSystemPasswordChar = true;
            // 
            // txt_domain
            // 
            this.txt_domain.Location = new System.Drawing.Point(147, 95);
            this.txt_domain.Name = "txt_domain";
            this.txt_domain.Size = new System.Drawing.Size(196, 20);
            this.txt_domain.TabIndex = 5;
            // 
            // txt_users
            // 
            this.txt_users.Location = new System.Drawing.Point(147, 69);
            this.txt_users.Name = "txt_users";
            this.txt_users.Size = new System.Drawing.Size(196, 20);
            this.txt_users.TabIndex = 4;
            // 
            // btn_tester
            // 
            this.btn_tester.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_tester.Location = new System.Drawing.Point(151, 248);
            this.btn_tester.Name = "btn_tester";
            this.btn_tester.Size = new System.Drawing.Size(102, 28);
            this.btn_tester.TabIndex = 101;
            this.btn_tester.Text = "Tester";
            this.btn_tester.UseVisualStyleBackColor = true;
            this.btn_tester.Click += new System.EventHandler(this.btn_tester_Click);
            // 
            // Form_Societe
            // 
            this.AcceptButton = this.btn_save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 281);
            this.Controls.Add(this.btn_tester);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.lb_adresse);
            this.Controls.Add(this.lb_name);
            this.Controls.Add(this.txt_adresse);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.cbox_societe);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(383, 136);
            this.Name = "Form_Societe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Socièté";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Societe_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Societe_FormClosed);
            this.Load += new System.EventHandler(this.Form_Societe_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_port)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbox_societe;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Label lb_name;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.TextBox txt_adresse;
        private System.Windows.Forms.Label lb_adresse;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbox_type_connexion;
        private System.Windows.Forms.NumericUpDown txt_port;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.TextBox txt_domain;
        private System.Windows.Forms.TextBox txt_users;
        private System.Windows.Forms.Button btn_tester;
    }
}