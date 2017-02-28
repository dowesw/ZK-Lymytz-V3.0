using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;

namespace ZK_Lymytz.IHM
{
    public partial class Dial_Connet_Externe : Form
    {
        Pointeuse pointeuse;

        public Dial_Connet_Externe(Pointeuse pointeuse)
        {
            InitializeComponent();
            Configuration.Load(this);
            this.pointeuse = pointeuse;
        }

        private void Dial_Connet_Externe_Load(object sender, EventArgs e)
        {
                if (pointeuse != null ? pointeuse.Id > 0 : false)
                {
                    this.Text = pointeuse.Ip;
                }
        }

        private void btnUSBConnect_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txt_machine.Tag) > 0)
            {
                Cursor = Cursors.WaitCursor;
                string sCom = Utils.SearchforCom();
                if (sCom == null || sCom.Trim().Length < 1)
                {
                    Utils.WriteLog("Impossible de trouver le port serie virtuel utilisé");
                    Cursor = Cursors.Default;
                    return;
                }

                int iPort;
                for (iPort = 1; iPort < 10; iPort++)
                {
                    if (sCom.IndexOf(iPort.ToString()) > -1)
                    {
                        break;
                    }
                }

                int iMachineNumber = Convert.ToInt32(txt_machine.Text.Trim());
                Appareil z = new Appareil();
                bool connect = z.ConnectCom(iPort, iMachineNumber);
                if (connect == true)
                {
                    z.RegEvent(iMachineNumber);//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
                    if (Constantes.FORM_PARENT != null)
                    {
                        pointeuse.Zkemkeeper = z;
                        Utils.WriteLog("-- Connexion de l'appareil : " + pointeuse.Ip + " effectuée");
                        Constantes.POINTEUSES.Find(x => x.Id == pointeuse.Id).Zkemkeeper = z;
                        Constantes.FORM_PARENT.UpdatePointeuse(pointeuse);
                    }
                }
                else
                {
                    Utils.WriteLog("Connexion impossible vers la machine " + iMachineNumber);
                }
                Cursor = Cursors.Default;
            }
            else
            {
                Utils.WriteLog("Connexion impossible... car numéro de machine invalide");
            }
        }

        private void txt_machine_Leave(object sender, EventArgs e)
        {
            int v = Convert.ToInt32(txt_machine.Text.Trim());
            if (v == 0 || v > 255)
            {
                txt_machine.ForeColor = Color.Red;
                txt_machine.Tag = 0;
            }
            else
            {
                txt_machine.ForeColor = Color.FromName(Configuration.fore_color_Text);
                txt_machine.Tag = 1;
            }
        }
    }
}
