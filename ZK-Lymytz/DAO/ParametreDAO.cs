using System;
using System.Collections.Generic;
using System.Text;

using ZK_Lymytz.TOOLS;
using ZK_Lymytz.ENTITE;

using NpgsqlTypes;
using Npgsql;

namespace ZK_Lymytz.DAO
{
    class ParametreDAO
    {
        private static Parametre Return(NpgsqlDataReader lect)
        {
            Parametre bean = new Parametre();
            bean.Id = Convert.ToInt32(lect["id"].ToString());
            bean.PlanningDynamique = Convert.ToBoolean((lect["calcul_planning_dynamique"].ToString() != "") ? lect["calcul_planning_dynamique"].ToString() : "true");
            bean.TimeMargeAvance = (DateTime)((lect["time_marge_avance"] != null) ? (!lect["time_marge_avance"].ToString().Trim().Equals("") ? lect["time_marge_avance"] : new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 1, 0, 0)) : new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 1, 0, 0));
            bean.TimeMargeAutorise = (DateTime)((lect["duree_retard_autorise"] != null) ? (!lect["duree_retard_autorise"].ToString().Trim().Equals("") ? lect["duree_retard_autorise"] : new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 15, 0)) : new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 15, 0));
            bean.TimeMargeRetard = (DateTime)((lect["time_marge_retard"] != null) ? (!lect["time_marge_retard"].ToString().Trim().Equals("") ? lect["time_marge_retard"] : new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 2, 0, 0)) : new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 2, 0, 0));
            bean.LimiteHeureSup = Convert.ToDouble((lect["limit_heure_sup"].ToString() != "") ? lect["limit_heure_sup"].ToString() : "2");
            return bean;
        }

        public static Parametre getOneById(int id)
        {
            Parametre bean = new Parametre();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_parametre_grh where id =" + id + ";";
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
                Messages.Exception("ParametreDao (getOneById) ", ex);
                return bean;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static Parametre getOneBySociete(int id)
        {
            Parametre bean = new Parametre();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_parametre_grh where societe = " + id + ";";
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
                Messages.Exception("ParametreDao (getOneBySociete) ", ex);
                return bean;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static List<Parametre> getList(string query)
        {
            List<Parametre> list = new List<Parametre>();
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
                Messages.Exception("ParametreDao (getList) ", ex);
                return list;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }
    }
}
