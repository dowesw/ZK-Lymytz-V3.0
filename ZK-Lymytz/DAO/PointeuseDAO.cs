using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using Npgsql;
using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;

namespace ZK_Lymytz.DAO
{
    class PointeuseDAO
    {
        private static Pointeuse Return(NpgsqlDataReader lect)
        {
            Pointeuse bean = new Pointeuse();
            bean.Id = Convert.ToInt32(lect["id"].ToString());
            bean.Ip = lect["adresse_ip"].ToString();
            bean.Port = (Int32)((lect["port"] != null) ? (!lect["port"].ToString().Trim().Equals("") ? lect["port"] : 0) : 0);
            bean.IMachine = (Int32)((lect["i_machine"] != null) ? (!lect["i_machine"].ToString().Trim().Equals("") ? lect["i_machine"] : 0) : 0);
            bean.Societe = (Int64)((lect["societe"] != null) ? (!lect["societe"].ToString().Trim().Equals("") ? lect["societe"] : 0) : 0);
            bean.Description = lect["description"].ToString();
            bean.Emplacement = lect["emplacement"].ToString();
            bean.Societe = Convert.ToInt32(lect["societe"].ToString());
            bean.Connecter = (Boolean)((lect["connecter"] != null) ? (!lect["connecter"].ToString().Trim().Equals("") ? lect["connecter"] : false) : false);
            bean.Actif = (Boolean)((lect["actif"] != null) ? (!lect["actif"].ToString().Trim().Equals("") ? lect["actif"] : false) : false);
            bean.MultiSociete = (Boolean)((lect["multi_societe"] != null) ? (!lect["multi_societe"].ToString().Trim().Equals("") ? lect["multi_societe"] : false) : false);
            return bean;
        }

        public static Pointeuse getOneById(int id)
        {
            Pointeuse bean = new Pointeuse();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_pointeuse where id =" + id + ";";
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
                Messages.Exception("PointeuseDao (getOneById) ", ex);
                return bean;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static Pointeuse getOneByIp(string ip)
        {
            Pointeuse bean = new Pointeuse();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_pointeuse where adresse_ip ='" + ip + "'";
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
                Messages.Exception("PointeuseDao (getOneById) ", ex);
                return bean;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static Pointeuse getOneByIp(string ip, int societe)
        {
            Pointeuse bean = new Pointeuse();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_pointeuse where adresse_ip ='" + ip + "' and societe = " + societe;
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
                Messages.Exception("PointeuseDao (getOneById) ", ex);
                return bean;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static List<Pointeuse> getList(string query)
        {
            List<Pointeuse> list = new List<Pointeuse>();
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
                Messages.Exception("PointeuseDao (getList) ", ex);
                return list;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static bool getInsert(Pointeuse bean)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                Pointeuse p = getOneByIp(bean.Ip);
                if (p != null ? p.Id < 1 : true)
                {
                    string query = "insert into yvs_pointeuse(adresse_ip, port, description, emplacement, connecter, actif, i_machine, multi_societe, societe) values " +
                        "('" + bean.Ip + "'," + bean.Port + ",'" + bean.Description + "','" + bean.Emplacement + "','" + bean.Connecter + "','" + bean.Actif + "'," + bean.IMachine + ",'" + bean.MultiSociete + "'," + Constantes.SOCIETE.Id + ")";
                    NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    Utils.WriteLog("Impossible d'ajouter car l'appareil " + bean.Ip + " existe déja");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("PointeuseDao (getInsert) ", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static bool getUpdate(Pointeuse bean, int id)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "update yvs_pointeuse set adresse_ip = '" + bean.Ip + "', port = " + bean.Port + ", description = '" + bean.Description + "', emplacement = '" + bean.Emplacement + "', connecter = " + bean.Connecter + ", i_machine =" + bean.IMachine + ", multi_societe ='" + bean.MultiSociete + "' where id = " + id + "";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("PointeuseDao (getUpdate) ", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static bool getDelete(int id)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "delete from yvs_pointeuse where id = " + id + "";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("PointeuseDao (getDelete) ", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static bool setDeconnect(int id)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "update yvs_pointeuse set  connecter = false where id = " + id + "";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                if (DialogResult.OK == Messages.Exception("PointeuseDao (setDeconnect) ", ex))
                {
                    new IHM.Form_Serveur().Show();
                }
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static bool setConnect(int id, int iMachine)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "update yvs_pointeuse set  connecter = true, i_machine = " + iMachine + " where id = " + id + "";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("PointeuseDao (setConnect) ", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static bool setActifById(int id, bool actif)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "update yvs_pointeuse set actif = '" + actif + "' where id = " + id + "";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("PointeuseDao (setActifById) ", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static bool setActifByIp(string ip, bool actif)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "update yvs_pointeuse set actif = '" + actif + "' where adresse_ip = '" + ip + "'";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("PointeuseDao (setActifById) ", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }
    }
}
