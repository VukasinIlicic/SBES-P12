﻿using Common;
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

		public static void PoveziSe(string adresa)
		{
<<<<<<< HEAD
			proxy = ClientProxy.GetProxy<IMainServer>("localhost", "51000", "MainServer", AuthType.WinAuth);
			string myIp = IPAdressHelper.VratiIP();
			proxy.PosaljiSvojePodatke(myIp, Program.portServeraZaGlavni, Formatter.ParseName(WindowsIdentity.GetCurrent().Name));
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
=======
            proxy = ClientProxy.GetProxy<IMainServer>(adresa, "51000", "MainServer", AuthType.WinAuth);
            string myIp = IPAdressHelper.VratiIP();
            proxy.PosaljiSvojePodatke(myIp, Program.portServera, Formatter.ParseName(WindowsIdentity.GetCurrent().Name));
>>>>>>> bdab9792c6b5984797338dde9d5970ed63f1a318
        }
    }
}