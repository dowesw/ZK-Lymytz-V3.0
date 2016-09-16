﻿using System;
using System.Collections.Generic;
using System.Text;
using ZK_Lymytz.DAO;
using ZK_Lymytz.ENTITE;

namespace ZK_Lymytz.BLL
{
    class PosteTravailBLL
    {

        public static PosteTravail OneById(int id)
        {
            try
            {
                return PosteTravailDAO.getOneById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<PosteTravail> List(string query)
        {
            try
            {
                return PosteTravailDAO.getList(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
