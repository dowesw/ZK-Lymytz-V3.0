using System;
using System.Collections.Generic;
using System.Text;

using Npgsql;
using Microsoft.Win32;
using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;

namespace ZK_Lymytz.DAO
{
    public class SocieteDAO
    {
        static string chemin = Chemins.CheminSociete();
        static List<Societe> listeSociete = new List<Societe>();

        public static bool CreateSociete(Societe societe)
        {
            try
            {
                using (RegistryKey Nkey = Registry.LocalMachine)
                {
                    CreateSociete(societe, Nkey);
                }
                return true;
            }
            catch (Exception e)
            {
                Messages.Exception("SocieteDAO (getCreateSociete) ", e);
                return false;
            }
        }

        public static bool CreateSociete(Societe societe, RegistryKey Nkey)
        {
            try
            {
                if (societe != null)
                {
                    RegistryKey valKey = Nkey.OpenSubKey(@chemin, true);
                    if (valKey == null)
                    {
                        Nkey.CreateSubKey(@chemin);
                        valKey = Nkey.OpenSubKey(@chemin, true);
                    }
                    valKey.SetValue("id", societe.Id);
                    valKey.SetValue("name", societe.Name);
                    valKey.SetValue("adresse", societe.AdresseIp);
                    valKey.SetValue("port", societe.Port);
                    valKey.SetValue("users", societe.Users);
                    valKey.SetValue("password", societe.Password);
                    valKey.SetValue("domain", societe.Domain);
                    valKey.SetValue("type", societe.TypeConnexion);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Messages.Exception("SocieteDAO (getCreateSociete) ", e);
                return false;
            }
            finally
            {
                Nkey.Close();
            }
        }

        public static Societe ReturnSociete()
        {
            RegistryKey Nkey = Registry.LocalMachine;
            try
            {
                return ReturnSociete(Nkey);
            }
            catch (Exception e)
            {
                Messages.Exception("SocieteDAO (getReturnSociete) ", e);
                return null;
            }
            finally
            {
                Nkey.Close();
            }
        }

        public static Societe ReturnSociete(RegistryKey Nkey)
        {
            try
            {
                Societe serveur = new Societe();
                RegistryKey valKey = Nkey.OpenSubKey(@chemin, true);
                if (valKey == null)
                {
                    serveur.Id = 0;
                    serveur.Name = "LYMYTZ SARL";
                    serveur.AdresseIp = "127.0.0.1";
                    serveur.TypeConnexion = "DESKTOP";
                    CreateSociete(serveur);
                }
                else
                {
                    serveur.Id = Convert.ToInt32(valKey.GetValue("id") != null ? valKey.GetValue("id") : 0);
                    serveur.Name = (string)(valKey.GetValue("name") != null ? valKey.GetValue("name") : "");
                    serveur.AdresseIp = (string)(valKey.GetValue("adresse") != null ? valKey.GetValue("adresse") : "");
                    serveur.Port = Convert.ToInt32(valKey.GetValue("port") != null ? valKey.GetValue("port") : 0);
                    serveur.Users = (string)(valKey.GetValue("users") != null ? valKey.GetValue("users") : "");
                    serveur.Password = (string)(valKey.GetValue("password") != null ? valKey.GetValue("password") : "");
                    serveur.Domain = (string)(valKey.GetValue("domain") != null ? valKey.GetValue("domain") : "");
                    serveur.TypeConnexion = (string)(valKey.GetValue("type") != null ? valKey.GetValue("type") : "");
                    valKey.Close();
                }
                return serveur;
            }
            catch (Exception e)
            {
                Messages.Exception("SocieteDAO (getReturnSociete) ", e);
                return null;
            }
            finally
            {
                Nkey.Close();
            }
        }

        public static bool getUpdate(Societe bean)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "update yvs_societes set adresse_ip = '" + bean.AdresseIp + "' where id = " + bean.Id;
                bool result = false;
                using (NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect))
                    result = Lcmd.ExecuteNonQuery() > 0;
                if (result)
                {
                    long id = 0;
                    query = "SELECT id FROM yvs_societes_connexion WHERE societe = " + bean.Id;
                    using (NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect))
                    using (NpgsqlDataReader lect = Lcmd.ExecuteReader())
                    {
                        if (lect.HasRows && lect.Read())
                        {
                            id = Convert.ToInt32(lect["id"] != null ? lect["id"].ToString() : "0");
                        }
                    }
                    if (id > 0)
                        query = "update yvs_societes_connexion set port = " + bean.Port + ", users = '" + bean.Users + "', password = '" + bean.Password + "', domain = '" + bean.Domain + "', type_connexion = '" + bean.TypeConnexion + "' where id = " + id;
                    else
                        query = "insert into yvs_societes_connexion(port, users, password, domain, type_connexion, societe)  VALUES(" + bean.Port + ", '" + bean.Users + "', '" + bean.Password + "', '" + bean.Domain + "', '" + bean.TypeConnexion + "', " + bean.Id + ")";

                    using (NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect))
                        Lcmd.ExecuteNonQuery();
                }
                return result;
            }
            catch (Exception ex)
            {
                Messages.Exception("SocieteDao (getUpdate) ", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        private static Societe Get(NpgsqlDataReader lect)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            Societe bean = new Societe();
            try
            {
                bean.Id = Convert.ToInt32(lect["id"].ToString());
                bean.Name = lect["name"].ToString();
                bean.AdresseIp = lect["adresse_ip"].ToString();
                bean.Port = Convert.ToInt32(lect["port"] != null ? lect["port"].ToString() : "0");
                bean.Users = lect["users"].ToString();
                bean.Password = lect["password"].ToString();
                bean.Domain = lect["domain"].ToString();
                bean.TypeConnexion = lect["type_connexion"].ToString();
            }
            catch (Exception ex)
            {
                Messages.Exception(ex);
            }
            return bean;
        }

        public static Societe getOneByName(string name)
        {
            Societe bean = new Societe();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select y.id, y.name, y.adresse_ip, COALESCE(i.port, 0) AS port, i.users, i.password, i.domain, i.type_connexion " +
                            "from yvs_societes y left join yvs_societes_connexion i on i.societe = y.id where y.name ='" + name + "';";
                using (NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect))
                using (NpgsqlDataReader lect = Lcmd.ExecuteReader())
                {
                    if (lect.HasRows)
                    {
                        while (lect.Read())
                        {
                            bean = Get(lect);
                        }
                    }
                }
                return bean;
            }
            catch (Exception ex)
            {
                Messages.Exception("SocieteDao (getOneByName) ", ex);
                return bean;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static Societe getOneById(int id)
        {
            Societe bean = new Societe();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select y.id, y.name, y.adresse_ip, COALESCE(i.port, 0) AS port, i.users, i.password, i.domain, i.type_connexion " +
                            "from yvs_societes y left join yvs_societes_connexion i on i.societe = y.id where y.id = " + id + ";";
                using (NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect))
                using (NpgsqlDataReader lect = Lcmd.ExecuteReader())
                {
                    if (lect.HasRows)
                    {
                        while (lect.Read())
                        {
                            bean = Get(lect);
                        }
                    }
                }
                return bean;
            }
            catch (Exception ex)
            {
                Messages.Exception("SocieteDao (getOneById) ", ex);
                return bean;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static List<Societe> getList(string query)
        {
            List<Societe> list = new List<Societe>();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                using (NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect))
                using (NpgsqlDataReader lect = Lcmd.ExecuteReader())
                {
                    if (lect.HasRows)
                    {
                        while (lect.Read())
                        {
                            list.Add(Get(lect));
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                Messages.Exception("SocieteDao (getList) ", ex);
                return list;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }
    }
}
