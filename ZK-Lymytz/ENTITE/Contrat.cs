using System;
using System.Collections.Generic;
using System.Text;

namespace ZK_Lymytz.ENTITE
{
    public class Contrat
    {
        private long id;
        private string reference;
        private Calendrier calendrier = new Calendrier();

        public long Id
        {
            get { return id; }
            set { id = value; }
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
            get { return typeTranche != null ? typeTranche : "JN"; }
            set { typeTranche = value; }
        }
    }
}
