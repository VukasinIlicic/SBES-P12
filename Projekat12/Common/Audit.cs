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
        public static EventLog KreirajAudit(string logName, string sourceName)
        {
            EventLog customLog;
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

            return customLog;
        }

        public static void AzuriranjePotrosnje(EventLog customLog)
        {
            UpisivanjeLoga("je azurirao potrosnju", customLog);
        }

        public static void DodavanjeEntiteta(EventLog customLog)
        {
            UpisivanjeLoga("je dodao entitet", customLog);
        }

        public static void BrisanjeEntiteta(EventLog customLog)
        {
            UpisivanjeLoga("je obrisao entitet", customLog);
        }

        public static void IntegrityUpdate(EventLog customLog)
        {
            UpisivanjeLoga("radi IntegrityUpdate", customLog);
        }

        private static void UpisivanjeLoga(string nastavakPoruke, EventLog customLog)
        {
            if (customLog != null)
            {
                var klijent = WindowsIdentity.GetCurrent();

                string poruka = String.Format("Klijent: {0} {1}", klijent.Name, nastavakPoruke);

                customLog.WriteEntry(poruka, EventLogEntryType.Information);    // mozda pokreces sa istog user-a 2 aplikacije
            }
            else
                Console.WriteLine("CustomLog je null");
        }

        public static void AuditServerLog(EventLog customLog, string poruka)
        {
            if (customLog != null)
            {
                var audit = WindowsIdentity.GetCurrent();

                customLog.WriteEntry(poruka, EventLogEntryType.Warning);
            }
            else
                Console.WriteLine("AuditLog je null");
        }

    }
}
