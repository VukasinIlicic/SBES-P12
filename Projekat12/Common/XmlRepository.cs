using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Common.Entiteti;

namespace Common
{
	public class XmlRepository
	{
		public void UpisiUXml(Dictionary<string, DataObj> podaci, string imeBaze)
		{
			using (var streamWriter = new StreamWriter(imeBaze))
			{
				var xmlSerializer = new XmlSerializer(typeof (Dictionary<string, DataObj>));
				xmlSerializer.Serialize(streamWriter, podaci);
			}
		}

		public Dictionary<string, DataObj> IscitajIzXml(string imeBaze)
		{
			Dictionary<string, DataObj> podaci;

			using (var streamReader = new StreamReader(imeBaze))
			{
				var serializer = new XmlSerializer(typeof (Dictionary<string, DataObj>));
				podaci = (Dictionary<string, DataObj>) serializer.Deserialize(streamReader);
			}

			return podaci;
		}
	}
}