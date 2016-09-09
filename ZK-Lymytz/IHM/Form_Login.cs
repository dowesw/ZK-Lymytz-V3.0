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
        int i = 0, j = 0, max = 30, nbreerror = 0;
        int form = 0;
        bool view = false;

        public Form_Login(int _form)
        {
            InitializeComponent();
            Configuration.Load(this);

            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Algerian", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(119, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 32);
            this.label1.TabIndex = 3;
            this.label1.Text = "Connexion";

            form = _form;
        }

        private void Form_Login_Load(object sender, EventArgs e)
        {
            txt_id.Focus();
            timer1.Enabled = true;
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
                if (nbreerror >= 3)
                {
                    progressBar1.Value += 100 - progressBar1.Value;
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
            this.Dispose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (i != max)
            {
                ObjectThread o = new ObjectThread(temps);
                o.TextLabel("Il reste " + (max - i).ToString() + " secondes");
                timer1.Stop();
                timer2.Start();
                System.Threading.Thread.Sleep(1000);
                i++;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            ObjectThread o = new ObjectThread(progressBar1);
            if (j == 3)
            {
                timer2.Stop();
                timer1.Start();
                if (i == max)
                {
                    o.UpdateSimpleBar(100 - progressBar1.Value);
                    Messages.Information("Temps de connexion expiré. Merci");
                    this.Dispose();
                }
                j = 0;
            }
            else
            {
                if (progressBar1.Value < progressBar1.Maximum)
                {
                    o.UpdateSimpleBar(1);
                }
                j++;
            }
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
