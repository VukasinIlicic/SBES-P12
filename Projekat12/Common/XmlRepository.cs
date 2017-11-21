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
		public void UpisiUXml(Dictionary<string, DataObj> podaci)
		{
			using (var streamWriter = new StreamWriter(Konstanta.IME_BAZE))
			{
				var xmlSerializer = new XmlSerializer(typeof (Dictionary<string, DataObj>));
				xmlSerializer.Serialize(streamWriter, podaci);
			}
		}

		public Dictionary<string, DataObj> IscitajIzXml()
		{
			Dictionary<string, DataObj> podaci;

			using (var streamReader = new StreamReader(Konstanta.IME_BAZE))
			{
				var serializer = new XmlSerializer(typeof (Dictionary<string, DataObj>));
				podaci = (Dictionary<string, DataObj>) serializer.Deserialize(streamReader);
			}

			return podaci;
		}
	}
}