namespace ZK_Lymytz.IHM
{
    public partial class Form_Add_Empreinte
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Add_Empreinte));
            this.box_main_droite = new System.Windows.Forms.PictureBox();
            this.panel_1_5 = new System.Windows.Forms.Panel();
            this.panel_1_4 = new System.Windows.Forms.Panel();
            this.panel_1_3 = new System.Windows.Forms.Panel();
            this.panel_1_2 = new System.Windows.Forms.Panel();
            this.panel_1_1 = new System.Windows.Forms.Panel();
            this.com_employe = new System.Windows.Forms.ComboBox();
            this.txt_id = new System.Windows.Forms.TextBox();
            this.grp_employe = new System.Windows.Forms.GroupBox();
            this.grp_action = new System.Windows.Forms.GroupBox();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.box_doigt = new System.Windows.Forms.PictureBox();
            this.panel_2_3 = new System.Windows.Forms.Panel();
            this.panel_2_1 = new System.Windows.Forms.Panel();
            this.panel_2_2 = new System.Windows.Forms.Panel();
            this.panel_2_4 = new System.Windows.Forms.Panel();
            this.panel_2_5 = new System.Windows.Forms.Panel();
            this.box_main_gauche = new System.Windows.Forms.PictureBox();
            this.txt_result = new System.Windows.Forms.TextBox();
            this.tt_index = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.box_main_droite)).BeginInit();
            this.grp_employe.SuspendLayout();
            this.grp_action.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.box_doigt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.box_main_gauche)).BeginInit();
            this.SuspendLayout();
            // 
            // box_main_droite
            // 
            this.box_main_droite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.box_main_droite.Image = global::ZK_Lymytz.Properties.Resources.mains_droite;
            this.box_main_droite.Location = new System.Drawing.Point(331, 70);
            this.box_main_droite.Name = "box_main_droite";
            this.box_main_droite.Size = new System.Drawing.Size(289, 396);
            this.box_main_droite.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.box_main_droite.TabIndex = 0;
            this.box_main_droite.TabStop = false;
            // 
            // panel_1_5
            // 
            this.panel_1_5.Location = new System.Drawing.Point(345, 147);
            this.panel_1_5.Name = "panel_1_5";
            this.panel_1_5.Size = new System.Drawing.Size(25, 34);
            this.panel_1_5.TabIndex = 1;
            this.tt_index.SetToolTip(this.panel_1_5, "auriculaire main droite");
            this.panel_1_5.Click += new System.EventHandler(this.panel_1_5_Click);
            // 
            // panel_1_4
            // 
            this.panel_1_4.Location = new System.Drawing.Point(401, 100);
            this.panel_1_4.Name = "panel_1_4";
            this.panel_1_4.Size = new System.Drawing.Size(29, 43);
            this.panel_1_4.TabIndex = 1;
            this.tt_index.SetToolTip(this.panel_1_4, "annulaire main droite");
            this.panel_1_4.Click += new System.EventHandler(this.panel_1_4_Click);
            // 
            // panel_1_3
            // 
            this.panel_1_3.Location = new System.Drawing.Point(456, 79);
            this.panel_1_3.Name = "panel_1_3";
            this.panel_1_3.Size = new System.Drawing.Size(36, 43);
            this.panel_1_3.TabIndex = 1;
            this.tt_index.SetToolTip(this.panel_1_3, "majeur main droite");
            this.panel_1_3.Click += new System.EventHandler(this.panel_1_3_Click);
            // 
            // panel_1_2
            // 
            this.panel_1_2.Location = new System.Drawing.Point(515, 104);
            this.panel_1_2.Name = "panel_1_2";
            this.panel_1_2.Size = new System.Drawing.Size(29, 43);
            this.panel_1_2.TabIndex = 1;
            this.tt_index.SetToolTip(this.panel_1_2, "index main droite");
            this.panel_1_2.Click += new System.EventHandler(this.panel_1_2_Click);
            // 
            // panel_1_1
            // 
            this.panel_1_1.Location = new System.Drawing.Point(580, 229);
            this.panel_1_1.Name = "panel_1_1";
            this.panel_1_1.Size = new System.Drawing.Size(34, 43);
            this.panel_1_1.TabIndex = 1;
            this.tt_index.SetToolTip(this.panel_1_1, "pouce main droite");
            this.panel_1_1.Click += new System.EventHandler(this.panel_1_1_Click);
            // 
            // com_employe
            // 
            this.com_employe.FormattingEnabled = true;
            this.com_employe.Location = new System.Drawing.Point(106, 19);
            this.com_employe.Name = "com_employe";
            this.com_employe.Size = new System.Drawing.Size(177, 21);
            this.com_employe.TabIndex = 2;
            this.com_employe.SelectedIndexChanged += new System.EventHandler(this.com_employe_SelectedIndexChanged);
            // 
            // txt_id
            // 
            this.txt_id.Location = new System.Drawing.Point(6, 20);
            this.txt_id.Name = "txt_id";
            this.txt_id.ReadOnly = true;
            this.txt_id.Size = new System.Drawing.Size(94, 20);
            this.txt_id.TabIndex = 3;
            // 
            // grp_employe
            // 
            this.grp_employe.Controls.Add(this.com_employe);
            this.grp_employe.Controls.Add(this.txt_id);
            this.grp_employe.Location = new System.Drawing.Point(19, 12);
            this.grp_employe.Name = "grp_employe";
            this.grp_employe.Size = new System.Drawing.Size(289, 48);
            this.grp_employe.TabIndex = 4;
            this.grp_employe.TabStop = false;
            this.grp_employe.Text = "Employé";
            // 
            // grp_action
            // 
            this.grp_action.Controls.Add(this.btn_cancel);
            this.grp_action.Controls.Add(this.btn_save);
            this.grp_action.Location = new System.Drawing.Point(626, 6);
            this.grp_action.Name = "grp_action";
            this.grp_action.Size = new System.Drawing.Size(121, 94);
            this.grp_action.TabIndex = 5;
            this.grp_action.TabStop = false;
            this.grp_action.Text = "Actions";
            // 
            // btn_cancel
            // 
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Location = new System.Drawing.Point(7, 57);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(109, 26);
            this.btn_cancel.TabIndex = 1;
            this.btn_cancel.Text = "Annuler";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_save
            // 
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.Location = new System.Drawing.Point(6, 20);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(109, 27);
            this.btn_save.TabIndex = 0;
            this.btn_save.Text = "Enregistrer";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // box_doigt
            // 
            this.box_doigt.Image = global::ZK_Lymytz.Properties.Resources.empreinte;
            this.box_doigt.Location = new System.Drawing.Point(632, 208);
            this.box_doigt.Name = "box_doigt";
            this.box_doigt.Size = new System.Drawing.Size(115, 169);
            this.box_doigt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.box_doigt.TabIndex = 6;
            this.box_doigt.TabStop = false;
            // 
            // panel_2_3
            // 
            this.panel_2_3.Location = new System.Drawing.Point(147, 78);
            this.panel_2_3.Name = "panel_2_3";
            this.panel_2_3.Size = new System.Drawing.Size(36, 43);
            this.panel_2_3.TabIndex = 8;
            this.tt_index.SetToolTip(this.panel_2_3, "majeur main gauche");
            this.panel_2_3.Click += new System.EventHandler(this.panel_2_3_Click);
            // 
            // panel_2_1
            // 
            this.panel_2_1.Location = new System.Drawing.Point(25, 228);
            this.panel_2_1.Name = "panel_2_1";
            this.panel_2_1.Size = new System.Drawing.Size(34, 43);
            this.panel_2_1.TabIndex = 9;
            this.tt_index.SetToolTip(this.panel_2_1, "pouce main gauche");
            this.panel_2_1.Click += new System.EventHandler(this.panel_2_1_Click);
            // 
            // panel_2_2
            // 
            this.panel_2_2.Location = new System.Drawing.Point(95, 103);
            this.panel_2_2.Name = "panel_2_2";
            this.panel_2_2.Size = new System.Drawing.Size(29, 43);
            this.panel_2_2.TabIndex = 10;
            this.tt_index.SetToolTip(this.panel_2_2, "index main gauche");
            this.panel_2_2.Click += new System.EventHandler(this.panel_2_2_Click);
            // 
            // panel_2_4
            // 
            this.panel_2_4.Location = new System.Drawing.Point(208, 99);
            this.panel_2_4.Name = "panel_2_4";
            this.panel_2_4.Size = new System.Drawing.Size(29, 43);
            this.panel_2_4.TabIndex = 11;
            this.tt_index.SetToolTip(this.panel_2_4, "annulaire main gauche");
            this.panel_2_4.Click += new System.EventHandler(this.panel_2_4_Click);
            // 
            // panel_2_5
            // 
            this.panel_2_5.Location = new System.Drawing.Point(268, 146);
            this.panel_2_5.Name = "panel_2_5";
            this.panel_2_5.Size = new System.Drawing.Size(25, 34);
            this.panel_2_5.TabIndex = 12;
            this.tt_index.SetToolTip(this.panel_2_5, "auriculaire main gauche");
            this.panel_2_5.Click += new System.EventHandler(this.panel_2_5_Click);
            // 
            // box_main_gauche
            // 
            this.box_main_gauche.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.box_main_gauche.Image = global::ZK_Lymytz.Properties.Resources.mains_gauche;
            this.box_main_gauche.Location = new System.Drawing.Point(19, 69);
            this.box_main_gauche.Name = "box_main_gauche";
            this.box_main_gauche.Size = new System.Drawing.Size(289, 396);
            this.box_main_gauche.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.box_main_gauche.TabIndex = 7;
            this.box_main_gauche.TabStop = false;
            // 
            // txt_result
            // 
            this.txt_result.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_result.Location = new System.Drawing.Point(632, 436);
            this.txt_result.Name = "txt_result";
            this.txt_result.ReadOnly = true;
            this.txt_result.Size = new System.Drawing.Size(115, 22);
            this.txt_result.TabIndex = 13;
            this.txt_result.Text = "En Attente";
            this.txt_result.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tt_index
            // 
            this.tt_index.ToolTipTitle = "Doigt";
            // 
            // Form_Add_Empreinte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 478);
            this.Controls.Add(this.txt_result);
            this.Controls.Add(this.panel_2_3);
            this.Controls.Add(this.panel_2_1);
            this.Controls.Add(this.panel_2_2);
            this.Controls.Add(this.panel_2_4);
            this.Controls.Add(this.panel_2_5);
            this.Controls.Add(this.box_main_gauche);
            this.Controls.Add(this.box_doigt);
            this.Controls.Add(this.grp_action);
            this.Controls.Add(this.grp_employe);
            this.Controls.Add(this.panel_1_3);
            this.Controls.Add(this.panel_1_1);
            this.Controls.Add(this.panel_1_2);
            this.Controls.Add(this.panel_1_4);
            this.Controls.Add(this.panel_1_5);
            this.Controls.Add(this.box_main_droite);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(770, 517);
            this.MinimumSize = new System.Drawing.Size(770, 517);
            this.Name = "Form_Add_Empreinte";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sauvegarde Empreinte";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form_Empreinte_FormClosed);
            this.Load += new System.EventHandler(this.Form_Empreinte_Load);
            ((System.ComponentModel.ISupportInitialize)(this.box_main_droite)).EndInit();
            this.grp_employe.ResumeLayout(false);
            this.grp_employe.PerformLayout();
            this.grp_action.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.box_doigt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.box_main_gauche)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox box_main_droite;
        private System.Windows.Forms.Panel panel_1_5;
        private System.Windows.Forms.Panel panel_1_4;
        private System.Windows.Forms.Panel panel_1_3;
        private System.Windows.Forms.Panel panel_1_2;
        private System.Windows.Forms.Panel panel_1_1;
        private System.Windows.Forms.ComboBox com_employe;
        private System.Windows.Forms.TextBox txt_id;
        private System.Windows.Forms.GroupBox grp_employe;
        private System.Windows.Forms.GroupBox grp_action;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Panel panel_2_3;
        private System.Windows.Forms.Panel panel_2_1;
        private System.Windows.Forms.Panel panel_2_2;
        private System.Windows.Forms.Panel panel_2_4;
        private System.Windows.Forms.Panel panel_2_5;
        private System.Windows.Forms.PictureBox box_main_gauche;
        public System.Windows.Forms.PictureBox box_doigt;
        public System.Windows.Forms.TextBox txt_result;
        private System.Windows.Forms.ToolTip tt_index;
    }
}