using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MainServer
{
    public class MainServerClass : IMainServer
    {
        static string imeBaze = "Baza.xml";

        public List<DataObj> IntegrityUpdate(List<DataObj> lokalnaBazaServera)
        {
            List<DataObj> lista = IscitajIzXml();
            return lista;
        }

        public static void UpisiUXml(List<DataObj> lista)
        {
            using (StreamWriter streamWriter = new StreamWriter(imeBaze))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<DataObj>));
                xmlSerializer.Serialize(streamWriter, lista);
            }
        }

        public static List<DataObj> IscitajIzXml()
        {
            List<DataObj> lista;

            using (StreamReader streamReader = new StreamReader(imeBaze))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<DataObj>));
                lista = (List<DataObj>)serializer.Deserialize(streamReader);
            }

            return lista;
        }
    }
}
