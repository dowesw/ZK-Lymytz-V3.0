using System;
using System.Collections.Generic;
using System.Text;
using ZK_Lymytz.DAO;
using ZK_Lymytz.ENTITE;

namespace ZK_Lymytz.BLL
{
    class SettingBLL
    {
        public static bool CreateSetting(Setting config)
        {
            try
            {
                return SettingDAO.getCreateSetting(config);
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
                return SettingDAO.getReturnSetting();
            }
            catch (Exception ex)
            {
                throw new Exception("Echec de Création de fichier", ex);
            }

        }
    }
}
