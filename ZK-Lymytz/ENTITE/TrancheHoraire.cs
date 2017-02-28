using System;
using System.Collections.Generic;
using System.Text;

namespace ZK_Lymytz.ENTITE
{
    public class TrancheHoraire
    {

        public TrancheHoraire()
        {

        }

        public TrancheHoraire(long id)
        {
            this.id = id;
        }

        public TrancheHoraire(long id, DateTime heure_debut, DateTime heure_fin)
        {
            this.id = id;
            this.heure_debut = heure_debut;
            this.heure_fin = heure_fin;
            this.duree_pause = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 2, 0, 0);
        }

        private long id;
        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        private string titre;
        public string Titre
        {
            get { return titre; }
            set { titre = value; }
        }

        private DateTime heure_debut;
        public DateTime HeureDebut
        {
            get { return heure_debut; }
            set { heure_debut = value; }
        }

        private DateTime heure_fin;
        public DateTime HeureFin
        {
            get { return heure_fin; }
            set { heure_fin = value; }
        }

        private string type_journee;
        public string TypeJournee
        {
            get { return type_journee; }
            set { type_journee = value; }
        }

        private DateTime duree_pause;
        public DateTime DureePause
        {
            get { return duree_pause; }
            set { duree_pause = value; }
        }
    }
}
