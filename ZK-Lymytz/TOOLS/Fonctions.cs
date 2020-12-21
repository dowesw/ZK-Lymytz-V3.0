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

        public static bool InsertionPointage(Employe employe_, DateTime date_, DateTime heure_, Pointeuse pointeuse_, int action)
        {
            return InsertionPointage(employe_, date_, heure_, pointeuse_, action, null);
        }

        static Parametre parametre;

        public static bool InsertionPointage(Employe employe_, DateTime date_, DateTime heure_, Pointeuse pointeuse_, int action, string adresse)
        {
            try
            {
                if (parametre == null)
                {
                    parametre = ParametreBLL.OneBySociete(employe_.Agence.Societe.Id);
                }
                foreach (Serveur serveur in Form_Evenement.liaisons)
                {
                    SynchroniseCreneau(employe_.Id, date_, serveur);
                }
                DateTime h = new DateTime(heure_.Year, heure_.Month, heure_.Day, heure_.Hour, heure_.Minute, 0);
                string req = "select p.* from yvs_grh_pointage p inner join yvs_grh_presence r on p.presence = r.id where r.employe = " + employe_.Id + " and ((heure_entree is not null and heure_entree = '" + h + "') or (heure_sortie is not null and heure_sortie = '" + h + "'))";
                List<Pointage> p = PointageBLL.List(req, false, adresse);
                if (p != null ? p.Count < 1 : true)
                {
                    return OnSavePointage(employe_, heure_, date_, pointeuse_, action, adresse);
                }
            }
            catch (Exception ex)
            {
                Utils.Exception(ex);
            }
            return false;
        }

        public static bool OnSavePointage(Employe employe, DateTime current_time, DateTime current_date, Pointeuse pointeuse, int action, string adresse)
        {
            current_time = new DateTime(current_time.Year, current_time.Month, current_time.Day, current_time.Hour, current_time.Minute, 0);
            try
            {
                if ((employe != null) ? employe.Id > 0 : false)
                {
                    bool have_supplementaire_ = false;
                    Presence presence = GetPresence(employe, current_time, false, adresse);
                    if (presence != null ? presence.Id < 1 : true)//Si elle n'existe pas
                    {
                        have_supplementaire_ = true;
                        //On recherche le planning en fonction de l'heure courante
                        Planning planning = GetPlanning((Employe)employe, current_time, adresse);
                        //On recherche la fiche de présence en fonction du planning
                        presence = GetPresence((Employe)employe, (Planning)planning, adresse);
                        if ((presence != null) ? presence.Id < 1 : true)
                        {
                            if (PresenceBLL.Insert(Presence_(planning, employe), adresse))
                            {
                                List<Presence> lr = PresenceBLL.List("select * from yvs_grh_presence where date_debut = '" + planning.DateDebut.ToString("dd-MM-yyyy") + "' and date_fin = '" + planning.DateFin.ToString("dd-MM-yyyy") + "' and heure_debut = '" + planning.HeureDebut.ToString("HH:mm:ss") + "' and heure_fin = '" + planning.HeureFin.ToString("HH:mm:ss") + "' and employe = " + employe.Id + " order by heure_debut desc", false, adresse);
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
                        OnSavePointage(employe, current_time, current_date, pointeuse, action, adresse);
                        return false;
                    }
                    else
                    {
                        if (!have_supplementaire_)
                        {
                            Calendrier calendrier = null;
                            if ((employe.Contrat != null) ? (employe.Contrat.Id != 0 ? ((employe.Contrat.Calendrier != null) ? employe.Contrat.Calendrier.Id != 0 : false) : false) : false)
                            {
                                calendrier = employe.Contrat.Calendrier;
                            }
                            else
                            {
                                calendrier = CalendrierBLL.Default(employe.Agence.Societe);
                            }
                            if (calendrier != null ? calendrier.Id > 0 : false)
                            {
                                JoursOuvres jour_ouvree = JoursOuvresBLL.OneByCalendrier(calendrier, Utils.jourSemaine(presence.DateDebut), adresse);
                                if (jour_ouvree != null ? jour_ouvree.Id > 0 : false)
                                {
                                    presence.Supplementaire = parametre.LimiteHeureSup > 0 ? !jour_ouvree.Ouvrable : false;
                                }
                            }
                        }
                    }
                    if (!presence.Valider)
                    {
                        return OnSavePointage(presence, current_time, pointeuse, action, adresse);
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
                Utils.Exception(ex);
                return false;
            }
        }

        public static bool OnSavePointage(Presence presence, DateTime current_time, Pointeuse pointeuse, int action, string adresse)
        {
            try
            {
                // action  ----  0—Check-In (default value) 1—Check-Out 2—Break-Out 3—Break-In 4—OT-In 5—OT-Out

                //Recherche le dernier pointage
                List<Pointage> lp = PointageBLL.List("select * from yvs_grh_pointage where presence = " + presence.Id + " and heure_entree is not null order by heure_entree desc", false, adresse);
                if (lp != null ? lp.Count < 1 : true)//S'il n'y'a pas de pointage
                {
                    //On insere une entrée
                    switch (action)
                    {
                        case 1:
                        case 2:
                        case 5:
                            return OnSavePointage("S", null, (Presence)presence, current_time, pointeuse, adresse);
                        case 3:
                        case 4:
                            return OnSavePointage("E", null, (Presence)presence, current_time, pointeuse, adresse);
                        default:
                            return OnSavePointage("E", null, (Presence)presence, current_time, pointeuse, adresse);
                    }
                }
                else
                {
                    //S'il existe on le recupère
                    Pointage po = lp[0];
                    //On verifi si le dernier pointage est une entrée
                    if ((po.HeureSortie != null) ? po.HeureSortie.ToString() == "01/01/0001 00:00:00" : true)//Si le dernier pointage etait une entrée
                    {
                        //On insere une entrée
                        switch (action)
                        {
                            case 1:
                            case 2:
                            case 5:
                                return OnSavePointage("S", po, (Presence)presence, current_time, pointeuse, adresse);
                            case 3:
                            case 4:
                                return OnSavePointage("E", null, (Presence)presence, current_time, pointeuse, adresse);
                            default:
                                return OnSavePointage("S", po, (Presence)presence, current_time, pointeuse, adresse);
                        }
                    }
                    else//Si le dernier pointage etait une sortie
                    {
                        switch (action)
                        {
                            case 1:
                            case 2:
                            case 5:
                                return OnSavePointage("S", null, (Presence)presence, current_time, pointeuse, adresse);
                            case 3:
                            case 4:
                                return OnSavePointage("E", po, (Presence)presence, current_time, pointeuse, adresse);
                            default:
                                return OnSavePointage("E", po, (Presence)presence, current_time, pointeuse, adresse);
                        }
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

        public static bool OnSavePointage(string mouv, Pointage po, Presence pe, DateTime current_time, Pointeuse pointeuse, string adresse)
        {
            try
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
                                        if (PointageBLL.Update(Pointage_(pe, pe.HeureDebut, pe.HeureDebut, false, pointeuse, true, true), po.Id, adresse))
                                        {
                                            //On verifi si l'heure actuelle est superieur a l'heure de sortie prevu
                                            if (current_time > pe.HeureFin) //Si l'heure actuelle est superieur a l'heure de sortie prevu
                                            {
                                                //On insert un pointage supplementaire qui va de l'heure d'entre prevu a l'heure de sortie prevu
                                                if (PointageBLL.InsertU(Pointage_(pe, pe.HeureDebut, pe.HeureFin, true, pointeuse, true, true), adresse))
                                                {
                                                    //On insert un pointage supplementaire qui va de l'heure de sortie prevu a l'heure actuelle
                                                    return PointageBLL.InsertU(Pointage_(pe, pe.HeureFin, current_time, false, pointeuse, true, false), adresse);
                                                }
                                            }
                                            else //Si l'heure actuelle est infereiur a l'heure de sortie prevu
                                            {
                                                //On insert un pointage supplementaire qui va de l'heure d'entree prevu a l'heure actuelle
                                                return PointageBLL.InsertU(Pointage_(pe, pe.HeureDebut, current_time, true, pointeuse, true, false), adresse);
                                            }
                                        }
                                    }
                                    else//Si l'heure actuelle est inferieur a l'heure d'entree prevu
                                    {
                                        return PointageBLL.Update(Pointage_(pe, current_time, current_time, false, pointeuse, false, false), po.Id, adresse);
                                    }
                                }//On verifi si l'heure d'entre etait superieur a l'heure de sorti prevu
                                else if (po.HeureEntree >= pe.HeureFin)//Si l'heure d'entree etait superieur ou egale a l'heure de sortie prevu
                                {
                                    //On Complete la sortie du dernier pointage par l'heure actuelle
                                    return PointageBLL.Update(Pointage_(pe, current_time, current_time, false, pointeuse, false, false), po.Id, adresse);
                                }
                                else//Si l'heure d'entree etait compris entre l'heure d'entree prevu et l'heure de sortie prevu
                                {
                                    //On verifi si l'heure actuelle est superieur a l'heure de sortie prevu
                                    if (current_time > pe.HeureFin)//Si l'heure actuelle est superieur a l'heure de sortie prevu
                                    {
                                        //On Complete la sortie du dernier pointage par l'heure de sortie prevu
                                        if (PointageBLL.Update(Pointage_(pe, pe.HeureFin, pe.HeureFin, true, pointeuse, false, true), po.Id, adresse))
                                        {
                                            //On insert un pointage supplementaire qui va de l'heure de sortie prevu a l'heure actuelle
                                            return PointageBLL.InsertU(Pointage_(pe, pe.HeureFin, current_time, false, pointeuse, true, false), adresse);
                                        }
                                    }
                                    else
                                    {
                                        //On Complete la sortie du dernier pointage par l'heure actuelle
                                        return PointageBLL.Update(Pointage_(pe, current_time, current_time, true, pointeuse, false, false), po.Id, adresse);
                                    }
                                }
                            }
                            else
                            {
                                //On verifi si l'heure actuelle est superieur a l'heure de sortie prevu
                                if (current_time > pe.HeureFin)//Si l'heure actuelle est superieur a l'heure de sortie prevu
                                {
                                    //On Complete la sortie du dernier pointage par l'heure de sortie prevu
                                    if (PointageBLL.InsertU(Pointage_(pe, null, pe.HeureFin, false, pointeuse, false, true), adresse))
                                    {
                                        //On insert un pointage supplementaire qui va de l'heure de sortie prevu a l'heure actuelle
                                        return PointageBLL.InsertU(Pointage_(pe, pe.HeureFin, current_time, false, pointeuse, true, false), adresse);
                                    }
                                }
                                else
                                {
                                    //On Complete la sortie du dernier pointage par l'heure actuelle
                                    return PointageBLL.InsertU(Pointage_(pe, null, current_time, false, pointeuse, false, false), adresse);
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
                            return PointageBLL.Insert(Pointage_(pe, current_time, current_time, true, pointeuse, false, false), adresse);
                        }
                    default:
                        break;

                }
            }
            catch (Exception ex)
            {
                Utils.Exception(ex);
            }
            return false;
        }

        public static TrancheHoraire GetTrancheHoraire(Employe employe, DateTime current_heure, List<TrancheHoraire> tranches, List<TrancheHoraire> chevauches, string adresse)
        {
            TrancheHoraire result = new TrancheHoraire();
            try
            {
                if (tranches == null)
                    return result;

                if (chevauches != null)
                {
                    for (int i = 0; i < chevauches.Count; i++)
                    {
                        TrancheHoraire tranche = tranches[i];
                        DateTime heure_debut = new DateTime(current_heure.Year, current_heure.Month, current_heure.Day, tranche.HeureDebut.Hour, tranche.HeureDebut.Minute, 0).AddDays(-1);
                        DateTime heure_fin = new DateTime(current_heure.Year, current_heure.Month, current_heure.Day, tranche.HeureFin.Hour, tranche.HeureFin.Minute, 0);
                        List<Presence> presences = PresenceBLL.List("select * from yvs_grh_presence where date_debut = '" + heure_debut.ToString("dd-MM-yyyy") + "' and date_fin = '" + heure_fin.ToString("dd-MM-yyyy") + "' and heure_debut = '" + heure_debut.ToString("HH:mm:ss") + "' and heure_fin = '" + heure_fin.ToString("HH:mm:ss") + "' and employe = " + employe.Id + " order by heure_debut desc", false, adresse);
                        if (presences != null ? presences.Count > 0 : false)
                        {
                            foreach (Presence presence in presences)
                            {
                                DateTime heure_fin_with_marge = Utils.AddTimeInDate(Utils.GetTimeStamp(presence.DateFin, presence.HeureFin), Constantes.PARAMETRE.TimeMargeAvance);
                                if (heure_fin_with_marge >= current_heure)
                                {
                                    result = tranche;
                                    result.Chevauche = true;
                                    break;
                                }
                            }
                        }
                    }
                }

                if (result != null ? result.Id > 0 : false)
                    return result;

                for (int i = 0; i < tranches.Count; i++)
                {
                    TrancheHoraire tranche = tranches[i];
                    DateTime heure_debut = new DateTime(current_heure.Year, current_heure.Month, current_heure.Day, tranche.HeureDebut.Hour, tranche.HeureDebut.Minute, 0);
                    DateTime heure_fin = Utils.GetTimeStamp(heure_debut, tranche.HeureFin);
                    if (current_heure < heure_debut)
                    {
                        if (i == 0)
                            result = tranche;
                        else
                        {
                            TrancheHoraire precedent = tranches[i - 1];
                            DateTime heure_debut_with_marge = Utils.AddTimeInDate(current_heure, Constantes.PARAMETRE.TimeMargeAvance);
                            heure_debut = new DateTime(current_heure.Year, current_heure.Month, current_heure.Day, tranche.HeureDebut.Hour, tranche.HeureDebut.Minute, tranche.HeureDebut.Second);
                            if (heure_debut_with_marge < heure_debut)
                                result = precedent;
                            else
                                result = tranche;
                        }
                        break;
                    }
                    else if ((heure_debut == current_heure) || ((heure_debut < current_heure) && (current_heure < heure_fin)))
                    {
                        if (i < tranches.Count - 1)
                        {
                            TrancheHoraire suivant = tranches[i + 1];
                            DateTime heure_debut_with_marge = Utils.AddTimeInDate(current_heure, Constantes.PARAMETRE.TimeMargeAvance);
                            heure_debut = new DateTime(current_heure.Year, current_heure.Month, current_heure.Day, suivant.HeureDebut.Hour, suivant.HeureDebut.Minute, suivant.HeureDebut.Second);
                            if (heure_debut_with_marge >= heure_debut)
                                result = suivant;
                            else
                                result = tranche;
                        }
                        else
                            result = tranche;
                        break;
                    }
                    else if (i == tranches.Count)
                        result = tranche;
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("Fonctions (GetTrancheHoraire)", ex);
                return null;
            }
            return result;
        }

        public static TrancheHoraire GetTrancheHoraire(Employe e, DateTime current_heure, string adresse)
        {
            try
            {
                // On cherche la tranche horaire correspondante
                string type = ((e.Contrat != null) ? e.Contrat.TypeTranche : "JN");
                long societe = 0;
                if (e.Agence != null ? e.Agence.Societe != null ? e.Agence.Societe.Id > 0 : false : false)
                {
                    societe = e.Agence.Societe.Id;
                }
                else
                {
                    Agence a = AgenceBLL.OneById(e.Agence.Id);
                    e.Agence = a;
                    int index = Constantes.EMPLOYES.FindIndex(x => x.Id == e.Id);
                    if (index > -1)
                    {
                        Constantes.EMPLOYES[index].Agence = a;
                    }
                    societe = a.Societe.Id;
                }
                // Recherche des tranches de l'employé a partir de son type de tranche
                List<TrancheHoraire> tranches = Constantes.TRANCHES.FindAll(x => x.TypeJournee.ToUpper().Equals(type.ToUpper()) && x.Societe == societe);
                if (tranches != null ? tranches.Count < 1 : true)
                {
                    string query = "select * from yvs_grh_tranche_horaire where upper(type_journee) = upper('" + type + "') and societe = " + societe + " order by heure_debut asc, type_journee";
                    tranches = TrancheHoraireBLL.List(query, adresse);
                    if (tranches != null ? tranches.Count > 0 : false)
                    {
                        Constantes.TRANCHES.AddRange(new List<TrancheHoraire>(tranches));
                    }
                }
                // Recherche des tranches qui chevauche
                List<TrancheHoraire> chevauches = new List<TrancheHoraire>(tranches.FindAll(x => x.HeureDebut > x.HeureFin));
                TrancheHoraire t = GetTrancheHoraire(e, current_heure, tranches, chevauches, adresse);
                if (t != null ? t.Id < 1 : true)
                {
                    string query = "select * from yvs_grh_tranche_horaire where societe = " + societe + " order by heure_debut asc, type_journee";
                    tranches = TrancheHoraireBLL.List(query, adresse);
                    Constantes.TRANCHES = new List<TrancheHoraire>(new List<TrancheHoraire>(tranches));
                    t = GetTrancheHoraire(e, current_heure, tranches, null, adresse);
                }
                return t;
            }
            catch (Exception ex)
            {
                Messages.Exception("Fonctions (GetTrancheHoraire)", ex);
                return null;
            }
            return null;
        }

        public static Planning GetSimplePlanning(Employe e, DateTime heure_, string adresse)
        {
            try
            {
                Planning planning = new Planning();
                // On verifie si l'employé a un horaire dynamique
                if (e.HoraireDynamique) // Si oui
                {
                    // On recherche le planning de l'employé a la date
                    List<Planning> lp = PlanningBLL.List("select p.* from yvs_grh_planning_employe p inner join yvs_grh_tranche_horaire t on p.tranche = t.id where p.employe =" + e.Id + " and '" + heure_.ToString("dd-MM-yyyy") + "' between p.date_debut and p.date_fin order by p.date_debut, t.heure_debut", adresse);
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
                                    List<Presence> lr = PresenceBLL.List("select * from yvs_grh_presence where date_debut = '" + date_debut + "' and employe = " + e.Id + " order by date_debut desc, heure_debut desc", false, adresse);
                                    if (lr != null ? lr.Count > 0 : false)
                                    {
                                        Presence pe = lr[0];
                                        // On verifie s'il le dernier pointage de la fiche est une entrée
                                        List<Pointage> lo = PointageBLL.List("select * from yvs_grh_pointage where presence = " + pe.Id + " order by heure_entree desc limit 1", false, adresse);
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
                                    List<Pointage> lo = PointageBLL.List("select * from yvs_grh_pointage where presence = " + planning.Id + " order by heure_entree desc limit 1", false, adresse);
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
                            JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(e.Contrat.Calendrier, Utils.jourSemaine(planning.DateDebut), adresse);
                            if (jour != null ? jour.Id > 0 : false)
                            {
                                planning.Supplementaire = parametre.LimiteHeureSup > 0 ? !jour.Ouvrable : false;
                            }
                        }
                        else
                        {
                            JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(CalendrierBLL.Default(e.Agence.Societe), Utils.jourSemaine(planning.DateDebut), adresse);
                            if (jour != null ? jour.Id > 0 : false)
                            {
                                planning.Supplementaire = parametre.LimiteHeureSup > 0 ? !jour.Ouvrable : false;
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
                            JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(e.Contrat.Calendrier, Utils.jourSemaine(heure_.AddDays(-1)), adresse);
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
                                jour = JoursOuvresBLL.OneByCalendrier(e.Contrat.Calendrier, Utils.jourSemaine(heure_), adresse);
                                if (jour != null ? jour.Id > 0 : false)
                                {
                                    planning = PlanningBLL.getPlanningForJoursOuvres(jour, heure_);
                                }
                            }
                        }
                    }
                }
                if (planning != null)
                {
                    planning.HeureDebut = new DateTime(planning.DateDebut.Year, planning.DateDebut.Month, planning.DateDebut.Day, planning.HeureDebut.Hour, planning.HeureDebut.Minute, 0);
                    planning.HeureFin = new DateTime(planning.DateFin.Year, planning.DateFin.Month, planning.DateFin.Day, planning.HeureFin.Hour, planning.HeureFin.Minute, 0);
                    return planning;
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("Fonctions (GetSimplePlanning)", ex);
            }
            return null;
        }

        public static Planning GetPlanning(Employe e, DateTime heure_, string adresse)
        {
            try
            {
                if (parametre == null)
                {
                    parametre = ParametreBLL.OneBySociete(e.Agence.Societe.Id);
                }
                Planning planning = new Planning();
                if ((e != null) ? e.Id > 0 : false)
                {
                    planning = GetSimplePlanning(e, heure_, adresse);
                    if ((planning != null) ? planning.Id < 1 : true)
                    {
                        planning = new Planning();
                        if (Constantes.PARAMETRE.PlanningDynamique)
                        {
                            // On cherche la tranche horaire correspondante
                            TrancheHoraire tranche = GetTrancheHoraire(e, heure_, adresse);
                            if (tranche != null ? tranche.Id > 0 : false)
                            {
                                planning.Id = tranche.Id;
                                if (tranche.Chevauche)
                                    planning.DateDebut = new DateTime(heure_.Year, heure_.Month, heure_.Day, tranche.HeureDebut.Hour, tranche.HeureDebut.Minute, 0).AddDays(-1);
                                else
                                    planning.DateDebut = new DateTime(heure_.Year, heure_.Month, heure_.Day, tranche.HeureDebut.Hour, tranche.HeureDebut.Minute, 0);
                                planning.DateFin = Utils.GetTimeStamp(planning.DateDebut, tranche.HeureFin);
                                planning.HeureDebut = tranche.HeureDebut;
                                planning.HeureFin = tranche.HeureFin;
                                planning.DureePause = tranche.DureePause;
                                planning.Chevauche = tranche.Chevauche;
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
                                Calendrier calendrier = null;
                                if ((e.Contrat != null) ? (e.Contrat.Id != 0 ? ((e.Contrat.Calendrier != null) ? e.Contrat.Calendrier.Id != 0 : false) : false) : false)
                                {
                                    calendrier = e.Contrat.Calendrier;
                                }
                                else
                                {
                                    calendrier = CalendrierBLL.Default(e.Agence.Societe);
                                }
                                if (calendrier != null ? calendrier.Id > 0 : false)
                                {
                                    JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(calendrier, Utils.jourSemaine(planning.DateDebut), adresse);
                                    if (jour != null ? jour.Id > 0 : false)
                                    {
                                        planning.Supplementaire = parametre.LimiteHeureSup > 0 ? !jour.Ouvrable : false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if ((planning != null) ? planning.Id < 1 : true)
                            {
                                Calendrier calendrier = null;
                                if ((e.Contrat != null) ? e.Contrat.Id != 0 : false)
                                {
                                    calendrier = e.Contrat.Calendrier;
                                }
                                else
                                {
                                    calendrier = CalendrierBLL.Default(e.Agence.Societe);
                                }
                                if (calendrier != null ? calendrier.Id > 0 : false)
                                {
                                    planning.Id = 1;
                                    planning.DateDebut = planning.DateFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 0, 0, 0);
                                    planning.HeureDebut = new DateTime(heure_.Year, heure_.Month, heure_.Day, 7, 30, 0);
                                    planning.HeureFin = new DateTime(heure_.Year, heure_.Month, heure_.Day, 17, 00, 0);
                                    planning.Valide = false;
                                    JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(calendrier, Utils.jourSemaine(planning.DateDebut), adresse);
                                    if (jour != null ? jour.Id > 0 : false)
                                    {
                                        planning.Supplementaire = parametre.LimiteHeureSup > 0 ? !jour.Ouvrable : false;
                                    }
                                    planning.DureePause = new DateTime(heure_.Year, heure_.Month, heure_.Day, 1, 30, 0);
                                    Utils.WriteLog("L'employé " + e.Nom + " n'a pas de contrat !");
                                }
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
                    JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(CalendrierBLL.Default(e.Agence.Societe), Utils.jourSemaine(planning.DateDebut), adresse);
                    if (jour != null ? jour.Id > 0 : false)
                    {
                        planning.Supplementaire = parametre.LimiteHeureSup > 0 ? !jour.Ouvrable : false;
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

        public static List<Presence> GetPresences(Employe employe, DateTime current_time, string adresse)
        {
            List<Presence> presences = new List<Presence>();
            try
            {
                DateTime date = new DateTime(current_time.Year, current_time.Month, current_time.Day, 0, 0, 0);
                presences = employe.Presences.FindAll(x => date >= x.DateDebut && date <= x.DateFinPrevu);
                if (presences != null ? presences.Count < 1 : true)//Si elle n'existe pas
                {
                    presences = PresenceBLL.List("select * from yvs_grh_presence where '" + current_time.ToString("dd-MM-yyyy") + "' between date_debut and date_fin_prevu and employe = " + employe.Id + " order by date_debut, heure_debut", false, adresse);
                    if (presences != null ? presences.Count > 0 : false)//Si elle existe
                    {
                        employe.Presences.AddRange(presences);
                        int index = Constantes.EMPLOYES.FindIndex(x => x.Id == employe.Id && (Utils.asString(x.Adresse) && Utils.asString(adresse) ? x.Adresse == adresse : true));
                        if (index > -1)
                        {
                            Constantes.EMPLOYES[index].Presences.AddRange(presences);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.Exception(ex);
            }
            return presences;
        }

        public static Presence GetPresence(Employe e, Planning p, string adresse)
        {
            try
            {
                if ((e != null) ? e.Id > 0 : false)
                {
                    if (p != null ? p.Id > 0 : false)
                    {
                        //On recherche s'il une fiche de presence n'existe pas a la date début et la date fin du planning
                        List<Presence> lr = PresenceBLL.List("select * from yvs_grh_presence where date_debut = '" + p.DateDebut.ToString("dd-MM-yyyy") + "' and date_fin = '" + p.DateFin.ToString("dd-MM-yyyy") + "' and employe = " + e.Id + " order by heure_debut desc", false, adresse);
                        bool deja = false;
                        if (lr != null ? lr.Count > 0 : false)
                        {
                            deja = true;
                        }
                        else
                        {
                            //On recherche s'il une fiche de presence n'existe pas a la date début du planning
                            lr = PresenceBLL.List("select * from yvs_grh_presence where date_debut = '" + p.DateDebut.ToString("dd-MM-yyyy") + "' and employe = " + e.Id + " order by heure_debut desc", false, adresse);
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
                                JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(e.Contrat.Calendrier, Utils.jourSemaine(p.DateDebut), adresse);
                                if (jour != null ? jour.Id > 0 : false)
                                {
                                    presence.Supplementaire = !jour.Ouvrable;
                                }
                            }
                            else
                            {
                                JoursOuvres jour = JoursOuvresBLL.OneByCalendrier(CalendrierBLL.Default(e.Agence.Societe), Utils.jourSemaine(p.DateDebut), adresse);
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

        public static Presence GetPresence(Employe e, DateTime current_time, bool search_only, string adresse)
        {
            try
            {
                if (e != null ? e.Id > 0 : false)
                {
                    current_time = new DateTime(current_time.Year, current_time.Month, current_time.Day, current_time.Hour, current_time.Minute, 0);
                    //On recherche la fiche de presence a la date debut et la date de fin
                    List<Presence> lr = GetPresences(e, current_time, adresse);
                    if (lr != null ? lr.Count > 0 : false)//Si elle existe
                    {
                        foreach (Presence pe in lr)
                        {
                            bool sortie = false;//Defini la nature du mouvement (entree ou sortie)
                            List<Pointage> lo = PointageBLL.List("select * from yvs_grh_pointage where presence = " + pe.Id + " order by heure_entree desc limit 1", false, adresse);
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
                                if (!sortie)//Si c'est une entree                
                                {
                                    if (current_time > heure_fin)//On controle si le pointage est hors des marges de la fiche
                                    {
                                        // On modifie l'heure et/ou la date de fin prevu de la fiche
                                        TrancheHoraire t = GetTrancheHoraire(e, current_time, adresse);
                                        pe.HeureFinPrevu = t.HeureFin;
                                        pe.DateFinPrevu = Utils.GetTimeStamp(Utils.TimeStamp(pe.DateDebut, pe.HeureDebut), pe.HeureFinPrevu);
                                        if (!search_only)
                                            PresenceBLL.Update(pe, adresse);
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
            }
            catch (Exception ex)
            {
                Utils.Exception(ex);
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

        public static void SynchroniseInfosServeur(Pointeuse p, List<Empreinte> le)
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
                int i = 0;
                switch (p.Type)
                {
                    case Constantes.TYPE_IFACE:
                        foreach (Empreinte m in le)
                        {
                            if (z.SSR_SetUserInfo(p.IMachine, (int)m.Employe.Id, m.Employe.NomPrenom, null, 0, true))//upload user information to the memory
                                i++;
                        }
                        break;
                    default:
                        foreach (Empreinte m in le)
                        {
                            if (z.SetUserInfo(p.IMachine, (int)m.Employe.Id, m.Employe.NomPrenom, null, 0, true))//upload user information to the memory
                                i++;
                        }
                        break;
                }
                if (i > 0)
                {
                    Utils.WriteLog("------ Total synchonisé : " + i.ToString() + " empreinte(s)");
                }
            }
            catch (Exception ex)
            {
                Utils.WriteLog("Fonctions (SynchroniseInfosServeur) : " + ex.Message);
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
                int i = 0;
                switch (p.Type)
                {
                    case Constantes.TYPE_IFACE:
                        i = z.SSR_SetAllTemplate(le, p.IMachine, p.Connecter);
                        break;
                    default:
                        i = z.SetAllTemplate(le, p.IMachine, p.Connecter);
                        break;
                }
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

        public static void SynchroniseFaceOneServeur(Pointeuse p, List<Empreinte> le)
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
                int i = 0;
                switch (p.Type)
                {
                    case Constantes.TYPE_IFACE:
                        i = z.SSR_SetAllFaceTemplate(le, p.IMachine, p.Connecter);
                        break;
                    default:
                        Utils.WriteLog("Les empreintes faciales ne sont pas integrées dans l'appareil " + p.Ip);
                        break;
                }
                if (i > 0)
                {
                    Utils.WriteLog("------ Total synchonisé : " + i.ToString() + " empreinte(s)");
                }
            }
            catch (Exception ex)
            {
                Utils.WriteLog("Fonctions (SynchroniseFaceOneServeur) : " + ex.Message);
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
                List<IOEMDevice> l = new List<IOEMDevice>();
                switch (_pointeuse.Type)
                {
                    case Constantes.TYPE_IFACE:
                        l = z.SSR_GetAllAttentdData(1, _pointeuse.Connecter);
                        break;
                    default:
                        l = z.GetAllAttentdData(1, _pointeuse.Connecter);
                        break;
                }
                SynchroniseServer(l, _pointeuse.Ip, true, null, false, _dateDebut, _dateFin, _invalid_only);
            }
            catch (Exception ex)
            {
                Utils.WriteLog("Fonctions (SynchroniseLogOneServeur) : " + ex.Message);
            }
        }

        public static void SynchroniseLog(Employe e, bool v, bool thread)
        {
            List<Pointeuse> l = PointeuseBLL.List("select * from yvs_pointeuse where societe = " + e.Agence.Societe.Id + " and actif is true order by adresse_ip");
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
                List<IOEMDevice> l = new List<IOEMDevice>();
                switch (_pointeuse.Type)
                {
                    case Constantes.TYPE_IFACE:
                        l = z.SSR_GetAllAttentdData(1, _pointeuse.Connecter, _employe, false, _dateDebut, _dateFin);
                        break;
                    default:
                        l = z.GetAllAttentdData(1, _pointeuse.Connecter, _employe, false, _dateDebut, _dateFin);
                        break;
                }
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
                List<IOEMDevice> l = new List<IOEMDevice>();
                switch (_pointeuse.Type)
                {
                    case Constantes.TYPE_IFACE:
                        l = z.SSR_GetAllAttentdData(1, _pointeuse.Connecter, null, true, _dateDebut, _dateFin);
                        break;
                    default:
                        l = z.GetAllAttentdData(1, _pointeuse.Connecter, null, true, _dateDebut, _dateFin);
                        break;
                }
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

                    List<IOEMDevice> s = new List<IOEMDevice>();
                    switch (_pointeuse.Type)
                    {
                        case Constantes.TYPE_IFACE:
                            s = z.SSR_GetAllAttentdData(1, _pointeuse.Connecter, null, true, _dateDebut, _dateFin);
                            break;
                        default:
                            s = z.GetAllAttentdData(1, _pointeuse.Connecter, null, true, _dateDebut, _dateFin);
                            break;
                    }
                    l.AddRange(s);
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
            List<Pointeuse> l = PointeuseBLL.List("select * from yvs_pointeuse where societe = " + e.Agence.Societe.Id + " and actif is true order by adresse_ip");
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
                List<IOEMDevice> l = new List<IOEMDevice>();
                switch (_pointeuse.Type)
                {
                    case Constantes.TYPE_IFACE:
                        l = z.SSR_GetAllAttentdData(1, _pointeuse.Connecter, null, true, _dateDebut, _dateFin);
                        break;
                    default:
                        l = z.GetAllAttentdData(1, _pointeuse.Connecter, null, true, _dateDebut, _dateFin);
                        break;
                }
                SynchroniseServer(l, _pointeuse.Ip, true, _employe, true, _dateDebut, _dateFin, _invalid_only);
            }
            catch (Exception ex)
            {
                Utils.WriteLog("Fonctions (SynchroniseLogEmployeOneServeurDate) : " + ex.Message);
            }
        }

        public static void SynchroniseServer(List<IOEMDevice> lp, String ip, bool auto, bool filter)
        {
            try
            {
                if (lp.Count > 0)
                {
                    bool synchro = auto;
                    string adresse = Constantes.SOCIETE.AdresseIp;
                    if (parametre == null)
                    {
                        parametre = ParametreBLL.OneBySociete(Constantes.SOCIETE.Id);
                    }
                    if (ip != null ? ip.Trim().Length > 0 : false)
                        Utils.WriteLog("-- Début de la synchronisation des données de pointage de la pointeuse " + ip + " avec le serveur " + (Utils.asString(adresse) ? adresse : "") + ".....");
                    else
                        Utils.WriteLog("-- Début de la synchronisation des données de pointage ds pointeuses avec le serveur " + (Utils.asString(adresse) ? adresse : "") + ".....");

                    int cpt = 0;

                    if (Constantes.PBAR_WAIT != null)
                    {
                        ObjectThread o = new ObjectThread(Constantes.PBAR_WAIT);
                        o.UpdateMaxBar(Constantes.PBAR_WAIT.Maximum + lp.Count);
                    }

                    foreach (IOEMDevice p in lp.FindAll(x => !x.exclure))
                    {
                        Employe employe = EmployeBLL.OneById(p.idwSEnrollNumber, adresse);
                        if (employe != null ? employe.Id > 0 : false)
                        {
                            DateTime heure = new DateTime(p.idwYear, p.idwMonth, p.idwDay, p.idwHour, p.idwMinute, p.idwSecond);
                            DateTime date = heure;
                            bool _suivre = false;
                            while (!_suivre)
                            {
                                if (Fonctions.InsertionPointage((Employe)employe, date, heure, (p.pointeuse != null ? p.pointeuse : null), p.idwInOutMode, adresse))
                                {
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
                        Constantes.LoadPatience(false);
                    }
                    if (ip != null ? ip.Trim().Length > 0 : false)
                    {
                        string file = Chemins.CheminPing() + ip + ".txt";
                        if (File.Exists(file))
                        {
                            try
                            {
                                File.Delete(file);
                            }
                            catch (Exception ex) { }
                        }
                    }
                    if (!auto)
                    {
                        BackupLogData(lp, ip, auto);
                    }
                    else
                    {
                        Constantes.LoadPatience(true);
                    }
                    if (ip != null ? ip.Trim().Length > 0 : false)
                        Utils.WriteLog("-- Fin de la synchronisation des données de pointage de la pointeuse " + ip + " avec le serveur " + (Utils.asString(adresse) ? adresse : "") + ". Nombre de synchronisation " + cpt + "....");
                    else
                        Utils.WriteLog("-- Fin de la synchronisation des données de pointage des pointeuses avec le serveur " + (Utils.asString(adresse) ? adresse : "") + ". Nombre de synchronisation " + cpt + "....");
                }
                else
                {
                    Utils.WriteLog("-- Synchronisation impossible...paramètres incorrects");
                }
            }
            catch (Exception ex)
            {
                Utils.Exception(ex);
            }
        }

        public static void SynchroniseServer(List<IOEMDevice> lp, String ip, bool addEmploye, Employe e, bool addDate, DateTime d, DateTime f, bool invalid)
        {
            SynchroniseServer(lp, ip, true, addEmploye, e, addDate, d, f, invalid);
        }

        public static void SynchroniseServer(List<IOEMDevice> lp, String ip, bool auto, bool addEmploye, Employe e, bool addDate, DateTime d, DateTime f, bool invalid)
        {
            try
            {
                if (lp.Count > 0)
                {
                    string query = "delete from yvs_grh_presence where employe in (select e.id from yvs_grh_employes e inner join yvs_agences a on e.agence = a.id where a.societe = " + Constantes.SOCIETE.Id;
                    if (Constantes.AGENCE != null ? Constantes.AGENCE.Id > 0 : false)
                    {
                        query += " and a.id = " + Constantes.AGENCE.Id;
                    }
                    if (addEmploye ? (e != null ? e.Id > 0 : false) : false)
                    {
                        query += " and e.id = " + e.Id;
                    }
                    query += ")";
                    if (addDate)
                    {
                        query += " and date_debut between '" + d.ToShortDateString() + "' and '" + f.ToShortDateString() + "' and date_fin between '" + d.ToShortDateString() + "' and '" + f.ToShortDateString() + "'";
                    }
                    if (invalid)
                    {
                        query += " and valider = false";
                    }
                    Constantes.EMPLOYES.ForEach(x => x.Presences.Clear());
                    if (Utils.RequeteLibre(query, Constantes.SOCIETE.AdresseIp))
                    {
                        SynchroniseServer(lp, ip, auto, true);
                    }
                }
                else
                {
                    Utils.WriteLog("-- Synchronisation impossible...paramètres incorrects");
                }
            }
            catch (Exception ex)
            {
                Utils.Exception(ex);
            }
        }

        public static List<IOEMDevice> ReorganiseLog(List<IOEMDevice> lp, DateTime dateDebut, DateTime dateFin)
        {
            //List<DateTime> data = Utils.TimeEmployeNotSystem(
            List<IOEMDevice> _lp = new List<IOEMDevice>();
            string adresse = Constantes.SOCIETE.AdresseIp;
            foreach (IOEMDevice io in lp)
            {
                Employe employe = EmployeBLL.OneById(Convert.ToInt32(io.idwSEnrollNumber));
                if (employe != null ? employe.Id > 0 : false)
                {
                    DateTime heure = new DateTime(io.idwYear, io.idwMonth, io.idwDay, io.idwHour, io.idwMinute, 0);
                    string req = "select p.* from yvs_grh_pointage p inner join yvs_grh_presence r on p.presence = r.id where r.employe = " + employe.Id + " and ((heure_entree is not null and heure_entree = '" + heure + "') or (heure_sortie is not null and heure_sortie = '" + heure + "'))";
                    List<Pointage> lo = PointageBLL.List(req, false, adresse);
                    if (lo != null ? lo.Count < 1 : true)
                    {
                        req = "select p.* from yvs_grh_pointage p inner join yvs_grh_presence r on p.presence = r.id where r.employe = " + employe.Id + " and ((heure_entree is not null and heure_entree = '" + heure + "') or (heure_sortie is not null and heure_sortie = '" + heure + "'))";
                        lo = PointageBLL.List(req, false, adresse);
                    }
                }
            }
            return _lp;
        }

        public static List<IOEMDevice> BackupLogDataDevice()
        {
            List<IOEMDevice> list = new List<IOEMDevice>();
            List<Pointeuse> l = PointeuseBLL.List("select * from yvs_pointeuse where actif = true");
            if (l != null ? l.Count > 0 : false)
            {
                Utils.WriteLog("Début de la sauvegarder automatique");
                foreach (Pointeuse p_ in l)
                {
                    Pointeuse p = p_;

                    Appareil z = Utils.ReturnAppareil(p_);
                    Utils.VerifyZkemkeeper(ref z, ref p, 2);
                    if (z != null)
                    {
                        p.Zkemkeeper = z;
                        List<IOEMDevice> le = new List<IOEMDevice>();
                        switch (p.Type)
                        {
                            case Constantes.TYPE_IFACE:
                                le = z.SSR_GetAllAttentdData(p.IMachine, p.Connecter);
                                break;
                            default:
                                le = z.GetAllAttentdData(p.IMachine, p.Connecter);
                                break;
                        }
                        BackupLogData(le, p.Ip, true);
                        list.AddRange(le);
                    }
                }
                Utils.WriteLog("Fin de la sauvegarder automatique");
            }
            return list;
        }

        public static void BackupLogData(List<IOEMDevice> lp, String ip, bool auto)
        {
            if (ip != null ? ip.Trim().Length > 0 : false)
            {
                List<Appareil> aps = new List<Appareil>();
                if (lp.Count > 0)
                {
                    if (Constantes.PBAR_WAIT != null)
                    {
                        ObjectThread o = new ObjectThread(Constantes.PBAR_WAIT);
                        o.UpdateMaxBar(Constantes.PBAR_WAIT.Maximum + lp.Count);
                    }

                    Utils.WriteLog((auto ? "--" : "") + "Début de la sauvegarde des opérations enrégistrées dans la pointeuse " + ip + " .....");
                    String backup = TOOLS.Chemins.CheminBackup(ip) + DateTime.Now.ToString("dd-MM-yyyy") + ".csv";
                    if (File.Exists(backup))
                    {
                        File.Delete(backup);
                    }
                    for (int i = 0; i < lp.Count; i++)
                    {
                        Logs.WriteCsv(backup, lp[i]);
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
                    DateTime _last = DateTime.Now;
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
            if (pointeuses.Count > 0)
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
                f = z._FINGER;
                if (z.EnableDevice(z._I_MACHINE_NUMBER, bEnabled))
                {
                    Cursor c = Cursors.WaitCursor;
                    if (z.ReadAllTemplate(z._I_MACHINE_NUMBER))
                    {
                        switch (z._POINTEUSE.Type)
                        {
                            case Constantes.TYPE_IFACE:
                                {
                                    if (z.SSR_SetUserInfo(z._I_MACHINE_NUMBER, (int)z._EMPLOYE.Id, z._EMPLOYE.NomPrenom, null, 0, bEnabled))//upload user information to the memory
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
                                    break;
                                }
                            default:
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
                                    break;
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
                Utils.WriteLog("-- Empreinte du Doigt (" + f.Doigt + ") de la Main (" + f.Main + ") de l'employé " + z._EMPLOYE.Nom + " " + z._EMPLOYE.Prenom + " incomplète!");
            }
            else
            {
                Utils.WriteLog("-- Empreinte du Doigt (" + f.Doigt + ") de la Main (" + f.Main + ") de l'employé " + z._EMPLOYE.Nom + " " + z._EMPLOYE.Prenom + " sauvegardé!");
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
            string query = "select * from yvs_pointeuse where adresse_ip != '" + _ip.Trim() + "' and actif = true and societe = " + e.Agence.Societe.Id + " order by adresse_ip";
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
                    switch (p.Type)
                    {
                        case Constantes.TYPE_IFACE:
                            z.SSR_SetAllTemplate(l, z._I_MACHINE_NUMBER, p.Connecter);
                            break;
                        default:
                            z.SetAllTemplate(l, z._I_MACHINE_NUMBER, p.Connecter);
                            break;
                    }
                }
            }
        }

        public static void ImporteFileInDataBase(Pointeuse pointeuse, string fileName)
        {
            try
            {
                ImporteFileInDataBase(pointeuse, fileName, null);
            }
            catch (Exception ex)
            {
                Messages.Exception(ex);
            }
        }

        public static void ImporteFileInDataBase(Pointeuse pointeuse, string fileName, List<IOEMDevice> logs)
        {
            try
            {
                Utils.WriteLog("-- Début de l'importation du fichier tampon de la pointeuse " + pointeuse.Ip + " dans la base de données");
                IOEMDeviceBLL.Delete(pointeuse, Constantes.SOCIETE.AdresseIp);
                string adresse = null;
                if (Utils.asString(Constantes.SOCIETE.AdresseIp))
                {
                    adresse = Constantes.SOCIETE.AdresseIp;
                }
                else
                {
                    ENTITE.Serveur bean = ServeurBLL.ReturnServeur();
                    adresse = bean.Adresse;
                }
                if (!Utils.IsLocalAdress(adresse))
                {
                    fileName = new RemoteAcces(adresse, Constantes.SOCIETE.Port, Constantes.SOCIETE.TypeConnexion, Constantes.SOCIETE.Users, Constantes.SOCIETE.Password).GetPathFile(@fileName);
                }
                if (Utils.asString(fileName))
                {
                    string query = @"COPY yvs_grh_ioem_device(machine, employe, verify_mode, in_out_mode, work_code, reserved, date_action, time_action, date_time_action, pointeuse) " +
                                   " FROM '" + fileName + "' DELIMITER ';' CSV;";
                    Bll.RequeteLibre(query, adresse);
                    Utils.WriteLog("-- Fin de l'importation du fichier tampon de la pointeuse " + pointeuse.Ip + " dans la base de données");
                }
                else
                {
                    Utils.WriteLog("-- Echec de l'importation du fichier tampon de la pointeuse " + pointeuse.Ip + " dans la base de données");
                }
            }
            catch (Exception ex)
            {
                Messages.Exception(ex);
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
            Utils.WriteLog("-- Début du chargerment du fichier tampon de la pointeuse " + p.Ip + "");
            p.FileLoad = false;
            switch (p.Type)
            {
                case Constantes.TYPE_IFACE:
                    p.Logs = z.SSR_GetAllAttentdData(1, p.Connecter);
                    break;
                default:
                    p.Logs = z.GetAllAttentdData(1, p.Connecter);
                    break;
            }
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
            foreach (IOEMDevice io in p.Logs)
            {
                Logs.WriteCsv(fileBack, io);
            }
            ImporteFileInDataBase(p, fileBack, p.Logs);
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
            new Thread(delegate()
            {
                foreach (Serveur serveur in LiaisonBLL.ReturnServeur())
                {
                    SynchroniseCreneau(serveur);
                }
            }).Start();
            Utils.WriteLog("-- Fin du chargerment du fichier tampon de la pointeuse " + p.Ip + "");
        }

        public static void CreateJobBackup()
        {
            DateTime endDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 30, 59);
            Scheduler job = new Scheduler(1, 0, 0, 0);
            job.Type = TypeScheduler.TYPE_EVERY_TIME;
            job.TimeExecution = endDay;
            job.Start(new Scheduler.Fonction(BackupDevice));
        }

        private static void BackupDevice()
        {
            new Thread(delegate() { BackupLogDataDevice(); }).Start();
        }

        public static void CreateJobBackupAndSynchronise()
        {
            Scheduler job = new Scheduler(1, 0, 0, 0);
            job.Type = TypeScheduler.TYPE_EVERY_TIME;
            job.TimeExecution = Constantes.SETTING.TimeSynchroAuto;
            job.Start(new Scheduler.Fonction(BackupAndSynchronise));
        }

        public static void BackupAndSynchronise()
        {
            List<IOEMDevice> list = BackupLogDataDevice();
            DateTime debut = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-1).Day, 23, 59, 59);
            DateTime fin = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            //List<IOEMDevice> list = Logs.ReadCsv(Chemins.CheminBackup("192.168.30.231") + "25-10-2017.csv");
            //DateTime debut = new DateTime(2017, 10, 24, 23, 59, 59);
            //DateTime fin = new DateTime(2017, 10, 25, 23, 59, 59);
            list = Utils.FindLogsInFileTamponLogs(list, false, null, true, debut, fin);
            list.Sort();
            SynchroniseServer(list, "", false, null, true, debut, fin, true);
        }

        public static bool SynchroniseCreneau(long employe, DateTime date, Serveur serveur)
        {
            try
            {
                Npgsql.NpgsqlConnection connect = new Connexion().returnConnexion(serveur, false);
                try
                {
                    if (connect == null)
                    {
                        return false;
                    }
                    bool tableUsers = Utils.VerifyTable("users", connect);
                    bool tableTranche = Utils.VerifyTable("tranchehoraire", connect);
                    bool colonneUsersExterne = Utils.VerifyColumn("users", "externe", connect);
                    bool colonneTrancheExterne = Utils.VerifyColumn("tranchehoraire", "externe", connect);
                    bool colonneCreneauExterne = Utils.VerifyColumn("creneauhoraire", "externe", connect);
                    if ((tableUsers ? !colonneUsersExterne : true) || (tableTranche ? !colonneTrancheExterne : true))
                    {
                        return true;
                    }
                    string query = "SELECT c.id, t.externe, c.datedebut, COALESCE(c.datefin, c.datedebut) AS datefin, c.actif FROM creneauhoraire c INNER JOIN users u ON c.users = u.coderep INNER JOIN creneaudepot d ON c.creneau = d.id INNER JOIN tranchehoraire t ON d.tranche = t.id WHERE u.externe = " + employe + " AND '" + date.ToString("dd-MM-yyyy") + "' BETWEEN c.datedebut AND COALESCE(c.datefin, c.datedebut) AND t.externe IS NOT NULL";
                    Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(query, connect);
                    Npgsql.NpgsqlDataReader lect = cmd.ExecuteReader();
                    if (lect.HasRows)
                    {
                        Planning planning;
                        while (lect.Read())
                        {
                            planning = new Planning();
                            planning.Employe = employe;
                            planning.Tranche = new TrancheHoraire(Convert.ToInt32(lect["externe"] != null ? lect["externe"].ToString() : "0"));
                            planning.DateDebut = (DateTime)((lect["datedebut"] != null) ? (!lect["datedebut"].ToString().Trim().Equals("") ? lect["datedebut"] : DateTime.Now) : DateTime.Now);
                            planning.DateFin = (DateTime)((lect["datefin"] != null) ? (!lect["datefin"].ToString().Trim().Equals("") ? lect["datefin"] : DateTime.Now) : DateTime.Now);
                            planning.Valide = Convert.ToBoolean((lect["actif"].ToString() != "") ? lect["actif"].ToString() : "false");
                            if (planning.Tranche != null ? planning.Tranche.Id > 0 : false)
                            {
                                Planning y = PlanningBLL.OneByDateEmploye(employe, planning.Tranche.Id, date);
                                if (y != null ? y.Id < 1 : true)
                                {
                                    PlanningBLL.Insert(planning);
                                    if (colonneCreneauExterne)
                                        y = PlanningBLL.OneByDateEmploye(employe, planning.Tranche.Id, date);
                                }
                                if (colonneCreneauExterne ? (y != null ? y.Id > 0 : false) : false)
                                {
                                    int id = Convert.ToInt32(lect["id"] != null ? lect["id"].ToString() : "0");
                                    query = "UPDATE creneauhoraire SET externe = " + y.Id + " WHERE id = " + id;
                                    cmd = new Npgsql.NpgsqlCommand(query, connect);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Utils.Exception(ex);
                }
                finally
                {
                    Connexion.Close(connect);
                }
            }
            catch (Exception ex)
            {
                Utils.Exception(ex);
            }
            return false;
        }

        public static bool SynchroniseCreneau(Serveur serveur)
        {
            Utils.WriteLog("-- Début d'importation des planning employés du serveur " + serveur.Adresse + " à partir du " + serveur.DateDebut.ToString("dd-MM-yyy"));
            Npgsql.NpgsqlConnection connect = null, second = null;
            Npgsql.NpgsqlCommand cmd = null, update = null;
            Npgsql.NpgsqlDataReader lect = null;
            try
            {
                connect = new Connexion().returnConnexion(serveur, false);
                second = new Connexion().returnConnexion(serveur, false);
                if (connect == null)
                {
                    return false;
                }
                bool tableUsers = Utils.VerifyTable("users", connect);
                if (!tableUsers)
                {
                    Utils.WriteLog("-- Le serveur " + serveur.Adresse + " ne possède pas la table users");
                    return true;
                }
                bool tableTranche = Utils.VerifyTable("tranchehoraire", connect);
                if (!tableTranche)
                {
                    Utils.WriteLog("-- Le serveur " + serveur.Adresse + " ne possède pas la table tranchehoraire");
                    return true;
                }
                bool tableCreneau = Utils.VerifyTable("creneauhoraire", connect);
                if (!tableCreneau)
                {
                    Utils.WriteLog("-- Le serveur " + serveur.Adresse + " ne possède pas la table creneauhoraire");
                    return true;
                }
                bool colonneUsersExterne = Utils.VerifyColumn("users", "externe", connect);
                if (!colonneUsersExterne)
                {
                    Utils.WriteLog("-- Le table users ne possède pas la colonne externe sur le serveur " + serveur.Adresse);
                    return true;
                }
                bool colonneTrancheExterne = Utils.VerifyColumn("tranchehoraire", "externe", connect);
                if (!colonneTrancheExterne)
                {
                    Utils.WriteLog("-- Le table tranchehoraire ne possède pas la colonne externe sur le serveur " + serveur.Adresse);
                    return true;
                }
                bool colonneCreneauExterne = Utils.VerifyColumn("creneauhoraire", "externe", connect);
                if (!colonneCreneauExterne)
                {
                    Utils.WriteLog("-- Le table creneauhoraire ne possède pas la colonne externe sur le serveur " + serveur.Adresse);
                    return true;
                }
                string query = "SELECT c.id, u.externe AS users, t.externe AS tranche, c.datedebut, COALESCE(c.datefin, c.datedebut) AS datefin, c.actif FROM creneauhoraire c INNER JOIN users u ON c.users = u.coderep INNER JOIN creneaudepot d ON c.creneau = d.id INNER JOIN tranchehoraire t ON d.tranche = t.id WHERE c.datedebut >= '" + serveur.DateDebut + "' AND c.externe IS NULL AND u.externe IS NOT NULL AND t.externe IS NOT NULL";
                cmd = new Npgsql.NpgsqlCommand(query, connect);
                lect = cmd.ExecuteReader();
                if (lect.HasRows)
                {
                    Planning planning;
                    while (lect.Read())
                    {
                        planning = new Planning();
                        planning.Employe = Convert.ToInt32(lect["users"] != null ? lect["users"].ToString() : "0");
                        planning.Tranche = new TrancheHoraire(Convert.ToInt32(lect["tranche"] != null ? lect["tranche"].ToString() : "0"));
                        planning.DateDebut = (DateTime)((lect["datedebut"] != null) ? (!lect["datedebut"].ToString().Trim().Equals("") ? lect["datedebut"] : DateTime.Now) : DateTime.Now);
                        planning.DateFin = (DateTime)((lect["datefin"] != null) ? (!lect["datefin"].ToString().Trim().Equals("") ? lect["datefin"] : DateTime.Now) : DateTime.Now);
                        planning.Valide = Convert.ToBoolean((lect["actif"].ToString() != "") ? lect["actif"].ToString() : "false");
                        if (planning.Tranche != null ? planning.Tranche.Id > 0 : false)
                        {
                            Planning y = PlanningBLL.OneByDateEmploye(planning.Employe, planning.Tranche.Id, planning.DateDebut, planning.DateFin);
                            if (y != null ? y.Id < 1 : true)
                            {
                                PlanningBLL.Insert(planning);
                                y = PlanningBLL.OneByDateEmploye(planning.Employe, planning.Tranche.Id, planning.DateDebut, planning.DateFin);
                            }
                            if (y != null ? y.Id > 0 : false)
                            {
                                int id = Convert.ToInt32(lect["id"] != null ? lect["id"].ToString() : "0");
                                query = "UPDATE creneauhoraire SET externe = " + y.Id + " WHERE id = " + id;
                                Bll.RequeteLibre(second, query);
                            }
                        }
                    }
                }
                Utils.WriteLog("-- Fin d'importation des planning employés du serveur " + serveur.Adresse + " à partir du " + serveur.DateDebut);
                return true;
            }
            catch (Exception ex)
            {
                Utils.Exception(ex);
                Utils.WriteLog("-- Echec d'importation des planning employés du serveur " + serveur.Adresse + " à partir du " + serveur.DateDebut);
            }
            finally
            {
                Connexion.Close(connect);
                Connexion.Close(second);
                if (cmd != null)
                    cmd.Dispose();
                if (lect != null)
                    lect.Dispose();
            }
            return false;
        }

    }
}
