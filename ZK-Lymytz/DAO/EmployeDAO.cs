using System;
using System.Collections.Generic;
using System.Text;

using ZK_Lymytz.TOOLS;
using ZK_Lymytz.ENTITE;

using NpgsqlTypes;
using Npgsql;

namespace ZK_Lymytz.DAO
{
    class EmployeDAO
    {
        public static Employe getOneById(int id)
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
                        bean.Matricule = lect["matricule"].ToString();
                        bean.Photo = lect["photos"].ToString();
                        bean.HoraireDynamique = (Boolean)((lect["horaire_dynamique"] != null) ? (!lect["horaire_dynamique"].ToString().Trim().Equals("") ? lect["horaire_dynamique"] : false) : false);
                        string q = "select * from yvs_grh_contrat_emps where employe = " + bean.Id + " and actif = true and contrat_principal = true limit 1";
                        List<Contrat> lc = ContratDAO.getList(q);
                        if (lc.Count > 0)
                        {
                            bean.Contrat = lc[0];
                        }
                        q = "select * from yvs_grh_poste_employes where employe = " + bean.Id + " and actif = true and valider = true limit 1";
                        List<PosteTravail> lp = PosteTravailDAO.getList(q);
                        if (lp.Count > 0)
                        {
                            bean.Poste = lp[0];
                        }
                        if ((lect["agence"] != null) ? lect["agence"].ToString() != "" : false)
                        {
                            bean.Agence = AgenceDAO.getOneById(Convert.ToInt32(lect["agence"].ToString()));
                        }
                    }
                }
                return bean;
            }
            catch (Exception ex)
            {
                Messages.Exception("EmployeDao (getOneById) ", ex);
                return bean;
            }
            finally
            {
                connect.Close();
            }
        }
        public static Employe getOneById(int id, int societe)
        {
            Employe bean = new Employe();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select e.* from yvs_grh_employes e inner join yvs_agences a on e.agence = a.id where e.id = " + id + " and a.societe = " + societe;
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        bean = getOneById(Convert.ToInt32(lect["id"].ToString()));
                    }
                }
                return bean;
            }
            catch (Exception ex)
            {
                Messages.Exception("EmployeDao (getOneById) ", ex);
                return bean;
            }
            finally
            {
                connect.Close();
            }
        }

        public static Employe getOneByNom(string nom, string prenom, int societe)
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
                        bean = getOneById(Convert.ToInt32(lect["id"].ToString()));
                    }
                }
                return bean;
            }
            catch (Exception ex)
            {
                Messages.Exception("EmployeDao (getOneByNom) ", ex);
                return bean;
            }
            finally
            {
                connect.Close();
            }
        }

        public static Employe getOneByNom(string nom_prenom, int societe)
        {
            Employe bean = new Employe();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                string query = "select e.* from yvs_grh_employes e inner join yvs_agences a on e.agence = a.id where concat(nom, ' ',prenom) = '" + nom_prenom + "' and a.societe = " + societe;
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        bean = getOneById(Convert.ToInt32(lect["id"].ToString()));
                    }
                }
                return bean;
            }
            catch (Exception ex)
            {
                Messages.Exception("EmployeDao (getOneByNom) ", ex);
                return bean;
            }
            finally
            {
                connect.Close();
            }
        }

        public static List<Employe> getList(string query)
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
                        list.Add(getOneById(id));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                Messages.Exception("EmployeDao (getList) ", ex);
                return list;
            }
            finally
            {
                connect.Close();
            }
        }
    }
}
