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

        public static bool CreateUsers(Users user)
        {
            try
            {
                using (RegistryKey Nkey = Registry.LocalMachine)
                {
                    CreateUsers(user, Nkey);
                }
                if (Utils.Is64BitOperatingSystem())
                {
                    using (RegistryKey Nkey = Registry.CurrentUser)
                    {
                        CreateUsers(user, Nkey);
                    }
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
                valKey.SetValue("passwordpc", user.PasswordPC);
                valKey.SetValue("passwordlog", user.PasswordLog);
                valKey.SetValue("name", user.Name);
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
                Users user = ReturnUsers(Nkey);
                if (user != null ? (user.Name != null ? user.Name.Trim().Length < 1 : true) : true)
                {
                    Nkey = Registry.CurrentUser;
                    user = ReturnUsers(Nkey);
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

        public static Users ReturnUsers(RegistryKey Nkey)
        {
            try
            {
                Users user = new Users();
                RegistryKey valKey = Nkey.OpenSubKey(@chemin, true);
                if (valKey != null)
                {
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
                string query = "select * from yvs_users where id =" + id + ";";
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        bean.Id = Convert.ToInt32(lect["id"].ToString());
                        bean.Code = lect["code_users"].ToString();
                        bean.Name = lect["nom_users"].ToString();
                        bean.PasswordLog = lect["password_user"].ToString();
                        bean.AleaMdp = lect["alea_mdp"].ToString();
                        bean.Actif = Convert.ToBoolean((lect["actif"].ToString() != "") ? lect["actif"].ToString() : "false");
                        bean.AccesPointeuse = Convert.ToBoolean((lect["actif"].ToString() != "") ? lect["actif"].ToString() : "false");
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

        public static Users getOneByName(string id, string password)
        {
            Users bean = new Users();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_users where code_users ='" + id + "' and password_user = '" + password + "';";
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        bean.Id = Convert.ToInt32(lect["id"].ToString());
                        bean.Code = lect["code_users"].ToString();
                        bean.Name = lect["nom_users"].ToString();
                        bean.PasswordLog = lect["password_user"].ToString();
                        bean.AleaMdp = lect["alea_mdp"].ToString();
                        bean.Actif = Convert.ToBoolean((lect["actif"].ToString() != "") ? lect["actif"].ToString() : "false");
                        bean.AccesPointeuse = Convert.ToBoolean((lect["actif"].ToString() != "") ? lect["actif"].ToString() : "false");
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
