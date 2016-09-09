using System;
using System.Collections.Generic;
using System.Text;

using ZK_Lymytz.TOOLS;
using ZK_Lymytz.ENTITE;

using NpgsqlTypes;
using Npgsql;

namespace ZK_Lymytz.DAO
{
    class JoursOuvresDAO
    {
        public static JoursOuvres getOneById(int id)
        {
            JoursOuvres bean = new JoursOuvres();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_jours_ouvres where id =" + id + ";";
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        bean.Id = Convert.ToInt32(lect["id"].ToString());
                        bean.Jour = lect["jour"].ToString();
                        bean.HeureDebutTravail = (DateTime)((lect["heure_debut_travail"] != null) ? (!lect["heure_debut_travail"].ToString().Trim().Equals("") ? lect["heure_debut_travail"] : DateTime.Now) : DateTime.Now);
                        bean.HeureFinTravail = (DateTime)((lect["heure_fin_travail"] != null) ? (!lect["heure_fin_travail"].ToString().Trim().Equals("") ? lect["heure_fin_travail"] : DateTime.Now) : DateTime.Now);
                        bean.DureePause = (DateTime)((lect["duree_pause"] != null) ? (!lect["duree_pause"].ToString().Trim().Equals("") ? lect["duree_pause"] : DateTime.Now) : DateTime.Now);
                        bean.HeureDebutPause = (DateTime)((lect["heure_debut_pause"] != null) ? (!lect["heure_debut_pause"].ToString().Trim().Equals("") ? lect["heure_debut_pause"] : DateTime.Now) : DateTime.Now);
                        bean.HeureFinPause = (DateTime)((lect["heure_fin_pause"] != null) ? (!lect["heure_fin_pause"].ToString().Trim().Equals("") ? lect["heure_fin_pause"] : DateTime.Now) : DateTime.Now);
                        bean.Ouvrable = (Boolean)((lect["ouvrable"] != null) ? (!lect["ouvrable"].ToString().Trim().Equals("") ? lect["ouvrable"] : false) : false);
                    }
                }
                return bean;
            }
            catch (Exception ex)
            {
                Messages.Exception("JoursOuvres (getOneById) ", ex);
                return bean;
            }
            finally
            {
                connect.Close();
            }
        }

        public static JoursOuvres getOneByCalendier(Calendrier calendrier, string jour)
        {
            JoursOuvres bean = new JoursOuvres();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_jours_ouvres where calendrier =" + calendrier.Id + " and upper(jour) = upper('" + jour + "') limit 1;";
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        bean.Id = Convert.ToInt32(lect["id"].ToString());
                        bean.Jour = lect["jour"].ToString();
                        bean.HeureDebutTravail = (DateTime)((lect["heure_debut_travail"] != null) ? (!lect["heure_debut_travail"].ToString().Trim().Equals("") ? lect["heure_debut_travail"] : DateTime.Now) : DateTime.Now);
                        bean.HeureFinTravail = (DateTime)((lect["heure_fin_travail"] != null) ? (!lect["heure_fin_travail"].ToString().Trim().Equals("") ? lect["heure_fin_travail"] : DateTime.Now) : DateTime.Now);
                        bean.DureePause = (DateTime)((lect["duree_pause"] != null) ? (!lect["duree_pause"].ToString().Trim().Equals("") ? lect["duree_pause"] : DateTime.Now) : DateTime.Now);
                        bean.HeureDebutPause = (DateTime)((lect["heure_debut_pause"] != null) ? (!lect["heure_debut_pause"].ToString().Trim().Equals("") ? lect["heure_debut_pause"] : DateTime.Now) : DateTime.Now);
                        bean.HeureFinPause = (DateTime)((lect["heure_fin_pause"] != null) ? (!lect["heure_fin_pause"].ToString().Trim().Equals("") ? lect["heure_fin_pause"] : DateTime.Now) : DateTime.Now);
                        bean.Ouvrable = (Boolean)((lect["ouvrable"] != null) ? (!lect["ouvrable"].ToString().Trim().Equals("") ? lect["ouvrable"] : false) : false);
                    }
                }
                return bean;
            }
            catch (Exception ex)
            {
                Messages.Exception("JoursOuvres (getOneByCalendier) ", ex);
                return bean;
            }
            finally
            {
                connect.Close();
            }
        }

        public static List<JoursOuvres> getByCalendier(Calendrier calendrier)
        {
            List<JoursOuvres> list = new List<JoursOuvres>();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_jours_ouvres where calendrier =" + calendrier.Id + ";";
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
                Messages.Exception("JoursOuvres (getByCalendier) ", ex);
                return list;
            }
            finally
            {
                connect.Close();
            }
        }

        public static List<JoursOuvres> getList(string query)
        {
            List<JoursOuvres> list = new List<JoursOuvres>();
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
                Messages.Exception("JoursOuvres (getList) ", ex);
                return list;
            }
            finally
            {
                connect.Close();
            }
        }
    }
}
