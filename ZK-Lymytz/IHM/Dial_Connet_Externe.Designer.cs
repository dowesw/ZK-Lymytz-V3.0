namespace ZK_Lymytz.IHM
{
    partial class Dial_Connet_Externe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dial_Connet_Externe));
            this.label4 = new System.Windows.Forms.Label();
            this.txt_machine = new System.Windows.Forms.TextBox();
            this.btnUSBConnect = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(32, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Machine SN";
            // 
            // txt_machine
            // 
            this.txt_machine.BackColor = System.Drawing.Color.AliceBlue;
            this.txt_machine.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txt_machine.Location = new System.Drawing.Point(105, 21);
            this.txt_machine.Name = "txt_machine";
            this.txt_machine.Size = new System.Drawing.Size(39, 20);
            this.txt_machine.TabIndex = 13;
            this.txt_machine.Tag = "1";
            this.txt_machine.Text = "1";
            this.txt_machine.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_machine.Leave += new System.EventHandler(this.txt_machine_Leave);
            // 
            // btnUSBConnect
            // 
            this.btnUSBConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUSBConnect.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnUSBConnect.Location = new System.Drawing.Point(164, 19);
            this.btnUSBConnect.Name = "btnUSBConnect";
            this.btnUSBConnect.Size = new System.Drawing.Size(75, 23);
            this.btnUSBConnect.TabIndex = 11;
            this.btnUSBConnect.Text = "Connect";
            this.btnUSBConnect.UseVisualStyleBackColor = true;
            this.btnUSBConnect.Click += new System.EventHandler(this.btnUSBConnect_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnUSBConnect);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_machine);
            this.groupBox1.ForeColor = System.Drawing.Color.Red;
            this.groupBox1.Location = new System.Drawing.Point(1, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(249, 49);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Virtual USBClient";
            // 
            // Dial_Connet_Externe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 55);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(268, 94);
            this.MinimumSize = new System.Drawing.Size(268, 94);
            this.Name = "Dial_Connet_Externe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "192.168.1.201";
            this.Load += new System.EventHandler(this.Dial_Connet_Externe_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_machine;
        private System.Windows.Forms.Button btnUSBConnect;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}