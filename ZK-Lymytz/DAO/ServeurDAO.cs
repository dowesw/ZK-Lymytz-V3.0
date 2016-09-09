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
        static string chemin = Chemins.getCheminServeur();

        public static bool getCreateServeur(Serveur serveur)
        {
            try
            {
                using (RegistryKey Nkey = Registry.CurrentUser)
                {
                    getCreateServeur(serveur, Nkey);
                }
                using (RegistryKey Nkey = Registry.LocalMachine)
                {
                    getCreateServeur(serveur, Nkey);
                }
            }
            catch (Exception e)
            {
                Messages.Exception("ServeurDAO (getCreateServeur)", e);
                return false;
            }
            return true;
        }

        public static bool getCreateServeur(Serveur serveur, RegistryKey Nkey)
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

        public static Serveur getReturnServeur()
        {
            RegistryKey Nkey = Registry.CurrentUser;
            try
            {
                Serveur serveur = getReturnServeur(Nkey);
                if (serveur != null ? (serveur.Adresse != null ? serveur.Adresse.Trim().Length < 1 : true) : true)
                {
                    RegistryKey Nkey_ = Registry.CurrentUser;
                    try
                    {
                        serveur = getReturnServeur(Nkey_);
                    }
                    catch (Exception e)
                    {
                        Messages.Exception("ServeurDAO (getReturnServeur)", e);
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
                Messages.Exception("ServeurDAO (getReturnServeur)", e);
                return null;
            }
            finally
            {
                Nkey.Close();
            }
        }

        public static Serveur getReturnServeur(RegistryKey Nkey)
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
                    getCreateServeur(serveur);
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
