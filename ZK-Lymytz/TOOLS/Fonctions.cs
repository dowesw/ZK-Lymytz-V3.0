using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

using Microsoft.Win32;

using ZK_Lymytz.IHM;
using ZK_Lymytz.BLL;
using ZK_Lymytz.ENTITE;

namespace ZK_Lymytz.TOOLS
{
    class Fonctions
    {
        private class ThreadArgs
        {
            private Employe _employe;
            public Employe Employe
            {
                get { return _employe; }
                set { _employe = value; }
            }

            private Empreinte _empreinte;
            public Empreinte Empreinte
            {
                get { return _empreinte; }
                set { _empreinte = value; }
            }
        }

        static List<Empreinte> _le = new List<Empreinte>();
        public static Pointeuse _pointeuse = new Pointeuse();

        public Fonctions() { }

        public Fonctions(Pointeuse p)
        {
            _pointeuse = p;
        }

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
        }

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
        }

        public static Presence Presence_(Planning p, Employe e)
        {
            Presence bean = new Presence();
            bean.HeureDebut = p.HeureDebut;
            bean.HeureFin = p.HeureFin;
            bean.Employe = e;
            bean.DateDebut = p.DateDebut;
            bean.DateFin = p.DateFin;
            bean.DureePause = p.DureePause;
            bean.Valider = p.Valide;
            bean.Supplementaire = p.Supplementaire;
            return bean;
        }

        public static Pointage Pointage_(Presence pe, DateTime heureEntree, DateTime heureSortie, Pointeuse pointeuse, bool systemIn, bool systemOut)
        {
            return Pointage_(pe, 0, heureEntree, heureSortie, false, pointeuse, systemIn, systemOut);
        }

        public static Pointage Pointage_(Presence pe, DateTime heureEntree, DateTime heureSortie, bool valider, Pointeuse pointeuse, bool systemIn, bool systemOut)
        {
            return Pointage_(pe, 0, heureEntree, heureSortie, valider, pointeuse, systemIn, systemOut);
        }

        public static Pointage Pointage_(Presence pe, long id, DateTime heureEntree, DateTime heureSortie, bool valider, Pointeuse pointeuse, bool systemIn, bool systemOut)
        {
            Pointage bean = new Pointage();
            bean.Id = id;
            bean.Presence = pe;
            bean.HeureEntree = heureEntree;
            bean.HeureSortie = heureSortie;
            bean.PointeuseIn = pointeuse;
            bean.PointeuseOut = pointeuse;
            bean.SystemOut = systemOut;
            bean.SystemIn = systemIn;
            bean.Supplementaire = pe.Supplementaire;
            bean.Valider = (!bean.Supplementaire ? valider : true);
            return bean;
        }

        public static bool InsertionPointage(Employe employe_, DateTime date_, DateTime heure_, Pointeuse pointeuse_)
        {
            DateTime h = new DateTime(heure_.Year, heure_.Month, heure_.Day, heure_.Hour, heure_.Minute, 0);
            string req = "select p.* from yvs_grh_pointage p inner join yvs_grh_presence r on p.presence = r.id where r.employe = " + employe_.Id + " and ((heure_entree is not null and heure_entree = '" + h + "') or (heure_sortie is not null and heure_sortie = '" + h + "'))";
            List<Pointage> p = PointageBLL.List(req);
            if (p != null ? p.Count < 1 : true)
            {
                return OnSavePointage(employe_, heure_, date_, pointeuse_);
            }
            return false;
        }

        public static bool OnSavePointage(Employe employe, DateTime current_time, DateTime current_date, Pointeuse pointeuse)
        {
            current_time = new DateTime(current_time.Year, current_time.Month, current_time.Day, current_time.Hour, current_time.Minute, 0);
            try
            {
                if ((employe != null) ? employe.Id > 0 : false)
                {
                    Planning planning = GetPlanning((Employe)employe, current_time);

                    Presence presence = GetPresence((Employe)employe, (Planning)planning, current_time);
                    if ((presence != null) ? presence.Id < 1 : true)
                    {
                        if (PresenceBLL.Insert(Presence_(planning, employe)))
                        {
                            List<Presence> lr = PresenceBLL.List("select * from yvs_grh_presence where date_debut = '" + planning.DateDebut.ToString("dd-MM-yyyy") + "' and date_fin = '" + planning.DateFin.ToString("dd-MM-yyyy") + "' and heure_debut = '" + planning.HeureDebut.ToString("HH:mm:ss") + "' and heure_fin = '" + planning.HeureFin.ToString("HH:mm:ss") + "' and employe = " + employe.Id + " order by heure_debut desc");
                            if (lr != null ? lr.Count > 0 : false)
                            {
                                Presence p_ = lr[0];
                                p_.HeureDebut = new DateTime(p_.DateDebut.Year, p_.DateDebut.Month, p_.DateDebut.Day, p_.HeureDebut.Hour, p_.HeureDebut.Minute, 0);
                                p_.HeureFin = new DateTime(p_.DateFin.Year, p_.DateFin.Month, p_.DateFin.Day, p_.HeureFin.Hour, p_.HeureFin.Minute, 0);
                                bool c = false;
                                if ((employe.Contrat != null) ? employe.Contrat.Id != 0 : false)
                                {
                                    if ((employe.Contrat.Calendrier != null) ? employe.Contrat.Calendrier.Id != 0 : false)
                                    {
                                        c = true;
                                        JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(employe.Contrat.Calendrier, Utils.jourSemaine(planning.DateDebut));
                                        if (jour != null ? jour.Id > 0 : false)
                                        {
                                            p_.Supplementaire = !jour.Ouvrable;
                                        }
                                    }
                                }
                                if (!c)
                                {
                                    JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(CalendrierBLL.Default(), Utils.jourSemaine(planning.DateDebut));
                                    if (jour != null ? jour.Id > 0 : false)
                                    {
                                        p_.Supplementaire = !jour.Ouvrable;
                                    }
                                }
                                presence = p_;
                            }
                            if (presence != null ? presence.Id < 1 : true)
                            {
                                OnSavePointage(employe, current_time, current_date, pointeuse);
                            }
                        }
                    }
                    //Recherche le dernier pointage
                    List<Pointage> lp = PointageBLL.List("select * from yvs_grh_pointage where presence = " + presence.Id + " and heure_entree is not null order by heure_entree desc");
                    if (lp != null ? lp.Count < 1 : true)//S'il n'y'a pas de pointage
                    {
                        //On insere une entrée
                        return OnSavePointage("E", null, (Presence)presence, current_time, pointeuse);
                    }
                    else
                    {
                        //S'il existe on le recupère
                        Pointage po = lp[0];
                        //On verifi si le dernier pointage est une entrée
                        if ((po.HeureSortie != null) ? po.HeureSortie.ToString() == "01/01/0001 00:00:00" : true)//Si le dernier pointage etait une entrée
                        {
                            return OnSavePointage("S", po, (Presence)presence, current_time, pointeuse);
                        }
                        else//Si le dernier pointage etait une sortie
                        {
                            return OnSavePointage("E", po, (Presence)presence, current_time, pointeuse);
                        }
                    }
                }
                else
                {
                    Utils.WriteLog("Employé Inconnu !");
                }
                return false;
            }
            catch (Exception ex)
            {
                Messages.Exception("RegEvent (OnSavePointage)", ex);
                return false;
            }
        }

        private static bool OnSavePointage(string mouv, Pointage po, Presence pe, DateTime current_time, Pointeuse pointeuse)
        {
            switch (mouv)
            {
                case "S":
                    {
                        //On verifi si l'heure d'entrée etait inferieur a l'heure d'entree prevu
                        if (po.HeureEntree < pe.HeureDebut)//Si l'heure d'entree etait inferieur a l'heure d'entree prevu
                        {
                            //On verifi si l'heure actuelle est superieur a l'heure d'entree prevu
                            if (current_time > pe.HeureDebut)//Si l'heure actuelle  est superieur a l'heure d'entree prevu
                            {
                                //On Complete la sortie du dernier pointage par l'heure d'entree prevu
                                if (PointageBLL.Update(Pointage_(pe, pe.HeureDebut, pe.HeureDebut, false, pointeuse, true, true), po.Id))
                                {
                                    //On verifi si l'heure actuelle est superieur a l'heure de sortie prevu
                                    if (current_time > pe.HeureFin) //Si l'heure actuelle est superieur a l'heure de sortie prevu
                                    {
                                        //On insert un pointage supplementaire qui va de l'heure d'entre prevu a l'heure de sortie prevu
                                        if (PointageBLL.InsertU(Pointage_(pe, pe.HeureDebut, pe.HeureFin, true, pointeuse, true, true)))
                                        {
                                            //On insert un pointage supplementaire qui va de l'heure de sortie prevu a l'heure actuelle
                                            return PointageBLL.InsertU(Pointage_(pe, pe.HeureFin, current_time, false, pointeuse, true, false));
                                        }
                                    }
                                    else //Si l'heure actuelle est infereiur a l'heure de sortie prevu
                                    {
                                        //On insert un pointage supplementaire qui va de l'heure d'entree prevu a l'heure actuelle
                                        return PointageBLL.InsertU(Pointage_(pe, pe.HeureDebut, current_time, true, pointeuse, true, false));
                                    }
                                }
                            }
                            else//Si l'heure actuelle est inferieur a l'heure d'entree prevu
                            {
                                return PointageBLL.Update(Pointage_(pe, current_time, current_time, false, pointeuse, false, false), po.Id);
                            }
                        }//On verifi si l'heure d'entre etait superieur a l'heure de sorti prevu
                        else if (po.HeureEntree >= pe.HeureFin)//Si l'heure d'entree etait superieur ou egale a l'heure de sortie prevu
                        {
                            //On Complete la sortie du dernier pointage par l'heure actuelle
                            return PointageBLL.Update(Pointage_(pe, current_time, current_time, false, pointeuse, false, false), po.Id);
                        }
                        else//Si l'heure d'entree etait compris entre l'heure d'entree prevu et l'heure de sortie prevu
                        {
                            //On verifi si l'heure actuelle est superieur a l'heure de sortie prevu
                            if (current_time > pe.HeureFin)//Si l'heure actuelle est superieur a l'heure de sortie prevu
                            {
                                //On Complete la sortie du dernier pointage par l'heure de sortie prevu
                                if (PointageBLL.Update(Pointage_(pe, pe.HeureFin, pe.HeureFin, true, pointeuse, false, true), po.Id))
                                {
                                    //On insert un pointage supplementaire qui va de l'heure de sortie prevu a l'heure actuelle
                                    return PointageBLL.InsertU(Pointage_(pe, pe.HeureFin, current_time, false, pointeuse, true, false));
                                }
                            }
                            else
                            {
                                //On Complete la sortie du dernier pointage par l'heure actuelle
                                return PointageBLL.Update(Pointage_(pe, current_time, current_time, true, pointeuse, false, false), po.Id);
                            }
                        }
                        break;
                    }
                case "E":
                    {
                        //On insert une entrée
                        DateTime h_ = Utils.SetTimeStamp(pe.HeureDebut, Constantes.PARAMETRE.TimeMargeAutorise);
                        if (pe.HeureDebut < current_time && current_time < h_)
                        {
                            Presence p_ = new Presence();
                            p_.Id = pe.Id;
                            p_.MargeApprouve = Constantes.PARAMETRE.TimeMargeAutorise;
                            PresenceBLL.Update(p_);
                        }
                        else
                        {
                            Presence p_ = new Presence();
                            p_.Id = pe.Id;
                            p_.MargeApprouve = new DateTime(pe.DateFin.Year, pe.DateFin.Month, pe.DateFin.Day, 0, 0, 0);
                            PresenceBLL.Update(p_);
                        }
                        return PointageBLL.Insert(Pointage_(pe, current_time, current_time, true, pointeuse, false, false));
                    }
                default:
                    break;

            }
            return false;
        }

        public static TrancheHoraire GetTrancheHoraire(DateTime heure_, string query)
        {
            TrancheHoraire p_ = new TrancheHoraire();
            List<TrancheHoraire> lt = TrancheHoraireBLL.List(query);
            if (lt != null ? lt.Count > 0 : false)
            {
                for (int i = 0; i < lt.Count; i++)
                {
                    TrancheHoraire t = lt[i];
                    DateTime heureDebut = new DateTime(heure_.Year, heure_.Month, heure_.Day, t.HeureDebut.Hour, t.HeureDebut.Minute, 0);
                    DateTime heureFin = Utils.GetTimeStamp(heureDebut, t.HeureFin);

                    if (heure_ < heureDebut)
                    {
                        if (i == 0)
                        {
                            p_.Id = t.Id;
                            p_.HeureDebut = t.HeureDebut;
                            p_.HeureFin = t.HeureFin;
                            p_.DureePause = t.DureePause;
                        }
                        else
                        {
                            TrancheHoraire t_ = lt[i - 1];
                            DateTime d = Utils.SetTimeStamp(heure_, Constantes.PARAMETRE.TimeMargeAvance);
                            DateTime hd = new DateTime(heure_.Year, heure_.Month, heure_.Day, t.HeureDebut.Hour, t.HeureDebut.Minute, t.HeureDebut.Second);
                            if (d < hd)
                            {
                                p_.Id = t_.Id;
                                p_.HeureDebut = t_.HeureDebut;
                                p_.HeureFin = t_.HeureFin;
                                p_.DureePause = t_.DureePause;
                            }
                            else
                            {
                                p_.Id = t.Id;
                                p_.HeureDebut = t.HeureDebut;
                                p_.HeureFin = t.HeureFin;
                                p_.DureePause = t.DureePause;
                            }
                        }
                        break;
                    }
                    else if ((heureDebut < heure_) && (heure_ < heureFin))
                    {
                        if (i < lt.Count - 1)
                        {
                            TrancheHoraire t_ = lt[i + 1];
                            DateTime d = Utils.SetTimeStamp(heure_, Constantes.PARAMETRE.TimeMargeAvance);
                            DateTime hd = new DateTime(heure_.Year, heure_.Month, heure_.Day, t_.HeureDebut.Hour, t_.HeureDebut.Minute, t_.HeureDebut.Second);
                            if (d >= hd)
                            {
                                p_.Id = t_.Id;
                                p_.HeureDebut = t_.HeureDebut;
                                p_.HeureFin = t_.HeureFin;
                                p_.DureePause = t_.DureePause;
                            }
                            else
                            {
                                p_.Id = t.Id;
                                p_.HeureDebut = t.HeureDebut;
                                p_.HeureFin = t.HeureFin;
                                p_.DureePause = t.DureePause;
                            }
                        }
                        else
                        {
                            p_.Id = t.Id;
                            p_.HeureDebut = t.HeureDebut;
                            p_.HeureFin = t.HeureFin;
                            p_.DureePause = t.DureePause;
                        }
                        break;
                    }
                }
            }
            return p_;
        }

        public static Planning GetPlanning(Employe e, DateTime heure_) 
        {
            try
            {
                Planning p_ = new Planning();
                if ((e != null) ? e.Id > 0 : false)
                {
                    // On verifie si l'employé a un horaire dynamique
                    if (e.HoraireDynamique) // Si oui
                    {
                        // On recherche le planning de l'employé a la date
                        List<Planning> lp = PlanningBLL.List("select * from yvs_grh_planning_employe where employe =" + e.Id + " and '" + heure_.ToString("dd-MM-yyyy") + "' between date_debut and date_fin order by date_debut");
                        if (lp.Count > 0)// Si il a un planning
                        {
                            if (lp.Count > 1)// Si il a plus d'un planning
                            {
                                int i = 0;
                                // On recherche le bon planning
                                foreach (Planning p in lp)
                                {
                                    p_ = p;

                                    DateTime dateD = new DateTime(p.DateDebut.Year, p.DateDebut.Month, p.DateDebut.Day, 0, 0, 0);
                                    DateTime dateF = new DateTime(p.DateFin.Year, p.DateFin.Month, p.DateFin.Day, 0, 0, 0);
                                    DateTime heureD = p.HeureDebut;
                                    DateTime heureF = p.HeureFin;

                                    DateTime heure_debut = new DateTime(dateD.Year, dateD.Month, dateD.Day, heureD.Hour, heureD.Minute, 0);
                                    DateTime heure_fin = new DateTime(dateF.Year, dateF.Month, dateF.Day, heureF.Hour, heureF.Minute, 0);

                                    // On verifi si l'heure est compris dans le planning actuelle
                                    if (heure_ >= heure_debut && heure_ <= heure_fin) // Si l'heure est compris on sort
                                    {
                                        break;
                                    }
                                    else if (heure_ < heure_debut && i == 0) // Si l'heure est inferieur au 1er planning on sort
                                    {
                                        break;
                                    }
                                    else if (heure_ > heure_fin)
                                    {
                                        List<Presence> lr = PresenceBLL.List("select * from yvs_grh_presence where date_debut = '" + dateD + "' and date_fin = '" + dateF + "' and employe = " + e.Id + " order by date_debut desc, heure_debut desc");
                                        if (lr != null ? lr.Count > 0 : false)
                                        {
                                            Presence pe = lr[0];
                                            List<Pointage> lo = PointageBLL.List("select * from yvs_grh_pointage where presence = " + pe.Id + " order by heure_entree desc limit 1");
                                            if (lo != null ? lo.Count > 0 : false)
                                            {
                                                Pointage po = lo[0];
                                                if ((po != null) ? po.Id != 0 : false)
                                                {
                                                    if ((po.HeureSortie != null) ? po.HeureSortie.ToString() == "01/01/0001 00:00:00" : true)
                                                    {
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    ++i;
                                }
                            }
                            else
                            {
                                p_ = lp[0];
                            }
                        }

                        if ((p_ != null) ? p_.Id < 1 : true)
                        {
                            if (Constantes.PARAMETRE.PlanningDynamique)
                            {
                                bool deja = false;
                                List<Presence> lr = PresenceBLL.List("select * from yvs_grh_presence where '" + heure_.ToString("dd-MM-yyyy") + "' between date_debut and date_fin and employe = " + e.Id + " order by date_debut desc, heure_debut desc");
                                if (lr != null ? lr.Count > 0 : false)
                                {
                                    Presence pe = lr[0];
                                    List<Pointage> lo = PointageBLL.List("select * from yvs_grh_pointage where presence = " + pe.Id + " order by heure_entree desc limit 1");
                                    if (lo != null ? lo.Count > 0 : false)
                                    {
                                        Pointage po = lo[0];
                                        if ((po != null) ? po.Id != 0 : false)
                                        {
                                            if ((po.HeureSortie != null) ? po.HeureSortie.ToString() == "01/01/0001 00:00:00" : true)
                                            {
                                                p_.Id = pe.Id;
                                                p_.DateDebut = pe.DateDebut;
                                                p_.DateFin = pe.DateFin;
                                                p_.HeureDebut = pe.HeureDebut;
                                                p_.HeureFin = pe.HeureFin;
                                                p_.Valide = pe.Valider;
                                                p_.DureePause = pe.DureePause;
                                                p_.Supplementaire = po.Supplementaire;
                                                deja = true;
                                            }
                                        }
                                    }
                                }
                                if (!deja)
                                {
                                    string type = ((e.Contrat != null) ? e.Contrat.TypeTranche : "JN");
                                    string query = "select * from yvs_grh_tranche_horaire where upper(type_journee) = upper('" + type + "') order by heure_debut asc, type_journee";
                                    TrancheHoraire t = GetTrancheHoraire(heure_, query);
                                    if (t != null ? t.Id < 1 : true)
                                    {
                                        query = "select * from yvs_grh_tranche_horaire order by heure_debut asc, type_journee";
                                        t = GetTrancheHoraire(heure_, query);
                                    }
                                    if (t != null ? t.Id > 0 : false)
                                    {
                                        p_.Id = t.Id;
                                        p_.DateDebut = new DateTime(heure_.Year, heure_.Month, heure_.Day, t.HeureDebut.Hour, t.HeureDebut.Minute, 0); ;
                                        p_.DateFin = Utils.GetTimeStamp(p_.DateDebut, t.HeureFin); ;
                                        p_.HeureDebut = t.HeureDebut;
                                        p_.HeureFin = t.HeureFin;
                                        p_.DureePause = t.DureePause;
                                        p_.Valide = false;
                                        JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(CalendrierBLL.Default(), Utils.jourSemaine(p_.DateDebut));
                                        if (jour != null ? jour.Id > 0 : false)
                                        {
                                            p_.Supplementaire = !jour.Ouvrable;
                                        }
                                    }
                                    else
                                    {
                                        p_.Id = 1;
                                        p_.DateDebut = p_.DateFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 0, 0, 0);
                                        p_.HeureDebut = new DateTime(heure_.Year, heure_.Month, heure_.Day, 7, 30, 0);
                                        p_.HeureFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 17, 00, 0);
                                        p_.DureePause = new DateTime(heure_.Year, heure_.Month, heure_.Day, 1, 30, 0);
                                        p_.Valide = false;
                                        JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(CalendrierBLL.Default(), Utils.jourSemaine(p_.DateDebut));
                                        if (jour != null ? jour.Id > 0 : false)
                                        {
                                            p_.Supplementaire = !jour.Ouvrable;
                                        }
                                        Utils.WriteLog("L'employé " + e.Nom + " (Dynamique-'D') n'a pas été programmé  à la date (" + heure_.ToShortDateString() + ")....");
                                    }
                                }
                            }
                            else
                            {
                                p_.Id = 1;
                                p_.DateDebut = p_.DateFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 0, 0, 0);
                                p_.HeureDebut = new DateTime(heure_.Year, heure_.Month, heure_.Day, 7, 30, 0);
                                p_.HeureFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 17, 00, 0);
                                p_.DureePause = new DateTime(heure_.Year, heure_.Month, heure_.Day, 1, 30, 0);
                                p_.Valide = false;
                                JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(CalendrierBLL.Default(), Utils.jourSemaine(p_.DateDebut));
                                if (jour != null ? jour.Id > 0 : false)
                                {
                                    p_.Supplementaire = !jour.Ouvrable;
                                }
                                Utils.WriteLog("L'employé " + e.Nom + " (Dynamique) n'a pas été programmé  à la date (" + heure_.ToShortDateString() + ")....");
                            }
                        }
                        // On verifi si le jour est un jour ouvrable
                        if ((p_ != null) ? p_.Id > 0 : false)
                        {
                            bool c = false;
                            if ((e.Contrat != null) ? e.Contrat.Id != 0 : false)
                            {
                                if ((e.Contrat.Calendrier != null) ? e.Contrat.Calendrier.Id != 0 : false)
                                {
                                    c = true;
                                    JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(e.Contrat.Calendrier, Utils.jourSemaine(p_.DateDebut));
                                    if (jour != null ? jour.Id > 0 : false)
                                    {
                                        p_.Supplementaire = !jour.Ouvrable;
                                    }
                                }
                            }
                            if (!c)
                            {
                                JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(CalendrierBLL.Default(), Utils.jourSemaine(p_.DateDebut));
                                if (jour != null ? jour.Id > 0 : false)
                                {
                                    p_.Supplementaire = !jour.Ouvrable;
                                }
                            }
                        }
                    }
                    else
                    {
                        if ((e.Contrat != null) ? e.Contrat.Id != 0 : false)
                        {
                            if ((e.Contrat.Calendrier != null) ? e.Contrat.Calendrier.Id != 0 : false)
                            {
                                JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(e.Contrat.Calendrier, Utils.jourSemaine(heure_));
                                if (jour != null ? jour.Id > 0 : false)
                                {
                                    p_ = PlanningBLL.getPlanningForJoursOuvres(jour, heure_);
                                }
                            }

                            if ((p_ != null) ? p_.Id < 1 : true)
                            {
                                p_.Id = 1;
                                p_.DateDebut = p_.DateFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 0, 0, 0);
                                p_.HeureDebut = new DateTime(heure_.Year, heure_.Month, heure_.Day, 7, 30, 0);
                                p_.HeureFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 17, 00, 0);
                                p_.Valide = false;
                                JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(CalendrierBLL.Default(), Utils.jourSemaine(p_.DateDebut));
                                if (jour != null ? jour.Id > 0 : false)
                                {
                                    p_.Supplementaire = !jour.Ouvrable;
                                }
                                p_.DureePause = new DateTime(heure_.Year, heure_.Month, heure_.Day, 1, 30, 0);
                                Utils.WriteLog("L'employé " + e.Nom + " (Statique) n'a pas été programmé  à la date (" + heure_.ToShortDateString() + ")....");

                            }
                        }
                        else
                        {
                            p_.Id = 1;
                            p_.DateDebut = p_.DateFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 0, 0, 0);
                            p_.HeureDebut = new DateTime(heure_.Year, heure_.Month, heure_.Day, 7, 30, 0);
                            p_.HeureFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 17, 00, 0);
                            p_.Valide = false;
                            JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(CalendrierBLL.Default(), Utils.jourSemaine(p_.DateDebut));
                            if (jour != null ? jour.Id > 0 : false)
                            {
                                p_.Supplementaire = !jour.Ouvrable;
                            }
                            p_.DureePause = new DateTime(heure_.Year, heure_.Month, heure_.Day, 1, 30, 0);
                            Utils.WriteLog("L'employé " + e.Nom + " n'a pas de contrat !");
                        }
                    }
                }
                else
                {
                    p_.Id = 1;
                    p_.DateDebut = p_.DateFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 0, 0, 0);
                    p_.HeureDebut = new DateTime(heure_.Year, heure_.Month, heure_.Day, 7, 30, 0);
                    p_.HeureFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 17, 00, 0);
                    p_.Valide = false;
                    JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(CalendrierBLL.Default(), Utils.jourSemaine(p_.DateDebut));
                    if (jour != null ? jour.Id > 0 : false)
                    {
                        p_.Supplementaire = !jour.Ouvrable;
                    }
                    p_.DureePause = new DateTime(heure_.Year, heure_.Month, heure_.Day, 1, 30, 0);
                    Utils.WriteLog("Employé Inconnu !");
                }
                p_.HeureDebut = new DateTime(p_.DateDebut.Year, p_.DateDebut.Month, p_.DateDebut.Day, p_.HeureDebut.Hour, p_.HeureDebut.Minute, 0);
                p_.HeureFin = new DateTime(p_.DateFin.Year, p_.DateFin.Month, p_.DateFin.Day, p_.HeureFin.Hour, p_.HeureFin.Minute, 0);
                return p_;
            }
            catch (Exception ex)
            {
                Messages.Exception("RegEvent (GetPlanning)", ex);
                return null;
            }
        }

        public static Presence GetPresence(Employe e, Planning p, DateTime heure_)
        {
            try
            {
                if ((e != null) ? e.Id > 0 : false)
                {
                    if (p != null ? p.Id > 0 : false)
                    {
                        //List<Presence> lr = PresenceBLL.List("select * from yvs_grh_presence where date_debut = '" + p.DateDebut.ToString("dd-MM-yyyy") + "' and date_fin = '" + p.DateFin.ToString("dd-MM-yyyy") + "' and heure_debut = '" + p.HeureDebut.ToString("HH:mm:ss") + "' and heure_fin = '" + p.HeureFin.ToString("HH:mm:ss") + "' and employe = " + e.Id + " order by heure_debut desc");
                        List<Presence> lr = PresenceBLL.List("select * from yvs_grh_presence where date_debut = '" + p.DateDebut.ToString("dd-MM-yyyy") + "' and date_fin = '" + p.DateFin.ToString("dd-MM-yyyy") + "' and employe = " + e.Id + " order by heure_debut desc");
                        if (lr != null ? lr.Count > 0 : false)
                        {
                            Presence p_ = lr[0];
                            p_.HeureDebut = new DateTime(p_.DateDebut.Year, p_.DateDebut.Month, p_.DateDebut.Day, p_.HeureDebut.Hour, p_.HeureDebut.Minute, 0);
                            p_.HeureFin = new DateTime(p_.DateFin.Year, p_.DateFin.Month, p_.DateFin.Day, p_.HeureFin.Hour, p_.HeureFin.Minute, 0);

                            bool c = false;
                            if ((e.Contrat != null) ? e.Contrat.Id != 0 : false)
                            {
                                if ((e.Contrat.Calendrier != null) ? e.Contrat.Calendrier.Id != 0 : false)
                                {
                                    c = true;
                                    JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(e.Contrat.Calendrier, Utils.jourSemaine(p.DateDebut));
                                    if (jour != null ? jour.Id > 0 : false)
                                    {
                                        p_.Supplementaire = !jour.Ouvrable;
                                    }
                                }
                            }
                            if (!c)
                            {
                                JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(CalendrierBLL.Default(), Utils.jourSemaine(p.DateDebut));
                                if (jour != null ? jour.Id > 0 : false)
                                {
                                    p_.Supplementaire = !jour.Ouvrable;
                                }
                            }
                            return p_;
                        }
                    }
                }
                else
                {
                    Utils.WriteLog("Employé Inconnu !");

                }
                return null;
            }
            catch (Exception ex)
            {
                Messages.Exception("RegEvent (GetPresence)", ex);
                return null;
            }
        }

        public static void DefaultLCD(bool bIsConnected)
        {
            new Appareil().ClearLCD(bIsConnected);
            new Appareil().WriteLCD(bIsConnected, 0, 0, "Welcome");
            new Appareil().WriteLCD(bIsConnected, 1, 3, DateTime.Now.ToShortTimeString());
            new Appareil().WriteLCD(bIsConnected, 3, 0, DateTime.Now.ToString("yy-MM-d"));
            new Appareil().WriteLCD(bIsConnected, 3, 12, DateTime.Now.ToString("ddd").ToUpper());
        }

        public static void SynchroniseTmpOneServeur(Pointeuse p, List<Empreinte> le)
        {
            try
            {
                Appareil z = Utils.ReturnAppareil(p);
                Utils.VerifyZkemkeeper(ref z, ref p);
                if (z == null)
                {
                    Utils.WriteLog("La liaison avec l'appareil " + p.Ip + " est corrompue");
                    return;
                }
                p.Zkemkeeper = z;
                int i = z.SetAllTemplate(le, p.IMachine, p.Connecter);
                if (i > 0)
                {
                    Utils.WriteLog("------ Total synchonisé : " + i.ToString() + " empreinte(s)");
                }
            }
            catch (Exception ex)
            {
                Utils.WriteLog("RegEvent (SynchroniseTmpOneServeur_) : " + ex.Message);
            }
        }

        public static void SynchroniseLogOneServeur(Pointeuse p)
        {
            _pointeuse = p;
            Thread t = new Thread(new ThreadStart(SynchroniseLogOneServeur_));
            t.Start();
        }

        public static void SynchroniseLogOneServeur_()
        {
            try
            {
                Appareil z = Utils.ReturnAppareil(_pointeuse);
                Utils.VerifyZkemkeeper(ref z, ref _pointeuse);
                if (z == null)
                {
                    Utils.WriteLog("La liaison avec l'appareil " + _pointeuse.Ip + " est corrompue");
                    return;
                }
                List<IOEMDevice> l = z.GetAllAttentdData(1, _pointeuse.Connecter);
                SynchroniseServer(l, _pointeuse.Ip, true, z);
            }
            catch (Exception ex)
            {
                Utils.WriteLog("RegEvent (SynchroniseLogOneServeur_) : " + ex.Message);
            }
        }

        public static void SynchroniseLogServeur()
        {
            Thread t = new Thread(new ThreadStart(SynchroniseLogServeur_));
            t.Start();
        }

        public static void SynchroniseLogServeur_()
        {
            try
            {
                Societe s = SocieteBLL.ReturnSociete();
                List<Pointeuse> pointeuses = PointeuseBLL.List("select * from yvs_pointeuse where societe = " + s.Id + " and actif = true order by adresse_ip");
                foreach (ENTITE.Pointeuse p in pointeuses)
                {
                    Appareil z = Utils.ReturnAppareil(p);
                    Pointeuse p_ = p;
                    Utils.VerifyZkemkeeper(ref z, ref p_);
                    if (z != null)
                    {
                        List<IOEMDevice> l = z.GetAllAttentdData(p_.IMachine, true);
                        SynchroniseServer(l, p_.Ip, true, z);
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.WriteLog("RegEvent (SynchroniseLogServeur) : " + ex.Message);
            }
        }

        public static bool StartAllDeviceDisconnect()
        {
            try
            {
                List<ENTITE.Pointeuse> pointeuses = PointeuseBLL.List("select * from yvs_pointeuse where connecter = false and actif = true");
                foreach (ENTITE.Pointeuse p in pointeuses)
                {
                    new Appareil().StartOne(p.Id, p.Ip);
                }
                return true;
            }
            catch (Exception ex)
            {
                // log errors
                Messages.Exception("RegEvent (StartAllDeviceDisconnect) ", ex);
                return false;
            }
        }

        public static int StartAllDevice()
        {
            try
            {
                Societe s = SocieteBLL.ReturnSociete();
                int result = 0;
                List<Pointeuse> l = PointeuseBLL.List("select * from yvs_pointeuse where societe = " + s.Id + " and actif = true order by adresse_ip");
                foreach (ENTITE.Pointeuse p in l)
                {
                    Appareil z = Utils.ReturnAppareil(p);
                    Pointeuse p_ = p;
                    Utils.VerifyZkemkeeper(ref z, ref p_);
                    if (z != null)
                    {
                        if (z.StartOneDirect())
                        {
                            ++result;
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                // log errors
                Messages.Exception("RegEvent (StartAllDevice) ", ex);
                return 0;
            }
        }

        public static bool StartDevices()
        {
            if (StartAllDevice() > 0)
            {
                Utils.WriteLog("Application deploie avec tous les appareils connectes.....");
                return true;
            }
            else
            {
                Utils.WriteLog("Aucun appareil n'est connecté.....");
                return false;
            }
        }

        public static void SynchroniseServer(List<IOEMDevice> lp, String ip, bool auto, Appareil z)
        {
            if (lp.Count > 0 && ip.Length > 0)
            {
                bool synchro = auto;
                Utils.WriteLog("-- Début de la synchronisation des données de pointage de la pointeuse " + ip + " avec le serveur.....");

                List<IOEMDevice> ls = Logs.ReadCsv();
                int cpt = 0;

                ObjectThread o = new ObjectThread(Constantes.PBAR_WAIT);
                o.UpdateMaxBar(Constantes.PBAR_WAIT.Maximum + lp.Count);

                foreach (IOEMDevice p in lp)
                {
                    bool deja = false;
                    foreach (IOEMDevice s in ls)
                    {
                        if (s.idwSEnrollNumber == p.idwSEnrollNumber && s.idwYear == p.idwYear && s.idwMonth == p.idwMonth && s.idwDay == p.idwDay && s.idwHour == p.idwHour && s.idwMinute == p.idwMinute && s.idwSecond == p.idwSecond)
                        {
                            deja = true;
                            break;
                        }
                    }
                    if (!deja)
                    {
                        Employe employe = EmployeBLL.OneById(Convert.ToInt32(p.idwSEnrollNumber));
                        if (employe != null ? employe.Id > 0 : false)
                        {
                            DateTime heure = new DateTime(p.idwYear, p.idwMonth, p.idwDay, p.idwHour, p.idwMinute, p.idwSecond);
                            DateTime date = heure;
                            if (Fonctions.InsertionPointage((Employe)employe, date, heure, (z != null ? z._POINTEUSE : null)))
                            {
                                Logs.WriteCsv(p);
                                cpt++;
                            }
                        }
                    }
                    Constantes.LoadPatience(false);
                }
                if (!auto)
                {
                    BackupLogData(lp, ip, auto, z);
                }
                else
                {
                    Constantes.LoadPatience(true);
                }
                Utils.WriteLog("-- Fin de la synchronisation des données de pointage de la pointeuse " + ip + " avec le serveur. Nombre de synchronisation " + cpt + "....");
            }
            else
            {
                Utils.WriteLog("-- Synchronisation impossible...paramètres incorrects");

            }
        }

        public static void SynchroniseServer(List<IOEMDevice> lp, String ip, bool auto, Appareil z, Employe e, bool date_, DateTime d, DateTime f, bool invalid)
        {
            bool synchro = auto;
            if (lp.Count > 0 && ip.Length > 0)
            {
                string query = "";
                if (e != null ? e.Id > 0 : false)
                {
                    if (date_)
                    {
                        if (!invalid)
                            query = "delete from yvs_grh_presence where employe = " + e.Id + " and date_debut between '" + d.ToShortDateString() + "' and '" + f.ToShortDateString() + "' and date_fin between '" + d.ToShortDateString() + "' and '" + f.ToShortDateString() + "'";
                        else
                            query = "";
                    }
                    else
                    {
                        if (!invalid)
                            query = "delete from yvs_grh_presence where employe = " + e.Id + "";
                        else
                            query = "delete from yvs_grh_presence where employe = " + e.Id + " and valider = false";
                    }
                }
                else
                {
                    if (date_)
                    {
                        if (!invalid)
                            query = "delete from yvs_grh_presence where employe in (select e.id from yvs_grh_employes e inner join yvs_agences a on e.agence = a.id where a.societe = " + Constantes.SOCIETE.Id + ") and date_debut between '" + d.ToShortDateString() + "' and '" + f.ToShortDateString() + "' and date_fin between '" + d.ToShortDateString() + "' and '" + f.ToShortDateString() + "'";
                        else
                            query = "delete from yvs_grh_presence where employe in (select e.id from yvs_grh_employes e inner join yvs_agences a on e.agence = a.id where a.societe = " + Constantes.SOCIETE.Id + ") and date_debut between '" + d.ToShortDateString() + "' and '" + f.ToShortDateString() + "' and date_fin between '" + d.ToShortDateString() + "' and '" + f.ToShortDateString() + "' and valider = false";
                    }
                    else
                    {
                        if (!invalid)
                            query = "delete from yvs_grh_presence where employe in (select e.id from yvs_grh_employes e inner join yvs_agences a on e.agence = a.id where a.societe = " + Constantes.SOCIETE.Id + ")";
                        else
                            query = "delete from yvs_grh_presence where employe in (select e.id from yvs_grh_employes e inner join yvs_agences a on e.agence = a.id where a.societe = " + Constantes.SOCIETE.Id + ") and valider = false";
                    }
                }
                if (Utils.RequeteLibre(query))
                {
                    Utils.WriteLog("-- Début de la synchronisation des données de pointage de la pointeuse " + ip + " avec le serveur.....");
                    List<IOEMDevice> ls = Logs.ReadCsv();
                    int cpt = 0;

                    ObjectThread o = new ObjectThread(Constantes.PBAR_WAIT);
                    o.UpdateMaxBar(Constantes.PBAR_WAIT.Maximum + lp.Count);

                    foreach (IOEMDevice p in lp)
                    {
                        Employe employe = EmployeBLL.OneById(Convert.ToInt32(p.idwSEnrollNumber));
                        if (employe != null ? employe.Id > 0 : false)
                        {
                            DateTime heure = new DateTime(p.idwYear, p.idwMonth, p.idwDay, p.idwHour, p.idwMinute, p.idwSecond);
                            DateTime date = heure;
                            if (Fonctions.InsertionPointage((Employe)employe, date, heure, (z != null ? z._POINTEUSE : null)))
                            {
                                cpt++;
                            }
                        }

                        bool deja = false;
                        foreach (IOEMDevice s in ls)
                        {
                            if (s.idwSEnrollNumber == p.idwSEnrollNumber && s.idwYear == p.idwYear && s.idwMonth == p.idwMonth && s.idwDay == p.idwDay && s.idwHour == p.idwHour && s.idwMinute == p.idwMinute && s.idwSecond == p.idwSecond)
                            {
                                deja = true;
                                break;
                            }
                        }
                        if (!deja)
                        {
                            Logs.WriteCsv(p);
                        }
                        Constantes.LoadPatience(false);
                    }
                    if (!auto)
                    {
                        BackupLogData(lp, ip, auto, z);
                    }
                    else
                    {
                        Constantes.LoadPatience(true);
                    }
                    Utils.WriteLog("-- Fin de la synchronisation des données de pointage de la pointeuse " + ip + " avec le serveur. Nombre de synchronisation " + cpt + "....");
                }
            }
            else
            {
                Utils.WriteLog("-- Synchronisation impossible...paramètres incorrects");

            }
        }

        public static List<IOEMDevice> _ReorganiseLog(List<IOEMDevice> lp, DateTime dateDebut, DateTime dateFin)
        {
            //List<DateTime> data = Utils.TimeEmployeNotSystem(
            List<IOEMDevice> _lp = new List<IOEMDevice>();
            foreach (IOEMDevice io in lp)
            {
                Employe employe = EmployeBLL.OneById(Convert.ToInt32(io.idwSEnrollNumber));
                if (employe != null ? employe.Id > 0 : false)
                {
                    DateTime heure = new DateTime(io.idwYear, io.idwMonth, io.idwDay, io.idwHour, io.idwMinute, 0);
                    string req = "select p.* from yvs_grh_pointage p inner join yvs_grh_presence r on p.presence = r.id where r.employe = " + employe.Id + " and ((heure_entree is not null and heure_entree = '" + heure + "') or (heure_sortie is not null and heure_sortie = '" + heure + "'))";
                    List<Pointage> lo = PointageBLL.List(req);
                    if (lo != null ? lo.Count < 1 : true)
                    {
                        req = "select p.* from yvs_grh_pointage p inner join yvs_grh_presence r on p.presence = r.id where r.employe = " + employe.Id + " and ((heure_entree is not null and heure_entree = '" + heure + "') or (heure_sortie is not null and heure_sortie = '" + heure + "'))";
                        lo = PointageBLL.List(req);
                    }
                }
            }
            return _lp;
        }

        public static void BackupLogDataDevice()
        {
            List<Pointeuse> l = PointeuseBLL.List("select * from yvs_pointeuse where actif = true");
            if (l != null ? l.Count > 0 : false)
            {
                Utils.WriteLog("Début de la sauvegarder automatique");
                foreach (Pointeuse p_ in l)
                {
                    Pointeuse p = p_;

                    Appareil z = Utils.ReturnAppareil(p_);
                    Utils.VerifyZkemkeeper(ref z, ref p);
                    if (z != null)
                    {
                        p.Zkemkeeper = z;
                        List<IOEMDevice> le = z.GetAllAttentdData(p.IMachine, p.Connecter);
                        BackupLogData(le, p.Ip, true, z);
                    }
                }
                Utils.WriteLog("Fin de la sauvegarder automatique");
            }
        }

        public static void BackupLogData(List<IOEMDevice> lp, String ip, bool auto, Appareil z)
        {
            if (lp.Count > 0)
            {
                ObjectThread o = new ObjectThread(Constantes.PBAR_WAIT);
                o.UpdateMaxBar(Constantes.PBAR_WAIT.Maximum + lp.Count);

                Utils.WriteLog((auto ? "--" : "") + "Début de la sauvegarde des opérations enrégistrées dans la pointeuse " + ip + " .....");
                for (int i = 0; i < lp.Count; i++)
                {
                    Logs.WriteCsv(TOOLS.Chemins.getCheminBackup(ip) + DateTime.Now.ToString("dd-MM-yyyy") + ".csv", lp[i]);
                    Constantes.LoadPatience(false);
                }
                Utils.WriteLog((auto ? "--" : "") + "Fin de la sauvegarde des opérations enrégistrées dans la pointeuse " + ip + " .....");
            }
            Setting s = SettingBLL.ReturnSetting();
            if (s.AutoClearAndBackup)
            {
                if (z != null)
                {
                    //z.ClearGLog();
                }
            }
            Constantes.LoadPatience(true);
        }

        public static bool StartSave(Appareil z, string ip)
        {
            bool bEnabled = true, correct_ = false;
            int flag, lon;
            string tmp;
            Finger f = new Finger();

            if (z.StartIdentify())//After enrolling templates,you should let the device into the 1:N verification condition
            {
                if (z.EnableDevice(z._I_MACHINE_NUMBER, bEnabled))
                {
                    Cursor c = Cursors.WaitCursor;
                    if (z.ReadAllTemplate(z._I_MACHINE_NUMBER))
                    {
                        if (z.SetUserInfo(z._I_MACHINE_NUMBER, (int)z._EMPLOYE.Id, z._EMPLOYE.NomPrenom, null, 0, bEnabled))//upload user information to the memory
                        {
                            if (z.GetUserTmpExStr(z._I_MACHINE_NUMBER, z._EMPLOYE.Id.ToString(), z._FINGER.Index, out flag, out tmp, out lon))
                            {
                                if ((tmp != null ? tmp.Trim() != "" : false) && (lon > 0))
                                {
                                    if (EmpreinteBLL.Insert(new Empreinte((Employe)z._EMPLOYE, z._FINGER.Index, flag, tmp, lon)))
                                    {
                                        correct_ = true;

                                        Setting s = SettingBLL.ReturnSetting();
                                        if (s.AddEnrollAuto)
                                        {
                                            CloneTemplateInOthers(ip, z._EMPLOYE, z._FINGER, flag, tmp, lon);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    z.RefreshData(z._I_MACHINE_NUMBER);//the data in the device should be refreshed
                    z.EnableDevice(z._I_MACHINE_NUMBER, true);
                    c = Cursors.Default;
                }
            }
            if (!correct_)
            {
                Utils.WriteLog("-- Empreinte du Doigt (" + f.Doigt + ") de la Constantes.MAIN (" + f.Main + ") de l'employé " + z._EMPLOYE.Nom + " " + z._EMPLOYE.Prenom + " incomplète!");
            }
            else
            {
                Utils.WriteLog("-- Empreinte du Doigt (" + f.Doigt + ") de la Constantes.MAIN (" + f.Main + ") de l'employé " + z._EMPLOYE.Nom + " " + z._EMPLOYE.Prenom + " sauvegardé!");
            }
            return correct_;
        }

        public static void CloneTemplateInOthers(string ip, Employe e, Finger f, int flag, string tmp, int lon)
        {
            Thread thread = new Thread(delegate() { _CloneTemplateInOthers(ip, e, f, flag, tmp, lon); });
            thread.Start();
        }

        public static void _CloneTemplateInOthers(string _ip, Employe e, Finger f, int flag, string tmp, int lon)
        {
            string query = "select * from yvs_pointeuse where adresse_ip != '" + _ip.Trim() + "' and actif = true";
            List<Pointeuse> pointeuses = PointeuseBLL.List(query);
            if (pointeuses.Count > 0)
            {
                Utils.WriteLog("Synchronisation du (" + f.Doigt + ") de la Constantes.MAIN(" + f.Main + ") de l'employé " + e.NomPrenom + " .....!");
                foreach (Pointeuse p in pointeuses)
                {
                    CloneTemplateInOther(p, new Empreinte((Employe)e, f.Index, flag, tmp, lon));
                }
            }
        }

        public static void CloneTemplateInOther(Pointeuse p, Empreinte e)
        {
            if (p != null ? p.Id > 0 : false)
            {
                Appareil z = Utils.ReturnAppareil(p);
                Utils.VerifyZkemkeeper(ref z, ref p);
                if (z != null)
                {
                    List<Empreinte> l = new List<Empreinte>();
                    l.Add(e);
                    z.SetAllTemplate(l, z._I_MACHINE_NUMBER, p.Connecter);
                }
            }
        }

    }
}
