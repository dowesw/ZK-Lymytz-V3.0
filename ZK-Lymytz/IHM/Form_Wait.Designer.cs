namespace ZK_Lymytz.IHM
{
    partial class Form_Wait
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Wait));
            this.p_bar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // p_bar
            // 
            this.p_bar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p_bar.Location = new System.Drawing.Point(0, 0);
            this.p_bar.Maximum = 60;
            this.p_bar.Name = "p_bar";
            this.p_bar.Size = new System.Drawing.Size(284, 23);
            this.p_bar.TabIndex = 0;
            // 
            // Form_Wait
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 23);
            this.Controls.Add(this.p_bar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(284, 23);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(284, 23);
            this.Name = "Form_Wait";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Patientez Svp";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Wait_FormClosed);
            this.Load += new System.EventHandler(this.Form_Wait_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar p_bar;
    }
}