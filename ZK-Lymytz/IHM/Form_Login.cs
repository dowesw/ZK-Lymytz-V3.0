using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Text;
using System.Windows.Forms;

using ZK_Lymytz.BLL;
using ZK_Lymytz.TOOLS;
using ZK_Lymytz.ENTITE;

namespace ZK_Lymytz.IHM
{
    public partial class Form_Login : Form
    {
        int nbreerror = 0;
        int form = 0;
        bool view = false, _died;

        ObjectThread object_temps;
        ObjectThread object_bar;

        public Form_Login(int _form)
        {
            InitializeComponent();
            form = _form;
            object_temps = new ObjectThread(temps);
            object_bar = new ObjectThread(p_bar);
        }

        private void Form_Login_Load(object sender, EventArgs e)
        {
            txt_id.Focus();
            object_temps.TextLabel("Vous avez " + (p_bar.Maximum).ToString() + " essai(s)");
        }

        private bool IsAdministrateur(string id, string pwd)
        {
            try
            {
                if (id.Equals(Constantes.ADMINISTRATEUR) && pwd.Equals(Constantes.ADMINISTRATEUR))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Login (IsAdministrateur)", ex);
                return false;
            }
        }

        private void MiseZero()
        {
            txt_id.ResetText();
            txt_pwd.ResetText();
        }

        private bool TestVide()
        {
            try
            {
                bool vide = false;
                if (txt_id.Text.Trim().Equals("") || txt_pwd.Text.Trim().Equals(""))
                {
                    if (DialogResult.OK == Messages.ChampsVide())
                    {
                        vide = true;
                        nbreerror += 1;
                        object_temps.TextLabel("Il vous reste " + (p_bar.Maximum - nbreerror).ToString() + " essai(s)");
                        if (p_bar.Value < p_bar.Maximum)
                            object_bar.UpdateSimpleBar(1);
                    }
                }
                return vide;
            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Login (TestVide)", ex);
                return true;
            }
        }

        private void btn_connecter_Click(object sender, EventArgs e)
        {
            try
            {
                bool trouv = false;
                if (!TestVide())
                {
                    string id = txt_id.Text.Trim();
                    string pwd = txt_pwd.Text.Trim();
                    Users u = new Users();
                    if (!IsAdministrateur(id, pwd))
                    {
                        if (Constantes.USERS.Name.Equals(id) && Utils.VerifyMd5Hash(pwd, Constantes.USERS.PasswordLog))
                        {
                            trouv = true;
                        }
                        else
                        {
                            Messages.ShowErreur("Mots de passe incorrect! Reessayer svp");
                            nbreerror += 1;
                            object_temps.TextLabel("Il vous reste " + (p_bar.Maximum - nbreerror).ToString() + " essai(s)");
                            if (p_bar.Value < p_bar.Maximum)
                                object_bar.UpdateSimpleBar(1);
                        }
                    }
                    else
                    {
                        trouv = true;
                    }
                    if (trouv)
                    {
                        if (Constantes.USERS != null)
                        {
                            Constantes.USERS.Id = Int32.MaxValue;
                        }
                        else
                        {
                            Constantes.USERS = UsersBLL.ReturnUsers();
                            Constantes.USERS.Id = Int32.MaxValue;
                        }
                        OpenForm();
                    }
                }
                if (nbreerror >= p_bar.Maximum)
                {
                    object_bar.UpdateSimpleBar(p_bar.Maximum - p_bar.Value);
                    Messages.Information("Nombre d'essai epuisé. Merci");
                    this.Dispose();
                }

            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Login (btn_connecter_Click)", ex);
            }
        }

        public void btn_annuler_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void OpenForm()
        {
            if (Constantes.FORM_PARENT != null)
            {
                switch (form)
                {
                    case 0:
                        {
                            Constantes.FORM_PARENT.Open();
                            break;
                        }
                    case 1:
                        {
                            Constantes.FORM_PARENT.OpenFormSetting();
                            break;
                        }
                    case 20:
                        {
                            Constantes.FORM_PARENT.StopProcessus();
                            break;
                        }
                    case 21:
                        {
                            Constantes.FORM_PARENT.Restart();
                            break;
                        }
                    case 22:
                        {
                            Constantes.FORM_PARENT.Exit();
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            else
            {
                Utils.WriteLog("Action impossible... veuillez redemarrer l'application");
            }
            this.Dispose();
        }

        private void btn_view_Click(object sender, EventArgs e)
        {
            txt_pwd.PasswordChar = view ? '*' : '\0';
            view = !view;
        }

    }
}
