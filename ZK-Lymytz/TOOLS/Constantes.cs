using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using ZK_Lymytz.IHM;
using ZK_Lymytz.ENTITE;

namespace ZK_Lymytz.TOOLS
{
    class Constantes
    {
        public const string APP_NAME = "ZK-Lymytz";

        public static string LAST_FORM = null;
        public static List<string> FIRST_FORM = new List<string>();
        public static List<Pointeuse> POINTEUSES = new List<Pointeuse>();
        public static Parametre PARAMETRE = new Parametre();
        public static Societe SOCIETE = new Societe();
        public static Setting SETTING = new Setting();
        public static Users USERS = new Users();

        public const string ADMINISTRATEUR = "MEG2710/";

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

        public static Form_Serveur FORM_SERVEUR = null;
        public static Form_Setting FORM_SETTING = null;
        public static Form_Parent FORM_PARENT = null;
        public static Form_Pointeuse FORM_ADD_POINTEUSE = null;
        public static Form_Pointeuse FORM_UPD_POINTEUSE = null;
        public static Form_Archive_Pointeuse FORM_ARCHIVE_POINTEUSE = null;
        public static Form_Archive_Serveur FORM_ARCHIVE_SERVEUR = null;
        public static Form_Add_Empreinte FORM_ADD_EMPREINTE = null;
        public static Form_ViewResult FORM_VIEW_RESULT = null;
        public static Form_Gestion_Pointeuse FORM_GESTION_POINTEUSE = null;
        public static Form_Evenement FORM_EVENEMENT = null;
        public static Form_Employe FORM_EMPLOYE = null;
        public static Form_Empreinte FORM_EMPREINTE = null;
        public static Form_Serveur_Distant FORM_SERVEUR_DISTANT = null;
        public static ProgressBar PBAR_WAIT = null;   

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

        public static void LoadPatience(bool _fin)
        {
            if (PBAR_WAIT != null)
            {
                if (!_fin)
                {
                    ObjectThread o = new ObjectThread(PBAR_WAIT);
                    o.UpdateBar(1);
                }
                else
                {
                    ObjectThread o = new ObjectThread(PBAR_WAIT);
                    o.UpdateBar(PBAR_WAIT.Maximum - PBAR_WAIT.Value);
                    PBAR_WAIT = null;
                }
            }
        }
    }
}
