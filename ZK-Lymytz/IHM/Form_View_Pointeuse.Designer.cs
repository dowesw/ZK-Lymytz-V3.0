namespace ZK_Lymytz.IHM
{
    partial class Form_View_Pointeuse
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_View_Pointeuse));
            this.dgv_pointeuse = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.adresse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emplacement = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pointeuse)).BeginInit();
            this.SuspendLayout();
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
            this.adresse,
            this.emplacement,
            this.type});
            this.dgv_pointeuse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_pointeuse.Location = new System.Drawing.Point(0, 0);
            this.dgv_pointeuse.Name = "dgv_pointeuse";
            this.dgv_pointeuse.ReadOnly = true;
            this.dgv_pointeuse.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_pointeuse.Size = new System.Drawing.Size(571, 261);
            this.dgv_pointeuse.TabIndex = 0;
            this.dgv_pointeuse.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgv_pointeuse_MouseDown);
            // 
            // id
            // 
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // adresse
            // 
            this.adresse.FillWeight = 80F;
            this.adresse.HeaderText = "Adresse";
            this.adresse.Name = "adresse";
            this.adresse.ReadOnly = true;
            // 
            // emplacement
            // 
            this.emplacement.HeaderText = "Emplacement";
            this.emplacement.Name = "emplacement";
            this.emplacement.ReadOnly = true;
            // 
            // type
            // 
            this.type.FillWeight = 50F;
            this.type.HeaderText = "Type";
            this.type.Name = "type";
            this.type.ReadOnly = true;
            // 
            // Form_View_Pointeuse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 261);
            this.Controls.Add(this.dgv_pointeuse);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(587, 300);
            this.MinimumSize = new System.Drawing.Size(587, 300);
            this.Name = "Form_View_Pointeuse";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Liste des pointeuse";
            this.Load += new System.EventHandler(this.Form_View_Pointeuse_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pointeuse)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_pointeuse;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn adresse;
        private System.Windows.Forms.DataGridViewTextBoxColumn emplacement;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
    }
}