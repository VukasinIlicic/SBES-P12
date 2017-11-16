using Common;
using System;
using System.Collections.Generic;
using System.Linq;
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
            VezaSaAuditom.PoveziSe();

            Console.ReadLine();
        }
    }
}
