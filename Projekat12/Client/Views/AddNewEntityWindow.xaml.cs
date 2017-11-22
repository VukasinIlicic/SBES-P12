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

namespace Client.Views
{
    /// <summary>
    /// Interaction logic for AddNewEntityWindow.xaml
    /// </summary>
    public partial class AddNewEntityWindow : UserControl, INotifyPropertyChanged
    {
        public IServer proxy;
        private string validation = "";
        public event PropertyChangedEventHandler PropertyChanged;

        public AddNewEntityWindow(IServer proxy)
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

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            string id = this.ConsumerIdTxtBox.Text;
            string region = this.ConsumerRegionTxtBox.Text;
            string city = this.CityTxtBox.Text;
            string year = this.YearTxtBox.Text;
            int year_;

            //Svaki put kad uradi click, ukloni validaciju
            Validation = "";
            CheckImg.Visibility = Visibility.Hidden;

            if (id=="" || region=="" || city=="" || year=="")
            {
                Validation = "Invalid input.";
                return;
            }

            try
            {
                year_ = Convert.ToInt32(year);

                if (year_ < 0)
                {
                    Validation = "Year can't be negative number.";
                    return;
                }

                DataObj newConsumer = new DataObj(id, region, city, year_);
                bool added = false;

                try
                {
                    added = proxy.DodajEntitet(newConsumer);
                }
                catch
                {

                }

                if (!added)
                {
                    Validation = "Database contains user with this id.";
                }
                else
                {
                    CheckImg.Visibility = Visibility.Visible;
                }

                return;
            }
            catch
            {
                Validation = "Year must be a number.";
                return;
            }
        }
    }
}
