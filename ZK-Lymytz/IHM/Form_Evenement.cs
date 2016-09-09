using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using ZK_Lymytz.BLL;
using ZK_Lymytz.IHM;
using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;

namespace ZK_Lymytz.IHM
{
    public partial class Form_Evenement : Form
    {
        List<Employe> employes = new List<Employe>();
        List<Employe> employes_select = new List<Employe>();
        Employe employe = new Employe();
        Pointeuse currentPointeuse = new Pointeuse();
        List<IOEMDevice> lIO = new List<IOEMDevice>();
        DateTime dateDebut = new DateTime(), dateFin = new DateTime(), heureDebut = new DateTime(), heureFin = new DateTime();
        bool _first = true;

        public Form_Evenement()
        {
            InitializeComponent();
            Configuration.Load(this);
        }

        private void Form_Evenement_FormClosed(object sender, FormClosedEventArgs e)
        {
            Constantes.FORM_EVENEMENT = null;
            Utils.WriteLog("Fermeture page (Entrée/Sortie)");
            Utils.removeFrom("Form_Evenement");
        }

        private void Form_Evenement_Load(object sender, EventArgs e)
        {
            LoadEmploye();
            LoadPointeuse();
            AddCheckBoxHeader();
        }

        public void ResetDataPointeuse()
        {
            for (int i = 0; i < dgv_pointeuse.Rows.Count; i++)
            {
                dgv_pointeuse.Rows[i].Selected = false;
            }
            currentPointeuse = null;
        }

        public void ResetDataEmploye()
        {
            for (int i = 0; i < dgv_employe.Rows.Count; i++)
            {
                dgv_employe.Rows[i].Selected = false;
            }
        }

        private void ResetFiche()
        {
            com_employe.ResetText();
            txt_id.Text = "0";
            employe = new Employe();
            dateDebut = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 7, 30, 0);
            dateFin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 30, 0);
            chk_date.Checked = false;
            chk_employe.Checked = false;
            dtp_date_debut.ResetText();
            dtp_date_fin.ResetText();
            txt_debut.Text = dateDebut.ToShortDateString() + " " + dateDebut.ToShortTimeString();
            txt_fin.Text = dateFin.ToShortDateString() + " " + dateFin.ToShortTimeString();
        }

        public void LoadEmploye()
        {
            try
            {
                employes.Clear();
                dgv_employe.Rows.Clear();

                string query = "select e.* from yvs_grh_employes e inner join yvs_agences a on e.agence = a.id where a.societe = " + Constantes.SOCIETE.Id + " order by e.nom, e.prenom";
                employes = EmployeBLL.List(query);
                com_employe.DisplayMember = "NomPrenom";
                com_employe.ValueMember = "Id";
                com_employe.DataSource = new BindingSource(employes, null);

                for (int i = 0; i < employes.Count; i++)
                {
                    Employe e = employes[i];
                    com_employe.AutoCompleteCustomSource.Add(e.NomPrenom);
                    dgv_employe.Rows.Add(new object[] { e.Id, false, e.NomPrenom });
                }
                com_employe.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                com_employe.AutoCompleteSource = AutoCompleteSource.CustomSource;

                ResetFiche();
            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Add_Empreinte (LoadEmploye)", ex);
            }
        }

        public void LoadPointeuse()
        {
            if (Constantes.POINTEUSES.Count < 1)
            {
                Constantes.POINTEUSES = PointeuseBLL.List("select * from yvs_pointeuse where societe = " + Constantes.SOCIETE.Id + " order by adresse_ip");
            }
            dgv_pointeuse.Rows.Clear();
            ObjectThread o = new ObjectThread(dgv_pointeuse);
            foreach (Pointeuse p in Constantes.POINTEUSES)
            {
                o.WriteDataGridView(new object[] { p.Id, p.Ip, p.Port, p.Emplacement, p.IMachine, p.Connecter, p.Actif });
            }
            ResetDataPointeuse();
        }


        private void AddCheckBoxHeader()
        {
            // add checkbox header
            Rectangle rect = dgv_employe.GetCellDisplayRectangle(1, -1, true);
            // set checkbox header to center of header cell. +1 pixel to position correctly.
            rect.X = 7;
            rect.Y = 2;

            CheckBox checkboxHeader = new CheckBox();
            checkboxHeader.Name = "checkboxHeader";
            checkboxHeader.Size = new Size(18, 18);
            checkboxHeader.Location = rect.Location;
            checkboxHeader.BackColor = Color.Transparent;
            checkboxHeader.CheckedChanged += new EventHandler(checkboxHeader_CheckedChanged);

            dgv_employe.Controls.Add(checkboxHeader);
        }

        private void checkboxHeader_CheckedChanged(object sender, EventArgs e)
        {
            employes_select.Clear();
            bool check = ((CheckBox)dgv_employe.Controls.Find("checkboxHeader", true)[0]).Checked;
            for (int i = 0; i < dgv_employe.RowCount; i++)
            {
                if (check)
                {
                    employes_select.Add(employes[i]);
                }
                dgv_employe[1, i].Value = check;
            }
            dgv_employe.EndEdit();
        }

        public void LoadLogs(List<IOEMDevice> l)
        {
            ObjectThread o_ = new ObjectThread(dgv_log);
            o_.ClearDataGridView(true);
            if (l != null ? l.Count > 0 : false)
            {
                ObjectThread o1 = new ObjectThread(Constantes.PBAR_WAIT);
                o1.UpdateMaxBar(Constantes.PBAR_WAIT.Maximum + l.Count);
                int i = 0;
                foreach (IOEMDevice o in l)
                {
                    ++i;
                    Employe e = EmployeBLL.OneById(o.idwSEnrollNumber);
                    if (e != null ? e.Id < 1 : true)
                    {
                        e = new Employe(o.idwSEnrollNumber, o.idwSEnrollNumber.ToString(), "");
                    }
                    DateTime date = new DateTime(o.idwYear, o.idwMonth, o.idwDay, o.idwHour, o.idwMinute, o.idwSecond);

                    o_.WriteDataGridView(new object[] { i, e.Id, e.Nom + " " + e.Prenom, date.ToShortDateString(), date.ToShortTimeString() });
                    Constantes.LoadPatience(false);
                }
                Constantes.LoadPatience(true);
            }
            else
            {
                Utils.WriteLog("La liste des logs venants de la pointeuse (" + currentPointeuse.Ip + ") est vide");
            }
        }

        public void LoadData_()
        {
            lIO.Clear();
            List<IOEMDevice> lIO_ = Logs.ReadCsv(Chemins.getCheminBackup(currentPointeuse.Ip) + "07-06-2016.csv");
            if (lIO_ != null)
            {
                foreach (IOEMDevice i in lIO_)
                {
                    if (chk_filter.Checked)
                    {
                        DateTime h = new DateTime(i.idwYear, i.idwMonth, i.idwDay, i.idwHour, i.idwMinute, i.idwSecond);
                        DateTime d = dtp_date_debut.Value;
                        DateTime f = dtp_date_fin.Value;
                        if (chk_employe.Checked)
                        {
                            if (employe != null ? employe.Id > 0 : false)
                            {
                                if (chk_date.Checked)
                                {
                                    if (i.idwSEnrollNumber == employe.Id && (d <= h && h <= f))
                                    {
                                        lIO.Add(i);
                                    }
                                }
                                else
                                {
                                    if (i.idwSEnrollNumber == employe.Id)
                                    {
                                        lIO.Add(i);
                                    }
                                }
                            }
                            else
                            {
                                Messages.ShowErreur("Vous devez selectionner un employé");
                            }
                        }
                        else
                        {
                            if (chk_date.Checked)
                            {
                                if (d <= h && h <= f)
                                {
                                    lIO.Add(i);
                                }
                            }
                            else
                            {
                                lIO.Add(i);
                            }
                        }
                    }
                    else
                    {
                        //if (i.idwSEnrollNumber == 3065)
                        //{
                        lIO.Add(i);
                        //}
                    }
                    Constantes.LoadPatience(false);
                }
            }
            ObjectThread o = new ObjectThread(Constantes.PBAR_WAIT);
            o.UpdateMaxBar(lIO.Count);
            LoadLogs(lIO);
        }

        public void LoadData()
        {
            lIO.Clear();
            Utils.SetZkemkeeper(ref currentPointeuse);
            Appareil z = currentPointeuse.Zkemkeeper;
            if (z == null)
            {
                Utils.WriteLog("La liaison avec l'appareil " + currentPointeuse.Ip + " est corrompue");
                return;
            }
            if (chk_filter.Checked)
            {
                DateTime d = dateDebut.AddMinutes(-Convert.ToDouble(txt_marge_heure_debut.Value));
                DateTime f = dateFin.AddMinutes(Convert.ToDouble(txt_marge_heure_fin.Value));
                lIO = z.GetAllAttentdData(currentPointeuse.IMachine, currentPointeuse.Connecter, employe, chk_date.Checked, d, f);
            }
            else
            {
                if (chk_exclus.Checked)
                {
                    lIO = z.GetAllAttentdDataEx(currentPointeuse.IMachine, currentPointeuse.Connecter, employes_select, mc_date_exclus.BoldedDates);
                }
                else
                {
                    lIO = z.GetAllAttentdData(currentPointeuse.IMachine, currentPointeuse.Connecter);
                }
            }
            ObjectThread o = new ObjectThread(Constantes.PBAR_WAIT);
            o.UpdateMaxBar(lIO.Count);
            LoadLogs(lIO);
        }

        private void dgv_pointeuse_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_pointeuse.CurrentRow.Cells["ID"].Value != null)
                {
                    int id = Convert.ToInt32(dgv_pointeuse.CurrentRow.Cells["ID"].Value);
                    if (id > 0)
                    {
                        currentPointeuse = PointeuseBLL.OneById(id);
                        dgv_log.Rows.Clear();
                    }
                    else
                    {
                        ResetDataPointeuse();
                    }
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Evenement (dgv_pointeuse_CellContentClick) ", ex);
            }
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            if (currentPointeuse != null ? currentPointeuse.Id > 0 : false)
            {
                pbar_statut.Value = 0;
                Constantes.PBAR_WAIT = pbar_statut;
                Thread t = new Thread(new ThreadStart(LoadData));
                t.Start();
            }
            else
            {
                Utils.WriteLog("Vous devez selectionner la pointeuse");
            }
        }

        private void btn_synchro_Click(object sender, EventArgs e)
        {
            Utils.WriteLog("Demande de synchronisation des logs de l'appareil " + currentPointeuse.Ip + "");
            if (Messages.Confirmation_Infos("synchroniser") == System.Windows.Forms.DialogResult.Yes)
            {
                if (lIO.Count > 0)
                {
                    pbar_statut.Value = 0;
                    Constantes.PBAR_WAIT = pbar_statut;
                    Thread t = new Thread(new ThreadStart(Synchro));
                    t.Start();
                }
                else
                {
                    Utils.WriteLog("-- Synchronisation impossible car le log est vide");
                }
            }
            else
            {
                Utils.WriteLog("-- Synchronisation des logs de l'appareil " + currentPointeuse.Ip + " annulée");
            }
        }

        public void Synchro()
        {
            ObjectThread o = new ObjectThread(Constantes.PBAR_WAIT);
            o.UpdateMaxBar(0);
            if (chk_filter.Checked)
            {
                Fonctions.SynchroniseServer(lIO, currentPointeuse.Ip, false, currentPointeuse.Zkemkeeper, employe, chk_date.Checked, dateDebut, dateFin);
            }
            else
            {
                Fonctions.SynchroniseServer(lIO, currentPointeuse.Ip, false, currentPointeuse.Zkemkeeper);
            }
        }

        private void btn_clean_memory_Click(object sender, EventArgs e)
        {
            if (currentPointeuse != null ? currentPointeuse.Id > 0 : false)
            {
                Utils.SetZkemkeeper(ref currentPointeuse);
                Appareil z = currentPointeuse.Zkemkeeper;
                if (z == null)
                {
                    Utils.WriteLog("La liaison avec l'appareil " + currentPointeuse.Ip + " est corrompue");
                    return;
                }
                Utils.WriteLog("Demande du nettoyage de la memoire des logs de l'appareil " + currentPointeuse.Ip + "");
                if (Messages.Confirmation("nettoyer la memoire") == System.Windows.Forms.DialogResult.Yes)
                {
                    if (z.ClearGLog(currentPointeuse.IMachine))
                    {
                        Utils.WriteLog("-- Nettoyage de la memoire des logs de l'appareil " + currentPointeuse.Ip + " effectuée");
                    }
                }
                else
                {
                    Utils.WriteLog("-- Nettoyage de la memoire des logs de l'appareil " + currentPointeuse.Ip + " annulée");
                }
            }
            else
            {
                Utils.WriteLog("Vous devez selectionner la pointeuse");
            }
        }

        private void chk_filter_CheckedChanged(object sender, EventArgs e)
        {
            ResetFiche();
            grp_filter.Enabled = chk_filter.Checked;
            SearchPlanning();
            if (chk_filter.Checked)
            {
                bool b = chk_exclus.Checked;
                if (b)
                {
                    if (DialogResult.Yes == Messages.Erreur_Oui_Non("Vous ne pouvez pas effectuer un filtrage et une exclusion. Annuler l'exclusion?"))
                    {
                        chk_exclus.Checked = false;
                    }
                    else
                    {
                        chk_filter.Checked = false;
                    }
                }
            }
        }

        private void chk_employe_CheckedChanged(object sender, EventArgs e)
        {
            com_employe.Enabled = chk_employe.Checked;
            employe = new Employe();
            com_employe.ResetText();
            txt_id.Text = "0";
        }

        private void chk_date_CheckedChanged(object sender, EventArgs e)
        {
            dtp_date_fin.Enabled = chk_date.Checked;
            dtp_date_debut.Enabled = chk_date.Checked;
            dtp_heure_fin.Enabled = chk_date.Checked;
            dtp_heure_debut.Enabled = chk_date.Checked;
            txt_marge_heure_debut.Enabled = chk_date.Checked;
            txt_marge_heure_fin.Enabled = chk_date.Checked;
        }

        private void com_employe_SelectedIndexChanged(object sender, EventArgs e)
        {
            Employe a = com_employe.SelectedItem as Employe;
            employe = employes.Find(x => x.Id == a.Id);
            txt_id.Text = employe.Id.ToString();
            SearchPlanning();
        }

        public void SearchPlanning_()
        {
            if (!_first)
            {
                if (employe != null ? employe.Id > 0 : false)
                {
                    Planning pd = Fonctions.GetPlanning(employe, dateDebut);
                    txt_debut.Text = pd.DateDebut.ToShortDateString() + " " + pd.HeureDebut.ToShortTimeString();
                    dateDebut = new DateTime(dateDebut.Year, dateDebut.Month, dateDebut.Day, pd.HeureDebut.Hour, pd.HeureDebut.Minute, pd.HeureDebut.Second);

                    Planning pf = Fonctions.GetPlanning(employe, dateFin);
                    txt_fin.Text = pf.DateFin.ToShortDateString() + " " + pf.HeureFin.ToShortTimeString();
                    dateFin = new DateTime(dateFin.Year, dateFin.Month, dateFin.Day, pf.HeureFin.Hour, pf.HeureFin.Minute, pf.HeureFin.Second);
                }
                else
                {
                    dateDebut = new DateTime(dateDebut.Year, dateDebut.Month, dateDebut.Day, 0, 0, 0);
                    dateFin = new DateTime(dateFin.Year, dateFin.Month, dateFin.Day, 0, 0, 0);
                }
            }
            _first = false;
        }

        public void SearchPlanning()
        {
            if (employe != null ? employe.Id > 0 : false)
            {
                Planning pd = Fonctions.GetPlanning(employe, dateDebut);
                dateDebut = new DateTime(dateDebut.Year, dateDebut.Month, dateDebut.Day, pd.HeureDebut.Hour, pd.HeureDebut.Minute, 0);

                Planning pf = Fonctions.GetPlanning(employe, dateFin);
                dateFin = new DateTime(dateFin.Year, dateFin.Month, dateFin.Day, pf.HeureFin.Hour, pf.HeureFin.Minute, 0);
            }
            txt_debut.Text = dateDebut.ToShortDateString() + " " + dateDebut.ToShortTimeString();
            txt_fin.Text = dateFin.ToShortDateString() + " " + dateFin.ToShortTimeString();
        }

        private void dtp_debut_ValueChanged(object sender, EventArgs e)
        {
            dateDebut = new DateTime(dtp_date_debut.Value.Year, dtp_date_debut.Value.Month, dtp_date_debut.Value.Day, 0, 0, 0);
            SearchPlanning();
        }

        private void dtp_fin_ValueChanged(object sender, EventArgs e)
        {
            dateFin = new DateTime(dtp_date_fin.Value.Year, dtp_date_fin.Value.Month, dtp_date_fin.Value.Day, 0, 0, 0);
            SearchPlanning();
        }

        private void dtp_heure_debut_ValueChanged(object sender, EventArgs e)
        {
            heureDebut = dtp_heure_debut.Value;
            dateDebut = new DateTime(dateDebut.Year, dateDebut.Month, dateDebut.Day, heureDebut.Hour, heureDebut.Minute, 0);
            SearchPlanning();
        }

        private void dtp_heure_fin_ValueChanged(object sender, EventArgs e)
        {
            heureFin = dtp_heure_fin.Value;
            dateFin = new DateTime(dateFin.Year, dateFin.Month, dateFin.Day, heureFin.Hour, heureFin.Minute, 0);
            SearchPlanning();
        }

        private void txt_id_Leave(object sender, EventArgs e)
        {
            employe = EmployeBLL.OneById(Convert.ToInt32(txt_id.Text));
            com_employe.SelectedText = employe.NomPrenom;
        }

        private void mc_date_exclus_MouseDown(object sender, MouseEventArgs e)
        {
            MonthCalendar.HitTestInfo info = mc_date_exclus.HitTest(e.Location);
            if (info.HitArea == MonthCalendar.HitArea.Date)
            {
                bool deja = false;
                foreach (DateTime d in mc_date_exclus.BoldedDates)
                {
                    if (d.Equals(info.Time))
                    {
                        deja = true;
                        break;
                    }
                }
                if (deja)
                {
                    mc_date_exclus.RemoveBoldedDate(info.Time);
                }
                else
                {
                    mc_date_exclus.AddBoldedDate(info.Time);
                }
                mc_date_exclus.UpdateBoldedDates();

                txt_list_date_exclus.ResetText();
                foreach (DateTime d in mc_date_exclus.BoldedDates)
                {
                    if (txt_list_date_exclus.Text.Trim().Length < 1)
                    {
                        txt_list_date_exclus.Text = d.ToShortDateString();
                    }
                    else
                    {
                        txt_list_date_exclus.Text += " - " + d.ToShortDateString();
                    }
                }
            }
        }

        private void effacerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txt_list_date_exclus.ResetText();
            mc_date_exclus.RemoveAllBoldedDates();
            mc_date_exclus.UpdateBoldedDates();
        }

        private void dgv_employe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_employe.CurrentRow.Cells["idEmp"].Value != null)
                {
                    int id = Convert.ToInt32(dgv_employe.CurrentRow.Cells["idEmp"].Value);
                    if (id > 0)
                    {
                        Employe p = employes.Find(x => x.Id == id);
                        if (p != null ? p.Id > 0 : false)
                        {
                            int pos = Utils.GetRowData(dgv_employe, id);
                            ObjectThread o = new ObjectThread(dgv_employe);
                            o.RemoveDataGridView(pos);

                            Employe p_ = employes_select.Find(x => x.Id == p.Id);
                            if (p_ != null ? p_.Id > 0 : false)
                            {
                                employes_select.RemoveAt(employes_select.FindIndex(x => x.Id == p.Id));
                                o.WriteDataGridView(pos, new object[] { p.Id, false, p.NomPrenom });
                            }
                            else
                            {
                                employes_select.Add(p);
                                o.WriteDataGridView(pos, new object[] { p.Id, true, p.NomPrenom });
                            }
                            ResetDataEmploye();
                            dgv_employe.Rows[pos].Selected = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Evenement (dgv_employe_CellContentClick) ", ex);
            }
        }

        private void chk_exclus_CheckedChanged(object sender, EventArgs e)
        {
            grp_date_exclus.Enabled = chk_exclus.Checked;
            grp_employe_exclu.Enabled = chk_exclus.Checked;

            if (chk_exclus.Checked)
            {
                bool b = chk_filter.Checked;
                if (b)
                {
                    if (DialogResult.Yes == Messages.Erreur_Oui_Non("Vous ne pouvez pas effectuer une exclusion et un filtrage. Annuler le filtrage?"))
                    {
                        chk_filter.Checked = false;
                    }
                    else
                    {
                        chk_exclus.Checked = false;
                    }
                }
            }
        }
    }
}
