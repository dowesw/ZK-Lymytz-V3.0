using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.IO;
using System.Security;
using Microsoft.Win32;
using System.ServiceProcess;
using System.Diagnostics;
using System.Threading;
using System.DirectoryServices;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Principal;
using System.Security.Permissions;
using System.Runtime.InteropServices;

using ZK_LymytzService.BLL;
using ZK_LymytzService.ENTITE;

namespace ZK_LymytzService.TOOLS
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
            string text = DateTime.Now.ToString() + " : ";
            text += logMessage;
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

        public static void CreateService(string service, string path)
        {
            string cmd = "create " + service + " binPath = " + path + " start = auto";
            Process.Start(@"C:\Windows\system32\sc.exe", cmd);
        }

        public static void DeleteService_(string service)
        {
            Process.Start(@"C:\Windows\system32\sc.exe", "delete " + service);
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

        public static Zkemkeeper Zkemkeeper(Pointeuse p)
        {
            if (Constantes.POINTEUSES != null ? Constantes.POINTEUSES.Count > 0 : false)
            {
                p = Constantes.POINTEUSES.Find(x => x.Id == p.Id);
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

        public static void DestroyZkemkeeper(Pointeuse p)
        {
            if (Constantes.POINTEUSES != null ? Constantes.POINTEUSES.Count > 0 : false)
            {
                Constantes.POINTEUSES.Find(x => x.Id == p.Id).Zkemkeeper = null;
            }
        }

        public static void VerifyZkemkeeper(ref Zkemkeeper z, ref Pointeuse p)
        {
            if (z == null && p != null)
            {
                z = new TOOLS.Zkemkeeper(p);
                bool b = z.ConnectNet(p.Ip, p.Port);
                if (b)
                {
                    z.RegEvent(Constantes.I_MACHINE_NUMBER);
                    if (PointeuseBLL.Connect(p.Id, Constantes.I_MACHINE_NUMBER))
                    {
                        p.IMachine = Constantes.I_MACHINE_NUMBER;
                        p.Zkemkeeper = z;

                        Utils.WriteLog("Connexion de l'appareil : " + p.Ip + " effectuée");
                        if (Constantes.POINTEUSES != null ? Constantes.POINTEUSES.Count > 0 : false)
                        {
                            Pointeuse p_ = p;
                            p_ = Constantes.POINTEUSES.Find(x => x.Id == p_.Id);
                            if (p_ != null ? p_.Id > 0 : false)
                            {
                                p_ = p;
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
    }
}
