using Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Contracts;
using Common.Entiteti;

namespace Server
{
    public class ServerClass : IServer
    {
        public static readonly Object lockObject = new Object();

        public bool AzurirajPotrosnju(string id, int month_, double consumption)
        {
            if(consumption<0)
            {
                return false;
            }

            Audit.AzuriranjePotrosnje(Program.customLog);

            try
            {
                lock(lockObject)
                {
                    if (Program.tajm)
                        Program.lokalnaBaza[id].AzuriranUTajmu[month_] = true;

                    Program.lokalnaBaza[id].Potrosnja[month_] = consumption;
                    Program.lokalnaBaza[id].AzurirajPotrosnju(month_, true);
                }
                
                return true;
            }
            catch
            {
                return false;
            }       
        }

        public bool DodajEntitet(DataObj noviPotrosac)
        {
            if(Program.lokalnaBaza.ContainsKey(noviPotrosac.Id))
            {
                if(Program.lokalnaBaza[noviPotrosac.Id].Obrisan == false)   // izbrise pa doda isti, ali godina ostane losa (mozda neki bool za godinu pa da na glavnom vidimo da li je na true i onda izmenimo godinu)
                    return false;                                           // mora novo polje

                lock(lockObject)
                {
                    Program.lokalnaBaza.Remove(noviPotrosac.Id);
                    noviPotrosac.AzuriranCeo = true;
                }  
            }                    

            
            try
            {
                lock(lockObject)
                {
                    if (Program.tajm)
                        noviPotrosac.DodatUTajmu = true;

                    Program.lokalnaBaza.Add(noviPotrosac.Id, noviPotrosac);
                }
    
                Audit.DodavanjeEntiteta(Program.customLog);
                return true;
            }
            catch { }
            
            
            return false;
        }

        public bool ObrisiEntitet(string id)
        {
            if(Program.lokalnaBaza.ContainsKey(id))
            {
                try
                {
                    lock(lockObject)
                    {
                        Program.lokalnaBaza[id].Obrisan = true;
                    }
                        
                    Audit.BrisanjeEntiteta(Program.customLog);
                    return true;
                }
                catch
                {
                    return false;
                }
                
            }

            return false;
        }
        
        public Dictionary<string,DataObj> PrikazInformacija()
        {
            return Program.lokalnaBaza;
        }

        public double SrednjaVrednostPotrosnje(string grad, int year)
        {
            Dictionary<string, DataObj> info = Program.lokalnaBaza;
            List<DataObj> objectList = new List<DataObj>();

            foreach(KeyValuePair<string,DataObj> kv in info)
            {
                if(kv.Value.Grad == grad && kv.Value.Godina == year && kv.Value.Obrisan == false)
                {
                    objectList.Add(kv.Value);
                }
            }

            double retVal = AnnualConsumption(objectList);
            return retVal;
        }

        private double AnnualConsumption(List<DataObj> data)
        {
            double ac = 0;
            foreach (DataObj obj in data)
            {
                double personalConsumption = 0;
                for (int i = 0; i < obj.Potrosnja.Count; i++)
                {
                    personalConsumption += obj.Potrosnja[i];
                }


                personalConsumption /= obj.Potrosnja.Count;
                ac += personalConsumption;
            }

            if (data.Count != 0)
                ac /= data.Count;

            return ac;
        }

        public Dictionary<string, DataObj> IntegrityUpdate()
        {
            Audit.IntegrityUpdate(Program.customLog);
            Program.tajm = true;
            return Program.lokalnaBaza;
        }

        public void VratiKonzistentnuBazu(Dictionary<string, DataObj> glavna)
        {
            lock (ServerClass.lockObject)
            {
                Dictionary<string, bool[]> pomocniDic = NapraviDic();
                Program.mb.Merge(Program.lokalnaBaza, glavna, Konstanta.MERGE_SA_LOKALNIM);
                Program.lokalnaBaza = glavna;
                ProveraAzuriranjaUTajmu(pomocniDic);
            }
            Program.tajm = false; // ili mozda pre ovoga
        }

        private static Dictionary<string, bool[]> NapraviDic()
        {
            Dictionary<string, bool[]> dic = new Dictionary<string, bool[]>();

            foreach (var lb in Program.lokalnaBaza)
            {
                bool[] pomocni = new bool[12];
                for (int i = 0; i < 12; i++)
                    pomocni[i] = lb.Value.AzuriranUTajmu[i];

                if (!dic.ContainsKey(lb.Key))
                    dic.Add(lb.Key, pomocni);
            }

            return dic;
        }

        private static void ProveraAzuriranjaUTajmu(Dictionary<string, bool[]> dic)
        {
            foreach (var lb in Program.lokalnaBaza)
            {
                if (!dic.ContainsKey(lb.Key))
                    continue;

                bool[] hehe = dic[lb.Key];
                for (int i = 0; i < 12; i++)
                    if (hehe[i])
                    {
                        lb.Value.Azuriran[i] = true;
                        lb.Value.AzuriranUTajmu[i] = false;
                    }
            }
        }
    }
}
