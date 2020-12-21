using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.IO;

using ZK_Lymytz.BLL;
using ZK_Lymytz.IHM;
using ZK_Lymytz.TOOLS;
using ZK_Lymytz.ENTITE;
using System.Net;
using System.Text;

namespace ZK_Lymytz
{
    public static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Main_(true);
            //Test();
        }

        public static void Main_(bool all)//boolean au cas ou on utilise les services pour démarrer l'application
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Configuration.Return();
                if (all)
                    StartProgram(true);
            }
            catch (Exception ex)
            {
                Messages.Exception(ex);
            }
        }

        static void Test()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Configuration.Return();
            Utils.Load();

            Form_Parent f = new Form_Parent();
            Constantes.FORM_PARENT = f;
            Constantes.ACTIVE = true;
            f.Hide();
            Application.Run(new Form_Setting());
        }

        static void insertAuto(DateTime debut, DateTime fin)
        {
            while (debut < fin)
            {
                Logs.WriteTxt(Chemins.CheminPing() + "192.168.30.230.txt", debut.ToString());
                debut = debut.AddSeconds(60);
            }
        }

        public static void StartProgram(bool registry)
        {
            try
            {
                Configuration.Return();
                Start(true);
            }
            catch (Exception ex)
            {
                Messages.Exception(ex);
            }
        }

        static void Start(bool registry)
        {
            try
            {
                Utils.WriteStatut(0);
                Utils.Load();

                Utils.WriteStatut(1);
                if (!Appareil.Verify() || !Utils.ExecuteScript())
                    Utils.InstallSDK(!Utils.ExecuteScript());

                Utils.WriteStatut(2);
                if (new TOOLS.Connexion().isConnection(ServeurBLL.ReturnServeur()) ? new TOOLS.Connexion().Connection() == null : true)
                {
                    CloseStart();
                    new IHM.Form_Serveur().ShowDialog();
                    return;
                }
                if (Constantes.USERS != null ? Constantes.USERS.Id < 1 : true)
                {
                    CloseStart();
                    new IHM.Form_Users().ShowDialog();
                    return;
                }
                if (Constantes.SOCIETE != null ? Constantes.SOCIETE.Id < 1 : true)
                {
                    CloseStart();
                    new IHM.Form_Societe(true).ShowDialog();
                    return;
                }

                Utils.WriteStatut(3);
                if (registry)
                    Utils.StartWithWindows();

                Utils.WriteStatut(4);
                if (Utils.Is64BitOperatingSystem())
                {
                    Utils.CreateRegistreDLL64Bits();
                }

                Form_Parent start = new Form_Parent();
                Constantes.FORM_PARENT = start;

                Constantes.FORM_PARENT.activerToolStripMenuItem.Text = Constantes.ACTIVE ? Mots.Cacher : Mots.Afficher;
                Constantes.FORM_PARENT.activerToolStripMenuItem.Image = Constantes.ACTIVE ? global::ZK_Lymytz.Properties.Resources.no_vue : global::ZK_Lymytz.Properties.Resources.vue;
                Constantes.FORM_PARENT.déconnectionToolStripMenuItem.Visible = Constantes.SETTING.CheckConnect;
                Constantes.FORM_PARENT.miseÀJourToolStripMenuItem.Visible = Utils.NewVersion();
                Constantes.FORM_PARENT.miseÀJourToolStripMenuItem1.Visible = Utils.NewVersion();

                Utils.WriteLog("-------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                Utils.WriteLog("DEMARRAGE DE L'APPLICATION.....");

                Utils.WriteStatut(5);
                if (Constantes.SETTING.Autorun)
                {
                    Fonctions.StartDevices();
                }
                Utils.WriteStatut(6);
                if (Constantes.SETTING.UseFileTamponLog)
                {
                    Fonctions.LoadFileTamponPointeuses(2, true);
                }
                Utils.WriteStatut(7);
                if (Constantes.SETTING.CreateService)
                {
                    Utils.RunService();
                }
                Utils.WriteStatut(8);
                if (Constantes.SETTING.AutoCheckConnectAndSynchro)
                {
                    Fonctions.CreateJobBackupAndSynchronise();
                }
                else
                {
                    Utils.WriteStatut(9);
                    if (Constantes.SETTING.AutoBackupDevice)
                    {
                        Fonctions.CreateJobBackup();
                    }
                }
                Utils.WriteStatut(10);
                if (Constantes.SETTING.AutoSynchro)
                {
                    new Thread(new ThreadStart(Fonctions.CheckPingAndSynchro)).Start();
                }
                Utils.WriteStatut(11);
                Utils.CreateExecuteService();

                CloseStart();
                Application.Run();
            }
            catch (Exception ex)
            {
                Messages.Exception(ex);
                Application.Restart();
            }
        }

        static void CloseStart()
        {
            try
            {
                Constantes.OBJECT_START.DisposeForm(true);
                if (Constantes.FORM_START != null)
                {
                    Constantes.OBJECT_START.Close();
                }
                Constantes._FIRST_OPEN = false;
            }
            catch (Exception ex)
            {
                Messages.Exception(ex);
            }
        }
    }
}
