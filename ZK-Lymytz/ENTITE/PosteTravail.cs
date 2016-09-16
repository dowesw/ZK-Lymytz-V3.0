using System;
using System.Collections.Generic;
using System.Text;

namespace ZK_Lymytz.ENTITE
{
    public class PosteTravail
    {
        private Int32 id;
        public Int32 Id
        {
            get { return id; }
            set { id = value; }
        }

        private Poste poste = new Poste();
        public Poste Poste
        {
            get { return poste; }
            set { poste = value; }
        }
    }
}
