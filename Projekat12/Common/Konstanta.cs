using Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Konstanta
    {
        public const int Vreme_Azuriranja = 30;
        public const int MERGE_SA_GLAVNIN = 0;
        public const int MERGE_SA_LOKALNIM = 1;
	    public const string SRV_CERT_CN = "wcfservice"; // ovde se menja
		public const string IME_BAZE = "Baza.xml";
        public readonly string IME_LOKALNE_BAZE = String.Format("{0}_Baza.xml", Formatter.ParseName(WindowsIdentity.GetCurrent().Name));

	}
}
