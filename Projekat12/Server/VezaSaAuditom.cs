using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class VezaSaAuditom
    {
        private static IAuditServer proxy;
        private static double kljucSesije; 

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

            double max = (Convert.ToDouble(publicKey[0]) - 1);
            int min = 1001;

            Random r = new Random();
            double m = r.NextDouble() * (max - min) + min;  // kljuc sesije
            kljucSesije = m = Math.Floor(m);
            BigInteger b = BigInteger.ModPow((BigInteger)m, (BigInteger)Convert.ToDouble(publicKey[1]), (BigInteger)Convert.ToDouble(publicKey[0]));

            Console.WriteLine("Kljuc sesije: {0}", m);
            proxy.PosaljiKljucSesije(b.ToString());
        }
    }
}
