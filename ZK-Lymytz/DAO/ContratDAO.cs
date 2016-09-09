using System;
using System.Collections.Generic;
using System.Text;

using ZK_Lymytz.TOOLS;
using ZK_Lymytz.ENTITE;

using NpgsqlTypes;
using Npgsql;

namespace ZK_Lymytz.DAO
{
    class ContratDAO
    {
        public static Contrat getOneById(int id)
        {
            Contrat bean = new Contrat();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_grh_contrat_emps where id =" + id + ";";
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        bean.Id = Convert.ToInt32(lect["id"].ToString());
                        bean.Reference = lect["reference_contrat"].ToString();
                        bean.TypeTranche = lect["type_tranche"].ToString();
                        Int32 cal = (Int32)((lect["calendrier"] != null) ? (!lect["calendrier"].ToString().Trim().Equals("") ? lect["calendrier"] : 0) : 0);
                        bean.Calendrier = CalendrierDAO.getOneById(cal);
                    }
                }
                return bean;
            }
            catch (Exception ex)
            {
                Messages.Exception("ContratDao (getOneById) ", ex);
                return bean;
            }
            finally
            {
                connect.Close();
            }
        }

        public static List<Contrat> getList(string query)
        {
            List<Contrat> list = new List<Contrat>();
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
                Messages.Exception("ContratDao (getList) ", ex);
                return list;
            }
            finally
            {
                connect.Close();
            }
        }
    }
}
