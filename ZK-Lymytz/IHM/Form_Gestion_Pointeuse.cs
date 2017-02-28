using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZK_Lymytz.BLL;
using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;

namespace ZK_Lymytz.IHM
{
    public partial class Form_Gestion_Pointeuse : Form
    {
        public Pointeuse pointeuse = new Pointeuse();
        Appareil z;

        public Form_Gestion_Pointeuse(Pointeuse pointeuse)
        {
            InitializeComponent();
            Configuration.Load(this);
            this.pointeuse = pointeuse;
        }

        private void Form_Gestion_Pointeuse_FormClosed(object sender, FormClosedEventArgs e)
        {
            Constantes.FORM_GESTION_POINTEUSE = null;
            Utils.WriteLog("Fermeture page (Gestion Pointeuse)");
            Utils.removeFrom("Form_Gestion_Pointeuse");
        }

        private void Form_Gestion_Pointeuse_Load(object sender, EventArgs e)
        {
            if (pointeuse != null ? pointeuse.Id > 0 : false)
            {
                LoadCurrent();
                LoadDateTime();
                txt_minute.Value = 60;
            }
            else
            {
                Constantes.FORM_GESTION_POINTEUSE = null;
                this.Dispose();
            }
        }

        public void LoadCurrent()
        {
            this.Text = "(" + pointeuse.Ip + ")";
            z = Utils.ReturnAppareil(pointeuse);
            Utils.VerifyZkemkeeper(ref z, ref pointeuse);
            if (z == null)
            {
                Constantes.FORM_GESTION_POINTEUSE = null;
                this.Dispose();
            }
        }

        public void LoadDateTime()
        {
            int idwYear = 0;
            int idwMonth = 0;
            int idwDay = 0;
            int idwHour = 0;
            int idwMinute = 0;
            int idwSecond = 0;
            Cursor = Cursors.WaitCursor;
            if (z.GetDeviceTime(pointeuse.IMachine, ref idwYear, ref idwMonth, ref idwDay, ref idwHour, ref idwMinute, ref idwSecond))
            {
                DateTime date = new DateTime(idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond);
                dtp_date.Value = date;
                dtp_heure.Value = date;
            }
            else
            {
                Utils.WriteLog("Retour date & heure de l'appareil " + pointeuse.Ip+ " impossible");
            }
            Cursor = Cursors.Default;
        }

        private void btn_reset_time_Click(object sender, EventArgs e)
        {
            if (z == null)
            {
                Utils.WriteLog("La liaison avec l'appareil " + pointeuse.Ip + " est corrompue");
                return;
            }
            Utils.WriteLog("Demande de la reinitialisation de date & heure de l'appareil " + pointeuse.Ip);
            if (Messages.Confirmation("reinitialiser la date & l'heure de l'appareil") == System.Windows.Forms.DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;
                if (z.SetDeviceTime(pointeuse.IMachine))
                {
                    Utils.WriteLog("-- Reinitialisation date & heure de l'appareil " + pointeuse.Ip + " effectuée");
                    z.RefreshData(pointeuse.IMachine);
                    LoadDateTime();
                }
                else
                {
                    Utils.WriteLog("-- Reinitialisation date & heure de l'appareil " + pointeuse.Ip + " impossible");
                }
                Cursor = Cursors.Default;
            }
            else
            {
                Utils.WriteLog("-- Reinitialisation date & heure de l'appareil " + pointeuse.Ip + " annulée");
            }
        }

        private void btn_save_time_Click(object sender, EventArgs e)
        {
            if (z == null)
            {
                Utils.WriteLog("La liaison avec l'appareil " + pointeuse.Ip + " est corrompue");
                return;
            }
            DateTime date = dtp_date.Value;
            DateTime heure = dtp_heure.Value;

            int idwYear = date.Year;
            int idwMonth = date.Month;
            int idwDay = date.Day;
            int idwHour = heure.Hour;
            int idwMinute = heure.Minute;
            int idwSecond = heure.Second;

            DateTime d = new DateTime(idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond);

            Utils.WriteLog("Demande de la modification de date & heure de l'appareil " + pointeuse.Ip + " en " + d);
            if (Messages.Confirmation("modifier la date & l'heure de l'appareil") == System.Windows.Forms.DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;
                if (z.SetDeviceTime(pointeuse.IMachine, idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond))
                {
                    z.RefreshData(pointeuse.IMachine);//the data in the device should be refreshed
                    Utils.WriteLog("-- Modification date & heure de l'appareil " + pointeuse.Ip + " effectuée en " + d);
                }
                else
                {
                    Utils.WriteLog("-- Modification date & heure de l'appareil " + pointeuse.Ip + " en " + d + " impossible");
                }
                Cursor = Cursors.Default;
            }
            else
            {
                Utils.WriteLog("-- Modification date & heure de l'appareil " + pointeuse.Ip + " annulée");
            }
        }

        private void txt_minute_ValueChanged(object sender, EventArgs e)
        {
            btn_disable_min.Text = "Désactiver (" + txt_minute.Value + " seconde(s))";
        }

        private void btn_disable_min_Click(object sender, EventArgs e)
        {
            if (z == null)
            {
                Utils.WriteLog("La liaison avec l'appareil " + pointeuse.Ip + " est corrompue");
                return;
            }
            Utils.WriteLog("Demande de la désactivation de l'appareil " + pointeuse.Ip + " pour " + txt_minute.Value + " seconde(s)");
            if (Messages.Confirmation("désactivation l'appareil") == System.Windows.Forms.DialogResult.Yes)
            {
                if (z.DisableDeviceWithTimeOut(pointeuse.IMachine, (int)txt_minute.Value))
                {
                    z.RefreshData(pointeuse.IMachine);//the data in the device should be refreshed
                    Utils.WriteLog("-- Desactivation de l'appareil " + pointeuse.Ip + " pour " + txt_minute.Value + " seconde(s) effectuée");
                }
                else
                {
                    Utils.WriteLog("-- Desactivation de l'appareil " + pointeuse.Ip + " pour " + txt_minute.Value + " seconde(s) impossible");
                }
            }
            else
            {
                Utils.WriteLog("-- Desactivation de l'appareil " + pointeuse.Ip + " pour " + txt_minute.Value + " seconde(s) annulée");
            }
        }

        private void btn_restart_Click(object sender, EventArgs e)
        {
            if (z == null)
            {
                Utils.WriteLog("La liaison avec l'appareil " + pointeuse.Ip + " est corrompue");
                return;
            }
            Utils.WriteLog("Demande du redemarrage de l'appareil " + pointeuse.Ip);
            if (Messages.Confirmation("redemarrer l'appareil") == System.Windows.Forms.DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;
                if (z.Restart(pointeuse.IMachine) == true)
                {
                    Utils.WriteLog("-- Redemarrage de l'appareil " + pointeuse.Ip + " effectuée");
                    z = null;
                    Utils.DestroyZkemkeeper(pointeuse);
                }
                else
                {
                    Utils.WriteLog("-- Redemarrage de l'appareil " + pointeuse.Ip + " impossible");
                }
                Cursor = Cursors.Default;
            }
            else
            {
                Utils.WriteLog("-- Redemarrage de l'appareil " + pointeuse.Ip + " annulée");
            }
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            if (z == null)
            {
                Utils.WriteLog("La liaison avec l'appareil " + pointeuse.Ip + " est corrompue");
                return;
            }
            Utils.WriteLog("Demande de l'arrêt de l'appareil " + pointeuse.Ip);
            if (Messages.Confirmation("arrêter l'appareil") == System.Windows.Forms.DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;
                if (z.Stop(pointeuse.IMachine) == true)
                {
                    Utils.WriteLog("-- Arrêt de l'appareil " + pointeuse.Ip + " effectuée");
                    z = null;
                    Utils.DestroyZkemkeeper(pointeuse);
                }
                else
                {
                    Utils.WriteLog("-- Arrêt de l'appareil " + pointeuse.Ip + " impossible");
                }
                Cursor = Cursors.Default;
            }
            else
            {
                Utils.WriteLog("-- Arrêt de l'appareil " + pointeuse.Ip + " annulée");
            }
        }

        private void btn_del_log_Click(object sender, EventArgs e)
        {
            if (z == null)
            {
                Utils.WriteLog("La liaison avec l'appareil " + pointeuse.Ip + " est corrompue");
                return;
            }
            Utils.WriteLog("Demande de la suppression des entrées et sorties de l'appareil " + pointeuse.Ip);
            if (Messages.Confirmation("supprimer des entrées et sorties de l'appareil") == System.Windows.Forms.DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;
                z.EnableDevice(pointeuse.IMachine, false);
                if (z.ClearGLog(pointeuse.IMachine))
                {
                    Utils.WriteLog("-- Suppression des entrées et sorties de l'appareil " + pointeuse.Ip + " effectuée");
                    z.RefreshData(pointeuse.IMachine);
                }
                else
                {
                    Utils.WriteLog("-- Suppression des entrées et sorties de l'appareil " + pointeuse.Ip + " impossible");
                }
                z.EnableDevice(pointeuse.IMachine, true);
                Cursor = Cursors.Default;
            }
            else
            {
                Utils.WriteLog("-- Suppression des entrées et sorties de l'appareil " + pointeuse.Ip + " annulée");
            }
        }

        private void btn_del_tmp_Click(object sender, EventArgs e)
        {
            if (z == null)
            {
                Utils.WriteLog("La liaison avec l'appareil " + pointeuse.Ip + " est corrompue");
                return;
            }
            Utils.WriteLog("Demande de la suppression des empreintes de l'appareil " + pointeuse.Ip);
            if (Messages.Confirmation("supprimer des empreintes de l'appareil") == System.Windows.Forms.DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;
                z.EnableDevice(pointeuse.IMachine, false);
                if (z.ClearTmp(pointeuse.IMachine))
                {
                    Utils.WriteLog("-- Suppression des empreintes de l'appareil " + pointeuse.Ip + " effectuée");
                    z.RefreshData(pointeuse.IMachine);
                }
                else
                {
                    Utils.WriteLog("-- Suppression des empreintes de l'appareil " + pointeuse.Ip + " impossible");
                }
                z.EnableDevice(pointeuse.IMachine, true);
                Cursor = Cursors.Default;
            }
            else
            {
                Utils.WriteLog("-- Suppression des empreintes de l'appareil " + pointeuse.Ip + " annulée");
            }
        }

        private void btn_del_user_Click(object sender, EventArgs e)
        {
            if (z == null)
            {
                Utils.WriteLog("La liaison avec l'appareil " + pointeuse.Ip + " est corrompue");
                return;
            }
            Utils.WriteLog("Demande de la suppression des employés de l'appareil " + pointeuse.Ip);
            if (Messages.Confirmation("supprimer des employés de l'appareil") == System.Windows.Forms.DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;
                z.EnableDevice(pointeuse.IMachine, false);
                if (z.ClearUsers(pointeuse.IMachine))
                {
                    Utils.WriteLog("-- Suppression des employés de l'appareil " + pointeuse.Ip + " effectuée");
                    z.RefreshData(pointeuse.IMachine);
                }
                else
                {
                    Utils.WriteLog("-- Suppression des employés de l'appareil " + pointeuse.Ip + " impossible");
                }
                z.EnableDevice(pointeuse.IMachine, true);
                Cursor = Cursors.Default;
            }
            else
            {
                Utils.WriteLog("-- Suppression des employés de l'appareil " + pointeuse.Ip + " annulée");
            }
        }

        private void btn_del_admin_Click(object sender, EventArgs e)
        {
            if (z == null)
            {
                Utils.WriteLog("La liaison avec l'appareil " + pointeuse.Ip + " est corrompue");
                return;
            }
            Utils.WriteLog("Demande de la suppression des administrateurs de l'appareil " + pointeuse.Ip);
            if (Messages.Confirmation("supprimer des administrateurs de l'appareil") == System.Windows.Forms.DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;
                z.EnableDevice(pointeuse.IMachine, false);
                if (z.ClearAdministrators(pointeuse.IMachine))
                {
                    Utils.WriteLog("-- Suppression des administrateurs de l'appareil " + pointeuse.Ip + " effectuée");
                    z.RefreshData(pointeuse.IMachine);
                }
                else
                {
                    Utils.WriteLog("-- Suppression des administrateurs de l'appareil " + pointeuse.Ip + " impossible");
                }
                z.EnableDevice(pointeuse.IMachine, true);
                Cursor = Cursors.Default;
            }
            else
            {
                Utils.WriteLog("-- Suppression des administrateurs de l'appareil " + pointeuse.Ip + " annulée");
            }
        }

        private void btn_test_Click(object sender, EventArgs e)
        {
            if (z == null)
            {
                Utils.WriteLog("La liaison avec l'appareil " + pointeuse.Ip + " est corrompue");
                return;
            }
            Utils.WriteLog("Test appareil " + pointeuse.Ip + " modification du LCD ('OK' sur ecran)");
            z.ClearLCD(true);
            z.WriteLCD(true, 2, 7, "OK");
        }

        private void btnGetDeviceStrInfo_Click(object sender, EventArgs e)
        {
            if (z == null)
            {
                Utils.WriteLog("La liaison avec l'appareil " + pointeuse.Ip + " est corrompue");
                return;
            }
            int idwInfo = 1;//the only possible value
            string sValue = "";
            Cursor = Cursors.WaitCursor;
            if (z.GetDeviceStrInfo(pointeuse.IMachine, idwInfo, out sValue))
            {
                txt_valeur.Text = sValue;
            }
            else
            {
                Utils.WriteLog("-- Lecture des information de l'appareil " + pointeuse.Ip + " impossible");
            }
            Cursor = Cursors.Default;
        }

        private void btnGetDeviceMAC_Click(object sender, EventArgs e)
        {
            if (z == null)
            {
                Utils.WriteLog("La liaison avec l'appareil " + pointeuse.Ip + " est corrompue");
                return;
            }
            string sMAC = "";
            Cursor = Cursors.WaitCursor;
            if (z.GetDeviceMAC(pointeuse.IMachine, ref sMAC))
            {
                txt_valeur.Text = sMAC;
            }
            else
            {
                Utils.WriteLog("-- Lecture de l'adresse MAC de l'appareil " + pointeuse.Ip + " impossible");
            }
            Cursor = Cursors.Default;
        }

        private void btnGetDeviceIP_Click(object sender, EventArgs e)
        {
            if (z == null)
            {
                Utils.WriteLog("La liaison avec l'appareil " + pointeuse.Ip + " est corrompue");
                return;
            }
            string sIP = "";
            Cursor = Cursors.WaitCursor;
            if (z.GetDeviceIP(pointeuse.IMachine, ref sIP))
            {
                txt_valeur.Text = sIP;
            }
            else
            {
                Utils.WriteLog("-- Lecture de l'adresse IP de l'appareil " + pointeuse.Ip + " impossible");
            }
            Cursor = Cursors.Default;
        }

        private void btnGetSerialNumber_Click(object sender, EventArgs e)
        {
            if (z == null)
            {
                Utils.WriteLog("La liaison avec l'appareil " + pointeuse.Ip + " est corrompue");
                return;
            }
            string sdwSerialNumber = "";
            Cursor = Cursors.WaitCursor;
            if (z.GetSerialNumber(pointeuse.IMachine, out sdwSerialNumber))
            {
                txt_valeur.Text = sdwSerialNumber;
            }
            else
            {
                Utils.WriteLog("-- Lecture du numéro de serie de l'appareil " + pointeuse.Ip + " impossible");
            }
            Cursor = Cursors.Default;
        }

        private void btnGetSysOption_Click(object sender, EventArgs e)
        {
            if (z == null)
            {
                Utils.WriteLog("La liaison avec l'appareil " + pointeuse.Ip + " est corrompue");
                return;
            }
            string sOption = "~PIN2Width";//You should input this parameter by yourself . 
            string sValue = "";

            Cursor = Cursors.WaitCursor;
            if (z.GetSysOption(pointeuse.IMachine, sOption, out sValue))
            {
                txt_valeur.Text = sValue;
            }
            else
            {
                Utils.WriteLog("-- Lecture des options systèmes de l'appareil " + pointeuse.Ip + " impossible");
            }
            Cursor = Cursors.Default;
        }

        private void btnGetProductCode_Click(object sender, EventArgs e)
        {
            if (z == null)
            {
                Utils.WriteLog("La liaison avec l'appareil " + pointeuse.Ip + " est corrompue");
                return;
            }
            string sProductCode = "";
            Cursor = Cursors.WaitCursor;
            if (z.GetProductCode(pointeuse.IMachine, out sProductCode))
            {
                txt_valeur.Text = sProductCode;
            }
            else
            {
                Utils.WriteLog("-- Lecture du code du produit de l'appareil " + pointeuse.Ip + " impossible");
            }
            Cursor = Cursors.Default;
        }

        private void btnGetFirmwareVersion_Click(object sender, EventArgs e)
        {
            if (z == null)
            {
                Utils.WriteLog("La liaison avec l'appareil " + pointeuse.Ip + " est corrompue");
                return;
            }
            string sVersion = "";
            Cursor = Cursors.WaitCursor;
            if (z.GetFirmwareVersion(pointeuse.IMachine, ref sVersion))
            {
                txt_valeur.Text = sVersion;
            }
            else
            {
                Utils.WriteLog("-- Lecture de la version du produit de l'appareil " + pointeuse.Ip + " impossible");
            }
            Cursor = Cursors.Default;
        }

        private void btnGetPlatform_Click(object sender, EventArgs e)
        {
            if (z == null)
            {
                Utils.WriteLog("La liaison avec l'appareil " + pointeuse.Ip + " est corrompue");
                return;
            }
            string sPlatform = "";

            Cursor = Cursors.WaitCursor;
            if (z.GetPlatform(pointeuse.IMachine, ref sPlatform))
            {
                txt_valeur.Text = sPlatform;
            }
            else
            {
                Utils.WriteLog("-- Lecture de la platforme de l'appareil " + pointeuse.Ip + " impossible");
            }
            Cursor = Cursors.Default;
        }

        private void btnGetCardFun_Click(object sender, EventArgs e)
        {
            if (z == null)
            {
                Utils.WriteLog("La liaison avec l'appareil " + pointeuse.Ip + " est corrompue");
                return;
            }
            int iCardFun = 0;

            Cursor = Cursors.WaitCursor;
            if (z.GetCardFun(pointeuse.IMachine, ref iCardFun))
            {
                txt_valeur.Text = iCardFun.ToString();
            }
            else
            {
                Utils.WriteLog("-- Lecture du lecteur de carte de l'appareil " + pointeuse.Ip + " impossible");
            }
            Cursor = Cursors.Default;
        }

        private void btnGetSDKVersion_Click(object sender, EventArgs e)
        {
            if (z == null)
            {
                Utils.WriteLog("La liaison avec l'appareil " + pointeuse.Ip + " est corrompue");
                return;
            }
            string sVersion = "";

            Cursor = Cursors.WaitCursor;
            if (z.GetSDKVersion(ref sVersion))
            {
                txt_valeur.Text = sVersion;
            }
            else
            {
                Utils.WriteLog("-- Lecture de la version de la SDK de l'appareil " + pointeuse.Ip + " impossible");
            }
            Cursor = Cursors.Default;
        }

        private void btnQueryState_Click(object sender, EventArgs e)
        {
            if (z == null)
            {
                Utils.WriteLog("La liaison avec l'appareil " + pointeuse.Ip + " est corrompue");
                return;
            }
            int iState = 0;

            Cursor = Cursors.WaitCursor;
            if (z.QueryState(ref iState))
            {
                txt_valeur.Text = iState.ToString();
            }
            else
            {
                Utils.WriteLog("-- Lecture du statut des requetes de l'appareil " + pointeuse.Ip + " impossible");
            }
            Cursor = Cursors.Default;
        }

        private void btnGetVendor_Click(object sender, EventArgs e)
        {
            if (z == null)
            {
                Utils.WriteLog("La liaison avec l'appareil " + pointeuse.Ip + " est corrompue");
                return;
            }
            string sVendor = "";

            Cursor = Cursors.WaitCursor;
            if (z.GetVendor(ref sVendor))
            {
                txt_valeur.Text = sVendor;
            }
            else
            {
                Utils.WriteLog("-- Lecture du concepteur de l'appareil " + pointeuse.Ip + " impossible");
            }
            Cursor = Cursors.Default;
        }
    }
}
