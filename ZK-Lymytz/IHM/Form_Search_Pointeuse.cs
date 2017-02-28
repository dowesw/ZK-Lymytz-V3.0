using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;

using ZK_Lymytz.BLL;
using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;

namespace ZK_Lymytz.IHM
{
    public partial class Form_Search_Pointeuse : Form
    {
        public List<string> adresses = new List<string>();
        List<Pointeuse> pointeuses = new List<Pointeuse>();
        Pointeuse pointeuse = new Pointeuse();
        ObjectThread object_pointeuse;
        ObjectThread object_pbar;

        public Form_Search_Pointeuse()
        {
            InitializeComponent();
            object_pointeuse = new ObjectThread(dgv_pointeuse);
            object_pbar = new ObjectThread(pbar_statut);
            Configuration.Load(this);
        }

        public Form_Search_Pointeuse(List<string> adresses)
        {
            this.adresses = adresses;
            InitializeComponent();
            object_pointeuse = new ObjectThread(dgv_pointeuse);
            object_pbar = new ObjectThread(pbar_statut);
            Configuration.Load(this);
        }

        public void ResetDataPointeuse()
        {
            for (int i = 0; i < dgv_pointeuse.Rows.Count; i++)
            {
                dgv_pointeuse.Rows[i].Selected = false;
            }
        }

        private void SearchPointeuse(List<string> adresses)
        {
            Utils.WriteLog("Recherche des appareils sur la plage " + adresses[0] + " à " + adresses[adresses.Count - 1]);
            int count = 1;
            foreach (string ip in adresses)
            {
                Appareil z = new Appareil();
                if (Utils.PingAdresse(ip, ref z))
                {
                    Pointeuse p = PointeuseBLL.OneByIp(ip);
                    if (p != null ? p.Id > 0 : false)
                    {
                        p.Zkemkeeper = z;
                    }
                    else
                    {
                        p = new Pointeuse(-(count));
                        p.Ip = ip;
                    }
                    object_pointeuse.WriteDataGridView(new object[] { p.Id, count, p.Ip, p.Connecter });
                    pointeuses.Add(p);
                    count++;
                }
                object_pbar.UpdateBar(1);
            }
            object_pbar.UpdateBar(pbar_statut.Maximum - pbar_statut.Value);
        }

        public void LoadAdresse()
        {
            if (adresses != null ? adresses.Count > 0 : false)
            {
                object_pointeuse.ClearDataGridView(true);
                pointeuses.Clear();
                int count = 1;
                foreach (string adresse in adresses)
                {
                    Pointeuse p = PointeuseBLL.OneByIp(adresse);
                    if (p != null ? p.Id > 0 : false)
                    {
                        p.Zkemkeeper = new Appareil(p);
                    }
                    else
                    {
                        p = new Pointeuse(-(count));
                        p.Ip = adresse;
                    }
                    object_pointeuse.WriteDataGridView(new object[] { p.Id, count, p.Ip, p.Connecter });
                    pointeuses.Add(p);
                    count++;
                }
            }
        }

        private void Form_List_Pointeuse_Load(object sender, EventArgs e)
        {
            string hôte = Dns.GetHostName();
            IPHostEntry iphe = Dns.Resolve(hôte);
            string ip = iphe.AddressList[0].ToString();
            if (ip != null ? ip.Trim().Length > 0 : false)
            {
                string subIp = "";
                for (int i = ip.Length; i > 1; i--)
                {
                    if (ip[i - 1] == '.')
                    {
                        subIp = ip.Substring(0, i);
                        break;
                    }
                }
                txt_ip_debut.Text = subIp + "1";
                txt_ip_fin.Text = subIp + "255";
            }
            LoadAdresse();
        }

        private void btn_scan_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt16(txt_ip_debut.Tag) == 1)
            {
                if (Convert.ToInt16(txt_ip_fin.Tag) == 1)
                {
                    string[] tabDebut = null;
                    string[] tabFin = null;
                    if (Utils.StringToAdress(txt_ip_debut.Text, ref tabDebut) && Utils.StringToAdress(txt_ip_fin.Text, ref tabFin))
                    {
                        List<string> adresses = Utils.Adresses(tabDebut, tabFin);
                        object_pbar.UpdateMaxBar(adresses.Count);
                        if (adresses != null ? adresses.Count > 0 : false)
                        {
                            Thread thread = new Thread(delegate() { SearchPointeuse(adresses); });
                            thread.Start();
                        }
                    }
                }
                else
                {
                    Messages.ShowErreur("L'adresse ip de fin est incorrect");
                }
            }
            else
            {
                Messages.ShowErreur("L'adresse ip de début est incorrect");
            }
        }

        private void dgv_pointeuse_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo info = dgv_pointeuse.HitTest(e.X, e.Y); //get info
            int pos = dgv_pointeuse.HitTest(e.X, e.Y).RowIndex;
            if (pos > -1)
            {
                if (dgv_pointeuse.Rows[pos].Cells["id"].Value != null)
                {
                    Int32 id = (Int32)dgv_pointeuse.Rows[pos].Cells["id"].Value;
                    pointeuse = pointeuses.Find(x => x.Id == id);
                    switch (e.Button)
                    {
                        case MouseButtons.Right:
                            {
                                ResetDataPointeuse();
                                dgv_pointeuse.Rows[pos].Selected = true; //Select the row
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void txt_ip_debut_Leave(object sender, EventArgs e)
        {
            string ip = txt_ip_debut.Text.Trim();
            try
            {
                IPAddress address = IPAddress.Parse(ip);
                txt_ip_debut.ForeColor = Color.FromName(Configuration.fore_color_Text);
                txt_ip_debut.Tag = 1;
            }
            catch (Exception ex)
            {
                txt_ip_debut.ForeColor = Color.Red;
                txt_ip_debut.Tag = 0;
            }
        }

        private void txt_ip_fin_Leave(object sender, EventArgs e)
        {
            string ip = txt_ip_fin.Text.Trim();
            try
            {
                IPAddress address = IPAddress.Parse(ip);
                txt_ip_fin.ForeColor = Color.FromName(Configuration.fore_color_Text);
                txt_ip_fin.Tag = 1;
            }
            catch (Exception ex)
            {
                txt_ip_fin.ForeColor = Color.Red;
                txt_ip_fin.Tag = 0;
            }
        }

        private void Form_List_Pointeuse_FormClosing(object sender, FormClosingEventArgs e)
        {
            Constantes.FORM_FIND_POINTEUSE = null;
            Utils.removeFrom("Form_List_Pointeuse");
            Utils.WriteLog("Fermeture page (Recherche pointeuse)");
        }

        private void insérerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Constantes.FORM_ADD_POINTEUSE == null)
            {
                Form_Pointeuse f = new Form_Pointeuse(pointeuse);
                f.Show();
                Constantes.FORM_ADD_POINTEUSE = f;
                Utils.WriteLog("Ouverture page (Ajout Appareil)");
                Utils.addFrom("Form_Pointeuse_I");
            }
            else
            {
                Utils.WriteLog("Veuillez finir l'opération encours sur l'ajout d'une pointeuse");
            }
        }
    }
}
