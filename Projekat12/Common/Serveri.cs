using Common.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Serveri
    {
        IServer proxy;
        string ime;
        bool javioSe;

        public Serveri()
        {

        }

        public IServer Proxy
        {
            get
            {
                return proxy;
            }

            set
            {
                proxy = value;
            }
        }

        public string Ime
        {
            get
            {
                return ime;
            }

            set
            {
                ime = value;
            }
        }

        public bool JavioSe
        {
            get
            {
                return javioSe;
            }

            set
            {
                javioSe = value;
            }
        }
    }
}
