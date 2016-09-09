using System;
using System.Collections.Generic;
using System.Text;
using ZK_LymytzService.DAO;
using ZK_LymytzService.ENTITE;

namespace ZK_LymytzService.BLL
{
    public class ServeurBLL
    {
        public static bool CreateServeur(Serveur config)
        {
            try
            {
                return ServeurDAO._getCreateServeur(config);
            }
            catch (Exception ex)
            {
                throw new Exception("Echec de Création de fichier", ex);
            }
        }

        public static Serveur ReturnServeur()
        {
            try
            {
                return ServeurDAO._getReturnServeur();
            }
            catch (Exception ex)
            {
                throw new Exception("Echec de Création de fichier", ex);
            }

        }
    }
}
