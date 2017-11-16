using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            ServiceHost svc = new ServiceHost(typeof(ServerClass));
            svc.AddServiceEndpoint(typeof(IServer), binding, new Uri("net.tcp://localhost:11000/Server"));
            svc.Open();

            Console.WriteLine("Otvorio");
            Console.ReadLine();
        }
    }
}
