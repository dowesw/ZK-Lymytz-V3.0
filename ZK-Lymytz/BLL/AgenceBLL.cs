using System;
using System.Collections.Generic;
using System.Text;
using ZK_Lymytz.DAO;
using ZK_Lymytz.ENTITE;

namespace ZK_Lymytz.BLL
{
    class AgenceBLL
    {

        public static bool CreateAgence(Agence uneConfig)
        {
            try
            {
                return AgenceDAO.CreateAgence(uneConfig);
            }
            catch (Exception ex)
            {
                throw new Exception("Echec de Création de fichier", ex);
            }
        }

        public static bool RemoveAgence()
        {
            try
            {
                return AgenceDAO.RemoveAgence();
            }
            catch (Exception ex)
            {
                throw new Exception("Echec de Création de fichier", ex);
            }
        }

        public static Agence ReturnAgence()
        {
            try
            {
                return AgenceDAO.ReturnAgence();
            }
            catch (Exception ex)
            {
                throw new Exception("Echec de Création de fichier", ex);
            }

        }

        public static Agence OneById(int id)
        {
            try
            {
                Agence a = TOOLS.Constantes.AGENCES.Find(x => x.Id == id);
                if (a != null ? a.Id < 1 : true)
                {
                    a = AgenceDAO.getOneById(id);
                    TOOLS.Constantes.AGENCES.Add(a);
                }
                return a;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Agence> List(string query)
        {
            try
            {
                return AgenceDAO.getList(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
