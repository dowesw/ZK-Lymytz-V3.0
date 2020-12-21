using System;
using System.Collections.Generic;
using System.Text;

using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;
using ZK_Lymytz.DAO;

namespace ZK_Lymytz.BLL
{
    public class EmployeBLL
    {
        public static Employe OneById(int id)
        {
            return OneById(id, null);
        }

        public static Employe OneById(int id, bool full)
        {
            return OneById(id, full, null);
        }

        public static Employe OneById(int id, string adresse)
        {
            return OneById(id, false, adresse);
        }

        public static Employe OneById(int id, bool full, string adresse)
        {
            try
            {
                Employe employe = Constantes.EMPLOYES.Find(x => x.Id == id && (Utils.asString(x.Adresse) && Utils.asString(adresse) ? x.Adresse == adresse : true));
                if (employe != null ? employe.Id < 1 : true)
                {
                    employe = EmployeDAO.getOneById(id, full, adresse);
                    if (employe != null ? employe.Id > 0 : false)
                    {
                        Constantes.EMPLOYES.Add(employe);
                    }
                }
                return employe;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Employe OneById(int id, int societe)
        {
            return OneById(id, true, societe);
        }

        public static Employe OneById(int id, bool full, int societe)
        {
            try
            {
                return EmployeDAO.getOneById(id, full, societe);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Employe OneByNom(string nom, string prenom, int societe)
        {
            return OneByNom(nom, prenom, true, societe);
        }

        public static Employe OneByNom(string nom, string prenom, bool full, int societe)
        {
            try
            {
                return EmployeDAO.getOneByNom(nom, prenom, full, societe);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Employe OneByNom(string nom_prenom, int societe)
        {
            return OneByNom(nom_prenom, true, societe);
        }

        public static Employe OneByNom(string nom_prenom, bool full, int societe)
        {
            try
            {
                return EmployeDAO.getOneByNom(nom_prenom, full, societe);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Employe> List(string query)
        {
            return List(query, true);
        }

        public static List<Employe> List(string query, bool full)
        {
            try
            {
                return EmployeDAO.getList(query, full);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
