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
    class LiaisonDAO
    {
        static string chemin = Chemins.CheminLiaisons();

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
                Messages.Exception("LiaisonDAO (getCreateServeur)", e);
                return false;
            }
            return true;
        }

        public static bool CreateServeur(Serveur serveur, RegistryKey Nkey)
        {
            try
            {
                string path = chemin + Constantes.FILE_SEPARATOR + serveur.Adresse;
                RegistryKey valKey = Nkey.OpenSubKey(@path, true);
                if (valKey == null)
                {
                    Nkey.CreateSubKey(@path);
                    valKey = Nkey.OpenSubKey(@path, true);
                }
                valKey.SetValue("adresse", serveur.Adresse);
                valKey.SetValue("port", serveur.Port);
                valKey.SetValue("database", serveur.Database);
                valKey.SetValue("user", serveur.User);
                valKey.SetValue("password", serveur.Password);
                valKey.SetValue("date_debut", serveur.DateDebut.ToString("dd-MM-yyyy"));
            }
            catch (Exception e)
            {
                Messages.Exception("LiaisonDAO (getCreateServeur)", e);
                return false;
            }
            finally
            {
                Nkey.Close();
            }
            return true;
        }

        public static List<Serveur> ReturnServeur()
        {
            RegistryKey Nkey = Registry.LocalMachine;
            try
            {
                return ReturnServeur(Nkey);
            }
            catch (Exception e)
            {
                Messages.Exception("LiaisonDAO (getReturnServeur)", e);
                return null;
            }
            finally
            {
                Nkey.Close();
            }
        }

        public static List<Serveur> ReturnServeur(RegistryKey Nkey)
        {
            try
            {
                List<Serveur> serveurs = new List<Serveur>();
                RegistryKey valKey = Nkey.OpenSubKey(@chemin, true);
                if (valKey != null ? valKey.GetSubKeyNames().Length > 0 : false)
                {
                    Serveur serveur = null;
                    foreach (string name in valKey.GetSubKeyNames())
                    {
                        serveur = ReturnServeur(chemin + Constantes.FILE_SEPARATOR + name, Nkey);
                        if (serveur != null)
                        {
                            serveurs.Add(serveur);
                        }
                    }
                }
                return serveurs;
            }
            catch (Exception e)
            {
                Messages.Exception("LiaisonDAO (getReturnServeur)", e);
                return null;
            }
            finally
            {
                Nkey.Close();
            }
        }

        public static Serveur ReturnServeur(string chemin, RegistryKey Nkey)
        {
            try
            {
                Serveur serveur = new Serveur();
                RegistryKey valKey = Nkey.OpenSubKey(@chemin, true);
                if (valKey != null)
                {
                    serveur.Adresse = (string)(valKey.GetValue("adresse") != null ? valKey.GetValue("adresse") : "");
                    serveur.Port = Convert.ToInt32(valKey.GetValue("port") != null ? valKey.GetValue("port") : 0);
                    serveur.Database = (string)(valKey.GetValue("database") != null ? valKey.GetValue("database") : "");
                    serveur.User = (string)(valKey.GetValue("user") != null ? valKey.GetValue("user") : "");
                    serveur.Password = (string)(valKey.GetValue("password") != null ? valKey.GetValue("password") : "");
                    serveur.DateDebut = valKey.GetValue("date_debut") != null ? Convert.ToDateTime(valKey.GetValue("date_debut")) : DateTime.Now.AddDays(-20);
                    valKey.Close();
                    return serveur;
                }
                return null;
            }
            catch (Exception e)
            {
                Messages.Exception("LiaisonDAO (getReturnServeur)", e);
                return null;
            }
            finally
            {
                Nkey.Close();
            }
        }

        public static bool DeleteServeur(Serveur serveur)
        {
            RegistryKey Nkey = Registry.LocalMachine;
            try
            {
                DeleteServeur(serveur, Nkey);
                return true;
            }
            catch (Exception e)
            {
                Messages.Exception("LiaisonDAO (deleteServeur)", e);
                return false;
            }
            finally
            {
                Nkey.Close();
            }
        }

        public static bool DeleteServeur(Serveur serveur, RegistryKey Nkey)
        {
            try
            {
                RegistryKey valKey = Nkey.OpenSubKey(@chemin, true);
                if (valKey != null ? valKey.GetSubKeyNames().Length > 0 : false)
                {
                    valKey.DeleteSubKey(serveur.Adresse);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Messages.Exception("LiaisonDAO (deleteServeur)", e);
                return false;
            }
            finally
            {
                Nkey.Close();
            }
        }
    }
}
