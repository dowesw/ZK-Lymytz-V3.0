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
        private static PosteTravail Return(NpgsqlDataReader lect, bool full)
        {
            PosteTravail bean = new PosteTravail();
            bean.Id = Convert.ToInt32(lect["id"].ToString());
            bean.Poste = PosteDAO.getOneById(Convert.ToInt32(lect["poste"].ToString()));
            bean.Poste = new Poste(Convert.ToInt32(lect["poste"].ToString()));
            if (full)
            {
                bean.Poste = PosteDAO.getOneById(Convert.ToInt32(lect["poste"].ToString()));
            }
            return bean;
        }

        public static PosteTravail getOneById(int id, bool full)
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
                        bean = Return(lect, full);
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
                Connexion.Close(connect);
            }
        }

        public static List<PosteTravail> getList(string query, bool full)
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
                        list.Add(Return(lect, full));
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
                Connexion.Close(connect);
            }
        }
    }
}
