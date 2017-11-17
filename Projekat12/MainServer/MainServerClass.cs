using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MainServer
{
    public class MainServerClass : IMainServer
    {
        static string imeBaze = "Baza.xml";
        private static readonly Object lockObject = new Object();
        static bool okidac = false;
        static DateTime vreme;
        static int sekunde = 2;

        public Dictionary<string, DataObj> IntegrityUpdate(Dictionary<string, DataObj> lokalnaBazaServera, string imeServera)
        {
            if (!Program.sviServeri.ContainsKey(imeServera))
                Program.sviServeri.Add(imeServera, false);

            if (!okidac)
            {
                vreme = DateTime.Now.AddSeconds(sekunde + 1);
                okidac = true;
            }

            Program.sviServeri[imeServera] = true;
                
            foreach (var lbs in lokalnaBazaServera)
            {
                if (!Program.glavnaBaza.ContainsKey(lbs.Key) && lbs.Value.Obrisan == false) // dodavanje
                {
                    lock (lockObject)
                    {
                        try
                        {
                            Program.glavnaBaza.Add(lbs.Key, lbs.Value);
                        }
                        catch { }
                    }
                }
                else if (lbs.Value.Obrisan == true && Program.glavnaBaza.ContainsKey(lbs.Key))  // brisanje
                {
                    lock (lockObject)
                    {
                        try
                        {
                            Program.glavnaBaza.Remove(lbs.Key);
                        }
                        catch { }
                    }
                }
            }

            Thread.Sleep(sekunde * 1000);  // cekamo da svi zavrse kako bi glavna baza bila ista za sve
            
            return Program.glavnaBaza;  
        }

        public static void Provera()
        {
            while (!okidac)
                Thread.Sleep(500);

            while (DateTime.Now < vreme)
                Thread.Sleep(500);

            List<string> neprijavljeni = new List<string>();

            foreach (var server in Program.sviServeri)
            {
                if (server.Value == false)
                    neprijavljeni.Add(server.Key);
                else
                    Program.sviServeri[server.Key] = false;
            }

            if (neprijavljeni.Count > 0)
                VezaSaAuditom.PrijaviNeprijavljene(neprijavljeni);

            okidac = false;
        }

        public static void UpisiUXml(Dictionary<string, DataObj> dic)
        {
            using (StreamWriter streamWriter = new StreamWriter(imeBaze))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Dictionary<string, DataObj>));
                xmlSerializer.Serialize(streamWriter, dic);
            }
        }

        public static Dictionary<string, DataObj> IscitajIzXml()
        {
            Dictionary<string, DataObj> dic;

            using (StreamReader streamReader = new StreamReader(imeBaze))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Dictionary<string, DataObj>));
                dic = (Dictionary<string, DataObj>)serializer.Deserialize(streamReader);
            }

            return dic;
        }
    }
}
