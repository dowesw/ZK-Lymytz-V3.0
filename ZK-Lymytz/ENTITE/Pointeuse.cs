using System;
using System.Collections.Generic;
using System.Text;

using ZK_Lymytz.TOOLS;

namespace ZK_Lymytz.ENTITE
{
    public class Pointeuse
    {
        private int id;
        private string ip;
        private int port;
        private int i_machine;
        private string description;
        private string emplacement;
        private bool connecter;
        private bool actif;
        private int societe;
        private Appareil zkemkeeper = null;

        internal Appareil Zkemkeeper
        {
            get { return zkemkeeper; }
            set { zkemkeeper = value; }
        }

        public int Societe
        {
            get { return societe; }
            set { societe = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Ip
        {
            get { return ip != null ? ip : "192.168.1.201"; }
            set { ip = value; }
        }

        public string Description
        {
            get { return description != null ? description : ""; }
            set { description = value; }
        }

        public string Emplacement
        {
            get { return emplacement != null ? emplacement : ""; }
            set { emplacement = value; }
        }

        public int Port
        {
            get { return port > 0 ? port : 4370; }
            set { port = value; }
        }

        public int IMachine
        {
            get { return i_machine; }
            set { i_machine = value; }
        }

        public bool Connecter
        {
            get { return zkemkeeper!=null; }
            set { connecter = value; }
        }

        public bool Actif
        {
            get { return actif; }
            set { actif = value; }
        }

        public static Pointeuse Defaut(){
            Pointeuse p = new Pointeuse();
            p.id = 3;
            p.Ip = "192.168.30.236";
            p.Port = 4370;
            return p;
        }
    }
}
