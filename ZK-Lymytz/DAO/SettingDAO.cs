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
        static string chemin = Chemins.CheminParametre();

        public static bool CreateSetting(Setting serveur)
        {
            try
            {
                using (RegistryKey Nkey = Registry.LocalMachine)
                {
                    CreateSetting(serveur, Nkey);
                }
                if (Utils.Is64BitOperatingSystem())
                {
                    using (RegistryKey Nkey = Registry.CurrentUser)
                    {
                        CreateSetting(serveur, Nkey);
                    }
                }
            }
            catch (Exception e)
            {
                Messages.Exception("SettingDAO (getCreateSetting)", e);
                return false;
            }
            return true;
        }
        public static bool CreateSetting(Setting serveur, RegistryKey Nkey)
        {
            try
            {
                RegistryKey valKey = Nkey.OpenSubKey(@chemin, true);
                if (valKey == null)
                {
                    Nkey.CreateSubKey(@chemin);
                    valKey = Nkey.OpenSubKey(@chemin, true);
                }
                valKey.SetValue("vide", serveur.Vide);
                valKey.SetValue("autorun", serveur.Autorun);
                valKey.SetValue("autosynchro", serveur.AutoSynchro);
                valKey.SetValue("autoclearbackup", serveur.AutoClearAndBackup);
                valKey.SetValue("autorattach", serveur.AutoRattach);
                valKey.SetValue("usefiletamponlog", serveur.UseFileTamponLog);
                valKey.SetValue("addenrollauto", serveur.AddEnrollAuto);
                valKey.SetValue("autobackupdevice", serveur.AutoBackupDevice);
                valKey.SetValue("checkconnect", serveur.CheckConnect);
                valKey.SetValue("autocheckconnectandsynchro", serveur.AutoCheckConnectAndSynchro);
                valKey.SetValue("timesynchroauto", serveur.AutoCheckConnectAndSynchro ? serveur.TimeSynchroAuto : new DateTime(0001, 01, 01, 0, 0, 0));
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

        public static Setting ReturnSetting()
        {
            RegistryKey Nkey = Registry.LocalMachine;
            try
            {
                Setting serveur = ReturnSetting(Nkey);
                if (serveur != null ? serveur.Vide == null : true)
                {
                    Nkey = Registry.CurrentUser;
                    serveur = ReturnSetting(Nkey);
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

        public static Setting ReturnSetting(RegistryKey Nkey)
        {
            try
            {
                Setting serveur = new Setting();
                RegistryKey valKey = Nkey.OpenSubKey(@chemin, true);
                if (valKey == null)
                {
                    serveur.Vide = false;
                    serveur.Autorun = false;
                    serveur.AutoSynchro = false;
                    serveur.AutoClearAndBackup = false;
                    serveur.AutoRattach = false;
                    serveur.AddEnrollAuto = false;
                    serveur.AutoBackupDevice = false;
                    serveur.UseFileTamponLog = false;
                    serveur.CheckConnect = false;
                    serveur.AutoCheckConnectAndSynchro = false;
                    serveur.TimeSynchroAuto = new DateTime(0001, 01, 01, 0, 0, 0);
                    serveur.CheminPersonal = Chemins.cheminDefault;
                    serveur.CheminStartup = Chemins.cheminStartup;
                    serveur.CheminPhoto = "";
                    CreateSetting(serveur);
                }
                else
                {
                    serveur.Vide = Convert.ToBoolean(valKey.GetValue("vide") != null ? valKey.GetValue("vide") : null);
                    serveur.AutoRattach = Convert.ToBoolean(valKey.GetValue("autorattach") != null ? valKey.GetValue("autorattach") : false);
                    serveur.UseFileTamponLog = Convert.ToBoolean(valKey.GetValue("usefiletamponlog") != null ? valKey.GetValue("usefiletamponlog") : false);
                    serveur.Autorun = Convert.ToBoolean(valKey.GetValue("autorun") != null ? valKey.GetValue("autorun") : false);
                    serveur.AutoSynchro = Convert.ToBoolean(valKey.GetValue("autosynchro") != null ? valKey.GetValue("autosynchro") : false);
                    serveur.AutoClearAndBackup = Convert.ToBoolean(valKey.GetValue("autoclearbackup") != null ? valKey.GetValue("autoclearbackup") : false);
                    serveur.AddEnrollAuto = Convert.ToBoolean(valKey.GetValue("addenrollauto") != null ? valKey.GetValue("addenrollauto") : false);
                    serveur.AutoBackupDevice = Convert.ToBoolean(valKey.GetValue("autobackupdevice") != null ? valKey.GetValue("autobackupdevice") : false);
                    serveur.CheckConnect = Convert.ToBoolean(valKey.GetValue("checkconnect") != null ? valKey.GetValue("checkconnect") : false);
                    serveur.AutoCheckConnectAndSynchro = Convert.ToBoolean(valKey.GetValue("autocheckconnectandsynchro") != null ? valKey.GetValue("autocheckconnectandsynchro") : false);
                    serveur.TimeSynchroAuto = Convert.ToDateTime(valKey.GetValue("timesynchroauto") != null ? valKey.GetValue("timesynchroauto") : new DateTime(0001, 01, 01, 0, 0, 0));
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
