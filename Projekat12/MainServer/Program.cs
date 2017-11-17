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

namespace MainServer
{
    class Program
    {
        static ServiceHost svc;
        public static Dictionary<string, DataObj> glavnaBaza = new Dictionary<string, DataObj>();
        public static Dictionary<string, bool> sviServeri = new Dictionary<string, bool>();

        static void Main(string[] args)
        {
            //Audit.KreirajAudit("Proba", WindowsIdentity.GetCurrent().Name);
            OtvoriServer();
            Thread t1 = new Thread(MainServerClass.Provera);    // odavde pokrecemo proveru
            t1.Start();

            Console.ReadLine();
            t1.Abort();
            svc.Close();
        }

        public static void OtvoriServer()
        {
            NetTcpBinding binding = new NetTcpBinding();
            svc = new ServiceHost(typeof(MainServerClass));
            svc.AddServiceEndpoint(typeof(IMainServer), binding, new Uri("net.tcp://localhost:51000/MainServer"));
            svc.Open();

            Console.WriteLine("Otvorio");
        }

        
    }
}
