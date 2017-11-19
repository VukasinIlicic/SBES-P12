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
            this.proxy = proxy;

            Dictionary<string, DataObj> consumers = proxy.PrikazInformacija();
            List<string> idList = GetAllConsumersId(consumers);
            IdComboBox.ItemsSource = idList;
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
            string consumption = ConsumptionTxtBox.Text;
            string month = comboBox.Text;
            string userId = IdComboBox.Text;
            double consumption_;
            bool edited;

            Validation = "";
            CheckImg.Visibility = Visibility.Hidden;

            if (consumption=="" || month=="" || userId=="")
            {
                Validation = "Invalid input.";
                return;
            }

            try
            {
                consumption_ = Convert.ToDouble(consumption);
                edited = proxy.AzurirajPotrosnju(userId, month, consumption_);
                CheckImg.Visibility = Visibility.Visible;
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

        public List<string> GetAllConsumersId(Dictionary<string,DataObj> consumers)
        {
            List<string> idList = new List<string>();

            foreach(KeyValuePair<string,DataObj> kv in consumers)
            {
                if(!kv.Value.Obrisan)
                    idList.Add(kv.Value.Id);
            }

            return idList;
        }
    }
}
