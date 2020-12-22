using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using ZK_Lymytz.BLL;
using ZK_Lymytz.TOOLS;

namespace ZK_Lymytz
{
    public partial class Form_Start : Form
    {
        ObjectThread _pbar;
        ObjectThread _lb_statut;
        int maximun = 0;
        int position = 0;
        bool wait = false;

        public Form_Start()
        {
            InitializeComponent();
            maximun = pbar_start.Maximum;
            _pbar = new ObjectThread(pbar_start);
            _lb_statut = new ObjectThread(lb_statut);
        }

        private void Form_Start_Activated(object sender, EventArgs e)
        {
            Constantes.FORM_START = this;
        }

        private void Form_Start_FormClosed(object sender, FormClosedEventArgs e)
        {
            Constantes.FORM_START = null;
            Constantes.OBJECT_START = null;
        }

        private void Form_Start_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Continuous();
            timer2.Start();
            timer1.Stop();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Continuous();
            timer2.Stop();
            timer1.Start();
        }

        public void Write(int position, bool wait)
        {
            this.wait = wait;
            if (wait)
                this.position = position;
            new Thread(delegate() { RunWrite(position); }).Start();
        }

        public void RunWrite(int position)
        {
            if (wait)
                position = this.position;
            _lb_statut.TextLabel(Constantes.STATUS[position < 0 ? 0 : (position > Constantes.STATUS.Length ? Constantes.STATUS.Length - 1 : position)]);
        }

        public void Continuous()
        {
            new Thread(new ThreadStart(RunContinuous)).Start();
        }

        public void RunContinuous()
        {
            if (pbar_start.Value == maximun)
                _pbar.SetValueBar(0);
            else
                _pbar.UpdateSimpleBar(1);
        }
    }
}
