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

        public bool AzurirajPotrosnju(DataObj potrosac)
        {
            //Audit.AzuriranjePotrosnje();
            return true;
        }

        public bool DodajEntitet(DataObj noviPotrosac)
        {
            foreach (var potrosac in Program.lokalnaBaza)
                if (potrosac.Id == noviPotrosac.Id)
                    return false;

            lock(lockObject)
            {
                Program.lokalnaBaza.Add(noviPotrosac);
            }
            //Audit.DodavanjeEntiteta();
            return true;
        }

        public bool ObrisiEntitet(string id)
        {
            foreach (var potrosac in Program.lokalnaBaza)
                if (potrosac.Id == id)
                {
                    lock(lockObject)
                    {
                        potrosac.Obrisan = true;
                    }
                    //Audit.BrisanjeEntiteta();
                    return true;
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
