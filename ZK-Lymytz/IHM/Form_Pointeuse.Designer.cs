namespace ZK_Lymytz.IHM
{
    partial class Form_Pointeuse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Pointeuse));
            this.btn_appliquer = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.com_type = new System.Windows.Forms.ComboBox();
            this.rbtn_multi = new System.Windows.Forms.CheckBox();
            this.rbtn_non = new System.Windows.Forms.RadioButton();
            this.rbtn_oui = new System.Windows.Forms.RadioButton();
            this.txt_emplacement = new System.Windows.Forms.RichTextBox();
            this.txt_description = new System.Windows.Forms.RichTextBox();
            this.txt_ip = new System.Windows.Forms.TextBox();
            this.txt_port = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbox_agence = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_appliquer
            // 
            this.btn_appliquer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_appliquer.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_appliquer.Image = global::ZK_Lymytz.Properties.Resources.save;
            this.btn_appliquer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_appliquer.Location = new System.Drawing.Point(365, 241);
            this.btn_appliquer.Name = "btn_appliquer";
            this.btn_appliquer.Size = new System.Drawing.Size(100, 29);
            this.btn_appliquer.TabIndex = 3;
            this.btn_appliquer.Text = "   &Save";
            this.btn_appliquer.UseVisualStyleBackColor = true;
            this.btn_appliquer.Click += new System.EventHandler(this.btn_appliquer_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbox_agence);
            this.groupBox1.Controls.Add(this.com_type);
            this.groupBox1.Controls.Add(this.rbtn_multi);
            this.groupBox1.Controls.Add(this.rbtn_non);
            this.groupBox1.Controls.Add(this.rbtn_oui);
            this.groupBox1.Controls.Add(this.txt_emplacement);
            this.groupBox1.Controls.Add(this.txt_description);
            this.groupBox1.Controls.Add(this.txt_ip);
            this.groupBox1.Controls.Add(this.txt_port);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(4, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(461, 235);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informations";
            // 
            // com_type
            // 
            this.com_type.FormattingEnabled = true;
            this.com_type.Items.AddRange(new object[] {
            "TFT",
            "IFACE",
            "B&W"});
            this.com_type.Location = new System.Drawing.Point(89, 172);
            this.com_type.Name = "com_type";
            this.com_type.Size = new System.Drawing.Size(140, 23);
            this.com_type.TabIndex = 13;
            // 
            // rbtn_multi
            // 
            this.rbtn_multi.AutoSize = true;
            this.rbtn_multi.Checked = true;
            this.rbtn_multi.CheckState = System.Windows.Forms.CheckState.Checked;
            this.rbtn_multi.Location = new System.Drawing.Point(89, 210);
            this.rbtn_multi.Name = "rbtn_multi";
            this.rbtn_multi.Size = new System.Drawing.Size(108, 19);
            this.rbtn_multi.TabIndex = 12;
            this.rbtn_multi.Text = "Multi Agence ?";
            this.rbtn_multi.UseVisualStyleBackColor = true;
            this.rbtn_multi.CheckedChanged += new System.EventHandler(this.rbtn_multi_CheckedChanged);
            // 
            // rbtn_non
            // 
            this.rbtn_non.AutoSize = true;
            this.rbtn_non.Location = new System.Drawing.Point(402, 175);
            this.rbtn_non.Name = "rbtn_non";
            this.rbtn_non.Size = new System.Drawing.Size(48, 19);
            this.rbtn_non.TabIndex = 11;
            this.rbtn_non.TabStop = true;
            this.rbtn_non.Text = "Non";
            this.rbtn_non.UseVisualStyleBackColor = true;
            this.rbtn_non.CheckedChanged += new System.EventHandler(this.rbtn_non_CheckedChanged);
            // 
            // rbtn_oui
            // 
            this.rbtn_oui.AutoSize = true;
            this.rbtn_oui.Location = new System.Drawing.Point(352, 175);
            this.rbtn_oui.Name = "rbtn_oui";
            this.rbtn_oui.Size = new System.Drawing.Size(44, 19);
            this.rbtn_oui.TabIndex = 11;
            this.rbtn_oui.TabStop = true;
            this.rbtn_oui.Text = "Oui";
            this.rbtn_oui.UseVisualStyleBackColor = true;
            this.rbtn_oui.CheckedChanged += new System.EventHandler(this.rbtn_oui_CheckedChanged);
            // 
            // txt_emplacement
            // 
            this.txt_emplacement.Location = new System.Drawing.Point(89, 49);
            this.txt_emplacement.Name = "txt_emplacement";
            this.txt_emplacement.Size = new System.Drawing.Size(362, 53);
            this.txt_emplacement.TabIndex = 9;
            this.txt_emplacement.Text = "";
            // 
            // txt_description
            // 
            this.txt_description.Location = new System.Drawing.Point(89, 107);
            this.txt_description.Name = "txt_description";
            this.txt_description.Size = new System.Drawing.Size(361, 59);
            this.txt_description.TabIndex = 10;
            this.txt_description.Text = "";
            // 
            // txt_ip
            // 
            this.txt_ip.Location = new System.Drawing.Point(92, 21);
            this.txt_ip.Name = "txt_ip";
            this.txt_ip.Size = new System.Drawing.Size(137, 22);
            this.txt_ip.TabIndex = 7;
            this.txt_ip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_port
            // 
            this.txt_port.Location = new System.Drawing.Point(351, 21);
            this.txt_port.Name = "txt_port";
            this.txt_port.ReadOnly = true;
            this.txt_port.Size = new System.Drawing.Size(100, 22);
            this.txt_port.TabIndex = 8;
            this.txt_port.Text = "4370";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(266, 177);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 15);
            this.label10.TabIndex = 3;
            this.label10.Text = "Activer : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Emplacement : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 175);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Type : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Description : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(307, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Port : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Adresse IP : ";
            // 
            // cbox_agence
            // 
            this.cbox_agence.Enabled = false;
            this.cbox_agence.FormattingEnabled = true;
            this.cbox_agence.Location = new System.Drawing.Point(201, 206);
            this.cbox_agence.Name = "cbox_agence";
            this.cbox_agence.Size = new System.Drawing.Size(250, 23);
            this.cbox_agence.TabIndex = 14;
            this.cbox_agence.SelectedIndexChanged += new System.EventHandler(this.cbox_agence_SelectedIndexChanged);
            // 
            // Form_Pointeuse
            // 
            this.AcceptButton = this.btn_appliquer;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(466, 274);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_appliquer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(482, 313);
            this.MinimumSize = new System.Drawing.Size(482, 313);
            this.Name = "Form_Pointeuse";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ajout Appareil";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Infos_Pointeuse_FormClosing);
            this.Load += new System.EventHandler(this.Form_Infos_Pointeuse_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_appliquer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_port;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.RichTextBox txt_emplacement;
        public System.Windows.Forms.RichTextBox txt_description;
        private System.Windows.Forms.TextBox txt_ip;
        private System.Windows.Forms.RadioButton rbtn_non;
        private System.Windows.Forms.RadioButton rbtn_oui;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox rbtn_multi;
        private System.Windows.Forms.ComboBox com_type;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbox_agence;
    }
}