using System;
using System.Collections.Generic;
using System.Text;
using ZK_Lymytz.DAO;
using ZK_Lymytz.ENTITE;

namespace ZK_Lymytz.BLL
{
    class PosteTravailBLL
    {

        public static PosteTravail OneById(int id)
        {
            return OneById(id, true);
        }

        public static PosteTravail OneById(int id, bool full)
        {
            try
            {
                return PosteTravailDAO.getOneById(id, full);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<PosteTravail> List(string query)
        {
            return List(query, true);
        }

        public static List<PosteTravail> List(string query, bool full)
        {
            try
            {
                return PosteTravailDAO.getList(query, full);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
