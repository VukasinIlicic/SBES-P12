using Common.Contracts;

namespace Common.Entiteti
{
	public class Server
	{
		public IUpdate Proxy { get; set; }

		public string Ime { get; set; }

		public bool JavioSe { get; set; }
	}
}