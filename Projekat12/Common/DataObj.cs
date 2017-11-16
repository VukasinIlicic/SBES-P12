using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class DataObj
    {
        private string id;
        private string grad;
        private int godina;
        private double potrosnja;

        #region Getters_and_setters
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

        public double Potrosnja
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
        #endregion

        #region Constructors
        public DataObj()
        {

        }

        public DataObj(string id, string grad, int godina, double potrosnja)
        {
            this.id = id;
            this.godina = godina;
            this.grad = grad;
            this.potrosnja = potrosnja;
        }
        #endregion
    }
}
