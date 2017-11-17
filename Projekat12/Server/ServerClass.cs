using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ServerClass : IServer
    {
        private static readonly Object lockObject = new Object();

        public bool AzurirajPotrosnju(string id, string month, double consumption)
        {
            //Audit.AzuriranjePotrosnje(Program.customLog);
            return true;
        }

        public bool DodajEntitet(DataObj noviPotrosac)
        {
            if(Program.lokalnaBaza.ContainsKey(noviPotrosac.Id))
                    return false;

            lock(lockObject)
            {
                try
                {
                    Program.lokalnaBaza.Add(noviPotrosac.Id, noviPotrosac);
                    //Audit.DodavanjeEntiteta(Program.customLog);
                    return true;
                }
                catch { }
            }
            
            return false;
        }

        public bool ObrisiEntitet(string id)
        {
            if(Program.lokalnaBaza.ContainsKey(id))
            {
                lock(lockObject)
                {
                    try
                    {
                        Program.lokalnaBaza[id].Obrisan = true;
                        //Audit.BrisanjeEntiteta(Program.customLog);
                        return true;
                    }
                    catch { }
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
                if(kv.Value.Grad == grad && kv.Value.Godina == year)
                {
                    objectList.Add(kv.Value);
                }
            }

            double retVal = AnnualConsumption(objectList);
            return retVal;
        }

        public double AnnualConsumption(List<DataObj> data)
        {
            double ac = 0;
            foreach(DataObj obj in data)
            {
                double personalConsumption = 0;
                for(int i=0; i<obj.Potrosnja.Count; i++)
                {
                    personalConsumption += obj.Potrosnja[i];
                }

                personalConsumption /= obj.Potrosnja.Count;
                ac += personalConsumption;
            }

            ac /= data.Count;
            return ac;
        }
    }
}
