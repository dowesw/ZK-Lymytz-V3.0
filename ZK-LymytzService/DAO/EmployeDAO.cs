using System;
using System.Collections.Generic;
using System.Text;

using ZK_LymytzService.TOOLS;
using ZK_LymytzService.ENTITE;

using NpgsqlTypes;
using Npgsql;

namespace ZK_LymytzService.DAO
{
    public class EmployeDAO
    {
        public static Employe _getOneById(int id)
        {
            Employe bean = new Employe();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select * from yvs_grh_employes where id =" + id + ";";
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        bean.Id = Convert.ToInt32(lect["id"].ToString());
                        bean.Nom = lect["nom"].ToString();
                        bean.Prenom = lect["prenom"].ToString();
                        bean.HoraireDynamique = (Boolean)((lect["horaire_dynamique"] != null) ? (!lect["horaire_dynamique"].ToString().Trim().Equals("") ? lect["horaire_dynamique"] : false) : false);
                        string l = "select * from yvs_grh_contrat_emps where employe = " + bean.Id + " and actif = true and contrat_principal = true";
                        List<Contrat> list = ContratDAO._getList(l);
                        if (list.Count > 0)
                        {
                            bean.Contrat = list[0];
                        }
                    }
                }
                return bean;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }

        public static Employe _getOneByNom(string nom, string prenom, int societe)
        {
            Employe bean = new Employe();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select e.* from yvs_grh_employes e inner join yvs_agences a on e.agence = a.id where nom = '" + nom + "' and prenom = '" + prenom + "' and a.societe = " + societe;
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        bean.Id = Convert.ToInt32(lect["id"].ToString());
                        bean.Nom = lect["nom"].ToString();
                        bean.Prenom = lect["prenom"].ToString();
                        bean.HoraireDynamique = (Boolean)((lect["horaire_dynamique"] != null) ? (!lect["horaire_dynamique"].ToString().Trim().Equals("") ? lect["horaire_dynamique"] : false) : false);
                        string l = "select * from yvs_grh_contrat_emps where employe = " + bean.Id + " and actif = true and contrat_principal = true";
                        List<Contrat> list = ContratDAO._getList(l);
                        if (list.Count > 0)
                        {
                            bean.Contrat = list[0];
                        }
                    }
                }
                return bean;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }

        public static List<Employe> _getList(string query)
        {
            List<Employe> list = new List<Employe>();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        int id = Convert.ToInt32(lect["id"].ToString());
                        list.Add(_getOneById(id));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }
    }
}
