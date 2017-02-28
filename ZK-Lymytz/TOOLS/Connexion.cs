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
                    INSTANCE = returnConnexion(bean);
                }
            }
            return INSTANCE;
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

        public NpgsqlConnection onConnection()
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection();
                string constr = "PORT=5432;TIMEOUT=15;POOLING=True;MINPOOLSIZE=1;MAXPOOLSIZE=20;COMMANDTIMEOUT=20;COMPATIBLE= 2.0.14.3;DATABASE=lymytz;HOST=192.168.1.251;PASSWORD=yves1910/;USER ID=postgres";
                con = new NpgsqlConnection(constr);
                con.Open();
                return con;
            }
            catch (Exception ex)
            {
                Messages.Exception("Connexion (onConnection) ", ex);
                return null;
            }
        }

        public NpgsqlConnection returnConnexion(ENTITE.Serveur bean)
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection();
                if (isConnection(out con, bean))
                {
                    return con;
                }
                else
                {
                    if (DialogResult.Retry == Messages.Erreur_Retry("Connexion impossible !Entrer de nouveaux parametres"))
                    {
                        new IHM.Form_Serveur().ShowDialog();
                    }
                    else
                    {
                        Environment.Exit(1);
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("Connexion (returnConnexion) ", ex);
                return null;
            }
        }

        public bool isConnection(out NpgsqlConnection con, ENTITE.Serveur bean)
        {
            con = null;
            try
            {
                if (bean != null)
                {
                    string constr = "PORT=" + bean.Port + ";TIMEOUT=15;POOLING=True;MINPOOLSIZE=1;MAXPOOLSIZE=20;COMMANDTIMEOUT=20;COMPATIBLE= 2.0.14.3;DATABASE=" + bean.Database + ";HOST=" + bean.Adresse + ";PASSWORD=" + bean.Password + ";USER ID=" + bean.User + "";
                    con = new NpgsqlConnection(constr);
                    try
                    {
                        con.Open();
                        return true;
                    }
                    catch (System.StackOverflowException ex)
                    {
                        return isConnection(out con, bean);
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool isInfosServeur(ENTITE.Serveur bean)
        {
            try
            {
                if (bean != null)
                {
                    string constr = "PORT=" + bean.Port + ";TIMEOUT=15;POOLING=True;MINPOOLSIZE=1;MAXPOOLSIZE=20;COMMANDTIMEOUT=20;COMPATIBLE= 2.0.14.3;DATABASE=" + bean.Database + ";HOST=" + bean.Adresse + ";PASSWORD=" + bean.Password + ";USER ID=" + bean.User + "";
                    NpgsqlConnection con = new NpgsqlConnection(constr);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("Connexion (isInfosServeur) ", ex);
                return false;
            }
        }

        public static bool RequeteLibre(string query)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                if (query != null ? query.Trim().Length > 0 : false)
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Messages.Exception("Connexion (RequeteLibre) ", ex);
                return false;
            }
            finally
            {
                Close(connect);
            }
        }

        public static object LoadOneObject(string query)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                if (query != null ? query.Trim().Length > 0 : false)
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                    NpgsqlDataReader lect = cmd.ExecuteReader();
                    if (lect.HasRows)
                    {
                        while (lect.Read())
                        {
                            return lect[0];
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Messages.Exception("Connexion (LoadOneObject) ", ex);
                return null;
            }
            finally
            {
                Close(connect);
            }
        }

        public static List<string> LoadListObject(string query)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                List<string> list = new List<string>();
                if (query != null ? query.Trim().Length > 0 : false)
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                    NpgsqlDataReader lect = cmd.ExecuteReader();
                    if (lect.HasRows)
                    {
                        while (lect.Read())
                        {
                            list.Add(lect[0].ToString());
                        }
                    }

                }
                return list;
            }
            catch (Exception ex)
            {
                Messages.Exception("Connexion (LoadListObject) ", ex);
                return null;
            }
            finally
            {
                Close(connect);
            }
        }
    }
}
