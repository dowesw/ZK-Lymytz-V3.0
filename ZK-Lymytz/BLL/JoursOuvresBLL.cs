using System;
using System.Collections.Generic;
using System.Text;

using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;
using ZK_Lymytz.DAO;

namespace ZK_Lymytz.BLL
{
    class JoursOuvresBLL
    {
        public static JoursOuvres OneById(int id)
        {
            try
            {
                return JoursOuvresDAO.getOneById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static JoursOuvres OneByCalendrier(Calendrier calendrier, string jour, string adresse)
        {
            try
            {
                JoursOuvres y;
                int index = Constantes.JOURSOUVRES.FindIndex(x => x.Jour == jour && x.Calendrier == (calendrier != null ? calendrier.Id : 0));
                if (index > -1)
                {
                    y = Constantes.JOURSOUVRES[index];
                }
                else
                {
                    y = JoursOuvresDAO.getOneByCalendier(calendrier, jour, adresse);
                    Constantes.JOURSOUVRES.Add(y);
                }
                return y;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<JoursOuvres> List(Calendrier calendrier)
        {
            try
            {
                return JoursOuvresDAO.getByCalendier(calendrier);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<JoursOuvres> List(string query)
        {
            try
            {
                return JoursOuvresDAO.getList(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
