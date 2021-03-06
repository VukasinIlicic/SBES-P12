﻿using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Common.CertManager;
using Common.Contracts;
using System.Collections.Concurrent;
using Common.Entiteti;
using Common.Helpers;

namespace Server
{
    public class Program
    {

        private static ServerHost<IServer, ServerClass> svc;
        private static ServerHost<IUpdate, VezaSaGlavnim> svcGlavni;
        private static ServerClass sc = new ServerClass();
        public static Dictionary<string, DataObj> lokalnaBaza = new Dictionary<string, DataObj>();
        public static EventLog customLog;
        public static MergeBaza mb = new MergeBaza();
        public static bool tajm = false;
        public static int portServeraZaGlavni;
        public static string IME_LOKALNE_BAZE = String.Format("{0}_Baza.xml", Formatter.ParseName(WindowsIdentity.GetCurrent().Name));

        public static void Main(string[] args)
        {
            string ime = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
            customLog = Audit.KreirajAudit(String.Format("LogoviServera({0})", ime), String.Format("Server({0})", ime));

            OtvoriServer();
            Console.WriteLine("Unesite adresu main servera");
            var adresa = Console.ReadLine();

            VezaSaGlavnim.PoveziSe(adresa);

            Console.ReadLine();
            svc.Close();
            svcGlavni.Close();
        }

        private static void OtvoriServer()
        {
            Console.WriteLine("Unesi port");
            string port = Console.ReadLine();
            portServeraZaGlavni = Convert.ToInt32(port);

            svc = new ServerHost<IServer, ServerClass>("Server", port, AuthType.CertAuth);
            svc.Open();
            Console.WriteLine("Otvorio za klijente");

            svcGlavni = new ServerHost<IUpdate, VezaSaGlavnim>("VezaSaGlavnim", port, AuthType.WinAuth);
            svcGlavni.Open();
            Console.WriteLine("Otvorio za glavni");
        }
    }
}