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
        public Pointeuse currentPointeuse = new Pointeuse();
        List<Pointeuse> destinations = new List<Pointeuse>();
        public List<Empreinte> empreintes = new List<Empreinte>();
        List<Empreinte> le = new List<Empreinte>();
        Empreinte selectEmpreinte = new Empreinte();
        List<Employe> employes = new List<Employe>();
        Employe employe = new Employe();

        bool IS_INFOS = false;
        bool IS_DIGITAL = true;

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
            employes.Clear();
            ObjectThread object_employe = new ObjectThread(com_employe);
            new Thread(delegate ()
            {
                try
                {
                    employes = EmployeBLL.List(Constantes.QUERY_EMPLOYE(Constantes.SOCIETE), false);
                    object_employe.DisplayMember("NomPrenom");
                    object_employe.ValueMember("Id");
                    object_employe.DataSource(new BindingSource(employes, null));

                    for (int i = 0; i < employes.Count; i++)
                    {
                        Employe e = employes[i];
                        String nom = e.NomPrenom;
                        if (com_employe.AutoCompleteCustomSource.Contains(nom))
                            nom += "°";
                        object_employe.AutoCompleteCustomSource_Add(nom);
                    }
                    object_employe.AutoCompleteMode(AutoCompleteMode.SuggestAppend);
                    object_employe.AutoCompleteSource(AutoCompleteSource.CustomSource);
                }
                catch (Exception ex)
                {
                    Messages.Exception("Form_Add_Empreinte (LoadEmploye)", ex);
                }
                Constantes.EMPLOYES = new List<Employe>(employes);

            }).Start();
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
            int i = 1;
            foreach (Empreinte e in l)
            {
                Finger f = (Finger)Finger.Get(e.Digital);
                o.WriteDataGridView(new object[] { e.Id, false, i, e.Employe.Id, e.Employe.NomPrenom, f.Main, f.Doigt });
                i++;
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
            tsmi_infos.Visible = chk_via_serveur.Checked;
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
                        currentPointeuse = Constantes.POINTEUSES.Find(x => x.Id == id);
                        if (currentPointeuse != null ? currentPointeuse.Id > 0 : false)
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
                        Pointeuse p = Constantes.POINTEUSES.Find(x => x.Id == id);
                        if (p != null ? p.Id > 0 : false)
                            p = PointeuseBLL.OneById(id);
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

        private void dgv_empreinte_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_empreinte.CurrentRow.Cells["id_em"].Value != null)
                {
                    int id = Convert.ToInt32(dgv_empreinte.CurrentRow.Cells["id_em"].Value);
                    if (!IS_INFOS ? (id > 0) : true)
                    {
                        Empreinte p = le.Find(x => x.Id == id);
                        if (p != null ? (!IS_INFOS ? p.Id > 0 : true) : false)
                        {
                            int pos = Utils.GetRowData(dgv_empreinte, id);
                            ObjectThread o = new ObjectThread(dgv_empreinte);
                            o.RemoveDataGridView(pos);

                            Empreinte p_ = empreintes.Find(x => x.Id == p.Id);
                            if (p_ != null ? (!IS_INFOS ? p_.Id > 0 : true) : false)
                            {
                                empreintes.RemoveAt(empreintes.FindIndex(x => x.Id == p.Id));
                                Finger f = (Finger)Finger.Get(p.Digital);
                                o.WriteDataGridView(pos, new object[] { p.Id, false, pos + 1, p.Employe.Id, p.Employe.NomPrenom, f.Main, f.Doigt });
                            }
                            else
                            {
                                empreintes.Add(p);
                                Finger f = (Finger)Finger.Get(p.Digital);
                                o.WriteDataGridView(pos, new object[] { p.Id, true, pos + 1, p.Employe.Id, p.Employe.NomPrenom, f.Main, f.Doigt });
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

        private void dgv_empreinte_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo info = dgv_empreinte.HitTest(e.X, e.Y); //get info
            int pos = dgv_empreinte.HitTest(e.X, e.Y).RowIndex;
            if (pos > -1)
            {
                if (dgv_empreinte.Rows[pos].Cells["id_em"].Value != null)
                {
                    int id = Convert.ToInt32(dgv_empreinte.Rows[pos].Cells["id_em"].Value);
                    if (id > 0)
                    {
                        Empreinte y = le.Find(x => x.Id == id);
                        if (y != null ? y.Id > 0 : false)
                        {
                            switch (e.Button)
                            {
                                case MouseButtons.Right:
                                    {
                                        selectEmpreinte = y;
                                        ResetDataEmpreinte_();
                                        dgv_empreinte.Rows[pos].Selected = true; //Select the row
                                    }
                                    break;
                                default:

                                    break;
                            }
                        }
                    }
                }
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
            if (IS_INFOS)
            {
                foreach (Pointeuse p in destinations)
                {
                    Utils.WriteLog("---- Début de la synchronisation sur l'appareil " + p.Ip);
                    Fonctions.SynchroniseInfosServeur(p, empreintes);
                    Utils.WriteLog("---- Fin de la synchronisation sur l'appareil " + p.Ip);
                    Constantes.LoadPatience(false);
                }
            }
            else
            {
                if (IS_DIGITAL)
                {
                    foreach (Pointeuse p in destinations)
                    {
                        Utils.WriteLog("---- Début de la synchronisation sur l'appareil " + p.Ip);
                        Fonctions.SynchroniseTmpOneServeur(p, empreintes);
                        Utils.WriteLog("---- Fin de la synchronisation sur l'appareil " + p.Ip);
                        Constantes.LoadPatience(false);
                    }
                }
                else
                {
                    foreach (Pointeuse p in destinations)
                    {
                        Utils.WriteLog("---- Début de la synchronisation sur l'appareil " + p.Ip);
                        Fonctions.SynchroniseFaceOneServeur(p, empreintes);
                        Utils.WriteLog("---- Fin de la synchronisation sur l'appareil " + p.Ip);
                        Constantes.LoadPatience(false);
                    }
                }
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
                Employe y = EmployeBLL.OneById((int)e.Employe.Id);
                if (y != null ? y.Id > 0 : false)
                {
                    if (IS_DIGITAL)
                    {
                        Empreinte e_ = EmpreinteBLL.OneByEmployeFinger(e.Employe.Id, e.Digital);
                        if (e_ != null ? e_.Id < 1 : true)
                        {
                            if (EmpreinteBLL.Insert(e))
                            {
                                Finger f = (Finger)Finger.Get(e.Digital);
                                Utils.WriteLog("---- Ajout de l'empreinte Doigt(" + f.Doigt + ") Main(" + f.Main + ")  de l'employé " + e.Employe.NomPrenom + " effectue!");
                            }
                            else
                            {
                                Utils.WriteLog("---- Ajout de l'empreinte de l'employé " + e.Employe.NomPrenom + " echoué!");
                            }
                        }
                    }
                    else
                    {
                        Empreinte e_ = EmpreinteBLL.OneByEmployeFacial(e.Employe.Id, e.Facial);
                        if (e_ != null ? e_.Id < 1 : true)
                        {
                            if (EmpreinteBLL.Insert(e))
                            {
                                Utils.WriteLog("---- Ajout de l'empreinte faciale de l'employé " + e.Employe.NomPrenom + " effectue!");
                            }
                            else
                            {
                                Utils.WriteLog("---- Ajout de l'empreinte de l'employé " + e.Employe.NomPrenom + " echoué!");
                            }
                        }
                    }
                }
                else
                {
                    Utils.WriteLog("---- Ajout de l'empreinte de l'employé " + e.Employe.NomPrenom + " echoué! Car ce tiers n'est pas un employé");
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

        private void tsmi_empreinte_Click(object sender, EventArgs e)
        {
            dgv_empreinte.Rows.Clear();
            le.Clear();
            pbar_statut.Value = 0;

            main.Visible = true;
            doigt.Visible = true;
            IS_DIGITAL = true;
            IS_INFOS = false;

            Constantes.PBAR_WAIT = pbar_statut;
            Thread thread = new Thread(delegate() { LoadTemplate(false, false); });
            thread.Start();
        }

        private void tsmi_faciale_Click(object sender, EventArgs e)
        {
            dgv_empreinte.Rows.Clear();
            le.Clear();
            pbar_statut.Value = 0;

            main.Visible = false;
            doigt.Visible = false;
            IS_DIGITAL = false;
            IS_INFOS = false;

            Constantes.PBAR_WAIT = pbar_statut;
            Thread thread = new Thread(delegate() { LoadTemplate(true, false); });
            thread.Start();
        }

        private void tsmi_infos_Click(object sender, EventArgs e)
        {
            dgv_empreinte.Rows.Clear();
            le.Clear();
            pbar_statut.Value = 0;

            main.Visible = false;
            doigt.Visible = false;
            IS_DIGITAL = false;
            IS_INFOS = true;

            Constantes.PBAR_WAIT = pbar_statut;
            Thread thread = new Thread(delegate() { LoadTemplate(true, true); });
            thread.Start();
        }

        private void LoadTemplate(bool facial, bool infos)
        {
            bool serveur = chk_via_serveur.Checked;
            if (serveur)
            {
                Utils.WriteLog("Chargement des empreintes du serveur");
                String query = "";
                if (infos)
                {
                    if (employe != null ? employe.Id < 1 : true)
                    {
                        query = "select e.* from yvs_grh_employes e inner join yvs_agences a on e.agence = a.id where a.societe = " + Constantes.SOCIETE.Id + " order by e.nom";
                    }
                    else
                    {
                        query = "select e.* from yvs_grh_employes e inner join yvs_agences a on e.agence = a.id where e.id = " + employe.Id;
                    }
                    List<Employe> list = EmployeBLL.List(query);
                    le.Clear();
                    foreach (Employe e in list)
                    {
                        le.Add(new Empreinte((long)-(le.Count + 1), e));
                    }
                }
                else
                {
                    if (employe != null ? employe.Id < 1 : true)
                    {
                        query = "select p.* from yvs_grh_empreinte_employe p inner join yvs_grh_employes e on p.employe = e.id inner join yvs_agences a on e.agence = a.id where (p.empreinte_faciale is null or p.empreinte_faciale = 0) and empreinte_digital > -1 and a.societe = " + Constantes.SOCIETE.Id + " order by e.nom";
                        if (facial)
                        {
                            query = "select p.* from yvs_grh_empreinte_employe p inner join yvs_grh_employes e on p.employe = e.id inner join yvs_agences a on e.agence = a.id where (p.empreinte_digital is null or p.empreinte_digital = 0) and empreinte_faciale > 0 and a.societe = " + Constantes.SOCIETE.Id + " order by e.nom";
                        }
                    }
                    else
                    {
                        query = "select p.* from yvs_grh_empreinte_employe p where (p.empreinte_faciale is null or p.empreinte_faciale = 0) and empreinte_digital > -1 and p.employe = " + employe.Id;
                        if (facial)
                        {
                            query = "select p.* from yvs_grh_empreinte_employe p where (p.empreinte_digital is null or p.empreinte_digital = 0) and empreinte_faciale > 0 and p.employe = " + employe.Id;
                        }
                    }
                    le = EmpreinteBLL.List(query);
                    if (chk_not_in.Checked)
                    {
                        List<Empreinte> list = new List<Empreinte>();
                        list.AddRange(le);
                        le.Clear();
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
                            if (facial)
                            {
                                switch (currentPointeuse.Type)
                                {
                                    case Constantes.TYPE_IFACE:
                                        foreach (Empreinte y in list)
                                        {
                                            List<Empreinte> l = z.SSR_GetAllFaceTemplate(currentPointeuse.IMachine, (int)y.Employe.Id, currentPointeuse.Connecter, false);
                                            if (l != null ? l.Count < 1 : true)
                                                le.Add(y);
                                        }
                                        break;
                                    default:
                                        Utils.WriteLog("Les empreintes faciales ne sont pas integrées dans l'appareil " + currentPointeuse.Ip);
                                        break;
                                }
                            }
                            else
                            {
                                switch (currentPointeuse.Type)
                                {
                                    case Constantes.TYPE_IFACE:
                                        foreach (Empreinte y in list)
                                        {
                                            List<Empreinte> l = z.SSR_GetAllTemplate(currentPointeuse.IMachine, (int)y.Employe.Id, currentPointeuse.Connecter, false);
                                            if (l != null ? l.Count < 1 : true)
                                                le.Add(y);
                                        }
                                        break;
                                    default:
                                        foreach (Empreinte y in list)
                                        {
                                            List<Empreinte> l = z.GetAllTemplate(currentPointeuse.IMachine, (int)y.Employe.Id, currentPointeuse.Connecter, false);
                                            if (l != null ? l.Count < 1 : true)
                                                le.Add(y);
                                        }
                                        break;
                                }
                            }
                        }
                        else
                        {
                            Utils.WriteLog("Vous devez selectionner une pointeuse ou déselectionner le filtre sur les empreintes interne");
                        }
                    }
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
                        if (facial)
                        {
                            switch (currentPointeuse.Type)
                            {
                                case Constantes.TYPE_IFACE:
                                    le = z.SSR_GetAllFaceTemplate(currentPointeuse.IMachine, currentPointeuse.Connecter, chk_not_in.Checked);
                                    break;
                                default:
                                    Utils.WriteLog("Les empreintes faciales ne sont pas integrées dans l'appareil " + currentPointeuse.Ip);
                                    break;
                            }
                        }
                        else
                        {
                            switch (currentPointeuse.Type)
                            {
                                case Constantes.TYPE_IFACE:
                                    le = z.SSR_GetAllTemplate(currentPointeuse.IMachine, currentPointeuse.Connecter, chk_not_in.Checked);
                                    break;
                                default:
                                    le = z.GetAllTemplate(currentPointeuse.IMachine, currentPointeuse.Connecter, chk_not_in.Checked);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        if (facial)
                        {
                            switch (currentPointeuse.Type)
                            {
                                case Constantes.TYPE_IFACE:
                                    le = z.SSR_GetAllFaceTemplate(currentPointeuse.IMachine, (int)employe.Id, currentPointeuse.Connecter, chk_not_in.Checked);
                                    break;
                                default:
                                    Utils.WriteLog("Les empreintes faciales ne sont pas integrées dans l'appareil " + currentPointeuse.Ip);
                                    break;
                            }
                        }
                        else
                        {
                            switch (currentPointeuse.Type)
                            {
                                case Constantes.TYPE_IFACE:
                                    le = z.SSR_GetAllTemplate(currentPointeuse.IMachine, (int)employe.Id, currentPointeuse.Connecter, chk_not_in.Checked);
                                    break;
                                default:
                                    le = z.GetAllTemplate(currentPointeuse.IMachine, (int)employe.Id, currentPointeuse.Connecter, chk_not_in.Checked);
                                    break;
                            }
                        }
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

        private void supprimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool serveur = chk_via_serveur.Checked;
            bool continu = serveur;
            Appareil z = new Appareil();
            if (!serveur)
            {
                if (currentPointeuse != null ? currentPointeuse.Id > 0 : false)
                {
                    z = Utils.ReturnAppareil(currentPointeuse);
                    Utils.VerifyZkemkeeper(ref z, ref currentPointeuse);
                    if (z == null)
                    {
                        Utils.WriteLog("La liaison avec l'appareil " + currentPointeuse.Ip + " est corrompue");
                        return;
                    }
                    currentPointeuse.Zkemkeeper = z;
                    if (!IS_DIGITAL)
                    {
                        switch (currentPointeuse.Type)
                        {
                            case Constantes.TYPE_TFT:
                                Utils.WriteLog("Les empreintes faciales ne sont pas integrées dans l'appareil " + currentPointeuse.Ip);
                                return;
                            default:
                                break;
                        }
                    }
                    continu = true;
                }
                else
                {
                    Utils.WriteLog("Vous devez selectionner une pointeuse");
                }

            }

            if (continu)
            {
                if (empreintes != null ? empreintes.Count > 0 : false)
                {
                    Utils.WriteLog("Demande de suppression des empreintes ");
                    if (Messages.Confirmation("supprimer les empreintes") == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (!serveur)
                            Utils.WriteLog("-- Suppression de l'empreintes de l'appareil " + currentPointeuse.Ip + " en cours");
                        else
                            Utils.WriteLog("-- Suppression de l'empreintes du serveur en cours");
                        foreach (Empreinte m in empreintes)
                        {
                            SupprimerEmpreinte(z, m, serveur);
                        }
                    }
                    else
                    {
                        Utils.WriteLog("-- Suppression de empreinte(s) annulée");
                    }
                }
                else
                {
                    Utils.WriteLog("Demande de suppression d'empreinte " + selectEmpreinte.Employe.Id);
                    if (Messages.Confirmation("supprimer l'empreinte") == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (!serveur)
                            Utils.WriteLog("-- Suppression de l'empreinte de l'appareil " + currentPointeuse.Ip + " en cours");
                        else
                            Utils.WriteLog("-- Suppression de l'empreintes du serveur en cours");
                        SupprimerEmpreinte(z, selectEmpreinte, serveur);
                    }
                    else
                    {
                        Utils.WriteLog("-- Suppression de empreinte(s) annulée");
                    }
                }
            }
        }

        private void chk_not_in_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_via_serveur.Checked && chk_not_in.Checked)
            {
                new Form_View_Pointeuse(this).ShowDialog();
            }
        }

        private void tsmi_recuperer_infos_Click(object sender, EventArgs e)
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
                if (empreintes != null ? empreintes.Count > 0 : false)
                {
                    Utils.WriteLog("Demande de recuperation des informations des employés ");
                    if (Messages.Confirmation("recuperer les informations") == System.Windows.Forms.DialogResult.Yes)
                    {
                        foreach (Empreinte m in empreintes)
                        {
                            RecupererInformation(z, m);
                        }
                    }
                    else
                    {
                        Utils.WriteLog("-- Recuperation des informations annulée");
                    }
                }
                else
                {
                    Utils.WriteLog("Demande de recuperation des informations sur l'employé " + selectEmpreinte.Employe.Id);
                    if (Messages.Confirmation("recuperer les informations") == System.Windows.Forms.DialogResult.Yes)
                    {
                        RecupererInformation(z, selectEmpreinte);
                    }
                    else
                    {
                        Utils.WriteLog("-- Recuperation des informations annulée");
                    }
                }
            }
            else
            {
                Utils.WriteLog("Vous devez selectionner une pointeuse");
            }
        }

        private void SupprimerEmpreinte(Appareil z, Empreinte selectEmpreinte, bool serveur)
        {
            if (selectEmpreinte != null ? selectEmpreinte.Id > 0 : false)
            {
                bool correct = false;
                if (serveur)
                {
                    Empreinte y = null;
                    if (IS_DIGITAL)
                    {
                        y = EmpreinteBLL.OneByEmployeFinger(selectEmpreinte.Employe.Id, selectEmpreinte.Digital);
                    }
                    else
                    {
                        y = EmpreinteBLL.OneByEmployeFacial(selectEmpreinte.Employe.Id, selectEmpreinte.Facial);
                    }
                    if (y != null ? y.Id > 0 : false)
                    {
                        correct = EmpreinteBLL.Delete(y, y.Id);
                    }
                }
                else
                {
                    if (IS_DIGITAL)
                    {
                        switch (z._POINTEUSE.Type)
                        {
                            case Constantes.TYPE_IFACE:
                                correct = z.SSR_DelUserTmp(currentPointeuse.IMachine, selectEmpreinte.Employe.Id.ToString(), selectEmpreinte.Digital);
                                break;
                            default:
                                correct = z.DelUserTmp(currentPointeuse.IMachine, (int)selectEmpreinte.Employe.Id, selectEmpreinte.Digital);
                                break;
                        }
                    }
                    else
                    {
                        switch (z._POINTEUSE.Type)
                        {
                            case Constantes.TYPE_IFACE:
                                correct = z.DelUserFace(currentPointeuse.IMachine, selectEmpreinte.Employe.Id.ToString(), selectEmpreinte.Facial);
                                break;
                            default:
                                Utils.WriteLog("Les empreintes faciales ne sont pas integrées dans l'appareil " + currentPointeuse.Ip);
                                break;
                        }
                    }
                }
                if (correct)
                {
                    Utils.WriteLog("---- Suppression effectuée ");
                    ObjectThread o = new ObjectThread(dgv_empreinte);
                    int idx = Utils.GetRowData(dgv_empreinte, selectEmpreinte.Id);
                    o.RemoveDataGridView(idx);
                }
                else
                {
                    Utils.WriteLog("---- Suppression échouée ");
                }
            }
        }

        private void RecupererInformation(Appareil z, Empreinte y)
        {
            if (y != null ? y.Id > 0 : false)
            {
                Employe emp = EmployeBLL.OneById((int)y.Employe.Id);
                if (emp != null ? emp.Id > 0 : false)
                {
                    bool correct = false;
                    switch (currentPointeuse.Type)
                    {
                        case Constantes.TYPE_IFACE:
                            correct = z.SSR_SetUserInfo(currentPointeuse.IMachine, (int)emp.Id, emp.NomPrenom, null, 0, true);//upload user information to the memory
                            break;
                        default:
                            correct = z.SetUserInfo(currentPointeuse.IMachine, (int)emp.Id, emp.NomPrenom, null, 0, true);//upload user information to the memory
                            break;
                    }
                    if (correct)
                    {
                        y.Employe = emp;

                        int pos = Utils.GetRowData(dgv_empreinte, y.Id);
                        ObjectThread o = new ObjectThread(dgv_empreinte);
                        o.RemoveDataGridView(pos);
                        Finger f = (Finger)Finger.Get(y.Digital);
                        Empreinte p = empreintes.Find(x => x.Id == y.Id);
                        bool select = (p != null ? p.Id > 0 : false);
                        o.WriteDataGridView(pos, new object[] { y.Id, select, pos + 1, y.Employe.Id, y.Employe.NomPrenom, f.Main, f.Doigt });

                        Utils.WriteLog("Recuperation Effectuée pour l'employé " + y.Employe.Id);
                    }
                    else
                    {
                        Utils.WriteLog("Recuperation Impossible pour l'employé " + y.Employe.Id);
                    }
                }
            }
        }
    }
}
