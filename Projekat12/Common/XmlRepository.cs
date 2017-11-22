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
			var listaPodataka = podaci.Select(p => p.Value).ToList();

			using (var streamWriter = new StreamWriter(imeBaze))
			{
				try
				{
					var xmlSerializer = new XmlSerializer(typeof (List<DataObj>));
					xmlSerializer.Serialize(streamWriter, listaPodataka);
				}
				catch
				{
					Console.WriteLine("Neuspesno upisivanje u xml");
				}
			}
		}

		public Dictionary<string, DataObj> IscitajIzXml(string imeBaze)
		{
			var listaPodataka = new List<DataObj>();

			if (!File.Exists(imeBaze))
				return new Dictionary<string, DataObj>();

			using (var streamReader = new StreamReader(imeBaze))
			{
				try
				{
					var serializer = new XmlSerializer(typeof (List<DataObj>));
					listaPodataka = (List<DataObj>) serializer.Deserialize(streamReader);
				}
				catch
				{
					Console.WriteLine("Neuspesno citanje iz xml");
				}
			}
			var podaci = listaPodataka.ToDictionary(x => x.Id);

			return podaci;
		}
	}
}