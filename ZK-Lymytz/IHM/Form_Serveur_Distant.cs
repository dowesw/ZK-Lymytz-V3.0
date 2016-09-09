using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using ZK_Lymytz.BLL;
using ZK_Lymytz.IHM;
using ZK_Lymytz.TOOLS;
using ZK_Lymytz.ENTITE;

namespace ZK_Lymytz.IHM
{
    public partial class Form_Serveur_Distant : Form
    {
        private Form_Empreinte F_parent;
        private Serveur serveur;

        public Form_Serveur_Distant(Form_Empreinte parent)
        {
            InitializeComponent();
            Configuration.Load(this);
            this.txt_ip.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_port.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.F_parent = parent;
        }

        private void Form_Serveur_Distant_FormClosed(object sender, FormClosedEventArgs e)
        {
            Constantes.FORM_SERVEUR_DISTANT = null;
            Utils.WriteLog("Fermeture page (Serveur Distant)");
            Utils.removeFrom("Form_Serveur_Distant");
        }

        private void Form_Serveur_Distant_Load(object sender, EventArgs e)
        {
            serveur = ServeurBLL.ReturnServeur();
            txt_ip.Text = serveur.Adresse;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(SendEmpreinte));
            t.Start();
        }

        private void SendEmpreinte()
        {
            string ip = txt_ip.Text.Trim();
            int port = (int)txt_port.Value;
            if (ip == serveur.Adresse && port == serveur.Port)
            {
                Utils.WriteLog("Envoi de(s) empreinte(s) sur le serveur " + ip + " [" + port + "] impossible... car le serveur distant est identique au serveur dedié");
            }
            else
            {
                if (F_parent.empreintes != null ? F_parent.empreintes.Count > 0 : false)
                {
                    Utils.WriteLog("Demande de l'envoi de(s) empreinte(s) sur le serveur " + ip + " [" + port + "]");
                    if (Messages.Confirmation("envoyer les empreintes sur le serveur distant") == System.Windows.Forms.DialogResult.Yes)
                    {
                        serveur.Adresse = ip;
                        Npgsql.NpgsqlConnection con = null;
                        if (new Connexion().isConnection(out con, serveur))
                        {
                            foreach (Empreinte em in F_parent.empreintes)
                            {
                                Finger d = (Finger)Finger.Get(em.Digital);
                                Empreinte e_ = EmpreinteBLL.OneByEmployeFinger(em.Employe.Id, em.Digital, con);
                                if (e_ != null ? e_.Id < 1 : true)
                                {
                                    if (EmpreinteBLL.Insert(em, con))
                                    {
                                        Utils.WriteLog("---- Envoi de l'empreinte de l'employé " + em.Employe.NomPrenom + " du doigt (" + d.Doigt + ") de la main (" + d.Main + ") sur le serveur " + ip + " [" + port + "] effectué");
                                    }
                                }
                                else
                                {
                                    Utils.WriteLog("---- Envoi de l'empreinte de l'employé " + em.Employe.NomPrenom + " du doigt (" + d.Doigt + ") de la main (" + d.Main + ") sur le serveur " + ip + " [" + port + "] impossible... car il existe déja");
                                }
                            }
                            Utils.WriteLog("-- Envoi de(s) empreinte(s) sur le serveur " + ip + " [" + port + "] effectué");
                        }
                        else
                        {
                            Utils.WriteLog("-- Envoi de(s) empreinte(s) sur le serveur " + ip + " [" + port + "] impossible... car connexion au serveur impossible");
                        }
                    }
                    else
                    {
                        Utils.WriteLog("-- Envoi de(s) empreinte(s) sur le serveur " + ip + " [" + port + "] annulé");
                    }
                    ObjectThread o = new ObjectThread(this);
                    o.DisposeForm(true);
                }
                else
                {
                    Utils.WriteLog("Envoi de(s) empreinte(s) sur le serveur " + ip + " [" + port + "] impossible... car la liste des empreintes ne peut pas etre vide");
                }
            }
        }
    }
}
