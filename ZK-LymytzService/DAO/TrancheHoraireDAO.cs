using System;
using System.Collections.Generic;
using System.Text;

using ZK_LymytzService.TOOLS;
using ZK_LymytzService.ENTITE;

using NpgsqlTypes;
using Npgsql;

namespace ZK_LymytzService.DAO
{
    public class TrancheHoraireDAO
    {
        public static TrancheHoraire _getOneById(int id)
        {
            TrancheHoraire bean = new TrancheHoraire();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_grh_tranche_horaire where id =" + id + ";";
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        bean.Id = Convert.ToInt32(lect["id"].ToString());
                        bean.HeureDebut = (DateTime)((lect["heure_debut"] != null) ? (!lect["heure_debut"].ToString().Trim().Equals("") ? lect["heure_debut"] : DateTime.Now) : DateTime.Now);
                        bean.HeureFin = (DateTime)((lect["heure_fin"] != null) ? (!lect["heure_fin"].ToString().Trim().Equals("") ? lect["heure_fin"] : DateTime.Now) : DateTime.Now);
                        bean.DureePause = (DateTime)((lect["duree_pause"] != null) ? (!lect["duree_pause"].ToString().Trim().Equals("") ? lect["duree_pause"] : DateTime.Now) : DateTime.Now);
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

        public static List<TrancheHoraire> _getList(string query)
        {
            List<TrancheHoraire> list = new List<TrancheHoraire>();
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
