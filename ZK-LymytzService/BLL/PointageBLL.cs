using System;
using System.Collections.Generic;
using System.Text;

using ZK_LymytzService.ENTITE;
using ZK_LymytzService.TOOLS;
using ZK_LymytzService.DAO;

namespace ZK_LymytzService.BLL
{
    public class PointageBLL
    {
        public static Pointage OneById(int id)
        {
            try
            {
                return PointageDAO._getOneById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool Insert(Pointage bean)
        {
            try
            {
                return PointageDAO._getInsert(bean);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool InsertU(Pointage bean)
        {
            try
            {
                return PointageDAO._getInsert_U(bean);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool Update(Pointage bean, long id)
        {
            try
            {
                return PointageDAO._getUpdate(bean, id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Pointage> List(string query)
        {
            try
            {
                return PointageDAO._getList(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
