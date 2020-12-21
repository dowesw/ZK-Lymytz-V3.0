using System;
using System.Collections.Generic;
using System.Text;

using ZK_Lymytz.TOOLS;
using ZK_Lymytz.ENTITE;

using NpgsqlTypes;
using Npgsql;

namespace ZK_Lymytz.DAO
{
    class TrancheHoraireDAO
    {
        private static TrancheHoraire Return(NpgsqlDataReader lect)
        {
            TrancheHoraire bean = new TrancheHoraire();
            try
            {
                bean.Id = Convert.ToInt32(lect["id"].ToString());
                bean.Societe = Convert.ToInt32(lect["societe"].ToString());
                bean.HeureDebut = (DateTime)((lect["heure_debut"] != null) ? (!lect["heure_debut"].ToString().Trim().Equals("") ? lect["heure_debut"] : DateTime.Now) : DateTime.Now);
                bean.HeureFin = (DateTime)((lect["heure_fin"] != null) ? (!lect["heure_fin"].ToString().Trim().Equals("") ? lect["heure_fin"] : DateTime.Now) : DateTime.Now);
                bean.DureePause = (DateTime)((lect["duree_pause"] != null) ? (!lect["duree_pause"].ToString().Trim().Equals("") ? lect["duree_pause"] : DateTime.Now) : DateTime.Now);
                bean.TypeJournee = lect["type_journee"].ToString();
                bean.Titre = lect["titre"].ToString();
            }
            catch (Exception ex)
            {
                Messages.Exception(ex);
            }
            return bean;
        }

        public static TrancheHoraire getOneById(int id)
        {
            TrancheHoraire bean = new TrancheHoraire();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_grh_tranche_horaire where id =" + id + ";";
                using (NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect))
                using (NpgsqlDataReader lect = Lcmd.ExecuteReader())
                {
                    if (lect.HasRows)
                    {
                        while (lect.Read())
                        {
                            bean = Return(lect);
                        }
                    }
                }
                return bean;
            }
            catch (Exception ex)
            {
                Messages.Exception("TrancheHoraire (getOneById) ", ex);
                return bean;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static List<TrancheHoraire> getList(string query, string adresse)
        {
            List<TrancheHoraire> list = new List<TrancheHoraire>();
            NpgsqlConnection connect = new Connexion().Connection(adresse);
            try
            {
                using (NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect))
                using (NpgsqlDataReader lect = Lcmd.ExecuteReader())
                {
                    if (lect.HasRows)
                    {
                        while (lect.Read())
                        {
                            list.Add(Return(lect));
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                Messages.Exception("TrancheHoraire (getList) ", ex);
                return list;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }
    }
}
