using System;
using System.Collections.Generic;
using System.Text;

using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;
using ZK_Lymytz.DAO;

namespace ZK_Lymytz.BLL
{
    class EmpreinteBLL
    {
        public static Empreinte OneById(long id)
        {
            try
            {
                return EmpreinteDAO.getOneById(id);
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
                return EmpreinteDAO.getOneByEmployeFinger(employe, finger);
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
                return EmpreinteDAO.getOneByEmployeFinger(employe, finger, connect);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Empreinte OneByEmployeFacial(long employe, int facial)
        {
            try
            {
                return EmpreinteDAO.getOneByEmployeFacial(employe, facial);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Empreinte OneByEmployeFacial(long employe, int facial, Npgsql.NpgsqlConnection connect)
        {
            try
            {
                return EmpreinteDAO.getOneByEmployeFacial(employe, facial, connect);
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
                return EmpreinteDAO.getList(query);
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
                return EmpreinteDAO.getInsert(bean);
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
                return EmpreinteDAO.getInsert(bean, connect);
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
                return EmpreinteDAO.getUpdate(bean, id);
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
                return EmpreinteDAO.getDelete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
