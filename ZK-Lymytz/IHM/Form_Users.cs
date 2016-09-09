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
    public partial class Form_Users : Form
    {
        bool ask_ = false;
        public Form_Users()
        {
            InitializeComponent();
            Configuration.Load(this);
        }

        private void Form_Users_FormClosing(object sender, FormClosingEventArgs e)
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

        private void Form_Users_Load(object sender, EventArgs e)
        {
            txt_domain.Text = Chemins.domainName;
            txt_name.Text = Chemins.usersName;
        }

        private Users Recopiew()
        {
            Users u = new Users();
            u.Name = txt_name.Text.Trim();
            u.PasswordPC = txt_password_pc.Text.Trim();
            u.PasswordLog = Utils.GetMd5Hash(txt_password_log.Text.Trim());
            return u;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Users u = Recopiew();
            if (u.Control())
            {
                if (Utils.IsAuthenticated(u.Name, u.PasswordPC))
                {
                    if (UsersBLL.CreateUsers(u))
                    {
                        ask_ = true;
                        Application.Restart();
                    }
                }
                else
                {
                    if (Messages.Erreur_Retry("Utilisateur Incorrect") == System.Windows.Forms.DialogResult.Retry)
                    {
                        btn_save_Click(sender, e);
                    }
                }
            }
        }
    }
}
