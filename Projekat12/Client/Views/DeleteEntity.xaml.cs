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
            string id = IdTxtBox.Text;
            bool deleted = false;
            Validation = "";

            if(id=="")
            {
                Validation = "Invalid input.";
                return;
            }

            try
            {
                deleted = proxy.ObrisiEntitet(id);
            }
            catch
            {
                Validation = "Unsuccess operation.";
                return;
            }

            if(!deleted)
            {
                Validation = "There is no user with this id.";
            }
        }
    }
}
