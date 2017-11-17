using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class DataObj
    {
        private string id;
        private string grad;
        private int godina;
        private bool obrisan;
        private string region;
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
        }

        //Konstruktor koji generise automatski potrosnju za svaki mesec u godini
        public DataObj(string id, string region, string grad, int godina)
        {
            this.id = id;
            this.grad = grad;
            this.region = region;
            this.godina = godina;
            this.Potrosnja = GenerateRandomConsumption();
        }
        #endregion

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
    }
}
