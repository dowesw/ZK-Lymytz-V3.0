using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;

namespace ZK_Lymytz.IHM
{
    public partial class Dial_Update_Action : Form
    {
        IOEMDevice current;
        List<IOEMDevice> selection;
        bool load = false;
        int action = 0;

        private Dial_Update_Action()
        {
            InitializeComponent();
            Configuration.Load(this);
        }

        public Dial_Update_Action(IOEMDevice current)
            : this()
        {
            this.current = current;
        }

        public Dial_Update_Action(List<IOEMDevice> presences)
            : this()
        {
            this.selection = presences;
        }

        private void Dial_Update_Action_Load(object sender, EventArgs e)
        {
            List<Options> options = new List<Options>();
            options.Add(new Options(0, Action(0)));
            options.Add(new Options(1, Action(1)));
            options.Add(new Options(2, Action(2)));
            options.Add(new Options(3, Action(3)));
            options.Add(new Options(4, Action(4)));
            options.Add(new Options(5, Action(5)));


            com_action.DisplayMember = "Libelle";
            com_action.ValueMember = "Valeur";
            com_action.DataSource = new BindingSource(options, null);

            load = true;
            if (current == null)
            {
                if (selection != null ? selection.Count < 1 : true)
                    this.Dispose();
            }
            else
                com_action.SelectedItem = new Options(current.idwInOutMode, Action(current.idwInOutMode));
        }

        private void com_action_SelectedIndexChanged(object sender, EventArgs e)
        {
            Options o = com_action.SelectedItem as Options;
            if (o != null ? o.Valeur != null : false)
            {
                action = Convert.ToInt32(o.Valeur);
                switch (action)
                {
                    case 1:
                        box_action.Image = new System.Drawing.Bitmap(global::ZK_Lymytz.Properties.Resources.check_out, 16, 16);
                        break;
                    case 2:
                        box_action.Image = new System.Drawing.Bitmap(global::ZK_Lymytz.Properties.Resources.break_out, 16, 16);
                        break;
                    case 3:
                        box_action.Image = new System.Drawing.Bitmap(global::ZK_Lymytz.Properties.Resources.break_in, 16, 16);
                        break;
                    case 4:
                        box_action.Image = new System.Drawing.Bitmap(global::ZK_Lymytz.Properties.Resources.override_in, 16, 16);
                        break;
                    case 5:
                        box_action.Image = new System.Drawing.Bitmap(global::ZK_Lymytz.Properties.Resources.override_out, 16, 16);
                        break;
                    default:
                        box_action.Image = new System.Drawing.Bitmap(global::ZK_Lymytz.Properties.Resources.check_in, 16, 16);
                        break;
                }
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                if (Constantes.FORM_EVENEMENT != null)
                {
                    if (Constantes.FORM_EVENEMENT.dgv_log != null && Constantes.FORM_EVENEMENT.logs != null)
                    {
                        if (current != null ? current.id > 0 : false)
                        {
                            current.idwInOutMode = action;
                            int pos = Utils.GetRowData(Constantes.FORM_EVENEMENT.dgv_log, current.id);
                            if (pos > -1)
                            {
                                Constantes.FORM_EVENEMENT.object_log.RemoveDataGridView(pos);
                                Constantes.FORM_EVENEMENT.AddRow(pos, current);
                            }
                        }
                        else if (selection != null ? selection.Count > 0 : false)
                        {
                            foreach (IOEMDevice o in selection)
                            {
                                o.idwInOutMode = action;
                                int pos = Utils.GetRowData(Constantes.FORM_EVENEMENT.dgv_log, o.id);
                                if (pos > -1)
                                {
                                    Constantes.FORM_EVENEMENT.object_log.RemoveDataGridView(pos);
                                    Constantes.FORM_EVENEMENT.AddRow(pos, o);
                                }
                            }
                        }
                        this.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static string Action(int action)
        {
            switch (action)
            {
                case 1:
                    return "Sortie";
                case 2:
                    return "Sortie en pause";
                case 3:
                    return "Retour de pause";
                case 4:
                    return "Annuler entrée";
                case 5:
                    return "Annuler sortie";
                default:
                    return "Entrée";
            }
        }
    }
}
