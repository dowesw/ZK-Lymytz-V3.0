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
using ZK_Lymytz.DAO;
using ZK_Lymytz.IHM;
using ZK_Lymytz.TOOLS;
using ZK_Lymytz.ENTITE;

namespace ZK_Lymytz.IHM
{
    public partial class Form_Presence : Form
    {
        public List<Presence> presences = new List<Presence>();
        List<Pointage> pointages = new List<Pointage>();
        List<Employe> employes = new List<Employe>();
        Employe employe = new Employe();
        ObjectThread object_statut;

        Presence presence;

        int position = 0;

        public Form_Presence()
        {
            InitializeComponent();
            Configuration.Load(this);
            object_statut = new ObjectThread(pbar_statut);
        }

        public Presence Presence
        {
            get { return presence; }
            set { presence = value; }
        }

        public Form_Presence(Presence presence)
        {
            InitializeComponent();
            Configuration.Load(this);
            object_statut = new ObjectThread(pbar_statut);
            this.presence = presence;
        }

        private void Form_Presence_Load(object sender, EventArgs e)
        {
            lnk_today.Text += " : " + DateTime.Now.ToString("dd MMM yyyy");
            LoadEmploye();
            InitForm();
        }

        public void InitForm()
        {
            if (presence != null ? presence.Id > 0 : false)
                LoadPresence(presence.DateDebut, presence.Employe);
            else
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
                com_employe.Items.Add("");

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
            object_statut.SetValueBar(0);
            Constantes.PBAR_WAIT = pbar_statut;
            position = 0;
            try
            {
                string query = "select p.* from yvs_grh_presence p inner join yvs_grh_employes e on p.employe = e.id inner join yvs_agences a on e.agence = a.id where a.societe = " + Constantes.SOCIETE.Id + " and p.date_debut = '" + d.ToShortDateString() + "' order by e.nom, e.prenom, p.heure_debut";
                string queryCount = "select count(p.id) from yvs_grh_presence p inner join yvs_grh_employes e on p.employe = e.id inner join yvs_agences a on e.agence = a.id where a.societe = " + Constantes.SOCIETE.Id + " and p.date_debut = '" + d.ToShortDateString() + "'";
                if (e != null ? e.Id > 0 : false)
                {
                    query = "select p.* from yvs_grh_presence p where p.employe = " + e.Id + " and p.date_debut = '" + d.ToShortDateString() + "' order by heure_debut";
                    queryCount = "select count(p.id) from yvs_grh_presence p where p.employe = " + e.Id + " and p.date_debut = '" + d.ToShortDateString() + "'";
                }
                presences = PresenceBLL.List(query, true, queryCount, Constantes.SOCIETE.AdresseIp);
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
                string adresse = Constantes.SOCIETE.AdresseIp;
                object_statut.SetValueBar(0);
                Constantes.PBAR_WAIT = pbar_statut;

                ObjectThread o = new ObjectThread(dgv_pointage);
                o.ClearDataGridView(true);

                string query = "select * from yvs_grh_pointage where presence = " + p.Id + " order by heure_entree, heure_sortie";
                string queryCount = "select count(id) from yvs_grh_pointage where presence = " + p.Id + "";
                pointages = PointageBLL.List(query, true, queryCount, adresse);
                for (int i = 0; i < pointages.Count; i++)
                {
                    o.WriteDataGridView(new object[] { pointages[i].Id, i + 1, pointages[i].HeureEntree.ToShortTimeString(), pointages[i].HeureSortie.ToShortTimeString(), Utils.GetTime(pointages[i].Duree), pointages[i].Valider, pointages[i].Supplementaire });
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
            txt_date_fin.Text = p.DateFinPrevu.ToString("dd MMM yyyy");
            txt_heure_debut.Text = p.HeureDebut.ToShortTimeString();
            txt_heure_fin.Text = p.HeureFinPrevu.ToShortTimeString();
            txt_total_presence.Text = Utils.GetTime(p.TotalPresence);
            txt_total_suppl.Text = Utils.GetTime(p.TotalSupplementaire);
            rbtn_yes.Checked = p.Valider;
            rbtn_no.Checked = !p.Valider;

            LoadPointage(p);

            lb_pagination.Text = (position + 1) + "/" + presences.Count;
            Constantes.LoadPatience(true);
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

            dgv_pointage.Rows.Clear();
        }

        private void com_employe_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nom_prenom = com_employe.Text.Trim();
            if (!(nom_prenom == null || nom_prenom.Trim().Equals("")))
            {
                if (employe != null ? (employe.Id < 0 || !employe.NomPrenom.Equals(nom_prenom)) : true)
                {
                    employe = EmployeBLL.OneByNom(nom_prenom, Constantes.SOCIETE.Id);
                    txt_id_search.Text = employe.Id.ToString();
                    LoadPresence(dtp_date.Value, employe);
                }
            }
            else
            {
                txt_id_search.Text = "0";
                com_employe.ResetText();
                employe = null;
                LoadPresence(dtp_date.Value, null);
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
            if (employe != null ? employe.Id < 1 : true)
            {
                if (presences != null ? presences.Count > 0 : false)
                {
                    position = position > 0 ? position - 1 : presences.Count - 1;
                    LoadOnView(presences[position]);
                }
            }
            else
            {
                dtp_date.Value = dtp_date.Value.AddDays(-1);
            }
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (employe != null ? employe.Id < 1 : true)
            {
                if (presences != null ? presences.Count > 0 : false)
                {
                    position = position < presences.Count - 1 ? position + 1 : 0;
                    LoadOnView(presences[position]);
                }
            }
            else
            {
                dtp_date.Value = dtp_date.Value.AddDays(1);
            }
        }

        private void dtp_date_ValueChanged(object sender, EventArgs e)
        {
            LoadPresence(dtp_date.Value, employe);
        }

        private void dgv_pointage_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewCell cell = this.dgv_pointage.Rows[e.RowIndex].Cells[0];
            Pointage po = PointageBLL.OneById(Convert.ToInt32(cell.Value));
            if ((e.ColumnIndex == this.dgv_pointage.Columns["heure_sortie"].Index) && e.Value != null)
            {
                cell = this.dgv_pointage.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if ((po.HeureSortie != null) ? po.HeureSortie.ToString() != "01/01/0001 00:00:00" : false)
                {
                    cell.ToolTipText = po.HeureSortie.ToString();
                }
            }
            else if ((e.ColumnIndex == this.dgv_pointage.Columns["heure_entree"].Index) && e.Value != null)
            {
                cell = this.dgv_pointage.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if ((po.HeureEntree != null) ? po.HeureEntree.ToString() != "01/01/0001 00:00:00" : false)
                {
                    cell.ToolTipText = po.HeureEntree.ToString();
                }
            }
        }

        private void reorganiserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (position > -1)
            {
                Presence presence = presences[position];
                presence.Pointages = pointages;
                ReorganiserFiche(presence);
            }
        }

        public void ReorganiserFiche(Presence presence)
        {
            if (presence != null ? presence.Id > 0 : false)
            {
                string adresse = Constantes.SOCIETE.AdresseIp;
                if (Dao.RequeteLibre("delete from yvs_grh_pointage where presence = " + presence.Id, adresse))
                {
                    List<DateTime> times = new List<DateTime>();
                    foreach (Pointage po in presence.Pointages)
                    {
                        if ((po.HeureEntree != null) ? po.HeureEntree.ToString() != "01/01/0001 00:00:00" : false)
                            times.Add(po.HeureEntree);
                        if ((po.HeureSortie != null) ? po.HeureSortie.ToString() != "01/01/0001 00:00:00" : false)
                            times.Add(po.HeureSortie);
                    }
                    times.Sort();

                    presence.HeureDebut = Utils.TimeStamp(presence.DateDebut, presence.HeureDebut);
                    presence.HeureFin = Utils.TimeStamp(presence.DateFin, presence.HeureFin);
                    foreach (DateTime current_time in times)
                    {
                        Fonctions.OnSavePointage(presence, current_time, null, 0, adresse);
                    }
                    LoadOnView(presence);
                    Messages.Succes();
                }
            }
        }

        private void fusionnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (position > -1)
            {
                Presence presence = presences[position];
                presence.Pointages = pointages;
                if (presence != null ? presence.Id > 0 : false)
                {
                    string adresse = Constantes.SOCIETE.AdresseIp;
                    List<Presence> list = PresenceBLL.List("select p.* from yvs_grh_presence p where p.employe = " + presence.Employe.Id + " and p.date_debut = '" + dtp_date.Value + "' order by heure_debut", true, adresse);
                    if (list != null ? list.Count > 1 : false)
                    {
                        foreach (Presence p in list)
                        {
                            if (p.Id != presence.Id)
                            {
                                presence.Pointages.AddRange(PointageBLL.List("select * from yvs_grh_pointage where presence = " + p.Id, true, adresse));
                                Bll.RequeteLibre("delete from yvs_grh_presence where id = " + p.Id, adresse);
                                presences.Remove(p);
                                lb_pagination.Text = (position + 1) + "/" + presences.Count;
                                txt_index_of.Text = (position + 1).ToString();
                            }
                        }
                    }
                    ReorganiserFiche(presence);
                }
            }
        }

        private void reevaluerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (position > -1)
            {
                Presence presence = presences[position];
                if (presence != null ? presence.Id > 0 : false)
                {
                    string adresse = Constantes.SOCIETE.AdresseIp;
                    if (Bll.RequeteLibre("select reevaluer_total_presence(" + presence.Id + "::bigint)", adresse))
                    {
                        presence = PresenceBLL.OneById((int)presence.Id);
                        presences.RemoveAt(position);
                        presences.Insert(position, presence);
                        LoadOnView(presence);
                    }
                }
            }
        }

        private void txt_index_of_Leave(object sender, EventArgs e)
        {
            position = Convert.ToInt32(txt_index_of.Text);
            if (position < presences.Count && position > -1)
            {
                position -= 1;
                LoadOnView(presences[position]);
            }
        }

        private void actualiserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (employe != null ? employe.Id > 0 : false)
                LoadPresence(dtp_date.Value, employe);
        }

        private void synchroniserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((presences != null ? presences.Count > 0 : false) && (Constantes.POINTEUSES != null ? Constantes.POINTEUSES.Count > 0 : false))
            {
                Presence p = presences[position];
                Employe emp = p.Employe;
                Utils.WriteLog("Demande de synchronisation des logs de l'employe " + emp.NomPrenom + "");
                if (Messages.Confirmation_Infos("synchroniser") == System.Windows.Forms.DialogResult.Yes)
                {
                    if (p.Id > 0)
                    {
                        if (p.Pointeuses == null)
                        {
                            p.Pointeuses = new List<string>();
                        }
                        if (p.Pointeuses.Count < 1)
                        {
                            foreach (Pointeuse y in Constantes.POINTEUSES)
                            {
                                if (y.Logs.Count > 0)
                                {
                                    p.Pointeuses.Add(y.Id.ToString());
                                }
                            }
                        }
                        if (p.Pointeuses.Count > 0)
                        {
                            object_statut.SetValueBar(0);
                            Constantes.PBAR_WAIT = pbar_statut;

                            List<IOEMDevice> logs = new List<IOEMDevice>();
                            foreach (string id in p.Pointeuses)
                            {
                                Pointeuse y = Constantes.POINTEUSES.Find(x => x.Id == Convert.ToInt32(id));
                                if (y != null ? y.Id > 0 : false)
                                    logs.InsertRange(logs.Count, Utils.FindLogsInFileTamponLogs(y.Logs, true, emp, true, p.DateDebut, p.DateFin));
                            }

                            if (logs.Count > 0)
                            {
                                Pointeuse y = Constantes.POINTEUSES.Find(x => x.Id == Convert.ToInt32(p.Pointeuses[0]));
                                object_statut.UpdateMaxBar(0);
                                Thread thread = new Thread(delegate() { SynchroniseFichePresence(logs, y, p); });
                                thread.Start();
                            }
                            else
                            {
                                Utils.WriteLog("-- Synchronisation impossible car le log est vide");
                            }
                        }
                    }
                }
                else
                {
                    Utils.WriteLog("-- Synchronisation des logs de l'employe " + emp.NomPrenom + " annulée");
                }
            }
        }

        public void SynchroniseFichePresence(List<IOEMDevice> logs, Pointeuse y, Presence p)
        {
            Employe emp = p.Employe;
            Thread thread = new Thread(delegate() { Fonctions.SynchroniseServer(logs, y.Ip, false, emp, true, p.DateDebut, p.DateFin, false); });
            thread.Start();
            if (thread.IsAlive)
            {
                thread.Join();
            }
            LoadPresence(dtp_date.Value, emp);
            Utils.WriteLog("-- Synchronisation des logs de l'employe " + emp.NomPrenom + " terminée");
        }

        private void voirLesHeuresPrévuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (position > -1)
            {
                new Dial_View_Heure_Prevu(this, presences[position]).ShowDialog();
            }
        }
    }
}
