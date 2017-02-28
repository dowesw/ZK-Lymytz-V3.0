using System;
using System.Collections.Generic;
using System.Text;

using ZK_Lymytz.TOOLS;
using ZK_Lymytz.ENTITE;

using NpgsqlTypes;
using Npgsql;

namespace ZK_Lymytz.DAO
{
    class AgenceDAO
    {
        private static Agence Return(NpgsqlDataReader lect)
        {
            Agence bean = new Agence();
            bean.Id = Convert.ToInt32(lect["id"].ToString());
            bean.Name = lect["designation"].ToString();
            return bean;
        }

        public static Agence getOneById(int id)
        {
            Agence bean = new Agence();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_agences where id =" + id + ";";
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        bean = Return(lect);
                    }
                }
                return bean;
            }
            catch (Exception ex)
            {
                Messages.Exception("AgenceDao (getOneById) ", ex);
                return bean;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static List<Agence> getList(string query)
        {
            List<Agence> list = new List<Agence>();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        list.Add(Return(lect));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                Messages.Exception("AgenceDao (getList) ", ex);
                return list;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }
    }
}
