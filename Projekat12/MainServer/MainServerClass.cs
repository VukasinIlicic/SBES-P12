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
		private static ConcurrentDictionary<string, Server> serveri = new ConcurrentDictionary<string, Server>();

		public static void Provera()
		{
			while (true)
			{
				Thread.Sleep(Konstanta.Vreme_Azuriranja*1000);

				string neprijavljeni = "";

				foreach (var server in serveri)
				{
					try
					{
						var lokalnaBazaServera = server.Value.Proxy.IntegrityUpdate();
						server.Value.JavioSe = true;
						Program.mb.Merge(lokalnaBazaServera, Program.glavnaBaza, Konstanta.MERGE_SA_GLAVNIN);
					}
					catch (Exception e)
					{
						neprijavljeni += server.Key + ';';
					}
				}
                }

                Program.mb.obrisani.Clear();

                if (neprijavljeni != "")
                {
                    //VezaSaAuditom.PrijaviNeprijavljene(neprijavljeni);
                    Console.WriteLine(neprijavljeni);
                }

                Thread.Sleep(5000);

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
			if (serveri.ContainsKey(imeServera)) return;

			if (adresa.Equals(IPAdressHelper.VratiIP()))
				adresa = "localhost";

			DodajProxy(adresa, port.ToString(), imeServera);
		}

		private void DodajProxy(string adresa, string port, string imeServera)
		{
			serveri.TryAdd(imeServera, new Server() {Ime = imeServera, Proxy = ClientProxy.GetProxy<IServer>(adresa, port, "Server")});
		}
	}
}