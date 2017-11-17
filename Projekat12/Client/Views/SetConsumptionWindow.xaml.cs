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
    /// Interaction logic for SetConsumptionWindow.xaml
    /// </summary>
    public partial class SetConsumptionWindow : UserControl, INotifyPropertyChanged
    {
        public IServer proxy;
        private string validation = "";
        public event PropertyChangedEventHandler PropertyChanged;

        public SetConsumptionWindow(IServer proxy)
        {
            InitializeComponent();
            this.DataContext = this;
            this.proxy = proxy;
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
            string consumption = ConsumptionTxtBox.Text;
            string month = comboBox.Text;
            string userId = IdTxtBox.Text;
            double consumption_;
            bool edited;

            Validation = "";

            if(consumption=="" || month=="" || userId=="")
            {
                Validation = "Invalid input.";
                return;
            }

            try
            {
                consumption_ = Convert.ToDouble(consumption);
                edited = proxy.AzurirajPotrosnju(userId, month, consumption_);
            }
            catch
            {
                Validation = "Consumption must be a number.";
                return;
            }

            if(!edited)
            {
                Validation = "Consumption isn't changed.";
                return;
            }
        }
    }
}
