using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common.Contracts;

namespace Server
{
    public class VezaSaGlavnim 
    {
        static IMainServer proxy;

        public static void PoveziSe()
        {
            NetTcpBinding binding = new NetTcpBinding();
            ChannelFactory<IMainServer> factory = new ChannelFactory<IMainServer>(binding, new EndpointAddress("net.tcp://localhost:51000/MainServer")); // pazi na localmachine
            proxy = factory.CreateChannel();
        }

        public static void PosaljiPodatke()
        {
            string myIp = IPAdressHelper.VratiIP();
            proxy.PosaljiSvojePodatke(myIp, Program.portServera, Formatter.ParseName(WindowsIdentity.GetCurrent().Name)); // dodat Formater
        }
           
    }
}
