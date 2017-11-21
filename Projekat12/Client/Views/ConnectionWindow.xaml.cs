using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
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
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Security;
using Common.CertManager;
using Common.Contracts;

namespace Client.Views
{
    /// <summary>
    /// Interaction logic for ConnectionWindow.xaml
    /// </summary>
    public partial class ConnectionWindow : UserControl, INotifyPropertyChanged
    {
        public IServer proxy;
        private string validation = "";
        private MainWindow mw;

        public ConnectionWindow(MainWindow mw)
        {
            InitializeComponent();
            this.DataContext = this;
            this.mw = mw;
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

        public void ConnectToServer(string adresa, string port)
        {
            InitializeComponent();

	        proxy = ClientProxy.GetProxy<IServer>(adresa, port, "Server", true);
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            string endpointName = ServerAddress.Text;
            string port = ServerPort.Text;
            int portNo;

            Validation = "";

            if (endpointName == "" || port == "")
            {
                Validation = "Your input is invalid.";
                return;
            }

            try
            {
                portNo = Convert.ToInt32(port);   
            }
            catch
            {
                Validation = "Port must be a number.";
                return;
            }

            try
            {
                ConnectToServer(endpointName, portNo.ToString());
                ConnectionCheck();
                mw.ServerConnection.Visibility = Visibility.Hidden;
                mw.Proxy = proxy;
                ShowMenu();
                mw._mainFrame.NavigationService.Navigate(new ShowInfo(proxy));
            }
            catch
            {
                Validation = "Port or server endpoint name is invalid.";
                return;
            }
        }

        private void ConnectionCheck()
        {
            proxy.PrikazInformacija();
        }

        private void CheckBoxCheck(object sender, RoutedEventArgs e)
        {
            ServerAddress.Text = "localhost";
            ServerAddress.IsReadOnly = true;
        }

        private void CheckBoxUncheck(object sender, RoutedEventArgs e)
        {
            ServerAddress.Text = "";
            ServerAddress.IsReadOnly = false;
        }

        private void ShowMenu()
        {
            mw.SearchInfo.Visibility = Visibility.Visible;
            mw.AnnualConsumptions.Visibility = Visibility.Visible;
            mw.NewConsumtion.Visibility = Visibility.Visible;
            mw.AddConsumerBtn.Visibility = Visibility.Visible;
            mw.DeleteConsumerBtn.Visibility = Visibility.Visible;
        }
    }
}
