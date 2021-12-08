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

        #region DroneAction
        IBL.IBL bl;
        IBL.BO.Drone drone;
        // this line should probalby be deleted...............public Visibility SendButton { get; set; }
        /// <summary>
        /// Constructor for updating a drone
        /// </summary>
        /// <param name="Drone"></param>
        /// <param name="BL"></param>
        public DroneDisplay(IBL.BO.Drone Drone, IBL.IBL BL)
        {
            bl = BL;
            drone = Drone;
            //droneView.Content = drone.ToString();
            //droneView.Inlines.Add(Drone.ToString());
            
            InitializeComponent();

            //Make Elements visible
            Update.Visibility = Visibility.Visible;
            UpdateModel.Visibility = Visibility.Visible;
            Send.Visibility = Visibility.Visible;
            Charge.Visibility = Visibility.Visible;
            Release.Visibility = Visibility.Visible;
            CollectPakcage.Visibility = Visibility.Visible;
            DeliverPackage.Visibility = Visibility.Visible;
            newModel.Visibility = Visibility.Visible;
            DroneView.Visibility = Visibility.Visible;
            HoursChargedPrompt.Visibility = Visibility.Visible;
            HoursCharged.Visibility = Visibility.Visible;

            DroneView.Text = Drone.ToString();
        }

        
        private void Update_ButtonClick(object sender, RoutedEventArgs e)
        {
            bl.UpdateDrone(drone.Id, newModel.Text);
            MessageBox.Show("Drone Model updated Successfully");
            
            drone = bl.DroneToListToDrone(drone.Id);
            DroneView.Text = drone.ToString();
        }

        private void Send_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                
                bl.AssignPackageToDrone(drone.Id);
                MessageBox.Show("Packge was assigned succefully");
                drone = bl.DroneToListToDrone(drone.Id);
                DroneView.Text = drone.ToString();

            }
            catch(IBL.BO.MessageException m)
            {
                MessageBox.Show(m.ToString());
            }
        }

        private void Collect_Package_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DroneCollectsAPackage(drone.Id);
                MessageBox.Show("Drone Collected Package Succefully");
                drone = bl.DroneToListToDrone(drone.Id);
                DroneView.Text = drone.ToString();
            }
            catch(IBL.BO.MessageException m)
            {
                MessageBox.Show(m.ToString());
            }
        }

        private void Deliver_Package_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DroneDeliversPakcage(drone.Id);
                MessageBox.Show("Drone Delivered Package succefully");
                drone = bl.DroneToListToDrone(drone.Id);
                DroneView.Text = drone.ToString();
            }
            catch(IBL.BO.MessageException m)
            {
                MessageBox.Show(m.ToString());
            }
        }

        private void Charge_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.SendDroneToCharge(drone.Id);
                MessageBox.Show("Drone Sent to Charger Succefully");
                drone = bl.DroneToListToDrone(drone.Id);
                DroneView.Text = drone.ToString();
            }
            catch(IBL.BO.MessageException m)
            {
                MessageBox.Show(m.ToString());
            }
        }

        private void Release_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime x = DateTime.MinValue;
                x = x.AddHours(ChargingTime);
                MessageBox.Show("Drone released from charge successfully");
                
                bl.ReleaseDroneFromCharge(drone.Id, x);
                drone = bl.DroneToListToDrone(drone.Id);
                DroneView.Text = drone.ToString();
            }
            catch(IBL.BO.MessageException m)
            {
                MessageBox.Show(m.ToString());
            }
        }
        #endregion

        private void Close_ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #region AddDrone

        //Variables for input
        string modelString;
        string weightString;
        int stationId;

        /// <summary>
        /// Constructor for adding a drone
        /// </summary>
        public DroneDisplay(IBL.IBL BL)
        {
            bl = BL;
            InitializeComponent();

            //Make Elements visible
            Model.Visibility = Visibility.Visible;
            ModelTextBox.Visibility = Visibility.Visible;
            Weight.Visibility = Visibility.Visible;
            WeightComboBox.Visibility = Visibility.Visible;
            BaseStationId.Visibility = Visibility.Visible;
            BaseStationTextBox.Visibility = Visibility.Visible;
            AddDroneTitle.Visibility = Visibility.Visible;
            AddDroneButton.Visibility = Visibility.Visible;
            DroneUpdateOptions.Visibility = Visibility.Visible;
            UpdateModel.Visibility = Visibility.Visible;


            //Add drone by just accepting the information, (valid only on the most basic level, rest of the validation done by BL)
            //Send info to logic layer to be added to the system
        }

        private void Model_TextBox_Changed(object sender, TextChangedEventArgs e)
        {
            modelString = ModelTextBox.Text;
        }

        /// <summary>
        /// Combo Box Weight selection changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CMB_Weight_Changed(object sender, SelectionChangedEventArgs e)
        {
            weightString = WeightComboBox.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last();
        }

        private void LocationValidation(object sender, TextCompositionEventArgs e)
        {
            //do not allow futher incorrect typing, got rid of max
            e.Handled = !(int.TryParse(((TextBox)sender).Text + e.Text, out int i) && i >= 1);
        }

        private void Location_TextBox_Changed(object sender, TextChangedEventArgs e)
        {
            #region validation
            //got rid of max
            if (!int.TryParse(((TextBox)sender).Text, out int j) || j < 1)
            {
                //delete incoret input
                ((TextBox)sender).Text = "";
            }
            else
            {
                //delete leading zeros
                ((TextBox)sender).Text = j.ToString();
            }
            #endregion

            stationId = j;

        }

        private void AddDroneButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                drone = bl.AddDrone(modelString, weightString, stationId);
                MessageBox.Show("Drone Succesfully Added.");
                Close();
            }
            catch (IBL.BO.MessageException m)
            {
                MessageBox.Show(m.ToString());
            }
            
        }


        #endregion
        int ChargingTime;
        private void HoursCharged_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!(int.TryParse(((TextBox)sender).Text, out int i) && i >= 1 && i <= 23))
            {
                ((TextBox)sender).Text = "";
            }
            
            ChargingTime = i;
        }
    }
}
