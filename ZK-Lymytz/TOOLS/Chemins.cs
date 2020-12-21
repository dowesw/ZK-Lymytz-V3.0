using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ZK_Lymytz.TOOLS
{
    public class Chemins
    {
        public static string cheminStartup = Application.StartupPath;
        public static string cheminDefault = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        public static string cheminRoot = System.IO.Directory.GetDirectoryRoot(Environment.ExpandEnvironmentVariables("%windir%"));
        public static string cheminWindows = Environment.ExpandEnvironmentVariables("%windir%");
        public static string cheminSystem32 = Environment.ExpandEnvironmentVariables("%windir%") + Constantes.FILE_SEPARATOR + "System32";
        public static string cheminSystem64 = Environment.ExpandEnvironmentVariables("%windir%") + Constantes.FILE_SEPARATOR + "SysWOW64";
        public static string domainName = Environment.UserDomainName;
        public static string usersName = Environment.UserName;
        public static string machineName = Environment.MachineName.Normalize();

        public static string CheminParametre()
        {
            return "Software" + Constantes.FILE_SEPARATOR + Constantes.APP_NAME + Constantes.FILE_SEPARATOR + "Parametres";
        }

        public static string CheminConfiguration()
        {
            return "Software" + Constantes.FILE_SEPARATOR + Constantes.APP_NAME + Constantes.FILE_SEPARATOR + "Configurations";
        }

        public static string CheminSociete()
        {
            return "Software" + Constantes.FILE_SEPARATOR + Constantes.APP_NAME + Constantes.FILE_SEPARATOR + "Informations" + Constantes.FILE_SEPARATOR + "Societe";
        }

        public static string CheminAgence()
        {
            return "Software" + Constantes.FILE_SEPARATOR + Constantes.APP_NAME + Constantes.FILE_SEPARATOR + "Informations" + Constantes.FILE_SEPARATOR + "Agence";
        }

        public static string CheminUsers()
        {
            return "Software" + Constantes.FILE_SEPARATOR + Constantes.APP_NAME + Constantes.FILE_SEPARATOR + "Informations" + Constantes.FILE_SEPARATOR + "Users";
        }

        public static string CheminServeur()
        {
            return "Software" + Constantes.FILE_SEPARATOR + Constantes.APP_NAME + Constantes.FILE_SEPARATOR + "Serveur";
        }

        public static string CheminLiaisons()
        {
            return "Software" + Constantes.FILE_SEPARATOR + Constantes.APP_NAME + Constantes.FILE_SEPARATOR + "Liaisons";
        }

        private static string CheminStart()
        {
            string chemin = cheminStartup;
            DirectoryInfo dossier = new DirectoryInfo(chemin);
            if (!dossier.Exists)
                dossier.Create();
            return chemin + Constantes.FILE_SEPARATOR;
        }

        public static string CheminDatabase()
        {
            string chemin = CheminStart() + "Database";
            DirectoryInfo dossier = new DirectoryInfo(chemin);
            if (!dossier.Exists)
                dossier.Create();
            return chemin + Constantes.FILE_SEPARATOR;
        }

        public static string CheminPing()
        {
            string chemin = CheminDatabase() + "Ping";
            DirectoryInfo dossier = new DirectoryInfo(chemin);
            if (!dossier.Exists)
                dossier.Create();
            return chemin + Constantes.FILE_SEPARATOR;
        }

        public static string CheminPing(string ip)
        {
            string chemin = CheminPing() + ip;
            DirectoryInfo dossier = new DirectoryInfo(chemin);
            if (!dossier.Exists)
                dossier.Create();
            return chemin + Constantes.FILE_SEPARATOR;
        }

        public static string CheminSDK()
        {
            string chemin = CheminDatabase() + "SDK";
            DirectoryInfo dossier = new DirectoryInfo(chemin);
            if (!dossier.Exists)
                dossier.Create();
            return chemin + Constantes.FILE_SEPARATOR;
        }

        public static string CheminBackup()
        {
            string chemin = CheminDatabase() + "Backup";
            DirectoryInfo dossier = new DirectoryInfo(chemin);
            if (!dossier.Exists)
                dossier.Create();
            return chemin + Constantes.FILE_SEPARATOR;
        }

        public static string CheminBackup(String name)
        {
            string chemin = CheminBackup() + name;
            DirectoryInfo dossier = new DirectoryInfo(chemin);
            if (!dossier.Exists)
                dossier.Create();
            return chemin + Constantes.FILE_SEPARATOR;
        }

        public static string CheminBackupServeur()
        {
            return CheminBackup("Serveur");
        }

        public static string CheminUser()
        {
            string chemin = cheminDefault;
            chemin = chemin.Substring(0, 1);
            chemin += Constantes.FILE_SEPARATOR + "Users" + Constantes.FILE_SEPARATOR + usersName;
            return chemin + Constantes.FILE_SEPARATOR;
        }

        public static string InstallUtil()
        {
            string path = cheminWindows + "\\Microsoft.NET\\Framework\\v2.0.50727\\";
            DirectoryInfo dossier = new DirectoryInfo(path);
            if (!dossier.Exists)
            {
                path = cheminWindows + "\\Microsoft.NET\\Framework\\v3.0\\";
                dossier = new DirectoryInfo(path);
                if (!dossier.Exists)
                {
                    path = cheminWindows + "\\Microsoft.NET\\Framework\\v3.5\\";
                    dossier = new DirectoryInfo(path);
                    if (!dossier.Exists)
                    {
                        path = cheminWindows + "\\Microsoft.NET\\Framework\\v4.0.30319\\";
                    }
                }
            }
            path += "InstallUtil.exe";
            if (File.Exists(path))
            {
                return path;
            }
            return null;
        }
    }
}
