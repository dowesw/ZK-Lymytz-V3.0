using System;
using System.Collections.Generic;
using System.Text;

using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;
using ZK_Lymytz.DAO;

namespace ZK_Lymytz.BLL
{
    class PlanningBLL
    {
        public static Planning OneById(int id)
        {
            try
            {
                return PlanningDAO.getOneById(id);
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
                return PlanningDAO.getOneByDateEmploye(employe, date);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Planning OneByDateEmploye(long employe, DateTime date, DateTime heure)
        {
            try
            {
                return PlanningDAO.getOneByDateEmploye(employe, date, heure);
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
                return PlanningDAO.setPlanning(jour, date);
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
                return PlanningDAO.getList(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
