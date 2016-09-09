using System;
using System.Collections.Generic;
using System.Text;
using ZK_LymytzService.DAO;
using ZK_LymytzService.ENTITE;

namespace ZK_LymytzService.BLL
{
    public class UsersBLL
    {
        public static bool CreateUsers(Users config)
        {
            try
            {
                return UsersDAO._getCreateUsers(config);
            }
            catch (Exception ex)
            {
                throw new Exception("Echec de Création de fichier", ex);
            }
        }

        public static Users ReturnUsers()
        {
            try
            {
                return UsersDAO._getReturnUsers();
            }
            catch (Exception ex)
            {
                throw new Exception("Echec de Création de fichier", ex);
            }
        }

        public static bool DestroyUsers()
        {
            try
            {
                return UsersDAO._getDestroyUsers();
            }
            catch (Exception ex)
            {
                throw new Exception("Echec de Création de fichier", ex);
            }
        }
    }
}
