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

        public AnnualConsumption(IServer proxy)
        {
            InitializeComponent();
            this.proxy = proxy;
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
            if (cityName == "")
            {
                Validation = "Invalid input.";
                return;
            }
            else
            {
                proxy.SrednjaVrednostPotrosnje(cityName);
            }
        }
    }
}
