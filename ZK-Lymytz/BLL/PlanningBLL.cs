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

        public static Planning OneByDateEmploye(long employe, long tranche, DateTime dateDebut, DateTime dateFin)
        {
            try
            {
                return PlanningDAO.getOneByDateEmploye(employe, tranche, dateDebut, dateFin);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Planning OneByDateEmploye(long employe, long tranche, DateTime date)
        {
            try
            {
                return PlanningDAO.getOneByDateEmploye(employe, tranche, date);
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
        public static bool Insert(Planning planning)
        {
            try
            {
                return PlanningDAO.getInsert(planning);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Planning> List(string query)
        {
            return List(query, null);
        }

        public static List<Planning> List(string query, string adresse)
        {
            try
            {
                return PlanningDAO.getList(query, adresse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
