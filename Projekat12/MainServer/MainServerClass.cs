using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Common.Contracts;
using System.ServiceModel;
using System.Collections.Concurrent;
using Common.Entiteti;
using Common.Helpers;

namespace MainServer
{
    public class MainServerClass : IMainServer
    {
        static ConcurrentDictionary<string, Server> serveri = new ConcurrentDictionary<string, Server>();

        public static void Provera()
        {
            while(true)
            {
                Thread.Sleep(Konstanta.Vreme_Azuriranja * 1000);

                string neprijavljeni = "";

                foreach (var s in serveri)
                {
                    Dictionary<string, DataObj> lokalna = null;
                    try
                    {
                        lokalna = s.Value.Proxy.IntegrityUpdate();
                        s.Value.JavioSe = true;
                        Program.mb.Merge(lokalna, Program.glavnaBaza, Konstanta.MERGE_SA_GLAVNIN);
                    }
                    catch(Exception e)
                    {
                        neprijavljeni += s.Key + ';';
                    }
                }
                  
                if (neprijavljeni != "")
                {
                    //VezaSaAuditom.PrijaviNeprijavljene(neprijavljeni);
                    Console.WriteLine(neprijavljeni);
                }

                foreach(var s in serveri)
                {
                    if(s.Value.JavioSe)
                    {
                        s.Value.JavioSe = false;
                        s.Value.Proxy.VratiKonzistentnuBazu(Program.glavnaBaza);
                    }
                }
            }    
        }

        public void PosaljiSvojePodatke(string adresa, int port, string imeServera)
        {
            if (!serveri.ContainsKey(imeServera))
            {
                if (adresa.Equals(IPAdressHelper.VratiIP()))
                    adresa = String.Format("localhost:{0}", port.ToString());
                else
                    adresa += ':' + port.ToString();

                DodajProxy(adresa, imeServera);
            }    
        }

        private void DodajProxy(string adresa, string imeServera)
        {
            NetTcpBinding binding = new NetTcpBinding();
            ChannelFactory<IServer> factory = new ChannelFactory<IServer>(binding, new EndpointAddress(String.Format("net.tcp://{0}/Server", adresa)));
            serveri.TryAdd(imeServera, new Server() { Ime = imeServera, Proxy = factory.CreateChannel()});  
        }
    }
}
