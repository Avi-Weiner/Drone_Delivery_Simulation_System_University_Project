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
        public DroneList(BlApi.IBL BLObj)
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

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(bl);
            mainWindow.Show();
            Close();
            
        }

        private void DroneListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(DroneListView.SelectedItem.GetType().ToString());
            BL.DroneToList drone = (BL.DroneToList)DroneListView.SelectedItem;
            int x = drone.Id;
            DroneDisplay droneDisplayWindow = new DroneDisplay(bl.DroneToListToDrone(x), bl);
            droneDisplayWindow.Show();
            Close();
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            StatusSelector.Items.Add(AllDrones);
        }
    }
}
