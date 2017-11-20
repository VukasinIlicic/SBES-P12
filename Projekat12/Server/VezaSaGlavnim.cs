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
                Dictionary<string, bool[]> pomocniDic = NapraviDic();
                Program.mb.Merge(Program.lokalnaBaza, rezultatGlavneBaze, Konstanta.MERGE_SA_LOKALNIM);
                Program.lokalnaBaza = rezultatGlavneBaze;
                ProveraAzuriranjaUTajmu(pomocniDic);
            }

        }

        private static Dictionary<string, bool[]> NapraviDic()
        {
            Dictionary<string, bool[]> dic = new Dictionary<string, bool[]>();

            foreach(var lb in Program.lokalnaBaza)
            {
                bool[] pomocni = new bool[12];
                for (int i = 0; i < 12; i++)
                    pomocni[i] = lb.Value.AzuriranUTajmu[i];

                if(!dic.ContainsKey(lb.Key))
                    dic.Add(lb.Key, pomocni);
            }

            return dic; 
        }

        private static void ProveraAzuriranjaUTajmu(Dictionary<string, bool[]> dic)
        {
            foreach(var lb in Program.lokalnaBaza)
            {
                if (!dic.ContainsKey(lb.Key))
                    continue;

                bool[] hehe = dic[lb.Key];
                for(int i = 0; i < 12; i++)
                    if(hehe[i])
                    {
                        lb.Value.Azuriran[i] = true;
                        lb.Value.AzuriranUTajmu[i] = false;
                    }
            }
        }
        

    }
}
