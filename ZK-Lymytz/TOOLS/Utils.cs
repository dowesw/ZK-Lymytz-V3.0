using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Security;
using Microsoft.Win32;
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

using ZK_Lymytz.IHM;
using ZK_Lymytz.BLL;
using ZK_Lymytz.ENTITE;

using NpgsqlTypes;
using Npgsql;

namespace ZK_Lymytz.TOOLS
{
    public class Utils
    {
        System.Timers.Timer timer = new System.Timers.Timer();

        static IDictionary mySavedState = new Hashtable();
        static string[] commandLineOptions = new string[1] { "/LogFile=example.log" };

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

        public static string jourSemaine(DateTime date)
        {
            string jour = date.ToString("dddd", new CultureInfo("fr-FR").DateTimeFormat);
            return jour;
        }

        public static void WriteLog(string logMessage)
        {
            ListBox lv = Constantes.FORM_PARENT.lv_report;
            string text = DateTime.Now.ToString() + " : ";
            if (lv == null)
            {
                text += "Formulaire principal fermé";
                Logs.WriteTxt("");
                return;
            }
            ObjectThread o = new ObjectThread(lv);
            text += logMessage;
            o.WriteListBox(text);
            Logs.WriteTxt(text);
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
            //RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            //if (rk != null)
            //{
            //    String key = (String)rk.GetValue(Constantes.APP_NAME);
            //    if (key != null ? key.Trim().Equals("") : true)
            //    {
            //        rk.SetValue(Constantes.APP_NAME, "\"" + Application.ExecutablePath.ToString() + "\" /autostart");
            //    }
            //}

            RegistryKey rk_ = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (rk_ != null)
            {
                String key = (String)rk_.GetValue(Constantes.APP_NAME);
                if (key != null ? key.Trim().Equals("") : true)
                {
                    rk_.SetValue(Constantes.APP_NAME, "\"" + Application.ExecutablePath.ToString() + "\" /autostart");
                }
            }
            //string chemin = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce";
            //RegistryKey rk_ = Registry.LocalMachine.OpenSubKey(@chemin, true);
            //if (rk_ != null)
            //{
            //    eventId = 0;
            //    String key = (String)rk_.GetValue(Constantes.APP_NAME);
            //    if (key != null ? key.Trim().Equals("") : true)
            //    {
            //        rk_.SetValue(Constantes.APP_NAME, "\"" + Application.ExecutablePath.ToString() + "\" /silent");
            //    }
            //}
            //else
            //{
            //    Registry.LocalMachine.CreateSubKey(@chemin);
            //    if (eventId < 3)
            //    {
            //        StartWithWindows();
            //    }
            //    else
            //    {
            //        Environment.Exit(0);
            //    }
            //    eventId++;
            //}
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

                //The MachineName property gets the name of your computer.

                const int LOGON32_PROVIDER_DEFAULT = 0;
                const int LOGON32_LOGON_INTERACTIVE = 2;
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

        public static void InstallService()
        {
            ENTITE.Users u = UsersBLL.ReturnUsers();
            if (u != null ? u.Name != null : false)
            {
                string service = "Zk_LymytzService";
                string mySc = "ZK-Lymytz.exe";
                if (!VerifyService(service))
                {
                    //InstallService(mySc);
                    CreateService(service, Application.ExecutablePath.ToString());
                }
                else
                {
                    if (UninstallService(mySc))
                    {
                        InstallService(mySc);
                    }
                }
            }
            else
            {
                new IHM.Form_Users().ShowDialog();
            }
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

        public static void ExecuteService()
        {
            Thread.Sleep(300000);
            Program.StartProgram(false);
        }

        public static void InstallService(string ExeFilename)
        {
            try
            {
                System.Configuration.Install.AssemblyInstaller Installer = new System.Configuration.Install.AssemblyInstaller(ExeFilename, commandLineOptions);
                Installer.UseNewContext = true;
                Installer.Install(null);
                Installer.Commit(null);
            }
            catch (Exception ex)
            {
                Messages.Exception("Utils (InstallService) ", ex);
            }
        }

        public static bool UninstallService(string ExeFilename)
        {
            try
            {
                System.Configuration.Install.AssemblyInstaller Installer = new System.Configuration.Install.AssemblyInstaller(ExeFilename, commandLineOptions);
                Installer.UseNewContext = true;
                Installer.Uninstall(null);
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("Utils (UninstallService) ", ex);
                return false;
            }
        }

        public static void CreateService(string service, string path)
        {
            string cmd = "create " + service + " binPath = " + path + " start = auto";
            Process.Start(@"C:\Windows\system32\sc.exe", cmd);
        }

        public static void DeleteService_(string service)
        {
            Process.Start(@"C:\Windows\system32\sc.exe", "delete " + service);
        }

        public static bool DeleteService(string service)
        {
            string chemin = "SYSTEM/CurrentControlSet/Services/" + service;
            RegistryKey Nkey = Registry.LocalMachine;
            try
            {
                RegistryKey valKey = Nkey.OpenSubKey(@chemin, true);
                if (valKey != null)
                {
                    Nkey.DeleteSubKey(@chemin);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Messages.Exception("Utils (DeleteService) ", e);
                return false;
            }
            finally
            {
                Nkey.Close();
            }
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

        public static void addFrom(string name)
        {
            removeFrom(name);
            Constantes.FIRST_FORM.Add(name);
        }

        public static bool ExecuteScript()
        {
            if (!File.Exists(TOOLS.Chemins.cheminSystem32 + "\\zkemkeeper.dll"))
            {
                if (!File.Exists(TOOLS.Chemins.cheminSystem64 + "\\zkemkeeper.dll"))
                {
                    return false;
                }
            }
            return true;
        }

        public static void SetZkemkeeper()
        {
            if (Constantes.POINTEUSES != null ? Constantes.POINTEUSES.Count > 0 : false)
            {
                foreach (Pointeuse p in Constantes.POINTEUSES)
                {
                    if (!p.Connecter)
                    {
                        Appareil z = new Appareil();
                        bool b = z.ConnectNet(p.Ip, p.Port);
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

        public static void SetZkemkeeper(ref Pointeuse p)
        {
            if (Constantes.POINTEUSES != null ? Constantes.POINTEUSES.Count > 0 : false)
            {
                if (!p.Connecter)
                {
                    Appareil z = new Appareil();
                    bool b = z.ConnectNet(p.Ip, p.Port);
                    if (b)
                    {
                        if (z.RegEvent(p.IMachine))
                        {
                            Pointeuse p_ = p;
                            z._POINTEUSE = p;
                            Constantes.POINTEUSES.Find(x => x.Id == p_.Id).Zkemkeeper = z;
                            p.Zkemkeeper = z;
                        }
                    }
                }
            }
        }

        //Retourne l'instance de l'appareil si elle est connecté
        public static Appareil ReturnAppareil(Pointeuse p)
        {
            if (Constantes.POINTEUSES != null ? Constantes.POINTEUSES.Count > 0 : false)
            {
                p = (Pointeuse)Constantes.POINTEUSES.Find(x => x.Id == p.Id);
                if (p != null ? p.Id > 0 : false)
                {
                    if (p.Zkemkeeper != null)
                    {
                        return p.Zkemkeeper;
                    }
                }
            }
            return null;
        }

        public static void VerifyZkemkeeper(ref Appareil z, ref Pointeuse p)
        {
            if (z == null && p != null)
            {
                z = new TOOLS.Appareil(p);
                bool b = z.ConnectNet(p.Ip, p.Port);
                if (b)
                {
                    z.RegEvent(z._I_MACHINE_NUMBER);
                    if (PointeuseBLL.Connect(p.Id, z._I_MACHINE_NUMBER))
                    {
                        p.IMachine = z._I_MACHINE_NUMBER;
                        p.Zkemkeeper = z;

                        Utils.WriteLog("Connexion de l'appareil : " + p.Ip + " effectuée");
                        if (Constantes.POINTEUSES != null ? Constantes.POINTEUSES.Count > 0 : false)
                        {
                            Pointeuse p_ = p;
                            p_ = (Pointeuse)Constantes.POINTEUSES.Find(x => x.Id == p_.Id);
                            if (p_ != null ? p_.Id > 0 : false)
                            {
                                p_ = p;
                                z._POINTEUSE = p;
                                Constantes.POINTEUSES.Find(x => x.Id == p_.Id).Zkemkeeper = z;
                            }
                        }
                        return;
                    }
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
            if (z == null && p != null && form != null)
            {
                z = new TOOLS.Appareil(form, p);
                bool b = z.ConnectNet(p.Ip, p.Port);
                if (b)
                {
                    z.RegEvent(z._I_MACHINE_NUMBER);
                    if (PointeuseBLL.Connect(p.Id, z._I_MACHINE_NUMBER))
                    {
                        p.IMachine = z._I_MACHINE_NUMBER;
                        p.Zkemkeeper = z;

                        Utils.WriteLog("Connexion de l'appareil : " + p.Ip + " effectuée");
                        if (Constantes.POINTEUSES != null ? Constantes.POINTEUSES.Count > 0 : false)
                        {
                            Pointeuse p_ = p;
                            p_ = (Pointeuse)Constantes.POINTEUSES.Find(x => x.Id == p_.Id);
                            if (p_ != null ? p_.Id > 0 : false)
                            {
                                p_ = p;
                                z._POINTEUSE = p;
                                Constantes.POINTEUSES.Find(x => x.Id == p_.Id).Zkemkeeper = z;
                            }
                        }
                        return;
                    }
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
            if (z == null && p != null && form != null)
            {
                z = new TOOLS.Appareil(p);
                bool b = z.ConnectNet(p.Ip, p.Port);
                if (b)
                {
                    z.RegEvent(z._I_MACHINE_NUMBER);
                    if (PointeuseBLL.Connect(p.Id, z._I_MACHINE_NUMBER))
                    {
                        p.IMachine = z._I_MACHINE_NUMBER;
                        p.Zkemkeeper = z;

                        Utils.WriteLog("Connexion de l'appareil : " + p.Ip + " effectuée");
                        if (Constantes.POINTEUSES != null ? Constantes.POINTEUSES.Count > 0 : false)
                        {
                            Pointeuse p_ = p;
                            p_ = (Pointeuse)Constantes.POINTEUSES.Find(x => x.Id == p_.Id);
                            if (p_ != null ? p_.Id > 0 : false)
                            {
                                p_ = p;
                                z._POINTEUSE = p;
                                Constantes.POINTEUSES.Find(x => x.Id == p_.Id).Zkemkeeper = z;
                            }
                        }
                        return;
                    }
                }
                else
                {
                    z = null;
                    Utils.WriteLog("Connexion de l'appareil : " + p.Ip + " impossible");
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

        public static DateTime SetTimeStamp(DateTime date, DateTime heure)
        {
            DateTime d = date;
            d = d.AddHours(heure.Hour);
            d = d.AddMinutes(heure.Minute);
            d = d.AddSeconds(heure.Second);
            return d;
        }

        public static DateTime GetTimeStamp(DateTime date, DateTime heure)
        {
            DateTime d = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
            DateTime f = new DateTime(date.Year, date.Month, date.Day, heure.Hour, heure.Minute, heure.Second);
            if (f < d)
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

        public static bool RequeteLibre(string query)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                int i = Lcmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("Utils (RequeteLibre) ", ex);
                return false;
            }
            finally
            {
                connect.Close();
            }
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
                connect.Close();
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
    }
}
