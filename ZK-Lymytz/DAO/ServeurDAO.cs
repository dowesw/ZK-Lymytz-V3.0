using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;
using Microsoft.Win32;
using Npgsql;

namespace ZK_Lymytz.DAO
{
    class ServeurDAO
    {
        static string chemin = Chemins.CheminServeur();

        public static bool CreateServeur(Serveur serveur)
        {
            try
            {
                using (RegistryKey Nkey = Registry.LocalMachine)
                {
                    CreateServeur(serveur, Nkey);
                }
            }
            catch (Exception e)
            {
                Messages.Exception("ServeurDAO (getCreateServeur)", e);
                return false;
            }
            return true;
        }

        public static bool CreateServeur(Serveur serveur, RegistryKey Nkey)
        {
            try
            {
                RegistryKey valKey = Nkey.OpenSubKey(@chemin, true);
                if (valKey == null)
                {
                    Nkey.CreateSubKey(@chemin);
                    valKey = Nkey.OpenSubKey(@chemin, true);
                }
                valKey.SetValue("adresse", serveur.Adresse);
                valKey.SetValue("port", serveur.Port);
                valKey.SetValue("database", serveur.Database);
                valKey.SetValue("user", serveur.User);
                valKey.SetValue("password", serveur.Password);
            }
            catch (Exception e)
            {
                Messages.Exception("ServeurDAO (getCreateServeur)", e);
                return false;
            }
            finally
            {
                Nkey.Close();
            }
            return true;
        }

        public static Serveur ReturnServeur()
        {
            RegistryKey Nkey = Registry.LocalMachine;
            try
            {
                return ReturnServeur(Nkey);
            }
            catch (Exception e)
            {
                Messages.Exception("ServeurDAO (getReturnServeur)", e);
                return null;
            }
            finally
            {
                Nkey.Close();
            }
        }

        public static Serveur ReturnServeur(RegistryKey Nkey)
        {
            try
            {
                Serveur serveur = new Serveur();
                RegistryKey valKey = Nkey.OpenSubKey(@chemin, true);
                if (valKey == null)
                {
                    serveur.Adresse = "127.0.0.1";
                    serveur.Port = 5432;
                    serveur.Database = "lymytz_demo_0";
                    serveur.User = "postgres";
                    serveur.Password = "yves1910/";
                    CreateServeur(serveur);
                }
                else
                {
                    serveur.Adresse = (string)(valKey.GetValue("adresse") != null ? valKey.GetValue("adresse") : "");
                    serveur.Port = Convert.ToInt32(valKey.GetValue("port") != null ? valKey.GetValue("port") : 0);
                    serveur.Database = (string)(valKey.GetValue("database") != null ? valKey.GetValue("database") : "");
                    serveur.User = (string)(valKey.GetValue("user") != null ? valKey.GetValue("user") : "");
                    serveur.Password = (string)(valKey.GetValue("password") != null ? valKey.GetValue("password") : "");
                    valKey.Close();
                }
                return serveur;
            }
            catch (Exception e)
            {
                Messages.Exception("ServeurDAO (getReturnServeur)", e);
                return null;
            }
            finally
            {
                Nkey.Close();
            }
        }
    }
}
