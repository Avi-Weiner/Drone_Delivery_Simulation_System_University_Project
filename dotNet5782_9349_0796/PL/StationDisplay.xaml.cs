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
            DroneView.Visibility = Visibility.Visible;
            

            DroneView.Text = station.ToString();
        }

       

        private void Close_ButtonClick(object sender, RoutedEventArgs e)
        {
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


 

    }
}