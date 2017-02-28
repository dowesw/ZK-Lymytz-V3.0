using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using NpgsqlTypes;
using Npgsql;

using ZK_Lymytz.TOOLS;
using ZK_Lymytz.ENTITE;

namespace ZK_Lymytz.DAO
{
    class PresenceDAO
    {
        private static Presence Return(NpgsqlDataReader lect)
        {
            Presence bean = new Presence();
            bean.Id = Convert.ToInt32(lect["id"].ToString());
            bean.DateDebut = (DateTime)((lect["date_debut"] != null) ? (!lect["date_debut"].ToString().Trim().Equals("") ? lect["date_debut"] : DateTime.Now) : DateTime.Now);
            bean.DateFin = (DateTime)((lect["date_fin"] != null) ? (!lect["date_fin"].ToString().Trim().Equals("") ? lect["date_fin"] : DateTime.Now) : DateTime.Now);
            bean.DateFinPrevu = (DateTime)((lect["date_fin_prevu"] != null) ? (!lect["date_fin_prevu"].ToString().Trim().Equals("") ? lect["date_fin_prevu"] : DateTime.Now) : DateTime.Now);
            bean.HeureDebut = (DateTime)((lect["heure_debut"] != null) ? (!lect["heure_debut"].ToString().Trim().Equals("") ? lect["heure_debut"] : DateTime.Now) : DateTime.Now);
            bean.HeureFin = (DateTime)((lect["heure_fin"] != null) ? (!lect["heure_fin"].ToString().Trim().Equals("") ? lect["heure_fin"] : DateTime.Now) : DateTime.Now);
            bean.HeureFinPrevu = (DateTime)((lect["heure_fin_prevu"] != null) ? (!lect["heure_fin_prevu"].ToString().Trim().Equals("") ? lect["heure_fin_prevu"] : DateTime.Now) : DateTime.Now);
            bean.TotalPresence = (Double)((lect["total_presence"] != null) ? (!lect["total_presence"].ToString().Trim().Equals("") ? lect["total_presence"] : 0.0) : 0.0);
            bean.TotalSupplementaire = (Double)((lect["total_heure_sup"] != null) ? (!lect["total_heure_sup"].ToString().Trim().Equals("") ? lect["total_heure_sup"] : 0.0) : 0.0);
            bean.Employe = EmployeDAO.getOneById(Convert.ToInt32(lect["employe"].ToString()));
            bean.Valider = Convert.ToBoolean((lect["valider"].ToString() != "") ? lect["valider"].ToString() : "false");
            //bean.Pointeuses = Connexion.LoadListObject("select DISTINCT(pointeuse_in) from yvs_grh_pointage where presence = " + bean.Id).ConvertAll(new Converter<object, string>(Utils.ObjectToString));
            bean.Pointeuses = Connexion.LoadListObject("select DISTINCT(pointeuse_in) from yvs_grh_pointage where pointeuse_in is not null and presence = " + bean.Id);
            return bean;
        }

        public static Presence getOneById(int id)
        {
            Presence bean = new Presence();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_grh_presence where id =" + id + ";";
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
                Messages.Exception("PrésenceDao (getOneById)", ex);
                return bean;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static Presence getOneByDate(Employe empl, DateTime date)
        {
            Presence bean = new Presence();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_grh_presence where employe =" + empl.Id + " and '" + date + "' between date_debut and date_fin order by date_debut";
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
                Messages.Exception("PresenceDao (getOneByDate)", ex);
                return bean;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static Presence getOneByDates(Employe empl, DateTime dateDebut, DateTime dateFin)
        {
            Presence bean = new Presence();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_grh_presence where employe =" + empl.Id + " and date_debut = '" + dateDebut.ToString("dd-MM-yyyy") + "' and date_fin = '" + dateFin.ToString("dd-MM-yyyy") + "' order by date_debut";
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
                Messages.Exception("PresenceDao (getOneByDates)", ex);
                return bean;
            }
            finally
            {
                Connexion.Close(connect);   
            }
        } 

        public static Presence getOneByDates(Employe empl, DateTime dateDebut, DateTime dateFin, DateTime dateFinPrevu)
        {
            Presence bean = new Presence();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_grh_presence where employe =" + empl.Id + " and date_debut = '" + dateDebut.ToString("dd-MM-yyyy") + "' and (date_fin = '" + dateFin.ToString("dd-MM-yyyy") + "' or date_fin_prevu = '" + dateFinPrevu.ToString("dd-MM-yyyy") + "') order by date_debut";
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
                Messages.Exception("PresenceDao (getOneByDates)", ex);
                return bean;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static List<Presence> List(string query)
        {
            return List(query, null);
        }

        public static List<Presence> List(string query, string countQuery)
        {
            List<Presence> list = new List<Presence>();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    if (Constantes.PBAR_WAIT != null && (countQuery != null ? countQuery.Trim().Length > 0 : false))
                    {
                        int count = Convert.ToInt32(Connexion.LoadOneObject(countQuery));
                        ObjectThread o = new ObjectThread(Constantes.PBAR_WAIT);
                        o.UpdateMaxBar(count);
                    }
                    while (lect.Read())
                    {
                        list.Add(Return(lect));
                        Constantes.LoadPatience(false);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                Messages.Exception("PresenceDao (getList)", ex);
                return list;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static bool getInsert(Presence bean)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "insert into yvs_grh_presence(date_debut, heure_debut, date_fin, heure_fin, date_fin_prevu, heure_fin_prevu, employe, valider, duree_pause, marge_approuve) values ('" + bean.DateDebut + "','" + bean.HeureDebut.ToString("HH:mm:ss") + "','" + bean.DateFin + "','" + bean.HeureFin.ToString("HH:mm:ss") + "','" + bean.DateFin + "','" + bean.HeureFin.ToString("HH:mm:ss") + "'," + bean.Employe.Id + ",false,'" + bean.DureePause.ToString("HH:mm:ss") + "', null)";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("PresenceDao (getInsert)", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static bool getUpdate(Presence bean)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "update yvs_grh_presence set marge_approuve = '" + bean.MargeApprouve.ToString("HH:mm:ss") + "', date_fin_prevu = '" + bean.DateFinPrevu + "' , heure_fin_prevu = '" + bean.HeureFinPrevu + "' where id = " + bean.Id;
                NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("PresenceDao (getUpdate)", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static Presence setInsert(Presence bean)
        {
            try
            {
                if (getInsert(bean))
                {
                    Presence new_ = List("select * from yvs_grh_presence order by id desc limit 1")[0];
                    return new_;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("PresenceDao (setInsert)", ex);
                return null;
            }
        }
    }
}
