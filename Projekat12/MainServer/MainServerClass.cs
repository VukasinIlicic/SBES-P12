﻿using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Common.Contracts;

namespace MainServer
{
    public class MainServerClass : IMainServer
    {
        static string imeBaze = "Baza.xml";
        private static readonly Object lockObject = new Object();
        private static readonly Object lockObject2 = new Object();
        static List<string> imenaServera = new List<string>();

        public Dictionary<string, DataObj> IntegrityUpdate(Dictionary<string, DataObj> lokalnaBazaServera, string imeServera)
        {
            if (!Program.sviServeri.ContainsKey(imeServera))
            {
                Program.sviServeri.Add(imeServera, false);
                imenaServera.Add(imeServera);
            }

            lock (lockObject2)
            {
                Program.sviServeri[imeServera] = true;
            }

            Program.mb.Merge(lokalnaBazaServera, Program.glavnaBaza, Konstanta.MERGE_SA_GLAVNIN);

            Thread.Sleep(11 * 1000);  // cekamo da svi zavrse kako bi glavna baza bila ista za sve

            Program.mb.obrisani.Clear();
            return Program.glavnaBaza;

        } 

        public static void Provera()
        {
            while(true)
            {
                while ((DateTime.Now.Second % Konstanta.Vreme_Azuriranja) != 0) //!okidac
                    Thread.Sleep(300);

                Thread.Sleep(3 * 1000);

                string neprijavljeni = "";

                lock(lockObject2)
                {
                    foreach (var server in imenaServera)
                        if (Program.sviServeri[server] == false)
                            neprijavljeni += server + ';';
                        else
                            Program.sviServeri[server] = false;
                }
                
                if (neprijavljeni != "")
                {   
                    VezaSaAuditom.PrijaviNeprijavljene(neprijavljeni);
                }

                Console.WriteLine("Proverio");
            }    
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
