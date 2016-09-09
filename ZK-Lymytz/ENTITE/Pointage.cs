using System;
using System.Collections.Generic;
using System.Text;

namespace ZK_Lymytz.ENTITE
{
    class Pointage
    {

        private long id;
        private Presence presence = new Presence();
        private DateTime heure_entree;
        private DateTime heure_sortie;
        private bool valider;
        private Pointeuse pointeuse_in = new Pointeuse();
        private Pointeuse pointeuse_out = new Pointeuse();
        private bool system_in;
        private bool system_out;
        private bool supplementaire;

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

        public DateTime HeureSortie
        {
            get { return heure_sortie; }
            set { heure_sortie = value; }
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
