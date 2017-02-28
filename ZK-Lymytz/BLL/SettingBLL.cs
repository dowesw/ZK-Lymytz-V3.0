using System;
using System.Collections.Generic;
using System.Text;
using ZK_Lymytz.DAO;
using ZK_Lymytz.ENTITE;

namespace ZK_Lymytz.BLL
{
    public class SettingBLL
    {
        public static bool CreateSetting(Setting config)
        {
            try
            {
                return SettingDAO.CreateSetting(config);
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
                return SettingDAO.ReturnSetting();
            }
            catch (Exception ex)
            {
                throw new Exception("Echec de Création de fichier", ex);
            }

        }
    }
}
