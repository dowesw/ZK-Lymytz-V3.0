using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ZK_Lymytz.TOOLS
{
    class Chemins
    {
        public static string cheminStartup = Application.StartupPath;
        public static string cheminDefault = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        public static string cheminSystem32 = Environment.ExpandEnvironmentVariables("%windir%") + Constantes.FILE_SEPARATOR + "System32";
        public static string cheminSystem64 = Environment.ExpandEnvironmentVariables("%windir%") + Constantes.FILE_SEPARATOR + "SysWOW64";
        public static string domainName = Environment.UserDomainName;
        public static string usersName = Environment.UserName;

        public static string getCheminParametre()
        {
            return "Software" + Constantes.FILE_SEPARATOR + Constantes.APP_NAME + Constantes.FILE_SEPARATOR + "Parametres";
        }

        public static string getCheminConfiguration()
        {
            return "Software" + Constantes.FILE_SEPARATOR + Constantes.APP_NAME + Constantes.FILE_SEPARATOR + "Configurations";
        }

        public static string getCheminSociete()
        {
            return "Software" + Constantes.FILE_SEPARATOR + Constantes.APP_NAME + Constantes.FILE_SEPARATOR + "Societe";
        }

        public static string getCheminServeur()
        {
            return "Software" + Constantes.FILE_SEPARATOR + Constantes.APP_NAME + Constantes.FILE_SEPARATOR + "Serveur";
        }

        public static string getCheminUsers()
        {
            return "Software" + Constantes.FILE_SEPARATOR + Constantes.APP_NAME + Constantes.FILE_SEPARATOR + "Users";
        }

        private static string CheminStart()
        {
            string chemin = cheminStartup;
            DirectoryInfo dossier = new DirectoryInfo(chemin);
            if (!dossier.Exists)
                dossier.Create();
            return chemin + Constantes.FILE_SEPARATOR;
        }

        public static string getCheminDatabase()
        {
            string chemin = CheminStart() + "Database";
            DirectoryInfo dossier = new DirectoryInfo(chemin);
            if (!dossier.Exists)
                dossier.Create();
            return chemin + Constantes.FILE_SEPARATOR;
        }

        public static string getCheminBackup()
        {
            string chemin = CheminStart() + "Backup";
            DirectoryInfo dossier = new DirectoryInfo(chemin);
            if (!dossier.Exists)
                dossier.Create();
            return chemin + Constantes.FILE_SEPARATOR;
        }

        public static string getCheminBackup(String name)
        {
            string chemin = getCheminBackup() + name;
            DirectoryInfo dossier = new DirectoryInfo(chemin);
            if (!dossier.Exists)
                dossier.Create();
            return chemin + Constantes.FILE_SEPARATOR;
        }

        public static string getCheminBackupServeur()
        {
            return getCheminBackup("Serveur");
        }

        public static string getCheminUser()
        {
            string chemin = cheminDefault;
            chemin = chemin.Substring(0, 1);
            chemin += Constantes.FILE_SEPARATOR + "Users" + Constantes.FILE_SEPARATOR + usersName;
            return chemin + Constantes.FILE_SEPARATOR;
        }
    }
}
