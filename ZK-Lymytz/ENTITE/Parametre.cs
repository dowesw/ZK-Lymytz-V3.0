using System;
using System.Collections.Generic;
using System.Text;

namespace ZK_Lymytz.ENTITE
{
    public class Parametre
    {
        private long id;
        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        private bool planningDynamique;
        public bool PlanningDynamique
        {
            get { return planningDynamique; }
            set { planningDynamique = value; }
        }

        private DateTime margeDebutTravail;
        public DateTime TimeMargeAvance
        {
            get { return margeDebutTravail; }
            set { margeDebutTravail = value; }
        }

        private DateTime timeMargeAutorise;
        public DateTime TimeMargeAutorise
        {
            get { return timeMargeAutorise; }
            set { timeMargeAutorise = value; }
        }

        private DateTime timeMargeRetard;
        public DateTime TimeMargeRetard
        {
            get { return timeMargeRetard; }
            set { timeMargeRetard = value; }
        }

        public bool InvalideTimeRetard
        {
            get { return (timeMargeRetard != null) ? ((timeMargeRetard.ToString() != "01/01/0001 00:00:00") ? !timeMargeRetard.ToShortTimeString().Equals("00:00:00") : false) : false; }
        }
    }
}
