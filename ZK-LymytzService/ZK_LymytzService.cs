using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

using Microsoft.Win32;

using ZK_LymytzService.BLL;
using ZK_LymytzService.ENTITE;
using ZK_LymytzService.TOOLS;

namespace ZK_LymytzService
{
    partial class ZK_LymytzService : ServiceBase
    {
        System.Timers.Timer timer = new System.Timers.Timer();

        public ZK_LymytzService()
        {
            InitializeComponent();
            string eventSourceName = "Log";
            string logName = "ZK-Lymytz";
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists(eventSourceName))
            {
                System.Diagnostics.EventLog.CreateEventSource(eventSourceName, logName);
            }
            eventLog1.Source = eventSourceName;
            eventLog1.Log = logName;
        }

        public  ZK_LymytzService(string[] args)
        {
            InitializeComponent();
            string eventSourceName = "Log";
            string logName = "ZK-Lymytz";
            if (args != null)
            {
                if (args.Length > 0)
                {
                    eventSourceName = args[0];
                } if (args.Length > 1)
                {
                    logName = args[1];
                }
            }
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists(eventSourceName))
            {
                System.Diagnostics.EventLog.CreateEventSource(eventSourceName, logName);
            }
            eventLog1.Source = eventSourceName;
            eventLog1.Log = logName;
        }

        int eventId = 0;

        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);

        [DllImport("User32.dll")]
        private static extern bool BringWindowToTop(IntPtr handle);


        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public long dwServiceType;
            public ServiceState dwCurrentState;
            public long dwControlsAccepted;
            public long dwWin32ExitCode;
            public long dwServiceSpecificExitCode;
            public long dwCheckPoint;
            public long dwWaitHint;
        };

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("In OnStart");
            // Update the service state to Start Pending.
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            Thread t = new Thread(new ThreadStart(StartProgram));
            t.Start();
            //StartProgram();

            // Update the service state to Running.
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

        }

        protected override void OnContinue()
        {
            eventLog1.WriteEntry("In OnContinue.");
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("In onStop.");
            Utils.WriteLog("FERMETURE DE L'APPLICATION.....");
            Thread t = new Thread(new ThreadStart(StopProgram));
            t.Start();
            timer.Enabled = false;
        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.
            eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
            StartProgram();
            timer.Enabled = false;
        }

        void StartProgram_()
        {
            eventLog1.WriteEntry("In Start ZK-Lymytz.");
            System.Diagnostics.ProcessStartInfo myInfo = new System.Diagnostics.ProcessStartInfo();
            myInfo.FileName = "ZK-Lymytz.exe";
            myInfo.WorkingDirectory = "D:\\Projets\\ERP\\Outils\\ZK-Lymytz\\ZK-Lymytz\bin\\Release\\";
            myInfo.Verb = "runas";
            myInfo.CreateNoWindow = true;
            myInfo.UseShellExecute = true;
            myInfo.RedirectStandardInput = true;
            myInfo.RedirectStandardOutput = true;
            myInfo.RedirectStandardError = true;
            myInfo.WindowStyle = ProcessWindowStyle.Normal;
            System.Diagnostics.Process arjProcess = System.Diagnostics.Process.Start(myInfo);
            arjProcess.WaitForExit();
            BringWindowToTop(arjProcess.MainWindowHandle);
            System.Threading.Thread.Sleep(5000);
        }

        public void StopProgram()
        {
            Utils.StopService(null);
        }

        public void StartProgram()
        {
            eventLog1.WriteEntry("In Start ZK-Lymytz.");
            //Utils.WriteLog("--- VERIFICATION STARTUP ---");
            Constantes.SETTING = SettingBLL.ReturnSetting();
            if (!Utils.ExecuteScript())
            {
                return;
            }
            if (new TOOLS.Connexion().isInfosServeur(ServeurBLL.ReturnServeur()))
            {
                if (new TOOLS.Connexion().Connection() != null)
                {
                    Constantes.SOCIETE = SocieteBLL.ReturnSociete();
                    if (Constantes.SOCIETE != null ? Constantes.SOCIETE.Id > 0 : false)
                    {
                        Constantes.PARAMETRE = ParametreBLL.OneBySociete(Constantes.SOCIETE.Id);
                        Constantes.POINTEUSES = PointeuseBLL.List("select * from yvs_pointeuse where societe = " + Constantes.SOCIETE.Id + " order by adresse_ip");

                        Utils.WriteLog("-------------------------------------------------------------------------------------------------------------------------------------------------------------------");
                        Utils.WriteLog("DEMARRAGE DE L'APPLICATION.....");
                        eventLog1.WriteEntry("-- DEMARRAGE DE L'APPLICATION.....");

                        if (Constantes.SETTING.Autorun)
                        {
                            Utils.WriteLog("-- AUTORUN DEVICE");
                            eventLog1.WriteEntry("---- AUTORUN DEVICE");
                            ThreadRegEvent.StartDevices();
                        }
                        if (Constantes.SETTING.AutoSynchro)
                        {
                            Utils.WriteLog("-- AUTO SYNCHRO DEVICE");
                            eventLog1.WriteEntry("---- AUTORUN DEVICE");
                            ThreadRegEvent.SynchroniseLogServeur();
                        }
                        if (Constantes.SETTING.AutoBackupDevice)
                        {
                            Utils.WriteLog("-- AUTO BACKUP DEVICE");
                            eventLog1.WriteEntry("---- AUTORUN DEVICE");
                            new JobScheduler().Start();
                        }
                        Application.Run();
                        //Console.ReadKey();
                    }
                }
            }
        }

        public static void WriteToFile()
        {
            string chemin = Chemins.getCheminParametre();
            WriteToFile("CHEMIN : " + @chemin);

            WriteToFile("PERSONAL : " + Constantes.SETTING.CheminPersonal);
            WriteToFile("STARTUP : " + Constantes.SETTING.CheminStartup);
        }

        public static void WriteToFile(string text)
        {
            string path = "E:\\file.txt";
            if (!File.Exists(path))
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.WriteLine(text);
                    writer.Close();
                }
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(string.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
                    writer.Close();
                }
            }
        }

    }
}
