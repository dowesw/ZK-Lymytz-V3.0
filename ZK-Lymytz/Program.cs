using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

        public static void Main_(bool all)
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
            ////Constantes.ACTIVE = true;
            ////Application.Run(f);

            f.Hide();
            Application.Run(new Form_Presence());
        }

        public static void StartProgram(bool registry)
        {
            Configuration.Return();
            Start(registry);
        }

        static void Load()
        {
            Constantes.SOCIETE = SocieteBLL.ReturnSociete();
            Constantes.SETTING = SettingBLL.ReturnSetting();
            Constantes.USERS = UsersBLL.ReturnUsers();
            Constantes.PARAMETRE = ParametreBLL.OneBySociete(Constantes.SOCIETE.Id);

            Constantes.POINTEUSES = PointeuseBLL.List("select * from yvs_pointeuse where societe = " + Constantes.SOCIETE.Id + " order by adresse_ip");
        }

        static void Start(bool registry)
        {
            Load();
            if (!Utils.ExecuteScript())
            {
                TOOLS.Messages.ShowErreur("Vous devez installer la sdk de la pointeuse");
                return;
            }
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
                            Form_Parent start = new Form_Parent();
                            Constantes.FORM_PARENT = start;

                            Constantes.FORM_PARENT.activerToolStripMenuItem.Text = Constantes.ACTIVE ? Mots.Cacher : Mots.Afficher;
                            Constantes.FORM_PARENT.activerToolStripMenuItem.Image = Constantes.ACTIVE ? global::ZK_Lymytz.Properties.Resources.no_vue : global::ZK_Lymytz.Properties.Resources.vue;
                            Constantes.FORM_PARENT.déconnectionToolStripMenuItem.Visible = Constantes.SETTING.CheckConnect;

                            Utils.WriteLog("-------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                            Utils.WriteLog("DEMARRAGE DE L'APPLICATION.....");

                            if (Constantes.SETTING.Autorun)
                            {
                                Fonctions.StartDevices();
                            }
                            if (Constantes.SETTING.AutoSynchro)
                            {
                                Fonctions.SynchroniseLogServeur();
                            }
                            if (Constantes.SETTING.AutoBackupDevice)
                            {
                                new JobScheduler().Start();
                            }
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
