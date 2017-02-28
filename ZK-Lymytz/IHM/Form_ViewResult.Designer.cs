namespace ZK_Lymytz.IHM
{
    partial class Form_ViewResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ViewResult));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lv_infos = new System.Windows.Forms.ListBox();
            this.btn_plus = new System.Windows.Forms.Button();
            this.context_lv_infos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.context_lv_infos.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_plus);
            this.groupBox1.Controls.Add(this.lv_infos);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(1, -2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(798, 392);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Information log";
            // 
            // lv_infos
            // 
            this.lv_infos.ContextMenuStrip = this.context_lv_infos;
            this.lv_infos.FormattingEnabled = true;
            this.lv_infos.ItemHeight = 15;
            this.lv_infos.Location = new System.Drawing.Point(6, 40);
            this.lv_infos.Name = "lv_infos";
            this.lv_infos.Size = new System.Drawing.Size(786, 349);
            this.lv_infos.TabIndex = 0;
            // 
            // btn_plus
            // 
            this.btn_plus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_plus.Location = new System.Drawing.Point(6, 18);
            this.btn_plus.Name = "btn_plus";
            this.btn_plus.Size = new System.Drawing.Size(786, 23);
            this.btn_plus.TabIndex = 1;
            this.btn_plus.Text = "Voir Plus";
            this.btn_plus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_plus.UseVisualStyleBackColor = true;
            this.btn_plus.Click += new System.EventHandler(this.btn_plus_Click);
            // 
            // context_lv_infos
            // 
            this.context_lv_infos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetToolStripMenuItem});
            this.context_lv_infos.Name = "context_lv_infos";
            this.context_lv_infos.Size = new System.Drawing.Size(103, 26);
            // 
            // resetToolStripMenuItem
            // 
            this.resetToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.rotate;
            this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
            this.resetToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.resetToolStripMenuItem.Text = "Reset";
            this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
            // 
            // Form_ViewResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(802, 393);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(818, 432);
            this.MinimumSize = new System.Drawing.Size(818, 432);
            this.Name = "Form_ViewResult";
            this.Text = "Rapports Lymytz pointeuse";
            this.Activated += new System.EventHandler(this.Form_ViewResult_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_ViewResult_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.context_lv_infos.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.ListBox lv_infos;
        private System.Windows.Forms.Button btn_plus;
        private System.Windows.Forms.ContextMenuStrip context_lv_infos;
        private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
    }
}