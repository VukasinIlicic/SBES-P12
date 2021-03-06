﻿using Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Contracts;
using Common.Entiteti;
using Common.Authorization;
using System.Threading;
using System.Security;
using System.ServiceModel;

namespace Server
{
    [ServiceBehavior]
    public class ServerClass : IServer
    {
        public static readonly Object lockObject = new Object();
        private readonly XmlRepository _xR = new XmlRepository();

        public List<string> GetRoles()
        {
            CustomPrincipal principal = Thread.CurrentPrincipal as CustomPrincipal;

            return principal.Roles;
        }

        public bool AzurirajPotrosnju(string id, int month_, double consumption)
        {
            var principal = Thread.CurrentPrincipal as CustomPrincipal;

            if (!principal.IsInRole("editor")) throw new FaultException<AuthorizationException>(new AuthorizationException() { Message = "Access Denied" });

            if (consumption < 0)
            {
                return false;
            }

            try
            {
                lock (lockObject)
                {
                    if (Program.tajm)
                        Program.lokalnaBaza[id].AzuriranUTajmu[month_] = true;

                    Program.lokalnaBaza[id].Potrosnja[month_] = consumption;
                    Program.lokalnaBaza[id].AzurirajPotrosnju(month_, true);
                    Audit.AzuriranjePotrosnje(Program.customLog);
                    _xR.UpisiUXml(Program.lokalnaBaza, Program.IME_LOKALNE_BAZE);

                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DodajEntitet(DataObj noviPotrosac)
        {
            var principal = Thread.CurrentPrincipal as CustomPrincipal;

            if (!principal.IsInRole("admin")) throw new FaultException<AuthorizationException>(new AuthorizationException() { Message = "Access Denied" });

            if (Program.lokalnaBaza.ContainsKey(noviPotrosac.Id))
            {
                if (Program.lokalnaBaza[noviPotrosac.Id].Obrisan == false)
                    return false;

                lock (lockObject)
                {
                    Program.lokalnaBaza.Remove(noviPotrosac.Id);
                    noviPotrosac.AzuriranCeo = true;
                }
            }

            try
            {
                lock (lockObject)
                {
                    if (Program.tajm)
                        noviPotrosac.DodatUTajmu = true;

                    Program.lokalnaBaza.Add(noviPotrosac.Id, noviPotrosac);
                }
                Audit.DodavanjeEntiteta(Program.customLog);
                _xR.UpisiUXml(Program.lokalnaBaza, Program.IME_LOKALNE_BAZE);
                return true;
            }
            catch { }


            return false;
        }

        public bool ObrisiEntitet(string id)
        {
            var principal = Thread.CurrentPrincipal as CustomPrincipal;

            if (!principal.IsInRole("admin")) throw new FaultException<AuthorizationException>(new AuthorizationException() { Message = "Access Denied" });

            if (!Program.lokalnaBaza.ContainsKey(id)) return false;

            try
            {
                lock (lockObject)
                {
                    Program.lokalnaBaza[id].Obrisan = true;
                }
                Audit.BrisanjeEntiteta(Program.customLog);
                _xR.UpisiUXml(Program.lokalnaBaza, Program.IME_LOKALNE_BAZE);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Dictionary<string, DataObj> PrikazInformacija()
        {
            var principal = Thread.CurrentPrincipal as CustomPrincipal;

            if (!principal.IsInRole("reader")) throw new FaultException<AuthorizationException>(new AuthorizationException() { Message = "Access Denied" });

            return Program.lokalnaBaza;
        }

        public double SrednjaVrednostPotrosnje(string grad, int year)
        {
            var principal = Thread.CurrentPrincipal as CustomPrincipal;

            if (!principal.IsInRole("reader")) throw new FaultException<AuthorizationException>(new AuthorizationException() { Message = "Access Denied" });

            Dictionary<string, DataObj> info = Program.lokalnaBaza;
            List<DataObj> objectList = new List<DataObj>();

            foreach (KeyValuePair<string, DataObj> kv in info)
            {
                if (kv.Value.Grad == grad && kv.Value.Godina == year && kv.Value.Obrisan == false)
                {
                    objectList.Add(kv.Value);
                }
            }

            double retVal = AnnualConsumption(objectList);
            return retVal;
        }

        private double AnnualConsumption(List<DataObj> data)
        {
            double ac = 0;
            foreach (DataObj obj in data)
            {
                double personalConsumption = 0;
                for (int i = 0; i < obj.Potrosnja.Count; i++)
                {
                    personalConsumption += obj.Potrosnja[i];
                }

                personalConsumption /= obj.Potrosnja.Count;
                ac += personalConsumption;
            }

            if (data.Count != 0)
                ac /= data.Count;

            return ac;
        }
    }
}
