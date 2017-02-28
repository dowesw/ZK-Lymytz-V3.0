using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

using ZK_Lymytz.BLL;
using ZK_Lymytz.IHM;
using ZK_Lymytz.TOOLS;
using ZK_Lymytz.ENTITE;

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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Configuration.Return();
            if (all)
                StartProgram(true);
        }

        static void Test()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Configuration.Return();
            Load();

            Form_Parent f = new Form_Parent();
            Constantes.FORM_PARENT = f;
            Constantes.ACTIVE = true;
            f.Hide();
            //insertAuto(new DateTime(2017, 02, 01, 07, 30, 0), new DateTime(2017, 02, 02, 15, 30, 0));
            //Fonctions.CheckPingAndSynchro(new Pointeuse("192.168.30.237"));
            var t = 0;
            Application.Run(new Form_Search_Pointeuse());
        }

        static void insertAuto(DateTime debut, DateTime fin)
        {
            while (debut < fin)
            {
                Logs.WriteTxt(Chemins.CheminPing() + "192.168.30.237.txt", debut.ToString());
                debut = debut.AddSeconds(50);
            }
        }

        public static void StartProgram(bool registry)
        {
            Configuration.Return();
            Start(registry);
        }

        static void Load()
        {
            Utils.Load();
        }

        static void Start(bool registry)
        {
            Load();
            if (!Appareil.Verify() || !Utils.ExecuteScript())
                Utils.InstallSDK(!Utils.ExecuteScript());

            if (Constantes.USERS != null ? Constantes.USERS.Name != null : false)
            {
                if (new TOOLS.Connexion().isInfosServeur(ServeurBLL.ReturnServeur()))
                {
                    if (new TOOLS.Connexion().Connection() != null)
                    {
                        if (Constantes.SOCIETE != null ? Constantes.SOCIETE.Id > 0 : false)
                        {
                            if (registry)
                                Utils.StartWithWindows();

                            if (Utils.Is64BitOperatingSystem())
                            {
                                Utils.CreateRegistreDLL64Bits();
                            }

                            Form_Parent start = new Form_Parent();
                            Constantes.FORM_PARENT = start;

                            Constantes.FORM_PARENT.activerToolStripMenuItem.Text = Constantes.ACTIVE ? Mots.Cacher : Mots.Afficher;
                            Constantes.FORM_PARENT.activerToolStripMenuItem.Image = Constantes.ACTIVE ? global::ZK_Lymytz.Properties.Resources.no_vue : global::ZK_Lymytz.Properties.Resources.vue;
                            Constantes.FORM_PARENT.déconnectionToolStripMenuItem.Visible = Constantes.SETTING.CheckConnect;

                            Utils.WriteLog("-------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                            Utils.WriteLog("DEMARRAGE DE L'APPLICATION.....");

                            if (Constantes.SETTING.UseFileTamponLog)
                            {
                                Fonctions.LoadFileTamponPointeuses(2, true);
                            }
                            if (Constantes.SETTING.Autorun)
                            {
                                Fonctions.StartDevices();
                            }
                            if (Constantes.SETTING.AutoCheckConnectAndSynchro || Constantes.SETTING.AutoSynchro)
                            {
                                Utils.RunService();
                            }
                            if (Constantes.SETTING.AutoSynchro)
                            {
                                Thread thread = new Thread(new ThreadStart(Fonctions.CheckPingAndSynchro));
                                thread.Start();
                            }
                            if (Constantes.SETTING.AutoBackupDevice)
                            {
                                new JobScheduler().StartBackupDataDevice();
                            }
                            Constantes._FIRST_OPEN = false;
                            Application.Run();
                        }
                        else
                        {
                            new IHM.Form_Societe().ShowDialog();
                        }
                    }
                }
                else
                {
                    new IHM.Form_Serveur().ShowDialog();
                }
            }
            else
            {
                new IHM.Form_Users().ShowDialog();
            }
        }
    }
}
