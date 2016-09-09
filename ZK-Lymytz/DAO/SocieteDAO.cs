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
        static string chemin = Chemins.getCheminSociete();
        static List<Societe> listeSociete = new List<Societe>();

        public static bool getCreateSociete(Societe societe)
        {

            try
            {
                using (RegistryKey Nkey = Registry.CurrentUser)
                {
                    getCreateSociete(societe, Nkey);
                }
                using (RegistryKey Nkey = Registry.LocalMachine)
                {
                    getCreateSociete(societe, Nkey);
                }
                return true;
            }
            catch (Exception e)
            {
                Messages.Exception("SocieteDAO (getCreateSociete) ", e);
                return false;
            }
        }


        public static bool getCreateSociete(Societe societe, RegistryKey Nkey)
        {
            try
            {
                RegistryKey valKey = Nkey.OpenSubKey(@chemin, true);
                if (valKey == null)
                {
                    Nkey.CreateSubKey(@chemin);
                    valKey = Nkey.OpenSubKey(@chemin, true);
                }
                valKey.SetValue("id", societe.Id);
                valKey.SetValue("name", societe.Name);
                return true;
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

        public static Societe getReturnSociete()
        {
            RegistryKey Nkey = Registry.CurrentUser;
            try
            {
                Societe serveur = getReturnSociete(Nkey);
                if (serveur != null ? serveur.Id < 1 : true)
                {
                    RegistryKey Nkey_ = Registry.LocalMachine;
                    try
                    {
                        serveur = getReturnSociete(Nkey);
                    }
                    catch (Exception e)
                    {
                        Messages.Exception("SocieteDAO (getReturnSociete) ", e);
                        return null;
                    }
                    finally
                    {
                        Nkey_.Close();
                    }
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

        public static Societe getReturnSociete(RegistryKey Nkey)
        {
            try
            {
                Societe serveur = new Societe();
                RegistryKey valKey = Nkey.OpenSubKey(@chemin, true);
                if (valKey == null)
                {
                    serveur.Id = 0;
                    serveur.Name = "LYMYTZ SARL";
                    getCreateSociete(serveur);
                }
                else
                {
                    serveur.Id = Convert.ToInt32(valKey.GetValue("id") != null ? valKey.GetValue("id") : 0);
                    serveur.Name = (string)(valKey.GetValue("name") != null ? valKey.GetValue("name") : "");
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

        public static Societe getOneByName(string name)
        {
            Societe bean = new Societe();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_societes where name ='" + name + "';";
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        bean.Id = Convert.ToInt32(lect["id"].ToString());
                        bean.Name = lect["name"].ToString();
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
                connect.Close();
            }
        }

        public static Societe getOneById(int id)
        {
            Societe bean = new Societe();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_societes where id =" + id + ";";
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        bean.Id = Convert.ToInt32(lect["id"].ToString());
                        bean.Name = lect["name"].ToString();
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
                connect.Close();
            }
        }

        public static List<Societe> getList(string query)
        {
            List<Societe> list = new List<Societe>();
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
                Messages.Exception("SocieteDao (getList) ", ex);
                return list;
            }
            finally
            {
                connect.Close();
            }
        }
    }
}
