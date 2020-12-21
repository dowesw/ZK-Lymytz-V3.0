using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;

using ZK_Lymytz.TOOLS;
using ZK_Lymytz.ENTITE;
using ZK_Lymytz.BLL;

namespace ZK_Lymytz.IHM
{
    public partial class Form_Users : Form
    {
        bool ask_ = false;
        List<Societe> societes = new List<Societe>();
        List<Agence> agences = new List<Agence>();
        Users users = new Users();
        Societe societe = new Societe();
        Agence agence = new Agence();
        public Form_Users()
        {
            InitializeComponent();
            Configuration.Load(this);
        }

        private void Form_Users_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ask_)
            {
                if (Messages.Confirmation(Mots.Msg_FermerApplication.ToLower()) == System.Windows.Forms.DialogResult.Yes)
                {
                    Environment.Exit(0);
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void Form_Users_FormClosed(object sender, FormClosedEventArgs e)
        {
            Constantes.FORM_USERS = null;
            Utils.WriteLog("Fermeture page (Connexion)");
            Utils.removeFrom("Form_Users");
        }

        private void Form_Users_Load(object sender, EventArgs e)
        {
            txt_domain.Text = Chemins.domainName;
            txt_name.Text = Chemins.usersName;
        }

        private Users Recopiew()
        {
            Users u = new Users();
            u.Code = txt_login.Text.Trim();
            u.Name = txt_name.Text.Trim();
            u.PasswordPC = txt_password_pc.Text.Trim();
            string password_log = txt_password_log.Text.Trim();
            if (Utils.asString(password_log))
                u.PasswordLog = Utils.GetMd5Hash(txt_password_log.Text.Trim());
            return u;
        }

        private void ResetFiche()
        {
            com_societe.Enabled = false;
            com_agence.Enabled = false;

            societes.Clear();
            agences.Clear();

            societe = new Societe();
            agence = new Agence();
            users = new Users();
        }

        private void txt_login_Leave(object sender, EventArgs e)
        {
            try
            {
                ResetFiche();
                Users u = Recopiew();
                if (Utils.asString(u.Code))
                {
                    users = UsersBLL.OneByName(u.Code);
                    if (users != null ? users.Id > 0 : false)
                    {
                        if (!users.Actif)
                        {
                            if (Messages.Erreur_Retry("Utilisateur Inactif") == System.Windows.Forms.DialogResult.Retry)
                            {
                                txt_login_Leave(sender, e);
                            }
                            return;
                        }
                        com_societe.Enabled = users.AccesMultiSociete;
                        com_agence.Enabled = users.AccesMultiSociete;
                        if (users.AccesMultiSociete)
                        {
                            if (users.SuperAdmin)
                            {
                                societes = SocieteBLL.List("select y.id, y.name, y.adresse_ip, COALESCE(i.port, 0) AS port, i.users, i.password, i.domain, i.type_connexion " +
                            "from yvs_societes y left join yvs_societes_connexion i on i.societe = y.id ");
                            }
                            else if (users.Agence.Societe.Groupe > 0)
                            {
                                societes = SocieteBLL.List("select y.id, y.name, y.adresse_ip, COALESCE(i.port, 0) AS port, i.users, i.password, i.domain, i.type_connexion " +
                            "from yvs_societes y left join yvs_societes_connexion i on i.societe = y.id where y.groupe = " + users.Agence.Societe.Groupe);
                            }
                            else
                            {
                                societes.Add(users.Agence.Societe);
                            }
                        }
                        else if (users.AccesMultiAgence)
                        {
                            societes.Add(users.Agence.Societe);
                        }
                        else
                        {
                            agence = users.Agence;
                            societe = agence.Societe;
                            societes.Add(societe);
                        }
                        if (societes.Count > 0)
                        {
                            LoadSociete(societes);
                        }
                    }
                    else
                    {
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

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                Users u = Recopiew();
                if (societe != null ? societe.Id < 1 : true)
                {
                    Messages.ShowErreur("Vous devez selectionner une socièté");
                    return;
                }
                if (agence != null ? agence.Id < 1 : true)
                {
                    Messages.ShowErreur("Vous devez selectionner une agence");
                    return;
                }
                if (users != null ? users.Id > 0 : false)
                {
                    if ((Utils.asString(u.Name) && Utils.asString(u.PasswordPC)) ? Utils.IsAuthenticated(u.Name, u.PasswordPC) : false)
                    {
                        users.Name = u.Name;
                        users.PasswordPC = u.PasswordPC;
                    }
                    users.PasswordLog = u.PasswordLog;
                    Loggin();
                }
            }
            catch (Exception ex)
            {
                Messages.Exception(ex);
            }
        }

        private void btn_pass_Click(object sender, EventArgs e)
        {
            Users u = Recopiew();
            if (UsersBLL.CreateUsers(u))
            {
                ask_ = true;
                Application.Restart();
            }
        }

        private void com_societe_SelectedIndexChanged(object sender, EventArgs e)
        {
            societe = societes[com_societe.SelectedIndex];
            if (users.AccesMultiAgence)
            {
                agences = AgenceBLL.List("select * from yvs_agences where societe = " + societe.Id);
            }
            else
            {
                agences.Add(users.Agence);
            }
            if (agences.Count > 0)
            {
                agence = agences[0];
                LoadAgence(agences);
            }
        }

        private void com_agence_SelectedIndexChanged(object sender, EventArgs e)
        {
            agence = agences[com_agence.SelectedIndex];
        }

        private void LoadSociete(List<Societe> societes)
        {
            try
            {
                com_societe.Items.Clear();
                for (int i = 0; i < societes.Count; i++)
                {
                    com_societe.Items.Add(societes[i].Name);
                    com_societe.AutoCompleteCustomSource.Add(societes[i].Name);
                }
                com_societe.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                com_societe.AutoCompleteSource = AutoCompleteSource.CustomSource;
                int index = societes.FindIndex(x => x.Id == users.Agence.Societe.Id);
                com_societe.SelectedIndex = index > -1 ? index : 0;
            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Users (LoadSociete)", ex);
                com_societe.Items.Clear();
            }
        }

        private void LoadAgence(List<Agence> agences)
        {
            try
            {
                com_agence.Items.Clear();
                for (int i = 0; i < agences.Count; i++)
                {
                    com_agence.Items.Add(agences[i].Name);
                    com_agence.AutoCompleteCustomSource.Add(agences[i].Name);
                }
                com_agence.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                com_agence.AutoCompleteSource = AutoCompleteSource.CustomSource;
                int index = agences.FindIndex(x => x.Id == users.Agence.Id);
                com_agence.SelectedIndex = index > -1 ? index : 0;
            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Users (LoadAgence)", ex);
                com_agence.Items.Clear();
            }
        }

        private void Loggin()
        {
            try
            {
                string query = "select id from yvs_users_agence where users = " + users.Id + " and agence = " + agence.Id;
                object author = Bll.LoadOneObject(query, null);
                users.Author = author != null ? Convert.ToInt32(author.ToString()) : 0;
                if (UsersBLL.CreateUsers(users))
                {
                    societe = SocieteBLL.OneById(societe.Id);
                    SocieteBLL.CreateSociete(societe);
                    AgenceBLL.CreateAgence(agence);
                    ask_ = true;
                    Application.Restart();
                }
            }
            catch (Exception ex)
            {
                Messages.Exception(ex);
            }
        }
    }
}
