namespace ZK_Lymytz.IHM
{
    partial class Dial_Update_Action
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dial_Update_Action));
            this.com_action = new System.Windows.Forms.ComboBox();
            this.btn_update = new System.Windows.Forms.Button();
            this.box_action = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.box_action)).BeginInit();
            this.SuspendLayout();
            // 
            // com_action
            // 
            this.com_action.FormattingEnabled = true;
            this.com_action.Location = new System.Drawing.Point(32, 4);
            this.com_action.Name = "com_action";
            this.com_action.Size = new System.Drawing.Size(210, 21);
            this.com_action.TabIndex = 0;
            this.com_action.SelectedIndexChanged += new System.EventHandler(this.com_action_SelectedIndexChanged);
            // 
            // btn_update
            // 
            this.btn_update.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_update.Image = global::ZK_Lymytz.Properties.Resources.save;
            this.btn_update.Location = new System.Drawing.Point(248, 2);
            this.btn_update.Name = "btn_update";
            this.btn_update.Size = new System.Drawing.Size(36, 23);
            this.btn_update.TabIndex = 1;
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // box_action
            // 
            this.box_action.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.box_action.Image = global::ZK_Lymytz.Properties.Resources.check_in;
            this.box_action.Location = new System.Drawing.Point(1, 4);
            this.box_action.Name = "box_action";
            this.box_action.Size = new System.Drawing.Size(25, 21);
            this.box_action.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.box_action.TabIndex = 2;
            this.box_action.TabStop = false;
            // 
            // Dial_Update_Action
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 30);
            this.Controls.Add(this.box_action);
            this.Controls.Add(this.btn_update);
            this.Controls.Add(this.com_action);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dial_Update_Action";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modifier Action";
            this.Load += new System.EventHandler(this.Dial_Update_Action_Load);
            ((System.ComponentModel.ISupportInitialize)(this.box_action)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox com_action;
        private System.Windows.Forms.Button btn_update;
        private System.Windows.Forms.PictureBox box_action;
    }
}