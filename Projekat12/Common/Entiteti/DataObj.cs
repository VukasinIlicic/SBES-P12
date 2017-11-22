using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace Common.Entiteti
{
    [Serializable]
    [DataContract]
    public class DataObj
    {
        private string id;
        private string grad;
        private int godina;
        private bool obrisan;
        private string region;
        private bool dodatUTajmu;
        private bool azuriranCeo;
        private List<bool> azuriran = Enumerable.Range(0, 12).Select(i => false).ToList();
        List<bool> azuriranUTajmu = Enumerable.Range(0, 12).Select(i => false).ToList();
        private List<double> potrosnja = new List<double>();

        #region Getters_and_setters
        [DataMember]
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        [DataMember]
        [DefaultValue("")]
        public string Grad
        {
            get
            {
                return grad;
            }
            set
            {
                grad = value;
            }
        }

        [DataMember]
        public int Godina
        {
            get
            {
                return godina;
            }
            set
            {
                godina = value;
            }
        }

        [DataMember]
        public bool Obrisan
        {
            get
            {
                return obrisan;
            }
            set
            {
                obrisan = value;
            }
        }

        [DataMember]
        public string Region
        {
            get
            {
                return region;
            }
            set
            {
                region = value;
            }
        }

        [DataMember]
        [DefaultValue(false)]
        public bool DodatUTajmu
        {
            get
            {
                return dodatUTajmu;
            }
            set
            {
                dodatUTajmu = value;
            }
        }

        [DataMember]
        [DefaultValue(false)]
        public bool AzuriranCeo
        {
            get
            {
                return azuriranCeo;
            }
            set
            {
                azuriranCeo = value;
            }
        }

        [DataMember]
        public List<bool> Azuriran
        {
            get
            {
                return azuriran;
            }
            set
            {
                azuriran = value;
            }
        }

        [DataMember]
        public List<bool> AzuriranUTajmu
        {
            get
            {
                return azuriranUTajmu;
            }
            set
            {
                azuriranUTajmu = value;
            }
        }

        [DataMember]
        public List<double> Potrosnja
        {
            get
            {
                return potrosnja;
            }
            set
            {
                potrosnja = value;
            }
        }

        //[DataMember]
        //public double Treci
        //{
        //    get
        //    {
        //        return potrosnja[0];
        //    }
        //    set
        //    {
        //        potrosnja[0] = value;
        //    }
        //}

        #endregion

        #region Constructors
        public DataObj()
        {

        }

        public DataObj(string id, string region, string grad, int godina, List<double> potrosnja)
        {
            this.id = id;
            this.godina = godina;
            this.grad = grad;
            this.region = region;
            this.potrosnja = potrosnja;
            this.obrisan = false;
        }

        //Konstruktor koji generise automatski potrosnju za svaki mesec u godini
        public DataObj(string id, string region, string grad, int godina)
        {
            this.id = id;
            this.grad = grad;
            this.region = region;
            this.godina = godina;
            this.Potrosnja = GenerateRandomConsumption();
            //this.azuriran = NapraviAzurirane();
            this.obrisan = false;
        }
        #endregion

        /*private List<bool> NapraviAzurirane()
        {
            List<bool> pomocna = new List<bool>();
            for (int i = 0; i < 12; i++)
                pomocna[i] = false;

            return pomocna;
        }*/

        public List<double> GenerateRandomConsumption()
        {
            Random rnd = new Random();
            List<double> consumptions = new List<double>();
            for(int i=0; i<12; i++)
            {
                double newConsumption = rnd.NextDouble() * 1000;
                consumptions.Add(newConsumption);
            }

            return consumptions;
        }

        public bool AzuriranaPotrosnja(int indeks)
        {
            return azuriran[indeks];
        }

        public void AzurirajPotrosnju(int indeks, bool vrednost)
        {
            azuriran[indeks] = vrednost;
        }
    }
}
