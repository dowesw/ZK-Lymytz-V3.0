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
    public partial class Form_Archive_Serveur : Form
    {
        string currentFile;
        bool current = false;
        List<IOEMDevice> lIO = new List<IOEMDevice>();

        public Form_Archive_Serveur()
        {
            InitializeComponent();
            Configuration.Load(this);
        }

        private void Form_Archive_Serveur_FormClosed(object sender, FormClosedEventArgs e)
        {
            Constantes.FORM_ARCHIVE_SERVEUR = null;
            Utils.WriteLog("Fermeture page (Archive Serveur)");
            Utils.removeFrom("Form_Archive_Serveur");
        }

        private void Form_Archive_Serveur_Load(object sender, EventArgs e)
        {
            LoadFileBackup();
        }

        public void ResetDataBackup()
        {
            for (int i = 0; i < dgv_backup.Rows.Count; i++)
            {
                dgv_backup.Rows[i].Selected = false;
            }
            currentFile = null;
            current = false;
        }

        public void LoadFileBackup()
        {
            dgv_backup.Rows.Clear();
            string[] files = System.IO.Directory.GetFiles(Chemins.getCheminBackupServeur(), "*.csv");
            foreach (string f in files)
            {
                FileInfo file = new FileInfo(f);
                if (file.Name != "LogRecord.csv")
                {
                    dgv_backup.Rows.Add(new object[] {file.Name});
                }
            }
            ResetDataBackup();
        }

        public void LoadLogs(List<IOEMDevice> l)
        {
            ObjectThread o1 = new ObjectThread(dgv_log);
            o1.ClearDataGridView(true);

            if (l != null ? l.Count > 0 : false)
            {
                int i = 0;
                ObjectThread o_ = new ObjectThread(Constantes.PBAR_WAIT);
                o_.UpdateMaxBar(Constantes.PBAR_WAIT.Maximum + l.Count);

                foreach (IOEMDevice o in l)
                {
                    ++i;
                    Employe e = EmployeBLL.OneById(o.idwSEnrollNumber);
                    if (e != null ? e.Id < 1 : true)
                    {
                        e = new Employe(o.idwSEnrollNumber, o.idwSEnrollNumber.ToString(), "");
                    }
                    DateTime date = new DateTime(o.idwYear, o.idwMonth, o.idwDay, o.idwHour, o.idwMinute, o.idwSecond);
                    o1.WriteDataGridView(new object[] { i, e.Id, e.Nom + " " + e.Prenom, date.ToShortDateString(), date.ToShortTimeString() });
                    Constantes.LoadPatience(false);
                }
                Constantes.LoadPatience(true);
            }
            else
            {
                Utils.WriteLog("La liste des logs venants du fichier (" + currentFile + ") est vide");
            }
        }

        private void dgv_backup_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_backup.CurrentRow.Cells["fileName"].Value != null)
                {
                    currentFile = Convert.ToString(dgv_backup.CurrentRow.Cells["fileName"].Value);
                    if (currentFile != null)
                    {
                        current = false;
                        pbar_statut.Value = 0;
                        Constantes.PBAR_WAIT = pbar_statut;
                        Thread t = new Thread(new ThreadStart(ReadSelectFile));
                        t.Start();
                    }
                    else
                    {
                        ResetDataBackup();
                    }
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Archive_Serveur (dgv_backup_CellContentClick) ", ex);
            }
        }

        private void ReadSelectFile()
        {
            List<IOEMDevice> l = Logs.ReadCsv(Chemins.getCheminBackupServeur() + currentFile);
            ObjectThread o = new ObjectThread(Constantes.PBAR_WAIT);
            o.UpdateMaxBar(l.Count);
            LoadLogs(l);
        }

        private void btn_current_Click(object sender, EventArgs e)
        {
            lIO = new List<IOEMDevice>();
            currentFile = "LogRecord.csv";
            current = false;
            string fileName = Chemins.getCheminDatabase() + currentFile;
            if (File.Exists(fileName))
            {
                current = true;
                pbar_statut.Value = 0;
                Constantes.PBAR_WAIT = pbar_statut;
                Thread t = new Thread(new ThreadStart(LoadCurrentLog));
                t.Start();
            }
            else
            {
                Utils.WriteLog("Le fichier courant n'existe pas");
            }
        }

        private void LoadCurrentLog()
        {
            lIO = Logs.ReadCsv();
            ObjectThread o = new ObjectThread(Constantes.PBAR_WAIT);
            o.UpdateMaxBar(lIO.Count);
            LoadLogs(lIO);
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (current)
            {
                if (lIO != null ? lIO.Count > 0 : false)
                {
                    Utils.WriteLog("Demande de la sauvegarde du fichier courant");
                    if (Messages.Confirmation_Infos("sauvegarder") == System.Windows.Forms.DialogResult.Yes)
                    {
                        pbar_statut.Value = 0;
                        Constantes.PBAR_WAIT = pbar_statut;
                        Thread t = new Thread(new ThreadStart(SaveCurrent));
                        t.Start();
                    }
                    else
                    {
                        Utils.WriteLog("-- Sauvegarde du fichier courant annulée");
                    }
                }
                else
                {
                    Utils.WriteLog("Le fichier courant est vide");
                }
            }
            else
            {
                Utils.WriteLog("Vous devez d'abord charger le fichier courant");
            }
        }

        private void SaveCurrent()
        {
            string fileName = Chemins.getCheminBackupServeur() + DateTime.Now.ToString("dd-MM-yyyy") + ".csv";
            bool deja = File.Exists(fileName);
            ObjectThread o_ = new ObjectThread(Constantes.PBAR_WAIT);
            o_.UpdateMaxBar(lIO.Count);

            foreach (IOEMDevice o in lIO)
            {
                Logs.WriteCsv(fileName, o);
                Constantes.LoadPatience(false);
            }
            if (!deja)
            {
                FileInfo file = new FileInfo(fileName);
                ObjectThread o1 = new ObjectThread(dgv_backup);
                o1.WriteDataGridView(new object[] { file.Name });
            }
            File.Delete(Chemins.getCheminDatabase() + "LogRecord.csv");
            File.Create(Chemins.getCheminDatabase() + "LogRecord.csv");

            ObjectThread o2 = new ObjectThread(dgv_log);
            o2.ClearDataGridView(true);
            Utils.WriteLog("-- Sauvegarde du fichier courant effectuée");
            Constantes.LoadPatience(true);
        }
    }
}
