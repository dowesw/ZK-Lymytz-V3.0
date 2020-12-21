using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Security;
using System.ServiceProcess;
using System.Diagnostics;
using System.Threading;
using System.Configuration.Install;
using System.DirectoryServices;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Net;
using System.Reflection;

using Microsoft.Win32;

using ZK_Lymytz.IHM;
using ZK_Lymytz.BLL;
using ZK_Lymytz.ENTITE;

using NpgsqlTypes;
using Npgsql;
using System.Net.Sockets;

namespace ZK_Lymytz.TOOLS
{
    public class Utils
    {
        System.Timers.Timer timer = new System.Timers.Timer();

        static IDictionary mySavedState = new Hashtable();
        static string[] commandLineOptions = new string[1] { "/LogFile=example.log" };

        //The MachineName property gets the name of your computer.

        public const int LOGON32_PROVIDER_DEFAULT = 0;
        public const int LOGON32_LOGON_INTERACTIVE = 2;

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool LogonUser(string lpszUsername,
            string lpszDomain,
            string lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            out IntPtr phToken
            );

        [DllImport("kernel32.dll")]
        public static extern int FormatMessage(int dwFlags, ref IntPtr lpSource, int dwMessageId, int dwLanguageId, ref String lpBuffer, int nSize, ref IntPtr Arguments);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);

        static int eventId = 0;

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);

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

        public static void Load()
        {
            Constantes.SOCIETE = SocieteBLL.ReturnSociete();
            Constantes.AGENCE = AgenceBLL.ReturnAgence();
            Constantes.SETTING = SettingBLL.ReturnSetting();
            Constantes.USERS = UsersBLL.ReturnUsers();
            Constantes.PARAMETRE = ParametreBLL.OneBySociete(Constantes.SOCIETE.Id);
            Constantes.POINTEUSES = PointeuseBLL.List("select * from yvs_pointeuse where (societe = " + Constantes.SOCIETE.Id + " and multi_societe IS TRUE) OR agence = " + Constantes.AGENCE.Id + " order by adresse_ip");
            Run();
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static void Run()
        {
            try
            {
                // Create a new FileSystemWatcher and set its properties.
                FileSystemWatcher watcher = new FileSystemWatcher();
                string path = Constantes.SETTING.CheminSetup;
                if (path != null ? path.Trim().Length > 0 : false)
                {
                    watcher.Path = path;
                    /* Watch for changes in LastAccess and LastWrite times, and
                       the renaming of files or directories. */
                    watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                       | NotifyFilters.FileName | NotifyFilters.DirectoryName;
                    // Only watch text files.
                    watcher.Filter = "*.exe";
                    // Add event handlers.
                    watcher.Changed += new FileSystemEventHandler(OnChanged);
                    watcher.Created += new FileSystemEventHandler(OnChanged);
                    watcher.Deleted += new FileSystemEventHandler(OnChanged);
                    // Begin watching.
                    watcher.EnableRaisingEvents = true;
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("Utils (Run) ", ex);
            }
        }

        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            if (Constantes.FORM_PARENT != null)
            {
                Constantes.FORM_PARENT.miseÀJourToolStripMenuItem.Visible = Utils.NewVersion();
                Constantes.FORM_PARENT.miseÀJourToolStripMenuItem1.Visible = Utils.NewVersion();
            }
        }

        public static bool NewVersion()
        {
            //String path = Application.StartupPath + Constantes.FILE_SEPARATOR + Application.ProductName + ".EXE";
            String path = Application.StartupPath + Constantes.FILE_SEPARATOR + "Uninstall.lnk";
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                path = Constantes.SETTING.CheminSetup + Constantes.FILE_SEPARATOR + Application.ProductName + ".EXE";
                FileInfo setup = new FileInfo(path);
                if (setup.Exists)
                {
                    if (file.LastWriteTime < setup.LastWriteTime)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsNumeric(object valeur)
        {
            try
            {
                int convert = Convert.ToInt32(valeur);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Cacher tous les formulaires actifs
        public static void CloseForm()
        {

            if (Constantes.FORM_ADD_EMPREINTE != null)
            {
                Constantes.FORM_ADD_EMPREINTE.Hide();
            }
            if (Constantes.FORM_SERVEUR != null)
            {
                Constantes.FORM_SERVEUR.Hide();
            }
            if (Constantes.FORM_SETTING != null)
            {
                Constantes.FORM_SETTING.Hide();
            }
            if (Constantes.FORM_ADD_POINTEUSE != null)
            {
                Constantes.FORM_ADD_POINTEUSE.Hide();
            }
            if (Constantes.FORM_UPD_POINTEUSE != null)
            {
                Constantes.FORM_UPD_POINTEUSE.Hide();
            }
            if (Constantes.FORM_ARCHIVE_POINTEUSE != null)
            {
                Constantes.FORM_ARCHIVE_POINTEUSE.Hide();
            }
            if (Constantes.FORM_ARCHIVE_SERVEUR != null)
            {
                Constantes.FORM_ARCHIVE_SERVEUR.Hide();
            }
            if (Constantes.FORM_VIEW_RESULT != null)
            {
                Constantes.FORM_VIEW_RESULT.Hide();
            }
            if (Constantes.FORM_VIEW_LOG != null)
            {
                Constantes.FORM_VIEW_LOG.Hide();
            }
            if (Constantes.FORM_GESTION_POINTEUSE != null)
            {
                Constantes.FORM_GESTION_POINTEUSE.Hide();
            }
            if (Constantes.FORM_EVENEMENT != null)
            {
                Constantes.FORM_EVENEMENT.Hide();
            }
            if (Constantes.FORM_EMPLOYE != null)
            {
                Constantes.FORM_EMPLOYE.Hide();
            }
            if (Constantes.FORM_EMPREINTE != null)
            {
                Constantes.FORM_EMPREINTE.Hide();
            }
            if (Constantes.FORM_PRESENCE != null)
            {
                Constantes.FORM_PRESENCE.Hide();
            }
            if (Constantes.FORM_FIND_POINTEUSE != null)
            {
                Constantes.FORM_FIND_POINTEUSE.Hide();
            }
            if (Constantes.FORM_PING_APPAREIL != null)
            {
                Constantes.FORM_PING_APPAREIL.Hide();
            }
        }

        // Afficher tous les formulaires cachés
        public static void OpenForm()
        {
            if (Constantes.FORM_ADD_EMPREINTE != null)
            {
                Constantes.FORM_ADD_EMPREINTE.Show();
            }
            if (Constantes.FORM_SERVEUR != null)
            {
                Constantes.FORM_SERVEUR.Show();
            }
            if (Constantes.FORM_SETTING != null)
            {
                Constantes.FORM_SETTING.Show();
            }
            if (Constantes.FORM_ADD_POINTEUSE != null)
            {
                Constantes.FORM_ADD_POINTEUSE.Show();
            }
            if (Constantes.FORM_UPD_POINTEUSE != null)
            {
                Constantes.FORM_UPD_POINTEUSE.Show();
            }
            if (Constantes.FORM_ARCHIVE_POINTEUSE != null)
            {
                Constantes.FORM_ARCHIVE_POINTEUSE.Show();
            }
            if (Constantes.FORM_ARCHIVE_SERVEUR != null)
            {
                Constantes.FORM_ARCHIVE_SERVEUR.Show();
            }
            if (Constantes.FORM_VIEW_RESULT != null)
            {
                Constantes.FORM_VIEW_RESULT.Show();
            }
            if (Constantes.FORM_VIEW_LOG != null)
            {
                Constantes.FORM_VIEW_LOG.Show();
            }
            if (Constantes.FORM_GESTION_POINTEUSE != null)
            {
                Constantes.FORM_GESTION_POINTEUSE.Show();
            }
            if (Constantes.FORM_EVENEMENT != null)
            {
                Constantes.FORM_EVENEMENT.Show();
            }
            if (Constantes.FORM_EMPLOYE != null)
            {
                Constantes.FORM_EMPLOYE.Show();
            }
            if (Constantes.FORM_EMPREINTE != null)
            {
                Constantes.FORM_EMPREINTE.Show();
            }
            if (Constantes.FORM_PRESENCE != null)
            {
                Constantes.FORM_PRESENCE.Show();
            }
            if (Constantes.FORM_FIND_POINTEUSE != null)
            {
                Constantes.FORM_FIND_POINTEUSE.Show();
            }
            if (Constantes.FORM_PING_APPAREIL != null)
            {
                Constantes.FORM_PING_APPAREIL.Show();
            }
        }

        public static string jourSemaine(DateTime date)
        {
            string jour = date.ToString("dddd", new CultureInfo("fr-FR").DateTimeFormat);
            return jour;
        }

        public static void WriteLog(string logMessage)
        {
            string text = DateTime.Now.ToString() + " : " + logMessage;
            if (Constantes.FORM_PARENT != null ? Constantes.FORM_PARENT.lv_report != null : false)
            {
                ListBox lv = Constantes.FORM_PARENT.lv_report;
                if (lv == null)
                {
                    text += "Formulaire principal fermé";
                    Logs.WriteTxt("");
                    return;
                }
                ObjectThread o = new ObjectThread(lv);
                o.WriteListBox(text);
                Logs.WriteTxt(text);
            }
            else
            {
                Console.WriteLine(text);
            }
        }

        public static void UpdateLog(int index, string logMessage)
        {
            string text = DateTime.Now.ToString() + " : " + logMessage;
            if (Constantes.FORM_PARENT != null ? Constantes.FORM_PARENT.lv_report != null : false)
            {
                ListBox lv = Constantes.FORM_PARENT.lv_report;
                if (lv == null)
                {
                    text += "Formulaire principal fermé";
                    return;
                }
                ObjectThread o = new ObjectThread(lv);
                o.RemoveListBox(index);
                o.UpdateListBox(index, logMessage);
            }
            else
            {
                Console.WriteLine(text);
            }
        }

        public static void WriteLog(ListBox lv, string logMessage)
        {
            ObjectThread o = new ObjectThread(lv);
            string text = DateTime.Now.ToString() + " : " + logMessage;
            o.WriteListBox(text);
            Logs.WriteTxt(text);
        }

        public static SecureString GetSecureString(string password)
        {
            SecureString secureString = new SecureString();
            foreach (char ch in password)
            {
                secureString.AppendChar(ch);
            }
            secureString.MakeReadOnly();
            return secureString;
        }

        public static void StopService(String name)
        {
            if (name == null || name.Trim().Equals(""))
            {
                name = "Zk-LymytzService";
            }
            Process[] processesList = Process.GetProcessesByName(name);
            foreach (Process p in processesList)
            {
                p.Kill();
            }
        }

        public static void StartWithWindows()
        {
            RegistryKey rk_ = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (rk_ != null)
            {
                String key = (String)rk_.GetValue(Constantes.APP_NAME);
                if (key != null ? key.Trim().Equals("") : true)
                {
                    rk_.SetValue(Constantes.APP_NAME, "\"" + Application.ExecutablePath.ToString() + "\" /autostart");
                }
            }
        }

        public static void CreateRegistreDLL64Bits()
        {
            try
            {
                using (RegistryKey Nkey = Registry.ClassesRoot)
                {
                    string chemin = "Wow6432Node\\CLSID\\" + Constantes.GUID_ZK + "";
                    RegistryKey rk = Nkey.OpenSubKey(@chemin, true);
                    if (rk == null)
                    {
                        Nkey.CreateSubKey(@chemin);
                        rk = Nkey.OpenSubKey(@chemin, true);
                    }
                    if (rk != null)
                    {
                        String key = (String)rk.GetValue("AppID");
                        if (key != null ? key.Trim().Equals("") : true)
                        {
                            rk.SetValue("AppID", Constantes.GUID_ZK);
                        }
                        else
                        {
                            if (!key.Equals(Constantes.GUID_ZK))
                                rk.SetValue("AppID", Constantes.GUID_ZK);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("Utils (CreateRegistreDLL64Bits) ", ex);
            }
        }

        public static void CreateRegistreDLL32Bits()
        {
            try
            {
                using (RegistryKey Nkey = Registry.ClassesRoot)
                {
                    string chemin = "CLSID\\" + Constantes.GUID_ZK + "";
                    RegistryKey rk = Nkey.OpenSubKey(@chemin, true);
                    if (rk == null)
                    {
                        Nkey.CreateSubKey(@chemin);
                        rk = Nkey.OpenSubKey(@chemin, true);
                    }
                    if (rk != null)
                    {
                        String key = (String)rk.GetValue("AppID");
                        if (key != null ? key.Trim().Equals("") : true)
                        {
                            rk.SetValue("AppID", Constantes.GUID_ZK);
                        }
                        else
                        {
                            if (!key.Equals(Constantes.GUID_ZK))
                                rk.SetValue("AppID", Constantes.GUID_ZK);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("Utils (CreateRegistreDLL64Bits) ", ex);
            }
        }

        public static bool IsAuthenticated(string username, string passwd)
        {
            return IsAuthenticated(username, passwd, Chemins.domainName);
        }

        public static bool IsAuthenticated(string username, string passwd, string domain)
        {
            try
            {
                IntPtr tokenHandle = new IntPtr(0);
                tokenHandle = IntPtr.Zero;

                //Call the LogonUser function to obtain a handle to an access token.
                bool returnValue = LogonUser(username, domain, passwd, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, out tokenHandle);
                if (returnValue == false)
                {
                    //This function returns the error code that the last unmanaged function returned.
                    int ret = Marshal.GetLastWin32Error();
                    string errmsg = GetErrorMessage(ret);
                    Messages.Erreur_Oui_Non(errmsg);
                }
                else
                {
                    //Create the WindowsIdentity object for the Windows user account that is
                    //represented by the tokenHandle token.
                    WindowsIdentity newId = new WindowsIdentity(tokenHandle);
                    WindowsPrincipal userperm = new WindowsPrincipal(newId);

                    //Verify whether the Windows user has administrative credentials.
                    if (newId.IsAuthenticated)
                    {
                        return true;
                    }
                }
                CloseHandle(tokenHandle);
                return false;
            }
            catch (Exception ex)
            {
                Messages.Exception("Utils (IsAuthenticated) ", ex);
                return false;
            }
        }

        public static void RunService()
        {
            if (!Utils.VerifyService("Zk-LymytzService"))
            {
                string service = Application.StartupPath + "\\Zk-Service.exe";
                if (File.Exists(service))
                {
                    using (Process process = new Process())
                    {
                        process.StartInfo.FileName = service;
                        process.StartInfo.CreateNoWindow = true;
                        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                        process.Start();
                    }
                }
            }
        }

        public static bool VerifyProcess(String name)
        {
            if (name == null || name.Trim().Equals(""))
            {
                return false;
            }
            Process[] processesList = Process.GetProcessesByName(name);
            bool r = processesList.Length > 0;
            return r;
        }

        public static bool VerifyService(String name)
        {
            if (name == null || name.Trim().Equals(""))
            {
                return false;
            }
            ServiceController[] scServices;
            scServices = ServiceController.GetServices();
            foreach (ServiceController scTemp in scServices)
            {
                if (scTemp.ServiceName == name)
                {
                    return true;
                }
            }
            return false;
        }

        public static void InstallService(string service)
        {
            ENTITE.Users u = UsersBLL.ReturnUsers();
            if (u != null ? u.Name != null : false)
            {
                CreateService(service, service, Application.ExecutablePath, "", "", "auto");
                Thread.Sleep(1000);
                StartService(service);
            }
            else
            {
                new IHM.Form_Users().ShowDialog();
            }
            if (!CheckRunningService(service))
                System.ServiceProcess.ServiceBase.Run(new System.ServiceProcess.ServiceBase());
        }

        public static void InstallService(string service, System.ServiceProcess.ServiceBase _base)
        {
            ENTITE.Users u = UsersBLL.ReturnUsers();
            if (u != null ? u.Name != null : false)
            {
                CreateService(service, service, Application.ExecutablePath, "", "", "auto");
                Thread.Sleep(1000);
                StartService(service);
            }
            else
            {
                new IHM.Form_Users().ShowDialog();
            }
            if (!CheckRunningService(service))
                System.ServiceProcess.ServiceBase.Run(_base);
        }

        public static void InstallService()
        {
            InstallService("Zk-LymytzService");
        }

        public static void InstallService(System.ServiceProcess.ServiceBase _base)
        {
            InstallService(_base.ServiceName, _base);
        }

        public static void DeleteService(string service)
        {
            Process.Start(@"C:\Windows\system32\sc.exe", "delete " + service);
        }

        public static void Cmd(string[] args)
        {
            if (args != null ? args.Length > 0 : false)
            {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < args.Length; i++)
                {
                    builder.AppendFormat("{" + i + "} ", args[i]);
                }
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = @"C:\Windows\system32\cmd.exe";
                    process.StartInfo.Arguments = builder.ToString();
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.Start();
                }
            }
        }

        public static void Cmd(string commande)
        {
            Process.Start(@"C:\Windows\system32\cmd.exe", commande);
        }

        public static void StartService(string service)
        {
            if (VerifyService(service))
            {
                ServiceController[] scServices;
                scServices = ServiceController.GetServices();
                foreach (ServiceController scTemp in scServices)
                {
                    if (scTemp.ServiceName == service)
                    {
                        if (scTemp.Status != ServiceControllerStatus.Running)
                        {
                            StringBuilder builder = new StringBuilder();
                            builder.AppendFormat("{0} {1} ", "Start", service);
                            using (Process process = new Process())
                            {
                                process.StartInfo.FileName = @"C:\Windows\system32\sc.exe";
                                process.StartInfo.Arguments = builder.ToString();
                                process.StartInfo.CreateNoWindow = true;
                                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                process.Start();
                            }
                        }
                        break;
                    }
                }
            }
        }

        public static bool CheckRunningService(string name)
        {
            if (VerifyService(name))
            {
                ServiceController[] scServices;
                scServices = ServiceController.GetServices();
                foreach (ServiceController scTemp in scServices)
                {
                    if (scTemp.ServiceName == name)
                    {
                        if (scTemp.Status == ServiceControllerStatus.Running)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static void CreateService(string name, string displayName, string binPath, string userName, string unecryptedPassword, string startupType)
        {
            // Determine statuptype
            string startupTypeConverted = string.Empty;
            switch (startupType)
            {
                case "Automatic":
                    startupTypeConverted = "auto";
                    break;
                case "Disabled":
                    startupTypeConverted = "disabled";
                    break;
                case "Manual":
                    startupTypeConverted = "demand";
                    break;
                default:
                    startupTypeConverted = "auto";
                    break;
            }
            // Determine if service has to be created (Create) or edited (Config)
            StringBuilder builder = new StringBuilder();
            if (VerifyService(name))
            {
                if (CheckRunningService(name))
                {
                    return;
                }
                builder.AppendFormat("{0} {1} ", "Config", name);
            }
            else
            {
                builder.AppendFormat("{0} {1} ", "Create", name);
            }
            builder.AppendFormat("binPath= \"{0}\"  ", binPath);
            builder.AppendFormat("displayName= \"{0}\"  ", displayName);
            // Only add "obj" when username is not empty. If omitted the "Local System" account will be used
            if (!string.IsNullOrEmpty(userName))
            {
                builder.AppendFormat("obj= \"{0}\"  ", userName);
            }
            // Only add password when unecryptedPassword it is not empty and user name is not "NT AUTHORITY\Local Service" or NT AUTHORITY\NetworkService
            if (!string.IsNullOrEmpty(unecryptedPassword) && !unecryptedPassword.Equals(@"NT AUTHORITY\Local Service") && !unecryptedPassword.Equals(@"NT AUTHORITY\NetworkService"))
            {
                builder.AppendFormat("password= \"{0}\"  ", unecryptedPassword);
            }
            builder.AppendFormat("start= \"{0}\"  ", startupTypeConverted);
            // Execute sc.exe commando
            using (Process process = new Process())
            {
                process.StartInfo.FileName = @"C:\Windows\system32\sc.exe";
                process.StartInfo.Arguments = builder.ToString();
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.Start();
            }
        }

        public static void ExecuteService()
        {
            Thread.Sleep(300000);
            Program.StartProgram(false);
        }

        public static double ParsedMaxDouble(String value)
        {
            String d = Double.MaxValue.ToString();
            if (value.Equals(d))
            {
                return Int64.MaxValue;
            }
            return Convert.ToDouble(value);
        }

        public static int GetRowData(DataGridView data, String id)
        {
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i].Cells[0].Value != null)
                {
                    String l = data.Rows[i].Cells[0].Value.ToString();
                    if (l.Equals(id))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public static int GetRowData(DataGridView data, long id)
        {
            for (int i = 0; i < data.Rows.Count; i++)
            {
                if (data.Rows[i].Cells[0].Value != null)
                {
                    long l = Convert.ToInt32(data.Rows[i].Cells[0].Value);
                    if (l.Equals(id))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public static bool verifyParametre()
        {
            Serveur serveur = BLL.ServeurBLL.ReturnServeur();
            if (serveur != null)
            {
                if (!serveur.Adresse.Equals("") && !serveur.Port.Equals(0) && !serveur.Database.Equals("") && !serveur.User.Equals("") && !serveur.Password.Equals(""))
                {
                    return true;
                }
            }
            return false;
        }

        public static void removeFrom(string name)
        {
            List<string> l = new List<string>();
            for (int i = 0; i < Constantes.FIRST_FORM.Count; i++)
            {
                string s = Constantes.FIRST_FORM[i];
                l.Add(s);
            }
            for (int i = 0; i < l.Count; i++)
            {
                string s = l[i];
                if (s.Trim().Equals(name.Trim(), StringComparison.CurrentCultureIgnoreCase))
                {
                    Constantes.FIRST_FORM.Remove(name);
                }
            }
        }

        public static bool asString(string value)
        {
            return value != null ? value.Trim().Length > 0 : false;
        }

        public static void addFrom(string name)
        {
            removeFrom(name);
            Constantes.FIRST_FORM.Add(name);
        }

        public static bool ExecuteScript()
        {
            if (!File.Exists(TOOLS.Chemins.cheminSystem32 + Constantes.FILE_SEPARATOR + Constantes.DLL))
            {
                if (!File.Exists(TOOLS.Chemins.cheminSystem64 + Constantes.FILE_SEPARATOR + Constantes.DLL))
                {
                    return false;
                }
            }
            return true;
        }

        //Retourne l'instance de l'appareil si elle est connecté
        public static Appareil ReturnAppareil(Pointeuse p)
        {
            if (Constantes.POINTEUSES != null ? Constantes.POINTEUSES.Count > 0 : false)
            {
                if (p != null)
                {
                    p = (Pointeuse)Constantes.POINTEUSES.Find(x => x.Id == p.Id);
                }
                if (p != null ? p.Id > 0 : false)
                {
                    if (p.Zkemkeeper != null)
                    {
                        p.Zkemkeeper._POINTEUSE = p;
                        return p.Zkemkeeper;
                    }
                }
            }
            return null;
        }

        public static void SetZkemkeeper()
        {
            SetZkemkeeper(0);
        }

        public static void SetZkemkeeper(int view)
        {
            if (Constantes.POINTEUSES != null ? Constantes.POINTEUSES.Count > 0 : false)
            {
                foreach (Pointeuse p in Constantes.POINTEUSES)
                {
                    if (!p.Connecter)
                    {
                        Appareil z = new Appareil();
                        bool b = false;
                        if (view > 0)
                            b = z.ConnectNet(p.Ip, p.Port, view == 1);
                        else
                            b = z.ConnectNet(p.Ip, p.Port);
                        if (b)
                        {
                            if (z.RegEvent(p.IMachine))
                            {
                                z._POINTEUSE = p;
                                p.Zkemkeeper = z;
                            }
                        }
                        else
                        {
                            BLL.PointeuseBLL.Deconnect(p.Id);
                        }
                    }
                }
            }
        }

        public static bool SetZkemkeeper(ref Pointeuse p)
        {
            return SetZkemkeeper(ref p, 0);
        }

        public static bool SetZkemkeeper(ref Pointeuse p, int view)
        {
            if (Constantes.POINTEUSES != null ? Constantes.POINTEUSES.Count > 0 : false)
            {
                if (!p.Connecter)
                {
                    if (p.Zkemkeeper != null ? p.Zkemkeeper._CONNEXION_RUNNING : true)
                    {
                        Appareil z = new Appareil();
                        z._CONNEXION_RUNNING = true;
                        if (view > 0)
                            z._BIS_CONNECTED = z.ConnectNet(p.Ip, p.Port, view == 1);
                        else
                            z._BIS_CONNECTED = z.ConnectNet(p.Ip, p.Port);
                        if (z._BIS_CONNECTED)
                        {
                            z._CONNEXION_RUNNING = false;
                            if (z.RegEvent(p.IMachine))
                            {
                                z._POINTEUSE = p;
                                int id = p.Id;
                                if (PointeuseBLL.Connect(id, z._I_MACHINE_NUMBER))
                                {
                                    p.Connecter = true;
                                    p.Zkemkeeper = z;

                                    int idx = Constantes.POINTEUSES.FindIndex(x => x.Id == id);
                                    if (idx > -1)
                                    {
                                        Constantes.POINTEUSES[idx] = p;
                                    }
                                    return true;
                                }
                            }
                        }
                    }
                    else
                    {
                        Messages.ShowWarning("Pattientez svp...");
                    }
                }
            }
            return false;
        }

        public static void VerifyZkemkeeper(ref Appareil z, ref Pointeuse p)
        {
            VerifyZkemkeeper(ref z, ref p, 0);
        }

        public static void VerifyZkemkeeper(ref Appareil z, ref Pointeuse p, int view)
        {
            if (z == null && p != null)
            {
                z = new TOOLS.Appareil(p);
                bool b = false;
                if (view > 0)
                    b = z.ConnectNet(p.Ip, p.Port, view == 1);
                else
                    b = z.ConnectNet(p.Ip, p.Port);
                if (b)
                {
                    RegEventAppareil(ref z, ref p);
                }
                else
                {
                    z = null;
                    Utils.WriteLog("Connexion de l'appareil : " + p.Ip + " impossible");
                }
            }
        }

        public static void VerifyZkemkeeper(ref Appareil z, ref Pointeuse p, Form_Add_Empreinte form)
        {
            VerifyZkemkeeper(ref z, ref p, form, 0);
        }

        public static void VerifyZkemkeeper(ref Appareil z, ref Pointeuse p, Form_Add_Empreinte form, int view)
        {
            if (z == null && p != null && form != null)
            {
                z = new TOOLS.Appareil(form, p);
                bool b = false;
                if (view > 0)
                    b = z.ConnectNet(p.Ip, p.Port, view == 1);
                else
                    b = z.ConnectNet(p.Ip, p.Port);
                if (b)
                {
                    RegEventAppareil(ref z, ref p);
                }
                else
                {
                    z = null;
                    Utils.WriteLog("Connexion de l'appareil : " + p.Ip + " impossible");
                }
            }
        }

        public static void VerifyZkemkeeper_(ref Appareil z, ref Pointeuse p, Form_Parent form)
        {
            VerifyZkemkeeper_(ref z, ref p, form, 0);
        }

        public static void VerifyZkemkeeper_(ref Appareil z, ref Pointeuse p, Form_Parent form, int view)
        {
            if (z == null && p != null && form != null)
            {
                z = new TOOLS.Appareil(p);
                bool b = false;
                if (view > 0)
                    b = z.ConnectNet(p.Ip, p.Port, view == 1);
                else
                    b = z.ConnectNet(p.Ip, p.Port);
                if (b)
                {
                    RegEventAppareil(ref z, ref p);
                }
                else
                {
                    z = null;
                    Utils.WriteLog("Connexion de l'appareil : " + p.Ip + " impossible");
                }
            }
        }

        private static void RegEventAppareil(ref Appareil z, ref Pointeuse p)
        {
            if (z.RegEvent(z._I_MACHINE_NUMBER))
            {
                z._POINTEUSE = p;
                int id = p.Id;
                if (PointeuseBLL.Connect(p.Id, z._I_MACHINE_NUMBER))
                {
                    p.IMachine = z._I_MACHINE_NUMBER;
                    p.Zkemkeeper = z;
                    p.Connecter = true;
                    p.Zkemkeeper = z;

                    Utils.WriteLog("Connexion de l'appareil : " + p.Ip + " effectuée");
                    if (Constantes.POINTEUSES != null ? Constantes.POINTEUSES.Count > 0 : false)
                    {
                        int idx = Constantes.POINTEUSES.FindIndex(x => x.Id == id);
                        if (idx > -1)
                        {
                            Constantes.POINTEUSES[idx] = p;
                        }
                    }
                    return;
                }
            }
        }

        public static void DestroyZkemkeeper(Pointeuse p)
        {
            if (Constantes.POINTEUSES != null ? Constantes.POINTEUSES.Count > 0 : false)
            {
                Constantes.POINTEUSES.Find(x => x.Id == p.Id).Zkemkeeper = null;
            }
        }

        public static List<ENTITE.Finger> Fingers()
        {
            List<ENTITE.Finger> list = new List<ENTITE.Finger>();
            ENTITE.Finger finger = null;
            finger = new ENTITE.Finger(0, "droite", "pouce");
            list.Add(finger);
            finger = new ENTITE.Finger(1, "droite", "index");
            list.Add(finger);
            finger = new ENTITE.Finger(2, "droite", "majeur");
            list.Add(finger);
            finger = new ENTITE.Finger(3, "droite", "annulaire");
            list.Add(finger);
            finger = new ENTITE.Finger(4, "droite", "auriculaire");
            list.Add(finger);
            finger = new ENTITE.Finger(5, "gauche", "pouce");
            list.Add(finger);
            finger = new ENTITE.Finger(6, "gauche", "index");
            list.Add(finger);
            finger = new ENTITE.Finger(7, "gauche", "majeur");
            list.Add(finger);
            finger = new ENTITE.Finger(8, "gauche", "annulaire");
            list.Add(finger);
            finger = new ENTITE.Finger(9, "gauche", "auriculaire");
            list.Add(finger);
            return list;
        }

        public static DateTime AddTimeInDate(DateTime date, DateTime heure)
        {
            DateTime d = date;
            try
            {
                d = d.AddHours(heure.Hour);
                d = d.AddMinutes(heure.Minute);
                d = d.AddSeconds(heure.Second);
            }
            catch (Exception ex)
            {
                Utils.Exception(ex);
            }
            return d;
        }

        public static DateTime RemoveTimeInDate(DateTime date, DateTime heure)
        {
            DateTime d = date;
            d = d.AddHours(-heure.Hour);
            d = d.AddMinutes(-heure.Minute);
            d = d.AddSeconds(-heure.Second);
            return d;
        }

        public static DateTime TimeStamp(DateTime date, DateTime heure)
        {
            if (date != null)
            {
                if (heure != null)
                {
                    return new DateTime(date.Year, date.Month, date.Day, heure.Hour, heure.Minute, heure.Second);
                }
                return new DateTime(date.Year, date.Month, date.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            }
            return DateTime.Now;
        }

        public static DateTime GetTimeStamp(DateTime date, DateTime heure)
        {
            DateTime d = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
            DateTime f = new DateTime(date.Year, date.Month, date.Day, heure.Hour, heure.Minute, heure.Second);
            if (f <= d)
            {
                d = f;
                d = d.AddDays(1.0);
            }
            else
            {
                d = f;
            }
            return d;
        }

        public static bool VerifyDateHeure(Planning p, DateTime h)
        {
            try
            {
                DateTime dateD = new DateTime(p.DateDebut.Year, p.DateDebut.Month, p.DateDebut.Day, 0, 0, 0);
                DateTime dateF = new DateTime(p.DateFin.Year, p.DateFin.Month, p.DateFin.Day, 0, 0, 0);
                DateTime heureD = p.HeureDebut;
                DateTime heureF = p.HeureFin;

                DateTime heure_debut = new DateTime(dateD.Year, dateD.Month, dateD.Day, heureD.Hour, heureD.Minute, 0);
                DateTime heure_fin = new DateTime(dateF.Year, dateF.Month, dateF.Day, heureF.Hour, heureF.Minute, 0);

                heure_debut = Utils.RemoveTimeInDate(heure_debut, Constantes.PARAMETRE.TimeMargeAvance);
                heure_fin = Utils.AddTimeInDate(heure_fin, Constantes.PARAMETRE.TimeMargeAvance);
                if (heure_debut <= h && h <= heure_fin)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Utils.Exception(ex);
            }
            return false;
        }

        public static string GetTime(double valeur)
        {
            string time = Convert.ToString(((Int32)valeur)).Length > 1 ? ((Int32)valeur) + "h" : "0" + ((Int32)valeur) + "h";
            double r = valeur - (Int32)valeur;
            time += Convert.ToString(Math.Round(r * 60)).Length > 1 ? Math.Round(r * 60) + "min" : "0" + Math.Round(r * 60) + "min";
            return time;
        }

        public static string GetErrorMessage(int errorCode)
        {
            int FORMAT_MESSAGE_ALLOCATE_BUFFER = 0x100;
            int FORMAT_MESSAGE_IGNORE_INSERTS = 0x200;
            int FORMAT_MESSAGE_FROM_SYSTEM = 0x1000;

            int msgSize = 255;
            string lpMsgBuf = null;
            int dwFlags = FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS;

            IntPtr lpSource = IntPtr.Zero;
            IntPtr lpArguments = IntPtr.Zero;
            int returnVal = FormatMessage(dwFlags, ref lpSource, errorCode, 0, ref lpMsgBuf, msgSize, ref lpArguments);

            if (returnVal == 0)
            {
                throw new Exception("Failed to format message for error code " + errorCode.ToString() + ". ");
            }
            return lpMsgBuf;

        }

        public static bool VerifyTable(string table, NpgsqlConnection connect)
        {
            NpgsqlCommand cmd = null;
            NpgsqlDataReader lect = null;
            try
            {
                if (connect == null)
                {
                    return false;
                }
                if (connect.State == System.Data.ConnectionState.Closed)
                {
                    connect.Open();
                }
                string query = "SELECT tablename FROM pg_tables WHERE tablename NOT LIKE 'pg_%' AND schemaname = 'public' AND tablename = '" + table + "'";
                cmd = new NpgsqlCommand(query, connect);
                lect = cmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        string result = lect[0] != null ? lect[0].ToString() : "";
                        return Utils.asString(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.Exception(ex);
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                if (lect != null)
                    lect.Dispose();
            }
            return false;
        }

        public static bool VerifyColumn(string table, string column, NpgsqlConnection connect)
        {
            NpgsqlCommand cmd = null;
            NpgsqlDataReader lect = null;
            try
            {
                if (connect == null)
                {
                    return false;
                }
                if (connect.State == System.Data.ConnectionState.Closed)
                {
                    connect.Open();
                }
                string query = "SELECT column_name FROM information_schema.columns WHERE table_name = '" + table + "' AND column_name = '" + column + "'";
                cmd = new NpgsqlCommand(query, connect);
                lect = cmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        string result = lect[0] != null ? lect[0].ToString() : "";
                        return Utils.asString(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.Exception(ex);
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
                if (lect != null)
                    lect.Dispose();
            }
            return false;
        }

        public static bool VerifyTable(string table, Serveur serveur)
        {
            try
            {
                NpgsqlConnection connect = new Connexion().returnConnexion(serveur, false);
                try
                {
                    return VerifyTable(table, connect);
                }
                catch (Exception ex)
                {
                    Utils.Exception(ex);
                }
                finally
                {
                    Connexion.Close(connect);
                }
            }
            catch (Exception ex)
            {
                Utils.Exception(ex);
            }
            return false;
        }

        public static bool VerifyColumn(string table, string column, Serveur serveur)
        {
            try
            {
                NpgsqlConnection connect = new Connexion().returnConnexion(serveur, false);
                try
                {
                    return VerifyColumn(table, column, connect);
                }
                catch (Exception ex)
                {
                    Utils.Exception(ex);
                }
                finally
                {
                    Connexion.Close(connect);
                }
            }
            catch (Exception ex)
            {
                Utils.Exception(ex);
            }
            return false;
        }

        public static bool RequeteLibre(string query)
        {
            return RequeteLibre(query, null);
        }

        public static bool RequeteLibre(string query, string adresse)
        {
            try
            {
                return Bll.RequeteLibre(query, adresse);
            }
            catch (Exception ex)
            {
                Utils.Exception(ex);
            }
            return false;
        }

        public static List<DateTime> TimeEmployeNotSystem(long employe_, DateTime dateDebut_, DateTime dateFin_)
        {
            List<DateTime> list = new List<DateTime>();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select restaure_pointage_employe_no_system(" + employe_ + ", '" + dateDebut_.ToShortDateString() + "', '" + dateFin_.ToShortDateString() + "')";
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        if ((lect["heure"] != null) ? lect["heure"].ToString() != "" : false)
                        {
                            list.Add(Convert.ToDateTime(lect["heure"].ToString()));
                        }

                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                Messages.Exception("Utils (TimeEmployeNotSystem) ", ex);
                return list;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }

        // Verify a hash against a string.
        public static bool VerifyMd5Hash(string input, string hash)
        {
            if (hash != null ? !hash.Equals("") : false)
            {
                // Hash the input.
                string hashOfInput = GetMd5Hash(input);
                // Create a StringComparer an compare the hashes.
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;

                if (0 == comparer.Compare(hashOfInput, hash))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public static bool Is64BitOperatingSystem()
        {
            if (Directory.Exists(Chemins.cheminSystem64))
            {
                return true;
            }
            return false;
        }

        public static void InstallSDK(bool copie)
        {
            string chemin = Chemins.CheminSDK();
            string path = (Is64BitOperatingSystem() ? Chemins.cheminSystem64 : Chemins.cheminSystem32) + Constantes.FILE_SEPARATOR;
            if (copie)
            {
                foreach (string dll in Constantes.DLL_SDK)
                {
                    if (File.Exists(chemin + dll) && !File.Exists(path + dll))
                    {
                        File.Copy(chemin + dll, path + dll);
                    }
                }
            }
            if (File.Exists(path + Constantes.DLL))
            {
                string cmd = (Is64BitOperatingSystem() ? Chemins.cheminSystem64 : Chemins.cheminSystem32) + Constantes.FILE_SEPARATOR;
                Process.Start(@cmd + "regsvr32.exe", "/s " + (path + Constantes.DLL));
            }
        }

        public static void UnInstallSDK(bool remove)
        {
            string path = (Is64BitOperatingSystem() ? Chemins.cheminSystem64 : Chemins.cheminSystem32) + Constantes.FILE_SEPARATOR;
            if (remove)
            {
                if (File.Exists(path + Constantes.DLL))
                {
                    string cmd = (Is64BitOperatingSystem() ? Chemins.cheminSystem64 : Chemins.cheminSystem32) + Constantes.FILE_SEPARATOR;
                    Process.Start(@cmd + "regsvr32.exe", "-u " + (path + Constantes.DLL));
                }
            }
            foreach (string dll in Constantes.DLL_SDK)
            {
                if (File.Exists(path + dll))
                {
                    File.Delete(path + dll);
                }
            }
        }

        public static void CreateExecuteService()
        {
            String file = Chemins.cheminStartup + Constantes.FILE_SEPARATOR + "run_synchro.bat";
            if (!File.Exists(file))
            {
                using (StreamWriter w = new StreamWriter(@file))
                {
                    w.WriteLine(Chemins.cheminStartup + Constantes.FILE_SEPARATOR + "ZK-Externe.exe");
                    w.WriteLine("pause");
                    w.Close();
                }
            }
        }

        public static List<IOEMDevice> FindLogsInFileTamponLogs(List<IOEMDevice> temporaires, bool addEmploye, Employe e, bool addDate, DateTime d, DateTime f)
        {
            bool with_time = !(d.ToShortTimeString().Equals("00:00:00:000") || d.ToShortTimeString().Equals("00:00:00") || d.ToShortTimeString().Equals("00:00") || d.ToShortTimeString().Equals("00"));
            List<IOEMDevice> logs = new List<IOEMDevice>();
            if (addEmploye ? (e != null ? e.Id > 0 : false) : false)
            {
                if (addDate)
                    if (with_time)
                        logs = temporaires.FindAll(x => (x.idwSEnrollNumber == e.Id && (d <= x.CurrentDateTime && x.CurrentDateTime <= f)));
                    else
                        logs = temporaires.FindAll(x => (x.idwSEnrollNumber == e.Id && (d <= x.CurrentDate && x.CurrentDate <= f)));
                else
                    logs = temporaires.FindAll(x => x.idwSEnrollNumber == e.Id);
            }
            else
            {
                if (addDate)
                    logs = temporaires.FindAll(x => (d <= (with_time ? x.CurrentDateTime : x.CurrentDate) && (with_time ? x.CurrentDateTime : x.CurrentDate) <= f));
                else
                    logs = temporaires;
            }
            return logs;
        }

        public static List<IOEMDevice> FindLogsInFileTamponLogsEx(List<IOEMDevice> temporaires, List<Employe> le, DateTime[] dates)
        {
            List<IOEMDevice> logs = new List<IOEMDevice>();
            if ((le != null ? le.Count > 0 : false) && (dates != null ? dates.Length > 0 : false))
            {
                if (le != null ? le.Count > 0 : false)
                {
                    if (dates != null ? dates.Length > 0 : false)
                    {
                        logs = temporaires.FindAll(x => (le.FindIndex(e => e.Id == x.idwSEnrollNumber) < 0 && !(new List<DateTime>(dates)).Contains(x.CurrentDate)));
                    }
                    else
                    {
                        logs = temporaires.FindAll(x => (le.FindIndex(e => e.Id == x.idwSEnrollNumber) < 0));
                    }
                }
                else
                {
                    if (dates != null ? dates.Length > 0 : false)
                    {
                        logs = temporaires.FindAll(x => (!(new List<DateTime>(dates)).Contains(x.CurrentDate)));
                    }
                    else
                    {
                        logs = FindLogsInFileTamponLogs(temporaires, false, null, false, DateTime.Now, DateTime.Now);
                    }
                }
            }
            else
            {
                logs = FindLogsInFileTamponLogs(temporaires, false, null, false, DateTime.Now, DateTime.Now);
            }
            return logs;
        }

        public static List<IOEMDevice> OLD_FindLogsInFileTamponLogs(List<IOEMDevice> temporaires, Employe e, bool date, DateTime d, DateTime f, bool load)
        {
            List<IOEMDevice> logs = new List<IOEMDevice>();
            string t = d.ToShortTimeString();
            bool heure_ = !(t.Equals("00:00:00:000") || t.Equals("00:00:00") || t.Equals("00:00") || t.Equals("00"));
            List<IOEMDevice> list = new List<IOEMDevice>(temporaires);
            if (e != null ? e.Id > 0 : false)
            {
                list = temporaires.FindAll(x => x.idwSEnrollNumber == e.Id);
            }
            foreach (IOEMDevice temp in list)
            {
                DateTime h = new DateTime(temp.idwYear, temp.idwMonth, temp.idwDay, 0, 0, 0);
                if (heure_)
                {
                    h = new DateTime(temp.idwYear, temp.idwMonth, temp.idwDay, temp.idwHour, temp.idwMinute, 0);
                }
                ENTITE.IOEMDevice iO = new ENTITE.IOEMDevice(temp.pointeuse, temp.iMachineNumber, temp.idwTMachineNumber, temp.idwSEnrollNumber, temp.idwInOutMode, temp.idwVerifyMode, temp.idwWorkCode, temp.idwReserved, temp.idwYear, temp.idwMonth, temp.idwDay, temp.idwHour, temp.idwMinute, temp.idwSecond);
                if (e != null ? e.Id > 0 : false)
                {
                    if (date)
                    {
                        if (temp.idwSEnrollNumber == e.Id && (d <= h && h <= f))
                        {
                            logs.Add(iO);
                        }
                    }
                    else
                    {
                        if (temp.idwSEnrollNumber == e.Id)
                        {
                            logs.Add(iO);
                        }
                    }
                }
                else
                {
                    if (date)
                    {
                        if (d <= h && h <= f)
                        {
                            logs.Add(iO);
                        }
                    }
                    else
                    {
                        logs.Add(iO);
                    }
                }
                if (load)
                {
                    Constantes.LoadPatience(false);
                }
            }
            return logs;
        }

        public static string ObjectToString(object o)
        {
            return o.ToString();
        }

        public static DateTime StringToDate(string o)
        {
            try
            {
                DateTime d = Convert.ToDateTime(o); ;
                return d;
            }
            catch (Exception ex)
            {
                return DateTime.Now;
            }
        }

        public static bool StringToAdress(string ip, ref string[] adresse)
        {
            try
            {
                if (ip != null ? ip.Trim().Length > 0 : false)
                {
                    ip = ip.Trim();
                    IPAddress address = IPAddress.Parse(ip);
                    adresse = ip.Split(new char[] { '.' });
                    if (4 > adresse.Length)
                    {
                        string[] temp = adresse;
                        adresse = new string[4];
                        for (int i = 0; i < temp.Length; i++)
                        {
                            adresse[i] = temp[i];
                        }
                        for (int i = temp.Length; i < 4; i++)
                        {
                            adresse[i] = "255";
                        }
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string GetLocalIPAddress()
        {
            string localIP = "";
            try
            {
                IPHostEntry host;
                host = Dns.GetHostEntry(Dns.GetHostName());

                foreach (IPAddress ip in host.AddressList)
                {
                    localIP = ip.ToString();

                    string[] temp = localIP.Split('.');

                    if (ip.AddressFamily == AddressFamily.InterNetwork && temp[0] == "192")
                    {
                        break;
                    }
                    else
                    {
                        localIP = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Messages.Exception(ex);
            }
            return localIP;
        }

        public static bool IsLocalAdress(string adresse)
        {
            try
            {
                if (!Utils.asString(adresse))
                {
                    return true;
                }
                if (adresse.Equals("127.0.0.1"))
                {
                    return true;
                }
                if (adresse.Equals("localhost"))
                {
                    return true;
                }
                string localhost = Utils.GetLocalIPAddress();
                if (adresse.Equals(localhost))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Messages.Exception(ex);
            }
            return false;
        }

        public static List<string> Adresses(string[] debut, string[] fin)
        {
            List<string> ips = new List<string>();
            int i = Convert.ToString(debut[0] + debut[1] + debut[2] + debut[3]).CompareTo(Convert.ToString(fin[0] + fin[1] + fin[2] + fin[3]));
            bool invers = i > 0;

            int d1 = invers ? Convert.ToInt16(fin[0]) : Convert.ToInt16(debut[0]);
            int d2 = invers ? Convert.ToInt16(fin[1]) : Convert.ToInt16(debut[1]);
            int d3 = invers ? Convert.ToInt16(fin[2]) : Convert.ToInt16(debut[2]);
            int d4 = invers ? Convert.ToInt16(fin[3]) : Convert.ToInt16(debut[3]);

            int f1 = invers ? Convert.ToInt16(debut[0]) : Convert.ToInt16(fin[0]);
            int f2 = invers ? Convert.ToInt16(debut[1]) : Convert.ToInt16(fin[1]);
            int f3 = invers ? Convert.ToInt16(debut[2]) : Convert.ToInt16(fin[2]);
            int f4 = invers ? Convert.ToInt16(debut[3]) : Convert.ToInt16(fin[3]);

            for (int un = d1; un < (d1 > f1 ? 256 : (f1 + 1)); un++)
            {
                for (int deux = d2; deux < (d2 > f2 ? 256 : (f2 + 1)); deux++)
                {
                    for (int trois = d3; trois < (d3 > f3 ? 256 : (f3 + 1)); trois++)
                    {
                        for (int quatre = d4; quatre < (d4 > f4 ? 256 : (f4 + 1)); quatre++)
                        {
                            ips.Add(un + "." + deux + "." + trois + "." + quatre);
                        }
                        d4 = 1;
                    }
                    d3 = 1;
                }
                d2 = 1;
            }
            return ips;
        }

        public static bool PingAdresse(string adresse, ref Appareil z)
        {
            try
            {
                if (z == null)
                    z = new Appareil();
                if (z.ConnectNet(adresse, 4370, false))
                    return true;
                return false;
            }
            catch (Exception ex) { return false; }
        }

        public static void Copy(string outputFilePath, string inputFilePath)
        {
            int bufferSize = 1024 * 1024;
            using (FileStream fileStream = new FileStream(outputFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            using (FileStream fs = new FileStream(inputFilePath, FileMode.Open, FileAccess.ReadWrite))
            {
                fileStream.SetLength(fs.Length);
                int bytesRead = -1;
                byte[] bytes = new byte[bufferSize];

                while ((bytesRead = fs.Read(bytes, 0, bufferSize)) > 0)
                {
                    fileStream.Write(bytes, 0, bytesRead);
                }
            }
        }

        public static long MilliSeconds(int day, int hour, int min, int sec)
        {
            int mDay = day * 24 * 60 * 60 * 1000;
            int mHour = hour * 60 * 60 * 1000;
            int mMin = min * 60 * 1000;
            int mSec = sec * 1000;
            long _milli = mDay + mHour + mMin + mSec;

            return _milli;
        }

        public static string SearchforCom()//modify by Dowes on Fev.14 2017
        {
            string sComValue;
            string sTmpara;
            RegistryKey myReg = Registry.LocalMachine.OpenSubKey("HARDWARE\\DEVICEMAP\\SERIALCOMM");
            string[] sComNames = myReg.GetValueNames();//strings array composed of the key name holded by the subkey "SERIALCOMM"
            for (int i = 0; i < sComNames.Length; i++)
            {
                sComValue = "";
                sComValue = myReg.GetValue(sComNames[i]).ToString();//obtain the key value of the corresponding key name
                if (sComValue == "")
                {
                    continue;
                }

                if (sComNames[i] == "\\Device\\USBSER000")//find the virtual serial port created by usbclient
                {
                    for (int j = 0; j <= 10; j++)
                    {
                        sTmpara = "";
                        RegistryKey myReg2 = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Enum\USB\VID_1B55&PID_B400\" + j.ToString() + @"\Device Parameters");//find the plug and play USB device
                        if (myReg2 != null)//modify by Dowes on Fev.14 2017
                        {
                            sTmpara = myReg2.GetValue("PortName").ToString();

                            if (sComValue == sTmpara)
                            {
                                return sTmpara;//modify by Dowes on Fev.14 2017
                            }
                        }
                    }
                }
            }
            return null;//modify by Dowes on Fev.14 2017
        }

        public static Form GetForm(string fullname)
        {
            //Ex Fullname : Scolaris.IHM.Form_Dictionnaire
            if (fullname != null ? fullname.Trim().Length > 0 : false)
            {
                Form form = (Form)Assembly.GetExecutingAssembly().CreateInstance(fullname);
                return form;
            }
            return null;
        }

        public static void WriteStatut(int position)
        {
            WriteStatut(position, false);
        }

        public static void WriteStatut(int position, bool wait)
        {
            if (Constantes.FORM_START == null)
            {
                Constantes.FORM_START = new Form_Start();
                Constantes.OBJECT_START = new ObjectThread(Constantes.FORM_START);
                Constantes.OBJECT_START.Show();
            }
            Constantes.FORM_START.Write(position, wait);
        }

        public static int hashCode(Object o)
        {
            return (o == null) ? 0 : o.GetHashCode();
        }

        public static string getMessageException(Exception ex)
        {
            StackTrace st = new StackTrace(ex, true);
            //Get the first stack frame
            StackFrame frame = st.GetFrame(0);
            //Get the file name
            string fileName = frame.GetFileName();
            //Get the method name
            string methodName = frame.GetMethod().Name;
            //Get the line number from the stack frame
            int line = frame.GetFileLineNumber();
            //Get the column number
            int col = frame.GetFileColumnNumber();

            string message = ex.Message + ". Class = " + fileName + ", Methode = " + methodName + " (ligne : " + line + ", colonne : " + col + ")";

            return message;
        }

        public static void Exception(Exception ex)
        {
            if (Constantes.DEBUG)
            {
                Utils.Exception(ex);
            }
            else
            {
                string message = Utils.getMessageException(ex);
                Utils.WriteLog(message);
            }
        }
    }
}
