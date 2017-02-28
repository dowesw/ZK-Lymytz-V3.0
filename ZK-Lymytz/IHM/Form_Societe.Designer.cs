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
            this.txt_name.TabIndex = 1;
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
            this.btn_save.Location = new System.Drawing.Point(259, 65);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(102, 28);
            this.btn_save.TabIndex = 3;
            this.btn_save.Text = "Enregistrer";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // Form_Societe
            // 
            this.AcceptButton = this.btn_save;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 97);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.lb_name);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.cbox_societe);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(383, 136);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(383, 136);
            this.Name = "Form_Societe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Socièté";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Societe_FormClosing);
            this.Load += new System.EventHandler(this.Form_Societe_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbox_societe;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Label lb_name;
        private System.Windows.Forms.Button btn_save;
    }
}