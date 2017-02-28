using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using ZK_Lymytz.IHM;
using ZK_Lymytz.ENTITE;

namespace ZK_Lymytz.TOOLS
{
    public class Constantes
    {
        public const string APP_NAME = "ZK-Lymytz";

        public const string GUID_ZK = "{00853A19-BD51-419B-9269-2DABE57EB61F}"; 

        public const string SDK_32 = "_Auto-install_sdk_32";
        public const string SDK_64 = "_Auto-install_sdk_64";

        public const string DLL = "zkemkeeper.dll";
        public static string[] DLL_SDK = new string[] { "commpro.dll", "comms.dll", "rscagent.dll", "rscomm.dll", "tcpcomm.dll", "usbcomm.dll", "zkemkeeper.dll", "zkemsdk.dll", "plcommpro.dll", "plcomms.dll", "plrscagent.dll", "plrscomm.dll", "pltcpcomm.dll" };

        public static string LAST_FORM = null;
        public static List<string> FIRST_FORM = new List<string>();
        public static List<Pointeuse> POINTEUSES = new List<Pointeuse>();
        public static Parametre PARAMETRE = new Parametre();
        public static Societe SOCIETE = new Societe();
        public static Setting SETTING = new Setting();
        public static Configuration CONFIGURATION = new Configuration();
        public static Users USERS = new Users();

        public const string ADMINISTRATEUR = "MEG2710/";

        public const int MAX_ERROR_CONNECT = 2;
        public const int MAX_TIME_CONNECT = 30;
        public const int MAX_TIME_PING = 60;

        public static bool ACTIVE = false;
        public static bool _FIRST_OPEN = true;
        public static int TRIAL_ESSAIE = 30;
        public const int MAX_ESSAIE = 30;

        public const string FILE_SEPARATOR = "\\";

        public const string LANGUE_FRANCAIS_NAME = "Français";
        public const string LANGUE_FRANCAIS = "Fr";
        public const string LANGUE_ANGLAIS_NAME = "English";
        public const string LANGUE_ANGLAIS = "En";

        public static Form_Serveur FORM_SERVEUR = null;
        public static Form_Setting FORM_SETTING = null;
        public static Form_Parent FORM_PARENT = null;
        public static Form_Pointeuse FORM_ADD_POINTEUSE = null;
        public static Form_Pointeuse FORM_UPD_POINTEUSE = null;
        public static Form_Archive_Pointeuse FORM_ARCHIVE_POINTEUSE = null;
        public static Form_Archive_Serveur FORM_ARCHIVE_SERVEUR = null;
        public static Form_Add_Empreinte FORM_ADD_EMPREINTE = null;
        public static Form_ViewResult FORM_VIEW_RESULT = null;
        public static Form_ViewLog FORM_VIEW_LOG = null;
        public static Form_Gestion_Pointeuse FORM_GESTION_POINTEUSE = null;
        public static Form_Evenement FORM_EVENEMENT = null;
        public static Form_Employe FORM_EMPLOYE = null;
        public static Form_Presence FORM_PRESENCE = null;
        public static Form_Empreinte FORM_EMPREINTE = null;
        public static Form_Serveur_Distant FORM_SERVEUR_DISTANT = null;
        public static Form_Search_Pointeuse FORM_FIND_POINTEUSE;
        public static Form_Ping_Appareil FORM_PING_APPAREIL;
        public static Form_Wait FORM_WAIT = null;

        public static ProgressBar PBAR_WAIT = null;
        public static Scheduler JOB_SYNCHRODEVICE = null;
        public static Scheduler JOB_BACKUPDEVICE = null;

        public static string[] MOIS = new string[] { "Janvier", "Fevrier", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre" };

        public static string QUERY_EMPLOYE(Societe SOCIETE)
        {
            return "select e.* from yvs_grh_employes e inner join yvs_agences a on e.agence = a.id where a.societe = " + SOCIETE.Id + " order by e.nom, e.prenom, e.actif desc";
        }

        public static List<ENTITE.Finger> FINGERS()
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

        public static long MILLISECONDS()
        {
            TimeSpan interval = new TimeSpan(DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
            long nMilliseconds = interval.Days * 24 * 60 * 60 * 1000 +
                                 interval.Hours * 60 * 60 * 1000 +
                                 interval.Minutes * 60 * 1000 +
                                 interval.Seconds * 1000 +
                                 interval.Milliseconds;
            return nMilliseconds;
        }

        public static long MILLISECONDS(DateTime date)
        {
            date = date != null ? date : DateTime.Now;
            TimeSpan interval = new TimeSpan(date.Day, date.Hour, date.Minute, date.Second, date.Millisecond);
            long nMilliseconds = interval.Days * 24 * 60 * 60 * 1000 +
                                 interval.Hours * 60 * 60 * 1000 +
                                 interval.Minutes * 60 * 1000 +
                                 interval.Seconds * 1000 +
                                 interval.Milliseconds;
            return nMilliseconds;
        }

        public static void LoadPatience(bool _fin)
        {
            if (PBAR_WAIT != null)
            {
                ObjectThread o = new ObjectThread(PBAR_WAIT);
                if (!_fin)
                {
                    o.UpdateBar(1);
                }
                else
                {
                    o.UpdateBar(PBAR_WAIT.Maximum - PBAR_WAIT.Value);
                    PBAR_WAIT = null;
                }
            }
        }

        public static void LoadPatience(bool _fin, bool _vide)
        {
            if (PBAR_WAIT != null)
            {
                ObjectThread o = new ObjectThread(PBAR_WAIT);
                if (!_fin)
                {
                    o.UpdateBar(1);
                }
                else
                {
                    if (_vide)
                    {
                        o._UpdateBar(PBAR_WAIT.Maximum, "Aucun resultat");
                    }
                    else
                    {
                        o.UpdateBar(PBAR_WAIT.Maximum - PBAR_WAIT.Value);
                        PBAR_WAIT = null;
                    }
                }
            }
        }
    }
}
