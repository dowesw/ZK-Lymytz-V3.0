using System;
using System.Collections.Generic;
using System.Text;

namespace ZK_Lymytz.ENTITE
{
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

        private string name;
        public string Name
        {
            get { return name; }
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

        private bool acces_pointeuse;
        public bool AccesPointeuse
        {
            get { return acces_pointeuse; }
            set { acces_pointeuse = value; }
        }

        private string alea_mdp;
        public string AleaMdp
        {
            get { return alea_mdp; }
            set { alea_mdp = value; }
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
            if (bean.name == null || bean.name.Trim().Equals(""))
            {
                TOOLS.Messages.ShowErreur("Le nom de l'utilisateur ne peut pas être null!");
                return false;
            }
            return true;
        }
    }
}
