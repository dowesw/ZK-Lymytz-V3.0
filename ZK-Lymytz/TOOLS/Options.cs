using System;
using System.Collections.Generic;
using System.Text;

namespace ZK_Lymytz.TOOLS
{
    class Options
    {
        private object valeur;
        private string libelle;

        public Options(object valeur, string libelle) { this.valeur = valeur; this.libelle = libelle; }

        public string Libelle
        {
            get { return libelle; }
            set { libelle = value; }
        }

        public object Valeur
        {
            get { return valeur; }
            set { valeur = value; }
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (this.GetType() != obj.GetType())
            {
                return false;
            }
            Options other = (Options)obj;
            if (this.valeur != other.valeur)
            {
                return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            int hash = 7;
            hash = 71 * hash + Utils.hashCode(this.valeur);
            return hash;
        }
    }
}
