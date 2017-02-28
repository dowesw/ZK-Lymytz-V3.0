using System;
using System.Collections.Generic;
using System.Text;

using ZK_Lymytz.TOOLS;

namespace ZK_Lymytz.ENTITE
{
    public class Pointage
    {

        private long id;
        private Presence presence = new Presence();
        private bool entreeNull = false;
        private DateTime heure_entree;
        private bool sortieNull = false;
        private DateTime heure_sortie;
        private bool valider;
        private Pointeuse pointeuse_in = new Pointeuse();
        private Pointeuse pointeuse_out = new Pointeuse();
        private bool system_in;
        private bool system_out;
        private bool supplementaire;
        private double duree;

        public double Duree
        {
            get { return ((heure_sortie - heure_entree).TotalHours) > 0 ? ((heure_sortie - heure_entree).TotalHours) : 0; }
            set { }
        }

        public bool SortieNull
        {
            get { return sortieNull; }
            set { sortieNull = value; }
        }

        public bool EntreeNull
        {
            get { return entreeNull; }
            set { entreeNull = value; }
        }

        public bool Supplementaire
        {
            get { return supplementaire; }
            set { supplementaire = value; }
        }

        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        public bool SystemOut
        {
            get { return system_out; }
            set { system_out = value; }
        }

        public bool SystemIn
        {
            get { return system_in; }
            set { system_in = value; }
        }

        public Presence Presence
        {
            get { return presence; }
            set { presence = value; }
        }

        public DateTime HeureEntree
        {
            get { return heure_entree; }
            set { heure_entree = value; }
        }

        public Object _HeureEntree()
        {
            if (heure_entree.ToString() != "01/01/0001 00:00:00")
                return heure_entree;
            else
                return null;
        }

        public DateTime HeureSortie
        {
            get { return heure_sortie; }
            set { heure_sortie = value; }
        }

        public Object _HeureSortie()
        {
            if (heure_sortie.ToString() != "01/01/0001 00:00:00")
                return heure_sortie;
            else
                return null;
        }

        public bool Valider
        {
            get { return valider; }
            set { valider = value; }
        }

        public Pointeuse PointeuseIn
        {
            get { return pointeuse_in; }
            set { pointeuse_in = value; }
        }

        public Pointeuse PointeuseOut
        {
            get { return pointeuse_out; }
            set { pointeuse_out = value; }
        }
    }
}
