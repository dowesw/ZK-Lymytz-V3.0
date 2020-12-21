using System;
using System.Collections.Generic;
using System.Text;

namespace ZK_Lymytz.ENTITE
{
    public class Poste
    {

        public Poste()
        {

        }

        public Poste(Int32 id)
        {
            this.id = id;
        }

        private Int32 id;
        public Int32 Id
        {
            get { return id; }
            set { id = value; }
        }

        private string intitule;
        public string Intitule
        {
            get { return intitule; }
            set { intitule = value; }
        }
    }
}
