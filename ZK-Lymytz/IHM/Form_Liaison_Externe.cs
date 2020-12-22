using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using ZK_Lymytz.BLL;
using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;

namespace ZK_Lymytz.IHM
{
    public partial class Form_Liaison_Externe : Form
    {
        Npgsql.NpgsqlConnection connect;
        List<string> employes = new List<string>();
        List<string> tranches = new List<string>();

        DataGridViewComboBoxEditingControl cbox_externe = null;


        public Form_Liaison_Externe(Npgsql.NpgsqlConnection connect)
        {
            InitializeComponent();
            Configuration.Load(this);
            this.connect = connect;
        }

        private void Form_Liaison_Externe_Load(object sender, EventArgs e)
        {
            try
            {
                //Chargement des employes du serveur
                string query = "SELECT CONCAT(id, ', ', CONCAT(nom, ' ', prenom)) AS Value FROM yvs_grh_employes WHERE agence = " + Constantes.AGENCE.Id + " ORDER BY nom, prenom";
                employes = Bll.LoadListObject(query, Constantes.SOCIETE.AdresseIp);
                employes.Insert(0, "0,---");

                //Chargement des employes du serveur
                query = "SELECT CONCAT(id, ', ', titre) AS Value FROM yvs_grh_tranche_horaire WHERE societe = " + Constantes.SOCIETE.Id + " ORDER BY type_journee, heure_debut";
                tranches = Bll.LoadListObject(query, Constantes.SOCIETE.AdresseIp);
                tranches.Insert(0, "0,---");
            }
            catch (Exception ex)
            {
                Messages.Exception(ex);
            }
        }

        private void cbox_table_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindingSource bs = new BindingSource();
                ObjectThread data_data_table = new ObjectThread(dgv_data_table);
                if (connect == null)
                {
                    Utils.WriteLog("Veillez reselectionner la liaison");
                }
                if (connect.State == System.Data.ConnectionState.Closed)
                {
                    connect.Open();
                }
                data_data_table.ClearDataGridView(true);
                if (cbox_table.SelectedItem.Equals("users"))
                {
                    //Chargement des utilisateurs de liaison
                    bs.DataSource = employes;
                    externe.DataSource = bs;
                    new Thread(delegate()
                    {
                        Npgsql.NpgsqlCommand cmd = null;
                        Npgsql.NpgsqlDataReader lect = null;
                        try
                        {
                            string query = "SELECT coderep, CONCAT(nom, ' ', prenom), externe FROM users ORDER BY coderep";
                            cmd = new Npgsql.NpgsqlCommand(query, connect);
                            lect = cmd.ExecuteReader();
                            if (lect.HasRows)
                            {
                                int i = 0;
                                while (lect.Read())
                                {
                                    var _externe = lect[2];
                                    int index = _externe != null ? employes.FindIndex(x => x.Split(',')[0].Equals(_externe.ToString())) : -1;
                                    data_data_table.WriteDataGridView(new object[] { lect[0].ToString(), lect[1].ToString(), null });
                                    if (index > -1 ? employes.Count > index ? dgv_data_table.Rows.Count > i : false : false)
                                    {
                                        ObjectThread cell = new ObjectThread(dgv_data_table.Rows[i].Cells[2] as DataGridViewComboBoxCell);
                                        cell.ValueComboBoxCell(employes[index]);
                                    }
                                    i++;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Messages.Exception(ex);
                        }
                        finally
                        {
                            if (cmd != null)
                                cmd.Dispose();
                            if (lect != null)
                                lect.Dispose();
                        }
                    }).Start();
                }
                else if (cbox_table.SelectedItem.Equals("tranchehoraire"))
                {
                    bs.DataSource = tranches;
                    //Chargement des utilisateurs de liaison
                    externe.DataSource = bs;
                    new Thread(delegate()
                    {
                        Npgsql.NpgsqlCommand cmd = null;
                        Npgsql.NpgsqlDataReader lect = null;
                        try
                        {
                            string query = "SELECT id, CONCAT(typedejrnee, ' ', CONCAT(heure_debut, '-', heure_fin)), externe FROM tranchehoraire ORDER BY typedejrnee, heure_debut";
                            cmd = new Npgsql.NpgsqlCommand(query, connect);
                            lect = cmd.ExecuteReader();
                            if (lect.HasRows)
                            {
                                int i = 0;
                                while (lect.Read())
                                {
                                    var _externe = lect[2];
                                    int index = _externe != null ? tranches.FindIndex(x => x.Split(',')[0].Equals(_externe.ToString())) : -1;
                                    data_data_table.WriteDataGridView(new object[] { lect[0].ToString(), lect[1].ToString(), null });
                                    if (index > -1 ? tranches.Count > index ? dgv_data_table.Rows.Count > i : false : false)
                                    {
                                        var var = dgv_data_table.Rows[i].Cells[2];
                                        if (var is DataGridViewComboBoxCell)
                                        {
                                            ObjectThread cell = new ObjectThread(var as DataGridViewComboBoxCell);
                                            cell.ValueComboBoxCell(tranches[index]);
                                        }
                                    }
                                    i++;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Messages.Exception(ex);
                        }
                        finally
                        {
                            if (cmd != null)
                                cmd.Dispose();
                            if (lect != null)
                                lect.Dispose();
                        }
                    }).Start();
                }
            }
            catch (Exception ex)
            {
                Messages.Exception(ex);
            }
        }

        private void dgv_data_table_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (e.Control is DataGridViewComboBoxEditingControl)
                {
                    cbox_externe = e.Control as DataGridViewComboBoxEditingControl;
                    cbox_externe.SelectedIndexChanged -= externe_SelectedIndexChanged;
                    cbox_externe.SelectedIndexChanged += externe_SelectedIndexChanged;
                }
            }
            catch (Exception ex)
            {
                Messages.Exception(ex);
            }
        }

        private void externe_SelectedIndexChanged(object sender, EventArgs e)
        {
            Npgsql.NpgsqlCommand cmd = null;
            try
            {
                if (cbox_externe != null)
                {
                    string item = cbox_externe.SelectedItem != null ? cbox_externe.SelectedItem.ToString() : "0,---";
                    int id = Convert.ToInt32(item.Split(',')[0]);
                    var row = cbox_externe.EditingControlRowIndex;
                    if (row > -1)
                    {
                        string code = dgv_data_table.Rows[row].Cells[0].Value.ToString();
                        if (Utils.asString(code))
                        {
                            if (cbox_table.SelectedItem.Equals("users"))
                            {
                                string query = "UPDATE users SET externe = " + (id > 0 ? id.ToString() : "null") + " WHERE coderep = '" + code + "'";
                                cmd = new Npgsql.NpgsqlCommand(query, connect);
                                int reponse = cmd.ExecuteNonQuery();
                                if (reponse == 1)
                                {
                                    if (id > 0)
                                        Utils.WriteLog("ID externe " + id + " défini sur l'utilisateur " + code);
                                    else
                                        Utils.WriteLog("ID externe rétiré sur l'utilisateur " + code);
                                }
                            }
                            else if (cbox_table.SelectedItem.Equals("tranchehoraire"))
                            {
                                string query = "UPDATE tranchehoraire SET externe = " + (id > 0 ? id.ToString() : "null") + " WHERE id = " + code + "";
                                cmd = new Npgsql.NpgsqlCommand(query, connect);
                                int reponse = cmd.ExecuteNonQuery();
                                if (reponse == 1)
                                {
                                    if (id > 0)
                                        Utils.WriteLog("ID externe " + id + " défini sur la tranche horaire " + code);
                                    else
                                        Utils.WriteLog("ID externe rétiré sur la tranche horaire " + code);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Messages.Exception(ex);
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
            }
        }

        private void dgv_data_table_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Error happened " + e.Context.ToString());

            if (e.Context == DataGridViewDataErrorContexts.Commit)
            {
                MessageBox.Show("Commit error");
            }
            if (e.Context == DataGridViewDataErrorContexts.CurrentCellChange)
            {
                MessageBox.Show("Cell change");
            }
            if (e.Context == DataGridViewDataErrorContexts.Parsing)
            {
                MessageBox.Show("parsing error");
            }
            if (e.Context == DataGridViewDataErrorContexts.LeaveControl)
            {
                MessageBox.Show("leave control error");
            }

            if ((e.Exception) is ConstraintException)
            {
                DataGridView view = (DataGridView)sender;
                view.Rows[e.RowIndex].ErrorText = "an error";
                view.Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = "an error";
                e.ThrowException = false;
            }
        }
    }
}
