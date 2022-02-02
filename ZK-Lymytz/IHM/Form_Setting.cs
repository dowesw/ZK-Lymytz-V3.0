using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
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
        private List<Agence> agences = new List<Agence>();
        private Agence agence = new Agence();
        private List<Serveur> liaisons = new List<Serveur>();
        private Serveur liaison = new Serveur();
        private Users users;

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
            LoadCurrentSociete();
            LoadCurrentAgence();
            LoadCurrentServeur();
            LoadCurrentSetting();
            LoadCurrentUser();
            LoadSociete(txt_group.Text);
            LoadLiaisons();
            ResetLiaison();
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
            chk_create_service.Checked = s.CreateService;
            dtp_time_synchro_auto.Value = s.TimeSynchroAuto != null ? (!s.TimeSynchroAuto.ToShortDateString().Equals("01/01/0001") ? s.TimeSynchroAuto : DateTime.Now) : DateTime.Now;
            dtp_time_synchro_auto.Enabled = s.AutoCheckConnectAndSynchro;
            string path = s.CheminPhoto;
            if (path != null ? path.Trim().Equals("") : true)
            {
                //C:\Users\Administrateur\lymytz\CCOS.A\documents\docEmps\perso\photo
                path = Chemins.CheminUser() + "lymytz" + Constantes.FILE_SEPARATOR + txt_name.Text.Trim() + Constantes.FILE_SEPARATOR + "documents" + Constantes.FILE_SEPARATOR + "docEmps" + Constantes.FILE_SEPARATOR + "perso" + Constantes.FILE_SEPARATOR + "photo" + Constantes.FILE_SEPARATOR;
            }
            txt_path_photo.Text = path;
            path = s.CheminSetup;
            if (path != null ? path.Trim().Equals("") : true)
            {
                path = Chemins.cheminStartup;
            }
            txt_path_setup.Text = path;
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
                txt_group.Text = s.Groupe != null ? s.Groupe.Libelle : "";
                LoadAgence(s);
            }
        }

        private void LoadCurrentAgence()
        {
            Agence s = AgenceBLL.ReturnAgence();
            if (s != null ? s.Id > 0 : false)
            {
                cbox_agence.SelectedText = s.Name;
                agence = s;
                txt_name_agence.Text = s.Name;
            }
        }

        private void LoadCurrentUser()
        {
            users = Constantes.USERS;
            txt_login.Text = Constantes.USERS.Code;
            txt_domain.Text = Chemins.domainName;
            txt_name_user.Text = Chemins.usersName;
            lb_id_users.Text = users.Id.ToString();
            txt_author.Text = users.Author.ToString();
        }

        private void LoadLiaisons()
        {
            liaisons = LiaisonBLL.ReturnServeur();
            foreach (Serveur serveur in liaisons)
            {
                AddLiaison(serveur);
            }
        }

        private void LoadSociete(string groupe)
        {
            string query = "select y.id, y.name, y.adresse_ip, COALESCE(i.port, 0) AS port, i.users, i.password, i.domain, i.type_connexion, y.groupe, g.libelle " +
                            "from yvs_societes y left join yvs_base_groupe_societe g on y.groupe = g.id left join yvs_societes_connexion i on i.societe = y.id";
            if (groupe != null ? groupe.Trim().Length > 0 : false)
            {
                query += " where g.libelle = '" + groupe + "'";
            }
            societes = SocieteBLL.List(query);
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

        private void LoadAgence(Societe societe)
        {
            agences = AgenceBLL.List("select * from yvs_agences where societe = " + societe.Id);
            agences.Insert(0, new Agence(0, "---"));
            try
            {
                agence = new Agence(0, "---");
                txt_name_agence.Text = "---";
                cbox_agence.Items.Clear();
                for (int i = 0; i < agences.Count; i++)
                {
                    cbox_agence.Items.Add(agences[i].Name);
                    cbox_agence.AutoCompleteCustomSource.Add(agences[i].Name);
                }
                cbox_agence.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbox_agence.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Setting (LoadAgence)", ex);
                cbox_agence.Items.Clear();
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

        private void btn_tester_Click(object sender, EventArgs e)
        {
            RecopieServeur();
            if (serveur.Control())
            {
                if (new TOOLS.Connexion().isConnection(serveur))
                    Messages.Information("Connecté");
                else
                    Messages.ShowErreur("Echec");
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
                    if (agence != null ? agence.Id > 0 : false)
                    {
                        AgenceBLL.CreateAgence(agence);
                    }
                    else
                    {
                        AgenceBLL.RemoveAgence();
                    }
                    Constantes.SOCIETE = societe;
                    Constantes.AGENCE = agence;
                    if (DialogResult.Yes == Messages.Confirmation(Mots.Restart_Now.ToLower()))
                    {
                        Application.ExitThread();
                        Application.Restart();
                    }
                }
            }
        }

        private void cbox_societe_SelectedIndexChanged(object sender, EventArgs e)
        {
            societe = societes[cbox_societe.SelectedIndex];
            txt_name.Text = societe.Name;
            LoadAgence(societe);
        }

        private void cbox_agence_SelectedIndexChanged(object sender, EventArgs e)
        {
            agence = agences[cbox_agence.SelectedIndex];
            txt_name_agence.Text = agence.Name;
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
            i.CreateService = chk_create_service.Checked;
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
            path = txt_path_setup.Text.Trim();
            if (path != null ? !path.Trim().Equals("") : false)
            {
                DirectoryInfo dossier = new DirectoryInfo(path);
                if (!dossier.Exists)
                {
                    Utils.WriteLog("Ce dossier n'existe pas.. veuillez verifier le chemin");
                    txt_path_setup.BackColor = System.Drawing.Color.Red;
                    return null;
                }
            }
            else
            {
                path = "";
            }
            i.CheminSetup = path;
            return i;
        }

        private Users RecopieUser()
        {
            Users u = new Users();
            u.Code = txt_login.Text.Trim();
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
                ofd_open.InitialDirectory = path;
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

        private void btn_search_path_setup_Click(object sender, EventArgs e)
        {
            fbd_search.SelectedPath = Chemins.CheminUser();
            if (fbd_search.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    txt_path_setup.Text = fbd_search.SelectedPath + Constantes.FILE_SEPARATOR;
                }
                catch (Exception ex)
                {
                    Messages.Exception("Form_Setting (btn_search_path_setup_Click)", ex);
                }
            }
        }

        private void btn_open_path_setup_Click(object sender, EventArgs e)
        {
            string path = txt_path_setup.Text.Trim();
            if (path != null ? path.Length > 0 : false)
            {
                DirectoryInfo dossier = new DirectoryInfo(path);
                if (dossier.Exists)
                {
                    ofd_open.InitialDirectory = path;
                    ofd_open.ShowDialog();
                }
                else
                {
                    Utils.WriteLog("Ce dossier n'existe pas.. veuillez verifier le chemin");
                    txt_path_setup.BackColor = System.Drawing.Color.Red;
                }
            }
        }

        private void lnk_default_maj_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txt_path_setup.Text = Chemins.cheminStartup;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txt_path_setup.BackColor == System.Drawing.Color.Red)
            {
                txt_path_setup.BackColor = Color.FromName(Configuration.back_color_Text);
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
                if (Utils.asString(Constantes.USERS.PasswordPC))
                {
                    if (!bean._PasswordPC.Equals(Constantes.USERS.PasswordPC))
                    {
                        Messages.ShowErreur("Mot de passe de la machine n'est pas correct");
                        txt_old_password_pc.BackColor = Color.Red;
                        return;
                    }
                    if (!bean._PasswordPC_.Equals(bean.PasswordPC))
                    {
                        Messages.ShowErreur("Confirmation du mot de passe de la machine n'est pas correct");
                        txt_new_repassword_pc.BackColor = Color.Red;
                        return;
                    }
                }
                if (Utils.asString(Constantes.USERS.PasswordLog))
                {
                    if (!Utils.VerifyMd5Hash(bean._PasswordLog, Constantes.USERS.PasswordLog))
                    {
                        Messages.ShowErreur("Mot de passe du logiciel n'est pas correct");
                        txt_old_password_log.BackColor = Color.Red;
                        return;
                    }
                    if (!bean._PasswordLog_.Equals(bean.PasswordLog))
                    {
                        Messages.ShowErreur("Confirmation du mot de passe du logiciel n'est pas correct");
                        txt_new_repassword_log.BackColor = Color.Red;
                        return;
                    }
                }

                string query = "select id from yvs_users_agence where users = " + users.Id + " and agence = " + Constantes.AGENCE.Id;
                object author = Bll.LoadOneObject(query, null);

                bean.PasswordLog = Utils.GetMd5Hash(bean.PasswordLog);
                users.Name = bean.Name;
                users.PasswordPC = bean.PasswordPC;
                users.PasswordLog = bean.PasswordLog;
                users.Author = author != null ? Convert.ToInt32(author.ToString()) : 0;
                txt_author.Text = users.Author.ToString();
                if (UsersBLL.CreateUsers(users))
                {
                    if (DialogResult.Yes == Messages.Confirmation(Mots.Restart_Now.ToLower()))
                    {
                        Application.ExitThread();
                        Application.Restart();
                    }
                    else
                    {
                        Constantes.USERS = users;
                    }
                }
            }
        }

        private void chk_connect_synchro_CheckedChanged(object sender, EventArgs e)
        {
            dtp_time_synchro_auto.Enabled = chk_connect_synchro.Checked;
            chk_auto_backup.Enabled = !chk_connect_synchro.Checked;
        }

        private void btn_reset_setting_Click(object sender, EventArgs e)
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

        private void btn_add_liaison_Click(object sender, EventArgs e)
        {
            try
            {
                Serveur serveur = new Serveur();
                serveur.Adresse = _ls_adresse.Text;
                serveur.Database = _ls_database.Text;
                serveur.User = _ls_users.Text;
                serveur.Password = _ls_password.Text;
                serveur.Port = Convert.ToInt16(_ls_port.Text);
                serveur.DateDebut = dtp_date_debut.Value;
                if (serveur.Control())
                {
                    Serveur y = liaisons.Find(x => x.Adresse == serveur.Adresse);
                    if (y != null ? Utils.asString(y.Adresse) ? (liaison != null ? !Utils.asString(liaison.Adresse) : true) : false : false)
                    {
                        TOOLS.Messages.ShowErreur("Vous avez deja associé ce serveur");
                        return;
                    }
                    bool continu = true;
                    if (liaison != null ? Utils.asString(liaison.Adresse) ? !liaison.Adresse.Equals(serveur.Adresse) : false : false)
                    {
                        continu = LiaisonBLL.DeleteServeur(liaison);
                    }
                    if (continu)
                    {
                        if (LiaisonBLL.CreateServeur(serveur))
                        {
                            if (liaison != null ? Utils.asString(liaison.Adresse) : false)
                            {
                                int index = liaisons.FindIndex(x => x.Adresse == liaison.Adresse);
                                if (index > -1)
                                {
                                    liaisons.RemoveAt(index);
                                }
                                liaisons.Insert((index > -1 ? index : 0), serveur);
                                if (!liaison.Adresse.Equals(serveur.Adresse))
                                {
                                    DeleteLiaison(liaison);
                                }
                                UpdateLiaison(serveur);
                            }
                            else
                            {
                                AddLiaison(serveur);
                                liaisons.Add(serveur);
                            }
                        }
                        ResetLiaison();
                        Messages.Succes();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.Exception(ex);
            }
        }

        private void dgv_liaison_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                DataGridView.HitTestInfo info = dgv_liaison.HitTest(e.X, e.Y); //get info
                int pos = dgv_liaison.HitTest(e.X, e.Y).RowIndex;
                if (pos > -1)
                {
                    if (dgv_liaison.Rows[pos].Cells["_adresse"].Value != null)
                    {
                        String adresse = (String)dgv_liaison.Rows[pos].Cells["_adresse"].Value;
                        if (Utils.asString(adresse))
                        {
                            Serveur f = liaisons.Find(x => x.Adresse == adresse);
                            switch (e.Button)
                            {
                                case MouseButtons.Right:
                                    {
                                        ResetLiaison();
                                        dgv_liaison.Rows[pos].Selected = true; //Select the row
                                        PopulateLiaison(f);
                                    }
                                    break;
                                default:
                                    PopulateLiaison(f);
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.Exception(ex);
            }
        }

        private void tsmi_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (LiaisonBLL.DeleteServeur(liaison))
                {
                    liaisons.Remove(liaison);
                    DeleteLiaison(liaison);
                    ResetLiaison();
                    Messages.Succes();
                }
            }
            catch (Exception ex)
            {
                Utils.Exception(ex);
            }
        }

        public void PopulateLiaison(Serveur serveur)
        {
            _ls_adresse.Text = serveur.Adresse;
            _ls_database.Text = serveur.Database;
            _ls_users.Text = serveur.User;
            _ls_password.Text = serveur.Password;
            _ls_port.Text = serveur.Port.ToString();
            dtp_date_debut.Value = serveur.DateDebut;
            liaison = serveur;
        }

        public void ResetLiaison()
        {
            _ls_adresse.Text = "";
            _ls_database.Text = "";
            _ls_users.Text = "";
            _ls_password.Text = "";
            _ls_port.Text = "0";
            liaison = new Serveur();
        }

        public void AddLiaison(Serveur p)
        {
            dgv_liaison.Rows.Add(new object[] { p.Adresse, p.Port, p.Database, p.User, p.Password, p.DateDebut.ToString("dd-MM-yyyy") });
            VerifyExterne(p);
        }

        public void UpdateLiaison(Serveur p)
        {
            int i = Utils.GetRowData(dgv_liaison, p.Adresse);
            if (i > -1)
                dgv_liaison.Rows.RemoveAt(i);
            else
                i = 0;
            dgv_liaison.Rows.Insert(i, new object[] { p.Adresse, p.Port, p.Database, p.User, p.Password, p.DateDebut.ToString("dd-MM-yyyy") });
            VerifyExterne(p);
        }

        public void DeleteLiaison(Serveur p)
        {
            int i = Utils.GetRowData(dgv_liaison, p.Adresse);
            if (i > -1)
            {
                dgv_liaison.Rows.RemoveAt(i);
                p = null;
            }
        }

        private void txt_group_Leave(object sender, EventArgs e)
        {
            try
            {
                LoadSociete(txt_group.Text);
            }
            catch (Exception ex)
            {
                Messages.Exception(ex);
            }
        }

        private void VerifyExterne(Serveur p)
        {
            Npgsql.NpgsqlConnection connect = null;
            try
            {
                int index = liaisons.FindIndex(x => x.Adresse == p.Adresse);
                if (index > -1)
                {
                    ObjectThread row = new ObjectThread(dgv_liaison.Rows[index]);
                    new Thread(delegate ()
                    {
                        try
                        {
                            Serveur liaison = liaisons[index];
                            connect = new Connexion().returnConnexion(liaison, false);
                            bool error = true;
                            bool tableCreneau = Utils.VerifyTable("creneauhoraire", connect);
                            if (tableCreneau)
                            {
                                error = !Utils.VerifyColumn("creneauhoraire", "externe", connect);
                            }
                            row.ForeColorDataGridViewRow(error ? Color.Red : Color.Black);
                        }
                        catch (Exception ex)
                        {
                            Messages.Exception(ex);
                        }
                    }).Start();
                }
            }
            catch (Exception ex)
            {
                Messages.Exception(ex);
            }
            finally
            {
                Connexion.Close(connect);
            }

        }

        private void btn_test_Click(object sender, EventArgs e)
        {
            try
            {
                if (new TOOLS.Connexion().isConnection(_ls_adresse.Text, Convert.ToInt16(_ls_port.Text), _ls_database.Text, _ls_users.Text, _ls_password.Text))
                {
                    Messages.Information("Connexion effectuée");
                }
            }
            catch (Exception ex)
            {
                Utils.Exception(ex);
            }
        }

        private void txt_login_Leave(object sender, EventArgs e)
        {
            try
            {
                Users u = RecopieUser();
                if (Utils.asString(u.Code))
                {
                    users = UsersBLL.OneByName(u.Code);
                    if (users != null ? users.Id > 0 : false)
                    {
                        if (!users.Actif)
                        {
                            lb_id_users.Text = "0";
                            txt_login.ForeColor = Color.Red;
                            users = new Users();
                            if (Messages.Erreur_Retry("Utilisateur Inactif") == System.Windows.Forms.DialogResult.Retry)
                            {
                                txt_login_Leave(sender, e);
                            }
                            return;
                        }
                        txt_login.ForeColor = Color.Black;
                        lb_id_users.Text = users.Id.ToString();
                    }
                    else
                    {
                        lb_id_users.Text = "0";
                        txt_login.ForeColor = Color.Red;
                        if (Messages.Erreur_Retry("Utilisateur Incorrect") == System.Windows.Forms.DialogResult.Retry)
                        {
                            txt_login_Leave(sender, e);
                        }
                    }
                }
                else
                {
                    Messages.ShowErreur("L'identifiant de l'utilisateur ne peut pas être null!");
                }
            }
            catch (Exception ex)
            {
                Messages.Exception(ex);
            }
        }

        private void relationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Npgsql.NpgsqlConnection connect = new Connexion().returnConnexion(liaison, false);
                if (connect == null)
                {
                    Utils.WriteLog("Connexion externe impossible. veuillez verifier les paramètres de connexion");
                    return;
                }
                bool tableUsers = Utils.VerifyTable("users", connect);
                if (!tableUsers)
                {
                    Messages.ShowErreur("La table utilisateur n'existe pas. Veillez contacter votre administrateur!");
                    return;
                }
                bool colonneUsersExterne = Utils.VerifyColumn("users", "externe", connect);
                if (!colonneUsersExterne)
                {
                    if (DialogResult.Yes == Messages.Erreur_Oui_Non("La colonne de liaison dans la table utilisateur n'existe pas. Voulez-vous la créer?"))
                    {
                        créerColonnesToolStripMenuItem_Click(sender, e);
                    }
                    return;
                }
                bool tableTranche = Utils.VerifyTable("tranchehoraire", connect);
                if (!tableTranche)
                {
                    Messages.ShowErreur("La table tranchehoraire n'existe pas. Veillez contacter votre administrateur!");
                    return;
                }
                bool colonneTrancheExterne = Utils.VerifyColumn("tranchehoraire", "externe", connect);
                if (!colonneTrancheExterne)
                {
                    if (DialogResult.Yes == Messages.Erreur_Oui_Non("La colonne de liaison dans la table tranchehoraire n'existe pas. Voulez-vous la créer?"))
                    {
                        créerColonnesToolStripMenuItem_Click(sender, e);
                    }
                    return;
                }
                bool tableCreneau = Utils.VerifyTable("creneauhoraire", connect);
                if (!tableCreneau)
                {
                    Messages.ShowErreur("La table creneauhoraire n'existe pas. Veillez contacter votre administrateur!");
                    return;
                }
                bool colonneCreneauExterne = Utils.VerifyColumn("creneauhoraire", "externe", connect);
                if (!colonneCreneauExterne)
                {
                    if (DialogResult.Yes == Messages.Erreur_Oui_Non("La colonne de liaison dans la table creneauhoraire n'existe pas. Voulez-vous la créer?"))
                    {
                        créerColonnesToolStripMenuItem_Click(sender, e);
                    }
                    return;
                }
                new Form_Liaison_Externe(connect).ShowDialog();
            }
            catch (Exception ex)
            {
                Messages.Exception(ex);
            }
        }

        private void créerColonnesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Npgsql.NpgsqlConnection connect = null;
            try
            {
                Utils.WriteLog("Début de la création des colonnes de liaison dans les tables (users, tranchehoraire, creneauhoraire)");
                connect = new Connexion().returnConnexion(liaison, false);
                if (connect == null)
                {
                    Utils.WriteLog("-- Connexion externe impossible. veuillez verifier les paramètres de connexion");
                    return;
                }
                bool tableUsers = Utils.VerifyTable("users", connect);
                if (tableUsers)
                {
                    bool colonneUsersExterne = Utils.VerifyColumn("users", "externe", connect);
                    if (!colonneUsersExterne)
                    {
                        string query = "ALTER TABLE users ADD COLUMN externe BIGINT;";
                        Bll.RequeteLibre(connect, query);
                        Utils.WriteLog("-- Création de la colonne de liaison dans la table users effectuée");
                    }

                }
                bool tableTranche = Utils.VerifyTable("tranchehoraire", connect);
                if (tableTranche)
                {
                    bool colonneTrancheExterne = Utils.VerifyColumn("tranchehoraire", "externe", connect);
                    if (!colonneTrancheExterne)
                    {
                        string query = "ALTER TABLE tranchehoraire ADD COLUMN externe BIGINT;";
                        Bll.RequeteLibre(connect, query);
                        Utils.WriteLog("-- Création de la colonne de liaison dans la table tranchehoraire effectuée");
                    }
                }
                bool tableCreneau = Utils.VerifyTable("creneauhoraire", connect);
                if (tableCreneau)
                {
                    bool colonneCreneauExterne = Utils.VerifyColumn("creneauhoraire", "externe", connect);
                    if (!colonneCreneauExterne)
                    {
                        string query = "ALTER TABLE creneauhoraire ADD COLUMN externe BIGINT;";
                        Bll.RequeteLibre(connect, query);
                        Utils.WriteLog("-- Création de la colonne de liaison dans la table creneauhoraire effectuée");
                    }
                }
                Utils.WriteLog("Fin de la création des colonnes de liaison dans les tables (users, tranchehoraire, creneauhoraire)");
            }
            catch (Exception ex)
            {
                Messages.Exception(ex);
            }
            finally
            {
                Connexion.Close(connect);
            }
        }
    }
}
