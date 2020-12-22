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
    public partial class Form_View_Pointeuse : Form
    {
        Pointeuse select;
        Form_Empreinte form;
        public Form_View_Pointeuse(Form_Empreinte form)
        {
            InitializeComponent();
            this.form = form;
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
                o.WriteDataGridView(new object[] { p.Id, p.Ip, p.Emplacement, p.Type });
            }
        }

        public void ResetData()
        {
            for (int i = 0; i < dgv_pointeuse.Rows.Count; i++)
            {
                dgv_pointeuse.Rows[i].Selected = false;
            }
        }

        private void Form_View_Pointeuse_Load(object sender, EventArgs e)
        {
            LoadPointeuse();
        }

        private void dgv_pointeuse_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo info = dgv_pointeuse.HitTest(e.X, e.Y); //get info
            int pos = dgv_pointeuse.HitTest(e.X, e.Y).RowIndex;
            if (pos > -1)
            {
                if (dgv_pointeuse.Rows[pos].Cells[0].Value != null)
                {
                    int id = Convert.ToInt32(dgv_pointeuse.Rows[pos].Cells[0].Value);
                    if (id > 0)
                    {
                        Pointeuse y = Constantes.POINTEUSES.Find(x => x.Id == id);
                        if (y != null ? y.Id > 0 : false)
                        {
                            switch (e.Button)
                            {
                                case MouseButtons.Right:
                                    {
                                        ResetData();
                                        dgv_pointeuse.Rows[pos].Selected = true; //Select the row
                                    }
                                    break;
                                default:
                                    select = y;
                                    form.currentPointeuse = y;
                                    this.Dispose();
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}
