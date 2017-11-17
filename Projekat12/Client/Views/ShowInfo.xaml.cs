using Common;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ShowInfo.xaml
    /// </summary>
    public partial class ShowInfo : UserControl
    {
        public ShowInfo(IServer p)
        {
            InitializeComponent();
            //DataObj dd = new DataObj("1", "Vojvodina", "NS", 2017);
            //Dictionary<string, DataObj> infoDic = new Dictionary<string, DataObj>();
            //infoDic.Add("1", dd);
            Dictionary<string,DataObj> infoDic = p.PrikazInformacija();
            List<DataObj> dataObjList = getDataObjList(infoDic);
            this.DataContext = dataObjList;
            lvDataBinding.ItemsSource = dataObjList;
        }

        public List<DataObj> getDataObjList(Dictionary<string,DataObj> data)
        {
            List<DataObj> returnList = new List<DataObj>();

            foreach(KeyValuePair<string, DataObj> kv in data)
            {
                if(!kv.Value.Obrisan)
                {
                    returnList.Add(kv.Value);
                }
            }

            return returnList;
        }
    }
}
