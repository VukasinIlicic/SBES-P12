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
        private static readonly Object lockNeprijavljeni = new Object();
        private static string neprijavljeni = "";

        public static void Provera()
		{
			while (true)
			{
				Thread.Sleep(Konstanta.Vreme_Azuriranja*1000);

				neprijavljeni = "";

                List<Thread> tredovi = new List<Thread>(serveri.Count);

                foreach (var server in serveri)
                {
                    tredovi.Add(new Thread(() => TreadFunkcija(server)));
                    tredovi[tredovi.Count - 1].Start();
                }

                for (int i = 0; i < tredovi.Count; i++)
                {
                    tredovi[i].Join();
                }


                if (neprijavljeni != "")            
                {
                    VezaSaAuditom.PrijaviNeprijavljene(neprijavljeni);
                }

                Program.mb.obrisani.Clear();
                
                foreach (var s in serveri)
				{
					if (s.Value.JavioSe)
					{
						s.Value.JavioSe = false;
						s.Value.Proxy.VratiKonzistentnuBazu(Program.glavnaBaza);
					}
				}
			}
		}

        private static void TreadFunkcija(KeyValuePair<string, Server> server)
        {
            Console.WriteLine(server.Key);
            try
            {
                var lokalnaBazaServera = server.Value.Proxy.IntegrityUpdate();
                server.Value.JavioSe = true;
                Program.mb.Merge(lokalnaBazaServera, Program.glavnaBaza, Konstanta.MERGE_SA_GLAVNIN);
            }
            catch
            {
                lock(lockNeprijavljeni)
                {
                    neprijavljeni += server.Key + ';';
                    Server outObjekat;
                    serveri.TryRemove(server.Key, out outObjekat);
                }
            }           
        }

        public void PosaljiSvojePodatke(string adresa, int port, string imeServera)
		{
			if (serveri.ContainsKey(imeServera))
                return;

			if (adresa.Equals(IPAdressHelper.VratiIP()))
				adresa = "localhost";

			DodajServer(adresa, port.ToString(), imeServera);
		}

		private void DodajServer(string adresa, string port, string imeServera)
		{
			serveri.TryAdd(imeServera, new Server() {Ime = imeServera, Proxy = ClientProxy.GetProxy<IUpdate>(adresa, port, "VezaSaGlavnim", AuthType.WinAuth) });
		}
	}
}