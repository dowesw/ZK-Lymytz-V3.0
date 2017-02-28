namespace ZK_Lymytz.IHM
{
    partial class Form_Search_Pointeuse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Search_Pointeuse));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_port = new System.Windows.Forms.NumericUpDown();
            this.txt_ip_debut = new System.Windows.Forms.TextBox();
            this.txt_ip_fin = new System.Windows.Forms.TextBox();
            this.btn_scan = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv_pointeuse = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exist = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.context_pointeuse = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.insérerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pbar_statut = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_port)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pointeuse)).BeginInit();
            this.context_pointeuse.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Début : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(232, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Fin :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(411, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Port :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_port);
            this.groupBox1.Controls.Add(this.txt_ip_debut);
            this.groupBox1.Controls.Add(this.txt_ip_fin);
            this.groupBox1.Controls.Add(this.btn_scan);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(2, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(701, 39);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Recherche";
            // 
            // txt_port
            // 
            this.txt_port.Location = new System.Drawing.Point(449, 12);
            this.txt_port.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.txt_port.Name = "txt_port";
            this.txt_port.Size = new System.Drawing.Size(65, 20);
            this.txt_port.TabIndex = 10;
            this.txt_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_port.Value = new decimal(new int[] {
            4370,
            0,
            0,
            0});
            // 
            // txt_ip_debut
            // 
            this.txt_ip_debut.Location = new System.Drawing.Point(79, 13);
            this.txt_ip_debut.Name = "txt_ip_debut";
            this.txt_ip_debut.Size = new System.Drawing.Size(147, 20);
            this.txt_ip_debut.TabIndex = 1;
            this.txt_ip_debut.Tag = "1";
            this.txt_ip_debut.Text = "192.168.30.1";
            this.txt_ip_debut.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_ip_debut.Leave += new System.EventHandler(this.txt_ip_debut_Leave);
            // 
            // txt_ip_fin
            // 
            this.txt_ip_fin.Location = new System.Drawing.Point(265, 13);
            this.txt_ip_fin.Name = "txt_ip_fin";
            this.txt_ip_fin.Size = new System.Drawing.Size(140, 20);
            this.txt_ip_fin.TabIndex = 5;
            this.txt_ip_fin.Tag = "1";
            this.txt_ip_fin.Text = "192.168.30.255";
            this.txt_ip_fin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_ip_fin.Leave += new System.EventHandler(this.txt_ip_fin_Leave);
            // 
            // btn_scan
            // 
            this.btn_scan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_scan.Image = global::ZK_Lymytz.Properties.Resources.search;
            this.btn_scan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_scan.Location = new System.Drawing.Point(620, 10);
            this.btn_scan.Name = "btn_scan";
            this.btn_scan.Size = new System.Drawing.Size(75, 23);
            this.btn_scan.TabIndex = 15;
            this.btn_scan.Text = "  Scanner";
            this.btn_scan.UseVisualStyleBackColor = true;
            this.btn_scan.Click += new System.EventHandler(this.btn_scan_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgv_pointeuse);
            this.groupBox2.Location = new System.Drawing.Point(2, 39);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(701, 248);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pointeuses";
            // 
            // dgv_pointeuse
            // 
            this.dgv_pointeuse.AllowUserToAddRows = false;
            this.dgv_pointeuse.AllowUserToDeleteRows = false;
            this.dgv_pointeuse.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_pointeuse.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgv_pointeuse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_pointeuse.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.pos,
            this.ip,
            this.exist});
            this.dgv_pointeuse.ContextMenuStrip = this.context_pointeuse;
            this.dgv_pointeuse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_pointeuse.Location = new System.Drawing.Point(3, 16);
            this.dgv_pointeuse.Name = "dgv_pointeuse";
            this.dgv_pointeuse.ReadOnly = true;
            this.dgv_pointeuse.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_pointeuse.Size = new System.Drawing.Size(695, 229);
            this.dgv_pointeuse.TabIndex = 0;
            this.dgv_pointeuse.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgv_pointeuse_MouseDown);
            // 
            // id
            // 
            this.id.HeaderText = "";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // pos
            // 
            this.pos.FillWeight = 10F;
            this.pos.HeaderText = "";
            this.pos.Name = "pos";
            this.pos.ReadOnly = true;
            // 
            // ip
            // 
            this.ip.HeaderText = "Adresse";
            this.ip.Name = "ip";
            this.ip.ReadOnly = true;
            // 
            // exist
            // 
            this.exist.FillWeight = 10F;
            this.exist.HeaderText = "";
            this.exist.Name = "exist";
            this.exist.ReadOnly = true;
            // 
            // context_pointeuse
            // 
            this.context_pointeuse.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insérerToolStripMenuItem});
            this.context_pointeuse.Name = "context_pointeuse";
            this.context_pointeuse.Size = new System.Drawing.Size(110, 26);
            // 
            // insérerToolStripMenuItem
            // 
            this.insérerToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.add;
            this.insérerToolStripMenuItem.Name = "insérerToolStripMenuItem";
            this.insérerToolStripMenuItem.Size = new System.Drawing.Size(109, 22);
            this.insérerToolStripMenuItem.Text = "Insérer";
            this.insérerToolStripMenuItem.Click += new System.EventHandler(this.insérerToolStripMenuItem_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pbar_statut);
            this.panel4.Location = new System.Drawing.Point(2, 290);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(701, 22);
            this.panel4.TabIndex = 7;
            // 
            // pbar_statut
            // 
            this.pbar_statut.Location = new System.Drawing.Point(3, 6);
            this.pbar_statut.Maximum = 10000;
            this.pbar_statut.Name = "pbar_statut";
            this.pbar_statut.Size = new System.Drawing.Size(696, 10);
            this.pbar_statut.TabIndex = 0;
            // 
            // Form_Search_Pointeuse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 317);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form_Search_Pointeuse";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Listes pointeuses du réseau";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_List_Pointeuse_FormClosing);
            this.Load += new System.EventHandler(this.Form_List_Pointeuse_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txt_port)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pointeuse)).EndInit();
            this.context_pointeuse.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_scan;
        private System.Windows.Forms.NumericUpDown txt_port;
        private System.Windows.Forms.TextBox txt_ip_debut;
        private System.Windows.Forms.TextBox txt_ip_fin;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgv_pointeuse;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn pos;
        private System.Windows.Forms.DataGridViewTextBoxColumn ip;
        private System.Windows.Forms.DataGridViewCheckBoxColumn exist;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ProgressBar pbar_statut;
        private System.Windows.Forms.ContextMenuStrip context_pointeuse;
        private System.Windows.Forms.ToolStripMenuItem insérerToolStripMenuItem;
    }
}