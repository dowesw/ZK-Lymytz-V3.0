using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZK_Lymytz.BLL;
using ZK_Lymytz.IHM;
using ZK_Lymytz.TOOLS;
using ZK_Lymytz.ENTITE;

namespace ZK_Lymytz.IHM
{
    public partial class Dial_View_No_Insert : Form
    {
        ObjectThread object_presence;
        ObjectThread object_pointage;

        Employe employe;
        IOEMDevice current;
        Presence presence;

        List<Presence> presences = new List<Presence>();
        List<Pointage> pointages = new List<Pointage>();

        public Dial_View_No_Insert(Employe employe, IOEMDevice current)
        {
            InitializeComponent();
            Configuration.Load(this);
            object_presence = new ObjectThread(dgv_presence);
            object_pointage = new ObjectThread(dgv_pointage);
            this.employe = employe;
            this.current = current;
        }

        private void ResetPresence()
        {
            for (int i = 0; i < dgv_presence.Rows.Count; i++)
            {
                dgv_presence.Rows[i].Selected = false;
            }
        }

        private void ResetPointage()
        {
            for (int i = 0; i < dgv_pointage.Rows.Count; i++)
            {
                dgv_pointage.Rows[i].Selected = false;
            }
        }

        private void Dial_View_No_Insert_Load(object sender, EventArgs e)
        {
            if (current.iCorrect)
            {
                this.Dispose();
                return;
            }
            string adresse = Constantes.SOCIETE.AdresseIp;
            //Recherche des fiches dont l'heure definie à deja été inserée
            DateTime time = new DateTime(current.idwYear, current.idwMonth, current.idwDay, current.idwHour, current.idwMinute, 0);
            string query = "select r.* from yvs_grh_pointage p inner join yvs_grh_presence r on p.presence = r.id where r.employe = " + employe.Id + " and ((heure_entree is not null and heure_entree = '" + time + "') or (heure_sortie is not null and heure_sortie = '" + time + "'))";
            presences = PresenceBLL.List(query, true, adresse);
            if (presences != null ? presences.Count > 0 : false)
            {
                foreach (Presence p in presences)
                {
                    object[] value = new object[] { p.Id, p.Employe.NomPrenom, p.DateDebut.ToShortDateString() + " à " + p.HeureDebut.ToShortTimeString(), p.DateFin.ToShortDateString() + " à " + p.HeureFin.ToShortTimeString(), p.DateFinPrevu.ToShortDateString(), p.HeureFinPrevu.ToShortTimeString(), p.Valider };
                    object_presence.WriteDataGridView(value);
                }
                lb_statut.Text = "Ce pointage à deja été inseré";
            }
            else
            {
                //Recherche des fiches dont l'heure definie se trouve dans une fiche validée
                query = "select * from yvs_grh_presence where employe = " + employe.Id + " and '" + current.CurrentDateTime + "' between date_debut and date_fin and valider is true";
                presences = PresenceBLL.List(query, true, adresse);
                if (presences != null ? presences.Count > 0 : false)
                {
                    foreach (Presence p in presences)
                    {
                        object[] value = new object[] { p.Id, p.Employe.NomPrenom, p.DateDebut.ToShortDateString() + " à " + p.HeureDebut.ToShortTimeString(), p.DateFin.ToShortDateString() + " à " + p.HeureFin.ToShortTimeString(), p.DateFinPrevu.ToShortDateString(), p.HeureFinPrevu.ToShortTimeString(), p.Valider };
                        object_presence.WriteDataGridView(value);
                    }
                    lb_statut.Text = "Ce pointage se trouve dans une fiche validée";
                }
                else
                {
                    //Recherche des fiches dont l'heure definie se trouve entre la date debut et la date de fin prevu
                    query = "select * from yvs_grh_presence where employe = " + employe.Id + " and '" + current.CurrentDateTime + "' between date_debut and date_fin_prevu";
                    presences = PresenceBLL.List(query, true, adresse);
                    if (presences != null ? presences.Count > 0 : false)
                    {
                        foreach (Presence p in presences)
                        {
                            object[] value = new object[] { p.Id, p.Employe.NomPrenom, p.DateDebut.ToShortDateString() + " à " + p.HeureDebut.ToShortTimeString(), p.DateFin.ToShortDateString() + " à " + p.HeureFin.ToShortTimeString(), p.DateFinPrevu.ToShortDateString(), p.HeureFinPrevu.ToShortTimeString(), p.Valider };
                            object_presence.WriteDataGridView(value);
                        }
                        lb_statut.Text = "Ce pointage se trouve entre la date debut et la date de fin prevu d'une fiche déjà validée";
                    }
                }
            }
            if (presences != null ? presences.Count > 0 && presences.Count < 2 : false)
            {
                loadPointage(presences[0], adresse);
            }
        }

        private void loadPointage(Presence y, string adresse)
        {
            object_pointage.ClearDataGridView(true);

            if (y != null ? y.Id > 0 : false)
            {
                presence = y;

                string query = "select * from yvs_grh_pointage where presence = " + y.Id;
                List<Pointage> lp = PointageBLL.List(query, true, adresse);
                if (lp != null ? lp.Count > 0 : false)
                {
                    foreach (Pointage p in lp)
                    {
                        object[] value = new object[] { p.Id, p.HeureEntree.ToShortTimeString(), p.HeureSortie.ToShortTimeString(), p.Duree, p.Valider, p.Supplementaire, false, true };
                        object_pointage.WriteDataGridView(value);
                    }
                }
            }
        }

        private void dgv_presence_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo info = dgv_presence.HitTest(e.X, e.Y); //get info
            int pos = dgv_presence.HitTest(e.X, e.Y).RowIndex;
            if (pos > -1)
            {
                if (dgv_presence.Rows[pos].Cells[0].Value != null)
                {
                    Int64 id = (Int64)dgv_presence.Rows[pos].Cells[0].Value;
                    if (id > 0)
                    {
                        string adresse = Constantes.SOCIETE.AdresseIp;
                        Presence f = presences.Find(x => x.Id == id);
                        switch (e.Button)
                        {
                            case MouseButtons.Right:
                                {
                                    ResetPresence();
                                    dgv_presence.Rows[pos].Selected = true; //Select the row
                                    loadPointage(f, adresse);
                                }
                                break;
                            default:
                                loadPointage(f, adresse);
                                break;
                        }
                    }
                }
            }
        }

        private void dgv_pointage_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo info = dgv_pointage.HitTest(e.X, e.Y); //get info
            int pos = dgv_pointage.HitTest(e.X, e.Y).RowIndex;
            if (pos > -1)
            {
                if (dgv_pointage.Rows[pos].Cells[0].Value != null)
                {
                    Int64 id = (Int64)dgv_pointage.Rows[pos].Cells[0].Value;
                    if (id > 0)
                    {
                        Pointage f = pointages.Find(x => x.Id == id);
                        switch (e.Button)
                        {
                            case MouseButtons.Right:
                                {
                                    ResetPointage();
                                    dgv_pointage.Rows[pos].Selected = true; //Select the row
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private void envoyerSurLaVueDesPrésencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (presence != null ? presence.Id > 0 : false)
            {
                if (Constantes.FORM_PRESENCE == null)
                {
                    Form_Presence f = new Form_Presence(presence);
                    f.Show();
                    Constantes.FORM_PRESENCE = f;
                    Utils.WriteLog("Ouverture page (Gestion Présence)");
                    Utils.addFrom("Form_Presence");
                }
                else
                {
                    Constantes.FORM_PRESENCE.WindowState = FormWindowState.Normal;
                    Constantes.FORM_PRESENCE.BringToFront();

                    Constantes.FORM_PRESENCE.Presence = presence;
                    Constantes.FORM_PRESENCE.InitForm();
                }

                this.Dispose();
            }
        }
    }
}
