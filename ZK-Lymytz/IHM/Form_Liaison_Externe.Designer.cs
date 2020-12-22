namespace ZK_Lymytz.IHM
{
    partial class Form_Liaison_Externe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Liaison_Externe));
            this.label1 = new System.Windows.Forms.Label();
            this.cbox_table = new System.Windows.Forms.ComboBox();
            this.dgv_data_table = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.intutle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.externe = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_data_table)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Table : ";
            // 
            // cbox_table
            // 
            this.cbox_table.FormattingEnabled = true;
            this.cbox_table.Items.AddRange(new object[] {
            "users",
            "tranchehoraire"});
            this.cbox_table.Location = new System.Drawing.Point(61, 6);
            this.cbox_table.Name = "cbox_table";
            this.cbox_table.Size = new System.Drawing.Size(210, 21);
            this.cbox_table.TabIndex = 1;
            this.cbox_table.SelectedIndexChanged += new System.EventHandler(this.cbox_table_SelectedIndexChanged);
            // 
            // dgv_data_table
            // 
            this.dgv_data_table.AllowUserToAddRows = false;
            this.dgv_data_table.AllowUserToDeleteRows = false;
            this.dgv_data_table.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_data_table.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgv_data_table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_data_table.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.intutle,
            this.externe});
            this.dgv_data_table.Location = new System.Drawing.Point(12, 33);
            this.dgv_data_table.Name = "dgv_data_table";
            this.dgv_data_table.Size = new System.Drawing.Size(796, 306);
            this.dgv_data_table.TabIndex = 2;
            this.dgv_data_table.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgv_data_table_DataError);
            this.dgv_data_table.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_data_table_EditingControlShowing);
            // 
            // id
            // 
            this.id.FillWeight = 40F;
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            // 
            // intutle
            // 
            this.intutle.FillWeight = 73.85786F;
            this.intutle.HeaderText = "Intitulé";
            this.intutle.Name = "intutle";
            // 
            // externe
            // 
            this.externe.FillWeight = 73.85786F;
            this.externe.HeaderText = "Externe";
            this.externe.Name = "externe";
            // 
            // Form_Liaison_Externe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 348);
            this.Controls.Add(this.dgv_data_table);
            this.Controls.Add(this.cbox_table);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_Liaison_Externe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Liaisons Externe";
            this.Load += new System.EventHandler(this.Form_Liaison_Externe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_data_table)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbox_table;
        private System.Windows.Forms.DataGridView dgv_data_table;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn intutle;
        private System.Windows.Forms.DataGridViewComboBoxColumn externe;
    }
}