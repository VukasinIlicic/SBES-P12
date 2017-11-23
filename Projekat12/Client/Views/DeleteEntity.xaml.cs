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
    /// Interaction logic for DeleteEntity.xaml
    /// </summary>
    public partial class DeleteEntity : UserControl, INotifyPropertyChanged
    {
        public IServer proxy;
        private string validation = "";
        public event PropertyChangedEventHandler PropertyChanged;

        public DeleteEntity(IServer proxy)
        {
            InitializeComponent();
            this.proxy = proxy;
            SetIdComboBox();
            this.DataContext = this;
        }

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
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

        private void DeleteConsumer(object sender, RoutedEventArgs e)
        {
            string id = IdComboBox.Text;
            bool deleted = false;
            Validation = "";
            CheckImg.Visibility = Visibility.Hidden;

            if (id=="")
            {
                Validation = "Invalid input.";
                return;
            }

            try
            {
                deleted = proxy.ObrisiEntitet(id);
            }
            catch(Exception ex)
            {
                var AuthException = ex as FaultException<AuthorizationException>;
                if (AuthException == null)
                {
                    MessageBox.Show("Server has not responded. Application will shutdown now.");
                    Environment.Exit(0);
                }
                else
                {
                    Validation = "Access denied";
                    return;
                }

                Validation = "Unsuccess operation.";
                return;
            }

            if(!deleted)
            {
                Validation = "There is no user with this id.";
            }

            SetIdComboBox();
            CheckImg.Visibility = Visibility.Visible;
        }

        public List<string> GetAllConsumersId(Dictionary<string, DataObj> consumers)
        {
            List<string> idList = new List<string>();

            foreach (KeyValuePair<string, DataObj> kv in consumers)
            {
                if(!kv.Value.Obrisan)
                    idList.Add(kv.Value.Id);
            }

            return idList;
        }

        public void SetIdComboBox()
        {
            Dictionary<string, DataObj> consumers = proxy.PrikazInformacija();
            List<string> idList = GetAllConsumersId(consumers);
            IdComboBox.ItemsSource = idList;
        }
    }
}
