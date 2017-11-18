using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Server
{
    class Program
    {

        static ServiceHost svc;
        public static Dictionary<string, DataObj> lokalnaBaza = new Dictionary<string, DataObj>();
        public static EventLog customLog;

        static void Main(string[] args)
        {
            //VezaSaAuditom.PoveziSe();
            //customLog = Audit.KreirajAudit("LogovanjaServera", WindowsIdentity.GetCurrent().Name);
            //OtvoriServer();
            VezaSaGlavnim.PoveziSe();
            Thread t1 = new Thread(Update);
            t1.Start();

            Console.ReadLine();
            t1.Abort(); // proveri da li je ok      
            svc.Close();
        }

        private static void Update()
        {
            DateTime vreme = DateTime.Now;
            while(true)
            {
                while ((DateTime.Now.Second % Konstanta.Vreme_Azuriranja) != 0)
                    Thread.Sleep(300);

                VezaSaGlavnim.IntegrityUpdate();
            }          
        }

        public static void OtvoriServer()
        {
            NetTcpBinding binding = new NetTcpBinding();
            svc = new ServiceHost(typeof(ServerClass));
            Console.WriteLine("Unesi port");
            string port = Console.ReadLine(); // pazi na validaciju

            svc.AddServiceEndpoint(typeof(IServer), binding, new Uri(String.Format("net.tcp://localhost:{0}/Server", port)));
            svc.Open();

            Console.WriteLine("Otvorio");
        }
    }
}
