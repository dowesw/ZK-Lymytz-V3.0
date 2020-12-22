using System;
using System.Collections.Generic;
using System.Text;
using ZK_Lymytz.DAO;

namespace ZK_Lymytz.BLL
{
    class Bll
    {
        public static bool RequeteLibre(Npgsql.NpgsqlConnection connect, string query)
        {
            try
            {
                return Dao.RequeteLibre(connect, query);
            }
            catch (Exception ex)
            {
                throw new Exception("Action Impossible", ex);
            }
        }

        public static bool RequeteLibre(string query, string adresse)
        {
            try
            {
                return Dao.RequeteLibre(query, adresse);
            }
            catch (Exception ex)
            {
                throw new Exception("Action Impossible", ex);
            }
        }

        public static object LoadOneObject(string query, string adresse)
        {
            try
            {
                return Dao.LoadOneObject(query, adresse);
            }
            catch (Exception ex)
            {
                throw new Exception("Action Impossible", ex);
            }
        }

        public static List<string> LoadListObject(string query, string adresse)
        {
            try
            {
                return Dao.LoadListObject(query, adresse);
            }
            catch (Exception ex)
            {
                throw new Exception("Action Impossible", ex);
            }
        }
    }
}
