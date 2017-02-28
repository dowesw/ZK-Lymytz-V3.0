using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using ZK_Lymytz.TOOLS;

namespace ZK_Lymytz.IHM
{
    public partial class Form_Wait : Form
    {
        public bool _stop = false, _exit;
        public int maximun = 60;
        Thread open, wait;
        ObjectThread object_bar;

        public Form_Wait()
        {
            InitializeComponent();
            p_bar.Maximum = maximun;
            object_bar = new ObjectThread(p_bar);
        }

        public Form_Wait(int maximun)
            : this()
        {
            this.maximun = maximun;
        }

        private void Form_Wait_Load(object sender, EventArgs e)
        {
            wait = new Thread(new ThreadStart(Start));
            wait.Start();
        }

        public void Start()
        {
            while (!_stop)
            {
                LoadPatience(false);
                Thread.Sleep(1000);
                if (p_bar.Value == p_bar.Maximum)
                {
                    _stop = true;
                }
            }
        }

        public void LoadPatience(bool _fin)
        {
            if (p_bar != null)
            {
                if (!_fin)
                {
                    object_bar._UpdateBar(1, "Connexion en cours");
                }
                else
                {
                    object_bar._UpdateBar(p_bar.Maximum - p_bar.Value, "Connexion en cours");
                    p_bar = null;
                }
            }
        }

        private void Form_Wait_FormClosed(object sender, FormClosedEventArgs e)
        {
            Constantes.FORM_WAIT = null;
            Fermer();
        }

        public void Open()
        {
            open = new Thread(new ThreadStart(this.OpenDialog));
            open.Start();
        }

        public void OpenDialog()
        {
            this.ShowDialog();
        }

        public void Fermer()
        {
            if (!_exit)
            {
                _stop = true;
                try
                {
                    if (wait != null)
                    {
                        wait.Abort();
                        wait.Join();
                    }
                }
                catch (ThreadAbortException ex){}
                try
                {
                    if (open != null)
                    {
                        open.Abort();
                        open.Join();
                    }
                }
                catch (ThreadAbortException ex){}
                this.Close();
                this.Dispose();
            }
            _exit = true;
        }

    }
}
