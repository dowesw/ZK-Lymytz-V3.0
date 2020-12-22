namespace ZK_Lymytz.IHM
{
    partial class Dial_View_No_Insert
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dial_View_No_Insert));
            this.dgv_presence = new System.Windows.Forms.DataGridView();
            this.id_presence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.personnel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date_debut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date_fin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date_fin_prevu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.heure_fin_prevu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valide = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.context_presence = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.envoyerSurLaVueDesPrésencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgv_pointage = new System.Windows.Forms.DataGridView();
            this.id_pointage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.heure_entree = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.heure_sortie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.duree_presence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valider = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.supplementaire = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.compensatoire = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.normale = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.lb_statut = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_presence)).BeginInit();
            this.context_presence.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pointage)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_presence
            // 
            this.dgv_presence.AllowUserToAddRows = false;
            this.dgv_presence.AllowUserToDeleteRows = false;
            this.dgv_presence.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_presence.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgv_presence.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_presence.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_presence,
            this.personnel,
            this.date_debut,
            this.date_fin,
            this.date_fin_prevu,
            this.heure_fin_prevu,
            this.valide});
            this.dgv_presence.ContextMenuStrip = this.context_presence;
            this.dgv_presence.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgv_presence.Location = new System.Drawing.Point(0, 0);
            this.dgv_presence.Name = "dgv_presence";
            this.dgv_presence.ReadOnly = true;
            this.dgv_presence.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_presence.Size = new System.Drawing.Size(825, 90);
            this.dgv_presence.TabIndex = 0;
            this.dgv_presence.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgv_presence_MouseDown);
            // 
            // id_presence
            // 
            this.id_presence.HeaderText = "ID";
            this.id_presence.Name = "id_presence";
            this.id_presence.ReadOnly = true;
            this.id_presence.Visible = false;
            // 
            // personnel
            // 
            this.personnel.HeaderText = "Employe";
            this.personnel.Name = "personnel";
            this.personnel.ReadOnly = true;
            // 
            // date_debut
            // 
            this.date_debut.HeaderText = "Date Début";
            this.date_debut.Name = "date_debut";
            this.date_debut.ReadOnly = true;
            // 
            // date_fin
            // 
            this.date_fin.HeaderText = "Date Fin";
            this.date_fin.Name = "date_fin";
            this.date_fin.ReadOnly = true;
            // 
            // date_fin_prevu
            // 
            this.date_fin_prevu.HeaderText = "Date Prévu";
            this.date_fin_prevu.Name = "date_fin_prevu";
            this.date_fin_prevu.ReadOnly = true;
            // 
            // heure_fin_prevu
            // 
            this.heure_fin_prevu.HeaderText = "Heure Prévu";
            this.heure_fin_prevu.Name = "heure_fin_prevu";
            this.heure_fin_prevu.ReadOnly = true;
            // 
            // valide
            // 
            this.valide.FillWeight = 20F;
            this.valide.HeaderText = "Val.";
            this.valide.Name = "valide";
            this.valide.ReadOnly = true;
            this.valide.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.valide.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // context_presence
            // 
            this.context_presence.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.envoyerSurLaVueDesPrésencesToolStripMenuItem});
            this.context_presence.Name = "context_presence";
            this.context_presence.Size = new System.Drawing.Size(246, 26);
            // 
            // envoyerSurLaVueDesPrésencesToolStripMenuItem
            // 
            this.envoyerSurLaVueDesPrésencesToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.next;
            this.envoyerSurLaVueDesPrésencesToolStripMenuItem.Name = "envoyerSurLaVueDesPrésencesToolStripMenuItem";
            this.envoyerSurLaVueDesPrésencesToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.envoyerSurLaVueDesPrésencesToolStripMenuItem.Text = "Envoyer sur la vue des présences";
            this.envoyerSurLaVueDesPrésencesToolStripMenuItem.Click += new System.EventHandler(this.envoyerSurLaVueDesPrésencesToolStripMenuItem_Click);
            // 
            // dgv_pointage
            // 
            this.dgv_pointage.AllowUserToAddRows = false;
            this.dgv_pointage.AllowUserToDeleteRows = false;
            this.dgv_pointage.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_pointage.BackgroundColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dgv_pointage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_pointage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_pointage,
            this.heure_entree,
            this.heure_sortie,
            this.duree_presence,
            this.valider,
            this.supplementaire,
            this.compensatoire,
            this.normale});
            this.dgv_pointage.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgv_pointage.Location = new System.Drawing.Point(0, 90);
            this.dgv_pointage.Name = "dgv_pointage";
            this.dgv_pointage.ReadOnly = true;
            this.dgv_pointage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_pointage.Size = new System.Drawing.Size(825, 110);
            this.dgv_pointage.TabIndex = 1;
            this.dgv_pointage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgv_pointage_MouseDown);
            // 
            // id_pointage
            // 
            this.id_pointage.HeaderText = "ID";
            this.id_pointage.Name = "id_pointage";
            this.id_pointage.ReadOnly = true;
            this.id_pointage.Visible = false;
            // 
            // heure_entree
            // 
            this.heure_entree.HeaderText = "Heure Entree";
            this.heure_entree.Name = "heure_entree";
            this.heure_entree.ReadOnly = true;
            // 
            // heure_sortie
            // 
            this.heure_sortie.HeaderText = "Heure Sortie";
            this.heure_sortie.Name = "heure_sortie";
            this.heure_sortie.ReadOnly = true;
            // 
            // duree_presence
            // 
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = "0";
            this.duree_presence.DefaultCellStyle = dataGridViewCellStyle1;
            this.duree_presence.HeaderText = "Durée";
            this.duree_presence.Name = "duree_presence";
            this.duree_presence.ReadOnly = true;
            // 
            // valider
            // 
            this.valider.FillWeight = 20F;
            this.valider.HeaderText = "Val.";
            this.valider.Name = "valider";
            this.valider.ReadOnly = true;
            this.valider.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.valider.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // supplementaire
            // 
            this.supplementaire.FillWeight = 20F;
            this.supplementaire.HeaderText = "Sup";
            this.supplementaire.Name = "supplementaire";
            this.supplementaire.ReadOnly = true;
            this.supplementaire.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.supplementaire.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // compensatoire
            // 
            this.compensatoire.FillWeight = 20F;
            this.compensatoire.HeaderText = "Comp.";
            this.compensatoire.Name = "compensatoire";
            this.compensatoire.ReadOnly = true;
            this.compensatoire.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.compensatoire.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // normale
            // 
            this.normale.FillWeight = 20F;
            this.normale.HeaderText = "Nor.";
            this.normale.Name = "normale";
            this.normale.ReadOnly = true;
            this.normale.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.normale.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // lb_statut
            // 
            this.lb_statut.AutoSize = true;
            this.lb_statut.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lb_statut.Location = new System.Drawing.Point(0, 203);
            this.lb_statut.Name = "lb_statut";
            this.lb_statut.Size = new System.Drawing.Size(35, 13);
            this.lb_statut.TabIndex = 3;
            this.lb_statut.Text = "label1";
            // 
            // Dial_View_No_Insert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 216);
            this.Controls.Add(this.lb_statut);
            this.Controls.Add(this.dgv_pointage);
            this.Controls.Add(this.dgv_presence);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dial_View_No_Insert";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Analyse poinatge";
            this.Load += new System.EventHandler(this.Dial_View_No_Insert_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_presence)).EndInit();
            this.context_presence.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pointage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_presence;
        private System.Windows.Forms.DataGridView dgv_pointage;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_presence;
        private System.Windows.Forms.DataGridViewTextBoxColumn personnel;
        private System.Windows.Forms.DataGridViewTextBoxColumn date_debut;
        private System.Windows.Forms.DataGridViewTextBoxColumn date_fin;
        private System.Windows.Forms.DataGridViewTextBoxColumn date_fin_prevu;
        private System.Windows.Forms.DataGridViewTextBoxColumn heure_fin_prevu;
        private System.Windows.Forms.DataGridViewCheckBoxColumn valide;
        private System.Windows.Forms.ContextMenuStrip context_presence;
        private System.Windows.Forms.ToolStripMenuItem envoyerSurLaVueDesPrésencesToolStripMenuItem;
        private System.Windows.Forms.Label lb_statut;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_pointage;
        private System.Windows.Forms.DataGridViewTextBoxColumn heure_entree;
        private System.Windows.Forms.DataGridViewTextBoxColumn heure_sortie;
        private System.Windows.Forms.DataGridViewTextBoxColumn duree_presence;
        private System.Windows.Forms.DataGridViewCheckBoxColumn valider;
        private System.Windows.Forms.DataGridViewCheckBoxColumn supplementaire;
        private System.Windows.Forms.DataGridViewCheckBoxColumn compensatoire;
        private System.Windows.Forms.DataGridViewCheckBoxColumn normale;
    }
}