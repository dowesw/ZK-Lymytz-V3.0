using System;
using System.Collections.Generic;
using System.Text;

using Npgsql;
using Microsoft.Win32;
using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;

namespace ZK_Lymytz.DAO
{
    public class UsersDAO
    {
        static string chemin = Chemins.CheminUsers();
        static List<Users> listeUsers = new List<Users>();

        private static Users Return(NpgsqlDataReader lect)
        {
            Users bean = new Users();
            try
            {
                bean.Id = Convert.ToInt32(lect["id"].ToString());
                bean.Code = lect["code_users"].ToString();
                bean.NomUsers = lect["nom_users"].ToString();
                bean.Password = lect["password_user"].ToString();
                bean.AleaMdp = lect["alea_mdp"].ToString();
                bean.Actif = Convert.ToBoolean((lect["actif"].ToString() != "") ? lect["actif"].ToString() : "false");
                bean.AccesMultiAgence = Convert.ToBoolean((lect["acces_multi_agence"].ToString() != "") ? lect["acces_multi_agence"].ToString() : "false");
                bean.AccesMultiSociete = Convert.ToBoolean((lect["acces_multi_societe"].ToString() != "") ? lect["acces_multi_societe"].ToString() : "false");
                bean.SuperAdmin = Convert.ToBoolean((lect["super_admin"].ToString() != "") ? lect["super_admin"].ToString() : "false");
                bean.Agence = new Agence(Convert.ToInt32(lect["agence"].ToString()), lect["designation"].ToString());
                bean.Agence.Societe = new Societe(Convert.ToInt32(lect["societe"].ToString()), lect["name"].ToString(), Convert.ToInt32(lect["groupe"].ToString()), lect["adresse_ip"].ToString());
            }
            catch (Exception ex)
            {
                Messages.Exception(ex);
            }
            return bean;
        }

        public static bool CreateUsers(Users user)
        {
            try
            {
                using (RegistryKey Nkey = Registry.LocalMachine)
                {
                    CreateUsers(user, Nkey);
                }
                return true;
            }
            catch (Exception e)
            {
                Messages.Exception("UsersDAO (getCreateUsers) ", e);
                return false;
            }
        }

        public static bool CreateUsers(Users user, RegistryKey Nkey)
        {
            try
            {
                RegistryKey valKey = Nkey.OpenSubKey(@chemin, true);
                if (valKey == null)
                {
                    Nkey.CreateSubKey(@chemin);
                    valKey = Nkey.OpenSubKey(@chemin, true);
                }
                valKey.SetValue("id", user.Id);
                valKey.SetValue("code", user.Code);
                valKey.SetValue("nom_users", user.NomUsers);
                valKey.SetValue("author", user.Author);
                valKey.SetValue("name", user.Name);
                valKey.SetValue("passwordpc", user.PasswordPC);
                valKey.SetValue("passwordlog", user.PasswordLog);
                return true;
            }
            catch (Exception e)
            {
                Messages.Exception("UsersDAO (getCreateUsers) ", e);
                return false;
            }
            finally
            {
                Nkey.Close();
            }
        }

        public static Users ReturnUsers()
        {
            RegistryKey Nkey = Registry.LocalMachine;
            try
            {
                return ReturnUsers(Nkey);
            }
            catch (Exception e)
            {
                Messages.Exception("UsersDAO (getReturnUsers) ", e);
                return null;
            }
            finally
            {
                Nkey.Close();
            }
        }

        public static Users ReturnUsers(RegistryKey Nkey)
        {
            try
            {
                Users user = new Users();
                RegistryKey valKey = Nkey.OpenSubKey(@chemin, true);
                if (valKey != null)
                {
                    user.Id = (int)(valKey.GetValue("id") != null ? valKey.GetValue("id") : 0);
                    user.Code = (string)(valKey.GetValue("code") != null ? valKey.GetValue("code") : "");
                    user.NomUsers = (string)(valKey.GetValue("nom_users") != null ? valKey.GetValue("nom_users") : "");
                    user.Author = (int)(valKey.GetValue("author") != null ? valKey.GetValue("author") : 0);
                    user.PasswordPC = (string)(valKey.GetValue("passwordpc") != null ? valKey.GetValue("passwordpc") : "");
                    user.PasswordLog = (string)(valKey.GetValue("passwordlog") != null ? valKey.GetValue("passwordlog") : "");
                    user.Name = (string)(valKey.GetValue("name") != null ? valKey.GetValue("name") : "");
                    valKey.Close();
                }
                return user;
            }
            catch (Exception e)
            {
                Messages.Exception("UsersDAO (getReturnUsers) ", e);
                return null;
            }
            finally
            {
                Nkey.Close();
            }
        }

        public static bool DestroyUsers()
        {
            RegistryKey Nkey = Registry.CurrentUser;
            try
            {
                Users user = new Users();
                RegistryKey valKey = Nkey.OpenSubKey(@chemin, true);
                if (valKey != null)
                {
                    Nkey.DeleteSubKey(@chemin);
                }
                return true;
            }
            catch (Exception e)
            {
                Messages.Exception("UsersDAO (getDestroyUsers) ", e);
                return false;
            }
            finally
            {
                Nkey.Close();
            }
        }

        public static Users getOneById(int id)
        {
            Users bean = new Users();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select u.*, a.designation, a.designation, a.societe, s.name, s.groupe, s.adresse_ip from yvs_users u inner join yvs_agences a on u.agence = a.id inner join yvs_societes s on a.societe = s.id where id = " + id + ";";
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
                Messages.Exception("UsersDao (getOneById) ", ex);
                return bean;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static Users getOneByName(string code)
        {
            Users bean = new Users();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select u.*, a.designation, a.designation, a.societe, s.name, s.groupe, s.adresse_ip from yvs_users u inner join yvs_agences a on u.agence = a.id inner join yvs_societes s on a.societe = s.id where code_users = '" + code + "';";
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        bean = Return(lect);
                        break;
                    }
                }
                return bean;
            }
            catch (Exception ex)
            {
                Messages.Exception("UsersDao (getOneByName) ", ex);
                return bean;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }
    }
}
