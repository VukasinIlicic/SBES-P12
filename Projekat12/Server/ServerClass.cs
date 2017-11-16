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
        public bool AzurirajPotrosnju(DataObj potrosac)
        {
            Audit.AzuriranjePotrosnje();
            return true;
        }

        public bool DodajEntitet(DataObj noviPotrosac)
        {
            Audit.DodavanjeEntiteta();
            return true;
        }

        public bool ObrisiEntitet(string id)
        {
            Audit.BrisanjeEntiteta();
            return true;
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
