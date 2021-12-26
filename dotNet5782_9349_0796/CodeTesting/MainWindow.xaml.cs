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
using BL;

namespace CodeTesting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BlApi.IBL BLObj;
        public MainWindow()
        {
            InitializeComponent();
            BLObj = BlFactory.GetBl();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Isn't this cool?");
            
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //avi.SelectedItem.ToString();
            //MessageBox.Show(avi.SelectedItem.ToString());
        }

        private void avi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        //    switch (avi.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
        //    {
        //        case "free":
        //            MessageBox.Show("this work avi");
        //            break;
        //        case "2":
        //            //Handle for the second combobox
        //            break;
        //        case "3":
        //            //Handle for the third combobox
        //            break;
        //    }
        }
    }
}
