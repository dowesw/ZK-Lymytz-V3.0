using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Diagnostics;

using System.Windows.Forms;

using ZK_LymytzService.TOOLS;

namespace ZK_LymytzService
{
    class Program
    {
        static void _Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] { new ZK_LymytzService(args) };
            ServiceBase.Run(ServicesToRun);
        }


        static void Main_(string[] args)
        {
            Zkemkeeper z = new Zkemkeeper();

            if (z.ConnectNet("192.168.1.202", 4370))
            {
                Thread createComAndMessagePumpThread = new Thread(() =>
                {
                    z.RegEvent(1);
                    z.axCZKEM.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(z.axCZKEM1_OnAttTransactionEx);
                    Application.Run();
                });
                createComAndMessagePumpThread.SetApartmentState(ApartmentState.STA);
                createComAndMessagePumpThread.Start();
            }
            var t = 0;
            var s = t;
        }

        static void Main(string[] args)
        {
            Zkemkeeper z = new Zkemkeeper();
            if (z.StartOne("192.168.1.202"))
            {
                Application.Run();
            }
            var t = 0;
            var s = t;
        }
    }
}
