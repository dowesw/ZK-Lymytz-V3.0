using System;
using System.Collections.Generic;
using System.Text;

namespace ZK_LymytzService.ENTITE
{
    public class Presence
    {
        private long id;
        private Employe employe = new Employe();
        private DateTime date_debut;
        private DateTime heure_debut;
        private DateTime date_fin;
        private DateTime heure_fin;
        private DateTime duree_pause;
        private DateTime marge_approuve;
        private bool valider;

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
