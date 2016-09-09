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

        private void Form_ViewResult_Activated(object sender, EventArgs e)
        {
            lv_infos.Items.Clear();
            foreach (string line in Logs.ReadTxt())
            {
                lv_infos.Items.Add(line);
            }
        }
    }
}
