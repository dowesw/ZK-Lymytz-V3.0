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
        List<Pointeuse> pointeuses;
        List<Pointeuse> pointeuses_select = new List<Pointeuse>();
        Pointeuse currentPointeuse;
        public List<IOEMDevice> logs = new List<IOEMDevice>();
        DateTime dateDebut = new DateTime(), dateFin = new DateTime(), heureDebut = new DateTime(), heureFin = new DateTime();
        bool _first = true;
        bool _clean = false;

        public IOEMDevice currentPointage = new IOEMDevice();

        public ObjectThread object_log;
        ObjectThread object_pointeuse;

        public Form_Evenement()
        {
            InitializeComponent();
            Configuration.Load(this);
            object_log = new ObjectThread(dgv_log);
            object_pointeuse = new ObjectThread(dgv_pointeuse);
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

        public void ResetDataPointeuse(bool clean, int pos)
        {
            for (int i = 0; i < dgv_pointeuse.Rows.Count; i++)
            {
                if (pos < 0)
                {
                    dgv_pointeuse.Rows[i].Selected = false;
                }
                else
                {
                    if (pos != i)
                    {
                        dgv_pointeuse.Rows[i].Selected = false;
                    }
                }
            }
            if (clean)
            {
                currentPointeuse = null;
                pointeuses_select.Clear();
            }
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
            chk_invalid_only.Checked = true;
            dtp_date_debut.ResetText();
            dtp_date_fin.ResetText();
            txt_debut_plan.Text = dateDebut.ToShortDateString() + " " + dateDebut.ToShortTimeString();
            txt_fin_plan.Text = dateFin.ToShortDateString() + " " + dateFin.ToShortTimeString();
        }

        public void LoadEmploye()
        {
            try
            {
                employes.Clear();
                dgv_employe.Rows.Clear();

                employes = EmployeBLL.List(Constantes.QUERY_EMPLOYE(Constantes.SOCIETE));
                com_employe.DisplayMember = "NomPrenom";
                com_employe.ValueMember = "Id";
                com_employe.DataSource = new BindingSource(employes, null);

                for (int i = 0; i < employes.Count; i++)
                {
                    Employe e = employes[i];
                    String nom = e.NomPrenom;
                    if (com_employe.AutoCompleteCustomSource.Contains(nom))
                        nom += "°";
                    com_employe.AutoCompleteCustomSource.Add(nom);
                    dgv_employe.Rows.Add(new object[] { e.Id, false, nom });
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
            pointeuses = new List<Pointeuse>(Constantes.POINTEUSES);
            dgv_pointeuse.Rows.Clear();
            foreach (Pointeuse p in pointeuses)
            {
                //o.WriteDataGridView(new object[] { p.Id, p.Ip, p.Port, p.Emplacement, p.IMachine, p.Connecter, p.Actif, true });
                object_pointeuse.WriteDataGridView(new object[] { p.Id, p.Ip, false });
            }
            ResetDataPointeuse(true, -1);
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
            object_log.ClearDataGridView(true);
            if (l != null ? l.Count > 0 : false)
            {
                ObjectThread o1 = new ObjectThread(Constantes.PBAR_WAIT);
                o1.UpdateMaxBar(Constantes.PBAR_WAIT.Maximum + l.Count);
                int i = 0;
                foreach (IOEMDevice o in l)
                {
                    AddRow(i, o);
                    ++i;
                    Constantes.LoadPatience(false);
                }
                Constantes.LoadPatience(true);
            }
            else
            {
                Utils.WriteLog("La liste des logs venants de(s) pointeuse(s) est vide");
                Constantes.LoadPatience(true, true);
            }
        }

        public void AddRow(int pos, IOEMDevice o)
        {
            Employe e = EmployeBLL.OneById(o.idwSEnrollNumber);
            if (e != null ? e.Id < 1 : true)
            {
                e = new Employe(o.idwSEnrollNumber, o.idwSEnrollNumber.ToString(), "");
            }
            DateTime date = new DateTime(o.idwYear, o.idwMonth, o.idwDay, o.idwHour, o.idwMinute, o.idwSecond);
            o.id = pos + 1;
            object_log.WriteDataGridView(pos, new object[] { pos + 1, o.Icon(), e.Id, e.Nom + " " + e.Prenom, date.ToShortDateString(), date.ToShortTimeString(), !o.exclure });
        }

        public void LoadData_()
        {
            logs.Clear();
            List<IOEMDevice> lIO_ = new List<IOEMDevice>();
            foreach (Pointeuse p in pointeuses_select)
            {
                lIO_.AddRange(Logs.ReadCsv(Chemins.CheminBackup(p.Ip) + "16-01-2017.csv"));
            }
            lIO_.Sort();
            if (lIO_ != null)
            {
                foreach (IOEMDevice i in lIO_)
                {
                    if (chk_filter.Checked)
                    {
                        DateTime h = new DateTime(i.idwYear, i.idwMonth, i.idwDay, 0, 0, 0);
                        DateTime d = dateDebut.AddMinutes(-Convert.ToDouble(txt_marge_heure_debut.Value));
                        DateTime f = dateFin.AddMinutes(Convert.ToDouble(txt_marge_heure_fin.Value));
                        if (chk_employe.Checked)
                        {
                            if (employe != null ? employe.Id > 0 : false)
                            {
                                if (chk_date.Checked)
                                {
                                    if (i.idwSEnrollNumber == employe.Id && (d <= h && h <= f))
                                    {
                                        logs.Add(i);
                                    }
                                }
                                else
                                {
                                    if (i.idwSEnrollNumber == employe.Id)
                                    {
                                        logs.Add(i);
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
                                    logs.Add(i);
                                }
                            }
                            else
                            {
                                logs.Add(i);
                            }
                        }
                    }
                    else
                    {
                        logs.Add(i);
                    }
                    Constantes.LoadPatience(false);
                }
            }
            ObjectThread o = new ObjectThread(Constantes.PBAR_WAIT);
            o.UpdateMaxBar(logs.Count);
            LoadLogs(logs);
        }

        public Appareil VerifyConnexion(Pointeuse p)
        {
            Appareil z = null;
            if (p.Connecter)
            {
                z = p.Zkemkeeper;
            }
            if (z == null)
            {
                bool update = Utils.SetZkemkeeper(ref p);
                z = p.Zkemkeeper;
                if (z == null)
                {
                    Utils.WriteLog("La liaison avec l'appareil " + p.Ip + " est corrompue");
                    return null;
                }
                if (update)
                {
                    int idx = pointeuses.FindIndex(x => x.Id == p.Id);
                    if (idx > -1)
                    {
                        pointeuses[idx] = p;
                    }
                }
            }
            return z;
        }

        public void LoadData()
        {
            logs.Clear();
            DateTime d = dateDebut.AddMinutes(-Convert.ToDouble(txt_marge_heure_debut.Value));
            DateTime f = dateFin.AddMinutes(Convert.ToDouble(txt_marge_heure_fin.Value));

            foreach (Pointeuse p in pointeuses_select)
            {
                if (p.Tampon)
                {
                    Utils.WriteLog("Début du chargement de la sauvegarde des logs de l'appareil " + p.Ip + "");
                    if (p.Logs.Count < 1)
                    {
                        p.Logs = Logs.ReadCsv(Chemins.CheminTemp() + p.Ip + ".csv");
                        if (p.Logs.Count < 1)
                            Fonctions.LoadFileTamponPointeuse(p, 1, false);
                    }
                    if (chk_filter.Checked)
                        logs.AddRange(Utils.FindLogsInFileTamponLogs(p.Logs, employe, chk_date.Checked, d, f));
                    else
                    {
                        if (chk_exclus.Checked)
                            logs.AddRange(Utils.FindLogsInFileTamponLogsEx(p.Logs, employes_select, mc_date_exclus.BoldedDates));
                        else
                            logs.AddRange(Utils.FindLogsInFileTamponLogs(p.Logs, null, false, d, f));
                    }
                }
                else
                {
                    Appareil z = VerifyConnexion(p);
                    if (z != null)
                    {
                        z._POINTEUSE = p;
                        Utils.WriteLog("Début du chargement de la liste des logs de l'appareil " + p.Ip + "");
                        if (chk_filter.Checked)
                            logs.AddRange(z.GetAllAttentdData(p.IMachine, p.Connecter, employe, chk_date.Checked, d, f));
                        else
                        {
                            if (chk_exclus.Checked)
                                logs.AddRange(z.GetAllAttentdDataEx(p.IMachine, p.Connecter, employes_select, mc_date_exclus.BoldedDates));
                            else
                                logs.AddRange(z.GetAllAttentdData(p.IMachine, p.Connecter));
                        }
                    }
                }
            }
            logs.Sort();
            ObjectThread o = new ObjectThread(Constantes.PBAR_WAIT);
            o.UpdateMaxBar(logs.Count);
            LoadLogs(logs);
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
                        currentPointeuse = pointeuses.Find(x => x.Id == id);
                        if (currentPointeuse != null ? currentPointeuse.Id < 1 : true)
                            currentPointeuse = PointeuseBLL.OneById(id);
                        if (pointeuses_select.Contains(currentPointeuse))
                            pointeuses_select.Remove(currentPointeuse);
                        else
                            pointeuses_select.Add(currentPointeuse);
                        dgv_log.Rows.Clear();

                        if (e.ColumnIndex == dgv_pointeuse.CurrentRow.Cells["temp"].ColumnIndex)
                        {
                            int pos = Utils.GetRowData(dgv_pointeuse, id);
                            bool tampon = Convert.ToBoolean(dgv_pointeuse.CurrentRow.Cells["temp"].Value);
                            tampon = !tampon;
                            if (tampon && !pointeuses_select.Contains(currentPointeuse))
                                pointeuses_select.Add(currentPointeuse);

                            ObjectThread o = new ObjectThread(dgv_pointeuse);
                            o.RemoveDataGridView(pos);
                            o.WriteDataGridView(pos, new object[] { currentPointeuse.Id, currentPointeuse.Ip, tampon });

                            currentPointeuse.Tampon = tampon;
                            ResetDataPointeuse(false, pos);
                            dgv_pointeuse.Rows[pos].Selected = true;
                        }
                    }
                    else
                    {
                        ResetDataPointeuse(true, -1);
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
            if (pointeuses_select != null ? pointeuses_select.Count > 0 : false)
            {
                _clean = false;
                pbar_statut.Value = 0;
                Constantes.PBAR_WAIT = pbar_statut;
                //Thread t = new Thread(new ThreadStart(LoadData_));
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
                if (!_clean)
                {
                    if (logs.Count > 0)
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
                    Utils.WriteLog("-- Synchronisation impossible.. Vous devez recharger la liste des logs");
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
                Fonctions.SynchroniseServer(logs, currentPointeuse.Ip, false, employe, chk_date.Checked, dateDebut, dateFin, chk_invalid_only.Checked);
            }
            else
            {
                Fonctions.SynchroniseServer(logs, currentPointeuse.Ip, false, false);
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
                    txt_debut_plan.Text = pd.DateDebut.ToShortDateString() + " " + pd.HeureDebut.ToShortTimeString();
                    dateDebut = new DateTime(dateDebut.Year, dateDebut.Month, dateDebut.Day, pd.HeureDebut.Hour, pd.HeureDebut.Minute, pd.HeureDebut.Second);

                    Planning pf = Fonctions.GetPlanning(employe, dateFin);
                    txt_fin_plan.Text = pf.DateFin.ToShortDateString() + " " + pf.HeureFin.ToShortTimeString();
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
            chk_heure_debut.Checked = true;
            chk_heure_fin.Checked = true;

            SearchPlanning(true, true, true);

            txt_debut_plan.Text = dateDebut.ToShortDateString() + " " + dateDebut.ToShortTimeString();
            txt_fin_plan.Text = dateFin.ToShortDateString() + " " + dateFin.ToShortTimeString();
            txt_debut.Text = dateDebut.ToShortDateString() + " " + dateDebut.ToShortTimeString();
            txt_fin.Text = dateFin.ToShortDateString() + " " + dateFin.ToShortTimeString();
        }

        public void SearchPlanning(bool with_time, bool debut, bool fin)
        {
            if (employe != null ? employe.Id > 0 : false)
            {
                if (debut)
                {
                    Planning pd = Fonctions.GetPlanning(employe, new DateTime(dateDebut.Year, dateDebut.Month, dateDebut.Day, 0, 0, 0));
                    if (with_time)
                        dateDebut = new DateTime(dateDebut.Year, dateDebut.Month, dateDebut.Day, pd.HeureDebut.Hour, pd.HeureDebut.Minute, 0);
                    else
                        dateDebut = new DateTime(dateDebut.Year, dateDebut.Month, dateDebut.Day, 0, 0, 0);
                }

                if (fin)
                {
                    Planning pf = Fonctions.GetPlanning(employe, new DateTime(dateFin.Year, dateFin.Month, dateFin.Day, 0, 0, 0));
                    if (with_time)
                        dateFin = new DateTime(dateFin.Year, dateFin.Month, dateFin.Day, pf.HeureFin.Hour, pf.HeureFin.Minute, 0);
                    else
                        dateFin = new DateTime(dateFin.Year, dateFin.Month, dateFin.Day, 0, 0, 0);
                }
            }
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
            DateTime date = dateDebut;
            if (chk_heure_debut.Checked)
            {
                dateDebut = new DateTime(dateDebut.Year, dateDebut.Month, dateDebut.Day, heureDebut.Hour, heureDebut.Minute, 0);
                date = dateDebut;
                date = date.AddMinutes(-(Convert.ToInt32(txt_marge_heure_debut.Value)));
            }
            txt_debut.Text = date.ToShortDateString() + " " + date.ToShortTimeString();
        }

        private void dtp_heure_fin_ValueChanged(object sender, EventArgs e)
        {
            heureFin = dtp_heure_fin.Value;
            DateTime date = dateFin;
            if (chk_heure_fin.Checked)
            {
                dateFin = new DateTime(dateFin.Year, dateFin.Month, dateFin.Day, heureFin.Hour, heureFin.Minute, 0);
                date = dateFin;
                date = date.AddMinutes((Convert.ToInt32(txt_marge_heure_fin.Value)));
            }
            txt_fin.Text = date.ToShortDateString() + " " + date.ToShortTimeString();
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

        private void chk_heure_debut_CheckedChanged(object sender, EventArgs e)
        {
            SearchPlanning(chk_heure_debut.Checked, true, false);
            DateTime date = dateDebut;
            if (chk_heure_debut.Checked)
            {
                string t = heureDebut.ToShortTimeString();
                if (!(t.Equals("00:00:00:000") || t.Equals("00:00:00") || t.Equals("00:00") || t.Equals("00")))
                {
                    dateDebut = new DateTime(dateDebut.Year, dateDebut.Month, dateDebut.Day, heureDebut.Hour, heureDebut.Minute, 0);
                    date = dateDebut;
                }
                date = date.AddMinutes(-(Convert.ToInt32(txt_marge_heure_debut.Value)));
            }
            txt_debut.Text = date.ToShortDateString() + " " + date.ToShortTimeString();
        }

        private void chk_heure_fin_CheckedChanged(object sender, EventArgs e)
        {
            SearchPlanning(chk_heure_fin.Checked, false, true);
            DateTime date = dateFin;
            if (chk_heure_fin.Checked)
            {
                string t = heureFin.ToShortTimeString();
                if (!(t.Equals("00:00:00:000") || t.Equals("00:00:00") || t.Equals("00:00") || t.Equals("00")))
                {
                    dateFin = new DateTime(dateFin.Year, dateFin.Month, dateFin.Day, heureFin.Hour, heureFin.Minute, 0);
                    date = dateFin;
                }
                date = date.AddMinutes((Convert.ToInt32(txt_marge_heure_fin.Value)));
            }
            txt_fin.Text = date.ToShortDateString() + " " + date.ToShortTimeString();
        }

        private void txt_marge_heure_debut_ValueChanged(object sender, EventArgs e)
        {
            DateTime date = dateDebut.AddMinutes(-(Convert.ToInt32(txt_marge_heure_debut.Value)));
            txt_debut.Text = date.ToShortDateString() + " " + date.ToShortTimeString();
        }

        private void txt_marge_heure_fin_ValueChanged(object sender, EventArgs e)
        {
            DateTime date = dateFin.AddMinutes((Convert.ToInt32(txt_marge_heure_fin.Value)));
            txt_fin.Text = date.ToShortDateString() + " " + date.ToShortTimeString();
        }

        private void effacerLesLignesInseréesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (logs != null ? logs.Count > 0 : false)
            {
                List<IOEMDevice> l = new List<IOEMDevice>(logs);
                logs.Clear();
                foreach (IOEMDevice o in l)
                {
                    if (!o.iCorrect)
                    {
                        logs.Add(o);
                    }
                }
                pbar_statut.Value = 0;
                Constantes.PBAR_WAIT = pbar_statut;
                LoadLogs(logs);
                _clean = l.Count != logs.Count;
            }
        }

        private void dgv_log_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dgv_log.CurrentRow.Cells["exclus"].ColumnIndex)
                {
                    if (dgv_log.CurrentRow.Cells["num"].Value != null)
                    {
                        int pos = Convert.ToInt32(dgv_log.CurrentRow.Cells["pos"].Value);
                        if (pos > 0)
                        {

                            pos -= 1;
                            bool exclus = logs[pos].exclure;
                            logs[pos].exclure = !exclus;
                            IOEMDevice p = logs[pos];
                            Constantes.FORM_EVENEMENT.object_log.RemoveDataGridView(pos);
                            Constantes.FORM_EVENEMENT.AddRow(pos, p);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Evenement (dgv_log_CellContentClick) ", ex);
            }
        }

        private void dgv_pointeuse_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.ColumnIndex == this.dgv_pointeuse.Columns["temp"].Index) && e.Value != null)
            {
                DataGridViewCell cell = this.dgv_pointeuse.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.ToolTipText = "Utiliser une liste tampon?";
            }
        }

        private void dgv_log_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo info = dgv_log.HitTest(e.X, e.Y); //get info
            int pos = dgv_log.HitTest(e.X, e.Y).RowIndex;
            if (pos > -1)
            {
                switch (e.Button)
                {
                    case MouseButtons.Right:
                        {
                            currentPointage = null;
                            for (int i = 0; i < dgv_log.Rows.Count; i++)
                            {
                                dgv_log.Rows[i].Selected = false;
                            }

                            dgv_log.Rows[pos].Selected = true; //Select the row
                            if (dgv_log.Rows[pos].Cells["pos"] != null ? dgv_log.Rows[pos].Cells["pos"].Value != null : false)
                            {
                                Int32 position = (Int32)dgv_log.Rows[pos].Cells["pos"].Value;
                                if (position > 0)
                                {
                                    currentPointage = logs[position - 1];
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void insererDansUneFicheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Dial_Insert_Pointage(this).ShowDialog();
        }

        public void InsertPointageInFiche(Employe e, DateTime current, DateTime dateDebut, DateTime dateFin, bool decal, DateTime heureDecalage, string mouv, Pointeuse pp)
        {
            try
            {
                if (e != null ? e.Id > 0 : false)
                {
                    Presence p = PresenceBLL.OneByDate(e, dateDebut, dateFin, dateFin);
                    if (p != null ? p.Id > 0 : false)
                    {
                        if (!p.Valider)
                        {
                            DateTime h = new DateTime(current.Year, current.Month, current.Day, current.Hour, current.Minute, 0);
                            string req = "select p.* from yvs_grh_pointage p inner join yvs_grh_presence r on p.presence = r.id where r.employe = " + e.Id + " and ((heure_entree is not null and heure_entree = '" + h + "') or (heure_sortie is not null and heure_sortie = '" + h + "'))";
                            List<Pointage> lp = PointageBLL.List(req);
                            if (lp != null ? lp.Count < 1 : true)
                            {
                                p.HeureDebut = Utils.TimeStamp(p.DateDebut, p.HeureDebut);
                                p.HeureFin = Utils.TimeStamp(p.DateFin, p.HeureFin);
                                if (!decal)
                                {
                                    AddPointage(p, lp, current, mouv, pp);
                                }
                                else
                                {
                                    string query = "select heure_entree from yvs_grh_pointage where presence = " + p.Id + " and (heure_entree is not null and heure_entree >= '" + heureDecalage + "') and system_in is false order by heure_entree";
                                    List<string> list = Connexion.LoadListObject(query);
                                    query = "select heure_sortie from yvs_grh_pointage where presence = " + p.Id + " and (heure_sortie is not null and heure_sortie >= '" + heureDecalage + "') and system_out is false order by heure_sortie";
                                    list.InsertRange(list.Count, Connexion.LoadListObject(query));

                                    List<DateTime> dates = list.ConvertAll(Utils.StringToDate);
                                    dates.Sort();

                                    Connexion.RequeteLibre("delete from yvs_grh_pointage where presence = " + p.Id + " and '" + heureDecalage + "' <= heure_entree and heure_entree is not null");
                                    Connexion.RequeteLibre("update yvs_grh_pointage set heure_sortie = null where presence = " + p.Id + " and '" + heureDecalage + "' <= heure_sortie and heure_sortie is not null");

                                    AddPointage(p, lp, current, mouv, pp);
                                    foreach (DateTime heure in dates)
                                    {
                                        Fonctions.OnSavePointage((Presence)p, heure, currentPointeuse);
                                    }
                                }
                            }
                        }
                        else
                            Utils.WriteLog("Insertion impossible car cette fiche est déjà validée !");
                    }
                    else
                        Utils.WriteLog("Insertion impossible car l'employé n'a pas de fiche à cette date !");
                }
                else
                    Utils.WriteLog("Employé Inconnu !");
            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Evenement (InsertPointageInFiche)", ex);
            }
        }

        private void AddPointage(Presence p, List<Pointage> lp, DateTime current, string mouv, Pointeuse pointeuse)
        {
            bool correct = false;
            switch (mouv)
            {
                case "S":
                    //Recherche le dernier pointage
                    lp = PointageBLL.List("select * from yvs_grh_pointage where presence = " + p.Id + " and heure_entree is not null order by heure_entree desc");
                    if (lp != null ? lp.Count < 1 : true)//S'il n'y'a pas de pointage
                    {
                        //On insere une sortie sans entree
                        correct = Fonctions.OnSavePointage("S", null, (Presence)p, current, pointeuse);
                    }
                    else
                    {
                        //S'il existe on le recupère
                        Pointage po = lp[0];
                        //On verifi si le dernier pointage est une entrée
                        if ((po.HeureSortie != null) ? po.HeureSortie.ToString() == "01/01/0001 00:00:00" : true)//Si le dernier pointage etait une entrée
                        {
                            //On insere une sortie avec entree
                            correct = Fonctions.OnSavePointage("S", po, (Presence)p, current, pointeuse);
                        }
                        else//Si le dernier pointage etait une sortie
                        {
                            //On insere une sortie sans entree
                            correct = Fonctions.OnSavePointage("S", null, (Presence)p, current, pointeuse);
                        }
                    }
                    break;
                case "E":
                    correct = Fonctions.OnSavePointage("E", null, (Presence)p, current, pointeuse);
                    break;
                default:
                    correct = Fonctions.OnSavePointage((Presence)p, current, pointeuse);
                    break;
            }
            if (correct)
            {
                currentPointage.iCorrect = true;
                int pos = Utils.GetRowData(dgv_log, currentPointage.id);
                if (pos > -1)
                {
                    object_log.RemoveDataGridView(pos);
                    AddRow(pos, currentPointage);
                }
            }
        }
    }
}
