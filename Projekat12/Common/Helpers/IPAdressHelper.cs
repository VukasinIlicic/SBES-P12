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

			var ipAddress = hostEntry.AddressList.LastOrDefault(address => address.IsIPv6LinkLocal.Equals(false));
			if (ipAddress != null)
				myIP = ipAddress.ToString();

			return myIP;
		}
	}
}