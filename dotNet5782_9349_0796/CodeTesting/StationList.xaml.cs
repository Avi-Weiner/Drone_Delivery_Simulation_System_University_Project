//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;

//namespace PL
//{
//    /// <summary>
//    /// Interaction logic for StationList.xaml
//    /// </summary>
//    public partial class StationList : Window
//    {
//        BlApi.IBL bl;
//        public StationList(BlApi.IBL BLObj)
//        {

//            InitializeComponent();
//            //StatusSelector.SelectedItem = AllDrones;
//            //DroneListView.ItemsSource = BLObj.DroneListFilter("free");
//            bl = BLObj;
//        }
//        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
//        {

//            string x = StatusSelector.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last();
//            DroneListView.ItemsSource = bl.StationListFilter(x);

//        }

//        private void Button_Click(object sender, RoutedEventArgs e)
//        {
//            MainWindow m = new MainWindow(bl);
//            m.Show();
//            Close();
//        }

//        private void DroneListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
//        {
//            BL.BaseStationToList station = (BL.BaseStationToList)DroneListView.SelectedItem;
//            int x = station.Id;
//            StationDisplay stationDisplayWindow = new StationDisplay(bl.DalToBlStation(x), bl);
//            stationDisplayWindow.Show();
//            Close();
//        }

//        /// <summary>
//        /// Opens an add drone screen
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        private void Add_Drone_ButtonClick(object sender, RoutedEventArgs e)
//        {
//            StationDisplay stationDisplayWindow = new StationDisplay(bl);
            
//            stationDisplayWindow.Show();
//        }
//        /// <summary>
//        /// This function will simply reselect the item currently selected which will 
//        /// inevitebly refresh the list.
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        private void Button_Click_1(object sender, RoutedEventArgs e)
//        {
//            //if there was no selection yet nothing should be refreshed.
//            if (StatusSelector.SelectedItem == null)
//                return;
//            string x = StatusSelector.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last();
//            DroneListView.ItemsSource = bl.DroneListFilter(x);
//        }
//    }
//}
