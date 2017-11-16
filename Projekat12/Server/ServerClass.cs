using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ServerClass : IServer
    {
        public string Proba(string message)
        {
            Console.WriteLine("Stigla poruka: {0}", message);
            return "Gde si Vule";
        }
    }
}
