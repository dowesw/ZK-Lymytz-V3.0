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

using ZK_Lymytz.BLL;
using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;

namespace ZK_Service
{
    public partial class Zk_LymytzService : ServiceBase
    {
        System.Diagnostics.EventLog log = new System.Diagnostics.EventLog();
        System.Timers.Timer timer = new System.Timers.Timer();

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

        public Zk_LymytzService()
        {
            InitializeComponent();
            string source = "Log";
            string logName = "ZK-Lymytz";
            log = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists(source))
            {
                System.Diagnostics.EventLog.CreateEventSource(source, logName);
            }
            log.Source = source;
            log.Log = logName;

            timer.Interval = 5000; // 5 seconds  
            timer.Elapsed += new System.Timers.ElapsedEventHandler(WorkProcess);
        }

        public void WorkProcess(object sender, System.Timers.ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.  
            OnContinue();
        }

        protected override void OnStart(string[] args)
        {
            log.WriteEntry("In OnStart");
            // Update the service state to Start Pending.
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            // Update the service state to Running.
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            timer.Enabled = true;
        }

        protected override void OnStop()
        {
            log.WriteEntry("In OnStop.");
            // Update the service state to Running.
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            timer.Enabled = false;
        }

        protected override void OnContinue()
        {
            log.WriteEntry("In OnContinue.");
            Societe s = SocieteBLL.ReturnSociete();
            List<Pointeuse> l = PointeuseBLL.List("select * from yvs_pointeuse where societe = " + s.Id + " and actif is true order by adresse_ip");
            foreach (Pointeuse p in l)
            {
                string file = Chemins.CheminPing() + p.Ip + ".txt";
                if (!File.Exists(file))
                {
                    File.Create(file);
                }
                log.WriteEntry("In Begin Ping On " + p.Ip);
                timer.Enabled = false;
                bool _died = false;
                while (!_died)
                {
                    if (new Appareil(p).ConnectNet())
                    {
                        Logs.WriteTxt(Chemins.CheminPing() + p.Ip + ".txt", DateTime.Now.ToString());
                    }
                    _died = true;
                }
                timer.Enabled = true;
            }
        }
    }
}
