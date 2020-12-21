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
                return PointageDAO.getOneById(id, false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool Insert(Pointage bean, string adresse)
        {
            try
            {
                return PointageDAO.getInsert(bean, adresse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool InsertU(Pointage bean, string adresse)
        {
            try
            {
                return PointageDAO.getInsert_U(bean, adresse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool Update(Pointage bean, long id, string adresse)
        {
            try
            {
                return PointageDAO.getUpdate(bean, id, adresse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Pointage> List(string query, bool full, string adresse)
        {
            try
            {
                return PointageDAO.List(query, full, adresse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Pointage> List(string query, bool full, string queryCount, string adresse)
        {
            try
            {
                return PointageDAO.List(query, full, queryCount, adresse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
