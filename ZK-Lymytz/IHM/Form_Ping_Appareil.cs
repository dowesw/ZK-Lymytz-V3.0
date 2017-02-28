using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;

using ZK_Lymytz.TOOLS;

namespace ZK_Lymytz.IHM
{
    public partial class Form_Ping_Appareil : Form
    {
        public Form_Ping_Appareil()
        {
            InitializeComponent();
            Configuration.Load(this);
        }

        private void btn_test_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(txt_ip.Tag) == 1)
            {
                if (txt_ip.Text != null && txt_port.Value != null)
                {
                    string ip = txt_ip.Text.Trim();
                    if (new Appareil().ConnectNet(ip, Convert.ToInt16(txt_port.Value), true))
                    {
                        Messages.Show("Connection Succès");
                    }
                    else
                    {
                        Messages.Show("Connection Echec");
                    }
                }
            }
        }

        private void txt_ip_Leave(object sender, EventArgs e)
        {
            try
            {
                string ip = txt_ip.Text.Trim();
                IPAddress.Parse(ip);
                txt_ip.ForeColor = Color.FromName(Configuration.fore_color_Text);
                txt_ip.Tag = 1;
            }
            catch (Exception ex)
            {
                txt_ip.ForeColor = Color.Red;
                txt_ip.Tag = 0;
            }
        }

        private void Form_Ping_Appareil_FormClosing(object sender, FormClosingEventArgs e)
        {
            Constantes.FORM_PING_APPAREIL = null;
            Utils.removeFrom("Form_Ping_Appareil");
            Utils.WriteLog("Fermeture page (Ping Appareil)");
        }
    }
}
