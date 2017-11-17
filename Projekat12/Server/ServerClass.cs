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
        
        public void PrikazInformacija()
        {
            throw new NotImplementedException();
        }

        public double SrednjaVrednostPotrosnje(string grad)
        {
            throw new NotImplementedException();
        }
    }
}
