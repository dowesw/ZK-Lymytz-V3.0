using System;
using System.Collections.Generic;
using System.Text;
using ZK_Lymytz.DAO;
using ZK_Lymytz.ENTITE;

namespace ZK_Lymytz.BLL
{
    class ServeurBLL
    {
        public static bool CreateServeur(Serveur config)
        {
            try
            {
                return ServeurDAO.CreateServeur(config);
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
                return ServeurDAO.ReturnServeur();
            }
            catch (Exception ex)
            {
                throw new Exception("Echec de Création de fichier", ex);
            }

        }
    }
}
