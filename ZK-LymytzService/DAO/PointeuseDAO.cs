using System;
using System.Collections.Generic;
using System.Text;

using Npgsql;
using ZK_LymytzService.ENTITE;
using ZK_LymytzService.TOOLS;

namespace ZK_LymytzService.DAO
{
    public class PointeuseDAO
    {
        public static Pointeuse _getOneById(int id)
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
                        bean.Id = Convert.ToInt32(lect["id"].ToString());
                        bean.Ip = lect["adresse_ip"].ToString();
                        bean.Port = (Int32)((lect["port"] != null) ? (!lect["port"].ToString().Trim().Equals("") ? lect["port"] : 0) : 0);
                        bean.IMachine = (Int32)((lect["i_machine"] != null) ? (!lect["i_machine"].ToString().Trim().Equals("") ? lect["i_machine"] : 0) : 0);
                        bean.Description = lect["description"].ToString();
                        bean.Emplacement = lect["emplacement"].ToString();
                        bean.Societe = Convert.ToInt32(lect["societe"].ToString());
                        bean.Connecter = (Boolean)((lect["connecter"] != null) ? (!lect["connecter"].ToString().Trim().Equals("") ? lect["connecter"] : false) : false);
                        bean.Actif = (Boolean)((lect["actif"] != null) ? (!lect["actif"].ToString().Trim().Equals("") ? lect["actif"] : false) : false);
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

        public static Pointeuse _getOneByIp(string ip, int societe)
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
                        bean.Id = Convert.ToInt32(lect["id"].ToString());
                        bean.Ip = lect["adresse_ip"].ToString();
                        bean.Port = (Int32)((lect["port"] != null) ? (!lect["port"].ToString().Trim().Equals("") ? lect["port"] : 0) : 0);
                        bean.IMachine = (Int32)((lect["i_machine"] != null) ? (!lect["i_machine"].ToString().Trim().Equals("") ? lect["i_machine"] : 0) : 0);
                        bean.Description = lect["description"].ToString();
                        bean.Emplacement = lect["emplacement"].ToString();
                        bean.Societe = Convert.ToInt32(lect["societe"].ToString());
                        bean.Connecter = (Boolean)((lect["connecter"] != null) ? (!lect["connecter"].ToString().Trim().Equals("") ? lect["connecter"] : false) : false);
                        bean.Actif = (Boolean)((lect["actif"] != null) ? (!lect["actif"].ToString().Trim().Equals("") ? lect["actif"] : false) : false);
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

        public static List<Pointeuse> _getList(string query)
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

        public static bool _getInsert(Pointeuse bean)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                Societe s = SocieteDAO._getReturnSociete();
                Pointeuse p = _getOneByIp(bean.Ip, s.Id);
                if (p != null ? p.Id < 1 : true)
                {
                    string query = "insert into yvs_pointeuse(adresse_ip, port, description, emplacement, connecter, actif, i_machine, societe) values " +
                        "('" + bean.Ip + "'," + bean.Port + ",'" + bean.Description + "','" + bean.Emplacement + "','" + bean.Connecter + "','" + bean.Actif + "'," + bean.IMachine + "," + s.Id + ")";
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
                throw new Exception(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }

        public static bool _getUpdate(Pointeuse bean, int id)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "update yvs_pointeuse set adresse_ip = '" + bean.Ip + "', port = " + bean.Port + ", description = '" + bean.Description + "', emplacement = '" + bean.Emplacement + "', connecter = " + bean.Connecter + ", i_machine =" + bean.IMachine + " where id = " + id + "";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                cmd.ExecuteNonQuery();
                return true;
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

        public static bool _getDelete(int id)
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
                throw new Exception(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }

        public static bool _setDeconnect(int id)
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
                throw new Exception(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }

        public static bool _setConnect(int id, int iMachine)
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
                throw new Exception(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }

        public static bool _setActifById(int id, bool actif)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "update yvs_pointeuse set  actif = '" + actif + "' where id = " + id + "";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                cmd.ExecuteNonQuery();
                return true;
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

        public static bool _setActifByIp(string ip, bool actif)
        {
            Societe s = SocieteDAO._getReturnSociete();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "update yvs_pointeuse set  actif = '" + actif + "' where adresse_ip = '" + ip + "' and societe = " + s.Id;
                NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                cmd.ExecuteNonQuery();
                return true;
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
