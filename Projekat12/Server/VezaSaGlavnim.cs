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
	public class VezaSaGlavnim : IUpdate
	{
		private static IMainServer proxy;
        private readonly XmlRepository _xR = new XmlRepository();
		public static void PoveziSe(string adresa)
		{
			proxy = ClientProxy.GetProxy<IMainServer>(adresa, "51000", "MainServer", AuthType.WinAuth);
			string myIp = IPAdressHelper.VratiIP();
			Program.lokalnaBaza = proxy.PosaljiSvojePodatke(myIp, Program.portServeraZaGlavni, Formatter.ParseName(WindowsIdentity.GetCurrent().Name));

            if (Program.lokalnaBaza == null)
                Environment.Exit(0);

            XmlRepository xR = new XmlRepository();
            xR.UpisiUXml(Program.lokalnaBaza, Program.IME_LOKALNE_BAZE);
        }

        public Dictionary<string, DataObj> IntegrityUpdate()
        {
            Audit.IntegrityUpdate(Program.customLog);
            Program.tajm = true;
            return Program.lokalnaBaza;
        }

        public void VratiKonzistentnuBazu(Dictionary<string, DataObj> glavna)
        {
            lock (ServerClass.lockObject)
            {                
                Dictionary<string, bool[]> pomocniDic = NapraviDic();
                Program.tajm = false; // ovo je bilo ispod lock
                Program.mb.Merge(Program.lokalnaBaza, glavna, Konstanta.MERGE_SA_LOKALNIM);
                Program.lokalnaBaza = glavna;
                ProveraAzuriranjaUTajmu(pomocniDic);
                _xR.UpisiUXml(Program.lokalnaBaza, Program.IME_LOKALNE_BAZE);
            }            
        }

        private static Dictionary<string, bool[]> NapraviDic()
        {
            Dictionary<string, bool[]> dic = new Dictionary<string, bool[]>();

            foreach (var lb in Program.lokalnaBaza)
            {
                bool[] pomocni = new bool[12];
                for (int i = 0; i < 12; i++)
                    pomocni[i] = lb.Value.AzuriranUTajmu[i];

                if (!dic.ContainsKey(lb.Key))
                    dic.Add(lb.Key, pomocni);
            }

            return dic;
        }

        private static void ProveraAzuriranjaUTajmu(Dictionary<string, bool[]> dic)
        {
            foreach (var lb in Program.lokalnaBaza)
            {
                if (!dic.ContainsKey(lb.Key))
                    continue;

                bool[] hehe = dic[lb.Key];
                for (int i = 0; i < 12; i++)
                    if (hehe[i])
                    {
                        lb.Value.Azuriran[i] = true;
                        lb.Value.AzuriranUTajmu[i] = false;
                    }
            }
        }
    }
}