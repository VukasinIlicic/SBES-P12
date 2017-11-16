using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MainServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Audit.KreirajAudit("Proba", WindowsIdentity.GetCurrent().Name);
            Audit.AzuriranjePotrosnje();

            Console.ReadLine();
        }

        public static void UpisiUXml(List<DataObj> lista)
        {
            using (StreamWriter streamWriter = new StreamWriter("Baza.xml"))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<DataObj>));
                xmlSerializer.Serialize(streamWriter, lista);
            }
        }

        public static List<DataObj> IscitajIzXml()
        {
            List<DataObj> lista;

            using (StreamReader streamReader = new StreamReader("Baza.xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<DataObj>));
                lista = (List<DataObj>)serializer.Deserialize(streamReader);
            }

            return lista;
        }
    }
}
