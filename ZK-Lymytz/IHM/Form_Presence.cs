using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;

using ZK_Lymytz.BLL;
using ZK_Lymytz.IHM;
using ZK_Lymytz.TOOLS;
using ZK_Lymytz.ENTITE;

namespace ZK_Lymytz.IHM
{
    public partial class Form_Presence : Form
    {
        List<Presence> presences = new List<Presence>();
        List<Employe> employes = new List<Employe>();
        Employe employe = new Employe();
        int position = 0;

        public Form_Presence()
        {
            InitializeComponent();
            Configuration.Load(this);
        }

        private void Form_Presence_Load(object sender, EventArgs e)
        {
            lnk_today.Text += " : " + DateTime.Now.ToString("dd MMM yyyy");
            LoadEmploye();
            LoadPresence(DateTime.Now, null);
        }

        private void Form_Presence_FormClosed(object sender, FormClosedEventArgs e)
        {
            Constantes.FORM_PRESENCE = null;
            Utils.WriteLog("Fermeture page (Gestion Présence)");
            Utils.removeFrom("Form_Presence");
        }

        private void LoadEmploye()
        {
            try
            {
                employes.Clear();
                com_employe.Items.Clear();

                employes = EmployeBLL.List(Constantes.QUERY_EMPLOYE(Constantes.SOCIETE));
                foreach (Employe e in employes)
                {
                    com_employe.AutoCompleteCustomSource.Add(e.NomPrenom);
                    com_employe.Items.Add(e.NomPrenom);
                }
                com_employe.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                com_employe.AutoCompleteSource = AutoCompleteSource.CustomSource;

            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Presence (LoadEmploye)", ex);
            }
        }

        private void LoadPresence(DateTime d, Employe e)
        {
            position = 0;
            try
            {
                string query = "select p.* from yvs_grh_presence p inner join yvs_grh_employes e on p.employe = e.id inner join yvs_agences a on e.agence = a.id where a.societe = " + Constantes.SOCIETE.Id + " and p.date_debut = '" + d.ToShortDateString() + "' order by p.heure_debut, e.nom, e.prenom";
                if (e != null ? e.Id > 0 : false)
                {
                    query = "select p.* from yvs_grh_presence p where p.employe = " + e.Id + " and p.date_debut = '" + d.ToShortDateString() + "' order by heure_debut";
                }
                presences = PresenceBLL.List(query);
                if (presences != null ? presences.Count > 0 : false)
                {
                    LoadOnView(presences[0]);
                }
                else
                {
                    ResetFiche();
                    if (e != null ? e.Id > 0 : false)
                        Utils.WriteLog(e.NomPrenom + " n'a pas de fiche de présence pour la date du " + dtp_date.Value.ToShortDateString());
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Presence (LoadPresence)", ex);
            }
        }

        private void LoadPointage(Presence p)
        {
            try
            {
                dgv_pointage.Rows.Clear();
                string query = "select * from yvs_grh_pointage where presence = " + p.Id + " order by heure_entree, heure_sortie";
                List<Pointage> lp = PointageBLL.List(query);
                for (int i = 0; i < lp.Count; i++)
                {
                    dgv_pointage.Rows.Add(new object[] { i + 1, lp[i].HeureEntree.ToShortTimeString(), lp[i].HeureSortie.ToShortTimeString(), Utils.GetTime(lp[i].Duree), lp[i].Valider, lp[i].Supplementaire });
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Presence (LoadPointage)", ex);
            }
        }

        private void LoadOnView(Presence p)
        {
            txt_id.Text = p.Employe.Id.ToString();
            txt_nom.Text = p.Employe.Nom;
            txt_prenom.Text = p.Employe.Prenom;
            txt_matricule.Text = p.Employe.Matricule;
            txt_poste.Text = p.Employe.Poste.Poste.Intitule;

            string path = Constantes.SETTING.CheminPhoto + p.Employe.Photo;
            if (File.Exists(path))
                box_identity.Image = new Bitmap(path);
            else
                box_identity.Image = global::ZK_Lymytz.Properties.Resources.contact;

            txt_date_debut.Text = p.DateDebut.ToString("dd MMM yyyy");
            txt_date_fin.Text = p.DateFin.ToString("dd MMM yyyy");
            txt_heure_debut.Text = p.HeureDebut.ToShortTimeString();
            txt_heure_fin.Text = p.HeureFin.ToShortTimeString();
            txt_total_presence.Text = Utils.GetTime(p.TotalPresence);
            txt_total_suppl.Text = Utils.GetTime(p.TotalSupplementaire);
            rbtn_yes.Checked = p.Valider;
            rbtn_no.Checked = !p.Valider;

            LoadPointage(p);

            lb_pagination.Text = (position + 1) + "/" + presences.Count;
        }

        private void ResetFiche()
        {
            txt_id.Text = "0";
            txt_nom.Text = "";
            txt_prenom.Text = "";
            txt_matricule.Text = "";
            txt_poste.Text = "";
            box_identity.Image = global::ZK_Lymytz.Properties.Resources.contact;

            txt_date_debut.Text = dtp_date.Value.ToString("dd MMM yyyy");
            txt_date_fin.Text = dtp_date.Value.ToString("dd MMM yyyy");
            txt_heure_debut.Text = "00:00:00";
            txt_heure_fin.Text = "00:00:00";
            txt_total_presence.Text = Utils.GetTime(0);
            txt_total_suppl.Text = Utils.GetTime(0);
            rbtn_yes.Checked = false;
            rbtn_no.Checked = false;
        }

        private void com_employe_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nom_prenom = com_employe.Text.Trim();
            if (employe != null ? (employe.Id < 0 || !employe.NomPrenom.Equals(nom_prenom)) : true)
            {
                employe = EmployeBLL.OneByNom(nom_prenom, Constantes.SOCIETE.Id);
                txt_id_search.Text = employe.Id.ToString();
                LoadPresence(dtp_date.Value, employe);
            }
        }

        private void txt_id_search_Leave(object sender, EventArgs e)
        {
            Int32 id = Convert.ToInt32(txt_id_search.Text.Trim());
            if (employe != null ? (employe.Id < 0 || !employe.Id.Equals(id)) : true)
            {
                employe = EmployeBLL.OneById(id, Constantes.SOCIETE.Id);
                com_employe.Text = employe.NomPrenom;
                LoadPresence(dtp_date.Value, employe);
            }

        }

        private void lnk_today_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadPresence(DateTime.Now, null);
            dtp_date.Value = DateTime.Now;
            txt_id_search.Text = "0";
            com_employe.ResetText();
        }

        private void btn_prec_Click(object sender, EventArgs e)
        {
            if (presences != null ? presences.Count > 0 : false)
            {
                position = position > 0 ? position - 1 : presences.Count - 1;
                LoadOnView(presences[position]);
            }
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (presences != null ? presences.Count > 0 : false)
            {
                position = position < presences.Count - 1 ? position + 1 : 0;
                LoadOnView(presences[position]);
            }
        }

        private void dtp_date_ValueChanged(object sender, EventArgs e)
        {
            LoadPresence(dtp_date.Value, null);
            txt_id_search.Text = "0";
            com_employe.ResetText();
        }
    }
}
