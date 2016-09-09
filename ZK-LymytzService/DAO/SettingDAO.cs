using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using ZK_LymytzService.ENTITE;
using ZK_LymytzService.TOOLS;

using Microsoft.Win32;
using NpgsqlTypes;
using Npgsql;

namespace ZK_LymytzService.DAO
{
    public class SettingDAO
    {
        static string chemin = Chemins.getCheminParametre();

        public static bool _getCreateSetting(Setting serveur)
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
                valKey.SetValue("autorun", serveur.Autorun);
                valKey.SetValue("autosynchro", serveur.AutoSynchro);
                valKey.SetValue("autoclearbackup", serveur.AutoClearAndBackup);
                valKey.SetValue("autorattach", serveur.AutoRattach);
                valKey.SetValue("addenrollauto", serveur.AddEnrollAuto);
                valKey.SetValue("autobackupdevice", serveur.AutoBackupDevice);
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

        public static Setting _getReturnSetting()
        {
            RegistryKey Nkey = Registry.LocalMachine;
            try
            {
                Setting serveur = _getReturnSetting(Nkey);
                if (serveur != null ? (serveur.CheminStartup!=null ? serveur.CheminStartup.Trim().Length < 1 :true) : true)
                {
                    RegistryKey Nkey_ = Registry.CurrentUser;
                    try
                    {
                        serveur = _getReturnSetting(Nkey_);
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

        public static Setting _getReturnSetting(RegistryKey Nkey)
        {
            try
            {
                Setting serveur = new Setting();
                RegistryKey valKey = Nkey.OpenSubKey(@chemin, true);
                if (valKey == null)
                {
                    serveur.Id = 0;
                    serveur.Autorun = false;
                    serveur.AutoSynchro = false;
                    serveur.AutoClearAndBackup = false;
                    serveur.AutoRattach = false;
                    serveur.AddEnrollAuto = false;
                    serveur.AutoBackupDevice = false;
                    _getCreateSetting(serveur);
                }
                else
                {
                    serveur.Id = Convert.ToInt32(valKey.GetValue("id") != null ? valKey.GetValue("id") : 0);
                    serveur.AutoRattach = Convert.ToBoolean(valKey.GetValue("autorattach") != null ? valKey.GetValue("autorattach") : false);
                    serveur.Autorun = Convert.ToBoolean(valKey.GetValue("autorun") != null ? valKey.GetValue("autorun") : false);
                    serveur.AutoSynchro = Convert.ToBoolean(valKey.GetValue("autosynchro") != null ? valKey.GetValue("autosynchro") : false);
                    serveur.AutoClearAndBackup = Convert.ToBoolean(valKey.GetValue("autoclearbackup") != null ? valKey.GetValue("autoclearbackup") : false);
                    serveur.AddEnrollAuto = Convert.ToBoolean(valKey.GetValue("addenrollauto") != null ? valKey.GetValue("addenrollauto") : false);
                    serveur.AutoBackupDevice = Convert.ToBoolean(valKey.GetValue("autobackupdevice") != null ? valKey.GetValue("autobackupdevice") : false);
                    serveur.CheminPersonal = Convert.ToString(valKey.GetValue("pathpersonal") != null ? valKey.GetValue("pathpersonal") : "");
                    serveur.CheminStartup = Convert.ToString(valKey.GetValue("pathstartup") != null ? valKey.GetValue("pathstartup") : "");
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
