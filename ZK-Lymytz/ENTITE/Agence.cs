using System;
using System.Collections.Generic;
using System.Text;

namespace ZK_Lymytz.ENTITE
{
    [Serializable]
    public class Agence
    {
        public Agence() { }

        public Agence(int id)
        {
            this.id = id;
        }

        public Agence(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

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

        private Societe societe = new Societe();

        public Societe Societe
        {
            get { return societe != null ? societe.Id > 0 ? societe : TOOLS.Constantes.SOCIETE : TOOLS.Constantes.SOCIETE; }
            set { societe = value; }
        }
    }
}
