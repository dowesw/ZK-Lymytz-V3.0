using System;
using System.Collections.Generic;
using System.Text;

using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;
using ZK_Lymytz.DAO;

namespace ZK_Lymytz.BLL
{
    class PointageBLL
    {
        public static Pointage OneById(int id)
        {
            try
            {
                return PointageDAO.getOneById(id);
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
                return PointageDAO.getInsert(bean);
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
                return PointageDAO.getInsert_U(bean);
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
                return PointageDAO.getUpdate(bean, id);
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
                return PointageDAO.getList(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
