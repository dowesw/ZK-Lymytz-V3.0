using System;
using System.Collections.Generic;
using System.Text;

using ZK_Lymytz.TOOLS;
using ZK_Lymytz.ENTITE;

using NpgsqlTypes;
using Npgsql;

namespace ZK_Lymytz.DAO
{
    class IOEMDeviceDAO
    {
        private static IOEMDevice Return(NpgsqlDataReader lect)
        {
            IOEMDevice bean = new IOEMDevice();
            try
            {
                bean.id = Convert.ToInt32(lect["id"].ToString());
                bean.iMachineNumber = Convert.ToInt32(lect["machine"].ToString());
                bean.idwSEnrollNumber = Convert.ToInt32(lect["employe"].ToString());
                bean.idwVerifyMode = Convert.ToInt32(lect["verify_mode"].ToString());
                bean.idwInOutMode = Convert.ToInt32(lect["in_out_mode"].ToString());
                bean.idwWorkCode = Convert.ToInt32(lect["work_code"].ToString());
                bean.idwReserved = Convert.ToInt32(lect["reserved"].ToString());
                bean.pointeuse = new Pointeuse(Convert.ToInt32(lect["pointeuse"].ToString()));
                bean.date_action = (DateTime)((lect["date_action"] != null) ? (!lect["date_action"].ToString().Trim().Equals("") ? lect["date_action"] : DateTime.Now) : DateTime.Now);
                bean.time_action = (DateTime)((lect["time_action"] != null) ? (!lect["time_action"].ToString().Trim().Equals("") ? lect["time_action"] : DateTime.Now) : DateTime.Now);
                bean.date_time_action = (DateTime)((lect["date_time_action"] != null) ? (!lect["date_time_action"].ToString().Trim().Equals("") ? lect["date_time_action"] : DateTime.Now) : DateTime.Now);
                bean.idwYear = Convert.ToInt32(bean.date_action.ToString("yyyy"));
                bean.idwMonth = Convert.ToInt32(bean.date_action.ToString("MM"));
                bean.idwDay = Convert.ToInt32(bean.date_action.ToString("dd"));
                bean.idwHour = Convert.ToInt32(bean.time_action.ToString("HH"));
                bean.idwMinute = Convert.ToInt32(bean.time_action.ToString("mm"));
                bean.idwSecond = Convert.ToInt32(bean.time_action.ToString("ss"));
            }
            catch (Exception ex)
            {
                Messages.Exception("IOEMDeviceDAO (Return)", ex);
            }
            return bean;
        }

        public static bool getInsert(IOEMDevice bean, string adresse)
        {
            NpgsqlConnection connect = new Connexion().Connection(adresse);
            try
            {
                string query = "INSERT INTO yvs_grh_ioem_device(machine, employe, verify_mode, in_out_mode, work_code, reserved, date_action, time_action, date_time_action, pointeuse, author) " +
                    " VALUES (" + bean.iMachineNumber + "," + bean.idwSEnrollNumber + ", " + bean.idwVerifyMode + ", " + bean.idwInOutMode + ", " + bean.idwWorkCode + ", " + bean.idwReserved + ", '" + bean.date_action + "', '" + bean.time_action.ToString("HH:mm:ss") + "', '" + bean.date_time_action + "', " + bean.pointeuse.Id + ", " + (Constantes.USERS.Author > 0 ? Constantes.USERS.Author.ToString() : "null") + ")";
                NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("IOEMDeviceDAO (getInsert)", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static bool getDelete(Pointeuse pointeuse, string adresse)
        {
            NpgsqlConnection connect = new Connexion().Connection(adresse);
            try
            {
                string query = "DELETE FROM yvs_grh_ioem_device WHERE pointeuse = " + pointeuse.Id;
                NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("IOEMDeviceDAO (getDelete)", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static bool getDelete(Pointeuse pointeuse, Employe employe, DateTime debut, DateTime fin, string adresse)
        {
            NpgsqlConnection connect = new Connexion().Connection(adresse);
            try
            {
                string query = "DELETE FROM yvs_grh_ioem_device WHERE date_time_action BETWEEN '" + debut + "' AND '" + fin + "'";
                if (employe != null ? employe.Id > 0 : false)
                {
                    query += "AND employe =" + employe.Id;
                }
                query += "AND pointeuse =" + pointeuse.Id;
                NpgsqlCommand cmd = new NpgsqlCommand(query, connect);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("IOEMDeviceDAO (getDelete)", ex);
                return false;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static IOEMDevice getOne(Pointeuse pointeuse, Employe employe, DateTime date, string adresse)
        {
            IOEMDevice result = new IOEMDevice();
            NpgsqlConnection connect = new Connexion().Connection(adresse);
            try
            {
                string query = "SELECT * FROM yvs_grh_ioem_device WHERE date_time_action = '" + date + "' AND employe =" + employe.Id + " AND pointeuse =" + pointeuse.Id;
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        result = Return(lect);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("IOEMDeviceDAO (getOne)", ex);
                return result;
            }
            finally
            {
                Connexion.Close(connect);
            }
            return result;
        }

        public static List<IOEMDevice> getListByEmploye(Pointeuse pointeuse, Employe employe, string adresse)
        {
            List<IOEMDevice> result = new List<IOEMDevice>();
            NpgsqlConnection connect = new Connexion().Connection(adresse);
            try
            {
                string query = "SELECT * FROM yvs_grh_ioem_device WHERE pointeuse = " + pointeuse.Id;
                if (employe != null ? employe.Id > 0 : false)
                {
                    query += "AND employe =" + employe.Id;
                }
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        result.Add(Return(lect));
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Messages.Exception("IOEMDeviceDAO (getListByEmployeDates) ", ex);
                return result;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static List<IOEMDevice> getListByEmployeDates(Pointeuse pointeuse, Employe employe, DateTime debut, DateTime fin, string adresse)
        {
            List<IOEMDevice> result = new List<IOEMDevice>();
            NpgsqlConnection connect = new Connexion().Connection(adresse);
            try
            {
                bool addTime = !debut.ToString("HH:mm:ss").Equals("00:00:00");
                string query = "SELECT * FROM yvs_grh_ioem_device WHERE " + (addTime ? "date_time_action" : "date_action") + " BETWEEN '" + debut + "' AND '" + fin + "'";
                if (employe != null ? employe.Id > 0 : false)
                {
                    query += "AND employe =" + employe.Id;
                }
                query += "AND pointeuse =" + pointeuse.Id;
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        result.Add(Return(lect));
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Messages.Exception("IOEMDeviceDAO (getListByEmployeDates) ", ex);
                return result;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }

        public static List<IOEMDevice> getListNoByEmployesDates(Pointeuse pointeuse, List<Employe> employes, DateTime[] dates, string adresse)
        {
            List<IOEMDevice> result = new List<IOEMDevice>();
            NpgsqlConnection connect = new Connexion().Connection(adresse);
            try
            {
                string employes_string = "(0";
                foreach (Employe e in employes)
                {
                    employes_string += "," + e.Id;
                }
                employes_string += ")";

                string dates_string = "('01-01-2000'";
                foreach (DateTime d in dates)
                {
                    employes_string += ",'" + d + "'";
                }
                dates_string += ")";

                string query = "SELECT * FROM yvs_grh_ioem_device WHERE employe NOT IN " + employes_string + " AND date_action NOT IN " + dates_string + " AND pointeuse =" + pointeuse.Id;
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        result.Add(Return(lect));
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                Messages.Exception("IOEMDeviceDAO (getListByEmployeDates) ", ex);
                return result;
            }
            finally
            {
                Connexion.Close(connect);
            }
        }
    }
}
