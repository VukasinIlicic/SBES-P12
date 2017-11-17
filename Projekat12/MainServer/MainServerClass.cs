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

        public Dictionary<string, DataObj> IntegrityUpdate(Dictionary<string, DataObj> lokalnaBazaServera)
        {
            Dictionary<string, DataObj> glavnaBaza = IscitajIzXml();

            return glavnaBaza;
        }

        public static void UpisiUXml(Dictionary<string, DataObj> dic)
        {
            using (StreamWriter streamWriter = new StreamWriter(imeBaze))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Dictionary<string, DataObj>));
                xmlSerializer.Serialize(streamWriter, dic);
            }
        }

        public static Dictionary<string, DataObj> IscitajIzXml()
        {
            Dictionary<string, DataObj> dic;

            using (StreamReader streamReader = new StreamReader(imeBaze))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Dictionary<string, DataObj>));
                dic = (Dictionary<string, DataObj>)serializer.Deserialize(streamReader);
            }

            return dic;
        }
    }
}
