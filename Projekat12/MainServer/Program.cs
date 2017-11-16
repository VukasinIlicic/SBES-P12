using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MainServer
{
    class Program
    {
        static void Main(string[] args)
        {
            NetTcpBinding binding = new NetTcpBinding();
            Console.WriteLine("Unesi adresu");
            string adresa = Console.ReadLine();
            ChannelFactory<IServer> factory = new ChannelFactory<IServer>(binding, new EndpointAddress(String.Format("net.tcp://{0}:11000/Server", adresa)));
            IServer proxy = factory.CreateChannel();

            Console.WriteLine(proxy.Proba("caoooo"));
            Console.ReadLine();
        }
    }
}
