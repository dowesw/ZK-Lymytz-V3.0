using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZK_Lymytz.TOOLS;

namespace ZK_Lymytz.IHM
{
    public partial class Form_ViewResult : Form
    {
		DateTime date = new DateTime();
		bool definedDate = false;
        string nameBtnPlus = "Voir Plus";

        public Form_ViewResult()
        {
            InitializeComponent();
            Configuration.Load(this);
        }

        private void Form_ViewResult_FormClosing(object sender, FormClosingEventArgs e)
        {
            Constantes.FORM_VIEW_RESULT = null;
            Utils.WriteLog("Fermeture page (Viewer Logs)");
        }

        private void Load()
        {
            nameBtnPlus = "Voir Plus (du " + date.AddDays(-7).ToShortDateString() + " au " + DateTime.Now.ToShortDateString() + ")";
            btn_plus.Text = nameBtnPlus;

            lv_infos.Items.Clear();
            foreach (string line in Logs.ReadTxt(date, DateTime.Now))
            {
                lv_infos.Items.Add(line);
            }
		}

        private void Form_ViewResult_Activated(object sender, EventArgs e)
        {
            if (!definedDate)
            {
                date = DateTime.Now;
                date = date.AddDays(-7);
            }
            Load();
        }

        private void btn_plus_Click(object sender, EventArgs e)
        {
            definedDate = true;
            date = date.AddDays(-7);
            var v = date;
            Load();
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            date = DateTime.Now;
            date = date.AddDays(-7);
            Load();
        }
    }
}
