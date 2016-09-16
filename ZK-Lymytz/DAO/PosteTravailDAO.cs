using System;
using System.Collections.Generic;
using System.Text;

using ZK_Lymytz.TOOLS;
using ZK_Lymytz.ENTITE;

using NpgsqlTypes;
using Npgsql;

namespace ZK_Lymytz.DAO
{
    class PosteTravailDAO
    {
        public static PosteTravail getOneById(int id)
        {
            PosteTravail bean = new PosteTravail();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_grh_poste_employes where id =" + id + ";";
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        bean.Id = Convert.ToInt32(lect["id"].ToString());
                        bean.Poste = PosteDAO.getOneById(Convert.ToInt32(lect["poste"].ToString()));
                    }
                }
                return bean;
            }
            catch (Exception ex)
            {
                Messages.Exception("PosteTravailDAO (getOneById) ", ex);
                return bean;
            }
            finally
            {
                connect.Close();
            }
        }

        public static List<PosteTravail> getList(string query)
        {
            List<PosteTravail> list = new List<PosteTravail>();
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
                        list.Add(getOneById(id));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                Messages.Exception("PosteTravailDAO (getList) ", ex);
                return list;
            }
            finally
            {
                connect.Close();
            }
        }
    }
}
