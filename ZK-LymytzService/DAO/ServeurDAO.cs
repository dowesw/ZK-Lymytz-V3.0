using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using ZK_LymytzService.ENTITE;
using ZK_LymytzService.TOOLS;
using Microsoft.Win32;
using Npgsql;

namespace ZK_LymytzService.DAO
{
    public class ServeurDAO
    {
        static string chemin = Chemins.getCheminServeur();

        public static bool _getCreateServeur(Serveur serveur)
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
                valKey.SetValue("adresse", serveur.Adresse);
                valKey.SetValue("port", serveur.Port);
                valKey.SetValue("database", serveur.Database);
                valKey.SetValue("user", serveur.User);
                valKey.SetValue("password", serveur.Password);
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

        public static Serveur _getReturnServeur()
        {
            RegistryKey Nkey = Registry.LocalMachine;
            try
            {
                Serveur serveur = _getReturnServeur(Nkey);
                if (serveur != null ? (serveur.Adresse != null ? serveur.Adresse.Trim().Length < 1 : true) : true)
                {
                    RegistryKey Nkey_ = Registry.CurrentUser;
                    try
                    {
                        serveur = _getReturnServeur(Nkey_);
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

        public static Serveur _getReturnServeur(RegistryKey Nkey)
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
                    _getCreateServeur(serveur);
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
                throw new Exception(e.Message);
            }
            finally
            {
                Nkey.Close();
            }
        }
    }
}
