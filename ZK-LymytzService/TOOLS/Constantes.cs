using System;
using System.Collections.Generic;
using System.Text;

using ZK_LymytzService.ENTITE;

namespace ZK_LymytzService.TOOLS
{
    public class Constantes
    {
        public const string APP_NAME = "ZK-Lymytz";

        public static int FINGER_IN = 3;
        public static int FINGER_ID = -1;
        public static int FLAG = 3;
        public static string DOIGT = "";
        public static string MAIN = "";
        public static string S_TEMPLATE = "";
        public static int LONG_TMPL = 0;

        public static bool BIS_CONNECTED = false;
        public static int I_MACHINE_NUMBER = 1;
        public static bool VALIDER;
        public static string IP;
        public static DateTime CURRENT_DATE = new DateTime();
        public static DateTime CURRENT_TIME = new DateTime();

        public static string LAST_FORM = null;
        public static List<string> FIRST_FORM = new List<string>();
        public static List<Pointeuse> POINTEUSES = new List<Pointeuse>();

        public static Employe EMPLOYE = new Employe();
        public static Presence PRESENCE = new Presence();
        public static Planning PLANNING = new Planning();
        public static Parametre PARAMETRE = new Parametre();
        public static Societe SOCIETE = new Societe();
        public static Setting SETTING = new Setting();

        public static ENTITE.IOEMDevice iO = new IOEMDevice();
         

        public const string NAME_ADMIN = "MEG2710/";

        public const int MAX_ERROR_CONNECT = 2;
        public const int MAX_TIME_CONNECT = 30;

        public static bool ACTIVE = false;
        public static int TRIAL_ESSAIE = 30;
        public const int MAX_ESSAIE = 30;

        public const string FILE_SEPARATOR = "\\";

        public const string LANGUE_FRANCAIS_NAME = "Français";
        public const string LANGUE_FRANCAIS = "Fr";
        public const string LANGUE_ANGLAIS_NAME = "English";
        public const string LANGUE_ANGLAIS = "En";

        static public string[] MOIS = new string[] { "Janvier", "Fevrier", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre" };

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
    }
}
