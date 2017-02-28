using System;
using System.Collections.Generic;
using System.Text;
using ZK_Lymytz.DAO;
using ZK_Lymytz.ENTITE;

namespace ZK_Lymytz.BLL
{
    class UsersBLL
    {
        public static bool CreateUsers(Users config)
        {
            try
            {
                return UsersDAO.CreateUsers(config);
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
                return UsersDAO.ReturnUsers();
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
                return UsersDAO.DestroyUsers();
            }
            catch (Exception ex)
            {
                throw new Exception("Echec de Création de fichier", ex);
            }
        }

        public static Users OneById(int id)
        {
            try
            {
                return UsersDAO.getOneById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Users OneByName(string id, string password)
        {
            try
            {
                return UsersDAO.getOneByName(id, password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
