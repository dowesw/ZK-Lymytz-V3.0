using System;
using System.Collections.Generic;
using System.Text;

using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;
using ZK_Lymytz.DAO;

namespace ZK_Lymytz.BLL
{
    class PointeuseBLL
    {
        public static bool Insert(Pointeuse bean)
        {
            try
            {
                return PointeuseDAO.getInsert(bean);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool Update(Pointeuse bean, int id)
        {
            try
            {
                return PointeuseDAO.getUpdate(bean, id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool Delete(int id)
        {
            try
            {
                return PointeuseDAO.getDelete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Pointeuse OneById(int id)
        {
            try
            {
                return PointeuseDAO.getOneById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Pointeuse OneByIp(string ip)
        {
            try
            {
                return PointeuseDAO.getOneByIp(ip);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Pointeuse OneByIp(string ip, Societe s)
        {
            try
            {
                return PointeuseDAO.getOneByIp(ip, s.Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Pointeuse> List(string query)
        {
            try
            {
                return PointeuseDAO.getList(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool Connect(int id, int iMachine)
        {
            try
            {
                return PointeuseDAO.setConnect(id, iMachine);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool Deconnect(int id)
        {
            try
            {
                return PointeuseDAO.setDeconnect(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool ActifById(int id, bool actif)
        {
            try
            {
                return PointeuseDAO.setActifById(id, actif);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool ActifByIp(string ip, bool actif)
        {
            try
            {
                return PointeuseDAO.setActifByIp(ip, actif);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
