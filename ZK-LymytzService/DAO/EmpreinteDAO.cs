using System;
using System.Collections.Generic;
using System.Text;

using ZK_LymytzService.ENTITE;
using ZK_LymytzService.TOOLS;
using Npgsql;

namespace ZK_LymytzService.DAO
{
    public class EmpreinteDAO
    {
        public static Empreinte _getOneById(long id)
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
                        bean.Id = Convert.ToInt32(lect["id"].ToString());
                        bean.Digital = Convert.ToInt32(lect["empreinte_digital"].ToString());
                        bean.Facial = Convert.ToInt32(lect["empreinte_faciale"].ToString());
                        bean.Numerique = Convert.ToInt32(lect["empreinte_numerique"].ToString());
                        bean.Flag = Convert.ToInt32(lect["flag"].ToString());
                        bean.Longueur= Convert.ToInt32(lect["longueur"].ToString());
                        bean.Employe = EmployeDAO._getOneById(Convert.ToInt32(lect["employe"].ToString()));
                        bean.Template = Convert.FromBase64String(lect["template"].ToString());
                        bean.STemplate = lect["template"].ToString(); 
                    }
                }
                return bean;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }

        public static Empreinte _getOneByEmployeFinger(long employe, int finger)
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
                        bean.Id = Convert.ToInt32(lect["id"].ToString());
                        bean.Digital = Convert.ToInt32(lect["empreinte_digital"].ToString());
                        bean.Facial = Convert.ToInt32(lect["empreinte_faciale"].ToString());
                        bean.Numerique = Convert.ToInt32(lect["empreinte_numerique"].ToString());
                        bean.Flag = Convert.ToInt32(lect["flag"].ToString());
                        bean.Longueur = Convert.ToInt32(lect["longueur"].ToString());
                        bean.Employe = EmployeDAO._getOneById(Convert.ToInt32(lect["employe"].ToString()));
                        bean.Template = Convert.FromBase64String(lect["template"].ToString());
                        bean.STemplate = lect["template"].ToString(); 
                    }
                }
                return bean;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }

        public static Empreinte _getOneByEmployeFinger(long employe, int finger, NpgsqlConnection connect)
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
                        bean.Id = Convert.ToInt32(lect["id"].ToString());
                        bean.Digital = Convert.ToInt32(lect["empreinte_digital"].ToString());
                        bean.Facial = Convert.ToInt32(lect["empreinte_faciale"].ToString());
                        bean.Numerique = Convert.ToInt32(lect["empreinte_numerique"].ToString());
                        bean.Flag = Convert.ToInt32(lect["flag"].ToString());
                        bean.Longueur = Convert.ToInt32(lect["longueur"].ToString());
                        bean.Employe = EmployeDAO._getOneById(Convert.ToInt32(lect["employe"].ToString()));
                        bean.Template = Convert.FromBase64String(lect["template"].ToString());
                        bean.STemplate = lect["template"].ToString();
                    }
                }
                return bean;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }

        public static List<Empreinte> _getList(string query)
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
                        int id = Convert.ToInt32(lect["id"].ToString());
                        list.Add(_getOneById(id));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }

        public static bool _getInsert(Empreinte bean)
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
                throw new Exception(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }

        public static bool _getInsert(Empreinte bean, NpgsqlConnection connect)
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
                throw new Exception(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }

        public static bool _getUpdate(Empreinte bean,long id)
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
                throw new Exception(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }

        public static bool _getDelete(long id)
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
                throw new Exception(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }
    }
}
