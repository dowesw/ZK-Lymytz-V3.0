using System;
using System.Collections.Generic;
using System.Text;

namespace ZK_Lymytz.ENTITE
{
    [Serializable]
    public class Users
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private string nom_users;
        public string NomUsers
        {
            get { return nom_users; }
            set { nom_users = value; }
        }

        private string password;
        public string Password
        {
            get { return password != null ? password : ""; }
            set { password = value; }
        }

        private string name;
        public string Name
        {
            get { return name != null ? name : ""; }
            set { name = value; }
        }

        private string password_pc;
        public string PasswordPC
        {
            get { return password_pc != null ? password_pc : ""; }
            set { password_pc = value; }
        }

        private string password_log;
        public string PasswordLog
        {
            get { return password_log != null ? password_log : ""; }
            set { password_log = value; }
        }

        private string _password_pc;
        public string _PasswordPC
        {
            get { return _password_pc; }
            set { _password_pc = value; }
        }

        private string _password_log;
        public string _PasswordLog
        {
            get { return _password_log; }
            set { _password_log = value; }
        }

        private string _password_pc_;
        public string _PasswordPC_
        {
            get { return _password_pc_; }
            set { _password_pc_ = value; }
        }

        private string _password_log_;
        public string _PasswordLog_
        {
            get { return _password_log_; }
            set { _password_log_ = value; }
        }

        private bool actif;
        public bool Actif
        {
            get { return actif; }
            set { actif = value; }
        }

        private bool acces_multi_agence;
        public bool AccesMultiAgence
        {
            get { return acces_multi_agence; }
            set { acces_multi_agence = value; }
        }

        private bool acces_multi_societe;
        public bool AccesMultiSociete
        {
            get { return acces_multi_societe; }
            set { acces_multi_societe = value; }
        }

        private bool super_admin;
        public bool SuperAdmin
        {
            get { return super_admin; }
            set { super_admin = value; }
        }

        private string alea_mdp;
        public string AleaMdp
        {
            get { return alea_mdp; }
            set { alea_mdp = value; }
        }

        private int author;
        public int Author
        {
            get { return author; }
            set { author = value; }
        }

        private Agence agence = new Agence();
        public Agence Agence
        {
            get { return agence; }
            set { agence = value; }
        }

        public string Username
        {
            get { return TOOLS.Chemins.domainName + TOOLS.Constantes.FILE_SEPARATOR + name; }
            set { }
        }

        public bool Control()
        {
            return Control(this);
        }

        public static bool Control(Users bean)
        {
            if (bean == null)
            {
                TOOLS.Messages.ShowErreur("Users Incorrect!");
                return false;
            }
            if (bean.code == null || bean.code.Trim().Equals(""))
            {
                TOOLS.Messages.ShowErreur("L'identifiant de l'utilisateur ne peut pas être null!");
                return false;
            }
            if (bean.password == null || bean.password.Trim().Equals(""))
            {
                TOOLS.Messages.ShowErreur("Le mot de passe de l'utilisateur ne peut pas être null!");
                return false;
            }
            return true;
        }
    }
}
