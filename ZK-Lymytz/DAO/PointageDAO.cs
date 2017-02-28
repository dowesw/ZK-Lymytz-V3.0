using System;
using System.Collections.Generic;
using System.Text;

using NpgsqlTypes;
using Npgsql;

using ZK_Lymytz.TOOLS;
using ZK_Lymytz.ENTITE;

namespace ZK_Lymytz.DAO
{
    class PointageDAO
    {
        private static Pointage Return(NpgsqlDataReader lect)
        {
            Pointage bean = new Pointage();
            bean.Id = Convert.ToInt32(lect["id"].ToString());
            if ((lect["heure_entree"] != null) ? lect["heure_entree"].ToString() != "" : false)
            {
                bean.HeureEntree = Convert.ToDateTime(lect["heure_entree"].ToString());
            }
            if ((lect["heure_sortie"] != null) ? lect["heure_sortie"].ToString() != "" : false)
            {
                bean.HeureSortie = Convert.ToDateTime(lect["heure_sortie"].ToString());
            }
            bean.Valider = Convert.ToBoolean((lect["valider"].ToString() != "") ? lect["valider"].ToString() : "false");
            bean.Supplementaire = Convert.ToBoolean((lect["heure_supplementaire"].ToString() != "") ? lect["heure_supplementaire"].ToString() : "false");
            if ((lect["presence"] != null) ? lect["presence"].ToString() != "" : false)
            {
                bean.Presence = PresenceDAO.getOneById(Convert.ToInt32(lect["presence"].ToString()));
            }
            if ((lect["pointeuse_in"] != null) ? lect["pointeuse_in"].ToString() != "" : false)
            {
                bean.PointeuseIn = PointeuseDAO.getOneById(Convert.ToInt32(lect["pointeuse_in"].ToString()));
            }
            if ((lect["pointeuse_out"] != null) ? lect["pointeuse_out"].ToString() != "" : false)
            {
                bean.PointeuseOut = PointeuseDAO.getOneById(Convert.ToInt32(lect["pointeuse_out"].ToString()));
            }
            bean.SystemIn = (Boolean)((lect["system_in"] != null) ? (!lect["system_in"].ToString().Trim().Equals("") ? lect["system_in"] : false) : false);
            bean.SystemOut = (Boolean)((lect["system_out"] != null) ? (!lect["system_out"].ToString().Trim().Equals("") ? lect["system_out"] : false) : false);
            return bean;
        }

        public static Pointage getOneById(int id)
        {
            Pointage bean = new Pointage();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_grh_pointage where id =" + id + ";";
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
                Messages.Exception("PointageDao (getOneById)", ex);
                return bean;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static List<Pointage> List(string query)
        {
            return List(query, null);
        }

        public static List<Pointage> List(string query, string countQuery)
        {
            List<Pointage> list = new List<Pointage>();
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
                Messages.Exception("PointageDao (getList)", ex);
                return list;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public bool getInsert_N(Pointage bean)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                if (bean.Presence != null ? bean.Presence.Id > 0 : false)
                {
                    string query = "insert into yvs_grh_pointage(heure_entree, heure_sortie, valider, presence, date_save_entree, date_save_sortie) values (null, null, false," + bean.Presence.Id + ",null,null)";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Messages.Exception("PointageDao (getInsert)", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static bool getInsert(Pointage bean)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                if (bean.Presence != null ? bean.Presence.Id > 0 : false)
                {
                    string query = "insert into yvs_grh_pointage(heure_entree, valider, presence, date_save_entree, pointeuse_in, date_save_sortie, system_in, heure_supplementaire) values ";
                    if (bean.PointeuseIn != null ? bean.PointeuseIn.Id > 0 : false)
                    {
                        if (bean._HeureEntree() != null)
                            query += "('" + bean.HeureEntree + "',false ," + bean.Presence.Id + ",'" + bean.HeureEntree + "'," + bean.PointeuseIn.Id + ", null, '" + bean.SystemIn + "', '" + bean.Supplementaire + "')";
                        else
                            query += "(null, false ," + bean.Presence.Id + ", null," + bean.PointeuseIn.Id + ", null, '" + bean.SystemIn + "', '" + bean.Supplementaire + "')";
                    }
                    else
                    {
                        if (bean._HeureEntree() != null)
                            query += "('" + bean.HeureEntree + "', false," + bean.Presence.Id + ",'" + bean.HeureEntree + "', null, null, '" + bean.SystemIn + "', '" + bean.Supplementaire + "')";
                        else
                            query += "(null, false ," + bean.Presence.Id + ", null, null, null, '" + bean.SystemIn + "', '" + bean.Supplementaire + "')";
                    }
                    NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Messages.Exception("PointageDao (getInsert)", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static bool getInsert_U(Pointage bean)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                if (bean.Presence != null ? bean.Presence.Id > 0 : false)
                {
                    string query = "insert into yvs_grh_pointage(heure_entree, heure_sortie, valider, presence, date_save_entree, date_save_sortie, pointeuse_out, pointeuse_in, horaire_normale, system_in, system_out, heure_supplementaire) values ";
                    if ((bean.PointeuseIn != null ? bean.PointeuseIn.Id > 0 : false) && (bean.PointeuseOut != null ? bean.PointeuseOut.Id > 0 : false))
                    {
                        if (bean._HeureEntree() != null && bean._HeureSortie() != null)
                            query += "('" + bean.HeureEntree + "','" + bean.HeureSortie + "', " + bean.Valider + "," + bean.Presence.Id + ",'" + bean.HeureEntree + "','" + bean.HeureSortie + "'," + bean.PointeuseOut.Id + "," + bean.PointeuseIn.Id + ", " + bean.Valider + ", '" + bean.SystemIn + "','" + bean.SystemOut + "','" + bean.Supplementaire + "')";
                        else if (bean._HeureEntree() != null)
                            query += "('" + bean.HeureEntree + "', null, " + bean.Valider + "," + bean.Presence.Id + ",'" + bean.HeureEntree + "',null," + bean.PointeuseOut.Id + "," + bean.PointeuseIn.Id + ", " + bean.Valider + ", '" + bean.SystemIn + "','" + bean.SystemOut + "','" + bean.Supplementaire + "')";
                        else if (bean._HeureSortie() != null)
                            query += "(null,'" + bean.HeureSortie + "', " + bean.Valider + "," + bean.Presence.Id + ", null,'" + bean.HeureSortie + "'," + bean.PointeuseOut.Id + "," + bean.PointeuseIn.Id + ", " + bean.Valider + ", '" + bean.SystemIn + "','" + bean.SystemOut + "','" + bean.Supplementaire + "')";
                        else
                            query += "(null, null, " + bean.Valider + "," + bean.Presence.Id + ", null, null," + bean.PointeuseOut.Id + "," + bean.PointeuseIn.Id + ", " + bean.Valider + ", '" + bean.SystemIn + "','" + bean.SystemOut + "','" + bean.Supplementaire + "')";
                    }
                    else if (bean.PointeuseIn != null ? bean.PointeuseIn.Id > 0 : false)
                    {
                        if (bean._HeureEntree() != null && bean._HeureSortie() != null)
                            query += "('" + bean.HeureEntree + "','" + bean.HeureSortie + "', " + bean.Valider + "," + bean.Presence.Id + ",'" + bean.HeureEntree + "','" + bean.HeureSortie + "', null," + bean.PointeuseIn.Id + ", " + bean.Valider + ", '" + bean.SystemIn + "','" + bean.SystemOut + "','" + bean.Supplementaire + "')";
                        else if (bean._HeureEntree() != null)
                            query += "('" + bean.HeureEntree + "', null, " + bean.Valider + "," + bean.Presence.Id + ",'" + bean.HeureEntree + "', null, null," + bean.PointeuseIn.Id + ", " + bean.Valider + ", '" + bean.SystemIn + "','" + bean.SystemOut + "','" + bean.Supplementaire + "')";
                        else if (bean._HeureSortie() != null)
                            query += "(null,'" + bean.HeureSortie + "', " + bean.Valider + "," + bean.Presence.Id + ", null,'" + bean.HeureSortie + "', null," + bean.PointeuseIn.Id + ", " + bean.Valider + ", '" + bean.SystemIn + "','" + bean.SystemOut + "','" + bean.Supplementaire + "')";
                        else
                            query += "(null, null, " + bean.Valider + "," + bean.Presence.Id + ", null, null, null," + bean.PointeuseIn.Id + ", " + bean.Valider + ", '" + bean.SystemIn + "','" + bean.SystemOut + "','" + bean.Supplementaire + "')";
                    }
                    else if (bean.PointeuseOut != null ? bean.PointeuseOut.Id > 0 : false)
                    {
                        if (bean._HeureEntree() != null && bean._HeureSortie() != null)
                            query += "('" + bean.HeureEntree + "','" + bean.HeureSortie + "', " + bean.Valider + "," + bean.Presence.Id + ",'" + bean.HeureEntree + "','" + bean.HeureSortie + "'," + bean.PointeuseOut.Id + ", null, " + bean.Valider + ", '" + bean.SystemIn + "','" + bean.SystemOut + "','" + bean.Supplementaire + "')";
                        else if (bean._HeureEntree() != null)
                            query += "('" + bean.HeureEntree + "', null, " + bean.Valider + "," + bean.Presence.Id + ",'" + bean.HeureEntree + "', null," + bean.PointeuseOut.Id + ", null, " + bean.Valider + ", '" + bean.SystemIn + "','" + bean.SystemOut + "','" + bean.Supplementaire + "')";
                        else if (bean._HeureSortie() != null)
                            query += "(null,'" + bean.HeureSortie + "', " + bean.Valider + "," + bean.Presence.Id + ", null,'" + bean.HeureSortie + "'," + bean.PointeuseOut.Id + ", null, " + bean.Valider + ", '" + bean.SystemIn + "','" + bean.SystemOut + "','" + bean.Supplementaire + "')";
                        else
                            query += "(null, null, " + bean.Valider + "," + bean.Presence.Id + ", null, null," + bean.PointeuseOut.Id + ", null, " + bean.Valider + ", '" + bean.SystemIn + "','" + bean.SystemOut + "','" + bean.Supplementaire + "')";
                    }
                    else
                    {
                        if (bean._HeureEntree() != null && bean._HeureSortie() != null)
                            query += "('" + bean.HeureEntree + "','" + bean.HeureSortie + "', " + bean.Valider + "," + bean.Presence.Id + ",'" + bean.HeureEntree + "','" + bean.HeureSortie + "', null, null, " + bean.Valider + ", '" + bean.SystemIn + "','" + bean.SystemOut + "','" + bean.Supplementaire + "')";
                        else if (bean._HeureEntree() != null)
                            query += "('" + bean.HeureEntree + "', null, " + bean.Valider + "," + bean.Presence.Id + ",'" + bean.HeureEntree + "', null, null, null, " + bean.Valider + ", '" + bean.SystemIn + "','" + bean.SystemOut + "','" + bean.Supplementaire + "')";
                        else if (bean._HeureSortie() != null)
                            query += "(null,'" + bean.HeureSortie + "', " + bean.Valider + "," + bean.Presence.Id + ", null,'" + bean.HeureSortie + "', null, null, " + bean.Valider + ", '" + bean.SystemIn + "','" + bean.SystemOut + "','" + bean.Supplementaire + "')";
                        else
                            query += "(null, null, " + bean.Valider + "," + bean.Presence.Id + ", null, null, null, null, " + bean.Valider + ", '" + bean.SystemIn + "','" + bean.SystemOut + "','" + bean.Supplementaire + "')";
                    }
                    NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Messages.Exception("PointageDao (getInsert)", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static bool getUpdate(Pointage bean, long id)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "update yvs_grh_pointage set ";
                if (bean.PointeuseOut != null ? bean.PointeuseOut.Id > 0 : false)
                {
                    if (bean._HeureSortie() != null)
                        query += "valider = " + bean.Valider + " , horaire_normale = " + bean.Valider + " , heure_sortie ='" + bean.HeureSortie + "', date_save_sortie ='" + bean.HeureSortie + "', pointeuse_out =" + bean.PointeuseOut.Id + ", system_out = '" + bean.SystemOut + "'";
                    else
                        query += "valider = " + bean.Valider + " , horaire_normale = " + bean.Valider + " , pointeuse_out =" + bean.PointeuseOut.Id + ", system_out = '" + bean.SystemOut + "'";
                }
                else
                {
                    if (bean._HeureSortie() != null)
                        query += "valider = " + bean.Valider + " , horaire_normale = " + bean.Valider + " , heure_sortie ='" + bean.HeureSortie + "', date_save_sortie ='" + bean.HeureSortie + "', system_out = '" + bean.SystemOut + "'";
                    else
                        query += "valider = " + bean.Valider + " , horaire_normale = " + bean.Valider + " , system_out = '" + bean.SystemOut + "'";
                }
                query += " where id = " + id;
                NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("PointageDao (getUpdate)", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }
    }
}
