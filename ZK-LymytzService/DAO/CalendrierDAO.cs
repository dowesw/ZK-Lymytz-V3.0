using System;
using System.Collections.Generic;
using System.Text;

using ZK_LymytzService.TOOLS;
using ZK_LymytzService.ENTITE;

using NpgsqlTypes;
using Npgsql;

namespace ZK_LymytzService.DAO
{
    public class CalendrierDAO
    {
        public static Calendrier _getOneById(int id)
        {
            Calendrier bean = new Calendrier();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_calendrier where id =" + id + ";";
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        bean.Id = Convert.ToInt32(lect["id"].ToString());
                        bean.Reference = lect["reference"].ToString();
                        string list = "select * from yvs_jours_ouvres where calendrier = " + bean.Id + " and actif = true";
                        bean.JoursOuvres = JoursOuvresDAO._getList(list);
                    }
                }
                return bean;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }

        public static List<Calendrier> _getList(string query)
        {
            List<Calendrier> list = new List<Calendrier>();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        int id = Convert.ToInt32(lect["id"].ToString());
                        list.Add(_getOneById(id));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }
    }
}
