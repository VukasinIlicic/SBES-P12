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
        private static readonly Object lockDic = new object();
        private static string neprijavljeni = "";

        public static void Provera()
		{
			while (true)
			{
				Thread.Sleep(Konstanta.Vreme_Azuriranja*1000);

				neprijavljeni = "";

                int brojac = 0;
                Thread[] tredovi = new Thread[serveri.Count]; // sta ako ovde bude 2 pa dodje jos jedan dok ne stigne do foreach ... mozda je najbolje da se koristi lock ili da se brojac stavi na serveri.Count pa u foreach ako bude manje od 0 izadji

                foreach (var server in serveri)
                {
                    tredovi[brojac] = new Thread(() => TreadFunkcija(server));
                    tredovi[brojac].Start();
                    brojac++;
                }
                
                for (int i = 0; i < brojac; i++)
                {
                    tredovi[i].Join();
                    //tredovi[i].Abort();
                }

                if (neprijavljeni != "")            // kad se jednom ugasi stalno ce posle da ide u catch cak i isti port da ima, mozda ovde da izbacimo iz serveri sve one koji su ispali ( ??? )
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
                }
            }           
        }

        public void PosaljiSvojePodatke(string adresa, int port, string imeServera)
		{
			if (serveri.ContainsKey(imeServera))
                return;

			if (adresa.Equals(IPAdressHelper.VratiIP()))
				adresa = "localhost";

			DodajProxy(adresa, port.ToString(), imeServera);
		}

		private void DodajProxy(string adresa, string port, string imeServera)
		{
			serveri.TryAdd(imeServera, new Server() {Ime = imeServera, Proxy = ClientProxy.GetProxy<IServer>(adresa, port, "Server")}); // ako proba da doda u serveri a gore se radi foreach nad njim da li to znaci da onda nece dodati ili da ce cekati dok se foreach ne zavrsi ??
		}
	}
}