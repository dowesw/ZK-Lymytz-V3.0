using System;
using System.Collections.Generic;
using System.Text;

using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;
using ZK_Lymytz.DAO;

namespace ZK_Lymytz.BLL
{
    class CalendrierBLL
    {

        public static Calendrier Default(Societe societe)
        {
            try
            {
                if (Constantes.CALENDRIER != null ? Constantes.CALENDRIER.Id < 1 : true)
                {
                    Constantes.CALENDRIER = CalendrierDAO.getDefault(societe);
                }
                return Constantes.CALENDRIER;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Calendrier OneById(int id)
        {
            try
            {
                return CalendrierDAO.getOneById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Calendrier> List(string query)
        {
            try
            {
                return CalendrierDAO.getList(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
