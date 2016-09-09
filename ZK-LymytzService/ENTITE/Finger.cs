using System;
using System.Collections.Generic;
using System.Text;

namespace ZK_LymytzService.ENTITE
{
    public class Finger
    {
        public Finger()
        {

        }

        public Finger(int index, string main, string doigt)
        {
            this.index = index;
            this.doigt = doigt;
            this.main = main;
        }

        private int index;

        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        private string doigt;

        public string Doigt
        {
            get { return doigt; }
            set { doigt = value; }
        }

        private string main;

        public string Main
        {
            get { return main; }
            set { main = value; }
        }

        public static Finger Get(int finger)
        {
            return TOOLS.Constantes.FINGERS().Find(x => x.index == finger);
        }
    }
}
