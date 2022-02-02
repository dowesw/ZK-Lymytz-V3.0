using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZK_Lymytz.ENTITE
{
    [Serializable]
    public class Groupe
    {

        public Groupe() { }

        public Groupe(int id)
        {
            this.id = id;
        }

        public Groupe(int id, string libelle)
            : this(id)
        {
            this.libelle = libelle;
        }

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string libelle;
        public string Libelle
        {
            get { return libelle != null ? libelle : ""; }
            set { libelle = value; }
        }
    }
}
