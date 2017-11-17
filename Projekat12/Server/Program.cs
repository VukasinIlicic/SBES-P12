using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Server
{
    class Program
    {

        static ServiceHost svc;
        public static Dictionary<string, DataObj> lokalnaBaza = new Dictionary<string, DataObj>();
        static DispatcherTimer dt = new DispatcherTimer();
        static int sekunde = 30;

        static void Main(string[] args)
        {
            //VezaSaAuditom.PoveziSe();
            //Audit.KreirajAudit("LogovanjaServera", WindowsIdentity.GetCurrent().Name);

            dt.Tick += Dt_Tick; // krenuce u razlicitom vremenu, pazi kod mainServera kako onda da budu konzistentni
            dt.Interval = TimeSpan.FromSeconds(sekunde);
            dt.Start();

            Console.ReadLine();
            dt.Stop();
            svc.Close();
        }

        private static void Dt_Tick(object sender, EventArgs e)
        {
            VezaSaGlavnim.IntegrityUpdate();
        }

        public static void OtvoriServer()
        {
            NetTcpBinding binding = new NetTcpBinding();
            svc = new ServiceHost(typeof(ServerClass));
            svc.AddServiceEndpoint(typeof(IServer), binding, new Uri("net.tcp://localhost:31000/Server"));
            svc.Open();

            Console.WriteLine("Otvorio");
        }
    }
}
