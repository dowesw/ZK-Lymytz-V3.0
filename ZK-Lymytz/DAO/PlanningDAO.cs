using System;
using System.Collections.Generic;
using System.Text;

using ZK_Lymytz.TOOLS;
using ZK_Lymytz.ENTITE;

using NpgsqlTypes;
using Npgsql;

namespace ZK_Lymytz.DAO
{
    class PlanningDAO
    {
        private static Planning Return(NpgsqlDataReader lect)
        {
            Planning bean = new Planning();
            try
            {
                bean.Id = Convert.ToInt32(lect["id"].ToString());
                if ((lect["tranche"] != null) ? lect["tranche"].ToString() != "" : false)
                {
                    bean.Tranche = TrancheHoraireDAO.getOneById(Convert.ToInt32(lect["tranche"].ToString()));
                }
                bean.DateDebut = (DateTime)((lect["date_debut"] != null) ? (!lect["date_debut"].ToString().Trim().Equals("") ? lect["date_debut"] : DateTime.Now) : DateTime.Now);
                bean.DateFin = (DateTime)((lect["date_fin"] != null) ? (!lect["date_fin"].ToString().Trim().Equals("") ? lect["date_fin"] : DateTime.Now) : DateTime.Now);
                bean.Valide = Convert.ToBoolean((lect["actif"].ToString() != "") ? lect["actif"].ToString() : "false");
                bean.Repos = Convert.ToBoolean((lect["repos"].ToString() != "") ? lect["repos"].ToString() : "false");
            }
            catch (Exception ex)
            {
                var t = ex;
            }
            return bean;
        }

        public static Planning getOneById(int id)
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
                        bean = Return(lect);
                    }
                }
                return bean;
            }
            catch (Exception ex)
            {
                Messages.Exception("PlanningDao (getOneById) ", ex);
                return bean;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static Planning getOneByDateEmploye(long employe, long tranche, DateTime date)
        {
            Planning bean = new Planning();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_grh_planning_employe where employe = " + employe + " and tranche = " + tranche + " and '" + date.ToString("dd-MM-yyyy") + "' between date_debut and date_fin";
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
                Messages.Exception("PlanningDao (getOneByDateEmploye) ", ex);
                return bean;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static Planning getOneByDateEmploye(long employe, long tranche, DateTime dateDebut, DateTime dateFin)
        {
            Planning bean = new Planning();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_grh_planning_employe where employe = " + employe + " and tranche = " + tranche + " and  date_debut = '" + dateDebut.ToString("dd-MM-yyyy") + "' and date_fin = '" + dateFin.ToString("dd-MM-yyyy") + "'";
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
                Messages.Exception("PlanningDao (getOneByDateEmploye) ", ex);
                return bean;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static Planning getOneByDateEmploye(long employe, DateTime date)
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
                        bean = Return(lect);
                    }
                }
                return bean;
            }
            catch (Exception ex)
            {
                Messages.Exception("PlanningDao (getOneByDateEmploye) ", ex);
                return bean;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static bool getInsert(Planning bean)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "insert into yvs_grh_planning_employe(employe, date_debut, date_fin, tranche, actif, repos, date_save, date_update, duree_pause) values " +
                    "(" + bean.Employe + ",'" + bean.DateDebut.ToString("dd-MM-yyyy") + "','" + bean.DateFin.ToString("dd-MM-yyyy") + "'," + bean.Tranche.Id + ", true, " + bean.Repos + ", current_timestamp, current_timestamp, '00:00:00')";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("PlanningDao (getInsert) ", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static Planning getOneByDateEmploye(long employe, DateTime date, DateTime heure)
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
                        Planning p = Return(lect);

                        DateTime dateD = new DateTime(p.DateDebut.Year, p.DateDebut.Month, p.DateDebut.Day, 0, 0, 0);
                        DateTime dateF = new DateTime(p.DateFin.Year, p.DateFin.Month, p.DateFin.Day, 0, 0, 0);
                        DateTime heureD = p.HeureDebut;
                        DateTime heureF = p.HeureFin;

                        DateTime heure_debut = new DateTime(dateD.Year, dateD.Month, dateD.Day, heureD.Hour, heureD.Minute, 0);
                        DateTime heure_fin = new DateTime(dateF.Year, dateF.Month, dateF.Day, heureF.Hour, heureF.Minute, 0);

                        heure_debut = Utils.RemoveTimeInDate(heure_debut, Constantes.PARAMETRE.TimeMargeAvance);
                        heure_fin = Utils.AddTimeInDate(heure_fin, Constantes.PARAMETRE.TimeMargeAvance);
                        if (heure_debut <= heure && heure <= heure_fin)
                        {
                            bean = p;
                        }
                    }
                }
                return bean;
            }
            catch (Exception ex)
            {
                Messages.Exception("PlanningDao (getOneByDateEmploye) ", ex);
                return bean;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static List<Planning> getList(string query, string adresse)
        {
            List<Planning> list = new List<Planning>();
            NpgsqlConnection connect = new Connexion().Connection(adresse);
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
                Messages.Exception("PlanningDao (getList) ", ex);
                return list;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static Planning setPlanning(JoursOuvres jour, DateTime date)
        {
            Planning bean = new Planning();
            if (jour != null)
            {
                bean.Id = jour.Id;
                bean.DateDebut = date;
                DateTime d = new DateTime(date.Year, date.Month, date.Day, jour.HeureDebutTravail.Hour, jour.HeureDebutTravail.Minute, jour.HeureDebutTravail.Second);
                bean.DateFin = Utils.GetTimeStamp(d, jour.HeureFinTravail);
                bean.DureePause = jour.DureePause;
                bean.HeureDebut = jour.HeureDebutTravail;
                bean.HeureFin = jour.HeureFinTravail;
                bean.Supplementaire = !jour.Ouvrable;
            }
            return bean;
        }
    }
}
