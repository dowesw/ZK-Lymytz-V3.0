using System;
using System.Collections.Generic;
using System.Text;
using ZK_LymytzService.DAO;
using ZK_LymytzService.ENTITE;

namespace ZK_LymytzService.BLL
{
    public class SettingBLL
    {
        public static bool CreateSetting(Setting config)
        {
            try
            {
                return SettingDAO._getCreateSetting(config);
            }
            catch (Exception ex)
            {
                throw new Exception("Echec de Création de fichier", ex);
            }
        }

        public static Setting ReturnSetting()
        {
            try
            {
                return SettingDAO._getReturnSetting();
            }
            catch (Exception ex)
            {
                throw new Exception("Echec de Création de fichier", ex);
            }

        }
    }
}
