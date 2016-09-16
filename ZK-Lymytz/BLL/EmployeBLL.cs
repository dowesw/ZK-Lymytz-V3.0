using System;
using System.Collections.Generic;
using System.Text;

using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;
using ZK_Lymytz.DAO;

namespace ZK_Lymytz.BLL
{
    class EmployeBLL
    {
        public static Employe OneById(int id)
        {
            try
            {
                return EmployeDAO.getOneById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Employe OneById(int id, int societe)
        {
            try
            {
                return EmployeDAO.getOneById(id, societe);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Employe OneByNom(string nom, string prenom, int societe)
        {
            try
            {
                return EmployeDAO.getOneByNom(nom, prenom, societe);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Employe OneByNom(string nom_prenom, int societe)
        {
            try
            {
                return EmployeDAO.getOneByNom(nom_prenom, societe);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Employe> List(string query)
        {
            try
            {
                return EmployeDAO.getList(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
