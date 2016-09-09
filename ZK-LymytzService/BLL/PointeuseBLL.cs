using System;
using System.Collections.Generic;
using System.Text;

using ZK_LymytzService.ENTITE;
using ZK_LymytzService.TOOLS;
using ZK_LymytzService.DAO;

namespace ZK_LymytzService.BLL
{
    public class PointeuseBLL
    {
        public static bool Insert(Pointeuse bean)
        {
            try
            {
                return PointeuseDAO._getInsert(bean);
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
                return PointeuseDAO._getUpdate(bean, id);
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
                return PointeuseDAO._getDelete(id);
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
                return PointeuseDAO._getOneById(id);
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
                Societe s = SocieteDAO._getReturnSociete();
                return PointeuseDAO._getOneByIp(ip, s.Id);
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
                return PointeuseDAO._getList(query);
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
                return PointeuseDAO._setConnect(id, iMachine);
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
                return PointeuseDAO._setDeconnect(id);
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
                return PointeuseDAO._setActifById(id, actif);
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
                return PointeuseDAO._setActifByIp(ip, actif);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
