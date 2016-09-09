using System;
using System.Collections.Generic;
using System.Text;
using ZK_Lymytz.DAO;
using ZK_Lymytz.ENTITE;

namespace ZK_Lymytz.BLL
{
    class SocieteBLL
    {

        public static bool CreateSociete(Societe uneConfig)
        {
            try
            {
                return SocieteDAO.getCreateSociete(uneConfig);
            }
            catch (Exception ex)
            {
                throw new Exception("Echec de Création de fichier", ex);
            }
        }

        public static Societe ReturnSociete()
        {
            try
            {
                return SocieteDAO.getReturnSociete();
            }
            catch (Exception ex)
            {
                throw new Exception("Echec de Création de fichier", ex);
            }

        }

        public static Societe OneByName(string name)
        {
            try
            {
                return SocieteDAO.getOneByName(name);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Societe OneById(int id)
        {
            try
            {
                return SocieteDAO.getOneById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Societe> List(string query)
        {
            try
            {
                return SocieteDAO.getList(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
