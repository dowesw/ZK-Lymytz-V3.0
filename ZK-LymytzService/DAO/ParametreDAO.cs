using System;
using System.Collections.Generic;
using System.Text;

using ZK_LymytzService.TOOLS;
using ZK_LymytzService.ENTITE;

using NpgsqlTypes;
using Npgsql;

namespace ZK_LymytzService.DAO
{
    public class ParametreDAO
    {
        public static Parametre _getOneById(int id)
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
                        bean.Id = Convert.ToInt32(lect["id"].ToString());
                        bean.PlanningDynamique = Convert.ToBoolean((lect["calcul_planning_dynamique"].ToString() != "") ? lect["calcul_planning_dynamique"].ToString() : "false");
                        bean.TimeMargeAvance = (DateTime)((lect["time_marge_avance"] != null) ? (!lect["time_marge_avance"].ToString().Trim().Equals("") ? lect["time_marge_avance"] : new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 1, 0, 0)) : new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 1, 0, 0));
                        bean.TimeMargeAutorise = (DateTime)((lect["duree_retard_autorise"] != null) ? (!lect["duree_retard_autorise"].ToString().Trim().Equals("") ? lect["duree_retard_autorise"] : new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 15, 0)) : new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 15, 0));
                        bean.TimeMargeRetard = (DateTime)((lect["time_marge_retard"] != null) ? (!lect["time_marge_retard"].ToString().Trim().Equals("") ? lect["time_marge_retard"] : new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 2, 0, 0)) : new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 2, 0, 0));
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

        public static Parametre _getOneBySociete(int id)
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
                        bean.Id = Convert.ToInt32(lect["id"].ToString());
                        bean.PlanningDynamique = Convert.ToBoolean((lect["calcul_planning_dynamique"].ToString() != "") ? lect["calcul_planning_dynamique"].ToString() : "false");
                        bean.TimeMargeAvance = (DateTime)((lect["time_marge_avance"] != null) ? (!lect["time_marge_avance"].ToString().Trim().Equals("") ? lect["time_marge_avance"] : new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 1, 0, 0)) : new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 1, 0, 0));
                        bean.TimeMargeAutorise = (DateTime)((lect["duree_retard_autorise"] != null) ? (!lect["duree_retard_autorise"].ToString().Trim().Equals("") ? lect["duree_retard_autorise"] : new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 15, 0)) : new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 15, 0));
                        bean.TimeMargeRetard = (DateTime)((lect["time_marge_retard"] != null) ? (!lect["time_marge_retard"].ToString().Trim().Equals("") ? lect["time_marge_retard"] : new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 2, 0, 0)) : new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 2, 0, 0));
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

        public static List<Parametre> _getList(string query)
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
