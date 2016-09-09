using System;
using System.Collections.Generic;
using System.Text;

using ZK_LymytzService.ENTITE;
using ZK_LymytzService.TOOLS;
using ZK_LymytzService.DAO;

namespace ZK_LymytzService.BLL
{
    public class EmpreinteBLL
    {
        public static Empreinte OneById(long id)
        {
            try
            {
                return EmpreinteDAO._getOneById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Empreinte OneByEmployeFinger(long employe, int finger)
        {
            try
            {
                return EmpreinteDAO._getOneByEmployeFinger(employe, finger);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Empreinte OneByEmployeFinger(long employe, int finger, Npgsql.NpgsqlConnection connect)
        {
            try
            {
                return EmpreinteDAO._getOneByEmployeFinger(employe, finger, connect);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Empreinte> List(string query)
        {
            try
            {
                return EmpreinteDAO._getList(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool Insert(Empreinte bean)
        {
            try
            {
                return EmpreinteDAO._getInsert(bean);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool Insert(Empreinte bean, Npgsql.NpgsqlConnection connect)
        {
            try
            {
                return EmpreinteDAO._getInsert(bean, connect);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool Update(Empreinte bean, long id)
        {
            try
            {
                return EmpreinteDAO._getUpdate(bean, id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool Delete(Empreinte bean, long id)
        {
            try
            {
                return EmpreinteDAO._getDelete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
