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

namespace Server
{
    class Program
    {

        static ServiceHost svc;
        public static Dictionary<string, DataObj> lokalnaBaza = new Dictionary<string, DataObj>();
        public static EventLog customLog;
        static ServerClass sc = new ServerClass();
        public static MergeBaza mb = new MergeBaza();
        public static bool tajm = false;
        public static int portServera;

        static void Main(string[] args)
        {
            //VezaSaAuditom.PoveziSe();
            //customLog = Audit.KreirajAudit("LogovanjaServera", WindowsIdentity.GetCurrent().Name);
            OtvoriServer();
            VezaSaGlavnim.PoveziSe();
            VezaSaGlavnim.PosaljiPodatke();


            //sc.DodajEntitet(new DataObj("1", "sss", "sss", 2017));
            

            Console.ReadLine();    
            svc.Close();
        }

		public static void OtvoriServer()
		{
			var binding = new NetTcpBinding();
			//binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.Certificate;

			svc = new ServiceHost(typeof (ServerClass));
			Console.WriteLine("Unesi port");
			string port = Console.ReadLine(); //pazi na validaciju
            portServera = Convert.ToInt32(port);

			//var srvCertCN = Formatter.ParseName(WindowsIdentity.GetCurrent().Name);
			svc.AddServiceEndpoint(typeof (IServer), binding, new Uri(String.Format("net.tcp://localhost:{0}/Server", port)));
            //svc.Credentials.ClientCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.Custom;
            //svc.Credentials.ClientCertificate.Authentication.CustomCertificateValidator = new ServiceCertValidator();

            //svc.Credentials.ClientCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            //svc.Credentials.ServiceCertificate.Certificate = CertificateManager.GetCertificateFromStorage(StoreName.My,
            //	StoreLocation.LocalMachine, srvCertCN);

            svc.Open();

			Console.WriteLine("Otvorio");
		}
	}
}