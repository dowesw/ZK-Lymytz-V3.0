using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZK_Lymytz.BLL;
using ZK_Lymytz.IHM;
using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;

namespace ZK_Lymytz.IHM
{
    public partial class Dial_Insert_Pointage : Form
    {
        Form_Evenement parent = null;
        Employe employe = new Employe();
        DateTime current_time = new DateTime();
        DateTime time_decalage = new DateTime();
        string mouvement = "ES"; int cpt_click = 0;

        public Dial_Insert_Pointage(Form_Evenement parent)
        {
            InitializeComponent();
            Configuration.Load(this);
            this.parent = parent;
        }

        private void Dial_Insert_Pointage_Load(object sender, EventArgs e)
        {
            if (parent != null)
            {
                IOEMDevice current = parent.currentPointage;
                employe = EmployeBLL.OneById(current.idwSEnrollNumber); 
                txt_id.Text = employe.Id.ToString();
                txt_matricule.Text = employe.Matricule;
                txt_noms.Text = employe.NomPrenom;
                txt_poste.Text = employe.Poste.Poste.Intitule;
                try
                {
                    current_time = new DateTime(current.idwYear, current.idwMonth, current.idwDay, current.idwHour, current.idwMinute, current.idwSecond);
                    dtp_decalage.Value = current_time;
                    time_decalage = current_time;
                    Presence presence = Fonctions.GetPresence(employe, current_time, true);
                    if (presence != null ? presence.Id > 0 : false)
                    {
                        dtp_date_debut.Value = presence.DateDebut;
                        dtp_date_fin.Value = presence.DateFinPrevu;
                        dtp_heure_debut.Value = presence.HeureDebut;
                        dtp_heure_fin.Value = presence.HeureFinPrevu;
                        box_fich_exit.Image = global::ZK_Lymytz.Properties.Resources.vu;
                    }
                    else
                    {
                        Planning planning = Fonctions.GetPlanning(employe, new DateTime(current_time.Year, current_time.Month, current_time.Day, 0, 0, 0));
                        dtp_date_debut.Value = planning.DateDebut;
                        dtp_date_fin.Value = planning.DateFin;
                        dtp_heure_debut.Value = planning.HeureDebut;
                        dtp_heure_fin.Value = planning.HeureFin;
                    }
                }
                catch (Exception ex) { }
                this.Text += " [" + current_time.ToShortTimeString() + "]";
            }
            else
            {
                this.Dispose();
            }
        }

        private void chk_decalage_CheckedChanged(object sender, EventArgs e)
        {
            dtp_decalage.Enabled = chk_decalage.Checked;
        }

        private void btn_inserer_Click(object sender, EventArgs e)
        {
            if (parent != null)
                parent.InsertPointageInFiche(employe, current_time, dtp_date_debut.Value, dtp_date_fin.Value, chk_decalage.Checked, time_decalage, mouvement, parent.currentPointage.pointeuse);
            this.Dispose();
        }

        private void box_mouv_Click(object sender, EventArgs e)
        {
            cpt_click++;
            switch (cpt_click)
            {
                case 1:
                    box_mouv.Image = global::ZK_Lymytz.Properties.Resources._in;
                    mouvement = "E";
                    lb_mouv.Text = "Faire une entrée";
                    break;
                case 2:
                    box_mouv.Image = global::ZK_Lymytz.Properties.Resources._out;
                    mouvement = "S";
                    lb_mouv.Text = "Faire une sortie";
                    break;
                default:
                    box_mouv.Image = global::ZK_Lymytz.Properties.Resources.in_out;
                    mouvement = "ES";
                    lb_mouv.Text = "Action automatique";
                    cpt_click = 0;
                    break;
            }
        }

        private void dtp_decalage_ValueChanged(object sender, EventArgs e)
        {
            time_decalage = Utils.TimeStamp(time_decalage, dtp_decalage.Value);
            var t = time_decalage;
        }
    }
}
