using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Otvorio");
            //VezaSaAuditom.PoveziSe();
            Audit.KreirajAudit("LogovanjaServera", WindowsIdentity.GetCurrent().Name);
            Audit.AzuriranjePotrosnje();
            Audit.DodavanjeEntiteta();
            Audit.BrisanjeEntiteta();
            Console.ReadLine();
        }
    }
}
