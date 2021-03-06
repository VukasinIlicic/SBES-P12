﻿using Client.Views;
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
using Common.Contracts;
using System.Windows.Threading;

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
            InitializeComponent();
            HideMenu();
            StartClock();
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
            _mainFrame.NavigationService.Navigate(new SetConsumptionWindow(proxy));
        }

        private void AddNewEntity(object sender, RoutedEventArgs e)
        {
            _mainFrame.NavigationService.Navigate(new AddNewEntityWindow(proxy));
        }

        private void DeleteConsumer(object sender, RoutedEventArgs e)
        {
            _mainFrame.NavigationService.Navigate(new DeleteEntity(proxy));
        }
        private void HideMenu()
        {
            SearchInfo.Visibility = Visibility.Collapsed;
            AnnualConsumptions.Visibility = Visibility.Collapsed;
            NewConsumtion.Visibility = Visibility.Collapsed;
            AddConsumerBtn.Visibility = Visibility.Collapsed;
            DeleteConsumerBtn.Visibility = Visibility.Collapsed;
        }

        private void StartClock()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TickEvent;
            timer.Start();
        }

        private void TickEvent(object sender, EventArgs e)
        {
            datelbl.Text = DateTime.Now.ToString();
        }
    }
}
