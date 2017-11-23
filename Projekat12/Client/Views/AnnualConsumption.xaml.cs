using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Common.Contracts;
using Common.Entiteti;
using System.ServiceModel;

namespace Client.Views
{
    /// <summary>
    /// Interaction logic for AnnualConsumption.xaml
    /// </summary>
    public partial class AnnualConsumption : UserControl, INotifyPropertyChanged
    {
        public IServer proxy;
        private string validation = "";
        private double annualConsumption;
        private string stringResult;

        public AnnualConsumption(IServer proxy)
        {
            InitializeComponent();
            this.proxy = proxy;

            try
            {
                Dictionary<string, DataObj> dic = proxy.PrikazInformacija();
            }
            catch
            {
               
            }

            this.DataContext = this;
        }

        public string Validation
        {
            get
            {
                return validation;
            }

            set
            {
                validation = value;
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("Validation"));
            }
        }

        public double AnnualConsume
        {
            get
            {
                return annualConsumption;
            }

            set
            {
                annualConsumption = value;
            }
        }

        public string StringResult
        {
            get
            {
                return stringResult;
            }

            set
            {
                stringResult = value;
                OnPropertyChanged(new System.ComponentModel.PropertyChangedEventArgs("StringResult"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        private void GetConsumptionsBtn_Click(object sender, RoutedEventArgs e)
        {
            string cityName = this.CityNameTxtBox.Text;
            string year = this.YearTxtBox.Text;
            int year_;

            Validation = "";

            if (cityName == "" || year == "")
            {
                Validation = "Invalid input.";
                return;
            }

            try
            {
                year_ = Convert.ToInt32(year);
            }
            catch
            {
                Validation = "Invalid input.";
                return;
            }

            try
            {
                AnnualConsume = proxy.SrednjaVrednostPotrosnje(cityName, year_);
            }
            catch(Exception ex)
            {
                var AuthException = ex as FaultException<AuthorizationException>;
                if (AuthException == null)
                {
                    MessageBox.Show("Server has not responded. Application will shutdown now.");
                    Environment.Exit(0);
                }
            }

            if((AnnualConsume.ToString()).Length>8)
            {
                StringResult = (AnnualConsume.ToString()).Remove(7);
            }
            else
            {
                StringResult = AnnualConsume.ToString();
            }
        }
    }
}
