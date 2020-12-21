using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using Npgsql;
using ZK_Lymytz.TOOLS;
using ZK_Lymytz.BLL;

namespace ZK_Lymytz.TOOLS
{
    class Connexion
    {
        NpgsqlConnection INSTANCE = null;

        public NpgsqlConnection Connection()
        {
            if (INSTANCE != null)
            {
                if (INSTANCE.State == System.Data.ConnectionState.Closed)
                {
                    INSTANCE.Open();
                }
            }
            else
            {
                ENTITE.Serveur bean = ServeurBLL.ReturnServeur();
                if ((bean != null) ? bean.Port != 0 : false)
                {
                    INSTANCE = returnConnexion(bean, true);
                }
            }
            return INSTANCE;
        }

        public NpgsqlConnection Connection(string adresse)
        {
            if (Utils.asString(adresse))
            {
                ENTITE.Serveur bean = ServeurBLL.ReturnServeur();
                if ((bean != null) ? bean.Port != 0 : false)
                {
                    NpgsqlConnection connexion = returnConnexion(bean, adresse, true);
                    return connexion;
                }
                return null;
            }
            else
            {
                return Connection();
            }
        }

        public static void Close(NpgsqlConnection con)
        {
            if (con != null)
            {
                con.Close();
                con.Dispose();
            }
            con = null;
        }

        public NpgsqlConnection returnConnexion(ENTITE.Serveur bean, bool retry)
        {
            return returnConnexion(bean, null, retry);
        }

        public NpgsqlConnection returnConnexion(ENTITE.Serveur bean, string adresse, bool retry)
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection();
                if (isConnection(out con, bean, adresse))
                {
                    return con;
                }
                else
                {
                    if (!Utils.asString(adresse) && retry)
                    {
                        if (DialogResult.Retry == Messages.Erreur_Retry("Connexion impossible !Entrer de nouveaux parametres"))
                        {
                            new IHM.Form_Serveur().ShowDialog();
                        }
                        else
                        {
                            Environment.Exit(1);
                        }
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                if (retry)
                {
                    Messages.Exception("Connexion (returnConnexion) ", ex);
                }
                return null;
            }
        }

        public bool isConnection(ENTITE.Serveur bean)
        {
            return isConnection(bean, null);
        }

        public bool isConnection(ENTITE.Serveur bean, string adresse)
        {
            try
            {
                NpgsqlConnection con;
                return isConnection(out con, bean, adresse);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool isConnection(out NpgsqlConnection con, ENTITE.Serveur bean)
        {
            return isConnection(out con, bean, null);
        }

        public bool isConnection(out NpgsqlConnection con, ENTITE.Serveur bean, string adresse)
        {
            con = null;
            if (bean == null)
            {
                return false;
            }
            try
            {
                string current = bean.Adresse;
                if (Utils.asString(adresse))
                {
                    current = adresse;
                }
                return isConnection(out con, current, bean.Port, bean.Database, bean.User, bean.Password);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool isConnection(string adresse, int port, string database, string users, string password)
        {
            try
            {
                NpgsqlConnection con;
                return isConnection(out con, adresse, port, database, users, password);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool isConnection(out NpgsqlConnection con, string adresse, int port, string database, string users, string password)
        {
            con = null;
            try
            {
                string constr = "PORT=" + port + ";TIMEOUT=15;POOLING=True;MINPOOLSIZE=1;MAXPOOLSIZE=20;COMMANDTIMEOUT=20;COMPATIBLE= 2.0.14.3;DATABASE=" + database + ";HOST=" + adresse + ";PASSWORD=" + password + ";USER ID=" + users + "";
                con = new NpgsqlConnection(constr);
                con.Open();
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("Connexion (isConnection) ", ex);
                return false;
            }
        }
    }
}
