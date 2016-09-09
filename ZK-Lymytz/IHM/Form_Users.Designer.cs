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
            this.SuspendLayout();
            // 
            // btn_save
            // 
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.Location = new System.Drawing.Point(346, 98);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(87, 24);
            this.btn_save.TabIndex = 0;
            this.btn_save.Text = "Enregistrer";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nom : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password Ordinateur : ";
            // 
            // txt_domain
            // 
            this.txt_domain.Location = new System.Drawing.Point(149, 6);
            this.txt_domain.Name = "txt_domain";
            this.txt_domain.ReadOnly = true;
            this.txt_domain.Size = new System.Drawing.Size(100, 20);
            this.txt_domain.TabIndex = 3;
            this.txt_domain.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(259, 6);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(174, 20);
            this.txt_name.TabIndex = 4;
            // 
            // txt_password_pc
            // 
            this.txt_password_pc.Location = new System.Drawing.Point(149, 37);
            this.txt_password_pc.Name = "txt_password_pc";
            this.txt_password_pc.Size = new System.Drawing.Size(284, 20);
            this.txt_password_pc.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(248, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "\\";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Password Logiciel : ";
            // 
            // txt_password_log
            // 
            this.txt_password_log.Location = new System.Drawing.Point(149, 68);
            this.txt_password_log.Name = "txt_password_log";
            this.txt_password_log.Size = new System.Drawing.Size(284, 20);
            this.txt_password_log.TabIndex = 6;
            // 
            // Form_Users
            // 
            this.AcceptButton = this.btn_save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 126);
            this.Controls.Add(this.txt_password_log);
            this.Controls.Add(this.txt_password_pc);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_domain);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_save);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(453, 165);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(453, 165);
            this.Name = "Form_Users";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Utilisateur";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Users_FormClosing);
            this.Load += new System.EventHandler(this.Form_Users_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}