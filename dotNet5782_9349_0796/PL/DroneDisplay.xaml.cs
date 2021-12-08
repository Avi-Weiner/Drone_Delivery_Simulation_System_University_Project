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
    public partial class DroneDisplay : Window
    {
        IBL.IBL bl;
        IBL.BO.Drone drone;
        public Visibility SendButton { get; set; }
        public DroneDisplay(IBL.BO.Drone Drone, IBL.IBL BL)
        {
            
            bl = BL;
            drone = Drone;
            SendButton = Visibility.Hidden;
            //droneView.Inlines.Add(Drone.ToString());
            
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bl.UpdateDrone(drone.Id, newModel.Text);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DroneCollectsAPackage(drone.Id);
            }
            catch(IBL.BO.MessageException m)
            {
                MessageBox.Show(m.ToString());
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DroneCollectsAPackage(drone.Id);
            }
            catch(IBL.BO.MessageException m)
            {
                MessageBox.Show(m.ToString());
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DroneDeliversPakcage(drone.Id);
            }
            catch(IBL.BO.MessageException m)
            {
                MessageBox.Show(m.ToString());
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.SendDroneToCharge(drone.Id);
            }
            catch(IBL.BO.MessageException m)
            {
                MessageBox.Show(m.ToString());
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.ReleaseDroneFromCharge(drone.Id, DateTime.Now);
            }
            catch(IBL.BO.MessageException m)
            {
                MessageBox.Show(m.ToString());
            }
        }
    }
}
