using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AuditServer
{
    public class AuditServerClass : IAuditServer
    {
        public string[] DajKljuc()
        {
            var klijent = (WindowsIdentity)Thread.CurrentPrincipal.Identity;

            string[] kljucevi = Program.GenerisanjeRSAParametara(); // vraca n, e, d

            if(!Program.privateKey.ContainsKey(klijent.Name))
            {
                Program.privateKey.Add(klijent.Name, new string[2] { kljucevi[0], kljucevi[2] });
            }    
            else
            {
                Program.privateKey.Remove(klijent.Name);
                Program.kljuceviSesija.Remove(klijent.Name);
                Program.privateKey.Add(klijent.Name, new string[2] { kljucevi[0], kljucevi[2] });
            }

            return new string[2] { kljucevi[0], kljucevi[1] };
        }

        public void PosaljiKljucSesije(string m)
        {
            var klijent = (WindowsIdentity)Thread.CurrentPrincipal.Identity;
            double kljucSesije = (double)BigInteger.ModPow((BigInteger)Convert.ToDouble(m), (BigInteger)Convert.ToDouble(Program.privateKey[klijent.Name][1]), (BigInteger)Convert.ToDouble(Program.privateKey[klijent.Name][0]));  

            if (!Program.kljuceviSesija.ContainsKey(klijent.Name))
                Program.kljuceviSesija.Add(klijent.Name, kljucSesije);

            Console.WriteLine("Klijent: {0}\nKljuc sesije: {1}", klijent.Name, Program.kljuceviSesija[klijent.Name]);
        }

        public void PrijaviNeprijavljene(List<string> neprijavljeni)
        {
            string poruka = "";

            foreach (var server in neprijavljeni)
                poruka += String.Format("Server {0} se nije javio", server);

            Audit.AuditServerLog(Program.customLog, poruka);
        }
    }
}
