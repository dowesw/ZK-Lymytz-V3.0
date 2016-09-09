using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Drawing;
using ZK_Lymytz.TOOLS;


namespace ZK_Lymytz.TOOLS
{
    class Configuration
    {
        public static string langue = Constantes.LANGUE_ANGLAIS;

        public static string template;

        public static string back_color_Form;

        public static string back_color_Text;

        public static string fore_color_Label;

        public static string fore_color_Text;

        public static string police_Label;

        public static float taille_Label;

        public static string police_Text;

        public static float taille_Text;

        public static void Save()
        {
            string chemin = Chemins.getCheminConfiguration();
            RegistryKey Nkey = Registry.CurrentUser;
            try
            {
                RegistryKey valKey = Nkey.OpenSubKey(@chemin, true);
                if (valKey == null)
                {
                    Nkey.CreateSubKey(@chemin);
                    valKey = Nkey.OpenSubKey(@chemin, true);
                }
                valKey.SetValue("langue", langue);
                valKey.SetValue("nom_template", template);
                valKey.SetValue("back_color_form", back_color_Form);
                valKey.SetValue("fore_color_label", fore_color_Label);
                valKey.SetValue("back_color_text", back_color_Text);
                valKey.SetValue("fore_color_text", fore_color_Text);
                valKey.SetValue("police_label", police_Label);
                valKey.SetValue("police_text", police_Text);
                valKey.SetValue("taille_label", taille_Label);
                valKey.SetValue("taille_text", taille_Text);
            }
            catch (Exception e)
            {
                Messages.Exception("Configuration (Save) ", e);
            }
            finally
            {
                Nkey.Close();
            }
        }

        public static void Return()
        {
            string chemin = Chemins.getCheminConfiguration();
            RegistryKey Nkey = Registry.CurrentUser;
            try
            {
                Object[] lic = new Object[3];
                RegistryKey valKey = Nkey.OpenSubKey(@chemin, true);
                if (valKey != null)
                {
                    langue = (string)(valKey.GetValue("langue") != null ? valKey.GetValue("langue") : Constantes.LANGUE_FRANCAIS);
                    template = (string)(valKey.GetValue("nom_template") != null ? valKey.GetValue("nom_template") : "Basique");
                    back_color_Form = (string)(valKey.GetValue("back_color_form") != null ? valKey.GetValue("back_color_form") : "GradientInactiveCaption");
                    fore_color_Label = (string)(valKey.GetValue("fore_color_label") != null ? valKey.GetValue("fore_color_label") : "ControlText");
                    back_color_Text = (string)(valKey.GetValue("back_color_text") != null ? valKey.GetValue("back_color_text") : "Control");
                    fore_color_Text = (string)(valKey.GetValue("fore_color_text") != null ? valKey.GetValue("fore_color_text") : "WindowText");
                    police_Label = (string)(valKey.GetValue("police_label") != null ? valKey.GetValue("police_label") : "Microsoft Sans Serif");
                    police_Text = (string)(valKey.GetValue("police_text") != null ? valKey.GetValue("police_text") : "Microsoft Sans Serif");
                    taille_Label = float.Parse((string)(valKey.GetValue("taille_label") != null ? valKey.GetValue("taille_label") : "8,25"));
                    taille_Text = float.Parse((string)(valKey.GetValue("taille_text") != null ? valKey.GetValue("taille_text") : "8,25"));
                }
                else
                {
                    langue = (string)(Constantes.LANGUE_FRANCAIS);
                    template = (string)("Basique");
                    back_color_Form = (string)("GradientInactiveCaption");
                    fore_color_Label = (string)("ControlText");
                    back_color_Text = (string)("Control");
                    fore_color_Text = (string)("WindowText");
                    police_Label = (string)("Microsoft Sans Serif");
                    police_Text = (string)("Microsoft Sans Serif");
                    taille_Label = float.Parse((string)("8,25"));
                    taille_Text = float.Parse((string)("8,25"));
                    Save();
                }
            }
            catch (Exception e)
            {
                Messages.Exception("Configuration (Return) ", e);
            }
            finally
            {
                Nkey.Close();
            }
        }

        private static void Reload(Control parent)
        {
            foreach (Control crtl in parent.Controls)
            {
                if ((crtl.GetType().ToString() == "System.Windows.Forms.TextBox") ||
                    (crtl.GetType().ToString() == "System.Windows.Forms.ComboBox") ||
                    (crtl.GetType().ToString() == "System.Windows.Forms.NumericUpDown") ||
                    (crtl.GetType().ToString() == "System.Windows.Forms.MaskedTextBox") ||
                    (crtl.GetType().ToString() == "System.Windows.Forms.CheckBox"))
                {
                    FontFamily p = new FontFamily(police_Text);
                    crtl.Font = new Font((FontFamily)p, taille_Text);
                    crtl.ForeColor = Color.FromName(fore_color_Text);
                    crtl.BackColor = Color.FromName(back_color_Text);
                }
                else if (crtl.GetType().ToString() == "System.Windows.Forms.Button")
                    if (back_color_Form == "Black")
                    {
                        crtl.ForeColor = Color.White;
                    }
                    else
                    {
                        crtl.ForeColor = Color.Black;
                    }
                else if (crtl.GetType().ToString() == "System.Windows.Forms.DataGridView")
                    crtl.ForeColor = Color.Black;
                else if ((crtl.GetType().ToString() == "System.Windows.Forms.Label"))
                {
                    FontFamily p = new FontFamily(police_Label);
                    crtl.Font = new Font((FontFamily)p, taille_Label);
                    crtl.ForeColor = Color.FromName(fore_color_Label);
                }
                else if (crtl.GetType().ToString() == "System.Windows.Forms.GroupBox")
                {
                    FontFamily p = new FontFamily(police_Label);
                    crtl.Font = new Font((FontFamily)p, taille_Label);
                    crtl.ForeColor = Color.FromName(fore_color_Label);
                    Reload(crtl);
                }
                else if ((crtl.GetType().ToString() == "System.Windows.Forms.Panel"))
                {
                    Reload(crtl);
                }
                else if ((crtl.GetType().ToString() == "System.Windows.Forms.TabControl"))
                {
                    Reload(crtl);
                }
                else if ((crtl.GetType().ToString() == "System.Windows.Forms.TabPage"))
                {
                    FontFamily p = new FontFamily(police_Label);
                    crtl.Font = new Font((FontFamily)p, taille_Label);
                    crtl.ForeColor = Color.FromName(fore_color_Label);
                    crtl.BackColor = Color.FromName(back_color_Form);
                    Reload(crtl);
                }
            }
        }

        public static void Load(Form form)
        {
            try
            {
                form.BackColor = Color.FromName(back_color_Form);
                Reload(form);
            }
            catch (Exception e)
            {
                Messages.Exception("Configuration (Load) ", e);
            }
        }
    }
}
