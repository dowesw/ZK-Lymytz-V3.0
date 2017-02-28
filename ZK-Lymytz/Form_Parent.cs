using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.IO;

using ZK_Lymytz.ENTITE;
using ZK_Lymytz.BLL;
using ZK_Lymytz.IHM;
using ZK_Lymytz.TOOLS;

namespace ZK_Lymytz
{
    public partial class Form_Parent : Form
    {
        public Pointeuse currentPointeuse = new Pointeuse();
        private static volatile bool _pointeuseThread = true, _restartThread = true, _stopThread = true;
        public ObjectThread object_pointeuse;

        public Form_Parent()
        {
            InitializeComponent();
            object_pointeuse = new ObjectThread(dgv_pointeuse);
        }

        private void Form_Parent_Load(object sender, EventArgs e)
        {
            bubble.Text = Constantes.APP_NAME;
            LoadConfig();
            LoadPointeuse();
        }

        private void LoadConfig()
        {
            this.Text = Constantes.APP_NAME;
            fileMenu.Text = Mots.Fichier;
            viewMenu.Text = Mots.Affichage;
            toolsMenu.Text = Mots.Outil;
            windowsMenu.Text = Mots.Gestion;
            helpMenu.Text = Mots.Aide;
            newToolStripMenuItem.Text = Mots.Nouveau;
            tsb_new.Text = Mots.Nouveau;
            openToolStripMenuItem.Text = Mots.Actualiser;
            exitToolStripMenuItem.Text = Mots.Quitter;
            exitToolStripMenuItem1.Text = Mots.Quitter;
            toolStripStatusLabel.Text = Mots.Etat;
            toolBarToolStripMenuItem.Text = Mots.BarOutil;
            statusBarToolStripMenuItem.Text = Mots.BarEtat;
            optionsToolStripMenuItem.Text = Mots.Parametre;
            activerToolStripMenuItem.Text = Mots.Activer;
            settingToolStripMenuItem.Text = Mots.Parametre;
            aboutToolStripMenuItem.Text = Mots.A_Propos;
            redémarerToolStripMenuItem.Text = Mots.Restart;
            redémarerToolStripMenuItem1.Text = Mots.Restart;
            activerToolStripMenuItem.Text = Constantes.ACTIVE ? Mots.Cacher : Mots.Afficher;
            déconnectionToolStripMenuItem.Visible = Constantes.SETTING.CheckConnect;
        }

        public void AddPointeuse(Pointeuse p)
        {
            object_pointeuse.WriteDataGridView(new object[] { p.Id, p.Ip, p.Port, p.Emplacement, p.IMachine, p.Connecter, p.Actif, p.Icon() });
        }

        public void UpdatePointeuse(Pointeuse p)
        {
            int i = Utils.GetRowData(dgv_pointeuse, p.Id);
            if (i > -1)
            {
                object_pointeuse.RemoveDataGridView(i);
            }
            else
            {
                i = 0;
            }
            object_pointeuse.WriteDataGridView(i, new object[] { p.Id, p.Ip, p.Port, p.Emplacement, p.IMachine, p.Connecter, p.Actif, p.Icon() });
            ResetDataPointeuse();
            dgv_pointeuse.Rows[i].Selected = true;
            currentPointeuse = p;
        }

        public void DeletePointeuse(Pointeuse p)
        {
            int i = Utils.GetRowData(dgv_pointeuse, p.Id);
            if (i > -1)
            {
                object_pointeuse.RemoveDataGridView(i);
                p = null;
            }
        }

        public void LoadPointeuse()
        {
            object_pointeuse.ClearDataGridView(true);
            if (Constantes.POINTEUSES != null ? Constantes.POINTEUSES.Count < 1 : true)
            {
                Societe s = SocieteBLL.ReturnSociete();
                Constantes.POINTEUSES = PointeuseBLL.List("select * from yvs_pointeuse where societe = " + s.Id + " order by adresse_ip");
            }
            foreach (Pointeuse p in Constantes.POINTEUSES)
            {
                AddPointeuse(p);
            }
            ResetDataPointeuse();
            Setting i = SettingBLL.ReturnSetting();
            if (i.AutoRattach)
            {
                Utils.SetZkemkeeper();
            }
        }

        public void LoadPointeuse_()
        {
            if (!_restartThread || !_stopThread)
            {
                Utils.WriteLog("Veuillez patientez quelques instants");
            }

            _pointeuseThread = false;
            while (!_pointeuseThread)
            {
                Societe s = SocieteBLL.ReturnSociete();
                Constantes.POINTEUSES = PointeuseBLL.List("select * from yvs_pointeuse where societe = " + s.Id + " order by adresse_ip");
                Utils.SetZkemkeeper();
                foreach (Pointeuse p in Constantes.POINTEUSES)
                {
                    ObjectThread o = new ObjectThread(dgv_pointeuse);
                    o.WriteDataGridView(new object[] { p.Id, p.Ip, p.Port, p.Emplacement, p.IMachine, p.Connecter, p.Actif });
                }
                ResetDataPointeuse();
                _pointeuseThread = true;
            }
        }

        public void ResetDataPointeuse()
        {
            for (int i = 0; i < dgv_pointeuse.Rows.Count; i++)
            {
                dgv_pointeuse.Rows[i].Selected = false;
            }
            currentPointeuse = null;
        }

        private void Populate(Pointeuse p)
        {
            currentPointeuse = new Pointeuse();
            currentPointeuse.Actif = p.Actif;
            currentPointeuse.Connecter = p.Connecter;
            currentPointeuse.Description = p.Description;
            currentPointeuse.Emplacement = p.Emplacement;
            currentPointeuse.Id = p.Id;
            currentPointeuse.IMachine = p.IMachine;
            currentPointeuse.Ip = p.Ip;
            currentPointeuse.Port = p.Port;
            currentPointeuse.Societe = p.Societe;
            currentPointeuse.Zkemkeeper = p.Zkemkeeper;
            currentPointeuse.MultiSociete = p.MultiSociete;
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            if (Constantes.FORM_ADD_POINTEUSE == null)
            {
                Form_Pointeuse f = new Form_Pointeuse();
                f.Show();
                Constantes.FORM_ADD_POINTEUSE = f;
                Utils.WriteLog("Ouverture page (Ajout Appareil)");
                Utils.addFrom("Form_Pointeuse_I");
            }
            else
            {
                Constantes.FORM_ADD_POINTEUSE.WindowState = FormWindowState.Normal;
                Constantes.FORM_ADD_POINTEUSE.BringToFront();
            }
        }

        private bool VerifyActionPointeuse()
        {
            if (Constantes.FORM_UPD_POINTEUSE != null)
            {
                if (Constantes.FORM_UPD_POINTEUSE.pointeuse.Id == currentPointeuse.Id)
                {
                    Utils.WriteLog("Vous avez ouvert une page de modification sur l'appareil " + currentPointeuse.Ip + "....Terminer d'abord le traitement de cet appareil");
                    return false;
                }
            }
            if (Constantes.FORM_ADD_EMPREINTE != null)
            {
                if (Constantes.FORM_ADD_EMPREINTE.pointeuse.Id == currentPointeuse.Id)
                {
                    Utils.WriteLog("Vous avez ouvert une page d'ajout des empreinte sur l'appareil " + currentPointeuse.Ip + "....Terminer d'abord le traitement de cet appareil");
                    return false;
                }
            }
            if (Constantes.FORM_EMPLOYE != null)
            {
                if (Constantes.FORM_EMPLOYE.pointeuse.Id == currentPointeuse.Id)
                {
                    Utils.WriteLog("Vous avez ouvert une page de gestion des employés sur l'appareil " + currentPointeuse.Ip + "....Terminer d'abord le traitement de cet appareil");
                    return false;
                }
            }
            if (Constantes.FORM_GESTION_POINTEUSE != null)
            {
                if (Constantes.FORM_GESTION_POINTEUSE.pointeuse.Id == currentPointeuse.Id)
                {
                    Utils.WriteLog("Vous avez ouvert une page de gestion appareil sur l'appareil " + currentPointeuse.Ip + "....Terminer d'abord le traitement de cet appareil");
                    return false;
                }
            }
            return true;
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Constantes.SETTING != null ? Constantes.SETTING.CheckConnect : false)
            {
                if (Constantes.USERS != null ? Constantes.USERS.Id < 1 : false)
                {
                    Form_Login login = new Form_Login(22);
                    login.Show();
                }
                else
                {
                    Exit();
                }
            }
            else
            {
                Exit();
            }
        }

        public void Exit()
        {
            Utils.WriteLog("Demande de fermeture de l'application");
            if (Messages.Confirmation(Mots.Msg_FermerApplication.ToLower()) == System.Windows.Forms.DialogResult.Yes)
            {
                Utils.WriteLog("-- Fermeture de l'application effectuée");
                Utils.WriteLog("FERMETURE DE L'APPLICATION.....");
                Environment.Exit(0);
            }
            else
            {
                Utils.WriteLog("-- Fermeture de l'application annulée");
            }
        }


        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void Form_Parent_Activated(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                //childForm.MdiParent = this;
            }
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void activerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Constantes.SETTING != null ? Constantes.SETTING.CheckConnect : false)
            {
                if (Constantes.USERS != null ? Constantes.USERS.Id < 1 : false)
                {
                    Form_Login login = new Form_Login(0);
                    login.Show();
                }
                else
                {
                    Open();
                }
            }
            else
            {
                Open();
            }
        }

        public void Open()
        {
            if (!Constantes.ACTIVE)
            {
                if (Constantes.FORM_PARENT == null)
                {
                    Form_Parent f_parent = new Form_Parent();
                    Constantes.FORM_PARENT = f_parent;
                    f_parent.Show();
                }
                else
                {
                    Constantes.FORM_PARENT.Show();
                }
            }
            else
            {
                if (Constantes.FORM_PARENT != null)
                {
                    Constantes.FORM_PARENT.Hide();
                }

            }
            Constantes.ACTIVE = !Constantes.ACTIVE;
            activerToolStripMenuItem.Text = Constantes.ACTIVE ? Mots.Cacher : Mots.Afficher;
            activerToolStripMenuItem.Image = Constantes.ACTIVE ? global::ZK_Lymytz.Properties.Resources.no_vue : global::ZK_Lymytz.Properties.Resources.vue;
            if (Constantes.ACTIVE)
            {
                Utils.OpenForm();
            }
            else
            {
                Utils.CloseForm();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Form_Propos f_propos = new Form_Propos();
            //if (Constantes.f_propos == null)
            //{
            //    f_propos.MdiParent = this;
            //    f_propos.Show();
            //    Constantes.f_propos = f_propos;
            //}
            //else
            //{
            //    Constantes.f_propos.WindowState = FormWindowState.Normal;
            //    Constantes.f_propos.BringToFront();
            //}
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Constantes.SETTING != null ? Constantes.SETTING.CheckConnect : false)
            {
                if (Constantes.USERS != null ? Constantes.USERS.Id < 1 : false)
                {
                    Form_Login login = new Form_Login(1);
                    login.Show();
                }
                else
                {
                    OpenFormSetting();
                }
            }
            else
            {
                OpenFormSetting();
            }
        }

        public void OpenFormSetting()
        {
            Constantes.ACTIVE = true;
            Constantes.FORM_PARENT.Show();
            Constantes.FORM_PARENT.activerToolStripMenuItem.Text = Constantes.ACTIVE ? Mots.Cacher : Mots.Afficher;
            Constantes.FORM_PARENT.activerToolStripMenuItem.Image = Constantes.ACTIVE ? global::ZK_Lymytz.Properties.Resources.no_vue : global::ZK_Lymytz.Properties.Resources.vue;

            if (Constantes.FORM_SETTING == null)
            {
                Form_Setting f_setting = new Form_Setting();
                f_setting.Show();
                Constantes.FORM_SETTING = f_setting;
                Utils.addFrom("Form_Setting");
            }
            else
            {
                Constantes.FORM_SETTING.WindowState = FormWindowState.Normal;
                Constantes.FORM_SETTING.BringToFront();
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Constantes.FORM_SETTING == null)
            {
                Form_Setting f_setting = new Form_Setting();
                f_setting.Show();
                Constantes.FORM_SETTING = f_setting;
                Utils.WriteLog("Ouverture page (Paramètres)");
                Utils.addFrom("Form_Setting");
            }
            else
            {
                Constantes.FORM_SETTING.WindowState = FormWindowState.Normal;
                Constantes.FORM_SETTING.BringToFront();
            }
        }

        private void Form_Parent_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Constantes.ACTIVE = false;
            activerToolStripMenuItem.Text = Constantes.ACTIVE ? Mots.Cacher : Mots.Afficher;
            activerToolStripMenuItem.Image = Constantes.ACTIVE ? global::ZK_Lymytz.Properties.Resources.no_vue : global::ZK_Lymytz.Properties.Resources.vue;
            Utils.CloseForm();
            this.Hide();
        }


        private void redémarerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Constantes.SETTING != null ? Constantes.SETTING.CheckConnect : false)
            {
                if (Constantes.USERS != null ? Constantes.USERS.Id < 1 : false)
                {
                    Form_Login login = new Form_Login(21);
                    login.Show();
                }
                else
                {
                    Restart();
                }
            }
            else
            {
                Restart();
            }
        }

        public void Restart()
        {
            Utils.WriteLog("Demande du redemarrage de l'application");
            if (Messages.Confirmation(Mots.Restart.ToLower()) == System.Windows.Forms.DialogResult.Yes)
            {
                Application.ExitThread();
                Utils.WriteLog("-- Redemarrage de l'application effectué");
                Application.Restart();
            }
            else
            {
                Utils.WriteLog("-- Redemarrage de l'application annulé");
            }
        }

        private void dgv_pointeuse_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo info = dgv_pointeuse.HitTest(e.X, e.Y); //get info
            int pos = dgv_pointeuse.HitTest(e.X, e.Y).RowIndex;
            if (pos > -1)
            {
                if (dgv_pointeuse.Rows[pos].Cells["id"].Value != null)
                {
                    Int32 id = (Int32)dgv_pointeuse.Rows[pos].Cells["id"].Value;
                    if (id > 0)
                    {
                        Pointeuse f = Constantes.POINTEUSES.Find(x => x.Id == id);
                        switch (e.Button)
                        {
                            case MouseButtons.Right:
                                {
                                    ResetDataPointeuse();
                                    dgv_pointeuse.Rows[pos].Selected = true; //Select the row
                                    Populate(f);
                                    tsmi_active.Text = f.Actif ? "Désactiver" : "Activer";
                                    tsmi_active.Image = f.Actif ? global::ZK_Lymytz.Properties.Resources.no_vue : global::ZK_Lymytz.Properties.Resources.vue;
                                }
                                break;
                            default:
                                Populate(f);
                                break;
                        }
                        if (currentPointeuse != null ? currentPointeuse.Id > 0 : false)
                        {
                            Utils.WriteLog("Pointeuse " + currentPointeuse.Ip + " selectionnée");
                        }
                    }
                }
            }
        }

        private void tsmi_active_Click(object sender, EventArgs e)
        {
            Utils.WriteLog("Demande" + (currentPointeuse.Actif ? " de désactivation" : " d'activation ") + " de la pointeuse " + currentPointeuse.Ip + "");
            if (Messages.Confirmation(currentPointeuse.Actif ? "désactiver" : "activer") == System.Windows.Forms.DialogResult.Yes)
            {
                currentPointeuse.Actif = !currentPointeuse.Actif;
                if (PointeuseBLL.ActifById(currentPointeuse.Id, currentPointeuse.Actif))
                {
                    Utils.WriteLog("-- Pointeuse " + currentPointeuse.Ip + " " + (!currentPointeuse.Actif ? " désactivée" : " activée "));
                    UpdatePointeuse(currentPointeuse);
                }
            }
            else
            {
                Utils.WriteLog("-- " + (currentPointeuse.Actif ? " Désactivation" : " Activation ") + " de la pointeuse " + currentPointeuse.Ip + " annulée");
            }
        }

        private void tsb_actualise_Click(object sender, EventArgs e)
        {
            Utils.WriteLog("Actualisation des appareils");
            LoadPointeuse();
        }

        private void tsb_update_Click(object sender, EventArgs e)
        {
            if (currentPointeuse != null ? currentPointeuse.Id > 0 : false)
            {
                if (Constantes.FORM_UPD_POINTEUSE == null)
                {
                    Form_Pointeuse f = new Form_Pointeuse(currentPointeuse);
                    f.Text = "Modifier Appareil : " + currentPointeuse.Ip;
                    Constantes.FORM_UPD_POINTEUSE = f;
                    f.Show();
                    Utils.addFrom("Form_Pointeuse_U");
                }
                else
                {
                    if (Constantes.FORM_UPD_POINTEUSE.pointeuse.Id != currentPointeuse.Id)
                    {
                        Utils.WriteLog("Vous avez ouvert une page de modification sur l'appareil " + Constantes.FORM_UPD_POINTEUSE.pointeuse.Ip + "....Terminer d'abord le traitement de cet appareil");
                        return;
                    }
                    Constantes.FORM_UPD_POINTEUSE.WindowState = FormWindowState.Normal;
                    Constantes.FORM_UPD_POINTEUSE.BringToFront();
                }
                Utils.WriteLog("Ouverture page (Modification Appareil : " + currentPointeuse.Ip + ")");
            }
            else
            {
                Utils.WriteLog("Vous devez selectionner l'appareil");
            }
        }

        private void tsb_delete_Click(object sender, EventArgs e)
        {
            if (currentPointeuse != null ? currentPointeuse.Id > 0 : false)
            {
                if (!VerifyActionPointeuse())
                {
                    return;
                }

                Utils.WriteLog("Demande de la suppresion de l'appareil : " + currentPointeuse.Ip + "");
                if (Messages.Confirmation_Infos("supprimer") == System.Windows.Forms.DialogResult.Yes)
                {
                    if (PointeuseBLL.Delete(currentPointeuse.Id))
                    {
                        DeletePointeuse(currentPointeuse);
                        Utils.WriteLog("-- Suppresion de l'appareil : " + currentPointeuse.Ip + " effectuée");
                        Constantes.POINTEUSES.RemoveAt(Constantes.POINTEUSES.FindIndex(x => x.Id == currentPointeuse.Id));
                        ResetDataPointeuse();
                    }
                }
                else
                {
                    Utils.WriteLog("-- Suppresion de l'appareil : " + currentPointeuse.Ip + " annulée");
                }
            }
            else
            {
                Utils.WriteLog("Vous devez selectionner l'appareil");
            }
        }

        private void tsb_restart_Click(object sender, EventArgs e)
        {
            if (currentPointeuse != null ? currentPointeuse.Id > 0 : false)
            {
                if (!VerifyActionPointeuse())
                {
                    return;
                }

                if (currentPointeuse.Connecter)
                {
                    Utils.WriteLog("Demande de redemarrage de l'appareil : " + currentPointeuse.Ip + "");
                    if (Messages.Confirmation("redémarrer") == System.Windows.Forms.DialogResult.Yes)
                    {
                        Appareil z = Utils.ReturnAppareil(currentPointeuse);
                        if (z == null)
                        {
                            PointeuseBLL.Deconnect(currentPointeuse.Id);
                            currentPointeuse.Connecter = false;
                            Utils.WriteLog("-- Veuillez connecter l'appareil : " + currentPointeuse.Ip + "");
                            UpdatePointeuse(currentPointeuse);
                            return;
                        }
                        bool b = z.Restart(currentPointeuse.Connecter, currentPointeuse.IMachine);
                        if (b)
                        {
                            PointeuseBLL.Deconnect(currentPointeuse.Id);
                            currentPointeuse.Connecter = false;
                            UpdatePointeuse(currentPointeuse);
                            Utils.WriteLog("-- Redémarrage de l'appareil : " + currentPointeuse.Ip + " effectuée");
                        }
                        else
                        {
                            Utils.WriteLog("-- Redémarrage de l'appareil : " + currentPointeuse.Ip + " impossible");
                        }
                    }
                    else
                    {
                        Utils.WriteLog("-- Redémarrage de l'appareil : " + currentPointeuse.Ip + " annulé");
                    }
                }
                else
                {
                    Utils.WriteLog("Vous devez connecter l'appareil : " + currentPointeuse.Ip);
                }
            }
            else
            {
                Utils.WriteLog("Vous devez selectionner l'appareil");
            }
        }

        private void tsb_off_Click(object sender, EventArgs e)
        {
            if (currentPointeuse != null ? currentPointeuse.Id > 0 : false)
            {
                if (!VerifyActionPointeuse())
                {
                    return;
                }

                if (currentPointeuse.Connecter)
                {
                    Utils.WriteLog("Demande d'arrêt total de l'appareil : " + currentPointeuse.Ip + "");
                    if (Messages.Confirmation("arrêter") == System.Windows.Forms.DialogResult.Yes)
                    {
                        Appareil z = Utils.ReturnAppareil(currentPointeuse);
                        if (z == null)
                        {
                            PointeuseBLL.Deconnect(currentPointeuse.Id);
                            currentPointeuse.Connecter = false;
                            Utils.WriteLog("-- Veuillez connecter l'appareil : " + currentPointeuse.Ip + "");
                            UpdatePointeuse(currentPointeuse);
                            return;
                        }
                        bool b = z.Stop(currentPointeuse.Connecter, currentPointeuse.IMachine);
                        if (b)
                        {
                            PointeuseBLL.Deconnect(currentPointeuse.Id);
                            currentPointeuse.Connecter = false;
                            UpdatePointeuse(currentPointeuse);
                            Utils.WriteLog("-- Arrêt de l'appareil : " + currentPointeuse.Ip + " effectuée");
                        }
                        else
                        {
                            Utils.WriteLog("-- Arrêt de l'appareil : " + currentPointeuse.Ip + " impossible");
                        }
                    }
                    else
                    {
                        Utils.WriteLog("-- Arrêt de l'appareil : " + currentPointeuse.Ip + " annulé");
                    }
                }
                else
                {
                    Utils.WriteLog("Vous devez connecter l'appareil : " + currentPointeuse.Ip);
                }
            }
            else
            {
                Utils.WriteLog("Vous devez selectionner l'appareil");
            }
        }

        private void tsb_deconnect_Click(object sender, EventArgs e)
        {
            if (currentPointeuse != null ? currentPointeuse.Id > 0 : false)
            {
                if (!VerifyActionPointeuse())
                {
                    return;
                }

                Utils.WriteLog("Demande de déconnexion de l'appareil : " + currentPointeuse.Ip + "");
                if (Messages.Confirmation("déconnecter") == System.Windows.Forms.DialogResult.Yes)
                {
                    if (PointeuseBLL.Deconnect(currentPointeuse.Id))
                    {
                        currentPointeuse.Connecter = false;
                        currentPointeuse.IMachine = 1;
                        currentPointeuse.Zkemkeeper = null;

                        Utils.WriteLog("-- Déconnexion de l'appareil : " + currentPointeuse.Ip + " effectuée");
                        Constantes.POINTEUSES.Find(x => x.Id == currentPointeuse.Id).Zkemkeeper = null;
                        UpdatePointeuse(currentPointeuse);
                    }
                }
                else
                {
                    Utils.WriteLog("-- Déconnexion de l'appareil : " + currentPointeuse.Ip + " annulée");
                }
            }
            else
            {
                Utils.WriteLog("Vous devez selectionner l'appareil");
            }
        }

        private void tsb_connect_Click(object sender, EventArgs e)
        {
            if (currentPointeuse != null ? currentPointeuse.Id > 0 : false)
            {
                if (!VerifyActionPointeuse())
                {
                    return;
                }

                Utils.WriteLog("Demande connexion de l'appareil : " + currentPointeuse.Ip + "");
                if (Messages.Confirmation("connecter") == System.Windows.Forms.DialogResult.Yes)
                {
                    Appareil z = new Appareil();
                    bool b = z.ConnectNet(currentPointeuse.Ip, currentPointeuse.Port);
                    if (b)
                    {
                        z.RegEvent(z._I_MACHINE_NUMBER);
                        if (PointeuseBLL.Connect(currentPointeuse.Id, z._I_MACHINE_NUMBER))
                        {
                            currentPointeuse.IMachine = z._I_MACHINE_NUMBER;
                            currentPointeuse.Zkemkeeper = z;

                            Utils.WriteLog("-- Connexion de l'appareil : " + currentPointeuse.Ip + " effectuée");
                            Constantes.POINTEUSES.Find(x => x.Id == currentPointeuse.Id).Zkemkeeper = z;
                            UpdatePointeuse(currentPointeuse);
                        }
                    }
                    else
                    {
                        Utils.WriteLog("-- Connexion de l'appareil : " + currentPointeuse.Ip + " impossible");
                    }
                }
                else
                {
                    Utils.WriteLog("-- Connexion de l'appareil : " + currentPointeuse.Ip + " annulée");
                }
            }
            else
            {
                Utils.WriteLog("Vous devez selectionner l'appareil");
            }
        }

        private void tsb_rattach_Click(object sender, EventArgs e)
        {
            Utils.WriteLog("Début du rattachement des logs des appareils");
            Fonctions.LoadFileTamponPointeuses(1, true);
        }

        private void tsb_stop_Click(object sender, EventArgs e)
        {
            if (currentPointeuse != null ? currentPointeuse.Id > 0 : false)
            {
                Appareil z = Utils.ReturnAppareil(currentPointeuse);
                if (z != null)
                {
                    Utils.WriteLog("Demande d'arret du service de l'appareil " + currentPointeuse.Ip);
                    if (Messages.Confirmation("arreter le service") == System.Windows.Forms.DialogResult.Yes)
                    {
                        z.StopOneDirect();
                        Utils.DestroyZkemkeeper(currentPointeuse);
                        currentPointeuse.Zkemkeeper = null;
                        UpdatePointeuse(currentPointeuse);
                        Utils.WriteLog("-- Arret du service de l'appareil " + currentPointeuse.Ip + " effectué");
                    }
                    else
                    {
                        Utils.WriteLog("-- Arret du service de l'appareil " + currentPointeuse.Ip + " annulé");
                    }
                }
            }
            else
            {
                Utils.WriteLog("Vous devez selectionner l'appareil");
            }

        }

        private void tsb_start_Click(object sender, EventArgs e)
        {
            if (currentPointeuse != null ? currentPointeuse.Id > 0 : false)
            {
                Utils.WriteLog("Demande de démarrage du service de l'appareil : " + currentPointeuse.Ip + "");
                if (Messages.Confirmation("démarrer service") == System.Windows.Forms.DialogResult.Yes)
                {
                    Appareil z = Utils.ReturnAppareil(currentPointeuse);
                    Utils.VerifyZkemkeeper(ref z, ref currentPointeuse);
                    if (z == null)
                    {
                        Utils.WriteLog("-- La liaison avec l'appareil " + currentPointeuse.Ip + " est corrompue");
                        return;
                    }
                    currentPointeuse.Zkemkeeper = z;
                    if (z.StartOneDirect())
                    {
                        UpdatePointeuse(currentPointeuse);
                        Utils.WriteLog("-- Démarrage du service de l'appareil : " + currentPointeuse.Ip + " effectuée");
                    }
                    else
                    {
                        Utils.WriteLog("-- Démarrage du service de l'appareil : " + currentPointeuse.Ip + " impossible");
                    }
                }
                else
                {
                    Utils.WriteLog("-- Démarrage de l'appareil : " + currentPointeuse.Ip + " annulé");
                }
            }
            else
            {
                Utils.WriteLog("Vous devez selectionner l'appareil");
            }
        }

        private void appareilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Constantes.FORM_ARCHIVE_POINTEUSE == null)
            {
                Form_Archive_Pointeuse f = new Form_Archive_Pointeuse();
                f.Show();
                Constantes.FORM_ARCHIVE_POINTEUSE = f;
                Utils.WriteLog("Ouverture page (Archive Appareil)");
                Utils.addFrom("Form_Archive_Pointeuse");
            }
            else
            {
                Constantes.FORM_ARCHIVE_POINTEUSE.WindowState = FormWindowState.Normal;
                Constantes.FORM_ARCHIVE_POINTEUSE.BringToFront();
            }
        }

        private void serveurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Constantes.FORM_ARCHIVE_SERVEUR == null)
            {
                Form_Archive_Serveur f = new Form_Archive_Serveur();
                f.Show();
                Constantes.FORM_ARCHIVE_SERVEUR = f;
                Utils.WriteLog("Ouverture page (Archive Serveur)");
                Utils.addFrom("Form_Archive_Serveur");
            }
            else
            {
                Constantes.FORM_ARCHIVE_SERVEUR.WindowState = FormWindowState.Normal;
                Constantes.FORM_ARCHIVE_SERVEUR.BringToFront();
            }
        }

        private void enrollerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentPointeuse != null ? currentPointeuse.Id > 0 : false)
            {
                if (Constantes.FORM_ADD_EMPREINTE == null)
                {
                    if (!VerifyActionPointeuse())
                    {
                        return;
                    }
                    Form_Add_Empreinte f = new Form_Add_Empreinte(currentPointeuse);
                    f.Show();
                    Constantes.FORM_ADD_EMPREINTE = f;
                    Utils.WriteLog("Ouverture page (Sauvegarde Empreinte)");
                    Utils.addFrom("Form_Add_Empreinte");
                }
                else
                {
                    Constantes.FORM_ADD_EMPREINTE.WindowState = FormWindowState.Normal;
                    Constantes.FORM_ADD_EMPREINTE.BringToFront();
                }
            }
            else
            {
                Utils.WriteLog("Vous devez selectionner l'appareil");
            }
        }

        private void logsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Constantes.FORM_VIEW_RESULT == null)
            {
                Form_ViewResult f = new Form_ViewResult();
                f.Show();
                Constantes.FORM_VIEW_RESULT = f;
                Utils.WriteLog("Ouverture page (Viewer Logs)");
            }
            else
            {
                Constantes.FORM_VIEW_RESULT.WindowState = FormWindowState.Normal;
                Constantes.FORM_VIEW_RESULT.BringToFront();
            }
        }

        private void administrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentPointeuse != null ? currentPointeuse.Id > 0 : false)
            {
                if (Constantes.FORM_GESTION_POINTEUSE == null)
                {
                    if (!VerifyActionPointeuse())
                    {
                        return;
                    }
                    Form_Gestion_Pointeuse f = new Form_Gestion_Pointeuse(currentPointeuse);
                    f.Show();
                    Constantes.FORM_GESTION_POINTEUSE = f;
                    Utils.WriteLog("Ouverture page (Gestion Pointeuse)");
                    Utils.addFrom("Form_Gestion_Pointeuse");
                }
                else
                {
                    Constantes.FORM_GESTION_POINTEUSE.WindowState = FormWindowState.Normal;
                    Constantes.FORM_GESTION_POINTEUSE.BringToFront();
                }
            }
            else
            {
                Utils.WriteLog("Vous devez selectionner l'appareil");
            }
        }

        private void evenementsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Constantes.FORM_EVENEMENT == null)
            {
                Form_Evenement f = new Form_Evenement();
                f.Show();
                Constantes.FORM_EVENEMENT = f;
                Utils.WriteLog("Ouverture page (Entrée/Sortie)");
                Utils.addFrom("Form_Evenement");
            }
            else
            {
                Constantes.FORM_EVENEMENT.WindowState = FormWindowState.Normal;
                Constantes.FORM_EVENEMENT.BringToFront();
            }
        }

        private void empreintesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Constantes.FORM_EMPREINTE == null)
            {
                Form_Empreinte f = new Form_Empreinte();
                f.Show();
                Constantes.FORM_EMPREINTE = f;
                Utils.WriteLog("Ouverture page (Gestion Empreinte)");
                Utils.addFrom("Form_Empreinte");
            }
            else
            {
                Constantes.FORM_EMPREINTE.WindowState = FormWindowState.Normal;
                Constantes.FORM_EMPREINTE.BringToFront();
            }
        }

        private void arrêterProcessusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Constantes.SETTING != null ? Constantes.SETTING.CheckConnect : false)
            {
                if (Constantes.USERS != null ? Constantes.USERS.Id < 1 : false)
                {
                    Form_Login login = new Form_Login(20);
                    login.Show();
                }
                else
                {
                    StopProcessus();
                }
            }
            else
            {
                StopProcessus();
            }
        }

        public void StopProcessus()
        {
            Utils.WriteLog("Demande d'arrêt de(s) processus");
            if (Messages.Confirmation("arrêter le(s) processus") == System.Windows.Forms.DialogResult.Yes)
            {
                Application.ExitThread();
                Utils.WriteLog("-- Arrêt de processus effectué");
            }
            else
            {
                Utils.WriteLog("-- Arrêt de processus annulé");
            }
        }

        private void effacerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ObjectThread o = new ObjectThread(lv_report);
            o.ClearListBox(true);
        }

        private void testerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentPointeuse != null ? currentPointeuse.Id > 0 : false)
            {
                if (Constantes.FORM_EMPLOYE == null)
                {
                    Utils.WriteLog("Demande de testing (test empreinte) sur les employés");
                    if (Messages.Question("Ceci arrêtera le(s) évenement(s) sur l'appareil. Continuer?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (!VerifyActionPointeuse())
                        {
                            return;
                        }
                        Form_Employe f = new Form_Employe(currentPointeuse, false);
                        f.Show();
                        Constantes.FORM_EMPLOYE = f;
                        Utils.WriteLog("Ouverture page (Gestion Employe)");
                        Utils.addFrom("Form_Employe");
                        Utils.WriteLog("-- Testing (test empreinte) approuvé");
                    }
                    else
                    {
                        Utils.WriteLog("-- Testing (test empreinte) annulé");
                    }
                }
                else
                {
                    Utils.WriteLog("Vous ne pouvez pas tester les employés car la page est ouverte en gestion employé. Fermez la d'abord puis reouvrez!");
                    Constantes.FORM_EMPLOYE.WindowState = FormWindowState.Normal;
                    Constantes.FORM_EMPLOYE.BringToFront();
                }
            }
            else
            {
                Utils.WriteLog("Vous devez selectionner l'appareil");
            }
        }

        private void déconnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Constantes.USERS != null)
            {
                Constantes.USERS.Id = 0;
            }
        }

        private void listeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentPointeuse != null ? currentPointeuse.Id > 0 : false)
            {
                if (Constantes.FORM_EMPLOYE == null)
                {
                    if (!VerifyActionPointeuse())
                    {
                        return;
                    }
                    Form_Employe f = new Form_Employe(currentPointeuse, true);
                    f.Show();
                    Constantes.FORM_EMPLOYE = f;
                    Utils.WriteLog("Ouverture page (Gestion Employe)");
                    Utils.addFrom("Form_Employe");
                }
                else
                {
                    Constantes.FORM_EMPLOYE.WindowState = FormWindowState.Normal;
                    Constantes.FORM_EMPLOYE.BringToFront();
                }
            }
            else
            {
                Utils.WriteLog("Vous devez selectionner l'appareil");
            }
        }

        private void présenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Constantes.FORM_PRESENCE == null)
            {
                Form_Presence f = new Form_Presence();
                f.Show();
                Constantes.FORM_PRESENCE = f;
                Utils.WriteLog("Ouverture page (Gestion Présence)");
                Utils.addFrom("Form_Presence");
            }
            else
            {
                Constantes.FORM_PRESENCE.WindowState = FormWindowState.Normal;
                Constantes.FORM_PRESENCE.BringToFront();
            }
        }

        private void pingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Constantes.FORM_VIEW_LOG == null)
            {
                Form_ViewLog f = new Form_ViewLog();
                f.Show();
                Constantes.FORM_VIEW_LOG = f;
                Utils.WriteLog("Ouverture page (Viewer Ping)");
            }
            else
            {
                Constantes.FORM_VIEW_LOG.WindowState = FormWindowState.Normal;
                Constantes.FORM_VIEW_LOG.BringToFront();
            }
        }

        private void dgv_pointeuse_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewCell cell = this.dgv_pointeuse.Rows[e.RowIndex].Cells[0];
            if ((e.ColumnIndex == this.dgv_pointeuse.Columns["icon"].Index) && e.Value != null)
            {
                Pointeuse po = Constantes.POINTEUSES.Find(x => x.Id == Convert.ToInt32(cell.Value));
                cell = this.dgv_pointeuse.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (po != null ? (po.Logs != null ? po.Logs.Count > 0 : false) : false)
                    cell.ToolTipText = "Fichier tampon de cet appareil est chargé";
                else
                    cell.ToolTipText = "Fichier tampon de cet appareil n'est pas chargé";
            }
        }

        private void chargerFichierTamponToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentPointeuse != null ? currentPointeuse.Id > 0 : false)
            {
                Utils.WriteLog("Demande de chargement du fichier tampon de la pointeuse " + currentPointeuse.Ip + "");
                if (Messages.Confirmation("charger le fichier tampon") == System.Windows.Forms.DialogResult.Yes)
                {
                    currentPointeuse.Logs.Clear();
                    UpdatePointeuse(currentPointeuse);
                    Utils.WriteLog("-- Début du chargerment du fichier tampon de la pointeuse " + currentPointeuse.Ip + "");
                    Fonctions.LoadFileTamponPointeuse(currentPointeuse, 1, true);
                }
                else
                {
                    Utils.WriteLog("-- Chargement du fichier tampon de la pointeuse " + currentPointeuse.Ip + " annulée");
                }
            }
            else
            {
                Utils.WriteLog("Vous devez selectionner l'appareil");
            }
        }

        private void recherchePointeuseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utils.WriteLog("Demande de recherche des pointeuses du réseau");
            if (Messages.Question("Cela peut prendre du temps... Continuer?") == System.Windows.Forms.DialogResult.Yes)
            {
                Thread thread = new Thread(new ThreadStart(SearchAdresseByPing));
                thread.Start();
            }
            else
            {
                Utils.WriteLog("-- Recherche des pointeuses du réseau annulée");
            }
        }

        public void SearchAdresseByPing()
        {
            string hôte = Dns.GetHostName();
            IPHostEntry iphe = Dns.Resolve(hôte);
            string ip = iphe.AddressList[0].ToString();
            if (ip != null ? ip.Trim().Length > 0 : false)
            {
                string subIp = "";
                for (int i = ip.Length; i > 1; i--)
                {
                    if (ip[i - 1] == '.')
                    {
                        subIp = ip.Substring(0, i);
                        break;
                    }
                }
                string[] debut = null;
                string[] fin = null;
                if (Utils.StringToAdress(subIp + "1", ref debut) && Utils.StringToAdress(subIp + "255", ref fin))
                {
                    string file = Chemins.CheminDatabase() + "Adresses.txt";
                    if (File.Exists(file))
                        File.Delete(file);
                    List<string> adresses = Utils.Adresses(debut, fin);
                    List<string> pointeuses = new List<string>();
                    foreach (string adresse in adresses)
                    {
                        Appareil z = null;
                        if (Utils.PingAdresse(adresse, ref z))
                        {
                            Logs.WriteTxt(file, adresse);
                            if (!pointeuses.Contains(adresse))
                                pointeuses.Add(adresse);
                        }
                    }
                    Utils.WriteLog("-- Recherche des pointeuses du réseau terminée (" + pointeuses.Count + " pointeuse(s) trouvée(s))");
                }
            }
        }

        private void voirResultatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string file = Chemins.CheminDatabase() + "Adresses.txt";
            List<string> adresses = new List<string>();
            if (File.Exists(file))
                adresses = Logs.ReadTxt(file);
            if (Constantes.FORM_FIND_POINTEUSE == null)
            {
                Form_Search_Pointeuse f = new Form_Search_Pointeuse(adresses);
                f.Show();
                Constantes.FORM_FIND_POINTEUSE = f;
                Utils.WriteLog("Ouverture page (Recherche pointeuse)");
                Utils.addFrom("Form_Search_Pointeuse");
            }
            else
            {
                Constantes.FORM_FIND_POINTEUSE.adresses = adresses;
                Constantes.FORM_FIND_POINTEUSE.LoadAdresse();
                Constantes.FORM_FIND_POINTEUSE.WindowState = FormWindowState.Normal;
                Constantes.FORM_FIND_POINTEUSE.BringToFront();
            }
        }

        private void pingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Constantes.FORM_PING_APPAREIL == null)
            {
                Form_Ping_Appareil f = new Form_Ping_Appareil();
                f.Show();
                Constantes.FORM_PING_APPAREIL = f;
                Utils.WriteLog("Ouverture page (Ping Appareil)");
                Utils.addFrom("Form_Ping_Appareil");
            }
            else
            {
                Constantes.FORM_PING_APPAREIL.WindowState = FormWindowState.Normal;
                Constantes.FORM_PING_APPAREIL.BringToFront();
            }
        }

        private void connexionExterneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentPointeuse != null ? currentPointeuse.Id > 0 : false)
            {
                Dial_Connet_Externe f = new Dial_Connet_Externe(currentPointeuse);
                f.ShowDialog();
            }
            else
            {
                Utils.WriteLog("Vous devez selectionner l'appareil");
            }
        }
    }
}
