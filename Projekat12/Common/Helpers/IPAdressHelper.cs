using System.Linq;
using System.Net;

namespace Common.Helpers
{
	public class IPAdressHelper
	{
		public static string VratiIP()
		{
			string myHost = Dns.GetHostName();
			string myIP = null;
			var hostEntry = Dns.GetHostEntry(myHost);

			//for (int i = 0; i <= hostEntry.AddressList.Length - 1; i++)
			//{
			//	if (hostEntry.AddressList[i].IsIPv6LinkLocal == false) // sta ako ne udje .. :D
			//	{
			//		myIP = hostEntry.AddressList[i].ToString();
			//	}
			//}
			var ipAddress = hostEntry.AddressList.LastOrDefault(address => address.IsIPv6LinkLocal.Equals(false));
			if (ipAddress != null)
				myIP = ipAddress.ToString();

			return myIP;
		}
	}
}