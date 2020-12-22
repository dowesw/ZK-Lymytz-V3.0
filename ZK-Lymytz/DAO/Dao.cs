using System;
using System.Collections.Generic;
using System.Text;

using Npgsql;
using ZK_Lymytz.TOOLS;
using ZK_Lymytz.BLL;

namespace ZK_Lymytz.DAO
{
    class Dao
    {

        public static bool RequeteLibre(NpgsqlConnection connect, string query)
        {
            try
            {
                if (connect == null)
                {
                    return false;
                }
                if (connect.State == System.Data.ConnectionState.Closed)
                {
                    connect.Open();
                }
                if (query != null ? query.Trim().Length > 0 : false)
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("Dao (RequeteLibre) ", ex);
            }
            return false;
        }

        public static bool RequeteLibre(string query, string adresse)
        {
            NpgsqlConnection connect = new Connexion().Connection(adresse);
            try
            {
                return RequeteLibre(connect, query);
            }
            catch (Exception ex)
            {
                Messages.Exception("Dao (RequeteLibre) ", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static object LoadOneObject(string query, string adresse)
        {
            NpgsqlConnection connect = new Connexion().Connection(adresse);
            try
            {
                if (query != null ? query.Trim().Length > 0 : false)
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                    NpgsqlDataReader lect = cmd.ExecuteReader();
                    if (lect.HasRows)
                    {
                        while (lect.Read())
                        {
                            return lect[0];
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Messages.Exception("Dao (LoadOneObject) ", ex);
                return null;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static List<string> LoadListObject(string query, string adresse)
        {
            NpgsqlConnection connect = new Connexion().Connection(adresse);
            try
            {
                List<string> list = new List<string>();
                if (query != null ? query.Trim().Length > 0 : false)
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                    NpgsqlDataReader lect = cmd.ExecuteReader();
                    if (lect.HasRows)
                    {
                        while (lect.Read())
                        {
                            list.Add(lect[0].ToString());
                        }
                    }

                }
                return list;
            }
            catch (Exception ex)
            {
                Messages.Exception("Dao (LoadListObject) ", ex);
                return null;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }
    }
}
