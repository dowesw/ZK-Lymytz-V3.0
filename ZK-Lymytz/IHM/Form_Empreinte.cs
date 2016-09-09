using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using ZK_Lymytz.BLL;
using ZK_Lymytz.IHM;
using ZK_Lymytz.TOOLS;
using ZK_Lymytz.ENTITE;

namespace ZK_Lymytz.IHM
{
    public partial class Form_Empreinte : Form
    {
        Pointeuse currentPointeuse = new Pointeuse();
        List<Pointeuse> destinations = new List<Pointeuse>();
        public List<Empreinte> empreintes = new List<Empreinte>();
        List<Empreinte> le = new List<Empreinte>();
        List<Employe> employes = new List<Employe>();
        Employe employe = new Employe();

        public Form_Empreinte()
        {
            InitializeComponent();
            Configuration.Load(this);
        }

        private void Form_Empreinte_FormClosed(object sender, FormClosedEventArgs e)
        {
            Constantes.FORM_EMPREINTE = null;
            Utils.WriteLog("Fermeture page (Gestion Empreinte)");
            Utils.removeFrom("Form_Empreinte");
        }

        private void Form_Empreinte_Load(object sender, EventArgs e)
        {
            LoadPointeuse();
            LoadEmploye();
            AddCheckBoxHeader();
        }

        private void ResetEmploye()
        {
            com_employe.ResetText();
            employe = new Employe();
        }

        public void ResetDataPointeuse()
        {
            for (int i = 0; i < dgv_pointeuse.Rows.Count; i++)
            {
                dgv_pointeuse.Rows[i].Selected = false;
            }
            currentPointeuse = null;
        }

        public void ResetDataDestination()
        {
            ResetDataDestination_();
            destinations = new List<Pointeuse>();
        }

        public void ResetDataDestination_()
        {
            for (int i = 0; i < dgv_destination.Rows.Count; i++)
            {
                dgv_destination.Rows[i].Selected = false;
            }
        }

        public void ResetDataEmpreinte()
        {
            ResetDataEmpreinte_();
            empreintes = new List<Empreinte>();
        }

        public void ResetDataEmpreinte_()
        {
            for (int i = 0; i < dgv_empreinte.Rows.Count; i++)
            {
                dgv_empreinte.Rows[i].Selected = false;
            }
        }

        public void LoadEmploye()
        {
            try
            {
                employes.Clear();
                string query = "select e.* from yvs_grh_employes e inner join yvs_agences a on e.agence = a.id where a.societe = " + Constantes.SOCIETE.Id + " order by e.nom, e.prenom";
                employes = EmployeBLL.List(query);
                com_employe.DisplayMember = "NomPrenom";
                com_employe.ValueMember = "Id";
                com_employe.DataSource = new BindingSource(employes, null);

                for (int i = 0; i < employes.Count; i++)
                {
                    com_employe.AutoCompleteCustomSource.Add(employes[i].NomPrenom);
                }
                com_employe.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                com_employe.AutoCompleteSource = AutoCompleteSource.CustomSource;

                ResetEmploye();
            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Add_Empreinte (LoadEmploye)", ex);
            }
        }

        public void LoadPointeuse()
        {
            if (Constantes.POINTEUSES.Count < 1)
            {
                Societe s = SocieteBLL.ReturnSociete();
                Constantes.POINTEUSES = PointeuseBLL.List("select * from yvs_pointeuse where societe = " + s.Id + " order by adresse_ip");
            }
            dgv_pointeuse.Rows.Clear();
            ObjectThread o = new ObjectThread(dgv_pointeuse);
            foreach (Pointeuse p in Constantes.POINTEUSES)
            {
                o.WriteDataGridView(new object[] { p.Id, p.Ip });
            }
            ResetDataPointeuse();
        }

        private void LoadEmpreinte(List<Empreinte> l)
        {
            ObjectThread o = new ObjectThread(dgv_empreinte);
            o.ClearDataGridView(true);
            ObjectThread o_ = new ObjectThread(Constantes.PBAR_WAIT);
            o_.UpdateMaxBar(Constantes.PBAR_WAIT.Maximum + l.Count);
            foreach (Empreinte e in l)
            {
                Finger f = (Finger)Finger.Get(e.Digital);
                o.WriteDataGridView(new object[] { e.Id, false, e.Employe.Id, e.Employe.NomPrenom, f.Main, f.Doigt });
                Constantes.LoadPatience(false);
            }
            Constantes.LoadPatience(true);
        }

        private void LoadDestination()
        {
            ObjectThread o = new ObjectThread(dgv_destination);
            o.ClearDataGridView(true);
            if (currentPointeuse != null ? currentPointeuse.Id > 0 : false)
            {
                foreach (Pointeuse p in Constantes.POINTEUSES)
                {
                    if (currentPointeuse.Ip != p.Ip)
                    {
                        o.WriteDataGridView(new object[] { p.Id, false, p.Ip });
                    }
                }
            }
            else
            {
                foreach (Pointeuse p in Constantes.POINTEUSES)
                {
                    o.WriteDataGridView(new object[] { p.Id, false, p.Ip, });
                }
            }
            ResetDataDestination();
        }

        private void AddCheckBoxHeader_()
        {
            CheckBox checkbox = new CheckBox();
            checkbox.Size = new System.Drawing.Size(15, 15);
            checkbox.BackColor = Color.Transparent;

            // Reset properties
            checkbox.Padding = new Padding(0);
            checkbox.Margin = new Padding(0);
            checkbox.Text = "";

            // Add checkbox to datagrid cell
            dgv_empreinte.Controls.Add(checkbox);
            DataGridViewHeaderCell header = dgv_empreinte.Columns["check_"].HeaderCell;
            checkbox.Location = new Point(
                header.ContentBounds.Left + (header.ContentBounds.Right - header.ContentBounds.Left + checkbox.Size.Width) / 2,
                header.ContentBounds.Top + (header.ContentBounds.Bottom - header.ContentBounds.Top + checkbox.Size.Height) / 2
            );
        }


        private void AddCheckBoxHeader()
        {
            // add checkbox header
            Rectangle rect = dgv_empreinte.GetCellDisplayRectangle(1, -1, true);
            // set checkbox header to center of header cell. +1 pixel to position correctly.
            rect.X = rect.Location.X + (rect.Width / 4);
            rect.Y = 1;

            CheckBox checkboxHeader = new CheckBox();
            checkboxHeader.Name = "checkboxHeader";
            checkboxHeader.Size = new Size(18, 18);
            checkboxHeader.Location = rect.Location;
            checkboxHeader.BackColor = Color.Transparent;
            checkboxHeader.CheckedChanged += new EventHandler(checkboxHeader_CheckedChanged);

            dgv_empreinte.Controls.Add(checkboxHeader);
        }

        private void checkboxHeader_CheckedChanged(object sender, EventArgs e)
        {
            empreintes.Clear();
            bool check = ((CheckBox)dgv_empreinte.Controls.Find("checkboxHeader", true)[0]).Checked;
            for (int i = 0; i < dgv_empreinte.RowCount; i++)
            {
                if (check)
                {
                    empreintes.Add(le[i]);
                }
                dgv_empreinte[1, i].Value = check;
            }
            dgv_empreinte.EndEdit();
        }

        private void chk_all_CheckedChanged(object sender, EventArgs e)
        {
            com_employe.Enabled = !chk_all.Checked;
            ResetEmploye();
        }

        private void chk_via_serveur_CheckedChanged(object sender, EventArgs e)
        {
            grp_source.Enabled = !chk_via_serveur.Checked;
            ResetDataPointeuse();
            ResetDataEmpreinte();
            LoadDestination();
            ResetEmploye();
            dgv_empreinte.Rows.Clear();
            le = new List<Empreinte>();
        }

        private void dgv_pointeuse_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_pointeuse.CurrentRow.Cells["id"].Value != null)
                {
                    int id = Convert.ToInt32(dgv_pointeuse.CurrentRow.Cells["id"].Value);
                    if (id > 0)
                    {
                        currentPointeuse = PointeuseBLL.OneById(id);
                        LoadDestination();
                    }
                    else
                    {
                        ResetDataPointeuse();
                    }
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Empreinte (dgv_pointeuse_CellContentClick) ", ex);
            }
        }

        private void dgv_destination_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_destination.CurrentRow.Cells["id_d"].Value != null)
                {
                    int id = Convert.ToInt32(dgv_destination.CurrentRow.Cells["id_d"].Value);
                    if (id > 0)
                    {
                        Pointeuse p = PointeuseBLL.OneById(id);
                        if (p != null ? p.Id > 0 : false)
                        {
                            int pos = Utils.GetRowData(dgv_destination, id);
                            ObjectThread o = new ObjectThread(dgv_destination);
                            o.RemoveDataGridView(pos);

                            Pointeuse p_ = destinations.Find(x => x.Id == p.Id);
                            if (p_ != null ? p_.Id > 0 : false)
                            {
                                destinations.RemoveAt(destinations.FindIndex(x => x.Id == p.Id));
                                o.WriteDataGridView(pos, new object[] { p.Id, false, p.Ip, });
                            }
                            else
                            {
                                destinations.Add(p);
                                o.WriteDataGridView(pos, new object[] { p.Id, true, p.Ip, });
                            }
                            ResetDataDestination_();
                            dgv_destination.Rows[pos].Selected = true;
                        }
                    }
                    else
                    {
                        ResetDataDestination();
                    }
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Empreinte (dgv_destination_CellContentClick) ", ex);
            }
        }

        private void btn_load_template_Click(object sender, EventArgs e)
        {
            dgv_empreinte.Rows.Clear();
            le.Clear();
            pbar_statut.Value = 0;

            Constantes.PBAR_WAIT = pbar_statut;
            Thread t = new Thread(new ThreadStart(LoadTemplate));
            t.Start();
        }

        private void LoadTemplate()
        {
            bool serveur = chk_via_serveur.Checked;
            if (serveur)
            {
                Utils.WriteLog("Chargement des empreintes du serveur");
                if (employe != null ? employe.Id < 1 : true)
                {
                    Societe s = SocieteBLL.ReturnSociete();
                    le = EmpreinteBLL.List("select p.* from yvs_grh_empreinte_employe p inner join yvs_grh_employes e on p.employe = e.id inner join yvs_agences a on e.agence = a.id where a.societe = " + s.Id + " order by e.nom");
                }
                else
                {
                    le = EmpreinteBLL.List("select p.* from yvs_grh_empreinte_employe p where p.employe = " + employe.Id);
                }
            }
            else
            {
                if (currentPointeuse != null ? currentPointeuse.Id > 0 : false)
                {
                    Appareil z = Utils.ReturnAppareil(currentPointeuse);
                    Utils.VerifyZkemkeeper(ref z, ref currentPointeuse);
                    if (z == null)
                    {
                        Utils.WriteLog("La liaison avec l'appareil " + currentPointeuse.Ip + " est corrompue");
                        return;
                    }
                    currentPointeuse.Zkemkeeper = z;
                    Utils.WriteLog("Chargement des empreintes de l'appareil " + currentPointeuse.Ip);
                    if (employe != null ? employe.Id < 1 : true)
                    {
                        le = z.GetAllTemplate(currentPointeuse.IMachine, currentPointeuse.Connecter);
                    }
                    else
                    {
                        le = z.GetAllTemplate(currentPointeuse.IMachine, (int)employe.Id, currentPointeuse.Connecter);
                    }
                }
                else
                {
                    Utils.WriteLog("Vous devez selectionner une pointeuse");
                }
            }
            if (le != null ? le.Count > 0 : false)
            {
                ObjectThread o = new ObjectThread(Constantes.PBAR_WAIT);
                o.UpdateMaxBar(le.Count);
                LoadEmpreinte(le);
            }
            else
            {
                Constantes.LoadPatience(true);
            }
            ResetDataEmpreinte();
        }

        private void dgv_empreinte_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_empreinte.CurrentRow.Cells["id_em"].Value != null)
                {
                    int id = Convert.ToInt32(dgv_empreinte.CurrentRow.Cells["id_em"].Value);
                    if (id > 0)
                    {
                        Empreinte p = le.Find(x => x.Id == id);
                        if (p != null ? p.Id > 0 : false)
                        {
                            int pos = Utils.GetRowData(dgv_empreinte, id);
                            ObjectThread o = new ObjectThread(dgv_empreinte);
                            o.RemoveDataGridView(pos);

                            Empreinte p_ = empreintes.Find(x => x.Id == p.Id);
                            if (p_ != null ? p_.Id > 0 : false)
                            {
                                empreintes.RemoveAt(empreintes.FindIndex(x => x.Id == p.Id));
                                Finger f = (Finger)Finger.Get(p.Digital);
                                o.WriteDataGridView(pos, new object[] { p.Id, false, p.Employe.Id, p.Employe.NomPrenom, f.Main, f.Doigt });
                            }
                            else
                            {
                                empreintes.Add(p);
                                Finger f = (Finger)Finger.Get(p.Digital);
                                o.WriteDataGridView(pos, new object[] { p.Id, true, p.Employe.Id, p.Employe.NomPrenom, f.Main, f.Doigt });
                            }
                            ResetDataEmpreinte_();
                            dgv_empreinte.Rows[pos].Selected = true;
                        }
                    }
                    else
                    {
                        ResetDataDestination();
                    }
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Empreinte (dgv_destination_CellContentClick) ", ex);
            }
        }

        private void com_employe_SelectedIndexChanged(object sender, EventArgs e)
        {
            Employe a = com_employe.SelectedItem as Employe;
            employe = employes.Find(x => x.Id == a.Id);
        }

        private void btn_synchro_Click(object sender, EventArgs e)
        {
            if (empreintes != null ? empreintes.Count > 0 : false)
            {
                if (destinations != null ? destinations.Count > 0 : false)
                {
                    Utils.WriteLog("Demande de la synchronisation des empreintes");
                    if (Messages.Confirmation("synchroniser les empreintes") == System.Windows.Forms.DialogResult.Yes)
                    {
                        pbar_statut.Value = 0;
                        Constantes.PBAR_WAIT = pbar_statut;
                        Thread t = new Thread(new ThreadStart(Synchrone));
                        t.Start();
                    }
                    else
                    {
                        Utils.WriteLog("-- Synchronisation des empreintes annulée");
                    }
                }
                else
                {
                    Utils.WriteLog("Synchronisation impossible car la liste des destinataires ne peut pas etre vide");
                }
            }
            else
            {
                Utils.WriteLog("Synchronisation impossible car la liste des empreintes ne peut pas etre vide");
            }
        }

        private void Synchrone()
        {
            ObjectThread o = new ObjectThread(Constantes.PBAR_WAIT);
            o.UpdateMaxBar(empreintes.Count);
            Utils.WriteLog("-- Début de la synchronisation des empreintes");
            foreach (Pointeuse p in destinations)
            {
                Utils.WriteLog("---- Début de la synchronisation sur l'appareil " + p.Ip);
                Fonctions.SynchroniseTmpOneServeur(p, empreintes);
                Utils.WriteLog("---- Fin de la synchronisation sur l'appareil " + p.Ip);
                Constantes.LoadPatience(false);
            }
            Utils.WriteLog("-- Fin de la synchronisation des empreintes");
            Constantes.LoadPatience(true);
        }

        private void btn_add_serveur_Click(object sender, EventArgs e)
        {
            if (currentPointeuse != null ? currentPointeuse.Id > 0 : false)
            {
                if (empreintes != null ? empreintes.Count > 0 : false)
                {
                    Utils.WriteLog("Demande du téléchargement des empreintes");
                    if (Messages.Confirmation("télécharger les empreintes") == System.Windows.Forms.DialogResult.Yes)
                    {
                        pbar_statut.Value = 0;
                        Constantes.PBAR_WAIT = pbar_statut;
                        Thread t = new Thread(new ThreadStart(Download));
                        t.Start();
                    }
                    else
                    {
                        Utils.WriteLog("-- Téléchargement des empreintes annulée");
                    }
                }
                else
                {
                    Utils.WriteLog("Téléchargement impossible car la liste des empreintes ne peut pas etre vide");
                }
            }
            else
            {
                Utils.WriteLog("Vous devez selectionner une pointeuse");
            }
        }

        private void Download()
        {
            Utils.WriteLog("-- Début du téléchargement des empreintes");
            ObjectThread o = new ObjectThread(Constantes.PBAR_WAIT);
            o.UpdateMaxBar(empreintes.Count);
            foreach (Empreinte e in empreintes)
            {
                Empreinte e_ = EmpreinteBLL.OneByEmployeFinger(e.Employe.Id, e.Digital);
                if (e_ != null ? e_.Id < 1 : true)
                {
                    if (EmpreinteBLL.Insert(e))
                    {
                        Utils.WriteLog("---- Ajout de l'empreinte de l'employé " + e.Employe.NomPrenom + " effectue!");
                    }
                    else
                    {
                        Utils.WriteLog("---- Ajout de l'empreinte de l'employé " + e.Employe.NomPrenom + " echoué!");
                    }
                }
                Constantes.LoadPatience(false);
            }
            Utils.WriteLog("-- Fin du téléchargement des empreintes");
            Constantes.LoadPatience(true);
        }

        private void btn_distant_Click(object sender, EventArgs e)
        {
            Utils.WriteLog("Ouverture page (Serveur Distant)");
            new Form_Serveur_Distant(this).ShowDialog();
        }
    }
}
