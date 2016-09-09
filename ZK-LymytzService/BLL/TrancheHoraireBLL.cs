using System;
using System.Collections.Generic;
using System.Text;

using ZK_LymytzService.ENTITE;
using ZK_LymytzService.TOOLS;
using ZK_LymytzService.DAO;

namespace ZK_LymytzService.BLL
{
    public class TrancheHoraireBLL
    {
        public static TrancheHoraire OneById(int id)
        {
            try
            {
                return TrancheHoraireDAO._getOneById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<TrancheHoraire> List(string query)
        {
            try
            {
                return TrancheHoraireDAO._getList(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
