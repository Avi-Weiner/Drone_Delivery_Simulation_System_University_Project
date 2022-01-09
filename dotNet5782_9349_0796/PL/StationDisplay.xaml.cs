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
    /// Interaction logic for DroneDisplay.xaml
    /// </summary>
    public partial class StationDisplay : Window
    {


        BlApi.IBL bl;
        BL.BaseStation station;
        BL.Location location;

        #region UpdateStation
        // this line should probalby be deleted...............public Visibility SendButton { get; set; }
        /// <summary>
        /// Constructor for updating a drone
        /// </summary>
        /// <param name="Drone"></param>
        /// <param name="BL"></param>
        public StationDisplay(BL.BaseStation Station, BlApi.IBL BL)
        {
            bl = BL;
            station = Station;

            InitializeComponent();
            StationView.Visibility = Visibility.Visible;
            UpdateButton.Visibility = Visibility.Visible;
            StationName.Visibility = Visibility.Visible;
            ChargingStations.Visibility = Visibility.Visible;

            StationName.Text = Station.Name.ToString();
            station.Name = Station.Name;
            ChargingStations.Text = Station.AvailableChargeSlots.ToString();
            station.AvailableChargeSlots = Station.AvailableChargeSlots;
            StationView.Text = station.ToString();
        }

        private void Close_ButtonClick(object sender, RoutedEventArgs e)
        {
            ListDisplay st = new ListDisplay(bl);
            st.Show();
            Close();
        }


        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateStation(station.Id, Int32.Parse(StationName.Text), Int32.Parse(ChargingStations.Text));
                MessageBox.Show("Station updated successfully");
                ListDisplay st = new ListDisplay(bl);
                st.Show();
                Close();
            }
            catch (BL.MessageException m)
            {
                MessageBox.Show(m.ToString());
            }
        }


        #endregion

        #region AddStation
        /// <summary>
        /// Constructor for adding a station
        /// </summary>
        public StationDisplay(BlApi.IBL BL)
        {
            bl = BL;
            InitializeComponent();
        }

        private void Add_Station_Click(object sender, RoutedEventArgs e)
        {
            if ( )
            bl.AddBaseStation(station.Name, location.longitude, location.latitude, station.AvailableChargeSlots);
        }

        private void Longitude_Changed(object sender, TextChangedEventArgs e)
        {

            if (!int.TryParse(((TextBox)sender).Text, out int i) && ((TextBox)sender).Text != "")
            {
                MessageBox.Show("Invalid input");
            }

            location.longitude = int.Parse(Longitude.Text);

        }

        private void Latitude_Changed(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(((TextBox)sender).Text, out int i) && ((TextBox)sender).Text != "")
            {
                MessageBox.Show("Invalid input");
            }

            location.latitude = int.Parse(Latitude.Text);
        }

        #endregion

        #region Both
        private void StationName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(((TextBox)sender).Text, out int i) && ((TextBox)sender).Text != "")
            {
                MessageBox.Show("Invalid input");
                ((TextBox)sender).Text = station.Name.ToString();
            }

            station.Name = int.Parse(StationName.Text);

        }

        private void StationChargeSlots_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(((TextBox)sender).Text, out int i) && ((TextBox)sender).Text != "")
            {
                MessageBox.Show("Invalid input");
                ((TextBox)sender).Text = station.AvailableChargeSlots.ToString();
            }

            station.AvailableChargeSlots = int.Parse(ChargingStations.Text);

        }
        #endregion
    }
}