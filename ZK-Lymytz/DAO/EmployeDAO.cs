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
        private static Employe Return(NpgsqlDataReader lect, bool full)
        {
            Employe bean = new Employe();
            bean.Id = Convert.ToInt32(lect["id"].ToString());
            bean.Nom = lect["nom"].ToString();
            bean.Prenom = lect["prenom"].ToString();
            bean.Matricule = lect["matricule"].ToString();
            bean.Photo = lect["photos"].ToString();
            bean.HoraireDynamique = (Boolean)((lect["horaire_dynamique"] != null) ? (!lect["horaire_dynamique"].ToString().Trim().Equals("") ? lect["horaire_dynamique"] : false) : false);
            bean.Agence = new Agence(Convert.ToInt32(lect["agence"].ToString()));
            string q = "select * from yvs_grh_contrat_emps where employe = " + bean.Id + " and actif = true and contrat_principal = true limit 1";
            List<Contrat> lc = ContratDAO.getList(q, full);
            if (lc.Count > 0)
            {
                bean.Contrat = lc[0];
            }
            if (full)
            {
                q = "select * from yvs_grh_poste_employes where employe = " + bean.Id + " and actif = true and valider = true limit 1";
                List<PosteTravail> lp = PosteTravailDAO.getList(q, full);
                if (lp.Count > 0)
                {
                    bean.Poste = lp[0];
                }
                bean.Agence = AgenceDAO.getOneById(Convert.ToInt32(lect["agence"].ToString()));
            }
            return bean;
        }

        public static Employe getOneById(int id, bool full, string adresse)
        {
            Employe bean = new Employe();
            NpgsqlConnection connect = new Connexion().Connection(adresse);
            try
            {
                string query = "select * from yvs_grh_employes where id =" + id + ";";
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    while (lect.Read())
                    {
                        bean = Return(lect, full);
                        bean.Adresse = adresse;
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
                Connexion.Close(connect);
            }
        }
        
        public static Employe getOneById(int id, bool full, int societe)
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
                        bean = Return(lect, full);
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
                Connexion.Close(connect);
            }
        }

        public static Employe getOneByNom(string nom, string prenom, bool full, int societe)
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
                        bean = Return(lect, full);
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
                Connexion.Close(connect);
            }
        }

        public static Employe getOneByNom(string nom_prenom, bool full, int societe)
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
                        bean = Return(lect, full);
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
                Connexion.Close(connect);
            }
        }

        public static List<Employe> getList(string query, bool full)
        {
            List<Employe> list = new List<Employe>();
            NpgsqlConnection connect = new Connexion().Connection();
            try
            {
                NpgsqlCommand Lcmd = new NpgsqlCommand(query, connect);
                NpgsqlDataReader lect = Lcmd.ExecuteReader();
                if (lect.HasRows)
                {
                    List<string> noms = new List<string>();
                    while (lect.Read())
                    {
                        Employe e = Return(lect, full);
                        string nom = e.NomPrenom;
                        if (noms.Contains(nom))
                            e.Prenom += "°";
                        noms.Add(nom);
                        list.Add(e);
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
                Connexion.Close(connect);
            }
        }
    }
}
