using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

using ZK_Lymytz.TOOLS;
using ZK_Lymytz.ENTITE;
using ZK_Lymytz.BLL;

namespace ZK_Lymytz.IHM
{
    public partial class Form_Societe : Form
    {
        bool ask_ = false;
        private List<Societe> societes = new List<Societe>();
        private Societe societe = new Societe();

        public Form_Societe()
        {
            InitializeComponent();
            Configuration.Load(this);
        }

        private void Form_Societe_Load(object sender, EventArgs e)
        {
            LoadSociete();
            LoadCurrentSociete();
        }

        private void LoadCurrentSociete()
        {
            Societe s = SocieteBLL.ReturnSociete();
            if (s != null ? s.Id > 0 : false)
            {
                cbox_societe.SelectedText = s.Name;
                societe = s;
                txt_name.Text = s.Name;
            }
        }

        private void LoadSociete()
        {
            societes = SocieteBLL.List("select * from yvs_societes");
            try
            {
                cbox_societe.Items.Clear();
                for (int i = 0; i < societes.Count; i++)
                {
                    cbox_societe.Items.Add(societes[i].Name);
                    cbox_societe.AutoCompleteCustomSource.Add(societes[i].Name);
                }
                cbox_societe.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbox_societe.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Societe (LoadSociete)", ex);
                cbox_societe.Items.Clear();
            }

        }

        private void cbox_societe_SelectedIndexChanged(object sender, EventArgs e)
        {
            String name = cbox_societe.Text.Trim().Replace("'","''");
            societe = SocieteBLL.OneByName(name);
            txt_name.Text = societe.Name;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (societe != null?societe.Id > 0 :false)
            {
                if (SocieteBLL.CreateSociete(societe))
                {
                    ask_ = true;
                    //Application.ExitThread();
                    Application.Restart();
                }
            }
        }

        private void Form_Societe_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ask_)
            {
                if (Messages.Confirmation(Mots.Msg_FermerApplication.ToLower()) == System.Windows.Forms.DialogResult.Yes)
                {
                    Environment.Exit(0);
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
