using System;
using System.Collections.Generic;
using System.Text;

using Npgsql;
using Microsoft.Win32;
using ZK_LymytzService.ENTITE;
using ZK_LymytzService.TOOLS;

namespace ZK_LymytzService.DAO
{
    public class UsersDAO
    {
        static string chemin = Chemins.getCheminUsers();
        static List<Users> listeUsers = new List<Users>();

        public static bool _getCreateUsers(Users user)
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
                valKey.SetValue("password", user.Password);
                valKey.SetValue("name", user.Name);
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

        public static Users _getReturnUsers()
        {
            RegistryKey Nkey = Registry.LocalMachine;
            try
            {
                Users user = _getReturnUsers(Nkey);
                if (user != null ? (user.Name != null ? user.Name.Trim().Length < 1 : true) : true)
                {
                    RegistryKey Nkey_ = Registry.CurrentUser;
                    try
                    {
                        user = _getReturnUsers(Nkey_);
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
                return user;
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

        public static Users _getReturnUsers(RegistryKey Nkey)
        {
            try
            {
                Users user = new Users();
                RegistryKey valKey = Nkey.OpenSubKey(@chemin, true);
                if (valKey != null)
                {
                    user.Password = (string)(valKey.GetValue("password") != null ? valKey.GetValue("password") : "");
                    user.Name = (string)(valKey.GetValue("name") != null ? valKey.GetValue("name") : "");
                    valKey.Close();
                }
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                Nkey.Close();
            }
        }

        public static bool _getDestroyUsers()
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
                throw new Exception(e.Message);
            }
            finally
            {
                Nkey.Close();
            }

        }
    }
}
