using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {

        static ServiceHost svc;
        public static Dictionary<string, DataObj> lokalnaBaza = new Dictionary<string, DataObj>();

        static void Main(string[] args)
        {
            //VezaSaAuditom.PoveziSe();
            //Audit.KreirajAudit("LogovanjaServera", WindowsIdentity.GetCurrent().Name);

            Console.ReadLine();
            svc.Close();
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
