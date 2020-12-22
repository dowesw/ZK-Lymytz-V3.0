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
    public partial class Dial_View_Heure_Prevu : Form
    {
        Presence current;
        Form_Presence parent;

        public Dial_View_Heure_Prevu(Form_Presence parent, Presence presence)
        {
            InitializeComponent();
            Configuration.Load(this);
            this.current = presence;
            this.parent = parent;
        }

        private void Dial_View_Heure_Prevu_Load(object sender, EventArgs e)
        {
            if (current != null ? current.Id > 1 : false)
            {
                dtp_date.Value = current.DateFinPrevu;
                dtp_heure.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, current.HeureFinPrevu.Hour, current.HeureFinPrevu.Minute, current.HeureFinPrevu.Second);
            }
            else
            {
                this.Dispose();
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            Utils.WriteLog("Demande de modification de la fiche du " + current.DateDebut.ToShortDateString() + " de "+current.Employe.NomPrenom);
            if (Messages.Confirmation_Infos("modifier") == System.Windows.Forms.DialogResult.Yes)
            {
                current.DateFinPrevu = dtp_date.Value;
                current.HeureFinPrevu = dtp_heure.Value;
                if (PresenceBLL.Update(current))
                {
                    int idx = parent.presences.FindIndex(x => x.Id == current.Id);
                    if (idx > -1)
                        parent.presences[idx] = current;
                    Utils.WriteLog("-- Modification de la fiche du " + current.DateDebut.ToShortDateString() + " de " + current.Employe.NomPrenom + " reussie");
                    this.Dispose();
                }
                else
                    Utils.WriteLog("-- Modification de la fiche du " + current.DateDebut.ToShortDateString() + " de " + current.Employe.NomPrenom + " échouée");
            }
            else
            {
                Utils.WriteLog("-- Modification de la fiche du " + current.DateDebut.ToShortDateString() + " de " + current.Employe.NomPrenom + " annulée");
            }
        }
    }
}
