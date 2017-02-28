using System;
using System.Collections.Generic;
using System.Text;

namespace ZK_Lymytz.ENTITE
{
    public class Contrat
    {
        private long id;
        private string reference;
        private bool horaire_dynamique;
        private Calendrier calendrier = new Calendrier();

        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        public bool HoraireDynamique
        {
            get { return horaire_dynamique; }
            set { horaire_dynamique = value; }
        }

        public string Reference
        {
            get { return reference; }
            set { reference = value; }
        }

        public Calendrier Calendrier
        {
            get { return calendrier; }
            set { calendrier = value; }
        }

        private string typeTranche;
        public string TypeTranche
        {
            get { return typeTranche != null ? (typeTranche.Trim().Length > 0 ? typeTranche : "JN") : "JN"; }
            set { typeTranche = value; }
        }
    }
}
