namespace ZK_Lymytz
{
    partial class Form_Parent
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        public void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Parent));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.pingToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.redémarerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusBarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.enrollerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.archivesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appareilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serveurToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recherchePointeuseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.voirResultatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.employesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.présenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.evenementsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.empreintesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.administrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tsb_rattach = new System.Windows.Forms.ToolStripButton();
            this.tsb_actualise = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_new = new System.Windows.Forms.ToolStripButton();
            this.tsb_update = new System.Windows.Forms.ToolStripButton();
            this.tsb_delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_connect = new System.Windows.Forms.ToolStripButton();
            this.tsb_deconnect = new System.Windows.Forms.ToolStripButton();
            this.tsb_start = new System.Windows.Forms.ToolStripButton();
            this.tsb_stop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_restart = new System.Windows.Forms.ToolStripButton();
            this.tsb_off = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.context_bubble = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.activerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.arrêterProcessusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.déconnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redémarerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.bubble = new System.Windows.Forms.NotifyIcon(this.components);
            this.grp_pointeuse = new System.Windows.Forms.GroupBox();
            this.dgv_pointeuse = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.adresse = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.port = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emplacement = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imachine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.connect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.actif = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.icon = new System.Windows.Forms.DataGridViewImageColumn();
            this.context_dgv_pointeuse = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_active = new System.Windows.Forms.ToolStripMenuItem();
            this.chargerFichierTamponToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_report = new System.Windows.Forms.Panel();
            this.lv_report = new System.Windows.Forms.ListBox();
            this.context_log = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.effacerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connexionExterneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.context_bubble.SuspendLayout();
            this.grp_pointeuse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pointeuse)).BeginInit();
            this.context_dgv_pointeuse.SuspendLayout();
            this.panel_report.SuspendLayout();
            this.context_log.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.viewMenu,
            this.toolsMenu,
            this.windowsMenu,
            this.helpMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.MdiWindowListItem = this.windowsMenu;
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(632, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pingToolStripMenuItem1,
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator3,
            this.redémarerToolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(54, 20);
            this.fileMenu.Text = "&Fichier";
            // 
            // pingToolStripMenuItem1
            // 
            this.pingToolStripMenuItem1.Image = global::ZK_Lymytz.Properties.Resources.pc;
            this.pingToolStripMenuItem1.Name = "pingToolStripMenuItem1";
            this.pingToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.pingToolStripMenuItem1.Size = new System.Drawing.Size(165, 22);
            this.pingToolStripMenuItem1.Text = "Ping";
            this.pingToolStripMenuItem1.Click += new System.EventHandler(this.pingToolStripMenuItem1_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.newToolStripMenuItem.Text = "&Nouveau";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.ShowNewForm);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.connected;
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.openToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.openToolStripMenuItem.Text = "&Actualiser";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.tsb_actualise_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(162, 6);
            // 
            // redémarerToolStripMenuItem1
            // 
            this.redémarerToolStripMenuItem1.Image = global::ZK_Lymytz.Properties.Resources.restart;
            this.redémarerToolStripMenuItem1.Name = "redémarerToolStripMenuItem1";
            this.redémarerToolStripMenuItem1.Size = new System.Drawing.Size(165, 22);
            this.redémarerToolStripMenuItem1.Text = "Redémarer";
            this.redémarerToolStripMenuItem1.Click += new System.EventHandler(this.redémarerToolStripMenuItem1_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.exit;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.exitToolStripMenuItem.Text = "&Quitter";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolsStripMenuItem_Click);
            // 
            // viewMenu
            // 
            this.viewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBarToolStripMenuItem,
            this.statusBarToolStripMenuItem});
            this.viewMenu.Name = "viewMenu";
            this.viewMenu.Size = new System.Drawing.Size(70, 20);
            this.viewMenu.Text = "&Affichage";
            // 
            // toolBarToolStripMenuItem
            // 
            this.toolBarToolStripMenuItem.Checked = true;
            this.toolBarToolStripMenuItem.CheckOnClick = true;
            this.toolBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolBarToolStripMenuItem.Name = "toolBarToolStripMenuItem";
            this.toolBarToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.toolBarToolStripMenuItem.Text = "&barre d\'outils";
            this.toolBarToolStripMenuItem.Click += new System.EventHandler(this.ToolBarToolStripMenuItem_Click);
            // 
            // statusBarToolStripMenuItem
            // 
            this.statusBarToolStripMenuItem.Checked = true;
            this.statusBarToolStripMenuItem.CheckOnClick = true;
            this.statusBarToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.statusBarToolStripMenuItem.Name = "statusBarToolStripMenuItem";
            this.statusBarToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.statusBarToolStripMenuItem.Text = "&Barre d\'état";
            this.statusBarToolStripMenuItem.Click += new System.EventHandler(this.StatusBarToolStripMenuItem_Click);
            // 
            // toolsMenu
            // 
            this.toolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enrollerToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.archivesToolStripMenuItem,
            this.recherchePointeuseToolStripMenuItem,
            this.connexionExterneToolStripMenuItem});
            this.toolsMenu.Name = "toolsMenu";
            this.toolsMenu.Size = new System.Drawing.Size(50, 20);
            this.toolsMenu.Text = "&Outils";
            // 
            // enrollerToolStripMenuItem
            // 
            this.enrollerToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.edit_user;
            this.enrollerToolStripMenuItem.Name = "enrollerToolStripMenuItem";
            this.enrollerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.enrollerToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.enrollerToolStripMenuItem.Text = "&Enroller";
            this.enrollerToolStripMenuItem.Click += new System.EventHandler(this.enrollerToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.settings;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // archivesToolStripMenuItem
            // 
            this.archivesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.appareilToolStripMenuItem,
            this.serveurToolStripMenuItem});
            this.archivesToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.folder;
            this.archivesToolStripMenuItem.Name = "archivesToolStripMenuItem";
            this.archivesToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.archivesToolStripMenuItem.Text = "Archives";
            // 
            // appareilToolStripMenuItem
            // 
            this.appareilToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.appareil;
            this.appareilToolStripMenuItem.Name = "appareilToolStripMenuItem";
            this.appareilToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.appareilToolStripMenuItem.Text = "Appareil";
            this.appareilToolStripMenuItem.Click += new System.EventHandler(this.appareilToolStripMenuItem_Click);
            // 
            // serveurToolStripMenuItem
            // 
            this.serveurToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.db;
            this.serveurToolStripMenuItem.Name = "serveurToolStripMenuItem";
            this.serveurToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.serveurToolStripMenuItem.Text = "Serveur";
            this.serveurToolStripMenuItem.Click += new System.EventHandler(this.serveurToolStripMenuItem_Click);
            // 
            // recherchePointeuseToolStripMenuItem
            // 
            this.recherchePointeuseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.voirResultatToolStripMenuItem});
            this.recherchePointeuseToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.search;
            this.recherchePointeuseToolStripMenuItem.Name = "recherchePointeuseToolStripMenuItem";
            this.recherchePointeuseToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.recherchePointeuseToolStripMenuItem.Text = "Recherche Pointeuse";
            this.recherchePointeuseToolStripMenuItem.Click += new System.EventHandler(this.recherchePointeuseToolStripMenuItem_Click);
            // 
            // voirResultatToolStripMenuItem
            // 
            this.voirResultatToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.document;
            this.voirResultatToolStripMenuItem.Name = "voirResultatToolStripMenuItem";
            this.voirResultatToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.voirResultatToolStripMenuItem.Text = "Voir Resultat";
            this.voirResultatToolStripMenuItem.Click += new System.EventHandler(this.voirResultatToolStripMenuItem_Click);
            // 
            // windowsMenu
            // 
            this.windowsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.employesToolStripMenuItem,
            this.evenementsToolStripMenuItem,
            this.empreintesToolStripMenuItem,
            this.toolStripSeparator7,
            this.administrationToolStripMenuItem});
            this.windowsMenu.Name = "windowsMenu";
            this.windowsMenu.Size = new System.Drawing.Size(59, 20);
            this.windowsMenu.Text = "&Gestion";
            // 
            // employesToolStripMenuItem
            // 
            this.employesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listeToolStripMenuItem,
            this.présenceToolStripMenuItem,
            this.testerToolStripMenuItem});
            this.employesToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.group_user;
            this.employesToolStripMenuItem.Name = "employesToolStripMenuItem";
            this.employesToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.employesToolStripMenuItem.Text = "Employes";
            // 
            // listeToolStripMenuItem
            // 
            this.listeToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.easymoblog;
            this.listeToolStripMenuItem.Name = "listeToolStripMenuItem";
            this.listeToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.listeToolStripMenuItem.Text = "Liste";
            this.listeToolStripMenuItem.Click += new System.EventHandler(this.listeToolStripMenuItem_Click);
            // 
            // présenceToolStripMenuItem
            // 
            this.présenceToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.test;
            this.présenceToolStripMenuItem.Name = "présenceToolStripMenuItem";
            this.présenceToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.présenceToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.présenceToolStripMenuItem.Text = "&Présence";
            this.présenceToolStripMenuItem.Click += new System.EventHandler(this.présenceToolStripMenuItem_Click);
            // 
            // testerToolStripMenuItem
            // 
            this.testerToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.signal_1;
            this.testerToolStripMenuItem.Name = "testerToolStripMenuItem";
            this.testerToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.testerToolStripMenuItem.Text = "Tester";
            this.testerToolStripMenuItem.Click += new System.EventHandler(this.testerToolStripMenuItem_Click);
            // 
            // evenementsToolStripMenuItem
            // 
            this.evenementsToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.history;
            this.evenementsToolStripMenuItem.Name = "evenementsToolStripMenuItem";
            this.evenementsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.evenementsToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.evenementsToolStripMenuItem.Text = "E&venements";
            this.evenementsToolStripMenuItem.Click += new System.EventHandler(this.evenementsToolStripMenuItem_Click);
            // 
            // empreintesToolStripMenuItem
            // 
            this.empreintesToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.flag;
            this.empreintesToolStripMenuItem.Name = "empreintesToolStripMenuItem";
            this.empreintesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.empreintesToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.empreintesToolStripMenuItem.Text = "E&mpreintes";
            this.empreintesToolStripMenuItem.Click += new System.EventHandler(this.empreintesToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(176, 6);
            // 
            // administrationToolStripMenuItem
            // 
            this.administrationToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.admin_icon;
            this.administrationToolStripMenuItem.Name = "administrationToolStripMenuItem";
            this.administrationToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.administrationToolStripMenuItem.Text = "Administration";
            this.administrationToolStripMenuItem.Click += new System.EventHandler(this.administrationToolStripMenuItem_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator8,
            this.aboutToolStripMenuItem});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(43, 20);
            this.helpMenu.Text = "&Aide";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(193, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.documentinfo;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.aboutToolStripMenuItem.Text = "&À propos de ZK Lymytz";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_rattach,
            this.tsb_actualise,
            this.toolStripSeparator2,
            this.tsb_new,
            this.tsb_update,
            this.tsb_delete,
            this.toolStripSeparator1,
            this.tsb_connect,
            this.tsb_deconnect,
            this.tsb_start,
            this.tsb_stop,
            this.toolStripSeparator6,
            this.tsb_restart,
            this.tsb_off});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(632, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "ToolStrip";
            // 
            // tsb_rattach
            // 
            this.tsb_rattach.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_rattach.Image = global::ZK_Lymytz.Properties.Resources.next;
            this.tsb_rattach.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_rattach.Name = "tsb_rattach";
            this.tsb_rattach.Size = new System.Drawing.Size(23, 22);
            this.tsb_rattach.Text = "Rattacher";
            this.tsb_rattach.Click += new System.EventHandler(this.tsb_rattach_Click);
            // 
            // tsb_actualise
            // 
            this.tsb_actualise.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_actualise.Image = global::ZK_Lymytz.Properties.Resources.connected;
            this.tsb_actualise.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_actualise.Name = "tsb_actualise";
            this.tsb_actualise.Size = new System.Drawing.Size(23, 22);
            this.tsb_actualise.Text = "Actualiser";
            this.tsb_actualise.Click += new System.EventHandler(this.tsb_actualise_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsb_new
            // 
            this.tsb_new.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_new.Image = global::ZK_Lymytz.Properties.Resources.add;
            this.tsb_new.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsb_new.Name = "tsb_new";
            this.tsb_new.Size = new System.Drawing.Size(23, 22);
            this.tsb_new.Text = "Nouveau";
            this.tsb_new.Click += new System.EventHandler(this.ShowNewForm);
            // 
            // tsb_update
            // 
            this.tsb_update.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_update.Image = global::ZK_Lymytz.Properties.Resources.easymoblog;
            this.tsb_update.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_update.Name = "tsb_update";
            this.tsb_update.Size = new System.Drawing.Size(23, 22);
            this.tsb_update.Text = "Modifier";
            this.tsb_update.Click += new System.EventHandler(this.tsb_update_Click);
            // 
            // tsb_delete
            // 
            this.tsb_delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_delete.Image = global::ZK_Lymytz.Properties.Resources.delete;
            this.tsb_delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_delete.Name = "tsb_delete";
            this.tsb_delete.Size = new System.Drawing.Size(23, 22);
            this.tsb_delete.Text = "Supprimer";
            this.tsb_delete.Click += new System.EventHandler(this.tsb_delete_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsb_connect
            // 
            this.tsb_connect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_connect.Image = global::ZK_Lymytz.Properties.Resources.irkickflash;
            this.tsb_connect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_connect.Name = "tsb_connect";
            this.tsb_connect.Size = new System.Drawing.Size(23, 22);
            this.tsb_connect.Text = "Connecter";
            this.tsb_connect.Click += new System.EventHandler(this.tsb_connect_Click);
            // 
            // tsb_deconnect
            // 
            this.tsb_deconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_deconnect.Image = global::ZK_Lymytz.Properties.Resources.irkick;
            this.tsb_deconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_deconnect.Name = "tsb_deconnect";
            this.tsb_deconnect.Size = new System.Drawing.Size(23, 22);
            this.tsb_deconnect.Text = "Déconnexion";
            this.tsb_deconnect.Click += new System.EventHandler(this.tsb_deconnect_Click);
            // 
            // tsb_start
            // 
            this.tsb_start.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_start.Image = global::ZK_Lymytz.Properties.Resources.player_play;
            this.tsb_start.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_start.Name = "tsb_start";
            this.tsb_start.Size = new System.Drawing.Size(23, 22);
            this.tsb_start.Text = "Démarrer Service";
            this.tsb_start.Click += new System.EventHandler(this.tsb_start_Click);
            // 
            // tsb_stop
            // 
            this.tsb_stop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_stop.Image = global::ZK_Lymytz.Properties.Resources.player_stop;
            this.tsb_stop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_stop.Name = "tsb_stop";
            this.tsb_stop.Size = new System.Drawing.Size(23, 22);
            this.tsb_stop.Text = "Arreter Service";
            this.tsb_stop.Click += new System.EventHandler(this.tsb_stop_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tsb_restart
            // 
            this.tsb_restart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_restart.Image = global::ZK_Lymytz.Properties.Resources.restart_1;
            this.tsb_restart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_restart.Name = "tsb_restart";
            this.tsb_restart.Size = new System.Drawing.Size(23, 22);
            this.tsb_restart.Text = "Rédemarrer";
            this.tsb_restart.Click += new System.EventHandler(this.tsb_restart_Click);
            // 
            // tsb_off
            // 
            this.tsb_off.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_off.Image = global::ZK_Lymytz.Properties.Resources.endturn;
            this.tsb_off.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_off.Name = "tsb_off";
            this.tsb_off.Size = new System.Drawing.Size(23, 22);
            this.tsb_off.Text = "Eteindre";
            this.tsb_off.Click += new System.EventHandler(this.tsb_off_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 431);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(632, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(27, 17);
            this.toolStripStatusLabel.Text = "État";
            // 
            // toolTip
            // 
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.ToolTipTitle = "Bienvenu";
            // 
            // context_bubble
            // 
            this.context_bubble.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.activerToolStripMenuItem,
            this.logsToolStripMenuItem,
            this.settingToolStripMenuItem,
            this.toolStripSeparator5,
            this.arrêterProcessusToolStripMenuItem,
            this.déconnectionToolStripMenuItem,
            this.redémarerToolStripMenuItem,
            this.exitToolStripMenuItem1});
            this.context_bubble.Name = "context_bubble";
            this.context_bubble.Size = new System.Drawing.Size(166, 164);
            // 
            // activerToolStripMenuItem
            // 
            this.activerToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.vue;
            this.activerToolStripMenuItem.Name = "activerToolStripMenuItem";
            this.activerToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.activerToolStripMenuItem.Text = "Afficher/Cacher";
            this.activerToolStripMenuItem.Click += new System.EventHandler(this.activerToolStripMenuItem_Click);
            // 
            // logsToolStripMenuItem
            // 
            this.logsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pingToolStripMenuItem});
            this.logsToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.easymoblog;
            this.logsToolStripMenuItem.Name = "logsToolStripMenuItem";
            this.logsToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.logsToolStripMenuItem.Text = "Logs";
            this.logsToolStripMenuItem.Click += new System.EventHandler(this.logsToolStripMenuItem_Click);
            // 
            // pingToolStripMenuItem
            // 
            this.pingToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.admin_icon;
            this.pingToolStripMenuItem.Name = "pingToolStripMenuItem";
            this.pingToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.pingToolStripMenuItem.Text = "Ping";
            this.pingToolStripMenuItem.Click += new System.EventHandler(this.pingToolStripMenuItem_Click);
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.settings;
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.settingToolStripMenuItem.Text = "Setting";
            this.settingToolStripMenuItem.Click += new System.EventHandler(this.settingToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(162, 6);
            // 
            // arrêterProcessusToolStripMenuItem
            // 
            this.arrêterProcessusToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.pause;
            this.arrêterProcessusToolStripMenuItem.Name = "arrêterProcessusToolStripMenuItem";
            this.arrêterProcessusToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.arrêterProcessusToolStripMenuItem.Text = "Arrêter Processus";
            this.arrêterProcessusToolStripMenuItem.Visible = false;
            this.arrêterProcessusToolStripMenuItem.Click += new System.EventHandler(this.arrêterProcessusToolStripMenuItem_Click);
            // 
            // déconnectionToolStripMenuItem
            // 
            this.déconnectionToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.logout;
            this.déconnectionToolStripMenuItem.Name = "déconnectionToolStripMenuItem";
            this.déconnectionToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.déconnectionToolStripMenuItem.Text = "Déconnection";
            this.déconnectionToolStripMenuItem.Click += new System.EventHandler(this.déconnectionToolStripMenuItem_Click);
            // 
            // redémarerToolStripMenuItem
            // 
            this.redémarerToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.restart;
            this.redémarerToolStripMenuItem.Name = "redémarerToolStripMenuItem";
            this.redémarerToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.redémarerToolStripMenuItem.Text = "Redémarer";
            this.redémarerToolStripMenuItem.Click += new System.EventHandler(this.redémarerToolStripMenuItem1_Click);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Image = global::ZK_Lymytz.Properties.Resources.exit;
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(165, 22);
            this.exitToolStripMenuItem1.Text = "Quitter";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.ExitToolsStripMenuItem_Click);
            // 
            // bubble
            // 
            this.bubble.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.bubble.BalloonTipText = "Bienvenu";
            this.bubble.ContextMenuStrip = this.context_bubble;
            this.bubble.Icon = ((System.Drawing.Icon)(resources.GetObject("bubble.Icon")));
            this.bubble.Text = "ZK-Lymytz";
            this.bubble.Visible = true;
            this.bubble.DoubleClick += new System.EventHandler(this.activerToolStripMenuItem_Click);
            // 
            // grp_pointeuse
            // 
            this.grp_pointeuse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grp_pointeuse.Controls.Add(this.dgv_pointeuse);
            this.grp_pointeuse.Location = new System.Drawing.Point(2, 52);
            this.grp_pointeuse.Name = "grp_pointeuse";
            this.grp_pointeuse.Size = new System.Drawing.Size(630, 272);
            this.grp_pointeuse.TabIndex = 12;
            this.grp_pointeuse.TabStop = false;
            this.grp_pointeuse.Text = "Liste Appareil";
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
            this.port,
            this.emplacement,
            this.imachine,
            this.connect,
            this.actif,
            this.icon});
            this.dgv_pointeuse.ContextMenuStrip = this.context_dgv_pointeuse;
            this.dgv_pointeuse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_pointeuse.Location = new System.Drawing.Point(3, 16);
            this.dgv_pointeuse.Name = "dgv_pointeuse";
            this.dgv_pointeuse.ReadOnly = true;
            this.dgv_pointeuse.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_pointeuse.Size = new System.Drawing.Size(624, 253);
            this.dgv_pointeuse.TabIndex = 0;
            this.dgv_pointeuse.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_pointeuse_CellFormatting);
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
            this.adresse.FillWeight = 104.4721F;
            this.adresse.HeaderText = "Adresse";
            this.adresse.Name = "adresse";
            this.adresse.ReadOnly = true;
            // 
            // port
            // 
            this.port.FillWeight = 70.71317F;
            this.port.HeaderText = "Port";
            this.port.Name = "port";
            this.port.ReadOnly = true;
            // 
            // emplacement
            // 
            this.emplacement.FillWeight = 221.7335F;
            this.emplacement.HeaderText = "Emplacement";
            this.emplacement.Name = "emplacement";
            this.emplacement.ReadOnly = true;
            // 
            // imachine
            // 
            this.imachine.FillWeight = 74.00866F;
            this.imachine.HeaderText = "IMachine";
            this.imachine.Name = "imachine";
            this.imachine.ReadOnly = true;
            // 
            // connect
            // 
            this.connect.FillWeight = 70F;
            this.connect.HeaderText = "Connected";
            this.connect.Name = "connect";
            this.connect.ReadOnly = true;
            // 
            // actif
            // 
            this.actif.FillWeight = 50F;
            this.actif.HeaderText = "Actif";
            this.actif.Name = "actif";
            this.actif.ReadOnly = true;
            // 
            // icon
            // 
            this.icon.FillWeight = 20F;
            this.icon.HeaderText = "";
            this.icon.Name = "icon";
            this.icon.ReadOnly = true;
            // 
            // context_dgv_pointeuse
            // 
            this.context_dgv_pointeuse.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_active,
            this.chargerFichierTamponToolStripMenuItem});
            this.context_dgv_pointeuse.Name = "context_dgv_pointeuse";
            this.context_dgv_pointeuse.Size = new System.Drawing.Size(203, 48);
            // 
            // tsmi_active
            // 
            this.tsmi_active.Image = global::ZK_Lymytz.Properties.Resources.vue;
            this.tsmi_active.Name = "tsmi_active";
            this.tsmi_active.Size = new System.Drawing.Size(202, 22);
            this.tsmi_active.Text = "Activer";
            this.tsmi_active.Click += new System.EventHandler(this.tsmi_active_Click);
            // 
            // chargerFichierTamponToolStripMenuItem
            // 
            this.chargerFichierTamponToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources._in;
            this.chargerFichierTamponToolStripMenuItem.Name = "chargerFichierTamponToolStripMenuItem";
            this.chargerFichierTamponToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.chargerFichierTamponToolStripMenuItem.Text = "Charger Fichier Tampon";
            this.chargerFichierTamponToolStripMenuItem.Click += new System.EventHandler(this.chargerFichierTamponToolStripMenuItem_Click);
            // 
            // panel_report
            // 
            this.panel_report.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_report.Controls.Add(this.lv_report);
            this.panel_report.Location = new System.Drawing.Point(3, 327);
            this.panel_report.Name = "panel_report";
            this.panel_report.Size = new System.Drawing.Size(624, 100);
            this.panel_report.TabIndex = 13;
            // 
            // lv_report
            // 
            this.lv_report.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lv_report.ContextMenuStrip = this.context_log;
            this.lv_report.FormattingEnabled = true;
            this.lv_report.Location = new System.Drawing.Point(3, 3);
            this.lv_report.Name = "lv_report";
            this.lv_report.Size = new System.Drawing.Size(618, 95);
            this.lv_report.TabIndex = 0;
            // 
            // context_log
            // 
            this.context_log.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.effacerToolStripMenuItem});
            this.context_log.Name = "context_log";
            this.context_log.Size = new System.Drawing.Size(111, 26);
            // 
            // effacerToolStripMenuItem
            // 
            this.effacerToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.clean;
            this.effacerToolStripMenuItem.Name = "effacerToolStripMenuItem";
            this.effacerToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.effacerToolStripMenuItem.Text = "Effacer";
            this.effacerToolStripMenuItem.Click += new System.EventHandler(this.effacerToolStripMenuItem_Click);
            // 
            // connexionExterneToolStripMenuItem
            // 
            this.connexionExterneToolStripMenuItem.Image = global::ZK_Lymytz.Properties.Resources.irkickflash;
            this.connexionExterneToolStripMenuItem.Name = "connexionExterneToolStripMenuItem";
            this.connexionExterneToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.connexionExterneToolStripMenuItem.Text = "Connexion Externe";
            this.connexionExterneToolStripMenuItem.Click += new System.EventHandler(this.connexionExterneToolStripMenuItem_Click);
            // 
            // Form_Parent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 453);
            this.Controls.Add(this.panel_report);
            this.Controls.Add(this.grp_pointeuse);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(648, 492);
            this.Name = "Form_Parent";
            this.Text = "From_Parent";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.Form_Parent_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Parent_FormClosing);
            this.Load += new System.EventHandler(this.Form_Parent_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.context_bubble.ResumeLayout(false);
            this.grp_pointeuse.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_pointeuse)).EndInit();
            this.context_dgv_pointeuse.ResumeLayout(false);
            this.panel_report.ResumeLayout(false);
            this.context_log.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewMenu;
        private System.Windows.Forms.ToolStripMenuItem toolBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem statusBarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsMenu;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsMenu;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripButton tsb_new;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ContextMenuStrip context_bubble;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        public System.Windows.Forms.NotifyIcon bubble;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem redémarerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem redémarerToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem activerToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton tsb_actualise;
        private System.Windows.Forms.ToolStripButton tsb_delete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsb_update;
        private System.Windows.Forms.ToolStripButton tsb_start;
        private System.Windows.Forms.ToolStripButton tsb_stop;
        private System.Windows.Forms.ToolStripButton tsb_restart;
        private System.Windows.Forms.ToolStripButton tsb_connect;
        private System.Windows.Forms.ToolStripMenuItem archivesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem appareilToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serveurToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enrollerToolStripMenuItem;
        private System.Windows.Forms.GroupBox grp_pointeuse;
        private System.Windows.Forms.DataGridView dgv_pointeuse;
        private System.Windows.Forms.Panel panel_report;
        private System.Windows.Forms.ContextMenuStrip context_dgv_pointeuse;
        private System.Windows.Forms.ToolStripMenuItem tsmi_active;
        public System.Windows.Forms.ListBox lv_report;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsb_off;
        private System.Windows.Forms.ToolStripButton tsb_deconnect;
        private System.Windows.Forms.ToolStripButton tsb_rattach;
        private System.Windows.Forms.ToolStripMenuItem employesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem evenementsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem administrationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem empreintesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem arrêterProcessusToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip context_log;
        private System.Windows.Forms.ToolStripMenuItem effacerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testerToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem déconnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem présenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pingToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn adresse;
        private System.Windows.Forms.DataGridViewTextBoxColumn port;
        private System.Windows.Forms.DataGridViewTextBoxColumn emplacement;
        private System.Windows.Forms.DataGridViewTextBoxColumn imachine;
        private System.Windows.Forms.DataGridViewCheckBoxColumn connect;
        private System.Windows.Forms.DataGridViewCheckBoxColumn actif;
        private System.Windows.Forms.DataGridViewImageColumn icon;
        private System.Windows.Forms.ToolStripMenuItem chargerFichierTamponToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recherchePointeuseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem voirResultatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pingToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem connexionExterneToolStripMenuItem;
    }
}



