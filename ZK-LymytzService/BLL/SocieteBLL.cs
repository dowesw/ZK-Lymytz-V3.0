using System;
using System.Collections.Generic;
using System.Text;
using ZK_LymytzService.DAO;
using ZK_LymytzService.ENTITE;

namespace ZK_LymytzService.BLL
{
    public class SocieteBLL
    {

        public static bool CreateSociete(Societe uneConfig)
        {
            try
            {
                return SocieteDAO._getCreateSociete(uneConfig);
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
                return SocieteDAO._getReturnSociete();
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
                return SocieteDAO._getOneByName(name);
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
                return SocieteDAO._getOneById(id);
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
                return SocieteDAO._getList(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
