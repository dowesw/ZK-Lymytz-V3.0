using System;
using System.Collections.Generic;
using System.Text;

using ZK_LymytzService.ENTITE;
using ZK_LymytzService.TOOLS;
using ZK_LymytzService.DAO;

namespace ZK_LymytzService.BLL
{
    public class EmployeBLL
    {
        public static Employe OneById(int id)
        {
            try
            {
                return EmployeDAO._getOneById(id);
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
                return EmployeDAO._getOneByNom(nom, prenom, societe);
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
                return EmployeDAO._getList(query);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
