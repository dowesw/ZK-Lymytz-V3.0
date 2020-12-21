using System;
using System.Collections.Generic;
using System.Text;

namespace ZK_Lymytz.ENTITE
{
    public class Empreinte
    {
        private long id;
        private Employe employe = new Employe();
        private int digital;
        private int facial;
        private int numerique;
        private int flag;
        private byte[] template;
        private string sTemplate;
        private int longueur;
        private int privilege;

        public Empreinte()
        {

        }

        public Empreinte(long id, Employe employe)
        {
            this.id = id;
            this.employe = employe;
        }

        public Empreinte(Employe employe, int digital, int flag, string sTemplate, int longueur)
        {
            this.employe = employe;
            this.digital = digital;
            this.flag = flag;
            this.sTemplate = sTemplate;
            this.longueur = longueur;
        }

        public Empreinte(long id, Employe employe, int digital, int facial, int numerique, int flag, byte[] template, string sTemplate, int longueur, int privilege)
        {
            this.id = id;
            this.employe = employe;
            this.digital = digital;
            this.facial = facial;
            this.numerique = numerique;
            this.flag = flag;
            this.template = template;
            this.sTemplate = sTemplate;
            this.longueur = longueur;
            this.privilege = privilege;
        }

        public int Privilege
        {
            get { return privilege; }
            set { privilege = value; }
        }

        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        public string STemplate
        {
            get { return sTemplate; }
            set { sTemplate = value; }
        }

        public Employe Employe
        {
            get { return employe; }
            set { employe = value; }
        }

        public int Digital
        {
            get { return digital; }
            set { digital = value; }
        }

        public int Facial
        {
            get { return facial; }
            set { facial = value; }
        }

        public int Numerique
        {
            get { return numerique; }
            set { numerique = value; }
        }

        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }

        public byte[] Template
        {
            get { return template; }
            set { template = value; }
        }

        public int Longueur
        {
            get { return longueur; }
            set { longueur = value; }
        }
    }
}
