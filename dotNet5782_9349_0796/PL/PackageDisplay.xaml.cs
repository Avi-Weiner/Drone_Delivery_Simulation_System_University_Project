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

namespace PL
{
    /// <summary>
    /// Interaction logic for PackageDisplay.xaml
    /// </summary>
    public partial class PackageDisplay : Window
    {

        BlApi.IBL bl;
        BL.Package package;
        public PackageDisplay(BlApi.IBL BL, BL.Package Package)
        {
            package = Package;
            bl = BL;
            InitializeComponent();
            PackageView.Text = package.ToString();
            
        }

        private void Close_ButtonClick(object sender, RoutedEventArgs e)
        {

            Close();

        }
    }
}
