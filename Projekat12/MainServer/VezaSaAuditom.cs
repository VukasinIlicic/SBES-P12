using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common.Contracts;

namespace MainServer
{
    public class VezaSaAuditom
    {
        private static IAuditServer proxy;
        private static string kljucSesije;

        public static void PoveziSe()
        {
	        proxy = ClientProxy.GetProxy<IAuditServer>("localhost", "13000", "AuditServer");
            GenerisiKljucSesije();
        }

        private static void GenerisiKljucSesije()
        {
            string[] publicKey = proxy.DajKljuc(); // pk[0] = n   pk[1] = e;

            int min = 10000000;
            int max = 99999999;
            
            Random r = new Random();
            double kS = r.Next(min, max);  // kljuc sesije
            kljucSesije = kS.ToString();
            BigInteger b = BigInteger.ModPow((BigInteger)kS, (BigInteger)Convert.ToDouble(publicKey[1]), (BigInteger)Convert.ToDouble(publicKey[0]));

            proxy.PosaljiKljucSesije(b.ToString());
        }

        public static void PrijaviNeprijavljene(string neprijavljeni)
        {
            byte[] enkriptovaniNeprijavljeni = Enkripcija(neprijavljeni);
            proxy.PrijaviNeprijavljene(enkriptovaniNeprijavljeni);
        }

        private static byte[] Enkripcija(string original)
        {
            int razlika;

            if ((razlika = (original.Length % 8)) != 0)
            {
                razlika = 8 - razlika;

                /*while (razlika > 0)
                {
                    original += ';';
                    razlika--;
                }*/

                StringBuilder sb = new StringBuilder(original);
                sb.Append(';', razlika);

                original = sb.ToString();
            }

            byte[] org = Encoding.ASCII.GetBytes(original);

            DESCryptoServiceProvider desCrypto = new DESCryptoServiceProvider();
            desCrypto.Mode = CipherMode.ECB;
            desCrypto.Padding = PaddingMode.None;
            desCrypto.Key = Encoding.ASCII.GetBytes(kljucSesije);

            ICryptoTransform desEncript = desCrypto.CreateEncryptor();
            MemoryStream memoryStream = new MemoryStream();

            CryptoStream cryptoStream = new CryptoStream(memoryStream, desEncript, CryptoStreamMode.Write);

            cryptoStream.Write(org, 0, original.Length);

            byte[] kriptovanaPor = memoryStream.ToArray();
            return kriptovanaPor;
        }
    }
}

