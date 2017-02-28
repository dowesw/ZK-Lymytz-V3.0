using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

using ZK_Lymytz.BLL;
using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;

namespace ZK_Externe
{
    class Program
    {
        static bool _terminated = false;
        static void Main(string[] args)
        {
            //1-Ip 2-Employe 3-DateDebut 4-DateFin 5-Uniquement les fiches invalides
            //args = new string[] { "192.168.30.237", (2700).ToString(), "10-01-2017", "11-01-2017", "true" };
            Utils.Load();
            Execute(args);
        }

        static void Execute(object[] args)
        {
            Console.WriteLine("Synchronisation......");
            Pointeuse pointeuse = null;
            Employe employe = null;
            DateTime dateDebut = new DateTime();
            DateTime dateFin = new DateTime();
            bool invalid_only = false;
            bool p = false, e = false, d = false, f = false;
            if (args != null ? args.Length > 0 : false)
            {
                pointeuse = args[0] != null ? PointeuseBLL.OneByIp(args[0].ToString()) : null;
                if (args.Length > 1)
                {
                    employe = args[1] != null ? EmployeBLL.OneById(Convert.ToInt32(args[1])) : null;
                    if (args.Length > 2)
                    {
                        dateDebut = args[2] != null ? Convert.ToDateTime(args[2]) : new DateTime();
                        if (args.Length > 3)
                        {
                            dateFin = args[3] != null ? Convert.ToDateTime(args[3]) : new DateTime();
                            if (args.Length > 4)
                            {
                                invalid_only = args[4] != null ? Convert.ToBoolean(args[4]) : false;
                            }
                        }
                    }
                }
                if (pointeuse != null ? pointeuse.Id > 0 : false)
                {
                    Console.WriteLine("  Pointeuse : " + pointeuse.Ip);
                    p = true;
                }
                if (employe != null ? employe.Id > 0 : false)
                {
                    Console.WriteLine("  Employe : " + employe.NomPrenom);
                    e = true;
                }
                if ((dateDebut != null) ? dateDebut.ToString() != "01/01/0001 00:00:00" : false)
                {
                    Console.WriteLine("  Date Début : " + dateDebut.ToString());
                    d = true;
                }
                if ((dateFin != null) ? dateFin.ToString() != "01/01/0001 00:00:00" : false)
                {
                    Console.WriteLine("  Date Fin : " + dateFin.ToString());
                    f = true;
                }
                Console.WriteLine("  Uniquement les fiches invalides ? : " + (invalid_only ? "OUI" : "NON"));

                dateDebut = d ? dateDebut : dateFin;
                dateFin = f ? dateFin : DateTime.Now;
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(Running));
                thread.Start();
                if (p)
                {
                    if (e)
                    {
                        if (d || f)
                        {
                            Fonctions.SynchroniseLog(pointeuse, employe, dateDebut, dateFin, invalid_only, false);
                        }
                        else
                        {
                            Fonctions.SynchroniseLog(pointeuse, employe, invalid_only, false);
                        }
                    }
                    else
                    {
                        Fonctions.SynchroniseLog(pointeuse, invalid_only, false);
                    }
                }
                else
                {
                    if (e)
                    {
                        if (d || f)
                        {
                            Fonctions.SynchroniseLog(employe, dateDebut, dateFin, invalid_only, false);
                        }
                        else
                        {
                            Fonctions.SynchroniseLog(employe, invalid_only, false);
                        }
                    }
                    else
                    {
                        Fonctions.SynchroniseLog(invalid_only, false);
                    }
                }
                try
                {
                    _terminated = true;
                    thread.Abort();
                }
                catch (System.Threading.ThreadAbortException ex) { }
                Console.WriteLine(".");
                Console.WriteLine("Traitement terminé.");
            }
            else
            {
                Console.WriteLine("Traitement impossible. Veuillez entre les paramétres (1-Ip 2-Employe 3-DateDebut 4-DateFin 5-invalid_only)");
            }
            Console.ReadKey();
        }

        static void Running()
        {
            Console.Write("Traitement en cours.");
            while (!_terminated)
            {
                Console.Write(".");
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
