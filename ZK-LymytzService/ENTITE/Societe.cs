using System;
using System.Collections.Generic;
using System.Text;

namespace ZK_LymytzService.ENTITE
{
    [Serializable]
    public class Societe
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Societe() { }

        public Societe(int id)
        {
            this.id = id;
        }

        public Societe(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
