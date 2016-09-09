using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZK_Lymytz.TOOLS;
using ZK_Lymytz.BLL;
using ZK_Lymytz.ENTITE;

namespace ZK_Lymytz.IHM
{
    public partial class Form_Add_Empreinte : Form
    {
        private Employe _EMPLOYE = new Employe();
        private int _FINGER_ID;
        List<Employe> employes = new List<Employe>();
        public Pointeuse pointeuse = new Pointeuse();
        bool save = false;

        public Form_Add_Empreinte()
        {
            InitializeComponent();
            Configuration.Load(this);
        }

        public Form_Add_Empreinte(Pointeuse pointeuse)
        {
            InitializeComponent();
            Configuration.Load(this);
            this.pointeuse = pointeuse;
        }

        private void Form_Empreinte_FormClosed(object sender, FormClosedEventArgs e)
        {
            Constantes.FORM_ADD_EMPREINTE = null;
            Utils.WriteLog("Fermeture page (Sauvegarde Empreinte)");
            Utils.removeFrom("Form_Add_Empreinte");
        }

        private void Form_Empreinte_Load(object sender, EventArgs e)
        {
            txt_result.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            LoadEmploye();
            ResetDoigt();
            ResetEmploye();
            OpenForm();
        }

        public void OpenForm()
        {
            if (pointeuse != null ? pointeuse.Id > 0 : false)
            {
                this.Text = "Sauvegarde Empreinte (Pointeuse : " + pointeuse.Ip + ")";
                Appareil z = Utils.ReturnAppareil(pointeuse);
                Utils.VerifyZkemkeeper(ref z, ref pointeuse, this);
                z.axCZKEM.OnFingerFeature += new zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler(z.axCZKEM1_OnFingerFeature);
            }
        }

        public void ResetEmploye()
        {
            _FINGER_ID = -1;
            com_employe.ResetText();
            _EMPLOYE = new Employe();
            txt_id.ResetText();
            com_employe.Enabled = true;
        }

        public void ResetDoigt()
        {
            panel_1_1.BackColor = Color.White;
            panel_1_2.BackColor = Color.White;
            panel_1_3.BackColor = Color.White;
            panel_1_4.BackColor = Color.White;
            panel_1_5.BackColor = Color.White;

            panel_2_1.BackColor = Color.White;
            panel_2_2.BackColor = Color.White;
            panel_2_3.BackColor = Color.White;
            panel_2_4.BackColor = Color.White;
            panel_2_5.BackColor = Color.White;
        }

        public void LoadEmploye()
        {
            try
            {
                employes.Clear();
                string query = "select e.* from yvs_grh_employes e inner join yvs_agences a on e.agence = a.id where a.societe = " + Constantes.SOCIETE.Id + " order by e.nom, e.prenom";
                employes = EmployeBLL.List(query);
                com_employe.DisplayMember = "NomPrenom";
                com_employe.ValueMember = "Id";
                com_employe.DataSource = new BindingSource(employes, null);

                for (int i = 0; i < employes.Count; i++)
                {
                    com_employe.AutoCompleteCustomSource.Add(employes[i].NomPrenom);
                }
                com_employe.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                com_employe.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            catch (Exception ex)
            {
                Messages.Exception("Form_Add_Empreinte (LoadEmploye)", ex);
            }
        }

        private void com_employe_SelectedIndexChanged(object sender, EventArgs e)
        {
            Employe a = com_employe.SelectedItem as Employe;
            if (a != null)
            {
                _EMPLOYE = employes.Find(x => x.Id == a.Id);
                txt_id.Text = _EMPLOYE.Id.ToString();
                if (_FINGER_ID > -1)
                {
                    com_employe.Enabled = false;
                    Empreinte em = EmpreinteBLL.OneByEmployeFinger(_EMPLOYE.Id, _FINGER_ID);
                    save = (em != null ? em.Id < 1 : true);
                    if (!save)
                    {
                        switch (_FINGER_ID)
                        {
                            case 0:
                                panel_2_1.BackColor = Color.Red;
                                break;
                            case 1:
                                panel_2_2.BackColor = Color.Red;
                                break;
                            case 2:
                                panel_2_3.BackColor = Color.Red;
                                break;
                            case 3:
                                panel_2_4.BackColor = Color.Red;
                                break;
                            case 4:
                                panel_2_5.BackColor = Color.Red;
                                break;
                            case 5:
                                panel_1_1.BackColor = Color.Red;
                                break;
                            case 6:
                                panel_1_2.BackColor = Color.Red;
                                break;
                            case 7:
                                panel_1_3.BackColor = Color.Red;
                                break;
                            case 8:
                                panel_1_4.BackColor = Color.Red;
                                break;
                            case 9:
                                panel_1_5.BackColor = Color.Red;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            ResetEmploye();
            ResetDoigt();
        }

        private void panel_1_5_Click(object sender, EventArgs e)
        {
            ResetDoigt();
            _FINGER_ID = 9;
            panel_1_5.BackColor = SystemColors.GradientActiveCaption;
            if (_EMPLOYE != null ? _EMPLOYE.Id > 0 : false)
            {
                com_employe.Enabled = false;
                Empreinte em = EmpreinteBLL.OneByEmployeFinger(_EMPLOYE.Id, _FINGER_ID);
                save = (em != null ? em.Id < 1 : true);
                if (!save)
                {
                    panel_1_5.BackColor = Color.Red;
                }
            }
            pointeuse.Zkemkeeper._FINGER.Index = 9;
            pointeuse.Zkemkeeper._FINGER.Main = "droite";
            pointeuse.Zkemkeeper._FINGER.Doigt = "auriculaire";
        }

        private void panel_1_4_Click(object sender, EventArgs e)
        {
            ResetDoigt();
            _FINGER_ID = 8;
            panel_1_4.BackColor = SystemColors.GradientActiveCaption;
            if (_EMPLOYE != null ? _EMPLOYE.Id > 0 : false)
            {
                com_employe.Enabled = false;
                Empreinte em = EmpreinteBLL.OneByEmployeFinger(_EMPLOYE.Id, _FINGER_ID);
                save = (em != null ? em.Id < 1 : true);
                if (!save)
                {
                    panel_1_4.BackColor = Color.Red;
                }
            }
            pointeuse.Zkemkeeper._FINGER.Index = 8;
            pointeuse.Zkemkeeper._FINGER.Main = "droite";
            pointeuse.Zkemkeeper._FINGER.Doigt = "annulaire";
        }

        private void panel_1_3_Click(object sender, EventArgs e)
        {
            ResetDoigt();
            _FINGER_ID = 7;
            panel_1_3.BackColor = SystemColors.GradientActiveCaption;
            if (_EMPLOYE != null ? _EMPLOYE.Id > 0 : false)
            {
                com_employe.Enabled = false;
                Empreinte em = EmpreinteBLL.OneByEmployeFinger(_EMPLOYE.Id, _FINGER_ID);
                save = (em != null ? em.Id < 1 : true);
                if (!save)
                {
                    panel_1_3.BackColor = Color.Red;
                }
            }
            pointeuse.Zkemkeeper._FINGER.Index = 7;
            pointeuse.Zkemkeeper._FINGER.Main = "droite";
            pointeuse.Zkemkeeper._FINGER.Doigt = "majeur";
        }

        private void panel_1_2_Click(object sender, EventArgs e)
        {
            ResetDoigt();
            _FINGER_ID = 6;
            panel_1_2.BackColor = SystemColors.GradientActiveCaption;
            if (_EMPLOYE != null ? _EMPLOYE.Id > 0 : false)
            {
                com_employe.Enabled = false;
                Empreinte em = EmpreinteBLL.OneByEmployeFinger(_EMPLOYE.Id, _FINGER_ID);
                save = (em != null ? em.Id < 1 : true);
                if (!save)
                {
                    panel_1_2.BackColor = Color.Red;
                }
            }
            pointeuse.Zkemkeeper._FINGER.Index = 6;
            pointeuse.Zkemkeeper._FINGER.Main = "droite";
            pointeuse.Zkemkeeper._FINGER.Doigt = "index";
        }

        private void panel_1_1_Click(object sender, EventArgs e)
        {
            ResetDoigt();
            _FINGER_ID = 5;
            panel_1_1.BackColor = SystemColors.GradientActiveCaption;
            if (_EMPLOYE != null ? _EMPLOYE.Id > 0 : false)
            {
                com_employe.Enabled = false;
                Empreinte em = EmpreinteBLL.OneByEmployeFinger(_EMPLOYE.Id, _FINGER_ID);
                save = (em != null ? em.Id < 1 : true);
                if (!save)
                {
                    panel_1_1.BackColor = Color.Red;
                }
            }
            pointeuse.Zkemkeeper._FINGER.Index = 5;
            pointeuse.Zkemkeeper._FINGER.Main = "droite";
            pointeuse.Zkemkeeper._FINGER.Doigt = "pouce";
        }

        private void panel_2_1_Click(object sender, EventArgs e)
        {
            ResetDoigt();
            _FINGER_ID = 0;
            panel_2_1.BackColor = SystemColors.GradientActiveCaption;
            if (_EMPLOYE != null ? _EMPLOYE.Id > 0 : false)
            {
                com_employe.Enabled = false;
                Empreinte em = EmpreinteBLL.OneByEmployeFinger(_EMPLOYE.Id, _FINGER_ID);
                save = (em != null ? em.Id < 1 : true);
                if (!save)
                {
                    panel_2_1.BackColor = Color.Red;
                }
            }
            pointeuse.Zkemkeeper._FINGER.Index = 0;
            pointeuse.Zkemkeeper._FINGER.Main = "gauche";
            pointeuse.Zkemkeeper._FINGER.Doigt = "pouce";
        }

        private void panel_2_2_Click(object sender, EventArgs e)
        {
            ResetDoigt();
            _FINGER_ID = 1;
            panel_2_2.BackColor = SystemColors.GradientActiveCaption;
            if (_EMPLOYE != null ? _EMPLOYE.Id > 0 : false)
            {
                com_employe.Enabled = false;
                Empreinte em = EmpreinteBLL.OneByEmployeFinger(_EMPLOYE.Id, _FINGER_ID);
                save = (em != null ? em.Id < 1 : true);
                if (!save)
                {
                    panel_2_2.BackColor = Color.Red;
                }
            }
            pointeuse.Zkemkeeper._FINGER.Index = 1;
            pointeuse.Zkemkeeper._FINGER.Main = "gauche";
            pointeuse.Zkemkeeper._FINGER.Doigt = "index";
        }

        private void panel_2_3_Click(object sender, EventArgs e)
        {
            ResetDoigt();
            _FINGER_ID = 2;
            panel_2_3.BackColor = SystemColors.GradientActiveCaption;
            if (_EMPLOYE != null ? _EMPLOYE.Id > 0 : false)
            {
                com_employe.Enabled = false;
                Empreinte em = EmpreinteBLL.OneByEmployeFinger(_EMPLOYE.Id, _FINGER_ID);
                save = (em != null ? em.Id < 1 : true);
                if (!save)
                {
                    panel_2_3.BackColor = Color.Red;
                }
            }
            pointeuse.Zkemkeeper._FINGER.Index = 2;
            pointeuse.Zkemkeeper._FINGER.Main = "gauche";
            pointeuse.Zkemkeeper._FINGER.Doigt = "majeur";
        }

        private void panel_2_4_Click(object sender, EventArgs e)
        {
            ResetDoigt();
            _FINGER_ID = 3;
            panel_2_4.BackColor = SystemColors.GradientActiveCaption;
            if (_EMPLOYE != null ? _EMPLOYE.Id > 0 : false)
            {
                com_employe.Enabled = false;
                Empreinte em = EmpreinteBLL.OneByEmployeFinger(_EMPLOYE.Id, _FINGER_ID);
                save = (em != null ? em.Id < 1 : true);
                if (!save)
                {
                    panel_2_4.BackColor = Color.Red;
                }
            }
            pointeuse.Zkemkeeper._FINGER.Index = 3;
            pointeuse.Zkemkeeper._FINGER.Main = "gauche";
            pointeuse.Zkemkeeper._FINGER.Doigt = "annulaire";
        }

        private void panel_2_5_Click(object sender, EventArgs e)
        {
            ResetDoigt();
            _FINGER_ID = 4;
            panel_2_5.BackColor = SystemColors.GradientActiveCaption;
            if (_EMPLOYE != null ? _EMPLOYE.Id > 0 : false)
            {
                com_employe.Enabled = false;
                Empreinte em = EmpreinteBLL.OneByEmployeFinger(_EMPLOYE.Id, _FINGER_ID);
                save = (em != null ? em.Id < 1 : true);
                if (!save)
                {
                    panel_2_5.BackColor = Color.Red;
                }
            }
            pointeuse.Zkemkeeper._FINGER.Index = 4;
            pointeuse.Zkemkeeper._FINGER.Main = "gauche";
            pointeuse.Zkemkeeper._FINGER.Doigt = "auriculaire";
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            box_doigt.Image = global::ZK_Lymytz.Properties.Resources.empreinte;
            txt_result.BackColor = SystemColors.Control;
            txt_result.Text = "En Attente";

            if (_EMPLOYE != null ? _EMPLOYE.Id > 0 : false)
            {
                if (_FINGER_ID > -1)
                {
                    if (save)
                    {
                        if (pointeuse != null ? pointeuse.Id > 0 : false)
                        {
                            bool correct = false;
                            Appareil z = Utils.ReturnAppareil(pointeuse);
                            if (z == null)
                            {
                                Utils.VerifyZkemkeeper(ref z, ref pointeuse, this);
                                if (z == null)
                                {
                                    Utils.WriteLog("La liaison avec l'appareil " + pointeuse.Ip + " est corrompue");
                                    return;
                                }
                                z.axCZKEM.OnFingerFeature += new zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler(z.axCZKEM1_OnFingerFeature);
                            }

                            z.CancelOperation();
                            z.SSR_DelUserTmpExt(pointeuse.IMachine, _EMPLOYE.Id.ToString(), _FINGER_ID);//If the specified index of user's templates has existed ,delete it first.(SSR_DelUserTmp is also available sometimes)
                            Utils.WriteLog("Demande d'enrollement de l'employe " + _EMPLOYE.NomPrenom + ", Doigt (" + z._FINGER.Doigt + ") - Main (" + z._FINGER.Doigt + ")");
                            if (DialogResult.Yes == Messages.Confirmation("commencer"))
                            {
                                pointeuse.Zkemkeeper._EMPLOYE = _EMPLOYE;
                                pointeuse.Zkemkeeper._FINGER_ID = _FINGER_ID;
                                correct = false;
                                z._FLAG = 3;
                                while (!correct && z._FLAG > -1)
                                {
                                    if (z.StartEnrollEx(txt_id.Text.Trim(), _FINGER_ID, z._FLAG))
                                    {
                                        Utils.WriteLog("-- Debut de l'enregistrement de l'empreinte, Employe " + _EMPLOYE.NomPrenom + ", Doigt (" + z._FINGER.Doigt + ") - Main (" + z._FINGER.Doigt + ")");
                                        box_doigt.Image = global::ZK_Lymytz.Properties.Resources.empreinte01;
                                        correct = true;
                                        txt_result.BackColor = Color.Yellow;
                                        txt_result.Text = "Traitement";
                                    }
                                    else
                                    {
                                        z._FLAG -= 1;
                                    }
                                }
                            }
                            else
                            {
                                Utils.WriteLog("-- Enrollement de l'employe " + _EMPLOYE.NomPrenom + ", Doigt (" + z._FINGER.Doigt + ") - Main (" + z._FINGER.Doigt + ") annulé");
                            }
                            if (!correct)
                            {
                                Utils.WriteLog("-- Enrollement de l'employe " + _EMPLOYE.NomPrenom + ", Doigt (" + z._FINGER.Doigt + ") - Main (" + z._FINGER.Doigt + ") impossible");
                            }
                        }
                        else
                        {
                            Constantes.FORM_ADD_EMPREINTE = null;
                            this.Dispose();
                        }
                    }
                    else
                    {
                        Utils.WriteLog("Vous avez déja pris les empreintes sur ce doigt de cet employé");
                    }
                }
                else
                {
                    Utils.WriteLog("Vous devez selectionner un doigt");
                }
            }
            else
            {
                Utils.WriteLog("Vous devez selectionner un employé");
            }
        }
    }
}
