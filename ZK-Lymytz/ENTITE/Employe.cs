using System;
using System.Collections.Generic;
using System.Text;

namespace ZK_Lymytz.ENTITE
{
    public class Employe
    {
        public Employe() { }

        public Employe(long id)
        {
            this.id = id;
        }

        public Employe(long id, string nom)
        {
            this.id = id;
            this.nom = nom;
        }

        public Employe(long id, string nom, string prenom)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
        }

        private long id;
        private string nom;
        private string prenom;
        private string matricule;
        private bool horaire_dynamique;
        private Contrat contrat = new Contrat();
        private PosteTravail poste = new PosteTravail();
        private string password;
        private int privilege;
        private bool bEnabled;
        private string photo;
        private Agence agence = new Agence();

        public Agence Agence
        {
            get { return agence; }
            set { agence = value; }
        }

        public string Matricule
        {
            get { return matricule; }
            set { matricule = value; }
        }

        public bool BEnabled
        {
            get { return bEnabled; }
            set { bEnabled = value; }
        }

        public int Privilege
        {
            get { return privilege; }
            set { privilege = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }


        public long Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public bool HoraireDynamique
        {
            get { return horaire_dynamique; }
            set { horaire_dynamique = value; }
        }

        public string Prenom
        {
            get { return prenom; }
            set { prenom = value; }
        }

        public Contrat Contrat
        {
            get { return contrat; }
            set { contrat = value; }
        }

        public PosteTravail Poste
        {
            get { return poste; }
            set { poste = value; }
        }


        public string NomPrenom
        {
            get { return (nom != null ? nom : "") + " " + (prenom != null ? prenom : ""); }
            set {}
        }

        public string Photo
        {
            get { return photo; }
            set { photo = value; }
        }
    }
}
