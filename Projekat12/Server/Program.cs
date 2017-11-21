using Common;
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

namespace Server
{
    public class Program
    {

        private static ServerHost<IServer, ServerClass> svc;
		private static ServerClass sc = new ServerClass();
		public static Dictionary<string, DataObj> lokalnaBaza = new Dictionary<string, DataObj>();
        public static EventLog customLog;
        public static MergeBaza mb = new MergeBaza();
        public static bool tajm = false;
        public static int portServera;

        public static void Main(string[] args)
        {
            //VezaSaAuditom.PoveziSe();
            //customLog = Audit.KreirajAudit("LogovanjaServera", WindowsIdentity.GetCurrent().Name);
            OtvoriServer();

            VezaSaGlavnim.PoveziSe();

            //sc.DodajEntitet(new DataObj("1", "sss", "sss", 2017));
            
            Console.ReadLine();    
            svc.Close();
        }

		private static void OtvoriServer()
		{
			Console.WriteLine("Unesi port");
			string port = Console.ReadLine(); //pazi na validaciju
            portServera = Convert.ToInt32(port);

			svc = new ServerHost<IServer,ServerClass>("Server", port);
			svc.Open();
			Console.WriteLine("Otvorio");
		}
	}
}