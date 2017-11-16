using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class VezaSaGlavnim
    {
        static IMainServer proxy;

        public static void PoveziSe()
        {
            NetTcpBinding binding = new NetTcpBinding();
            ChannelFactory<IMainServer> factory = new ChannelFactory<IMainServer>(binding, new EndpointAddress("net.tcp://localhost:51000/MainServer"));
            proxy = factory.CreateChannel();
        }

        public static void IntegrityUpdate()
        {
            Program.lokalnaBaza = proxy.IntegrityUpdate(Program.lokalnaBaza);
        }
    }
}
