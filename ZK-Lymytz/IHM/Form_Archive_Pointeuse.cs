using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.IO;
using System.Windows.Forms;

using ZK_Lymytz.TOOLS;
using ZK_Lymytz.BLL;
using ZK_Lymytz.ENTITE;

namespace ZK_Lymytz.IHM
{
    public partial class Form_Archive_Pointeuse : Form
    {
        Pointeuse currentPointeuse = new Pointeuse();
        List<IOEMDevice> lIO = new List<IOEMDevice>(),lIO_ = new List<IOEMDevice>();
        string currentFile;
        bool bFile;

        public Form_Archive_Pointeuse()
        {
            InitializeComponent();
            Configuration.Load(this);
        }

        private void Form_Archive_Pointeuse_FormClosed(object sender, FormClosedEventArgs e)
        {
            Constantes.FORM_ARCHIVE_POINTEUSE = null;
            Utils.WriteLog("Fermeture page (Archive Appareil)");
            Utils.removeFrom("Form_Archive_Pointeuse");
        }

        private void Form_Archive_Pointeuse_Load(object sender, EventArgs e)
        {
            LoadPointeuse();
        }

        public void Reset()
        {
            ResetDataPointeuse();
            ResetDataBackup();
            btn_del_doublon.Enabled = false;
        }

        public void ResetDataPointeuse()
        {
            btn_load_by_appareil.Text = "Charger Log (Pointeuse : 192.168.1.201)";
            for (int i = 0; i < dgv_pointeuse.Rows.Count; i++)
            {
                dgv_pointeuse.Rows[i].Selected = false;
            }
            currentPointeuse = null;
            btn_del_doublon.Enabled = false;
        }

        public void ResetDataBackup()
        {
            btn_load_by_file.Text = "Charger Log (Fichier : 00-00-0000.csv)";
            for (int i = 0; i < dgv_backup.Rows.Count; i++)
            {
                dgv_backup.Rows[i].Selected = false;
            }
            currentFile = null;
            btn_del_doublon.Enabled = false;
        }

        public void LoadPointeuse()
        {
            if (Constantes.POINTEUSES.Count < 1)
            {
                Societe s = SocieteBLL.ReturnSociete();
                Constantes.POINTEUSES = PointeuseBLL.List("select * from yvs_pointeuse where societe = " + s.Id + " order by adresse_ip");
            }
            dgv_pointeuse.Rows.Clear();
            ObjectThread o = new ObjectThread(dgv_pointeuse);
            foreach (Pointeuse p in Constantes.POINTEUSES)
            {
                o.WriteDataGridView(new object[] { p.Id, p.Ip, p.Port, p.Emplacement, p.IMachine, p.Connecter, p.Actif });
            }
            ResetDataPointeuse();
        }

        public void LoadLogs(List<IOEMDevice> l, bool file)
        {
            bFile = file;
            ObjectThread o_ = new ObjectThread(btn_del_doublon);
            o_.EnableButton(file);

            o_ = new ObjectThread(dgv_log);
            o_.ClearDataGridView(true);
            if (l != null ? l.Count > 0 : false)
            {
                int i = 0;
                ObjectThread o1 = new ObjectThread(Constantes.PBAR_WAIT);
                o1.UpdateMaxBar(Constantes.PBAR_WAIT.Maximum + l.Count);
                foreach (IOEMDevice o in l)
                {
                    ++i;
                    Employe e = EmployeBLL.OneById(o.idwSEnrollNumber);
                    if (e != null ? e.Id < 1 : true)
                    {
                        e = new Employe(o.idwSEnrollNumber, o.idwSEnrollNumber.ToString(), "");
                    }
                    DateTime date = new DateTime(o.idwYear, o.idwMonth, o.idwDay, o.idwHour, o.idwMinute, o.idwSecond);

                    o_.WriteDataGridView(new object[] { i, e.Id, e.Nom + " " + e.Prenom, date.ToShortDateString(), date.ToLongTimeString() });
                    Constantes.LoadPatience(false);
                }
                Constantes.LoadPatience(true);
            }
            else
            {
                Utils.WriteLog("La liste des logs venants " + (file ? "du fichier (" + currentFile : "de la pointeuse (" + currentPointeuse.Ip) + ") est vide");
            }
        }

        public void LoadFileBackup()
        {
            if (currentPointeuse != null ? currentPointeuse.Id > 0 : false)
            {
                dgv_backup.Rows.Clear();
                int i = 0;
                string[] files = System.IO.Directory.GetFiles(Chemins.getCheminBackup(currentPointeuse.Ip), "*.csv");
                foreach (string f in files)
                {
                    ++i;
                    FileInfo file = new FileInfo(f);
                    dgv_backup.Rows.Add(new object[] { i, file.Name, file.CreationTime, file.FullName });
                }
            }
            ResetDataBackup();
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
                        btn_load_by_appareil.Text = "Charger Log (Pointeuse : " + currentPointeuse.Ip + ")";
                        LoadFileBackup();
                        dgv_log.Rows.Clear();
                    }
                    else
                    {
                        ResetDataPointeuse();
                    }
                    btn_del_doublon.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Archive_Pointeuse (dgv_pointeuse_CellContentClick) ", ex);
            }
        }

        private void btn_load_by_appareil_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentPointeuse != null ? currentPointeuse.Id > 0 : false)
                {
                    pbar_statut.Value = 0;
                    Constantes.PBAR_WAIT = pbar_statut;
                    Thread t = new Thread(new ThreadStart(LoadDataBtn));
                    t.Start();
                }
                else
                {
                    Utils.WriteLog("Vous devez selectionner un appareil ");
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Archive_Pointeuse (btn_load_by_appareil_Click) ", ex);
            }
        }

        public void LoadDataBtn()
        {
            Appareil z = Utils.ReturnAppareil(currentPointeuse);
            Utils.VerifyZkemkeeper(ref z, ref currentPointeuse);
            if (z == null)
            {
                Utils.WriteLog("La liaison avec l'appareil " + currentPointeuse.Ip + " est corrompue");
                return;
            }
            currentPointeuse.Zkemkeeper = z;
            lIO = z.GetAllAttentdData(currentPointeuse.IMachine, currentPointeuse.Connecter);
            ObjectThread o = new ObjectThread(Constantes.PBAR_WAIT);
            o.UpdateMaxBar(lIO.Count);
            LoadLogs(lIO, false);
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Utils.WriteLog("Demande de la sauvegarde des logs");
            if (Messages.Confirmation_Infos("sauvegarder") == System.Windows.Forms.DialogResult.Yes)
            {
                if (lIO.Count > 0)
                {
                    pbar_statut.Value = 0;
                    Constantes.PBAR_WAIT = pbar_statut;
                    Thread t = new Thread(new ThreadStart(BackupLog));
                    t.Start();
                }
                else
                {
                    Utils.WriteLog("Sauvegarde impossible car le log est vide");
                }
            }
            else
            {
                Utils.WriteLog("-- Sauvegarde des logs annulée");
            }
        }

        public void BackupLog()
        {
            ObjectThread o = new ObjectThread(Constantes.PBAR_WAIT);
            o.UpdateMaxBar(0);
            Fonctions.BackupLogData(lIO, currentPointeuse.Ip, true, null);
        }

        private void dgv_backup_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_backup.CurrentRow.Cells["cpt"].Value != null)
                {
                    currentFile = Convert.ToString(dgv_backup.CurrentRow.Cells["fileName"].Value);
                    if (currentFile != null)
                    {
                        btn_load_by_file.Text = "Charger Log (Fichier : " + currentFile + ")";
                    }
                    else
                    {
                        ResetDataBackup();
                    }
                    btn_del_doublon.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Archive_Pointeuse (dgv_backup_CellContentClick) ", ex);
            }
        }

        private void btn_load_by_file_Click(object sender, EventArgs e)
        {
            if (currentFile != null)
            {
                pbar_statut.Value = 0;
                Constantes.PBAR_WAIT = pbar_statut;
                Thread t = new Thread(new ThreadStart(LoadDataFile));
                t.Start();
            }
            else
            {
                Utils.WriteLog("Vous devez selectionner un fichier ");
            }
        }

        public void LoadDataFile()
        {
            lIO_ = Logs.ReadCsv(Chemins.getCheminBackup(currentPointeuse.Ip) + currentFile);
            ObjectThread o = new ObjectThread(Constantes.PBAR_WAIT);
            o.UpdateMaxBar(lIO.Count);
            LoadLogs(lIO_, true);
        }

        private void btn_del_doublon_Click(object sender, EventArgs e)
        {
            if (currentFile != null && bFile)
            {
                if (lIO_ != null ? lIO_.Count > 0 : false)
                {
                    Utils.WriteLog("Demande de suppression des doublons du fichier " + currentFile + " de l'appareil " + currentPointeuse.Ip);
                    if (Messages.Confirmation("Cette action peut prendre plusieurs minutes...Continuer") == System.Windows.Forms.DialogResult.Yes)
                    {
                        dgv_log.Rows.Clear();
                        pbar_statut.Value = 0;
                        Constantes.PBAR_WAIT = pbar_statut;
                        Utils.WriteLog("-- Début de la suppression des doublons du fichier " + currentFile + " de l'appareil " + currentPointeuse.Ip + "... Patientez Svp");
                        Thread t = new Thread(new ThreadStart(DeleteDoublon));
                        t.Start();
                    }
                    else
                    {
                        Utils.WriteLog("-- Suppression des doublons du fichier " + currentFile + " de l'appareil " + currentPointeuse.Ip+" annulée");
                    }
                }
                else
                {
                    Utils.WriteLog("Suppression des doublons impossible car le log est vide");
                }
            }
            else
            {
                Utils.WriteLog("Vous devez selectionner un fichier ");
            }
        }

        public void DeleteDoublon()
        {
            List<IOEMDevice> l = new List<IOEMDevice>();
            string fileName = Chemins.getCheminBackup(currentPointeuse.Ip) + currentFile;
            int i = 0;
            ObjectThread o = new ObjectThread(Constantes.PBAR_WAIT);
            o.UpdateMaxBar(lIO_.Count);
            foreach (IOEMDevice p in lIO_)
            {
                bool deja = false;
                foreach (IOEMDevice s in l)
                {
                    if (s.idwSEnrollNumber == p.idwSEnrollNumber && s.idwYear == p.idwYear && s.idwMonth == p.idwMonth && s.idwDay == p.idwDay && s.idwHour == p.idwHour && s.idwMinute == p.idwMinute && s.idwSecond == p.idwSecond)
                    {
                        ++i;
                        deja = true;
                        break;
                    }
                }
                if (!deja)
                {
                    l.Add(p);
                }
                Constantes.LoadPatience(false);
            }
            File.Delete(fileName);
            o.UpdateMaxBar(Constantes.PBAR_WAIT.Maximum + l.Count);
            foreach (IOEMDevice s in l)
            {
                Logs.WriteCsv(fileName, s);
                Constantes.LoadPatience(false);
            }
            Constantes.LoadPatience(true);
            Utils.WriteLog("---- Nombre Ligne Total : " + lIO_.Count);
            Utils.WriteLog("---- Nombre Ligne Supprimée : " + i);
            Utils.WriteLog("---- Nombre Ligne Restante : " + l.Count);
            LoadDataFile();
            Utils.WriteLog("-- Fin de la suppression des doublons du fichier " + currentFile + " de l'appareil " + currentPointeuse.Ip + "...");
        }

        private void grp_action_Enter(object sender, EventArgs e)
        {

        }
    }
}
