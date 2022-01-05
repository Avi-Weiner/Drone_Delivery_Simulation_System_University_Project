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
            StationName.Text = Station.Name.ToString();
            ChargingStations.Text = Station.AvailableChargeSlots.ToString();
            StationView.Text = station.ToString();
        }
        //public StationDisplay(BlApi.IBL BL)
        //{
            //StationView.Visibility
       // }

       

        private void Close_ButtonClick(object sender, RoutedEventArgs e)
        {
            DroneList st = new DroneList(bl);
            st.Show();
            Close();
        }

        

       

        /// <summary>
        /// Constructor for adding a drone
        /// </summary>
        public StationDisplay(BlApi.IBL BL)
        {
            bl = BL;
            InitializeComponent();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateStation(station.Id, Int32.Parse(StationName.Text), Int32.Parse(ChargingStations.Text));
                MessageBox.Show("Station updated successfully");
                StationList st = new StationList(bl);
                st.Show();
                Close();
            }
            catch (BL.MessageException m)
            {
                MessageBox.Show(m.ToString());
            }
        }
        private void StationName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(int.TryParse(((TextBox)sender).Text, out int i)) && ((TextBox)sender).Text != "")
            {
                MessageBox.Show("Invalid input");
                ((TextBox)sender).Text = station.Name.ToString();
            }

            
        }
        private void StationChargeSlots_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(int.TryParse(((TextBox)sender).Text, out int i)) && ((TextBox)sender).Text != "")
            {
                MessageBox.Show("Invalid input");
                ((TextBox)sender).Text = station.AvailableChargeSlots.ToString();
            }


        }
    }
}