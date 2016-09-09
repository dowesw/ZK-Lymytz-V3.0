using System;
using System.Collections.Generic;
using System.Text;

using ZK_LymytzService.ENTITE;
using ZK_LymytzService.TOOLS;
using ZK_LymytzService.DAO;

namespace ZK_LymytzService.BLL
{
    public class PlanningBLL
    {
        public static Planning OneById(int id)
        {
            try
            {
                return PlanningDAO._getOneById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Planning OneByDateEmploye(long employe, DateTime date)
        {
            try
            {
                return PlanningDAO._getOneByDateEmploye(employe, date);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Planning getPlanningForJoursOuvres(JoursOuvres jour, DateTime date)
        {
            try
            {
                return PlanningDAO._setPlanning(jour, date);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Planning> List(string query)
        {
            try
            {
                return PlanningDAO._getList(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
