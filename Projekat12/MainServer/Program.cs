using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Common.Contracts;
using Common.Entiteti;

namespace MainServer
{
	public class Program
	{
		private static ServerHost<IMainServer, MainServerClass> svc;
		public static Dictionary<string, DataObj> glavnaBaza = new Dictionary<string, DataObj>();
		public static MergeBaza mb = new MergeBaza();

		public static void Main(string[] args)
		{
			//Audit.KreirajAudit("MainServer", WindowsIdentity.GetCurrent().Name);
			OtvoriServer();
			Thread t1 = new Thread(MainServerClass.Provera); // odavde pokrecemo proveru
			t1.Start();
			VezaSaAuditom.PoveziSe();

			Console.ReadLine();
			t1.Abort();
			svc.Close();
		}

		private static void OtvoriServer()
		{
			svc = new ServerHost<IMainServer, MainServerClass>("MainServer", "51000");
			svc.Open();
			Console.WriteLine("Otvorio");
		}
	}
}