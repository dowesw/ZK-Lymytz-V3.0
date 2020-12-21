using System;
using System.Collections.Generic;
using System.Text;

using ZK_Lymytz.TOOLS;
using ZK_Lymytz.ENTITE;

using NpgsqlTypes;
using Microsoft.Win32;
using Npgsql;

namespace ZK_Lymytz.DAO
{
    class AgenceDAO
    {
        static string chemin = Chemins.CheminAgence();

        public static bool CreateAgence(Agence agence)
        {
            try
            {
                using (RegistryKey Nkey = Registry.LocalMachine)
                {
                    CreateAgence(agence, Nkey);
                }
                return true;
            }
            catch (Exception e)
            {
                Messages.Exception("AgenceDAO (CreateAgence) ", e);
                return false;
            }
        }

        public static bool CreateAgence(Agence agence, RegistryKey Nkey)
        {
            try
            {
                if (agence != null)
                {
                    RegistryKey valKey = Nkey.OpenSubKey(@chemin, true);
                    if (valKey == null)
                    {
                        Nkey.CreateSubKey(@chemin);
                        valKey = Nkey.OpenSubKey(@chemin, true);
                    }
                    valKey.SetValue("id", agence.Id);
                    valKey.SetValue("name", agence.Name);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Messages.Exception("AgenceDAO (CreateAgence) ", e);
                return false;
            }
            finally
            {
                Nkey.Close();
            }
        }

        public static bool RemoveAgence()
        {
            try
            {
                using (RegistryKey Nkey = Registry.LocalMachine)
                {
                    RemoveAgence(Nkey);
                }
                return true;
            }
            catch (Exception e)
            {
                Messages.Exception("AgenceDAO (RemoveAgence) ", e);
                return false;
            }
        }

        public static bool RemoveAgence(RegistryKey Nkey)
        {
            try
            {
                RegistryKey valKey = Nkey.OpenSubKey(@chemin, true);
                if (valKey == null)
                {
                    Nkey.CreateSubKey(@chemin);
                    valKey = Nkey.OpenSubKey(@chemin, true);
                }
                valKey.SetValue("id", 0);
                valKey.SetValue("name", "---");
                return true;
            }
            catch (Exception e)
            {
                Messages.Exception("AgenceDAO (RemoveAgence) ", e);
                return false;
            }
            finally
            {
                Nkey.Close();
            }
        }

        public static Agence ReturnAgence()
        {
            RegistryKey Nkey = Registry.LocalMachine;
            try
            {
                return ReturnAgence(Nkey);
            }
            catch (Exception e)
            {
                Messages.Exception("AgenceDAO (ReturnAgence) ", e);
                return null;
            }
            finally
            {
                Nkey.Close();
            }
        }

        public static Agence ReturnAgence(RegistryKey Nkey)
        {
            try
            {
                Agence agence = new Agence();
                RegistryKey valKey = Nkey.OpenSubKey(@chemin, true);
                if (valKey == null)
                {
                    agence.Id = 0;
                    agence.Name = "DIRECTION";
                    CreateAgence(agence);
                }
                else
                {
                    agence.Id = Convert.ToInt32(valKey.GetValue("id") != null ? valKey.GetValue("id") : 0);
                    agence.Name = (string)(valKey.GetValue("name") != null ? valKey.GetValue("name") : "");
                    valKey.Close();
                }
                return agence;
            }
            catch (Exception e)
            {
                Messages.Exception("AgenceDAO (ReturnAgence) ", e);
                return null;
            }
            finally
            {
                Nkey.Close();
            }
        }

        private static Agence Return(NpgsqlDataReader lect)
        {
            Agence bean = new Agence();
            bean.Id = Convert.ToInt32(lect["id"].ToString());
            bean.Name = lect["designation"].ToString();
            bean.Societe = new Societe(Convert.ToInt32(lect["societe"].ToString()));
            return bean;
        }

        public static Agence getOneById(int id)
        {
            Agence bean = new Agence();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_agences where id =" + id + ";";
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
                Messages.Exception("AgenceDao (getOneById) ", ex);
                return bean;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static List<Agence> getList(string query)
        {
            List<Agence> list = new List<Agence>();
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
                Messages.Exception("AgenceDao (getList) ", ex);
                return list;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }
    }
}
