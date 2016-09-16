using System;
using System.Collections.Generic;
using System.Text;

namespace ZK_Lymytz.ENTITE
{
    public class Poste
    {
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
