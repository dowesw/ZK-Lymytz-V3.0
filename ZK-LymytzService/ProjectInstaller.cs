using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace ZK_LymytzService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        protected override void OnBeforeInstall(IDictionary savedState) 
        {
            string parameter = "Log\" \"ZK-Lymytz"; 
            Context.Parameters["assemblypath"] = "\"" + Context.Parameters["assemblypath"] + "\" \"" + parameter + "\"";
            base.OnBeforeInstall(savedState);

            using (ServiceController sc = new ServiceController(serviceInstaller1.ServiceName))
            {
                if (sc.Status != ServiceControllerStatus.Running)
                {
                    sc.Start();
                }
            }
        }
    }
}
