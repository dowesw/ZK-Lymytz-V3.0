using System;
using System.Collections.Generic;
using System.Text;

using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;
using ZK_Lymytz.DAO;

namespace ZK_Lymytz.BLL
{
    class PresenceBLL
    {
        public static Presence OneById(int id)
        {
            try
            {
                return PresenceDAO.getOneById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Presence OneByDate(Employe empl, DateTime date)
        {
            try
            {
                return PresenceDAO.getOneByDate(empl, date);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Presence OneByDate(Employe empl, DateTime dateDebut, DateTime dateFin)
        {
            try
            {
                return PresenceDAO.getOneByDates(empl, dateDebut, dateFin);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Presence OneByDate(Employe empl, DateTime dateDebut, DateTime dateFin, DateTime dateFinPrevu)
        {
            try
            {
                return PresenceDAO.getOneByDates(empl, dateDebut, dateFin, dateFinPrevu);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool Insert(Presence bean)
        {
            try
            {
                return PresenceDAO.getInsert(bean);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Presence Insert_U(Presence bean)
        {
            try
            {
                return PresenceDAO.setInsert(bean);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool Update(Presence bean)
        {
            try
            {
                return PresenceDAO.getUpdate(bean);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Presence> List(string query)
        {
            try
            {
                return PresenceDAO.List(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Presence> List(string query, string queryCount)
        {
            try
            {
                return PresenceDAO.List(query, queryCount);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
