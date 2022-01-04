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
        BlApi.IBL bl;
        public List<string> ListType { get; set; }
        
        public DroneList(BlApi.IBL BLObj)
        {

            InitializeComponent();
            //StatusSelector.SelectedItem = AllDrones;
            //DroneListView.ItemsSource = BLObj.DroneListFilter("free");
            bl = BLObj;
            
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StatusSelector.SelectedItem == null)
                return;
            string x = StatusSelector.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last();
            if ((bool)Stations.IsChecked)
                DroneListView.ItemsSource = bl.StationListFilter(x);
            else if ((bool)Drones.IsChecked)
                DroneListView.ItemsSource = bl.DroneListFilter(x);
            else if ((bool)Customers.IsChecked)
                DroneListView.ItemsSource = bl.ListOfCustomers();
            

        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(bl);
            mainWindow.Show();
            Close();
            
        }

        private void DroneListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string Type = DroneListView.SelectedItem.GetType().ToString();
            //MessageBox.Show(DroneListView.SelectedItem.GetType().ToString());
            switch(Type)
            {
                case "BL.DroneToList":
                    BL.DroneToList drone = (BL.DroneToList)DroneListView.SelectedItem;
                    int x = drone.Id;
                    DroneDisplay droneDisplayWindow = new DroneDisplay(bl.DroneToListToDrone(x), bl);
                    droneDisplayWindow.Show();
                    Close();
                    break;
                case "BL.BaseStationToList":
                    BL.BaseStationToList station = (BL.BaseStationToList)DroneListView.SelectedItem;
                    int y = station.Id;
                    StationDisplay stationDisplayWindow = new StationDisplay(bl.DalToBlStation(y), bl);
                    stationDisplayWindow.Show();
                    Close();
                    break;
                case "BL.CustomerToList":
                    BL.CustomerToList customer = (BL.CustomerToList)DroneListView.SelectedItem;
                    int z = customer.Id;
                    CustomerDisplay CustomerDisplayWindow = new CustomerDisplay(bl.DalToBlCustomer(z), bl);
                    CustomerDisplayWindow.Show();
                    Close();
                    break;

            }
            //BL.DroneToList drone = (BL.DroneToList)DroneListView.SelectedItem;
            //int x = drone.Id;
            //DroneDisplay droneDisplayWindow = new DroneDisplay(bl.DroneToListToDrone(x), bl);
            //droneDisplayWindow.Show();
            //Close();
        }

        /// <summary>
        /// Opens an add drone screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Drone_ButtonClick(object sender, RoutedEventArgs e)
        {
            DroneDisplay droneDisplayWindow = new DroneDisplay(bl);
            droneDisplayWindow.Show();
        }

        /// <summary>
        /// This function will simply reselect the item currently selected which will 
        /// inevitebly refresh the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {
            //if there was no selection yet nothing should be refreshed.
            if (StatusSelector.SelectedItem == null)
                return;
            string x = StatusSelector.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last();
            DroneListView.ItemsSource = bl.DroneListFilter(x);
        }

        private void Stations_Checked(object sender, RoutedEventArgs e)
        {
            ListType = new List<string>()
            {
                "All Stations",
                "Available Charge Slots"
            };
            StatusSelector.ItemsSource = ListType;
        }

        private void Drones_Checked(object sender, RoutedEventArgs e)
        {
            //StatusSelector.Items.Clear();
            ListType = new List<string>()
            {
                "All Drones",
                "Free Drones",
                "Maintenance Drones",
                "DeliveryDrones",
                "Light",
                "Medium",
                "Heavy"
            };
            StatusSelector.ItemsSource = ListType;
            
           
        }

        private void Customers_Checked(object sender, RoutedEventArgs e)
        {
            ListType = new List<string>()
            {
                "All Customers"
            };
            StatusSelector.ItemsSource = ListType;
        }

        private void Packages_Checked(object sender, RoutedEventArgs e)
        {
            ListType = new List<string>()
            {
                "All Packages",
                "Unassigned Packages"
            };
            StatusSelector.ItemsSource = ListType;
        }

        private void Station_Click(object sender, RoutedEventArgs e)
        {
            StationDisplay stationDisplayWindow = new StationDisplay(bl);

            stationDisplayWindow.Show();
            Close();
        }
    }
}
