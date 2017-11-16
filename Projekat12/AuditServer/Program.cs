using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AuditServer
{
    class Program
    {
        static int minRandom = 50000;
        static int maxRandom = 550000;

        public static Dictionary<string, double> kljuceviSesija = new Dictionary<string, double>();
        public static Dictionary<string, string[]> privateKey = new Dictionary<string, string[]>();

        static void Main(string[] args)
        {
            OtvoriServer();

            Console.ReadLine();
        }

        public static string[] GenerisanjeRSAParametara()
        {
            // 1. korak
            Random r = new Random();
            int broj1 = r.Next(minRandom, maxRandom);
            int broj2 = r.Next(minRandom, maxRandom);

            while (!IsPrime(broj1))
                broj1++;

            while (!IsPrime(broj2))
                broj2++;

            if (broj1 == broj2)
                while (!IsPrime(broj2))
                    broj2++;

            // 2. korak
            double n = (double)broj1 * broj2;

            // 3. korak
            double mod = (double)(broj1 - 1) * (broj2 - 1);

            // 4. korak
            int e = r.Next(2, minRandom / 10);

            while (!IsPrime(e))
                e++;

            if ((mod % e) == 0)         // ovde mozes opet da ga vratis da trazi drugo e
                Console.WriteLine("Ne valja!!!");

            // 5. korak
            double d = MultiplicativeInverse(e, mod);

            return new string[3] { n.ToString(), e.ToString(), d.ToString() };
        }

        public static void OtvoriServer()
        {
            NetTcpBinding binding = new NetTcpBinding();
            ServiceHost svc = new ServiceHost(typeof(AuditServerClass));
            svc.AddServiceEndpoint(typeof(IAuditServer), binding, new Uri("net.tcp://localhost:11000/AuditServer"));
            svc.Open();

            Console.WriteLine("Otvorio");
        }

        public static bool IsPrime(int number)
        {
            if (number < 2)
                return false;
            if (number % 2 == 0)
                return (number == 2);

            int root = (int)Math.Sqrt((double)number);

            for (int i = 3; i <= root; i += 2)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }

        public static double MultiplicativeInverse(double e, double fi)
        {
            double result;
            int k = 1;
            while (true)
            {
                result = (1 + (k * fi)) / e;
                double hehe = Math.Round(result, 5);
                if ((Math.Round(result, 5) % 1) == 0) 
                    return Math.Floor(result);
                else
                    k++;
            }
        }
    }
}
