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
        private static Dictionary<string, DataObj> rezultatGlavneBaze = new Dictionary<string, DataObj>();

        public static void PoveziSe()
        {
            NetTcpBinding binding = new NetTcpBinding();
            ChannelFactory<IMainServer> factory = new ChannelFactory<IMainServer>(binding, new EndpointAddress("net.tcp://localhost:51000/MainServer"));
            proxy = factory.CreateChannel();
        }

        public static void IntegrityUpdate()
        {
            rezultatGlavneBaze = proxy.IntegrityUpdate(Program.lokalnaBaza, WindowsIdentity.GetCurrent().Name);        
            lock(ServerClass.lockObject)
            {
                //Program.mb.Merge(rezultatGlavneBaze, Program.lokalnaBaza);
                //Program.lokalnaBaza = rezultatGlavneBaze;
                Program.lokalnaBaza = rezultatGlavneBaze;
            }
            


            //Program.lokalnaBaza = proxy.IntegrityUpdate(Program.lokalnaBaza, WindowsIdentity.GetCurrent().Name);
        }
    }
}
