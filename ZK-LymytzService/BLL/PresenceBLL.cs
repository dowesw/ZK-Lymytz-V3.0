using System;
using System.Collections.Generic;
using System.Text;

using ZK_LymytzService.ENTITE;
using ZK_LymytzService.TOOLS;
using ZK_LymytzService.DAO;

namespace ZK_LymytzService.BLL
{
    public class PresenceBLL
    {
        public static Presence OneById(int id)
        {
            try
            {
                return PresenceDAO._getOneById(id);
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
                return PresenceDAO._getOneByDate(empl, date);
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
                return PresenceDAO._getOneByDates(empl, dateDebut, dateFin);
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
                return PresenceDAO._getInsert(bean);
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
                return PresenceDAO._setInsert(bean);
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
                return PresenceDAO._getUpdate(bean);
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
                return PresenceDAO._getList(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
