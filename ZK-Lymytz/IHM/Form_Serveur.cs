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
    public partial class Form_Serveur : Form
    {
        bool ask_ = false;
        public Form_Serveur()
        {
            InitializeComponent();
            Configuration.Load(this);
        }

        public ENTITE.Serveur getServeur()
        {
            ENTITE.Serveur bean = new ENTITE.Serveur();
            bean.Adresse = txt_adress.Text.Trim();
            bean.Database = txt_db.Text.Trim();
            bean.Password = txt_pwd.Text.Trim();
            bean.User = txt_user.Text.Trim();
            bean.Port = Convert.ToInt32((txt_port.Text.Trim() != "") ? txt_port.Text.Trim() : "5432");
            return bean;
        }

        private void Form_Serveur_Load(object sender, EventArgs e)
        {
            LoadCurrentServeur();
        }

        private void LoadCurrentServeur()
        {
            ENTITE.Serveur s = BLL.ServeurBLL.ReturnServeur();
            if (s != null ? !(s.Adresse == null || s.Adresse.Trim().Equals("")) : false)
            {
                txt_adress.Text = s.Adresse;
                txt_db.Text = s.Database;
                txt_pwd.Text = s.Password;
                txt_user.Text = s.User;
                txt_port.Text = s.Port.ToString();
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            ENTITE.Serveur serveur = getServeur();
            if (new TOOLS.Connexion().isConnection(serveur))
            {
                if (BLL.ServeurBLL.CreateServeur(serveur))
                {
                    ask_ = true;
                    //Application.ExitThread();
                    Application.Restart();
                }
            }
        }

        private void btn_tester_Click(object sender, EventArgs e)
        {
            ENTITE.Serveur serveur = getServeur();
            if (new TOOLS.Connexion().isConnection(serveur))
                Messages.Information("Connecté");
            else
                Messages.ShowErreur("Echec");
        }

        private void Form_Serveur_FormClosing(object sender, FormClosingEventArgs e)
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
