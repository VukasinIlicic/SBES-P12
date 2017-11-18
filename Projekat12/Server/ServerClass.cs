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
            if(consumption<0)
            {
                return false;
            }

            //Audit.AzuriranjePotrosnje(Program.customLog);
            int month_ = ConvertMonthToIndex(month);

            try
            {
                Program.lokalnaBaza[id].Potrosnja[month_] = consumption;
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
                    catch
                    {
                        return false;
                    }
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

        private int ConvertMonthToIndex(string month)
        {
            switch(month)
            {
                case "January":
                    return 0;
                case "February":
                    return 1;
                case "March":
                    return 2;
                case "April":
                    return 3;
                case "May":
                    return 4;
                case "June":
                    return 5;
                case "July":
                    return 6;
                case "August":
                    return 7;
                case "September":
                    return 8;
                case "October":
                    return 9;
                case "November":
                    return 10;
                case "December":
                    return 11;
                default:
                    return 0;
            }
        }
    }
}
