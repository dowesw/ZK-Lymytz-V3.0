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
        private static Contrat Return(NpgsqlDataReader lect)
        {
            Contrat bean = new Contrat();
            bean.Id = Convert.ToInt32(lect["id"].ToString());
            bean.Reference = lect["reference_contrat"].ToString();
            bean.TypeTranche = lect["type_tranche"].ToString();
            //bean.HoraireDynamique = (Boolean)((lect["horaire_dynamique"] != null) ? (!lect["horaire_dynamique"].ToString().Trim().Equals("") ? lect["horaire_dynamique"] : false) : false);
            Int32 cal = (Int32)((lect["calendrier"] != null) ? (!lect["calendrier"].ToString().Trim().Equals("") ? lect["calendrier"] : 0) : 0);
            bean.Calendrier = CalendrierDAO.getOneById(cal);
            return bean;
        }

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
                        bean = Return(lect);
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
                Connexion.Close(connect);
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
                        list.Add(Return(lect));
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
                Connexion.Close(connect);
            }
        }
    }
}
