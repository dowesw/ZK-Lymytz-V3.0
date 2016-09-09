using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;

using Microsoft.Win32;
using NpgsqlTypes;
using Npgsql;

namespace ZK_Lymytz.DAO
{
    class SettingDAO
    {
        static string chemin = Chemins.getCheminParametre();

        public static bool getCreateSetting(Setting serveur)
        {
            try
            {
                using (RegistryKey Nkey = Registry.CurrentUser)
                {
                    getCreateSetting(serveur, Nkey);
                }
                using (RegistryKey Nkey = Registry.LocalMachine)
                {
                    getCreateSetting(serveur, Nkey);
                }
            }
            catch (Exception e)
            {
                Messages.Exception("SettingDAO (getCreateSetting)", e);
                return false;
            }
            return true;
        }
        public static bool getCreateSetting(Setting serveur, RegistryKey Nkey)
        {
            try
            {
                RegistryKey valKey = Nkey.OpenSubKey(@chemin, true);
                if (valKey == null)
                {
                    Nkey.CreateSubKey(@chemin);
                    valKey = Nkey.OpenSubKey(@chemin, true);
                }
                valKey.SetValue("id", serveur.Id);
                valKey.SetValue("autorun", serveur.Autorun);
                valKey.SetValue("autosynchro", serveur.AutoSynchro);
                valKey.SetValue("autoclearbackup", serveur.AutoClearAndBackup);
                valKey.SetValue("autorattach", serveur.AutoRattach);
                valKey.SetValue("addenrollauto", serveur.AddEnrollAuto);
                valKey.SetValue("autobackupdevice", serveur.AutoBackupDevice);
                valKey.SetValue("checkconnect", serveur.CheckConnect);
                valKey.SetValue("pathpersonal", serveur.CheminPersonal);
                valKey.SetValue("pathstartup", serveur.CheminStartup);
                valKey.SetValue("pathpicture", serveur.CheminPhoto);
            }
            catch (Exception e)
            {
                Messages.Exception("SettingDAO (getCreateSetting)", e);
                return false;
            }
            finally
            {
                Nkey.Close();
            }
            return true;
        }

        public static Setting getReturnSetting()
        {
            RegistryKey Nkey = Registry.CurrentUser;
            try
            {
                Setting serveur = getReturnSetting(Nkey);
                if (serveur != null ? serveur.Id < 1 : true)
                {
                    RegistryKey Nkey_ = Registry.LocalMachine;
                    try
                    {
                        serveur = getReturnSetting(Nkey_);
                    }
                    catch (Exception e)
                    {
                        Messages.Exception("SettingDAO (getReturnSetting)", e);
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
                Messages.Exception("SettingDAO (getReturnSetting)", e);
                return null;
            }
            finally
            {
                Nkey.Close();
            }
        }

        public static Setting getReturnSetting(RegistryKey Nkey)
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
                    serveur.CheckConnect = false;
                    serveur.CheminPersonal = Chemins.cheminDefault;
                    serveur.CheminStartup = Chemins.cheminStartup;
                    serveur.CheminPhoto = "";
                    getCreateSetting(serveur);
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
                    serveur.CheckConnect = Convert.ToBoolean(valKey.GetValue("checkconnect") != null ? valKey.GetValue("checkconnect") : false);
                    serveur.CheminPersonal = Convert.ToString(valKey.GetValue("pathpersonal") != null ? valKey.GetValue("pathpersonal") : "");
                    serveur.CheminStartup = Convert.ToString(valKey.GetValue("pathstartup") != null ? valKey.GetValue("pathstartup") : "");
                    serveur.CheminPhoto = Convert.ToString(valKey.GetValue("pathpicture") != null ? valKey.GetValue("pathpicture") : "");
                    valKey.Close();
                }
                return serveur;
            }
            catch (Exception e)
            {
                Messages.Exception("SettingDAO (getReturnSetting)", e);
                return null;
            }
            finally
            {
                Nkey.Close();
            }
        }
    }
}
