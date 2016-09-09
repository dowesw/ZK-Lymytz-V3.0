using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Threading;
using System.Windows.Forms;

using ZK_Lymytz.BLL;
using ZK_Lymytz.IHM;
using ZK_Lymytz.TOOLS;
using ZK_Lymytz.ENTITE;

namespace ZK_Lymytz.IHM
{
    public partial class Form_Employe : Form
    {
        Employe employe = new Employe();
        public Pointeuse pointeuse = new Pointeuse();
        public bool all = true;
        Appareil z;
        int fingerID = -1;

        public Form_Employe(Pointeuse pointeuse, bool _all)
        {
            InitializeComponent();
            Configuration.Load(this);
            this.pointeuse = pointeuse;
            this.all = _all;
        }

        private void Form_Employe_FormClosed(object sender, FormClosedEventArgs e)
        {
            Constantes.FORM_EMPLOYE = null;
            Utils.WriteLog("Fermeture page (Gestion Employe)");
            Utils.removeFrom("Form_Employe");
        }

        private void Form_Employe_Load(object sender, EventArgs e)
        {
            VerifyPointeuse();
            ChangeFonction();
        }

        private void _ResetText()
        {
            txt_id.ResetText();
            txt_names.ResetText();
            txt_password.ResetText();
            txt_privilege.ResetText();
            txt_agence.ResetText();
            chk_actif.Checked = false;
            this.box_identity.Image = global::ZK_Lymytz.Properties.Resources.contact;
        }

        private void ChangeFonction()
        {
            _ResetText();
            if (all)
            {
                this.box_connect.Image = global::ZK_Lymytz.Properties.Resources.unconnecte;
                btn_change_fct.Text = "Passer en mode testing";
                z.Deconnect();
                z = null;
                Utils.VerifyZkemkeeper(ref z, ref pointeuse);
                pointeuse.Zkemkeeper = z;

                LoadEmploye();
            }
            else
            {
                this.box_connect.Image = global::ZK_Lymytz.Properties.Resources.connecte;
                btn_change_fct.Text = "Passer en mode gestion";
                pointeuse.Connecter = false;
                Utils.SetZkemkeeper(ref pointeuse);
                z = pointeuse.Zkemkeeper;
                z.StartTestEmpreint();

                ObjectThread o = new ObjectThread(dgv_employe);
                o.ClearDataGridView(true);
                o = new ObjectThread(dgv_empreinte);
                o.ClearDataGridView(true);
            }
            all = !all;
        }

        public void VerifyPointeuse()
        {
            if (pointeuse != null ? pointeuse.Id > 0 : false)
            {
                if (!all)
                {
                    pointeuse.Connecter = false;
                    Utils.SetZkemkeeper(ref pointeuse);
                    z = pointeuse.Zkemkeeper;
                    z.StartTestEmpreint();
                }
                else
                {
                    z = Utils.ReturnAppareil(pointeuse);
                    Utils.VerifyZkemkeeper(ref z, ref pointeuse);
                    pointeuse.Zkemkeeper = z;
                }
                if (z == null)
                {
                    Utils.WriteLog("La liaison avec l'appareil " + pointeuse.Ip + " est corrompue");
                    Constantes.FORM_EMPLOYE = null;
                    this.Dispose();
                }
                this.Text += " (Pointeuse : " + pointeuse.Ip + ")";
            }
            else
            {
                Constantes.FORM_EMPLOYE = null;
                this.Dispose();
            }
        }

        private void ResetEmploye()
        {
            for (int i = 0; i < dgv_employe.Rows.Count; i++)
            {
                dgv_employe.Rows[i].Selected = false;
            }

            employe = new Employe();
            _ResetText();
            ResetEmpreinte();
        }

        private void ResetEmpreinte()
        {
            fingerID = -1;
            ObjectThread o = new ObjectThread(dgv_empreinte);
            o.ClearDataGridView(true);
        }

        private void LoadEmploye()
        {
            ObjectThread o = new ObjectThread(dgv_employe);
            o.ClearDataGridView(true);

            int iEnrollNumber = 0;
            int iEMachineNumber = 0;
            int iBackupNumber = 0;
            int iPrivilege = 0;
            int iEnabled = 0;

            z.EnableDevice(pointeuse.IMachine, false);
            z.ReadAllUserID(pointeuse.IMachine);//read all the user information to the memory
            while (z.GetAllUserID(pointeuse.IMachine, ref iEnrollNumber, ref iEMachineNumber, ref iBackupNumber, ref iPrivilege, ref iEnabled))
            {
                Employe e = EmployeBLL.OneById(iEnrollNumber);
                if (e != null ? e.Id < 1 : true)
                {
                    e = new Employe(iEnrollNumber, iEnrollNumber.ToString(), "");
                }
                o.WriteDataGridView(new object[] { e.Id, e.NomPrenom });
            }
            z.EnableDevice(pointeuse.IMachine, true);
            ResetEmploye();
        }

        private void LoadOnView(Employe e)
        {
            txt_id.Text = e.Id.ToString();
            txt_names.Text = e.NomPrenom;
            txt_password.Text = e.Password;
            txt_privilege.Text = e.Privilege.ToString();
            chk_actif.Checked = e.BEnabled;

            LaodPhoto();
        }

        private Employe RecopieView()
        {
            Employe e = new Employe();
            e.Id = Convert.ToInt32(txt_id.Text);
            e.Nom = txt_names.Text;
            e.Password = txt_password.Text;
            e.Privilege = Convert.ToInt32(txt_privilege.Text);
            e.BEnabled = chk_actif.Checked;
            return e;
        }

        private void LoadInfos()
        {
            if (z.EnableDevice(pointeuse.IMachine, false))
            {
                Cursor c = Cursors.WaitCursor;
                if (z.ReadAllTemplate(pointeuse.IMachine))
                {
                    string names = "";
                    string password = "";
                    int privilege = 0;
                    bool bEnabled = false;

                    if (z.GetUserInfo(pointeuse.IMachine, (int)employe.Id, ref names, ref password, ref privilege, ref bEnabled))//upload user information to the memory
                    {
                        employe.Nom = names;
                        employe.Password = password;
                        employe.Privilege = privilege;
                        employe.BEnabled = bEnabled;

                        LoadOnView(employe);
                    }
                }
                z.RefreshData(pointeuse.IMachine);//the data in the device should be refreshed
                z.EnableDevice(pointeuse.IMachine, true);
                c = Cursors.Default;
            }
        }

        private void LaodPhoto()
        {
            Employe e = EmployeBLL.OneById((int)employe.Id);
            string path = Constantes.SETTING.CheminPhoto + e.Photo;
            if (File.Exists(path))
            {
                this.box_identity.Image = new Bitmap(path);
                txt_agence.Text = e.Agence.Name;
            }
            else
            {
                this.box_identity.Image = global::ZK_Lymytz.Properties.Resources.contact;
            }
        }

        private void LoadEmpreinte()
        {
            if (z.EnableDevice(pointeuse.IMachine, false))
            {
                Cursor c = Cursors.WaitCursor;
                if (z.ReadAllTemplate(pointeuse.IMachine))
                {
                    string sTemplate = "";
                    int longTmps = 0;
                    int flag_ = 0;
                    ObjectThread o = new ObjectThread(dgv_empreinte);

                    foreach (Finger f in Utils.Fingers())
                    {
                        if (z.GetUserTmpExStr(pointeuse.IMachine, employe.Id.ToString(), f.Index, out flag_, out sTemplate, out longTmps))
                        {
                            o.WriteDataGridView(new object[] { f.Index, f.Main, f.Doigt, flag_ });
                        }
                    }
                }
                z.RefreshData(pointeuse.IMachine);//the data in the device should be refreshed
                z.EnableDevice(pointeuse.IMachine, true);
                c = Cursors.Default;
            }
        }

        private void dgv_employe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_employe.CurrentRow.Cells["id"].Value != null)
                {
                    int id = Convert.ToInt32(dgv_employe.CurrentRow.Cells["id"].Value);
                    ResetEmpreinte();
                    if (id > 0)
                    {
                        string name = dgv_employe.CurrentRow.Cells["nom"].Value.ToString();
                        employe = new Employe(id, name);
                        LoadEmpreinte();
                        LoadInfos();
                    }
                    else
                    {
                        ResetEmploye();
                    }
                    if (e.ColumnIndex == 2)
                    {
                        Utils.WriteLog("Demande de la suppression de l'employe " + employe.Id + " [" + employe.Nom + "]");
                        if (DialogResult.Yes == Messages.Confirmation(Mots.Supprimer))
                        {
                            Thread t = new Thread(new ThreadStart(DeleteUser));
                            t.Start();
                        }
                        else
                        {
                            Utils.WriteLog("Suppression de l'employe " + employe.Id + " [" + employe.Nom + "] annulée");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Employe (dgv_employe_CellContentClick) ", ex);
            }
        }

        private void DeleteUser()
        {
            if (employe != null ? employe.Id > 0 : false)
            {
                if (z.EnableDevice(pointeuse.IMachine, false))
                {
                    Cursor c = Cursors.WaitCursor;
                    if (z.DeleteUserInfoEx(pointeuse.IMachine, (int)employe.Id))//upload user information to the memory
                    {
                        Utils.WriteLog("Supppression de l'employe " + employe.Id + " [" + employe.Nom + "] effectuée");
                        ObjectThread o = new ObjectThread(dgv_employe);
                        o.RemoveDataGridView(Utils.GetRowData(dgv_employe, employe.Id));
                    }
                    else
                    {
                        Utils.WriteLog("Supppression de l'employe " + employe.Id + " [" + employe.Nom + "] impossible");
                    }
                    z.RefreshData(pointeuse.IMachine);//the data in the device should be refreshed
                    z.EnableDevice(pointeuse.IMachine, true);
                    c = Cursors.Default;
                }
                else
                {
                    Utils.WriteLog("Supppression de l'employe " + employe.Id + " [" + employe.Nom + "] impossible");
                }
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            employe = RecopieView();
            if (employe != null ? employe.Id > 0 : false)
            {
                Utils.WriteLog("Demande de la modification des informations de l'employe " + employe.Id + " [" + employe.Nom + "]");
                if (Messages.Confirmation_Infos("enregistrer") == System.Windows.Forms.DialogResult.Yes)
                {
                    Thread t = new Thread(new ThreadStart(Update));
                    t.Start();
                }
                else
                {
                    Utils.WriteLog("-- Modification des informations de l'employe " + employe.Id + " [" + employe.Nom + "] annulée");
                }
            }
            else
            {
                Utils.WriteLog("Vous devez selectionner l'employé");
            }
        }

        public void Update()
        {
            if (z.EnableDevice(pointeuse.IMachine, false))
            {
                Cursor c = Cursors.WaitCursor;
                if (z.ReadAllTemplate(pointeuse.IMachine))
                {
                    if (z.SetUserInfo(pointeuse.IMachine, (int)employe.Id, employe.Nom, employe.Password, employe.Privilege, employe.BEnabled))//upload user information to the memory
                    {
                        Utils.WriteLog("-- Modification des informations de l'employe " + employe.Id + " [" + employe.Nom + "] effectuée");
                    }
                    else
                    {
                        Utils.WriteLog("-- Modification des informations de l'employe " + employe.Id + " [" + employe.Nom + "] impossible");
                    }
                }
                else
                {
                    Utils.WriteLog("-- Modification des informations de l'employe " + employe.Id + " [" + employe.Nom + "] impossible");
                }
                z.RefreshData(pointeuse.IMachine);//the data in the device should be refreshed
                z.EnableDevice(pointeuse.IMachine, true);
                c = Cursors.Default;
            }
            else
            {
                Utils.WriteLog("-- Modification des informations de l'employe " + employe.Id + " [" + employe.Nom + "] impossible");
            }
        }

        private void dgv_empreinte_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv_empreinte.CurrentRow.Cells["finger"].Value != null)
                {
                    fingerID = Convert.ToInt32(dgv_empreinte.CurrentRow.Cells["finger"].Value);
                    if (fingerID > -1)
                    {
                        if (e.ColumnIndex == 4)
                        {
                            Utils.WriteLog("Demande de la suppression de l'empreinte de l'employe " + employe.Id + " [" + employe.Nom + "]");
                            if (DialogResult.Yes == Messages.Confirmation(Mots.Supprimer))
                            {
                                Thread t = new Thread(new ThreadStart(DeleteTmp));
                                t.Start();
                            }
                            else
                            {
                                Utils.WriteLog("-- Suppression de l'empreinte de l'employe " + employe.Id + " [" + employe.Nom + "] annulée");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Employe (dgv_empreinte_CellContentClick) ", ex);
            }
        }

        private void DeleteTmp()
        {
            if (fingerID > -1)
            {
                if (z.EnableDevice(pointeuse.IMachine, false))
                {
                    Cursor c = Cursors.WaitCursor;
                    if (z.ReadAllTemplate(pointeuse.IMachine))
                    {
                        if (z.DelUserTmp(pointeuse.IMachine, (int)employe.Id, fingerID))//upload user information to the memory
                        {
                            Utils.WriteLog("-- Supppression de l'empreinte de l'employe " + employe.Id + " [" + employe.Nom + "] effectuée");
                            ObjectThread o = new ObjectThread(dgv_empreinte);
                            o.RemoveDataGridView(Utils.GetRowData(dgv_empreinte, (long)fingerID));
                        }
                        else
                        {
                            Utils.WriteLog("-- Supppression de l'empreinte de l'employe " + employe.Id + " [" + employe.Nom + "] impossible");
                        }
                    }
                    else
                    {
                        Utils.WriteLog("-- Supppression de l'empreinte de l'employe " + employe.Id + " [" + employe.Nom + "] impossible");
                    }
                    z.RefreshData(pointeuse.IMachine);//the data in the device should be refreshed
                    z.EnableDevice(pointeuse.IMachine, true);
                    c = Cursors.Default;
                }
                else
                {
                    Utils.WriteLog("-- Supppression de l'empreinte de l'employe " + employe.Id + " [" + employe.Nom + "] impossible");
                }
            }
        }

        public void LoadInfosEmploye(Employe e)
        {
            if (e != null ? e.Id > 0 : false)
            {
                ObjectThread o = new ObjectThread(dgv_employe);
                o.ClearDataGridView(true);
                o.WriteDataGridView(new object[] { e.Id, e.NomPrenom });
                z.EnableDevice(pointeuse.IMachine, true);
                ResetEmploye();

                employe = e;
                LoadEmpreinte();
                LoadInfos();
            }
        }

        private void btn_change_fct_Click(object sender, EventArgs e)
        {
            ChangeFonction();
        }
    }
}
