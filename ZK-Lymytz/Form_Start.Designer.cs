namespace ZK_Lymytz
{
    partial class Form_Start
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Start));
            this.pbar_start = new System.Windows.Forms.ProgressBar();
            this.lb_statut = new System.Windows.Forms.Label();
            this.box_label = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.box_label)).BeginInit();
            this.SuspendLayout();
            // 
            // pbar_start
            // 
            this.pbar_start.Location = new System.Drawing.Point(12, 68);
            this.pbar_start.Name = "pbar_start";
            this.pbar_start.Size = new System.Drawing.Size(379, 23);
            this.pbar_start.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbar_start.TabIndex = 0;
            // 
            // lb_statut
            // 
            this.lb_statut.AutoSize = true;
            this.lb_statut.Location = new System.Drawing.Point(12, 94);
            this.lb_statut.Name = "lb_statut";
            this.lb_statut.Size = new System.Drawing.Size(143, 13);
            this.lb_statut.TabIndex = 1;
            this.lb_statut.Text = "Chargement des informations";
            // 
            // box_label
            // 
            this.box_label.BackColor = System.Drawing.Color.Transparent;
            this.box_label.Image = global::ZK_Lymytz.Properties.Resources.label;
            this.box_label.Location = new System.Drawing.Point(12, 9);
            this.box_label.Name = "box_label";
            this.box_label.Size = new System.Drawing.Size(379, 50);
            this.box_label.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.box_label.TabIndex = 2;
            this.box_label.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Form_Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(403, 112);
            this.Controls.Add(this.box_label);
            this.Controls.Add(this.lb_statut);
            this.Controls.Add(this.pbar_start);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Start";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form_Start";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.Form_Start_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Start_FormClosed);
            this.Load += new System.EventHandler(this.Form_Start_Load);
            ((System.ComponentModel.ISupportInitialize)(this.box_label)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbar_start;
        private System.Windows.Forms.Label lb_statut;
        private System.Windows.Forms.PictureBox box_label;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
    }
}