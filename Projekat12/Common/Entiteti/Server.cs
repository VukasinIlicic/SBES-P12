using Common.Contracts;

namespace Common.Entiteti
{
	public class Server
	{
		public IServer Proxy { get; set; }

		public string Ime { get; set; }

		public bool JavioSe { get; set; }
	}
}