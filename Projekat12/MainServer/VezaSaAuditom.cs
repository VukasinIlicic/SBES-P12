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

namespace MainServer
{
    public class VezaSaAuditom
    {
        private static IAuditServer proxy;
        private static string kljucSesije;

        public static void PoveziSe()
        {
            NetTcpBinding binding = new NetTcpBinding();
            ChannelFactory<IAuditServer> factory = new ChannelFactory<IAuditServer>(binding, new EndpointAddress(String.Format("net.tcp://localhost:11000/AuditServer")));
            proxy = factory.CreateChannel();

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

