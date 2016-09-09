using System;
using System.Collections.Generic;
using System.Text;

namespace ZK_LymytzService.ENTITE
{
    public class Users
    {
        public string Username
        {
            get { return TOOLS.Chemins.domainName + TOOLS.Constantes.FILE_SEPARATOR + name; }
            set { }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public bool Control()
        {
            return Control(this);
        }

        public static bool Control(Users bean)
        {
            if (bean == null)
            {
                return false;
            }
            if (bean.name == null || bean.name.Trim().Equals(""))
            {
                return false;
            }
            return true;
        }
    }
}
