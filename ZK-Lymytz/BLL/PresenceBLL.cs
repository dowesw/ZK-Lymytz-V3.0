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
                return PresenceDAO.getOneById(id, false);
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
                return PresenceDAO.getOneByDate(empl, date, false);
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
                return PresenceDAO.getOneByDates(empl, dateDebut, dateFin, false);
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
                return PresenceDAO.getOneByDates(empl, dateDebut, dateFin, dateFinPrevu, false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool Insert(Presence bean, string adresse)
        {
            try
            {
                return PresenceDAO.getInsert(bean, adresse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Presence Insert_U(Presence bean, string adresse)
        {
            try
            {
                return PresenceDAO.setInsert(bean, adresse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool Update(Presence bean)
        {
            return Update(bean, null);
        }

        public static bool Update(Presence bean, string adresse)
        {
            try
            {
                return PresenceDAO.getUpdate(bean, adresse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Presence> List(string query, bool full, string adresse)
        {
            try
            {
                return PresenceDAO.List(query, full, adresse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Presence> List(string query, bool full, string queryCount, string adresse)
        {
            try
            {
                return PresenceDAO.List(query, full, queryCount, adresse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
