using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Contracts;

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
                Program.privateKey.Add(klijent.Name, new string[2] { kljucevi[0], kljucevi[2] });
            }

            return new string[2] { kljucevi[0], kljucevi[1] };
        }

        public void PosaljiKljucSesije(string m)
        {
            var klijent = (WindowsIdentity)Thread.CurrentPrincipal.Identity;
            double kljucSesije = (double)BigInteger.ModPow((BigInteger)Convert.ToDouble(m), (BigInteger)Convert.ToDouble(Program.privateKey[klijent.Name][1]), (BigInteger)Convert.ToDouble(Program.privateKey[klijent.Name][0]));

            Program.kljucSesije = kljucSesije.ToString();
        }

        public void PrijaviNeprijavljene(byte[] neprijavljeni)
        {
            string dekriptovaniNeprijavljeni = Dekripcija(neprijavljeni, Program.kljucSesije);

            string[] serveri = dekriptovaniNeprijavljeni.Split(';');

            string poruka = "";

            foreach (var server in serveri)
                if(server != "")
                    poruka += String.Format("Server {0} se nije javio\n", server);

            Console.WriteLine(poruka);
            //Audit.AuditServerLog(Program.customLog, poruka);
        }

        private static string Dekripcija(byte[] enkriptovani, string kljuc)
        {
            DESCryptoServiceProvider desCrypto = new DESCryptoServiceProvider();
            desCrypto.Mode = CipherMode.ECB;
            desCrypto.Padding = PaddingMode.None;
            desCrypto.Key = Encoding.ASCII.GetBytes(kljuc);

            ICryptoTransform desEncript = desCrypto.CreateDecryptor();
            Stream stream = new MemoryStream(enkriptovani);

            CryptoStream cryptoStream = new CryptoStream(stream, desEncript, CryptoStreamMode.Read);

            /*byte[] dekriptovana = new byte[stream.Length];
            cryptoStream.Read(dekriptovana, 0, dekriptovana.Length);

            string poruka = Encoding.ASCII.GetString(dekriptovana);*/

            StreamReader reader = new StreamReader(cryptoStream);
            string poruka = reader.ReadToEnd();

            return poruka;
        }
    }
}
