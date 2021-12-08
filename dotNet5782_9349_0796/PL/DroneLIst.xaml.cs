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
    /// Interaction logic for DroneList.xaml
    /// </summary>
    public partial class DroneList : Window
    {
        IBL.IBL bl;
        public DroneList(IBL.IBL BLObj)
        {

            InitializeComponent();
            //StatusSelector.SelectedItem = AllDrones;
            //DroneListView.ItemsSource = BLObj.DroneListFilter("free");
            bl = BLObj;
        }
        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            string x = StatusSelector.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last();
            DroneListView.ItemsSource = bl.DroneListFilter(x);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DroneListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IBL.BO.DroneToList drone = (IBL.BO.DroneToList)DroneListView.SelectedItem;
            int x = drone.Id;
            DroneDisplay droneDisplayWindow = new DroneDisplay(bl.DroneToListToDrone(drone.Id));
            droneDisplayWindow.Show();
        }
    }
}
