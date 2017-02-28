namespace ZK_Lymytz.IHM
{
    partial class Form_Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Login));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_view = new System.Windows.Forms.Button();
            this.txt_pwd = new System.Windows.Forms.TextBox();
            this.txt_id = new System.Windows.Forms.TextBox();
            this.lb_password = new System.Windows.Forms.Label();
            this.lb_identifian = new System.Windows.Forms.Label();
            this.lb_connexion = new System.Windows.Forms.Label();
            this.btn_annuler = new System.Windows.Forms.Button();
            this.btn_connecter = new System.Windows.Forms.Button();
            this.temps = new System.Windows.Forms.Label();
            this.p_bar = new System.Windows.Forms.ProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.btn_view);
            this.panel1.Controls.Add(this.txt_pwd);
            this.panel1.Controls.Add(this.txt_id);
            this.panel1.Controls.Add(this.lb_password);
            this.panel1.Controls.Add(this.lb_identifian);
            this.panel1.Controls.Add(this.lb_connexion);
            this.panel1.Controls.Add(this.btn_annuler);
            this.panel1.Controls.Add(this.btn_connecter);
            this.panel1.Location = new System.Drawing.Point(152, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(366, 185);
            this.panel1.TabIndex = 9;
            // 
            // btn_view
            // 
            this.btn_view.BackColor = System.Drawing.Color.Transparent;
            this.btn_view.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_view.Image = ((System.Drawing.Image)(resources.GetObject("btn_view.Image")));
            this.btn_view.Location = new System.Drawing.Point(321, 92);
            this.btn_view.Name = "btn_view";
            this.btn_view.Size = new System.Drawing.Size(35, 23);
            this.btn_view.TabIndex = 9;
            this.btn_view.UseVisualStyleBackColor = false;
            this.btn_view.Click += new System.EventHandler(this.btn_view_Click);
            // 
            // txt_pwd
            // 
            this.txt_pwd.Location = new System.Drawing.Point(127, 94);
            this.txt_pwd.Name = "txt_pwd";
            this.txt_pwd.PasswordChar = '*';
            this.txt_pwd.Size = new System.Drawing.Size(188, 20);
            this.txt_pwd.TabIndex = 7;
            // 
            // txt_id
            // 
            this.txt_id.Location = new System.Drawing.Point(129, 61);
            this.txt_id.Name = "txt_id";
            this.txt_id.Size = new System.Drawing.Size(227, 20);
            this.txt_id.TabIndex = 6;
            // 
            // lb_password
            // 
            this.lb_password.AutoSize = true;
            this.lb_password.Location = new System.Drawing.Point(13, 97);
            this.lb_password.Name = "lb_password";
            this.lb_password.Size = new System.Drawing.Size(108, 13);
            this.lb_password.TabIndex = 5;
            this.lb_password.Text = "Votre Mot de passe : ";
            // 
            // lb_identifian
            // 
            this.lb_identifian.AutoSize = true;
            this.lb_identifian.Location = new System.Drawing.Point(13, 61);
            this.lb_identifian.Name = "lb_identifian";
            this.lb_identifian.Size = new System.Drawing.Size(90, 13);
            this.lb_identifian.TabIndex = 4;
            this.lb_identifian.Text = "Votre Identifiant : ";
            // 
            // lb_connexion
            // 
            this.lb_connexion.AutoSize = true;
            this.lb_connexion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_connexion.Font = new System.Drawing.Font("Algerian", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_connexion.Location = new System.Drawing.Point(119, 8);
            this.lb_connexion.Name = "lb_connexion";
            this.lb_connexion.Size = new System.Drawing.Size(157, 32);
            this.lb_connexion.TabIndex = 3;
            this.lb_connexion.Text = "Connexion";
            // 
            // btn_annuler
            // 
            this.btn_annuler.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_annuler.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_annuler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_annuler.Image = global::ZK_Lymytz.Properties.Resources.cancel;
            this.btn_annuler.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_annuler.Location = new System.Drawing.Point(127, 135);
            this.btn_annuler.Name = "btn_annuler";
            this.btn_annuler.Size = new System.Drawing.Size(99, 31);
            this.btn_annuler.TabIndex = 1;
            this.btn_annuler.Text = "Sortir";
            this.btn_annuler.UseVisualStyleBackColor = true;
            this.btn_annuler.Click += new System.EventHandler(this.btn_annuler_Click);
            // 
            // btn_connecter
            // 
            this.btn_connecter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_connecter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_connecter.Image = global::ZK_Lymytz.Properties.Resources.connected;
            this.btn_connecter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_connecter.Location = new System.Drawing.Point(232, 135);
            this.btn_connecter.Name = "btn_connecter";
            this.btn_connecter.Size = new System.Drawing.Size(124, 31);
            this.btn_connecter.TabIndex = 0;
            this.btn_connecter.Text = "Se connecter";
            this.btn_connecter.UseVisualStyleBackColor = true;
            this.btn_connecter.Click += new System.EventHandler(this.btn_connecter_Click);
            // 
            // temps
            // 
            this.temps.AutoSize = true;
            this.temps.BackColor = System.Drawing.Color.Transparent;
            this.temps.ForeColor = System.Drawing.Color.White;
            this.temps.Location = new System.Drawing.Point(4, 151);
            this.temps.Name = "temps";
            this.temps.Size = new System.Drawing.Size(92, 13);
            this.temps.TabIndex = 12;
            this.temps.Text = "Temps Connexion";
            // 
            // p_bar
            // 
            this.p_bar.Location = new System.Drawing.Point(7, 179);
            this.p_bar.Maximum = 5;
            this.p_bar.Name = "p_bar";
            this.p_bar.Size = new System.Drawing.Size(133, 13);
            this.p_bar.TabIndex = 11;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ZK_Lymytz.Properties.Resources.acces;
            this.pictureBox1.Location = new System.Drawing.Point(7, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(135, 131);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // Form_Login
            // 
            this.AcceptButton = this.btn_connecter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.CancelButton = this.btn_annuler;
            this.ClientSize = new System.Drawing.Size(522, 198);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.temps);
            this.Controls.Add(this.p_bar);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(560, 327);
            this.Name = "Form_Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connexion";
            this.Load += new System.EventHandler(this.Form_Login_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_pwd;
        public System.Windows.Forms.TextBox txt_id;
        private System.Windows.Forms.Label lb_password;
        private System.Windows.Forms.Label lb_identifian;
        private System.Windows.Forms.Label lb_connexion;
        private System.Windows.Forms.Button btn_annuler;
        private System.Windows.Forms.Button btn_connecter;
        private System.Windows.Forms.Label temps;
        private System.Windows.Forms.ProgressBar p_bar;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_view;
    }
}