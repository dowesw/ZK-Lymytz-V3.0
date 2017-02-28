namespace ZK_Lymytz.IHM
{
    partial class Form_Ping_Appareil
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Ping_Appareil));
            this.btn_test = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_ip = new System.Windows.Forms.TextBox();
            this.txt_port = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.txt_port)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_test
            // 
            this.btn_test.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_test.Image = global::ZK_Lymytz.Properties.Resources.irkick;
            this.btn_test.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_test.Location = new System.Drawing.Point(393, 2);
            this.btn_test.Name = "btn_test";
            this.btn_test.Size = new System.Drawing.Size(75, 23);
            this.btn_test.TabIndex = 0;
            this.btn_test.Text = "Ping";
            this.btn_test.UseVisualStyleBackColor = true;
            this.btn_test.Click += new System.EventHandler(this.btn_test_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(226, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "IP : ";
            // 
            // txt_ip
            // 
            this.txt_ip.Location = new System.Drawing.Point(33, 5);
            this.txt_ip.Name = "txt_ip";
            this.txt_ip.Size = new System.Drawing.Size(169, 20);
            this.txt_ip.TabIndex = 3;
            this.txt_ip.Tag = "1";
            this.txt_ip.Text = "192.168.1.201";
            this.txt_ip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_ip.Leave += new System.EventHandler(this.txt_ip_Leave);
            // 
            // txt_port
            // 
            this.txt_port.Location = new System.Drawing.Point(267, 5);
            this.txt_port.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.txt_port.Name = "txt_port";
            this.txt_port.Size = new System.Drawing.Size(89, 20);
            this.txt_port.TabIndex = 4;
            this.txt_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_port.Value = new decimal(new int[] {
            4370,
            0,
            0,
            0});
            // 
            // Form_Ping_Appareil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(469, 32);
            this.Controls.Add(this.txt_port);
            this.Controls.Add(this.txt_ip);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_test);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(485, 71);
            this.MinimumSize = new System.Drawing.Size(485, 71);
            this.Name = "Form_Ping_Appareil";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ping Adresse";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Ping_Appareil_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.txt_port)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_test;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_ip;
        private System.Windows.Forms.NumericUpDown txt_port;
    }
}