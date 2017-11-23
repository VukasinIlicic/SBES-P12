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
using System.Windows.Shapes;

namespace Client
{
    /// <summary>
    /// Interaction logic for ProzorPotrosnje.xaml
    /// </summary>
    public partial class ProzorPotrosnje : Window
    {
        public ProzorPotrosnje()
        {
            InitializeComponent();
        }

        public void Postavi(List<double> listaPotrosnje)
        {
            mesec1.Content = Mesec(listaPotrosnje[0]);
            mesec2.Content = Mesec(listaPotrosnje[1]);
            mesec3.Content = Mesec(listaPotrosnje[2]);
            mesec4.Content = Mesec(listaPotrosnje[3]);
            mesec5.Content = Mesec(listaPotrosnje[4]);
            mesec6.Content = Mesec(listaPotrosnje[5]);
            mesec7.Content = Mesec(listaPotrosnje[6]);
            mesec8.Content = Mesec(listaPotrosnje[7]);
            mesec9.Content = Mesec(listaPotrosnje[8]);
            mesec10.Content = Mesec(listaPotrosnje[9]);
            mesec11.Content = Mesec(listaPotrosnje[10]);
            mesec12.Content = Mesec(listaPotrosnje[11]);
        }

        public string Mesec(double potrosnja)
        {
            if (potrosnja.ToString().Length > 8)
                return potrosnja.ToString().Remove(7);

            return potrosnja.ToString();
        }
    }
}
