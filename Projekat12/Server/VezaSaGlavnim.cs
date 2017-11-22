using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common.Contracts;
using Common.Helpers;
using Common.Entiteti;

namespace Server
{
	public class VezaSaGlavnim
	{
		private static IMainServer proxy;

		public static void PoveziSe(string adresa)
		{
            proxy = ClientProxy.GetProxy<IMainServer>(adresa, "51000", "MainServer", AuthType.WinAuth);
            string myIp = IPAdressHelper.VratiIP();
            proxy.PosaljiSvojePodatke(myIp, Program.portServera, Formatter.ParseName(WindowsIdentity.GetCurrent().Name));
        }
    }
}