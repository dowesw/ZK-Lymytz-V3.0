using System;
using System.Collections.Generic;
using System.Text;

using ZK_Lymytz.TOOLS;
using ZK_Lymytz.ENTITE;

using NpgsqlTypes;
using Npgsql;

namespace ZK_Lymytz.DAO
{
    class CalendrierDAO
    {
        private static Calendrier Return(NpgsqlDataReader lect)
        {
            Calendrier bean = new Calendrier();
            bean.Id = Convert.ToInt32(lect["id"].ToString());
            bean.Reference = lect["reference"].ToString();
            bean.JoursOuvres = JoursOuvresDAO.getByCalendier(bean);
            return bean;
        }

        public static Calendrier getDefault()
        {
            return getDefault(Constantes.SOCIETE);
        }

        public static Calendrier getDefault(Societe societe)
        {
            if (societe != null ? societe.Id < 1 : true)
                societe = Constantes.SOCIETE;

            Calendrier bean = new Calendrier();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_calendrier where defaut = true and societe = " + societe.Id + ";";
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
                Messages.Exception("Calendrier (getDefault) ", ex);
                return bean;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static Calendrier getOneById(int id)
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
                        bean = Return(lect);
                    }
                }
                return bean;
            }
            catch (Exception ex)
            {
                Messages.Exception("Calendrier (getOneById) ", ex);
                return bean;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static List<Calendrier> getList(string query)
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
                        list.Add(Return(lect));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                Messages.Exception("Calendrier (getList) ", ex);
                return list;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }
    }
}
