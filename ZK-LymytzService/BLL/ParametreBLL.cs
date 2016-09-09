using System;
using System.Collections.Generic;
using System.Text;

using ZK_LymytzService.ENTITE;
using ZK_LymytzService.TOOLS;
using ZK_LymytzService.DAO;

namespace ZK_LymytzService.BLL
{
    public class ParametreBLL
    {

        public static Parametre OneById(int id)
        {
            try
            {
                return ParametreDAO._getOneById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Parametre OneBySociete(int id)
        {
            try
            {
                return ParametreDAO._getOneBySociete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Parametre> List(string query)
        {
            try
            {
                return ParametreDAO._getList(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
