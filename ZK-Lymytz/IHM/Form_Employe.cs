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
        List<Employe> employes = new List<Employe>();
        Employe employe = new Employe();
        Employe select = new Employe();
        public Pointeuse pointeuse = new Pointeuse();
        public bool vue_all_employe = true;
        Appareil appareil;
        int fingerID = -1;

        ObjectThread object_employe;
        ObjectThread object_empreinte;

        public Form_Employe(Pointeuse pointeuse, bool vue_all_employe)
        {
            InitializeComponent();
            Configuration.Load(this);
            object_employe = new ObjectThread(dgv_employe);
            object_empreinte = new ObjectThread(dgv_empreinte);

            this.pointeuse = pointeuse;
            this.vue_all_employe = vue_all_employe;
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
            Exit();

            txt_id.ResetText();
            txt_names.ResetText();
            txt_password.ResetText();
            txt_privilege.ResetText();
            txt_agence.ResetText();
            lb_nom_prenom.Text = "---";
            chk_actif.Checked = false;
            this.box_identity.Image = global::ZK_Lymytz.Properties.Resources.contact;
        }

        private void ChangeFonction()
        {
            _ResetText();
            if (vue_all_employe)
            {
                this.box_connect.Image = global::ZK_Lymytz.Properties.Resources.unconnecte;
                btn_change_fct.Text = "Passer en mode testing";
                appareil.OnEmpreintTesting(false);
                LoadEmploye();
            }
            else
            {
                this.box_connect.Image = global::ZK_Lymytz.Properties.Resources.connecte;
                btn_change_fct.Text = "Passer en mode gestion";
                appareil.OnEmpreintTesting(true);

                object_employe.ClearDataGridView(true);
                object_empreinte.ClearDataGridView(true);
            }
            vue_all_employe = !vue_all_employe;
        }

        public void VerifyPointeuse()
        {
            if (pointeuse != null ? pointeuse.Id > 0 : false)
            {
                appareil = Utils.ReturnAppareil(pointeuse);
                if (appareil == null)
                {
                    Utils.SetZkemkeeper(ref pointeuse);
                    appareil = pointeuse.Zkemkeeper;
                }
                Exit();
                this.Text += " (Pointeuse : " + pointeuse.Ip + ")";
            }
            else
            {
                Constantes.FORM_EMPLOYE = null;
                this.Dispose();
            }
        }

        private void Exit()
        {
            if (appareil == null)
            {
                Utils.WriteLog("La liaison avec l'appareil " + pointeuse.Ip + " est corrompue");
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
            object_empreinte.ClearDataGridView(true);
        }

        private void LoadEmploye()
        {
            object_employe.ClearDataGridView(true);
            employes.Clear();

            appareil.EnableDevice(pointeuse.IMachine, false);
            appareil.ReadAllUserID(pointeuse.IMachine);//read all the user information to the memory
            switch (pointeuse.Type)
            {
                case Constantes.TYPE_IFACE:
                    {
                        string iEnrollNumber = "";
                        string iName = "";
                        int iPrivilege = 0;
                        string iPassword = "";
                        bool iEnabled = false;
                        while (appareil.SSR_GetAllUserInfo(pointeuse.IMachine, out iEnrollNumber, out iName, out iPassword, out iPrivilege, out iEnabled))
                        {
                            Employe e = new Employe(Convert.ToInt32(iEnrollNumber), iEnrollNumber, "");
                            e.Nom = iName;
                            e.Password = iPassword;
                            e.Privilege = iPrivilege;
                            e.BEnabled = iEnabled;

                            object_employe.WriteDataGridView(new object[] { e.Id, e.NomPrenom, e.IsPrivilege });
                            employes.Add(e);
                        }
                        break;
                    }
                default:
                    {
                        int iEnrollNumber = 0;
                        int iPrivilege = 0;
                        int iEMachineNumber = 0;
                        int iBackupNumber = 0;
                        int iEnabled = 0;

                        while (appareil.GetAllUserID(pointeuse.IMachine, ref iEnrollNumber, ref iEMachineNumber, ref iBackupNumber, ref iPrivilege, ref iEnabled))
                        {
                            Employe e = EmployeBLL.OneById(iEnrollNumber);
                            if (e != null ? e.Id < 1 : true)
                            {
                                e = new Employe(iEnrollNumber, iEnrollNumber.ToString(), "");
                            }
                            object_employe.WriteDataGridView(new object[] { e.Id, e.NomPrenom, e.IsPrivilege });
                        }
                        break;
                    }
            }
            appareil.EnableDevice(pointeuse.IMachine, true);
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
            switch (pointeuse.Type)
            {
                case Constantes.TYPE_IFACE:
                    {
                        int idx = employes.FindIndex(x => x.Id == employe.Id);
                        if (idx > -1)
                        {
                            employe = employes[idx];
                            LoadOnView(employe);
                        }
                        break;
                    }
                default:
                    {
                        if (appareil.EnableDevice(pointeuse.IMachine, false))
                        {
                            Cursor c = Cursors.WaitCursor;
                            if (appareil.ReadAllTemplate(pointeuse.IMachine))
                            {
                                string names = "";
                                string password = "";
                                int privilege = 0;
                                bool bEnabled = false;

                                if (appareil.GetUserInfo(pointeuse.IMachine, (int)employe.Id, ref names, ref password, ref privilege, ref bEnabled))//upload user information to the memory
                                {
                                    employe.Nom = names;
                                    employe.Password = password;
                                    employe.Privilege = privilege;
                                    employe.BEnabled = bEnabled;

                                    LoadOnView(employe);
                                }
                            }
                            appareil.RefreshData(pointeuse.IMachine);//the data in the device should be refreshed
                            appareil.EnableDevice(pointeuse.IMachine, true);
                            c = Cursors.Default;
                        }
                        break;
                    }
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
            lb_nom_prenom.Text = e.NomPrenom;
        }

        private void LoadEmpreinte()
        {
            if (appareil.EnableDevice(pointeuse.IMachine, false))
            {
                Cursor c = Cursors.WaitCursor;
                if (appareil.ReadAllTemplate(pointeuse.IMachine))
                {
                    string sTemplate = "";
                    int longTmps = 0;
                    int flag_ = 0;

                    foreach (Finger f in Utils.Fingers())
                    {
                        if (appareil.GetUserTmpExStr(pointeuse.IMachine, employe.Id.ToString(), f.Index, out flag_, out sTemplate, out longTmps))
                        {
                            object_empreinte.WriteDataGridView(new object[] { f.Index, f.Main, f.Doigt, flag_ });
                        }
                    }
                }
                appareil.RefreshData(pointeuse.IMachine);//the data in the device should be refreshed
                appareil.EnableDevice(pointeuse.IMachine, true);
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

        private void dgv_employe_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                DataGridView.HitTestInfo info = dgv_employe.HitTest(e.X, e.Y); //get info
                int pos = dgv_employe.HitTest(e.X, e.Y).RowIndex;
                if (pos > -1)
                {
                    if (dgv_employe.Rows[pos].Cells["id"] != null ? dgv_employe.Rows[pos].Cells["id"].Value != null : false)
                    {
                        int id = Convert.ToInt32(dgv_employe.Rows[pos].Cells["id"].Value);
                        if (id > 0)
                        {
                            int idx = employes.FindIndex(x => x.Id == id);
                            if (idx > -1)
                            {
                                switch (e.Button)
                                {
                                    case MouseButtons.Right:
                                        {
                                            for (int i = 0; i < dgv_employe.Rows.Count; i++)
                                            {
                                                dgv_employe.Rows[i].Selected = false;
                                            }
                                            select = employes[idx];
                                            tsmi_defined_niveau.Text = select.Privilege == 2 ? "Retirer Administrateur" : "Definir Administrateur";
                                            tsmi_defined_niveau.Image = select.Privilege == 2 ? global::ZK_Lymytz.Properties.Resources.edit_user : global::ZK_Lymytz.Properties.Resources.administrateur;
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Employe (dgv_employe_MouseDown)", ex);
            }
        }

        private void DeleteUser()
        {
            if (employe != null ? employe.Id > 0 : false)
            {
                if (appareil.EnableDevice(pointeuse.IMachine, false))
                {
                    Cursor c = Cursors.WaitCursor;
                    if (appareil.DeleteUserInfoEx(pointeuse.IMachine, (int)employe.Id))//upload user information to the memory
                    {
                        Utils.WriteLog("Supppression de l'employe " + employe.Id + " [" + employe.Nom + "] effectuée");
                        object_employe.RemoveDataGridView(Utils.GetRowData(dgv_employe, employe.Id));
                    }
                    else
                    {
                        Utils.WriteLog("Supppression de l'employe " + employe.Id + " [" + employe.Nom + "] impossible");
                    }
                    appareil.RefreshData(pointeuse.IMachine);//the data in the device should be refreshed
                    appareil.EnableDevice(pointeuse.IMachine, true);
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
            Update(employe);
        }

        public void Update(Employe e)
        {
            if (appareil.EnableDevice(pointeuse.IMachine, false))
            {
                Cursor c = Cursors.WaitCursor;
                if (appareil.ReadAllTemplate(pointeuse.IMachine))
                {
                    switch (pointeuse.Type)
                    {
                        case Constantes.TYPE_IFACE:
                            {
                                if (appareil.SSR_SetUserInfo(pointeuse.IMachine, (int)e.Id, e.Nom, e.Password, e.Privilege, e.BEnabled))//upload user information to the memory
                                {
                                    int pos = Utils.GetRowData(dgv_employe, e.Id);
                                    if (pos > -1)
                                    {
                                        object_employe.RemoveDataGridView(pos);
                                        object_employe.WriteDataGridView(pos, new object[] { e.Id, e.NomPrenom, e.IsPrivilege });
                                        int idx = employes.FindIndex(x => x.Id == e.Id);
                                        if (idx > -1)
                                            employes[idx].Privilege = e.Privilege;
                                    }
                                    Utils.WriteLog("-- Modification des informations de l'employe " + e.Id + " [" + e.Nom + "] effectuée");
                                }
                                else
                                {
                                    Utils.WriteLog("-- Modification des informations de l'employe " + e.Id + " [" + e.Nom + "] impossible");
                                }
                                break;
                            }
                        default:
                            {
                                if (appareil.SetUserInfo(pointeuse.IMachine, (int)e.Id, e.Nom, e.Password, e.Privilege, e.BEnabled))//upload user information to the memory
                                {
                                    int pos = Utils.GetRowData(dgv_employe, e.Id);
                                    if (pos > -1)
                                    {
                                        object_employe.RemoveDataGridView(pos);
                                        object_employe.WriteDataGridView(pos, new object[] { e.Id, e.NomPrenom, e.IsPrivilege });
                                        int idx = employes.FindIndex(x => x.Id == e.Id);
                                        if (idx > -1)
                                            employes[idx].Privilege = e.Privilege;
                                    }
                                    Utils.WriteLog("-- Modification des informations de l'employe " + e.Id + " [" + e.Nom + "] effectuée");
                                }
                                else
                                {
                                    Utils.WriteLog("-- Modification des informations de l'employe " + e.Id + " [" + e.Nom + "] impossible");
                                }
                                break;
                            }
                    }
                }
                else
                {
                    Utils.WriteLog("-- Modification des informations de l'employe " + e.Id + " [" + e.Nom + "] impossible");
                }
                appareil.RefreshData(pointeuse.IMachine);//the data in the device should be refreshed
                appareil.EnableDevice(pointeuse.IMachine, true);
                c = Cursors.Default;
            }
            else
            {
                Utils.WriteLog("-- Modification des informations de l'employe " + e.Id + " [" + e.Nom + "] impossible");
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
                if (appareil.EnableDevice(pointeuse.IMachine, false))
                {
                    Cursor c = Cursors.WaitCursor;
                    if (appareil.ReadAllTemplate(pointeuse.IMachine))
                    {
                        if (appareil.DelUserTmp(pointeuse.IMachine, (int)employe.Id, fingerID))//upload user information to the memory
                        {
                            Utils.WriteLog("-- Supppression de l'empreinte de l'employe " + employe.Id + " [" + employe.Nom + "] effectuée");
                            object_empreinte.RemoveDataGridView(Utils.GetRowData(dgv_empreinte, (long)fingerID));
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
                    appareil.RefreshData(pointeuse.IMachine);//the data in the device should be refreshed
                    appareil.EnableDevice(pointeuse.IMachine, true);
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
                object_employe.ClearDataGridView(true);
                object_employe.WriteDataGridView(new object[] { e.Id, e.NomPrenom });
                appareil.EnableDevice(pointeuse.IMachine, true);
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

        private void tsmi_defined_niveau_Click(object sender, EventArgs e)
        {
            if (select != null ? select.Id > 0 : false)
            {
                select.Privilege = select.Privilege == 0 ? 2 : 0;
                int idx = employes.FindIndex(x => x.Privilege == 2 && x.Id != select.Id);
                if (idx > -1 && select.Privilege == 2)
                {
                    Messages.Information("Vous avez déjà défini un administrateur");
                }
                else
                {
                    Utils.WriteLog("Demande de la modification des informations de l'employe " + select.Id + " [" + select.Nom + "]");
                    if (Messages.Confirmation_Infos("enregistrer") == System.Windows.Forms.DialogResult.Yes)
                    {
                        new Thread(delegate() { Update(select); }).Start();
                    }
                    else
                    {
                        Utils.WriteLog("-- Modification des informations de l'employe " + select.Id + " [" + select.Nom + "] annulée");
                    }
                }
            }
            else
            {
                Utils.WriteLog("Vous devez selectionner l'employé");
            }
        }
    }
}
