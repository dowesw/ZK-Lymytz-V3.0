using System;
using System.Collections.Generic;
using System.Text;

namespace ZK_Lymytz.ENTITE
{
    public class Planning
    {

        private long id;
        private DateTime date_debut;
        private DateTime date_fin;
        private TrancheHoraire tranche = new TrancheHoraire();
        private bool valide;
        private bool supplementaire = false;

        public bool Chevauche
        {
            get
            {
                if (DateDebut != null && DateFin !=null)
                {
                    return DateFin > DateDebut;
                }
                return false;
            }
            set { }
        }

        public bool Supplementaire
        {
            get { return supplementaire; }
            set { supplementaire = value; }
        }

        public bool Valide
        {
            get { return valide; }
            set { valide = value; }
        }

        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        public DateTime DureePause
        {
            get { return tranche.DureePause; }
            set { tranche.DureePause = value; }
        }

        public DateTime HeureDebut
        {
            get { return tranche.HeureDebut; }
            set { tranche.HeureDebut = value; }
        }

        public DateTime HeureFin
        {
            get { return tranche.HeureFin; }
            set { tranche.HeureFin = value; }
        }

        public DateTime DateDebut
        {
            get { return date_debut; }
            set { date_debut = value; }
        }

        public DateTime DateFin
        {
            get { return date_fin; }
            set { date_fin = value; }
        }

        internal TrancheHoraire Tranche
        {
            get { return tranche; }
            set { tranche = value; }
        }
    }
}
