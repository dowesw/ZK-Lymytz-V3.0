using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

using ZK_Lymytz.TOOLS;
using ZK_Lymytz.ENTITE;
using ZK_Lymytz.BLL;

namespace ZK_Lymytz.IHM
{
    public partial class Form_Societe : Form
    {
        bool ask_ = false, start = true;
        private List<Societe> societes = new List<Societe>();
        private Societe societe = new Societe();

        public Form_Societe(bool start)
        {
            InitializeComponent();
            Configuration.Load(this);
            this.start = start;
        }

        private void Form_Societe_Load(object sender, EventArgs e)
        {
            LoadSociete();
            LoadCurrentSociete();
        }

        private void Form_Societe_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (start && !ask_)
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

        private void Form_Societe_FormClosed(object sender, FormClosedEventArgs e)
        {
            Constantes.FORM_SOCIETE = null;
            Utils.WriteLog("Fermeture page (Gestion Socièté)");
            Utils.removeFrom("Form_Societe");
        }

        private void LoadCurrentSociete()
        {
            Societe s = SocieteBLL.ReturnSociete();
            if (s != null ? s.Id > 0 : false)
            {
                cbox_societe.SelectedText = s.Name;
                societe = s;
                txt_name.Text = s.Name;
                txt_adresse.Text = s.AdresseIp;
                cbox_type_connexion.Text = s.TypeConnexion;
                txt_users.Text = s.Users;
                txt_domain.Text = s.Domain;
                txt_password.Text = s.Password;
                txt_port.Value = s.Port;
            }
        }

        private void LoadSociete()
        {
            societes = SocieteBLL.List("select y.id, y.name, y.adresse_ip, COALESCE(i.port, 0) AS port, i.users, i.password, i.domain, i.type_connexion " +
                            "from yvs_societes y left join yvs_societes_connexion i on i.societe = y.id ");
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
                Messages.Exception("Form_Societe (LoadSociete)", ex);
                cbox_societe.Items.Clear();
            }

        }

        private void cbox_societe_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                String name = cbox_societe.Text.Trim().Replace("'", "''");
                societe = societes[cbox_societe.SelectedIndex];
                txt_name.Text = societe.Name;
                txt_adresse.Text = societe.AdresseIp;
                cbox_type_connexion.Text = societe.TypeConnexion;
                txt_users.Text = societe.Users;
                txt_domain.Text = societe.Domain;
                txt_password.Text = societe.Password;
                txt_port.Value = societe.Port;
            }
            catch (Exception ex)
            {
                Messages.Exception(ex);
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (societe != null ? societe.Id > 0 : false)
            {
                string adresse = txt_adresse.Text;
                societe.AdresseIp = adresse;
                societe.TypeConnexion = cbox_type_connexion.SelectedItem.ToString();
                societe.Users = txt_users.Text;
                societe.Domain = txt_domain.Text;
                societe.Password = txt_password.Text;
                societe.Port = Convert.ToInt32(txt_port.Value);

                if (SocieteBLL.Update(societe))
                {
                    if (start)
                    {
                        ask_ = true;
                        Application.Restart();
                        return;
                    }
                    else
                    {
                        if (Constantes.SOCIETE.Id.Equals(societe.Id) ? SocieteBLL.CreateSociete(societe) : false)
                        {
                            Constantes.SOCIETE = societe;
                            if (Constantes.FORM_PARENT != null)
                            {
                                Constantes.FORM_PARENT.LoadInfosSociete();
                            }
                        }
                    }
                }
                Messages.Succes();
            }
        }
    }
}
