using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZK_Lymytz.BLL;
using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;

namespace ZK_Lymytz.IHM
{
    public partial class Form_ViewLog : Form
    {
        List<String> pings = new List<string>();
        ObjectThread object_log;

        public Form_ViewLog()
        {
            InitializeComponent();
            Configuration.Load(this);
            object_log = new ObjectThread(dgv_log);
        }

        private void Form_ViewLog_Load(object sender, EventArgs e)
        {
            com_registre.Items.Clear();
            foreach (Pointeuse p in Constantes.POINTEUSES)
            {
                com_registre.Items.Add(p.Ip);
            }
        }

        private void Form_ViewLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            Constantes.FORM_VIEW_LOG = null;
            Utils.WriteLog("Fermeture page (Viewer Ping)");
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            pings = Logs.ReadTxt(Chemins.CheminPing() + com_registre.Text + ".txt");
            LoadView();
        }

        private void LoadView()
        {
            dgv_log.Rows.Clear();
            for (int i = 0; i < pings.Count; i++)
            {
                string line = pings[i];
                object_log.WriteDataGridView(new object[] { i + 1, line, false });
            }
        }

        private void checkErreurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pings != null ? pings.Count > 0 : false)
            {
                DateTime _last = DateTime.Now;
                for (int i = 0; i < pings.Count; i++)
                {
                    string line = pings[i];
                    if ((i > 0) && ((Constantes.MILLISECONDS(Convert.ToDateTime(line)) - Constantes.MILLISECONDS(_last)) > (1000 * Constantes.MAX_TIME_PING)))
                    {
                        object_log.RemoveDataGridView(i);
                        object_log.WriteDataGridView(i, new object[] { i + 1, line, true });
                    }
                    _last = Convert.ToDateTime(line);
                }
            }
        }
    }
}
