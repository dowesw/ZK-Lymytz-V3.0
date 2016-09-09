using System;
using System.Collections.Generic;
using System.Text;

using ZK_LymytzService.TOOLS;
using ZK_LymytzService.ENTITE;

using NpgsqlTypes;
using Npgsql;

namespace ZK_LymytzService.DAO
{
    public class PlanningDAO
    {
        public static Planning _getOneById(int id)
        {
            Planning bean = new Planning();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_grh_planning_employe where id =" + id + ";";
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        bean.Id = Convert.ToInt32(lect["id"].ToString());
                        if ((lect["tranche"] != null) ? lect["tranche"].ToString() != "" : false)
                        {
                            bean.Tranche = TrancheHoraireDAO._getOneById(Convert.ToInt32(lect["tranche"].ToString()));
                        }
                        bean.DateDebut = (DateTime)((lect["date_debut"] != null) ? (!lect["date_debut"].ToString().Trim().Equals("") ? lect["date_debut"] : DateTime.Now) : DateTime.Now);
                        bean.DateFin = (DateTime)((lect["date_fin"] != null) ? (!lect["date_fin"].ToString().Trim().Equals("") ? lect["date_fin"] : DateTime.Now) : DateTime.Now);
                        bean.Valide = Convert.ToBoolean((lect["actif"].ToString() != "") ? lect["actif"].ToString() : "false");   
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

        public static Planning _getOneByDateEmploye(long employe,DateTime date)
        {
            Planning bean = new Planning();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_grh_planning_employe where employe =" + employe + " and '" + date + "' between date_debut and date_fin";
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        bean.Id = Convert.ToInt32(lect["id"].ToString());
                        if ((lect["tranche"] != null) ? lect["tranche"].ToString() != "" : false)
                        {
                            bean.Tranche = TrancheHoraireDAO._getOneById(Convert.ToInt32(lect["tranche"].ToString()));
                        }
                        bean.DateDebut = (DateTime)((lect["date_debut"] != null) ? (!lect["date_debut"].ToString().Trim().Equals("") ? lect["date_debut"] : DateTime.Now) : DateTime.Now);
                        bean.DateFin = (DateTime)((lect["date_fin"] != null) ? (!lect["date_fin"].ToString().Trim().Equals("") ? lect["date_fin"] : DateTime.Now) : DateTime.Now);
                        bean.Valide = Convert.ToBoolean((lect["actif"].ToString() != "") ? lect["actif"].ToString() : "false");                   
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

        public static List<Planning> _getList(string query)
        {
            List<Planning> list = new List<Planning>();
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

        public static Planning _setPlanning(JoursOuvres jour, DateTime date)
        {
            Planning bean = new Planning();
            if (jour != null)
            {
                bean.Id = jour.Id;
                bean.DateDebut = date;
                bean.DateFin = date;
                bean.DureePause = jour.DureePause;
                bean.HeureDebut = jour.HeureDebutTravail;
                bean.HeureFin = jour.HeureFinTravail;
            }
            return bean;
        }
    }
}
