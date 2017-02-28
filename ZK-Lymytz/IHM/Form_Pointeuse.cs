using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using ZK_Lymytz.BLL;
using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;

namespace ZK_Lymytz.IHM
{
    public partial class Form_Pointeuse : Form
    {
        public bool actif;
        public Pointeuse pointeuse = new Pointeuse();
        Pointeuse bean;

        public Form_Pointeuse()
        {
            InitializeComponent();
            Configuration.Load(this);
        }

        public Form_Pointeuse(Pointeuse pointeuse)
        {
            InitializeComponent();
            Configuration.Load(this);
            this.pointeuse = pointeuse;
        }

        private void Form_Infos_Pointeuse_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool update = pointeuse != null ? pointeuse.Id > 0 : false;
            if (update)
            {
                Constantes.FORM_UPD_POINTEUSE = null;
                pointeuse = null;
                Utils.removeFrom("Form_Pointeuse_U");
            }
            else
            {
                Constantes.FORM_ADD_POINTEUSE = null;
                Utils.removeFrom("Form_Pointeuse_I");
            }
            Utils.WriteLog("Fermeture page (" + (update ? "Modification Appareil" : "Ajout Appareil") + ")");
        }

        private void Form_Infos_Pointeuse_Load(object sender, EventArgs e)
        {
            LoadCurrent();
            rbtn_non.Checked = !actif;
            rbtn_oui.Checked = actif;
        }

        public void LoadCurrent()
        {
            if (pointeuse != null)
            {
                txt_description.Text = pointeuse.Description;
                txt_emplacement.Text = pointeuse.Emplacement;
                txt_ip.Text = pointeuse.Ip;
                txt_port.Text = pointeuse.Port.ToString();
                actif = pointeuse.Actif;
                rbtn_multi.Checked = pointeuse.MultiSociete;
            }
        }

        private Pointeuse Pointeuse_()
        {
            return Pointeuse_(0);
        }

        private Pointeuse Pointeuse_(int id)
        {
            Pointeuse bean = new Pointeuse();
            bean.Id = id;
            bean.Connecter = false;
            bean.Description = (txt_description.Text.Trim() != "") ? txt_description.Text.Replace("'", "''") : "";
            bean.Emplacement = txt_emplacement.Text;
            bean.Ip = txt_ip.Text;
            bean.Port = Convert.ToInt32(txt_port.Text.Trim());
            bean.Actif = actif;
            bean.MultiSociete = rbtn_multi.Checked;
            bean.IMachine = 1;
            return bean;
        }

        private void btn_appliquer_Click(object sender, EventArgs e)
        {
            if (txt_ip.Text.Trim() == "")
            {
                Utils.WriteLog("Entrer l'adresse IP svp!");
                return;
            }

            if (pointeuse != null ? pointeuse.Id < 1 : true)
            {
                Utils.WriteLog("Demande d'enregistrement de l'appareil " + txt_ip.Text + "");
                if (Messages.Confirmation_Infos("ajouter") == System.Windows.Forms.DialogResult.Yes)
                {
                    if (PointeuseBLL.Insert(Pointeuse_()))
                    {
                        Utils.WriteLog("-- Enregistrement de l'appareil " + txt_ip.Text + " effectué");
                        Pointeuse bean = PointeuseBLL.OneByIp(Pointeuse_().Ip);
                        Constantes.FORM_PARENT.AddPointeuse(bean);
                        Constantes.POINTEUSES.Add(bean);
                    }
                }
                else
                {
                    Utils.WriteLog("-- Enregistrement de l'appareil " + txt_ip.Text + " annulé");
                }
            }
            else
            {
                string sIP = txt_ip.Text.Trim();
                Pointeuse new_ = PointeuseBLL.OneByIp(sIP);
                if (new_ != null ? (new_.Id > 0 ? (new_.Id.Equals(pointeuse.Id)) : true) : true)
                {
                    Utils.WriteLog("Demande de modification de l'appareil " + pointeuse.Ip + "");
                    if (Messages.Confirmation_Infos("modifier") == System.Windows.Forms.DialogResult.Yes)
                    {
                        bean = Pointeuse_(pointeuse.Id);
                        if (pointeuse.Ip.Equals(sIP))
                        {
                            Utils.WriteLog("-- Modification des informations l'appareil " + pointeuse.Ip + "");
                            if (PointeuseBLL.Update(bean, pointeuse.Id))
                            {
                                Utils.WriteLog("---- Modification des informations l'appareil " + pointeuse.Ip + " effectuée");
                                if (!bean.MultiSociete && !bean.Societe.Equals(Constantes.SOCIETE.Id))
                                    Constantes.FORM_PARENT.DeletePointeuse(bean);
                                else
                                    Constantes.FORM_PARENT.UpdatePointeuse(bean);
                                pointeuse = bean;
                            }
                            else
                            {
                                Utils.WriteLog("---- Modification des informations l'appareil " + pointeuse.Ip + " impossible");
                            }
                        }
                        else
                        {
                            Utils.WriteLog("-- Modification de l'appareil " + pointeuse.Ip + " en " + sIP + "...Patientez svp!");
                            Cursor = Cursors.WaitCursor;
                            Thread t = new Thread(new ThreadStart(Update));
                            t.Start();
                            Cursor = Cursors.Default;
                        }
                    }
                    else
                    {
                        Utils.WriteLog("-- Modification de l'appareil " + pointeuse.Ip + " annulée");
                    }
                }
                else
                {
                    Utils.WriteLog("L'appareil " + sIP + " existe deja");
                }
            }
        }

        public void Update()
        {
            string sIP_a = pointeuse.Ip;
            string sIP = txt_ip.Text.Trim();
            Appareil z = Utils.ReturnAppareil(pointeuse);
            Utils.VerifyZkemkeeper(ref z, ref pointeuse);
            if (z != null)
            {
                int Imachine = 1;

                z.RegEvent(Imachine, 65535);

                z.SetDeviceIP(Imachine, sIP);
                z.RefreshData(Imachine);//the data in the device should be refreshed

                pointeuse.IMachine = Imachine;

                if (PointeuseBLL.Update(bean, pointeuse.Id))
                {
                    Utils.WriteLog("---- Modifier de l'adresse de l'appareil" + sIP_a + " en " + sIP + " effectuée");
                    ObjectThread o = new ObjectThread(this);
                    o.WriteTextForm("Modifier Appareil : " + sIP);
                    Utils.SetZkemkeeper(ref bean);
                    if (!bean.MultiSociete && !bean.Societe.Equals(Constantes.SOCIETE.Id))
                        Constantes.FORM_PARENT.DeletePointeuse(bean);
                    else
                        Constantes.FORM_PARENT.UpdatePointeuse(bean);
                    pointeuse = bean;
                }
                else
                {
                    Utils.WriteLog("---- Modifier de l'adresse de l'appareil" + sIP_a + " en " + sIP + " impossible");
                }
            }
            else
            {
                Utils.WriteLog("-- Modifier de l'adresse de l'appareil" + sIP_a + " en " + sIP + " impossible car connexion à l'appareil impossible");
            }
        }

        private void rbtn_non_CheckedChanged(object sender, EventArgs e)
        {
            actif = false;
        }

        private void rbtn_oui_CheckedChanged(object sender, EventArgs e)
        {
            actif = true;
        }
    }
}
