using System;
using System.Collections.Generic;
using System.Text;

using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;
using Npgsql;

namespace ZK_Lymytz.DAO
{
    class EmpreinteDAO
    {
        private static Empreinte Return(NpgsqlDataReader lect)
        {
            Empreinte bean = new Empreinte();
            bean.Id = Convert.ToInt32(lect["id"].ToString());
            bean.Digital = Convert.ToInt32(lect["empreinte_digital"].ToString());
            bean.Facial = Convert.ToInt32(lect["empreinte_faciale"].ToString());
            bean.Numerique = Convert.ToInt32(lect["empreinte_numerique"].ToString());
            bean.Flag = Convert.ToInt32(lect["flag"].ToString());
            bean.Longueur = Convert.ToInt32(lect["longueur"].ToString());
            bean.Employe = EmployeDAO.getOneById(Convert.ToInt32(lect["employe"].ToString()));
            bean.Template = Convert.FromBase64String(lect["template"].ToString());
            bean.STemplate = lect["template"].ToString(); 
            return bean;
        }

        public static Empreinte getOneById(long id)
        {
            Empreinte bean = new Empreinte();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_grh_empreinte_employe where id =" + id + ";";
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
                Messages.Exception("EmpreinteDao (getOneById) ", ex);
                return bean;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static Empreinte getOneByEmployeFinger(long employe, int finger)
        {
            Empreinte bean = new Empreinte();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_grh_empreinte_employe where employe =" + employe + " and empreinte_digital = " + finger;
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
                Messages.Exception("EmpreinteDao (getOneByEmployeFinger) ", ex);
                return bean;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static Empreinte getOneByEmployeFinger(long employe, int finger, NpgsqlConnection connect)
        {
            Empreinte bean = new Empreinte();
            try
            {
                string query = "select * from yvs_grh_empreinte_employe where employe =" + employe + " and empreinte_digital = " + finger;
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
                Messages.Exception("EmpreinteDao (getOneByEmployeFinger) ", ex);
                return bean;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static List<Empreinte> getList(string query)
        {
            List<Empreinte> list = new List<Empreinte>();
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
                Messages.Exception("EmpreinteDao (getList) ", ex);
                return list;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static bool getInsert(Empreinte bean)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "insert into yvs_grh_empreinte_employe(longueur, empreinte_digital, empreinte_faciale, empreinte_numerique, template, flag, employe) values (" + bean.Longueur + "," + bean.Digital + "," + bean.Facial + "," + bean.Numerique + ",'" + bean.STemplate + "'," + bean.Flag + "," + bean.Employe.Id + ")";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("EmpreinteDao (getInsert) ", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static bool getInsert(Empreinte bean, NpgsqlConnection connect)
        {
            try
            {
                string query = "insert into yvs_grh_empreinte_employe(longueur, empreinte_digital, empreinte_faciale, empreinte_numerique, template, flag, employe) values (" + bean.Longueur + "," + bean.Digital + "," + bean.Facial + "," + bean.Numerique + ",'" + bean.STemplate + "'," + bean.Flag + "," + bean.Employe.Id + ")";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("EmpreinteDao (getInsert) ", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static bool getUpdate(Empreinte bean,long id)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "update yvs_grh_empreinte_employe set longueur = " + bean.Longueur + ",empreinte_digital = " + bean.Digital + ",empreinte_faciale = " + bean.Facial + ",empreinte_numerique = " + bean.Numerique + ",template = '" + bean.STemplate + "',flag = " + bean.Flag + ",employe = " + bean.Employe.Id + " where id = " + id + "";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("EmpreinteDao (getUpdate) ", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static bool getDelete(long id)
        {
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "delete from into yvs_grh_empreinte_employe where id = " + id + "";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("EmpreinteDao (getDelete) ", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }
    }
}
