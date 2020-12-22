namespace ZK_Lymytz.IHM
{
    partial class Dial_View_Heure_Prevu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dial_View_Heure_Prevu));
            this.grp_total = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.dtp_date = new System.Windows.Forms.DateTimePicker();
            this.dtp_heure = new System.Windows.Forms.DateTimePicker();
            this.btn_update = new System.Windows.Forms.Button();
            this.grp_total.SuspendLayout();
            this.SuspendLayout();
            // 
            // grp_total
            // 
            this.grp_total.Controls.Add(this.dtp_heure);
            this.grp_total.Controls.Add(this.dtp_date);
            this.grp_total.Controls.Add(this.label9);
            this.grp_total.Controls.Add(this.label10);
            this.grp_total.Dock = System.Windows.Forms.DockStyle.Top;
            this.grp_total.Location = new System.Drawing.Point(0, 0);
            this.grp_total.Name = "grp_total";
            this.grp_total.Size = new System.Drawing.Size(310, 45);
            this.grp_total.TabIndex = 12;
            this.grp_total.TabStop = false;
            this.grp_total.Text = "Temps Prévue";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Date : ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(183, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Heure :";
            // 
            // dtp_date
            // 
            this.dtp_date.CustomFormat = "";
            this.dtp_date.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_date.Location = new System.Drawing.Point(62, 16);
            this.dtp_date.Name = "dtp_date";
            this.dtp_date.Size = new System.Drawing.Size(96, 20);
            this.dtp_date.TabIndex = 11;
            // 
            // dtp_heure
            // 
            this.dtp_heure.CustomFormat = "";
            this.dtp_heure.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_heure.Location = new System.Drawing.Point(231, 16);
            this.dtp_heure.Name = "dtp_heure";
            this.dtp_heure.ShowUpDown = true;
            this.dtp_heure.Size = new System.Drawing.Size(73, 20);
            this.dtp_heure.TabIndex = 11;
            // 
            // btn_update
            // 
            this.btn_update.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_update.Image = global::ZK_Lymytz.Properties.Resources.save;
            this.btn_update.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_update.Location = new System.Drawing.Point(221, 49);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(88, 23);
            this.btn_update.TabIndex = 13;
            this.btn_update.Text = "Modifier";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // Dial_View_Heure_Prevu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 78);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.grp_total);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(326, 117);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(326, 117);
            this.Name = "Dial_View_Heure_Prevu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Temps Prévue";
            this.Load += new System.EventHandler(this.Dial_View_Heure_Prevu_Load);
            this.grp_total.ResumeLayout(false);
            this.grp_total.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_total;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dtp_date;
        private System.Windows.Forms.DateTimePicker dtp_heure;
        private System.Windows.Forms.Button btn_update;

    }
}