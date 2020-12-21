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
        private string type = "TFT";
        private Int64 societe;
        private Int64 agence;
        private bool multiSociete;
        private Appareil zkemkeeper = null;
        private List<IOEMDevice> logs = new List<IOEMDevice>();
        private bool tampon, fileLoad;

        public Pointeuse() { }

        public Pointeuse(int id) { this.id = id; }

        public Pointeuse(string ip) { this.ip = ip; }

        internal Appareil Zkemkeeper
        {
            get { return zkemkeeper; }
            set { zkemkeeper = value; }
        }

        public List<IOEMDevice> Logs
        {
            get { return logs; }
            set { logs = value; }
        }

        public string Type
        {
            get
            {
                return type != null ? type.Trim().Length > 0 ? type : "TFT" : "TFT";
            }
            set { type = value; }
        }

        public bool FileLoad
        {
            get { return fileLoad; }
            set { fileLoad = value; }
        }

        public bool MultiSociete
        {
            get { return multiSociete; }
            set { multiSociete = value; }
        }

        public Int64 Societe
        {
            get { return societe; }
            set { societe = value; }
        }

        public Int64 Agence
        {
            get { return agence; }
            set { agence = value; }
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
            get { return i_machine > 0 ? i_machine : 1; }
            set { i_machine = value; }
        }

        public bool Connecter
        {
            get { return zkemkeeper != null; }
            set { connecter = value; }
        }

        public bool Actif
        {
            get { return actif; }
            set { actif = value; }
        }

        public bool Tampon
        {
            get { return tampon; }
            set { tampon = value; }
        }

        public System.Drawing.Bitmap Icon()
        {
            if (logs != null ? logs.Count > 0 : false)
            {
                if (fileLoad)
                    return new System.Drawing.Bitmap(global::ZK_Lymytz.Properties.Resources._in, 16, 16);
                else
                    return new System.Drawing.Bitmap(global::ZK_Lymytz.Properties.Resources.in_out, 16, 16);

            }
            return new System.Drawing.Bitmap(global::ZK_Lymytz.Properties.Resources._out, 16, 16);
        }

        public static Pointeuse Defaut()
        {
            Pointeuse p = new Pointeuse();
            p.id = 3;
            p.Ip = "192.168.30.236";
            p.Port = 4370;
            return p;
        }
    }
}
