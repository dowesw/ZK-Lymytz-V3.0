﻿using System;
using System.Collections.Generic;
using System.Text;

using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;
using ZK_Lymytz.DAO;

namespace ZK_Lymytz.BLL
{
    class TrancheHoraireBLL
    {
        public static TrancheHoraire OneById(int id)
        {
            try
            {
                return TrancheHoraireDAO.getOneById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<TrancheHoraire> List(string query)
        {
            return List(query, null);
        }

        public static List<TrancheHoraire> List(string query, string adresse)
        {
            try
            {
                return TrancheHoraireDAO.getList(query, adresse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
