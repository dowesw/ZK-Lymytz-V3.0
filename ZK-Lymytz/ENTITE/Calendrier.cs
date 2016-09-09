using System;
using System.Collections.Generic;
using System.Text;

namespace ZK_Lymytz.ENTITE
{
    public class Calendrier
    {

        private int id;
        private string reference;
        private List<JoursOuvres> joursOuvres = new List<JoursOuvres>();

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Reference
        {
            get { return reference; }
            set { reference = value; }
        }

        public List<JoursOuvres> JoursOuvres
        {
            get { return joursOuvres; }
            set { joursOuvres = value; }
        }
    }
}
