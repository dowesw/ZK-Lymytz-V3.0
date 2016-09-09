using System;
using System.Collections.Generic;
using System.Text;

namespace ZK_Lymytz.ENTITE
{
    public class JoursOuvres
    {

        private int id;
        private string jour;
        private DateTime heure_debut_travail;
        private DateTime heure_fin_travail;
        private DateTime duree_pause;
        private DateTime heure_debut_pause;
        private DateTime heure_fin_pause;
        private bool ouvrable;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Jour
        {
            get { return jour; }
            set { jour = value; }
        }

        public DateTime DureePause
        {
            get { return duree_pause; }
            set { duree_pause = value; }
        }

        public DateTime HeureDebutTravail
        {
            get { return heure_debut_travail; }
            set { heure_debut_travail = value; }
        }

        public DateTime HeureFinTravail
        {
            get { return heure_fin_travail; }
            set { heure_fin_travail = value; }
        }

        public DateTime HeureDebutPause
        {
            get { return heure_debut_pause; }
            set { heure_debut_pause = value; }
        }

        public DateTime HeureFinPause
        {
            get { return heure_fin_pause; }
            set { heure_fin_pause = value; }
        }

        public bool Ouvrable
        {
            get { return ouvrable; }
            set { ouvrable = value; }
        }
    }
}
