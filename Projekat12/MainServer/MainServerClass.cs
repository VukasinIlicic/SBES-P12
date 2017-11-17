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

        public Dictionary<string, DataObj> IntegrityUpdate(Dictionary<string, DataObj> lokalnaBazaServera)
        {
        
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

            Thread.Sleep(2 * 1000);  // cekamo da svi zavrse kako bi glavna baza bila ista za sve
            
            return Program.glavnaBaza;  
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
