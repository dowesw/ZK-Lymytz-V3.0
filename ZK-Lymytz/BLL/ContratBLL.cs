using System;
using System.Collections.Generic;
using System.Text;

using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;
using ZK_Lymytz.DAO;

namespace ZK_Lymytz.BLL
{
    class ContratBLL
    {

        public static Contrat OneById(int id)
        {
            return OneById(id, true);
        }

        public static Contrat OneById(int id, bool full)
        {
            try
            {
                return ContratDAO.getOneById(id, full);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Contrat> List(string query)
        {
            return List(query, true);
        }

        public static List<Contrat> List(string query, bool full)
        {
            try
            {
                return ContratDAO.getList(query, full);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
