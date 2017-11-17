using Client.Views;
using Common;
using System;
using System.Collections.Generic;
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

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IServer proxy;

        public MainWindow()
        {
            

        }

        public IServer Proxy
        {
            get
            {
                return proxy;
            }

            set
            {
                proxy = value;
            }
        }

        private void ServerConnectionWindow(object sender, RoutedEventArgs e)
        {
            _mainFrame.NavigationService.Navigate(new ConnectionWindow(this));
        }

        private void InformationSearchWindow(object sender, RoutedEventArgs e)
        {
            _mainFrame.NavigationService.Navigate(new ShowInfo(proxy));  
        }

        private void AnnualConsumptionsWindow(object sender, RoutedEventArgs e)
        {
            _mainFrame.NavigationService.Navigate(new AnnualConsumption(proxy));
        }

        private void NewConsumptionWindow(object sender, RoutedEventArgs e)
        {
            _mainFrame.NavigationService.Navigate(new SetConsumptionWindow());
        }

        private void AddNewEntity(object sender, RoutedEventArgs e)
        {
            _mainFrame.NavigationService.Navigate(new AddNewEntityWindow(proxy));
        }
    }
}
