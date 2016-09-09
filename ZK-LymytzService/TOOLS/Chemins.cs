using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ZK_LymytzService.TOOLS
{
    public class Chemins
    {
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

        private static string CheminPersonal()
        {
            string chemin = Constantes.SETTING.CheminPersonal;
            DirectoryInfo dossier = new DirectoryInfo(chemin);
            if (!dossier.Exists)
                dossier.Create();
            return chemin + Constantes.FILE_SEPARATOR;
        }

        private static string CheminStart()
        {
            string chemin = Constantes.SETTING.CheminStartup;
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
    }
}
