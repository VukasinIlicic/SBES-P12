using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        public void ConnectToServer(string serverName, string port)
        {
            InitializeComponent();
            NetTcpBinding binding = new NetTcpBinding();
            ChannelFactory<IServer> factory = new ChannelFactory<IServer>(binding, new EndpointAddress(String.Format("net.tcp://{0}:{1}/Server", serverName, port)));
            this.proxy = factory.CreateChannel();
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            string endpointName = ServerAddress.Text;
            string port = ServerPort.Text;
            int portNo;

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
                ConnectionCheck(proxy);
                mw.ServerConnection.Visibility = Visibility.Hidden;
                mw.Proxy = this.proxy;
                mw._mainFrame.NavigationService.Navigate(new ShowInfo(proxy));
            }
            catch
            {
                Validation = "Port or server endpoint name is invalid.";
                return;
            }
        }

        private void ConnectionCheck(IServer proxy)
        {
            proxy.PrikazInformacija();
        }
    }
}
