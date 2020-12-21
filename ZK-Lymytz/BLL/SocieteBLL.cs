using System;
using System.Collections.Generic;
using System.Text;
using ZK_Lymytz.DAO;
using ZK_Lymytz.ENTITE;

namespace ZK_Lymytz.BLL
{
    public class SocieteBLL
    {

        public static bool CreateSociete(Societe uneConfig)
        {
            try
            {
                return SocieteDAO.CreateSociete(uneConfig);
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
                return SocieteDAO.ReturnSociete();
            }
            catch (Exception ex)
            {
                throw new Exception("Echec de Création de fichier", ex);
            }

        }

        public static bool Update(Societe bean)
        {
            try
            {
                return SocieteDAO.getUpdate(bean);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
