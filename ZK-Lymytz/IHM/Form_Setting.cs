using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;


using System.Windows.Forms;
using ZK_Lymytz.BLL;
using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;

namespace ZK_Lymytz.IHM
{
    public partial class Form_Setting : Form
    {
        Serveur serveur = new Serveur();
        private List<Societe> societes = new List<Societe>();
        private Societe societe = new Societe();
        bool view_old_password_pc, view_new_password_pc, view_new_repassword_pc,
            view_old_password_log, view_new_password_log, view_new_repassword_log;

        public Form_Setting()
        {
            InitializeComponent();
            Configuration.Load(this);
        }

        private void Form_Setting_FormClosed(object sender, FormClosedEventArgs e)
        {
            Constantes.FORM_SETTING = null;
            Utils.removeFrom("Form_Setting");
            Utils.WriteLog("Fermeture page (Paramètres)");
        }

        private void Form_Setting_Load(object sender, EventArgs e)
        {
            LoadConfig();
            LoadSociete();
            LoadCurrentSociete();
            LoadCurrentServeur();
            LoadCurrentSetting();
            LoadCurrentUser();
        }

        private void LoadCurrentSetting()
        {
            Setting s = SettingBLL.ReturnSetting();
            chk_autorun.Checked = s.Autorun;
            chk_auto_synchro.Checked = s.AutoSynchro;
            chk_save_delete.Checked = s.AutoClearAndBackup;
            chk_rattach.Checked = s.AutoRattach;
            chk_add_enroller_auto.Checked = s.AddEnrollAuto;
            chk_auto_backup.Checked = s.AutoBackupDevice;
            chk_connect.Checked = s.CheckConnect;
            chk_use_tampon_log.Checked = s.UseFileTamponLog;
            chk_connect_synchro.Checked = s.AutoCheckConnectAndSynchro;
            dtp_time_synchro_auto.Value = s.TimeSynchroAuto != null ? (!s.TimeSynchroAuto.ToShortDateString().Equals("01/01/0001") ? s.TimeSynchroAuto : DateTime.Now) : DateTime.Now;
            dtp_time_synchro_auto.Enabled = s.AutoCheckConnectAndSynchro;
            string path = s.CheminPhoto;
            if (path != null ? path.Trim().Equals("") : true)
            {
                //C:\Users\Administrateur\lymytz\CCOS.A\documents\docEmps\perso\photo
                path = Chemins.CheminUser() + "lymytz" + Constantes.FILE_SEPARATOR + txt_name.Text.Trim() + Constantes.FILE_SEPARATOR + "documents" + Constantes.FILE_SEPARATOR + "docEmps" + Constantes.FILE_SEPARATOR + "perso" + Constantes.FILE_SEPARATOR + "photo" + Constantes.FILE_SEPARATOR;
            }
            txt_path_photo.Text = path;
        }

        private void LoadCurrentServeur()
        {
            Serveur s = ServeurBLL.ReturnServeur();
            if (s != null)
            {
                PopulateServeur(s);
            }
        }

        private void LoadCurrentSociete()
        {
            Societe s = SocieteBLL.ReturnSociete();
            if (s != null ? s.Id > 0 : false)
            {
                cbox_societe.SelectedText = s.Name;
                societe = s;
                txt_name.Text = s.Name;
            }
        }

        private void LoadCurrentUser()
        {
            txt_domain.Text = Chemins.domainName;
            txt_name_user.Text = Chemins.usersName;
        }

        private void LoadSociete()
        {
            societes = SocieteBLL.List("select * from yvs_societes");
            try
            {
                cbox_societe.Items.Clear();
                for (int i = 0; i < societes.Count; i++)
                {
                    cbox_societe.Items.Add(societes[i].Name);
                    cbox_societe.AutoCompleteCustomSource.Add(societes[i].Name);
                }
                cbox_societe.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbox_societe.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Setting (LoadSociete)", ex);
                cbox_societe.Items.Clear();
            }

        }

        private void LoadConfig()
        {
            com_langue.Text = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? Constantes.LANGUE_ANGLAIS_NAME : Constantes.LANGUE_FRANCAIS_NAME;
            switch (com_langue.Text)
            {
                case Constantes.LANGUE_ANGLAIS_NAME:
                    box_langue.Image = global::ZK_Lymytz.Properties.Resources.En;
                    break;
                case Constantes.LANGUE_FRANCAIS_NAME:
                    box_langue.Image = global::ZK_Lymytz.Properties.Resources.Fr;
                    break;
                default:
                    break;
            }

            com_template.Text = Configuration.template.Equals("") ? "Basique" : Configuration.template;
            if (com_template.Text.Equals("Basique"))
            {
                this.box_template.Image = global::ZK_Lymytz.Properties.Resources.Basique;
            }
            else if (com_template.Text.Equals("BlackClass"))
            {
                this.box_template.Image = global::ZK_Lymytz.Properties.Resources.BlackClass;
            }
            else if (com_template.Text.Equals("BlueTrack"))
            {
                this.box_template.Image = global::ZK_Lymytz.Properties.Resources.BlueTrack;

            }
            else if (com_template.Text.Equals("Classique"))
            {
                this.box_template.Image = global::ZK_Lymytz.Properties.Resources.Classique;
            }

            LoadLangue();
        }

        private void LoadLangue()
        {
            this.Text = Mots.Parametre;
            tab_niveau.Text = Mots.General;
            tab_parametre.Text = Mots.Parametre;
            tab_serveur.Text = Mots.Serveur;
            grp_action_.Text = Mots.Actions;
            grp_action_s.Text = Mots.Actions;
            lb_langue.Text = Mots.Langue + " :";
            lb_adresse.Text = Mots.Adresse_IP + " :";
            lb_database.Text = Mots.Database + " :";
            lb_password.Text = Mots.Password + " :";
            lb_user.Text = Mots.Utilisateur + " :";
        }

        private void ResetServeur()
        {
            txt_adresse.Text = "127.0.0.1";
            txt_database.Text = "lymytz_demo_0";
            txt_password.Text = "yves1910/";
            txt_port.Text = "5432";
            txt_users.Text = "postgres";
        }

        private void RecopieServeur()
        {
            serveur.Adresse = txt_adresse.Text.Trim();
            serveur.Database = txt_database.Text.Trim();
            serveur.Password = txt_password.Text.Trim();
            try
            {
                serveur.Port = Convert.ToInt32(txt_port.Text.Trim());
            }
            catch (Exception ex)
            {
                Messages.ShowErreur("Le port est une valeur numerique");
                serveur.Port = 5432;
            }
            serveur.User = txt_users.Text.Trim();
        }

        private void PopulateServeur(Serveur s)
        {
            txt_adresse.Text = s.Adresse;
            txt_database.Text = s.Database;
            txt_password.Text = s.Password;
            txt_port.Text = s.Port.ToString();
            txt_users.Text = s.User;
        }

        private void btn_reset_serveur_Click(object sender, EventArgs e)
        {
            if (txt_adresse.Text.Trim().Equals(""))
            {
                ResetServeur();
            }
            else
            {
                if (DialogResult.Yes == Messages.Confirmation(Mots.Annuler.ToLower()))
                {
                    ResetServeur();
                }
            }
        }

        private void btn_save_serveur_Click(object sender, EventArgs e)
        {
            RecopieServeur();
            if (serveur.Control())
            {
                if (ServeurBLL.CreateServeur(serveur))
                {
                    Messages.Succes();
                    Application.ExitThread();
                    Application.Restart();
                }
            }
        }

        private void com_langue_SelectedIndexChanged(object sender, EventArgs e)
        {
            string langue = com_langue.Text;
            switch (langue)
            {
                case Constantes.LANGUE_ANGLAIS_NAME:
                    box_langue.Image = global::ZK_Lymytz.Properties.Resources.En;
                    Configuration.langue = Constantes.LANGUE_ANGLAIS;
                    break;
                case Constantes.LANGUE_FRANCAIS_NAME:
                    box_langue.Image = global::ZK_Lymytz.Properties.Resources.Fr;
                    Configuration.langue = Constantes.LANGUE_FRANCAIS;
                    break;
                default:
                    Configuration.langue = Constantes.LANGUE_FRANCAIS;
                    break;
            }
        }

        private void com_template_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (com_template.Text.Equals("Basique"))
            {
                Configuration.template = "Basique";
                this.box_template.Image = global::ZK_Lymytz.Properties.Resources.Basique;

                Configuration.back_color_Form = "Control";
                Configuration.fore_color_Label = "ControlText";
                Configuration.police_Label = "Microsoft Sans Serif";
                Configuration.taille_Label = float.Parse("8,25");

                Configuration.back_color_Text = "Window";
                Configuration.fore_color_Text = "WindowText";
                Configuration.police_Text = "Microsoft Sans Serif";
                Configuration.taille_Text = float.Parse("8,25");
            }
            else if (com_template.Text.Equals("BlackClass"))
            {
                Configuration.template = "BlackClass";
                this.box_template.Image = global::ZK_Lymytz.Properties.Resources.BlackClass;

                Configuration.back_color_Form = "Black";
                Configuration.fore_color_Label = "White";
                Configuration.police_Label = "Arial Narrow";
                Configuration.taille_Label = float.Parse("10,25");

                Configuration.back_color_Text = "WindowText";
                Configuration.fore_color_Text = "White";
                Configuration.police_Text = "Arial Narrow";
                Configuration.taille_Text = float.Parse("10,25");
            }
            else if (com_template.Text.Equals("BlueTrack"))
            {
                Configuration.template = "BlueTrack";
                this.box_template.Image = global::ZK_Lymytz.Properties.Resources.BlueTrack;

                Configuration.back_color_Form = "GradientInactiveCaption";
                Configuration.fore_color_Label = "Navy";
                Configuration.police_Label = "Tahoma";
                Configuration.taille_Label = float.Parse("9,75");

                Configuration.back_color_Text = "Window";
                Configuration.fore_color_Text = "WindowText";
                Configuration.police_Text = "Tahoma";
                Configuration.taille_Text = float.Parse("9,75");

            }
            else if (com_template.Text.Equals("Classique"))
            {
                Configuration.template = "Classique";
                this.box_template.Image = global::ZK_Lymytz.Properties.Resources.Classique;

                Configuration.back_color_Form = "White";
                Configuration.fore_color_Label = "Navy";
                Configuration.police_Label = "Rockwell";
                Configuration.taille_Label = float.Parse("9,75");

                Configuration.back_color_Text = "Window";
                Configuration.fore_color_Text = "WindowText";
                Configuration.police_Text = "Rockwell";
                Configuration.taille_Text = float.Parse("9,75");
            }
        }

        private void btn_save_setting_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == Messages.Confirmation(Mots.Modifier.ToLower()))
            {
                Configuration.Save();
                if (DialogResult.Yes == Messages.Confirmation(Mots.Restart_Now.ToLower()))
                {
                    Application.ExitThread();
                    Application.Restart();
                }
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (societe != null ? societe.Id > 0 : false)
            {
                if (SocieteBLL.CreateSociete(societe))
                {
                    if (DialogResult.Yes == Messages.Confirmation(Mots.Restart_Now.ToLower()))
                    {
                        Application.ExitThread();
                        Application.Restart();
                    }
                    else
                    {
                        Constantes.SOCIETE = societe;
                    }
                }
            }
        }

        private void cbox_societe_SelectedIndexChanged(object sender, EventArgs e)
        {
            String name = cbox_societe.Text.Trim().Replace("'", "''");
            societe = SocieteBLL.OneByName(name);
            txt_name.Text = societe.Name;
        }

        private Setting BuildSettign()
        {
            Setting i = new Setting();
            i.Vide = false;
            i.Autorun = chk_autorun.Checked;
            i.AutoSynchro = chk_auto_synchro.Checked;
            i.AutoClearAndBackup = chk_save_delete.Checked;
            i.AutoRattach = chk_rattach.Checked;
            i.AddEnrollAuto = chk_add_enroller_auto.Checked;
            i.AutoBackupDevice = chk_auto_backup.Checked;
            i.CheckConnect = chk_connect.Checked;
            i.UseFileTamponLog = chk_use_tampon_log.Checked;
            i.AutoCheckConnectAndSynchro = chk_connect_synchro.Checked;
            i.TimeSynchroAuto = dtp_time_synchro_auto.Value;
            i.CheminStartup = Chemins.cheminStartup;
            i.CheminPersonal = Chemins.cheminDefault;
            string path = txt_path_photo.Text.Trim();
            if (path != null ? !path.Trim().Equals("") : false)
            {
                DirectoryInfo dossier = new DirectoryInfo(path);
                if (!dossier.Exists)
                {
                    Utils.WriteLog("Ce dossier n'existe pas.. veuillez verifier le chemin");
                    txt_path_photo.BackColor = System.Drawing.Color.Red;
                    return null;
                }
            }
            else
            {
                path = "";
            }
            i.CheminPhoto = path;
            return i;
        }

        private Users RecopieUser()
        {
            Users u = new Users();
            u.Name = txt_name_user.Text.Trim();
            u.PasswordPC = txt_new_password_pc.Text.Trim();
            u._PasswordPC = txt_old_password_pc.Text.Trim();
            u._PasswordPC_ = txt_new_repassword_pc.Text.Trim();
            if (txt_new_password_pc.Text.Trim().Length < 1 && txt_new_repassword_pc.Text.Trim().Length < 1 && txt_old_password_pc.Text.Trim().Length < 1)
            {
                u.PasswordPC = Constantes.USERS.PasswordPC;
                u._PasswordPC = Constantes.USERS.PasswordPC;
                u._PasswordPC_ = Constantes.USERS.PasswordPC;
            }
            u.PasswordLog = txt_new_password_log.Text.Trim();
            u._PasswordLog = txt_old_password_log.Text.Trim();
            u._PasswordLog_ = txt_new_repassword_log.Text.Trim();
            if (txt_new_password_log.Text.Trim().Length < 1 && txt_new_repassword_log.Text.Trim().Length < 1 && txt_old_password_log.Text.Trim().Length < 1)
            {
                u.PasswordLog = Constantes.USERS.PasswordLog;
                u._PasswordLog = Constantes.USERS.PasswordLog;
                u._PasswordLog_ = Constantes.USERS.PasswordLog;
            }
            return u;
        }

        private void btn_save_preference_Click(object sender, EventArgs e)
        {
            if (BuildSettign() != null)
            {
                if (SettingBLL.CreateSetting(BuildSettign()))
                {
                    if (DialogResult.Yes == Messages.Confirmation(Mots.Restart_Now.ToLower()))
                    {
                        Application.ExitThread();
                        Application.Restart();
                    }
                    else
                    {
                        Constantes.SETTING = BuildSettign();
                    }
                }
            }
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            string path = txt_path_photo.Text.Trim();
            DirectoryInfo dossier = new DirectoryInfo(path);
            if (dossier.Exists)
            {
                ofd_open.InitialDirectory = txt_path_photo.Text;
                ofd_open.ShowDialog();
            }
            else
            {
                Utils.WriteLog("Ce dossier n'existe pas.. veuillez verifier le chemin");
                txt_path_photo.BackColor = System.Drawing.Color.Red;
            }
        }

        private void txt_search_Click(object sender, EventArgs e)
        {
            fbd_search.SelectedPath = Chemins.CheminUser();
            if (fbd_search.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    txt_path_photo.Text = fbd_search.SelectedPath + Constantes.FILE_SEPARATOR;
                }
                catch (Exception ex)
                {
                    Messages.Exception("Form_Setting (txt_search_Click)", ex);
                }
            }
        }

        private void lnk_default_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txt_path_photo.Text = Chemins.CheminUser() + "lymytz" + Constantes.FILE_SEPARATOR + txt_name.Text.Trim() + Constantes.FILE_SEPARATOR + "documents" + Constantes.FILE_SEPARATOR + "docEmps" + Constantes.FILE_SEPARATOR + "perso" + Constantes.FILE_SEPARATOR + "photo" + Constantes.FILE_SEPARATOR;
        }

        private void txt_path_photo_TextChanged(object sender, EventArgs e)
        {
            if (txt_path_photo.BackColor == System.Drawing.Color.Red)
            {
                txt_path_photo.BackColor = Color.FromName(Configuration.back_color_Text);
            }
        }

        private void btn_old_password_pc_Click(object sender, EventArgs e)
        {
            txt_old_password_pc.PasswordChar = view_old_password_pc ? '*' : '\0';
            view_old_password_pc = !view_old_password_pc;
        }

        private void btn_new_password_pc_Click(object sender, EventArgs e)
        {
            txt_new_password_pc.PasswordChar = view_new_password_pc ? '*' : '\0';
            view_new_password_pc = !view_new_password_pc;
        }

        private void btn_new_repassword_pc_Click(object sender, EventArgs e)
        {
            txt_new_repassword_pc.PasswordChar = view_new_repassword_pc ? '*' : '\0';
            view_new_repassword_pc = !view_new_repassword_pc;
        }

        private void btn_old_password_log_Click(object sender, EventArgs e)
        {
            txt_old_password_log.PasswordChar = view_old_password_log ? '*' : '\0';
            view_old_password_log = !view_old_password_log;
        }

        private void btn_new_password_log_Click(object sender, EventArgs e)
        {
            txt_new_password_log.PasswordChar = view_new_password_log ? '*' : '\0';
            view_new_password_log = !view_new_password_log;
        }

        private void btn_new_repassword_log_Click(object sender, EventArgs e)
        {
            txt_new_repassword_log.PasswordChar = view_new_repassword_log ? '*' : '\0';
            view_new_repassword_log = !view_new_repassword_log;
        }

        private void txt_old_password_pc_TextChanged(object sender, EventArgs e)
        {
            txt_old_password_pc.BackColor = Color.FromName(Configuration.back_color_Text); 
        }

        private void txt_new_repassword_pc_TextChanged(object sender, EventArgs e)
        {
            txt_new_repassword_pc.BackColor = Color.FromName(Configuration.back_color_Text); 
            if (txt_new_repassword_pc.Text.Length < 1)
            {
                lb_new_repassword_pc.Visible = false;
            }
            else
            {
                lb_new_repassword_pc.Visible = true;
                if (txt_new_repassword_pc.Text.Equals(txt_new_password_pc.Text))
                {
                    lb_new_repassword_pc.Text = "Correct";
                    lb_new_repassword_pc.ForeColor = Color.Green;
                }
                else
                {
                    lb_new_repassword_pc.Text = "Pas correct";
                    lb_new_repassword_pc.ForeColor = Color.Red;
                }
            }
        }

        private void txt_old_password_log_TextChanged(object sender, EventArgs e)
        {
            txt_old_password_log.BackColor = Color.FromName(Configuration.back_color_Text); 
        }

        private void txt_new_repassword_log_TextChanged(object sender, EventArgs e)
        {
            txt_new_repassword_log.BackColor = Color.FromName(Configuration.back_color_Text); 
            if (txt_new_repassword_log.Text.Length < 1)
            {
                lb_new_repassword_log.Visible = false;
            }
            else
            {
                lb_new_repassword_log.Visible = true;
                if (txt_new_repassword_log.Text.Equals(txt_new_password_log.Text))
                {
                    lb_new_repassword_log.Text = "Correct";
                    lb_new_repassword_log.ForeColor = Color.Green;
                }
                else
                {
                    lb_new_repassword_log.Text = "Pas correct";
                    lb_new_repassword_log.ForeColor = Color.Red;
                }
            }
        }

        private void btn_save_users_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == Messages.Confirmation(Mots.Modifier.ToLower()))
            {
                Users bean = RecopieUser();
                if (bean.Control())
                {
                    if (bean._PasswordPC.Equals(Constantes.USERS.PasswordPC))
                    {
                        if (bean._PasswordPC_.Equals(bean.PasswordPC))
                        {
                            if (Utils.VerifyMd5Hash(bean._PasswordLog, Constantes.USERS.PasswordLog))
                            {
                                if (bean._PasswordLog_.Equals(bean.PasswordLog))
                                {
                                    bean.PasswordLog = Utils.GetMd5Hash(bean.PasswordLog);
                                    if (UsersBLL.CreateUsers(bean))
                                    {
                                        if (DialogResult.Yes == Messages.Confirmation(Mots.Restart_Now.ToLower()))
                                        {
                                            Application.ExitThread();
                                            Application.Restart();
                                        }
                                        else
                                        {
                                            Constantes.USERS = bean;
                                        }
                                    }
                                }
                                else
                                {
                                    Messages.ShowErreur("Confirmation du mot de passe du logiciel n'est pas correct");
                                    txt_new_repassword_log.BackColor = Color.Red;
                                }
                            }
                            else
                            {
                                Messages.ShowErreur("Mot de passe du logiciel n'est pas correct");
                                txt_old_password_log.BackColor = Color.Red;
                            }
                        }
                        else
                        {
                            Messages.ShowErreur("Confirmation du mot de passe de la machine n'est pas correct");
                            txt_new_repassword_pc.BackColor = Color.Red;
                        }
                    }
                    else
                    {
                        Messages.ShowErreur("Mot de passe de la machine n'est pas correct");
                        txt_old_password_pc.BackColor = Color.Red;
                    }
                }
            }
        }

        private void chk_connect_synchro_CheckedChanged(object sender, EventArgs e)
        {
            dtp_time_synchro_auto.Enabled = chk_connect_synchro.Checked;
        }
    }
}
