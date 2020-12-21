using System;
using System.Collections.Generic;
using System.Text;
using ZK_Lymytz.TOOLS;

namespace ZK_Lymytz.ENTITE
{
    [Serializable]
    public class Societe
    {

        public Societe() { }

        public Societe(int id)
        {
            this.id = id;
        }

        public Societe(int id, string name)
            : this(id)
        {
            this.name = name;
        }

        public Societe(int id, string name, int groupe, string adresseIp)
            : this(id, name)
        {
            this.groupe = groupe;
            this.adresseIp = adresseIp;
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
            get { return name != null ? name : ""; }
            set { name = value; }
        }

        private string adresseIp;
        public string AdresseIp
        {
            get { return adresseIp != null ? adresseIp : ""; }
            set { adresseIp = value; }
        }

        private int port;
        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        private string users;
        public string Users
        {
            get { return users != null ? users : ""; }
            set { users = value; }
        }

        private string password;
        public string Password
        {
            get { return password != null ? password : ""; }
            set { password = value; }
        }

        private string domain;
        public string Domain
        {
            get { return domain != null ? domain : ""; }
            set { domain = value; }
        }

        private string typeConnexion = "DESKTOP";
        public string TypeConnexion
        {
            get { return Utils.asString(typeConnexion) ? typeConnexion : "DESKTOP"; }
            set { typeConnexion = value; }
        }

        private int groupe;
        public int Groupe
        {
            get { return groupe; }
            set { groupe = value; }
        }

        public string NameComplet
        {
            get { return name + (Utils.asString(adresseIp) ? (" [" + adresseIp + "]") : " [Courante]"); }
            set { }
        }
    }
}
