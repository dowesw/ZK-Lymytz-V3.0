using System;
using System.Collections.Generic;
using System.Text;
using ZK_Lymytz.DAO;
using ZK_Lymytz.ENTITE;

namespace ZK_Lymytz.BLL
{
    class LiaisonBLL
    {
        public static bool CreateServeur(Serveur config)
        {
            try
            {
                return LiaisonDAO.CreateServeur(config);
            }
            catch (Exception ex)
            {
                throw new Exception("Echec de Création de fichier", ex);
            }
        }

        public static List<Serveur> ReturnServeur()
        {
            try
            {
                return LiaisonDAO.ReturnServeur();
            }
            catch (Exception ex)
            {
                throw new Exception("Echec de Création de fichier", ex);
            }

        }
        public static bool DeleteServeur(Serveur config)
        {
            try
            {
                return LiaisonDAO.DeleteServeur(config);
            }
            catch (Exception ex)
            {
                throw new Exception("Echec de suppression de fichier", ex);
            }
        }
    }
}
