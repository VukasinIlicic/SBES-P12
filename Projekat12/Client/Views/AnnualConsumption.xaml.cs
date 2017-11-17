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

            AnnualConsume = proxy.SrednjaVrednostPotrosnje(cityName, year_);

            try
            {
                StringResult = (AnnualConsume.ToString()).Remove(7);
            }
            catch
            {
                StringResult = "0.0";
            }
        }
    }
}
