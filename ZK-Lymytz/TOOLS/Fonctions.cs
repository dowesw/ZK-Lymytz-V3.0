using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;

using Microsoft.Win32;

using ZK_Lymytz.IHM;
using ZK_Lymytz.BLL;
using ZK_Lymytz.ENTITE;

namespace ZK_Lymytz.TOOLS
{
    public class Fonctions
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
        public static Employe _employe = new Employe();
        public static bool _invalid_only = true;
        public static Pointeuse _pointeuse = new Pointeuse();
        public static List<Pointeuse> _pointeuses = new List<Pointeuse>();
        public static DateTime _dateDebut, _dateFin;

        public Fonctions() { }

        public Fonctions(Pointeuse p)
        {
            _pointeuse = p;
        }

        // Construit la fiche de présence en fonction du planning et de l'employé
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

        public static Pointage Pointage_(Presence pe, Object heureEntree, Object heureSortie, Pointeuse pointeuse, bool systemIn, bool systemOut)
        {
            return Pointage_(pe, 0, heureEntree, heureSortie, false, pointeuse, systemIn, systemOut);
        }

        public static Pointage Pointage_(Presence pe, Object heureEntree, Object heureSortie, bool valider, Pointeuse pointeuse, bool systemIn, bool systemOut)
        {
            return Pointage_(pe, 0, heureEntree, heureSortie, valider, pointeuse, systemIn, systemOut);
        }

        public static Pointage Pointage_(Presence pe, long id, Object heureEntree, Object heureSortie, bool valider, Pointeuse pointeuse, bool systemIn, bool systemOut)
        {
            Pointage bean = new Pointage();
            bean.Id = id;
            bean.Presence = pe;
            if (heureEntree != null)
                bean.HeureEntree = Convert.ToDateTime(heureEntree);
            if (heureSortie != null)
                bean.HeureSortie = Convert.ToDateTime(heureSortie);
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
                    bool have_supplementaire_ = false;
                    Presence presence = GetPresence(employe, current_time, false);
                    if (presence != null ? presence.Id < 1 : true)//Si elle n'existe pas
                    {
                        have_supplementaire_ = true;
                        //On recherche le planning en fonction de l'heure courante
                        Planning planning = GetPlanning((Employe)employe, current_time);
                        //On recherche la fiche de présence en fonction du planning
                        presence = GetPresence((Employe)employe, (Planning)planning);
                        if ((presence != null) ? presence.Id < 1 : true)
                        {
                            if (PresenceBLL.Insert(Presence_(planning, employe)))
                            {
                                List<Presence> lr = PresenceBLL.List("select * from yvs_grh_presence where date_debut = '" + planning.DateDebut.ToString("dd-MM-yyyy") + "' and date_fin = '" + planning.DateFin.ToString("dd-MM-yyyy") + "' and heure_debut = '" + planning.HeureDebut.ToString("HH:mm:ss") + "' and heure_fin = '" + planning.HeureFin.ToString("HH:mm:ss") + "' and employe = " + employe.Id + " order by heure_debut desc");
                                if (lr != null ? lr.Count > 0 : false)
                                {
                                    presence = lr[0];
                                    have_supplementaire_ = false;
                                }
                            }
                        }
                    }
                    if (presence != null ? presence.Id < 1 : true)
                    {
                        OnSavePointage(employe, current_time, current_date, pointeuse);
                        return false;
                    }
                    else
                    {
                        if (!have_supplementaire_)
                        {
                            if ((employe.Contrat != null) ? (employe.Contrat.Id != 0 ? ((employe.Contrat.Calendrier != null) ? employe.Contrat.Calendrier.Id != 0 : false) : false) : false)
                            {
                                JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(employe.Contrat.Calendrier, Utils.jourSemaine(presence.DateDebut));
                                if (jour != null ? jour.Id > 0 : false)
                                {
                                    presence.Supplementaire = !jour.Ouvrable;
                                }
                            }
                            else
                            {
                                JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(CalendrierBLL.Default(), Utils.jourSemaine(presence.DateDebut));
                                if (jour != null ? jour.Id > 0 : false)
                                {
                                    presence.Supplementaire = !jour.Ouvrable;
                                }
                            }
                        }
                    }
                    if (!presence.Valider)
                        return OnSavePointage(presence, current_time, pointeuse);
                }
                else
                {
                    Utils.WriteLog("Employé Inconnu !");
                }
                return false;
            }
            catch (Exception ex)
            {
                Messages.Exception("Fonctions (OnSavePointage)", ex);
                return false;
            }
        }

        public static bool OnSavePointage(Presence presence, DateTime current_time, Pointeuse pointeuse)
        {
            try
            {
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
                return false;
            }
            catch (Exception ex)
            {
                Messages.Exception("Fonctions (OnSavePointage)", ex);
                return false;
            }
        }

        public static bool OnSavePointage(string mouv, Pointage po, Presence pe, DateTime current_time, Pointeuse pointeuse)
        {
            switch (mouv)
            {
                case "S":
                    {
                        if (po != null ? po.Id > 0 : false)
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
                        }
                        else
                        {
                            //On verifi si l'heure actuelle est superieur a l'heure de sortie prevu
                            if (current_time > pe.HeureFin)//Si l'heure actuelle est superieur a l'heure de sortie prevu
                            {
                                //On Complete la sortie du dernier pointage par l'heure de sortie prevu
                                if (PointageBLL.InsertU(Pointage_(pe, null, pe.HeureFin, false, pointeuse, false, true)))
                                {
                                    //On insert un pointage supplementaire qui va de l'heure de sortie prevu a l'heure actuelle
                                    return PointageBLL.InsertU(Pointage_(pe, pe.HeureFin, current_time, false, pointeuse, true, false));
                                }
                            }
                            else
                            {
                                //On Complete la sortie du dernier pointage par l'heure actuelle
                                return PointageBLL.InsertU(Pointage_(pe, null, current_time, false, pointeuse, false, false));
                            }
                        }
                        break;
                    }
                case "E":
                    {
                        //On insert une entrée
                        DateTime h_ = Utils.AddTimeInDate(pe.HeureDebut, Constantes.PARAMETRE.TimeMargeAutorise);
                        if (pe.HeureDebut < current_time && current_time < h_)
                        {
                            Presence p_ = new Presence();
                            p_.Id = pe.Id;
                            p_.MargeApprouve = Constantes.PARAMETRE.TimeMargeAutorise;
                            //PresenceBLL.Update(p_);
                        }
                        else
                        {
                            Presence p_ = new Presence();
                            p_.Id = pe.Id;
                            p_.MargeApprouve = new DateTime(pe.DateFin.Year, pe.DateFin.Month, pe.DateFin.Day, 0, 0, 0);
                            //PresenceBLL.Update(p_);
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
                            DateTime d = Utils.AddTimeInDate(heure_, Constantes.PARAMETRE.TimeMargeAvance);
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
                            DateTime d = Utils.AddTimeInDate(heure_, Constantes.PARAMETRE.TimeMargeAvance);
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

        public static TrancheHoraire GetTrancheHoraire(Employe e, DateTime heure_)
        {
            // On cherche la tranche horaire correspondante
            string type = ((e.Contrat != null) ? e.Contrat.TypeTranche : "JN");
            string query = "select * from yvs_grh_tranche_horaire where upper(type_journee) = upper('" + type + "') order by heure_debut asc, type_journee";
            TrancheHoraire t = GetTrancheHoraire(heure_, query);
            if (t != null ? t.Id < 1 : true)
            {
                query = "select * from yvs_grh_tranche_horaire order by heure_debut asc, type_journee";
                t = GetTrancheHoraire(heure_, query);
            }
            return t;
        }

        public static Planning GetSimplePlanning(Employe e, DateTime heure_)
        {
            try
            {
                Planning planning = new Planning();
                // On verifie si l'employé a un horaire dynamique
                if (e.HoraireDynamique) // Si oui
                {
                    // On recherche le planning de l'employé a la date
                    List<Planning> lp = PlanningBLL.List("select p.* from yvs_grh_planning_employe p inner join yvs_grh_tranche_horaire t on p.tranche = t.id where p.employe =" + e.Id + " and '" + heure_.ToString("dd-MM-yyyy") + "' between p.date_debut and p.date_fin order by p.date_debut, t.heure_debut");
                    if (lp.Count > 0)// Si il a un planning
                    {
                        if (lp.Count > 1)// Si il a plus d'un planning
                        {
                            int i = 0;
                            // On recherche le bon planning
                            foreach (Planning p in lp)
                            {
                                planning = p;

                                DateTime date_debut = new DateTime(p.DateDebut.Year, p.DateDebut.Month, p.DateDebut.Day, 0, 0, 0);
                                DateTime date_fin = new DateTime(p.DateFin.Year, p.DateFin.Month, p.DateFin.Day, 0, 0, 0);
                                DateTime heure_debut = new DateTime(date_debut.Year, date_debut.Month, date_debut.Day, p.HeureDebut.Hour, p.HeureDebut.Minute, 0);
                                DateTime heure_fin = new DateTime(date_fin.Year, date_fin.Month, date_fin.Day, p.HeureFin.Hour, p.HeureFin.Minute, 0);

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
                                    // On verifie s'il a une fiche de présence a ce planning
                                    List<Presence> lr = PresenceBLL.List("select * from yvs_grh_presence where date_debut = '" + date_debut + "' and employe = " + e.Id + " order by date_debut desc, heure_debut desc");
                                    if (lr != null ? lr.Count > 0 : false)
                                    {
                                        Presence pe = lr[0];
                                        // On verifie s'il le dernier pointage de la fiche est une entrée
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
                            // On verifie si l'heure est dans le bon interval
                            if (planning != null ? planning.Id > 0 : false)
                            {
                                DateTime heure_fin = new DateTime(planning.DateFin.Year, planning.DateFin.Month, planning.DateFin.Day, planning.HeureFin.Hour, planning.HeureFin.Minute, 0);
                                heure_fin = Utils.AddTimeInDate(heure_fin, Constantes.PARAMETRE.TimeMargeAvance);
                                if (heure_ > heure_fin)
                                {
                                    List<Pointage> lo = PointageBLL.List("select * from yvs_grh_pointage where presence = " + planning.Id + " order by heure_entree desc limit 1");
                                    if (lo != null ? lo.Count > 0 : false)
                                    {
                                        Pointage po = lo[0];
                                        if ((po != null) ? po.Id != 0 : false)
                                        {
                                            // Le dernier pointage est une sortie
                                            if ((po.HeureSortie != null) ? po.HeureSortie.ToString() != "01/01/0001 00:00:00" : false)
                                            {
                                                planning = null;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (Utils.VerifyDateHeure(lp[0], heure_))
                                planning = lp[0];
                        }
                    }
                    // On verifi si le jour est un jour ouvrable
                    if ((planning != null) ? planning.Id > 0 : false)
                    {
                        if ((e.Contrat != null) ? (e.Contrat.Id != 0 ? ((e.Contrat.Calendrier != null) ? e.Contrat.Calendrier.Id != 0 : false) : false) : false)
                        {
                            JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(e.Contrat.Calendrier, Utils.jourSemaine(planning.DateDebut));
                            if (jour != null ? jour.Id > 0 : false)
                            {
                                planning.Supplementaire = !jour.Ouvrable;
                            }
                        }
                        else
                        {
                            JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(CalendrierBLL.Default(), Utils.jourSemaine(planning.DateDebut));
                            if (jour != null ? jour.Id > 0 : false)
                            {
                                planning.Supplementaire = !jour.Ouvrable;
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
                            bool deja = false;
                            JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(e.Contrat.Calendrier, Utils.jourSemaine(heure_.AddDays(-1)));
                            if (jour != null ? jour.Id > 0 : false)
                            {
                                planning = PlanningBLL.getPlanningForJoursOuvres(jour, heure_.AddDays(-1));
                            }
                            if (planning != null ? planning.Id > 0 : false)
                            {
                                if (planning.Chevauche)
                                {
                                    DateTime d = new DateTime(planning.DateFin.Year, planning.DateFin.Month, planning.DateFin.Day, planning.HeureFin.Hour, planning.HeureFin.Minute, planning.HeureFin.Second);
                                    d = Utils.AddTimeInDate(d, Constantes.PARAMETRE.TimeMargeRetard);
                                    if (heure_ <= d)
                                    {
                                        deja = true;
                                    }
                                }
                            }
                            if (!deja)
                            {
                                planning = null;
                                jour = JoursOuvresBLL.OneByCalendrier(e.Contrat.Calendrier, Utils.jourSemaine(heure_));
                                if (jour != null ? jour.Id > 0 : false)
                                {
                                    planning = PlanningBLL.getPlanningForJoursOuvres(jour, heure_);
                                }
                            }
                        }
                    }
                }
                planning.HeureDebut = new DateTime(planning.DateDebut.Year, planning.DateDebut.Month, planning.DateDebut.Day, planning.HeureDebut.Hour, planning.HeureDebut.Minute, 0);
                planning.HeureFin = new DateTime(planning.DateFin.Year, planning.DateFin.Month, planning.DateFin.Day, planning.HeureFin.Hour, planning.HeureFin.Minute, 0);
                return planning;
            }
            catch (Exception ex)
            {
                Messages.Exception("Fonctions (GetSimplePlanning)", ex);
                return null;
            }
        }

        public static Planning GetPlanning(Employe e, DateTime heure_)
        {
            try
            {
                Planning planning = new Planning();
                if ((e != null) ? e.Id > 0 : false)
                {
                    planning = GetSimplePlanning(e, heure_);
                    if ((planning != null) ? planning.Id < 1 : true)
                    {
                        if (Constantes.PARAMETRE.PlanningDynamique)
                        {
                            // On cherche la tranche horaire correspondante
                            TrancheHoraire t = GetTrancheHoraire(e, heure_);
                            if (t != null ? t.Id > 0 : false)
                            {
                                planning.Id = t.Id;
                                planning.DateDebut = new DateTime(heure_.Year, heure_.Month, heure_.Day, t.HeureDebut.Hour, t.HeureDebut.Minute, 0); ;
                                planning.DateFin = Utils.GetTimeStamp(planning.DateDebut, t.HeureFin); ;
                                planning.HeureDebut = t.HeureDebut;
                                planning.HeureFin = t.HeureFin;
                                planning.DureePause = t.DureePause;
                                planning.Valide = false;
                            }
                            else
                            {
                                planning.Id = 1;
                                planning.DateDebut = planning.DateFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 0, 0, 0);
                                planning.HeureDebut = new DateTime(heure_.Year, heure_.Month, heure_.Day, 7, 30, 0);
                                planning.HeureFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 17, 00, 0);
                                planning.DureePause = new DateTime(heure_.Year, heure_.Month, heure_.Day, 1, 30, 0);
                                planning.Valide = false;
                                Utils.WriteLog("L'employé " + e.Nom + " (Dynamique) n'a pas été programmé  à la date (" + heure_.ToShortDateString() + ")....");
                            }
                        }
                        else
                        {
                            planning.Id = 1;
                            planning.DateDebut = planning.DateFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 0, 0, 0);
                            planning.HeureDebut = new DateTime(heure_.Year, heure_.Month, heure_.Day, 7, 30, 0);
                            planning.HeureFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 17, 00, 0);
                            planning.DureePause = new DateTime(heure_.Year, heure_.Month, heure_.Day, 1, 30, 0);
                            planning.Valide = false;
                            Utils.WriteLog("L'employé " + e.Nom + " (Dynamique [planning non dynamique]) n'a pas été programmé  à la date (" + heure_.ToShortDateString() + ")....");
                        }

                        // On verifie si l'employé a un horaire dynamique
                        if (e.HoraireDynamique) // Si oui
                        {
                            // On verifi si le jour est un jour ouvrable
                            if ((planning != null) ? planning.Id > 0 : false)
                            {
                                if ((e.Contrat != null) ? (e.Contrat.Id != 0 ? ((e.Contrat.Calendrier != null) ? e.Contrat.Calendrier.Id != 0 : false) : false) : false)
                                {
                                    JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(e.Contrat.Calendrier, Utils.jourSemaine(planning.DateDebut));
                                    if (jour != null ? jour.Id > 0 : false)
                                    {
                                        planning.Supplementaire = !jour.Ouvrable;
                                    }
                                }
                                else
                                {
                                    JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(CalendrierBLL.Default(), Utils.jourSemaine(planning.DateDebut));
                                    if (jour != null ? jour.Id > 0 : false)
                                    {
                                        planning.Supplementaire = !jour.Ouvrable;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if ((e.Contrat != null) ? e.Contrat.Id != 0 : false)
                            {
                                if ((planning != null) ? planning.Id < 1 : true)
                                {
                                    planning.Id = 1;
                                    planning.DateDebut = planning.DateFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 0, 0, 0);
                                    planning.HeureDebut = new DateTime(heure_.Year, heure_.Month, heure_.Day, 7, 30, 0);
                                    planning.HeureFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 17, 00, 0);
                                    planning.Valide = false;
                                    JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(CalendrierBLL.Default(), Utils.jourSemaine(planning.DateDebut));
                                    if (jour != null ? jour.Id > 0 : false)
                                    {
                                        planning.Supplementaire = !jour.Ouvrable;
                                    }
                                    planning.DureePause = new DateTime(heure_.Year, heure_.Month, heure_.Day, 1, 30, 0);
                                    Utils.WriteLog("L'employé " + e.Nom + " (Statique) n'a pas été programmé  à la date (" + heure_.ToShortDateString() + ")....");
                                }
                            }
                            else
                            {
                                planning.Id = 1;
                                planning.DateDebut = planning.DateFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 0, 0, 0);
                                planning.HeureDebut = new DateTime(heure_.Year, heure_.Month, heure_.Day, 7, 30, 0);
                                planning.HeureFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 17, 00, 0);
                                planning.Valide = false;
                                JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(CalendrierBLL.Default(), Utils.jourSemaine(planning.DateDebut));
                                if (jour != null ? jour.Id > 0 : false)
                                {
                                    planning.Supplementaire = !jour.Ouvrable;
                                }
                                planning.DureePause = new DateTime(heure_.Year, heure_.Month, heure_.Day, 1, 30, 0);
                                Utils.WriteLog("L'employé " + e.Nom + " n'a pas de contrat !");
                            }
                        }
                    }
                }
                else
                {
                    planning.Id = 1;
                    planning.DateDebut = planning.DateFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 0, 0, 0);
                    planning.HeureDebut = new DateTime(heure_.Year, heure_.Month, heure_.Day, 7, 30, 0);
                    planning.HeureFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 17, 00, 0);
                    planning.Valide = false;
                    JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(CalendrierBLL.Default(), Utils.jourSemaine(planning.DateDebut));
                    if (jour != null ? jour.Id > 0 : false)
                    {
                        planning.Supplementaire = !jour.Ouvrable;
                    }
                    planning.DureePause = new DateTime(heure_.Year, heure_.Month, heure_.Day, 1, 30, 0);
                    Utils.WriteLog("Employé Inconnu !");
                }
                planning.HeureDebut = new DateTime(planning.DateDebut.Year, planning.DateDebut.Month, planning.DateDebut.Day, planning.HeureDebut.Hour, planning.HeureDebut.Minute, 0);
                planning.HeureFin = new DateTime(planning.DateFin.Year, planning.DateFin.Month, planning.DateFin.Day, planning.HeureFin.Hour, planning.HeureFin.Minute, 0);
                return planning;
            }
            catch (Exception ex)
            {
                Messages.Exception("Fonctions (GetPlanning)", ex);
                return null;
            }
        }

        public static Presence GetPresence(Employe e, Planning p)
        {
            try
            {
                if ((e != null) ? e.Id > 0 : false)
                {
                    if (p != null ? p.Id > 0 : false)
                    {
                        //On recherche s'il une fiche de presence n'existe pas a la date début et la date fin du planning
                        List<Presence> lr = PresenceBLL.List("select * from yvs_grh_presence where date_debut = '" + p.DateDebut.ToString("dd-MM-yyyy") + "' and date_fin = '" + p.DateFin.ToString("dd-MM-yyyy") + "' and employe = " + e.Id + " order by heure_debut desc");
                        bool deja = false;
                        if (lr != null ? lr.Count > 0 : false)
                        {
                            deja = true;
                        }
                        else
                        {
                            //On recherche s'il une fiche de presence n'existe pas a la date début du planning
                            lr = PresenceBLL.List("select * from yvs_grh_presence where date_debut = '" + p.DateDebut.ToString("dd-MM-yyyy") + "' and employe = " + e.Id + " order by heure_debut desc");
                            if (lr != null ? lr.Count > 0 : false)
                            {
                                deja = true;
                            }
                        }
                        if (deja)
                        {
                            Presence presence = lr[0];
                            presence.HeureDebut = new DateTime(presence.DateDebut.Year, presence.DateDebut.Month, presence.DateDebut.Day, presence.HeureDebut.Hour, presence.HeureDebut.Minute, 0);
                            presence.HeureFin = new DateTime(presence.DateFin.Year, presence.DateFin.Month, presence.DateFin.Day, presence.HeureFin.Hour, presence.HeureFin.Minute, 0);

                            if ((e.Contrat != null) ? (e.Contrat.Id != 0 ? ((e.Contrat.Calendrier != null) ? e.Contrat.Calendrier.Id != 0 : false) : false) : false)
                            {
                                JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(e.Contrat.Calendrier, Utils.jourSemaine(p.DateDebut));
                                if (jour != null ? jour.Id > 0 : false)
                                {
                                    presence.Supplementaire = !jour.Ouvrable;
                                }
                            }
                            else
                            {
                                JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(CalendrierBLL.Default(), Utils.jourSemaine(p.DateDebut));
                                if (jour != null ? jour.Id > 0 : false)
                                {
                                    presence.Supplementaire = !jour.Ouvrable;
                                }
                            }
                            return presence;
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
                Messages.Exception("Fonctions (GetPresence)", ex);
                return null;
            }
        }

        public static Presence GetPresence(Employe e, DateTime current_time, bool search_only)
        {
            if (e != null ? e.Id > 0 : false)
            {
                current_time = new DateTime(current_time.Year, current_time.Month, current_time.Day, current_time.Hour, current_time.Minute, 0);
                //On recherche la fiche de presence a la date debut et la date de fin
                List<Presence> lr = PresenceBLL.List("select * from yvs_grh_presence where '" + current_time.ToString("dd-MM-yyyy") + "' between date_debut and date_fin_prevu and employe = " + e.Id + " order by date_debut, heure_debut");
                if (lr != null ? lr.Count > 0 : false)//Si elle existe
                {
                    foreach (Presence pe in lr)
                    {
                        bool sortie = false;//Defini la nature du mouvement (entree ou sortie)
                        List<Pointage> lo = PointageBLL.List("select * from yvs_grh_pointage where presence = " + pe.Id + " order by heure_entree desc limit 1");
                        if (lo != null ? lo.Count > 0 : false)
                        {
                            Pointage last = lo[0];
                            if ((last != null) ? (last.Id != 0 ? (last.HeureSortie != null ? last.HeureSortie.ToString() == "01/01/0001 00:00:00" : true) : false) : false)
                            {
                                sortie = true;
                            }
                        }

                        DateTime heure_fin = new DateTime(pe.DateFinPrevu.Year, pe.DateFinPrevu.Month, pe.DateFinPrevu.Day, pe.HeureFinPrevu.Hour, pe.HeureFinPrevu.Minute, 0);
                        //On Verifi si la date de pointage est egale à la date de début de la fiche
                        if (pe.DateDebut.ToString("dd-MM-yyyy").Equals(current_time.ToString("dd-MM-yyyy")))
                        {
                            if (!sortie)//Si c'est uen entree                
                            {
                                if (current_time > heure_fin)//On controle si le pointage est hors des marges de la fiche
                                {
                                    // On modifie l'heure et/ou la date de fin prevu de la fiche
                                    TrancheHoraire t = GetTrancheHoraire(e, current_time);
                                    pe.HeureFinPrevu = t.HeureFin;
                                    pe.DateFinPrevu = Utils.GetTimeStamp(Utils.TimeStamp(pe.DateDebut, pe.HeureDebut), pe.HeureFinPrevu);
                                    if (!search_only)
                                        PresenceBLL.Update(pe);
                                }
                            }
                            pe.HeureDebut = new DateTime(pe.DateDebut.Year, pe.DateDebut.Month, pe.DateDebut.Day, pe.HeureDebut.Hour, pe.HeureDebut.Minute, 0);
                            pe.HeureFin = new DateTime(pe.DateFin.Year, pe.DateFin.Month, pe.DateFin.Day, pe.HeureFin.Hour, pe.HeureFin.Minute, 0);
                            pe.HeureFinPrevu = new DateTime(pe.DateFinPrevu.Year, pe.DateFinPrevu.Month, pe.HeureFinPrevu.Day, pe.HeureFinPrevu.Hour, pe.HeureFinPrevu.Minute, 0);
                            return pe;
                        }
                        else
                        {
                            if (sortie)//Si c'est une sortie on ajoute la marge a l'heure de fin
                                heure_fin = Utils.AddTimeInDate(heure_fin, Constantes.PARAMETRE.TimeMargeAvance);
                            if (current_time <= heure_fin)//On controle si le pointage est dans les marges de la fiche
                            {
                                pe.HeureDebut = new DateTime(pe.DateDebut.Year, pe.DateDebut.Month, pe.DateDebut.Day, pe.HeureDebut.Hour, pe.HeureDebut.Minute, 0);
                                pe.HeureFin = new DateTime(pe.DateFin.Year, pe.DateFin.Month, pe.DateFin.Day, pe.HeureFin.Hour, pe.HeureFin.Minute, 0);
                                pe.HeureFinPrevu = new DateTime(pe.DateFinPrevu.Year, pe.DateFinPrevu.Month, pe.HeureFinPrevu.Day, pe.HeureFinPrevu.Hour, pe.HeureFinPrevu.Minute, 0);
                                return pe;
                            }
                        }
                    }
                }
            }
            return null;
        }

        public static void DefaultLCD(bool bIsConnected)
        {
            new Appareil().ClearLCD(bIsConnected);
            new Appareil().WriteLCD(bIsConnected, 0, 0, "Welcome");
            new Appareil().WriteLCD(bIsConnected, 1, 3, DateTime.Now.ToShortTimeString());
            new Appareil().WriteLCD(bIsConnected, 3, 0, DateTime.Now.ToString("yy-MM-d"));
            new Appareil().WriteLCD(bIsConnected, 3, 12, DateTime.Now.ToString("ddd").ToUpper());
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
                Messages.Exception("Fonctions (StartAllDeviceDisconnect) ", ex);
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
                            p.Connecter = true;
                            Constantes.POINTEUSES.Find(x => x.Id == p_.Id).Connecter = true;
                            ++result;
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                // log errors
                Messages.Exception("Fonctions (StartAllDevice) ", ex);
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
                Utils.WriteLog("Fonctions (SynchroniseTmpOneServeur_) : " + ex.Message);
            }
        }

        public static void SynchroniseLog(bool v, bool thread)
        {
            List<Pointeuse> l = PointeuseBLL.List("select * from yvs_pointeuse where societe = " + Constantes.SOCIETE.Id + " and actif is true order by adresse_ip");
            foreach (Pointeuse p in l)
            {
                SynchroniseLog(p, v, thread);
            }
        }

        public static void SynchroniseLog(Pointeuse p, bool v, bool thread)
        {
            _pointeuse = p;
            _invalid_only = v;
            if (thread)
            {
                Thread t = new Thread(new ThreadStart(SynchroniseLogOneServeur));
                t.Start();
            }
            else
            {
                SynchroniseLogOneServeur();
            }
        }

        public static void SynchroniseLogOneServeur()
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
                SynchroniseServer(l, _pointeuse.Ip, true, null, false, _dateDebut, _dateFin, _invalid_only);
            }
            catch (Exception ex)
            {
                Utils.WriteLog("Fonctions (SynchroniseLogOneServeur) : " + ex.Message);
            }
        }

        public static void SynchroniseLog(Employe e, bool v, bool thread)
        {
            List<Pointeuse> l = PointeuseBLL.List("select * from yvs_pointeuse where societe = " + Constantes.SOCIETE.Id + " and actif is true order by adresse_ip");
            foreach (Pointeuse p in l)
            {
                SynchroniseLog(p, e, v, thread);
            }
        }

        public static void SynchroniseLog(Pointeuse p, Employe e, bool v, bool thread)
        {
            _pointeuse = p;
            _employe = e;
            _invalid_only = v;
            if (thread)
            {
                Thread t = new Thread(new ThreadStart(SynchroniseLogEmployeOneServeur));
                t.Start();
            }
            else
            {
                SynchroniseLogEmployeOneServeur();
            }
        }

        public static void SynchroniseLogEmployeOneServeur()
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
                List<IOEMDevice> l = z.GetAllAttentdData(1, _pointeuse.Connecter, _employe, false, _dateDebut, _dateFin);
                SynchroniseServer(l, _pointeuse.Ip, true, _employe, false, _dateDebut, _dateFin, _invalid_only);
            }
            catch (Exception ex)
            {
                Utils.WriteLog("Fonctions (SynchroniseLogOneServeur) : " + ex.Message);
            }
        }

        public static void SynchroniseLog(DateTime dateDebut, DateTime dateFin, bool v, bool thread)
        {
            List<Pointeuse> l = PointeuseBLL.List("select * from yvs_pointeuse where societe = " + Constantes.SOCIETE.Id + " and actif is true order by adresse_ip");
            foreach (Pointeuse p in l)
            {
                SynchroniseLog(p, dateDebut, dateFin, v, thread);
            }
        }

        public static void SynchroniseLog(Pointeuse p, DateTime dateDebut, DateTime dateFin, bool v, bool thread)
        {
            _pointeuse = p;
            _dateDebut = dateDebut;
            _dateFin = dateFin;
            _invalid_only = v;
            if (thread)
            {
                Thread t = new Thread(new ThreadStart(SynchroniseLogOneServeurDate));
                t.Start();
            }
            else
            {
                SynchroniseLogOneServeurDate();
            }
        }

        public static void SynchroniseLogOneServeurDate()
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
                List<IOEMDevice> l = z.GetAllAttentdData(1, _pointeuse.Connecter, null, true, _dateDebut, _dateFin);
                SynchroniseServer(l, _pointeuse.Ip, true, null, true, _dateDebut, _dateFin, _invalid_only);
            }
            catch (Exception ex)
            {
                Utils.WriteLog("Fonctions (SynchroniseLogOneServeurDate) : " + ex.Message);
            }
        }

        public static void SynchroniseLog(List<Pointeuse> list, DateTime dateDebut, DateTime dateFin, bool v, bool thread)
        {
            _pointeuses = list;
            _dateDebut = dateDebut;
            _dateFin = dateFin;
            _invalid_only = v;
            if (thread)
            {
                Thread t = new Thread(new ThreadStart(SynchroniseLogListServeurDate));
                t.Start();
            }
            else
            {
                SynchroniseLogListServeurDate();
            }
        }

        public static void SynchroniseLogListServeurDate()
        {
            try
            {
                List<IOEMDevice> l = new List<IOEMDevice>();
                foreach (Pointeuse p in _pointeuses)
                {
                    Appareil z = Utils.ReturnAppareil(_pointeuse);
                    Utils.VerifyZkemkeeper(ref z, ref _pointeuse);
                    if (z == null)
                    {
                        Utils.WriteLog("La liaison avec l'appareil " + _pointeuse.Ip + " est corrompue");
                        return;
                    }
                    l.AddRange(z.GetAllAttentdData(1, _pointeuse.Connecter, null, true, _dateDebut, _dateFin));
                }
                l.Sort();
                SynchroniseServer(l, _pointeuse.Ip, true, null, true, _dateDebut, _dateFin, _invalid_only);
            }
            catch (Exception ex)
            {
                Utils.WriteLog("Fonctions (SynchroniseLogOneServeurDate) : " + ex.Message);
            }
        }

        public static void SynchroniseLog(Employe e, DateTime dateDebut, DateTime dateFin, bool v, bool thread)
        {
            List<Pointeuse> l = PointeuseBLL.List("select * from yvs_pointeuse where societe = " + Constantes.SOCIETE.Id + " and actif is true order by adresse_ip");
            foreach (Pointeuse p in l)
            {
                SynchroniseLog(p, e, dateDebut, dateFin, v, thread);
            }
        }

        public static void SynchroniseLog(Pointeuse p, Employe e, DateTime dateDebut, DateTime dateFin, bool v, bool thread)
        {
            _employe = e;
            _pointeuse = p;
            _dateDebut = dateDebut;
            _dateFin = dateFin;
            _invalid_only = v;
            if (thread)
            {
                Thread t = new Thread(new ThreadStart(SynchroniseLogEmployeOneServeurDate));
                t.Start();
            }
            else
            {
                SynchroniseLogEmployeOneServeurDate();
            }
        }

        public static void SynchroniseLogEmployeOneServeurDate()
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
                List<IOEMDevice> l = z.GetAllAttentdData(1, _pointeuse.Connecter, null, true, _dateDebut, _dateFin);
                SynchroniseServer(l, _pointeuse.Ip, true, _employe, true, _dateDebut, _dateFin, _invalid_only);
            }
            catch (Exception ex)
            {
                Utils.WriteLog("Fonctions (SynchroniseLogEmployeOneServeurDate) : " + ex.Message);
            }
        }

        public static void SynchroniseServer(List<IOEMDevice> lp, String ip, bool auto, bool filter)
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
                    if (!p.exclure)
                    {
                        bool deja = false;
                        if (!filter)
                        {
                            foreach (IOEMDevice s in ls)
                            {
                                if (s.idwSEnrollNumber == p.idwSEnrollNumber && s.idwYear == p.idwYear && s.idwMonth == p.idwMonth && s.idwDay == p.idwDay && s.idwHour == p.idwHour && s.idwMinute == p.idwMinute && s.idwSecond == p.idwSecond)
                                {
                                    deja = true;
                                    break;
                                }
                            }
                        }
                        if (!deja)
                        {
                            Employe employe = EmployeBLL.OneById(Convert.ToInt32(p.idwSEnrollNumber));
                            if (employe != null ? employe.Id > 0 : false)
                            {
                                DateTime heure = new DateTime(p.idwYear, p.idwMonth, p.idwDay, p.idwHour, p.idwMinute, p.idwSecond);
                                DateTime date = heure;
                                bool _suivre = false;
                                while (!_suivre)
                                {
                                    if (Fonctions.InsertionPointage((Employe)employe, date, heure, (p.pointeuse != null ? p.pointeuse : null)))
                                    {
                                        Logs.WriteCsv(p);
                                        ls.Add(p);
                                        cpt++;

                                        p.iCorrect = true;
                                        if (Constantes.FORM_EVENEMENT != null)
                                        {
                                            if (Constantes.FORM_EVENEMENT.dgv_log != null && Constantes.FORM_EVENEMENT.logs != null)
                                            {
                                                int pos = Utils.GetRowData(Constantes.FORM_EVENEMENT.dgv_log, p.id);
                                                if (pos > -1)
                                                {
                                                    Constantes.FORM_EVENEMENT.object_log.RemoveDataGridView(pos);
                                                    Constantes.FORM_EVENEMENT.AddRow(pos, p);
                                                }
                                            }
                                        }
                                    }
                                    _suivre = true;
                                }
                            }
                        }
                        if (filter)
                        {
                            deja = false;
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
                        }
                    }
                    Constantes.LoadPatience(false);
                }
                string file = Chemins.CheminPing() + ip + ".txt";
                if (File.Exists(file))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception ex) { }
                }
                if (!auto)
                {
                    BackupLogData(lp, ip, auto);
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

        public static void SynchroniseServer(List<IOEMDevice> lp, String ip, Employe e, bool date_, DateTime d, DateTime f, bool invalid)
        {
            SynchroniseServer(lp, ip, true, e, date_, d, f, invalid);
        }

        public static void SynchroniseServer(List<IOEMDevice> lp, String ip, bool auto, Employe e, bool date_, DateTime d, DateTime f, bool invalid)
        {
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
                            query = "delete from yvs_grh_presence where employe = " + e.Id + " and date_debut between '" + d.ToShortDateString() + "' and '" + f.ToShortDateString() + "' and date_fin between '" + d.ToShortDateString() + "' and '" + f.ToShortDateString() + "' and valider = false";
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
                    SynchroniseServer(lp, ip, auto, true);
                }
            }
            else
            {
                Utils.WriteLog("-- Synchronisation impossible...paramètres incorrects");
            }
        }

        public static List<IOEMDevice> ReorganiseLog(List<IOEMDevice> lp, DateTime dateDebut, DateTime dateFin)
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
                        BackupLogData(le, p.Ip, true);
                    }
                }
                Utils.WriteLog("Fin de la sauvegarder automatique");
            }
        }

        public static void BackupLogData(List<IOEMDevice> lp, String ip, bool auto)
        {
            List<Appareil> aps = new List<Appareil>();
            if (lp.Count > 0)
            {
                ObjectThread o = new ObjectThread(Constantes.PBAR_WAIT);
                o.UpdateMaxBar(Constantes.PBAR_WAIT.Maximum + lp.Count);

                Utils.WriteLog((auto ? "--" : "") + "Début de la sauvegarde des opérations enrégistrées dans la pointeuse " + ip + " .....");
                for (int i = 0; i < lp.Count; i++)
                {
                    Logs.WriteCsv(TOOLS.Chemins.CheminBackup(ip) + DateTime.Now.ToString("dd-MM-yyyy") + ".csv", lp[i]);
                    if (!aps.Contains(lp[i].pointeuse.Zkemkeeper))
                        aps.Add(lp[i].pointeuse.Zkemkeeper);
                    Constantes.LoadPatience(false);
                }
                Utils.WriteLog((auto ? "--" : "") + "Fin de la sauvegarde des opérations enrégistrées dans la pointeuse " + ip + " .....");
            }
            Setting s = SettingBLL.ReturnSetting();
            if (s.AutoClearAndBackup)
            {
                foreach (Appareil z in aps)
                {
                    if (z != null)
                    {
                        //z.ClearGLog();
                    }
                }
            }
            Constantes.LoadPatience(true);
        }

        public static void CheckPingAndSynchro()
        {
            List<Pointeuse> pointeuses = new List<Pointeuse>();
            foreach (Pointeuse p in Constantes.POINTEUSES)
            {
                bool b = true;
                Appareil z = Utils.ReturnAppareil(p);
                if (z == null)
                {
                    z = new Appareil(p);
                    b = z.ConnectNet();
                }
                if (b)
                {
                    p.Zkemkeeper = z;
                    int id = p.Id;
                    int idx = Constantes.POINTEUSES.FindIndex(x => x.Id == id);
                    if (idx > -1)
                    {
                        Constantes.POINTEUSES[idx] = p;
                    }
                    if (!pointeuses.Contains(p))
                        pointeuses.Add(p);
                }
            }
            CheckPingAndSynchro(pointeuses);
        }

        public static void CheckPingAndSynchro(List<Pointeuse> list)
        {
            List<Pointeuse> pointeuses = new List<Pointeuse>();
            foreach (Pointeuse p in list)
            {
                string file = Chemins.CheminPing() + p.Ip + ".txt";
                List<string> pings = Logs.ReadTxt(file);
                if (pings != null ? pings.Count > 0 : false)
                {
                    DateTime _last = new DateTime();
                    int i = 0;
                    foreach (string line in pings)
                    {
                        long temps = Constantes.MILLISECONDS(Convert.ToDateTime(line)) - Constantes.MILLISECONDS(_last);
                        if ((i > 0) && (temps > (1000 * Constantes.MAX_TIME_PING)))
                        {
                            pointeuses.Add(p);
                            break;
                        }
                        _last = Convert.ToDateTime(line);
                        i++;
                    }
                }
                if (File.Exists(file))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception ex) { }
                }
            }
            if (pointeuses.Count>0)
            {
                DateTime debut = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                DateTime fin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
                Fonctions.SynchroniseLog(pointeuses, debut, fin, true, true);
            }
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
                                            CloneTemplateInOthers(ip, z._EMPLOYE, z._FINGER, flag, tmp, lon, true);
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

        public static void CloneTemplateInOthers(string ip, Employe e, Finger f, int flag, string tmp, int lon, bool onThread)
        {
            if (onThread)
            {
                Thread thread = new Thread(delegate() { CloneTemplateInOthers(ip, e, f, flag, tmp, lon); });
                thread.Start();
            }
            else
            {
                CloneTemplateInOthers(ip, e, f, flag, tmp, lon);
            }
        }

        public static void CloneTemplateInOthers(string _ip, Employe e, Finger f, int flag, string tmp, int lon)
        {
            string query = "select * from yvs_pointeuse where adresse_ip != '" + _ip.Trim() + "' and actif = true and societe = " + Constantes.SOCIETE.Id + " order by adresse_ip";
            List<Pointeuse> pointeuses = PointeuseBLL.List(query);
            if (pointeuses.Count > 0)
            {
                Utils.WriteLog("Synchronisation du DOIGT(" + f.Doigt + ") de la MAIN(" + f.Main + ") de l'employé " + e.NomPrenom + " .....!");
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

        public static void LoadFileTamponPointeuses(int view, bool onThread)
        {
            List<Pointeuse> pointeuses = PointeuseBLL.List("select * from yvs_pointeuse where societe = " + Constantes.SOCIETE.Id + " and actif = true order by adresse_ip");
            if (pointeuses != null)
            {
                foreach (Pointeuse p in pointeuses)
                {
                    LoadFileTamponPointeuse(p, view, onThread);
                }
            }
        }

        public static void LoadFileTamponPointeuse(Pointeuse p, int view, bool onThread)
        {
            if (p != null ? p.Ip.Trim().Length > 0 : false)
            {
                Appareil z = Utils.ReturnAppareil(p);
                Utils.VerifyZkemkeeper(ref z, ref p, view);
                if (z != null)
                {
                    if (onThread)
                    {
                        Thread thread = new Thread(delegate() { LoadFileTamponPointeuse(z, p, view); });
                        thread.Start();
                    }
                    else
                    {
                        LoadFileTamponPointeuse(z, p, view);
                    }
                }
            }
        }

        public static void LoadFileTamponPointeuse(Appareil z, Pointeuse p, int view)
        {
            p.FileLoad = false;
            p.Logs = z.GetAllAttentdData(1, p.Connecter);
            if (Constantes.POINTEUSES != null)
            {
                int id = p.Id;
                int idx = Constantes.POINTEUSES.FindIndex(x => x.Id == id);
                if (idx > -1)
                {
                    Constantes.POINTEUSES[idx] = p;
                }
            }
            if (Constantes.FORM_PARENT != null)
            {
                Constantes.FORM_PARENT.UpdatePointeuse(p);
            }

            string fileBack = TOOLS.Chemins.CheminBackup(p.Ip) + DateTime.Now.ToString("dd-MM-yyyy") + ".csv";
            if (File.Exists(fileBack))
                File.Delete(fileBack);

            string fileTemp = Chemins.CheminTemp() + p.Ip + ".csv";
            if (File.Exists(fileTemp))
                File.Delete(fileTemp);

            foreach (IOEMDevice io in p.Logs)
            {
                Logs.WriteCsv(fileTemp, io);
                Logs.WriteCsv(fileBack, io);
            }
            p.FileLoad = true;
            if (Constantes.POINTEUSES != null)
            {
                int id = p.Id;
                int idx = Constantes.POINTEUSES.FindIndex(x => x.Id == id);
                if (idx > -1)
                {
                    Constantes.POINTEUSES[idx] = p;
                }
            }
            if (Constantes.FORM_PARENT != null)
            {
                Constantes.FORM_PARENT.UpdatePointeuse(p);
            }
        }

    }
}
