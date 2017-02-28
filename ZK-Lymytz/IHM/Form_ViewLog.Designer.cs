namespace ZK_Lymytz.IHM
{
    partial class Form_ViewLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ViewLog));
            this.grp_infos = new System.Windows.Forms.GroupBox();
            this.com_registre = new System.Windows.Forms.ComboBox();
            this.btn_ok = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.context_log = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.checkErreurToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgv_log = new System.Windows.Forms.DataGridView();
            this.pos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.error = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.grp_infos.SuspendLayout();
            this.context_log.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_log)).BeginInit();
            this.SuspendLayout();
            // 
            // grp_infos
            // 
            this.grp_infos.Controls.Add(this.dgv_log);
            this.grp_infos.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_infos.Location = new System.Drawing.Point(1, 28);
            this.grp_infos.Name = "grp_infos";
            this.grp_infos.Size = new System.Drawing.Size(350, 453);
            this.grp_infos.TabIndex = 1;
            this.grp_infos.TabStop = false;
            this.grp_infos.Text = "Information log";
            // 
            // com_registre
            // 
            this.com_registre.FormattingEnabled = true;
            this.com_registre.Location = new System.Drawing.Point(86, 7);
            this.com_registre.Name = "com_registre";
            this.com_registre.Size = new System.Drawing.Size(209, 21);
            this.com_registre.TabIndex = 2;
            // 
            // btn_ok
            // 
            this.btn_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ok.Location = new System.Drawing.Point(301, 7);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(50, 23);
            this.btn_ok.TabIndex = 3;
            this.btn_ok.Text = "OK";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Registre : ";
            // 
            // context_log
            // 
            this.context_log.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkErreurToolStripMenuItem});
            this.context_log.Name = "context_log";
            this.context_log.Size = new System.Drawing.Size(142, 26);
            // 
            // checkErreurToolStripMenuItem
            // 
            this.checkErreurToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.alert;
            this.checkErreurToolStripMenuItem.Name = "checkErreurToolStripMenuItem";
            this.checkErreurToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.checkErreurToolStripMenuItem.Text = "Check Erreur";
            this.checkErreurToolStripMenuItem.Click += new System.EventHandler(this.checkErreurToolStripMenuItem_Click);
            // 
            // dgv_log
            // 
            this.dgv_log.AllowUserToAddRows = false;
            this.dgv_log.AllowUserToDeleteRows = false;
            this.dgv_log.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_log.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgv_log.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_log.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pos,
            this.date,
            this.error});
            this.dgv_log.ContextMenuStrip = this.context_log;
            this.dgv_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_log.Location = new System.Drawing.Point(3, 18);
            this.dgv_log.Name = "dgv_log";
            this.dgv_log.ReadOnly = true;
            this.dgv_log.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_log.Size = new System.Drawing.Size(344, 432);
            this.dgv_log.TabIndex = 0;
            // 
            // pos
            // 
            this.pos.FillWeight = 20F;
            this.pos.HeaderText = "";
            this.pos.Name = "pos";
            this.pos.ReadOnly = true;
            // 
            // date
            // 
            this.date.HeaderText = "Date Ping";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            // 
            // error
            // 
            this.error.FillWeight = 10F;
            this.error.HeaderText = "";
            this.error.Name = "error";
            this.error.ReadOnly = true;
            // 
            // Form_ViewLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 482);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.com_registre);
            this.Controls.Add(this.grp_infos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(370, 521);
            this.MinimumSize = new System.Drawing.Size(370, 521);
            this.Name = "Form_ViewLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lecture Logs";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_ViewLog_FormClosing);
            this.Load += new System.EventHandler(this.Form_ViewLog_Load);
            this.grp_infos.ResumeLayout(false);
            this.context_log.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_log)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_infos;
        private System.Windows.Forms.ComboBox com_registre;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip context_log;
        private System.Windows.Forms.ToolStripMenuItem checkErreurToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgv_log;
        private System.Windows.Forms.DataGridViewTextBoxColumn pos;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewCheckBoxColumn error;
    }
}