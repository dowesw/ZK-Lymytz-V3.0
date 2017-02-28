using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using System.Collections;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

using ZK_Lymytz.BLL;
using ZK_Lymytz.TOOLS;
using ZK_Lymytz.ENTITE;

namespace ZK_Service
{
    public static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        static void Main()
        {
            Utils.Load();
            Utils.InstallService(new Zk_LymytzService());
            Setting s = SettingBLL.ReturnSetting();
            if (s.AutoCheckConnectAndSynchro)
            {
                new JobScheduler().StartSynchroDevice();
            }
        }
    }
}
