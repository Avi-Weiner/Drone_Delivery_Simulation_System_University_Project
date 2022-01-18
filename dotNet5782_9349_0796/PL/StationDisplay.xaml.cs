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
        /// Constructor for updating a station
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
            UpdateStationTitle.Visibility = Visibility.Visible;

            StationName.Text = Station.Name.ToString();
            station.Name = Station.Name;
            ChargingStations.Text = Station.AvailableChargeSlots.ToString();
            station.AvailableChargeSlots = Station.AvailableChargeSlots;
            StationView.Text = station.ToString();
        }


        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateStation(station.Id, Int32.Parse(StationName.Text), Int32.Parse(ChargingStations.Text));
                MessageBox.Show("Station updated successfully");
                //ListDisplay st = new ListDisplay(bl);
                //st.Show();
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
            station = new BL.BaseStation();
            location = new BL.Location();
            bl = BL;
            InitializeComponent();

            Longitude.Visibility = Visibility.Visible;
            Latitude.Visibility = Visibility.Visible;
            LongitudeText.Visibility = Visibility.Visible;
            LatitudeText.Visibility = Visibility.Visible;
            LongitudeRestrictions.Visibility = Visibility.Visible;
            LatitudeRestrictions.Visibility = Visibility.Visible;
            AddStationButton.Visibility = Visibility.Visible;
            AddBaseStationTitle.Visibility = Visibility.Visible;
        }

        private void Add_Station_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.AddBaseStation(station.Name, location.longitude, location.latitude, station.AvailableChargeSlots);
                MessageBox.Show("Station added succesfully");
                //ListDisplay listDisplay = new ListDisplay(bl);
                //listDisplay.Show();
                Close();
            }
            catch (BL.MessageException m)
            {
                MessageBox.Show(m.ToString());
            }
        }

        private void Longitude_Changed(object sender, TextChangedEventArgs e)
        {
            double.TryParse(((TextBox)sender).Text, out double d);
            if (d < -180 || d > 180)
                MessageBox.Show("Invalid Input: \n Longitude must be between -180 and 180");
            location.longitude = d;

        }

        private void Latitude_Changed(object sender, TextChangedEventArgs e)
        {
            double.TryParse(((TextBox)sender).Text, out double d);
            if (d < -90 || d > 90)
                MessageBox.Show("Invalid Input: \n Latitude must be between -90 and 90");
            location.latitude = d;
        }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text != "-" && !char.IsDigit(e.Text[0]) && e.Text != ".")
            {
                e.Handled = true;
            }
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

            station.Name = i;

        }

        private void StationChargeSlots_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(((TextBox)sender).Text, out int i) && ((TextBox)sender).Text != "")
            {
                MessageBox.Show("Invalid input");
                ((TextBox)sender).Text = station.AvailableChargeSlots.ToString();
            }

            station.AvailableChargeSlots = i;

        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            //ListDisplay st = new ListDisplay(bl);
            //st.Show();
            Close();
        }
        #endregion
    }
}