using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using Microsoft.Win32;

using ZK_LymytzService.BLL;
using ZK_LymytzService.ENTITE;

namespace ZK_LymytzService.TOOLS
{
    public class ThreadRegEvent
    {
        static List<Empreinte> _le = new List<Empreinte>();
        public static Pointeuse _pointeuse = new Pointeuse();

        public ThreadRegEvent() { }

        public ThreadRegEvent(Pointeuse p)
        {
            _pointeuse = p;
        }

        private static Presence Presence_()
        {
            Presence bean = new Presence();
            bean.HeureDebut = Constantes.PLANNING.HeureDebut;
            bean.HeureFin = Constantes.PLANNING.HeureFin;
            bean.Employe = Constantes.EMPLOYE;
            bean.DateDebut = Constantes.PLANNING.DateDebut;
            bean.DateFin = Constantes.PLANNING.DateFin;
            bean.DureePause = Constantes.PLANNING.DureePause;
            bean.Valider = Constantes.PLANNING.Valide;
            return bean;
        }

        private static Pointage Pointage_()
        {
            return Pointage_(0, Constantes.CURRENT_TIME, Constantes.CURRENT_TIME, Constantes.VALIDER);
        }

        private static Pointage Pointage_(bool valider)
        {
            return Pointage_(0, Constantes.CURRENT_TIME, Constantes.CURRENT_TIME, Constantes.VALIDER);
        }

        private static Pointage Pointage_(long id)
        {
            return Pointage_(id, Constantes.CURRENT_TIME, Constantes.CURRENT_TIME, Constantes.VALIDER);
        }

        private static Pointage Pointage_(DateTime heureEntree, DateTime heureSortie)
        {
            return Pointage_(0, heureEntree, heureSortie, false);
        }

        private static Pointage Pointage_(DateTime heureEntree, DateTime heureSortie, bool valider)
        {
            return Pointage_(0, heureEntree, heureSortie, Constantes.VALIDER);
        }

        private static Pointage Pointage_(long id, DateTime heureEntree, DateTime heureSortie, bool valider)
        {
            Pointage bean = new Pointage();
            bean.Id = id;
            bean.Presence = Constantes.PRESENCE;
            bean.HeureEntree = heureEntree;
            bean.HeureSortie = heureSortie;
            bean.Valider = Constantes.VALIDER;
            return bean;
        }

        public static bool InsertionPointage(Employe employe_, DateTime date_, DateTime heure_)
        {
            DateTime h = new DateTime(heure_.Year, heure_.Month, heure_.Day, heure_.Hour, heure_.Minute, 0);
            string req = "select p.* from yvs_grh_pointage p inner join yvs_grh_presence r on p.presence = r.id where r.employe = " + Constantes.EMPLOYE.Id + " and ((heure_entree is not null and heure_entree = '" + h + "') or (heure_sortie is not null and heure_sortie = '" + h + "'))";
            List<Pointage> p = PointageBLL.List(req);
            if (p != null ? p.Count < 1 : true)
            {
                Constantes.EMPLOYE = employe_;
                Constantes.CURRENT_DATE = date_;
                Constantes.CURRENT_TIME = heure_;
                return OnSavePointage();
            }
            return false;
        }

        public static bool OnSavePointage()
        {
            Constantes.CURRENT_TIME = new DateTime(Constantes.CURRENT_TIME.Year, Constantes.CURRENT_TIME.Month, Constantes.CURRENT_TIME.Day, Constantes.CURRENT_TIME.Hour, Constantes.CURRENT_TIME.Minute, 0);
            try
            {
                if ((Constantes.EMPLOYE != null) ? Constantes.EMPLOYE.Id > 0 : false)
                {
                    Constantes.PLANNING = GetPlanning(Constantes.EMPLOYE, Constantes.CURRENT_TIME);

                    Constantes.PRESENCE = GetPresence(Constantes.EMPLOYE, Constantes.PLANNING, Constantes.CURRENT_TIME);
                    if ((Constantes.PRESENCE != null) ? Constantes.PRESENCE.Id < 1 : true)
                    {
                        if (PresenceBLL.Insert(Presence_()))
                        {
                            List<Presence> lr = PresenceBLL.List("select * from yvs_grh_presence where date_debut = '" + Constantes.PLANNING.DateDebut + "' and date_fin = '" + Constantes.PLANNING.DateFin + "' and employe = " + Constantes.EMPLOYE.Id + " order by heure_debut desc");
                            if (lr != null ? lr.Count > 0 : false)
                            {
                                Presence p_ = lr[0];
                                p_.HeureDebut = new DateTime(p_.DateDebut.Year, p_.DateDebut.Month, p_.DateDebut.Day, p_.HeureDebut.Hour, p_.HeureDebut.Minute, 0);
                                p_.HeureFin = new DateTime(p_.DateFin.Year, p_.DateFin.Month, p_.DateFin.Day, p_.HeureFin.Hour, p_.HeureFin.Minute, 0);
                                Constantes.PRESENCE = p_;
                            }
                            if (Constantes.PRESENCE != null ? Constantes.PRESENCE.Id < 1 : true)
                            {
                                OnSavePointage();
                            }
                        }
                    }
                    //Recherche le dernier pointage
                    List<Pointage> lp = PointageBLL.List("select * from yvs_grh_pointage where presence = " + Constantes.PRESENCE.Id + " and heure_entree is not null order by heure_entree desc");
                    if (lp != null ? lp.Count < 1 : true)//S'il n'y'a pas de pointage
                    {
                        //On insere une entrée
                        return OnSavePointage("E", null, Constantes.PRESENCE);
                    }
                    else
                    {
                        //S'il existe on le recupère
                        Pointage po = lp[0];
                        //On verifi si le dernier pointage est une entrée
                        if ((po.HeureSortie != null) ? po.HeureSortie.ToString() == "01/01/0001 00:00:00" : true)//Si le dernier pointage etait une entrée
                        {
                            return OnSavePointage("S", po, Constantes.PRESENCE);
                        }
                        else//Si le dernier pointage etait une sortie
                        {
                            return OnSavePointage("E", po, Constantes.PRESENCE);
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
                return false;
            }
        }

        private static bool OnSavePointage(string mouv, Pointage po, Presence pe)
        {
            switch (mouv)
            {
                case "S":
                    {
                        bool valider_ = true;
                        if (Constantes.PARAMETRE.InvalideTimeRetard)
                        {
                            DateTime h_ = Utils.SetTimeStamp(pe.HeureDebut, Constantes.PARAMETRE.TimeMargeRetard);
                            if (h_ < po.HeureEntree)
                            {
                                valider_ = false;
                            }
                        }
                        //On verifi si l'heure d'entrée etait inferieur a l'heure d'entree prevu
                        if (po.HeureEntree < pe.HeureDebut)//Si l'heure d'entree etait inferieur a l'heure d'entree prevu
                        {
                            //On verifi si l'heure actuelle est superieur a l'heure d'entree prevu
                            if (Constantes.CURRENT_TIME > pe.HeureDebut)//Si l'heure actuelle  est superieur a l'heure d'entree prevu
                            {
                                //On Complete la sortie du dernier pointage par l'heure d'entree prevu
                                if (PointageBLL.Update(Pointage_(pe.HeureDebut, pe.HeureDebut), po.Id))
                                {
                                    //On verifi si l'heure actuelle est superieur a l'heure de sortie prevu
                                    if (Constantes.CURRENT_TIME > pe.HeureFin) //Si l'heure actuelle est superieur a l'heure de sortie prevu
                                    {
                                        //On insert un pointage supplementaire qui va de l'heure d'entre prevu a l'heure de sortie prevu
                                        if (PointageBLL.InsertU(Pointage_(pe.HeureDebut, pe.HeureFin, valider_)))
                                        {
                                            //On insert un pointage supplementaire qui va de l'heure de sortie prevu a l'heure actuelle
                                            return PointageBLL.InsertU(Pointage_(pe.HeureFin, Constantes.CURRENT_TIME));
                                        }
                                    }
                                    else //Si l'heure actuelle est infereiur a l'heure de sortie prevu
                                    {
                                        //On insert un pointage supplementaire qui va de l'heure d'entree prevu a l'heure actuelle
                                        return PointageBLL.InsertU(Pointage_(pe.HeureDebut, Constantes.CURRENT_TIME, valider_));
                                    }
                                }
                            }
                            else//Si l'heure actuelle  est inferieur a l'heure d'entree prevu
                            {
                                return PointageBLL.Update(Pointage_(), po.Id);
                            }
                        }//On verifi si l'heure d'entre etait superieur a l'heure de sorti prevu
                        else if (po.HeureEntree >= pe.HeureFin)//Si l'heure d'entree etait superieur ou egale a l'heure de sortie prevu
                        {
                            //On Complete la sortie du dernier pointage par l'heure actuelle
                            return PointageBLL.Update(Pointage_(), po.Id);
                        }
                        else//Si l'heure d'entree etait compris entre l'heure d'entre prevu et l'heure de sortie prevu
                        {
                            //On verifi si l'heure actuelle est superieur a l'heure de sortie prevu
                            if (Constantes.CURRENT_TIME > pe.HeureFin)//Si l'heure actuelle est superieur a l'heure de sortie prevu
                            {
                                //On Complete la sortie du dernier pointage par l'heure de sortie prevu
                                if (PointageBLL.Update(Pointage_(pe.HeureFin, pe.HeureFin, valider_), po.Id))
                                {
                                    //On insert un pointage supplementaire qui va de l'heure de sortie prevu a l'heure actuelle
                                    return PointageBLL.InsertU(Pointage_(pe.HeureFin, Constantes.CURRENT_TIME));
                                }
                            }
                            else
                            {
                                //On Complete la sortie du dernier pointage par l'heure actuelle
                                return PointageBLL.Update(Pointage_(valider_), po.Id);
                            }
                        }
                        break;
                    }
                case "E":
                    {
                        //On insert une entrée
                        DateTime h_ = Utils.SetTimeStamp(pe.HeureDebut, Constantes.PARAMETRE.TimeMargeAutorise);
                        if (pe.HeureDebut < Constantes.CURRENT_TIME && Constantes.CURRENT_TIME < h_)
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
                        return PointageBLL.Insert(Pointage_());
                    }
                default:
                    break;

            }
            return false;
        }

        private static TrancheHoraire GetTrancheHoraire(Employe e, DateTime heure_, string query)
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
                            break;
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
                                break;
                            }
                            else
                            {
                                p_.Id = t.Id;
                                p_.HeureDebut = t.HeureDebut;
                                p_.HeureFin = t.HeureFin;
                                p_.DureePause = t.DureePause;
                                break;
                            }
                        }
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
                                break;
                            }
                            else
                            {
                                p_.Id = t.Id;
                                p_.HeureDebut = t.HeureDebut;
                                p_.HeureFin = t.HeureFin;
                                p_.DureePause = t.DureePause;
                                break;
                            }
                        }
                        else
                        {
                            p_.Id = t.Id;
                            p_.HeureDebut = t.HeureDebut;
                            p_.HeureFin = t.HeureFin;
                            p_.DureePause = t.DureePause;
                            break;
                        }
                    }
                }
            }
            return p_;
        }

        private static Planning GetPlanning(Employe e, DateTime heure_)
        {
            try
            {
                Planning p_ = new Planning();
                if ((e != null) ? e.Id > 0 : false)
                {
                    if (e.HoraireDynamique)
                    {
                        List<Planning> lp = PlanningBLL.List("select * from yvs_grh_planning_employe where employe =" + e.Id + " and '" + heure_.ToString("yyyy-MM-dd") + "' between date_debut and date_fin order by date_debut");
                        if (lp.Count > 0)
                        {
                            if (lp.Count > 1)
                            {
                                int i = 0;
                                foreach (Planning p in lp)
                                {
                                    p_ = p;

                                    DateTime dateD = new DateTime(p.DateDebut.Year, p.DateDebut.Month, p.DateDebut.Day, 0, 0, 0);
                                    DateTime dateF = new DateTime(p.DateFin.Year, p.DateFin.Month, p.DateFin.Day, 0, 0, 0);
                                    DateTime heureD = p.HeureDebut;
                                    DateTime heureF = p.HeureFin;

                                    DateTime heure_debut = new DateTime(dateD.Year, dateD.Month, dateD.Day, heureD.Hour, heureD.Minute, 0);
                                    DateTime heure_fin = new DateTime(dateF.Year, dateF.Month, dateF.Day, heureF.Hour, heureF.Minute, 0);

                                    if (heure_ >= heure_debut && heure_ <= heure_fin)
                                    {
                                        break;
                                    }
                                    else if (heure_ < heure_debut && i == 0)
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
                                List<Presence> lr = PresenceBLL.List("select * from yvs_grh_presence where '" + heure_.ToString("yyyy-MM-dd") + "' between date_debut and date_fin and employe = " + e.Id + " order by date_debut desc, heure_debut desc");
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
                                                deja = true;
                                            }
                                        }
                                    }
                                }
                                if (!deja)
                                {
                                    string type = ((e.Contrat != null) ? e.Contrat.TypeTranche : "JN");
                                    string query = "select * from yvs_grh_tranche_horaire where type_journee = '" + type + "' order by heure_debut asc, type_journee";
                                    TrancheHoraire t = GetTrancheHoraire(e, heure_, query);
                                    if (t != null ? t.Id < 1 : true)
                                    {
                                        query = "select * from yvs_grh_tranche_horaire order by heure_debut asc, type_journee";
                                        t = GetTrancheHoraire(e, heure_, query);
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
                                    }
                                    else
                                    {
                                        p_.DateDebut = p_.DateFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 0, 0, 0);
                                        p_.HeureDebut = new DateTime(heure_.Year, heure_.Month, heure_.Day, 7, 30, 0);
                                        p_.HeureFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 17, 30, 0);
                                        p_.DureePause = new DateTime(heure_.Year, heure_.Month, heure_.Day, 2, 0, 0);
                                        p_.Valide = false;
                                        Utils.WriteLog("L'employé " + e.Nom + " n'a pas été programmé  à la date (" + heure_.ToShortDateString() + ")....");
                                    }
                                }
                            }
                            else
                            {
                                p_.DateDebut = p_.DateFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 0, 0, 0);
                                p_.HeureDebut = new DateTime(heure_.Year, heure_.Month, heure_.Day, 7, 30, 0);
                                p_.HeureFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 17, 30, 0);
                                p_.DureePause = new DateTime(heure_.Year, heure_.Month, heure_.Day, 2, 0, 0);
                                p_.Valide = false;
                                Utils.WriteLog("L'employé " + e.Nom + " n'a pas été programmé  à la date (" + heure_.ToShortDateString() + ")....");
                            }
                        }
                    }
                    else
                    {
                        if ((e.Contrat != null) ? e.Contrat.Id != 0 : false)
                        {
                            if ((e.Contrat.Calendrier != null) ? e.Contrat.Calendrier.Id != 0 : false)
                            {
                                if ((e.Contrat.Calendrier.JoursOuvres != null) ? e.Contrat.Calendrier.JoursOuvres.Count > 0 : false)
                                {
                                    foreach (JoursOuvres jour in e.Contrat.Calendrier.JoursOuvres)
                                    {
                                        if (jour.Jour.Equals(Utils.jourSemaine(Constantes.CURRENT_DATE), StringComparison.CurrentCultureIgnoreCase))
                                        {
                                            p_ = PlanningBLL.getPlanningForJoursOuvres(jour, heure_);
                                            break;
                                        }
                                    }
                                }
                            }

                            if ((p_ != null) ? p_.Id < 1 : true)
                            {
                                p_.DateDebut = p_.DateFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 0, 0, 0);
                                p_.HeureDebut = new DateTime(heure_.Year, heure_.Month, heure_.Day, 7, 30, 0);
                                p_.HeureFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 17, 30, 0);
                                p_.Valide = false;
                                p_.DureePause = new DateTime(heure_.Year, heure_.Month, heure_.Day, 2, 0, 0);
                                Utils.WriteLog("L'employé " + e.Nom + " n'a pas été programmé  à la  date (" + Constantes.CURRENT_DATE.ToShortDateString() + ")....");

                            }
                        }
                        else
                        {
                            Utils.WriteLog("L'employé " + e.Nom + " n'a pas de contrat !");
                        }
                    }
                }
                else
                {
                    Utils.WriteLog("Employé Inconnu !");
                }
                p_.HeureDebut = new DateTime(p_.DateDebut.Year, p_.DateDebut.Month, p_.DateDebut.Day, p_.HeureDebut.Hour, p_.HeureDebut.Minute, 0);
                p_.HeureFin = new DateTime(p_.DateFin.Year, p_.DateFin.Month, p_.DateFin.Day, p_.HeureFin.Hour, p_.HeureFin.Minute, 0);
                return p_;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static Presence GetPresence(Employe e, Planning p, DateTime heure_)
        {
            try
            {
                if ((e != null) ? e.Id > 0 : false)
                {
                    if (p != null ? p.Id > 0 : false)
                    {
                        List<Presence> lr = PresenceBLL.List("select * from yvs_grh_presence where date_debut = '" + p.DateDebut + "' and date_fin = '" + p.DateFin + "' and heure_debut = '" + p.HeureDebut.ToString("HH:mm:ss") + "' and heure_fin = '" + p.HeureFin.ToString("HH:mm:ss") + "' and employe = " + e.Id + " order by heure_debut desc");
                        if (lr != null ? lr.Count > 0 : false)
                        {
                            Presence p_ = lr[0];
                            p_.HeureDebut = new DateTime(p_.DateDebut.Year, p_.DateDebut.Month, p_.DateDebut.Day, p_.HeureDebut.Hour, p_.HeureDebut.Minute, 0);
                            p_.HeureFin = new DateTime(p_.DateFin.Year, p_.DateFin.Month, p_.DateFin.Day, p_.HeureFin.Hour, p_.HeureFin.Minute, 0);
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
                return null;
            }
        }

        public static bool StartAllDeviceDisconnect()
        {
            try
            {
                List<ENTITE.Pointeuse> pointeuses = PointeuseBLL.List("select * from yvs_pointeuse where connecter = false and actif = true");
                foreach (ENTITE.Pointeuse p in pointeuses)
                {
                    new Zkemkeeper().StartOne(p.Id, p.Ip);
                }
                return true;
            }
            catch (Exception ex)
            {
                // log errors
                return false;
            }
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

                    Zkemkeeper z = Utils.Zkemkeeper(p_);
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

        public static void BackupLogData(List<IOEMDevice> lp, String ip, bool auto, Zkemkeeper z)
        {
            if (lp.Count > 0)
            {
                Utils.WriteLog((auto ? "--" : "") + "Début de la sauvegarde des opérations enrégistrées dans la pointeuse " + ip + " .....");
                for (int i = 0; i < lp.Count; i++)
                {
                    Logs.WriteCsv(TOOLS.Chemins.getCheminBackup(ip) + DateTime.Now.ToString("dd-MM-yyyy") + ".csv", lp[i]);
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
        }

        public static int StartAllDevice()
        {
            try
            {
                int result = 0;
                List<Pointeuse> l = PointeuseBLL.List("select * from yvs_pointeuse where societe = " + Constantes.SOCIETE.Id + " and actif = true order by adresse_ip");
                foreach (ENTITE.Pointeuse p in l)
                {
                    Zkemkeeper z = Utils.Zkemkeeper(p);
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
                    Zkemkeeper z = Utils.Zkemkeeper(p);
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

        public static void SynchroniseServer(List<IOEMDevice> lp, String ip, bool auto, Zkemkeeper z)
        {
            if (lp.Count > 0 && ip.Length > 0)
            {
                bool synchro = auto;
                Utils.WriteLog("-- Début de la synchronisation des données de pointage de la pointeuse " + ip + " avec le serveur.....");

                List<IOEMDevice> ls = Logs.ReadCsv();
                int cpt = 0;

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
                        if (Constantes.EMPLOYE != null ? Constantes.EMPLOYE.Id > 0 : false)
                        {
                            DateTime heure = new DateTime(p.idwYear, p.idwMonth, p.idwDay, p.idwHour, p.idwMinute, p.idwSecond);
                            DateTime date = heure;
                            if (ThreadRegEvent.InsertionPointage((Employe)Constantes.EMPLOYE, date, heure))
                            {
                                Logs.WriteCsv(p);
                                cpt++;
                            }
                        }
                    }
                }
                if (!auto)
                {
                    BackupLogData(lp, ip, auto, z);
                }
                else
                {
                }
                Utils.WriteLog("-- Fin de la synchronisation des données de pointage de la pointeuse " + ip + " avec le serveur. Nombre de synchronisation " + cpt + "....");
            }
            else
            {
                Utils.WriteLog("-- Synchronisation impossible...paramètres incorrects");

            }
        }
    }
}
