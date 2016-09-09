using System;
using System.Collections.Generic;
using System.Text;

using Npgsql;
using Microsoft.Win32;
using ZK_LymytzService.ENTITE;
using ZK_LymytzService.TOOLS;

namespace ZK_LymytzService.DAO
{
    public class SocieteDAO
    {
        static string chemin = Chemins.getCheminSociete();
        static List<Societe> listeSociete = new List<Societe>();

        public static bool _getCreateSociete(Societe societe)
        {
            RegistryKey Nkey = Registry.CurrentUser;
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
                throw new Exception(e.Message);
            }
            finally
            {
                Nkey.Close();
            }
        }


        public static Societe _getReturnSociete()
        {
            RegistryKey Nkey = Registry.LocalMachine;
            try
            {
                Societe serveur = _getReturnSociete(Nkey);
                if (serveur != null ? serveur.Id < 1 : true)
                {
                    RegistryKey Nkey_ = Registry.CurrentUser;
                    try
                    {
                        serveur = _getReturnSociete(Nkey);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);

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
                throw new Exception(e.Message);

            }
            finally
            {
                Nkey.Close();
            }
        }

        public static Societe _getReturnSociete(RegistryKey Nkey)
        {
            try
            {
                Societe serveur = new Societe();
                RegistryKey valKey = Nkey.OpenSubKey(@chemin, true);
                if (valKey == null)
                {
                    serveur.Id = 0;
                    serveur.Name = "LYMYTZ SARL";
                    _getCreateSociete(serveur);
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
                throw new Exception(e.Message);

            }
            finally
            {
                Nkey.Close();
            }
        }

        public static Societe _getOneByName(string name)
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
                throw new Exception(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }

        public static Societe _getOneById(int id)
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
                throw new Exception(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }

        public static List<Societe> _getList(string query)
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
