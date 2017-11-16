using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common
{
    public class Audit
    {
        static EventLog customLog;
        
        public static void KreirajAudit(string logName, string sourceName)
        {
            Console.WriteLine(sourceName);
            try
            {
                if (!EventLog.SourceExists(sourceName))
                    EventLog.CreateEventSource(sourceName, logName);

                customLog = new EventLog(logName, Environment.MachineName, sourceName);
            }
            catch (Exception e)
            {
                customLog = null;
                Console.WriteLine("Error while trying to create log handle. Error = {0}", e.Message);
            }
        }

        public static void AzuriranjePotrosnje()
        {
            UpisivanjeLoga("azurirao potrosnju");
        }

        public static void DodavanjeEntiteta()
        {
            UpisivanjeLoga("dodao entitet");
        }

        public static void BrisanjeEntiteta()
        {
            UpisivanjeLoga("obrisao entitet");
        }

        private static void UpisivanjeLoga(string nastavakPoruke)
        {
            if (customLog != null)
            {
                var klijent = WindowsIdentity.GetCurrent();

                string poruka = String.Format("Klijent: {0} je {1}", klijent.Name, nastavakPoruke);

                customLog.WriteEntry(poruka, EventLogEntryType.Information);
            }
            else
                Console.WriteLine("CustomLog je null");
        }

    }
}
