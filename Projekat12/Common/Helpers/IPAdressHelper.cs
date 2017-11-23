using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Common.Helpers
{
	public class IPAdressHelper
	{
		public static string VratiIP()
		{
			string myHost = Dns.GetHostName();
			string myIP = null;
			var hostEntry = Dns.GetHostEntry(myHost);

			var ipAddress = hostEntry.AddressList.FirstOrDefault(address => address.AddressFamily.Equals(AddressFamily.InterNetwork));
			if (ipAddress != null)
				myIP = ipAddress.ToString();

			return myIP;
		}
	}
}