using System;
using System.Collections.Generic;
using System.Text;

using ZK_Lymytz.TOOLS;

namespace ZK_Lymytz.ENTITE
{
    public class Presence
    {
        public Presence() { }

        public Presence(long id)
        {
            this.id = id;
        }

        private long id;
        private Employe employe = new Employe();
        private DateTime date_debut;
        private DateTime heure_debut;
        private DateTime date_fin;
        private DateTime heure_fin;
        private DateTime date_fin_prevu;
        private DateTime heure_fin_prevu;
        private DateTime duree_pause;
        private DateTime marge_approuve;
        private double total_presence;
        private double total_supplementaire;
        private bool valider;
        private bool supplementaire;
        private List<Pointage> pointages = new List<Pointage>();
        private List<string> pointeuses = new List<string>();

        public List<string> Pointeuses
        {
            get { return pointeuses; }
            set { pointeuses = value; }
        }

        internal List<Pointage> Pointages
        {
            get { return pointages; }
            set { pointages = value; }
        }

        public DateTime DateFinPrevu
        {
            get { return date_fin_prevu; }
            set { date_fin_prevu = value; }
        }

        public DateTime HeureFinPrevu
        {
            get { return heure_fin_prevu; }
            set { heure_fin_prevu = value; }
        }

        public bool Chevauche
        {
            get
            {
                if (DateDebut != null && DateFin != null)
                {
                    return DateFin > DateDebut;
                }
                return false;
            }
            set { }
        }

        public double TotalSupplementaire
        {
            get { return total_supplementaire; }
            set { total_supplementaire = value; }
        }

        public double TotalPresence
        {
            get { return total_presence; }
            set { total_presence = value; }
        }

        public bool Supplementaire
        {
            get { return supplementaire; }
            set { supplementaire = value; }
        }

        public DateTime MargeApprouve
        {
            get { return marge_approuve; }
            set { marge_approuve = value; }
        }

        public DateTime DureePause
        {
            get { return duree_pause; }
            set { duree_pause = value; }
        }

        public bool Valider
        {
            get { return valider; }
            set { valider = value; }
        }

        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        public Employe Employe
        {
            get { return employe; }
            set { employe = value; }
        }

        public DateTime DateDebut
        {
            get { return date_debut; }
            set { date_debut = value; }
        }

        public DateTime HeureDebut
        {
            get { return heure_debut; }
            set { heure_debut = value; }
        }

        public DateTime DateFin
        {
            get { return date_fin; }
            set { date_fin = value; }
        }

        public DateTime HeureFin
        {
            get { return heure_fin; }
            set { heure_fin = value; }
        }
    }
}
