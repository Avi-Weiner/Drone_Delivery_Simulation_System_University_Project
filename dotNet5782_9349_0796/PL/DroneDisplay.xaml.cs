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
using System.Threading;
using System.ComponentModel;

namespace PL
{
    /// <summary>
    /// Interaction logic for DroneDisplay.xaml
    /// </summary>
    public partial class DroneDisplay : Window
    {

        #region DroneAction
        BlApi.IBL bl;
        BL.Drone drone;
        ListDisplay lst;
        private bool Thread_Running = false;
        private bool myClosing = false;
        // this line should probalby be deleted...............public Visibility SendButton { get; set; }

        /// <summary>
        /// Constructor for updating a drone
        /// </summary>
        /// <param name="Drone"></param>
        /// <param name="BL"></param>
        public DroneDisplay(BL.Drone Drone, BlApi.IBL BL, ListDisplay l)
        {
            bl = BL;
            drone = Drone;
            //droneView.Content = drone.ToString();
            //droneView.Inlines.Add(Drone.ToString());
            lst = l;
            InitializeComponent();
            #region Element Visibility 
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
            DroneUpdateOptions.Visibility = Visibility.Visible;
            DisplayPackage.Visibility = Visibility.Visible;
            #endregion

            DroneView.Text = Drone.ToString();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!myClosing) // Won't allow to cancel the window!!! It is not me!!!
            {
                e.Cancel = true;
                MessageBox.Show(@"DON""T CLOSE ME!!!", "STOP IT!!!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        //public void Deliveries()
        //{
        //    while(drone.)
        //}
        /// <summary>
        /// won't allow window to close unless its supposed to.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Update_ButtonClick(object sender, RoutedEventArgs e)
        {
            try 
            {
                bl.UpdateDrone(drone.Id, newModel.Text);
                MessageBox.Show("Drone updated Successfully");
            
                drone = bl.DroneToListToDrone(drone.Id);
                DroneView.Text = drone.ToString();
            }
            catch (BL.MessageException m)
            {
                MessageBox.Show(m.ToString());
            }
        }
        /// <summary>
        /// assignes a package to a drone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Send_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                
                bl.AssignPackageToDrone(drone.Id);
                MessageBox.Show("Package was assigned succefully");
                drone = bl.DroneToListToDrone(drone.Id);
                DroneView.Text = drone.ToString();
                
                
            }
            catch(BL.MessageException m)
            {
                MessageBox.Show(m.ToString());
            }
        }
        /// <summary>
        /// drone collects a package
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Collect_Package_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DroneCollectsAPackage(drone.Id);
                MessageBox.Show("Drone Collected Package Succefully");
                drone = bl.DroneToListToDrone(drone.Id);
                DroneView.Text = drone.ToString();
            }
            catch(BL.MessageException m)
            {
                MessageBox.Show(m.ToString());
            }
        }
        /// <summary>
        /// drone delivers a pakcage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Deliver_Package_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DroneDeliversPakcage(drone.Id);
                MessageBox.Show("Drone Delivered Package succefully");
                drone = bl.DroneToListToDrone(drone.Id);
                DroneView.Text = drone.ToString();
            }
            catch(BL.MessageException m)
            {
                MessageBox.Show(m.ToString());
            }
        }
        /// <summary>
        /// send a drone to charge in the nearenst base station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Charge_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.SendDroneToCharge(drone.Id);
                MessageBox.Show("Drone Sent to Charger Succefully");
                drone = bl.DroneToListToDrone(drone.Id);
                DroneView.Text = drone.ToString();
            }
            catch(BL.MessageException m)
            {
                MessageBox.Show(m.ToString());
            }
        }
        /// <summary>
        /// release a drone from a charger
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Release_ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //DateTime x = DateTime.MinValue;
                //x = x.AddHours(ChargingTime);
               
                
                bl.ReleaseDroneFromCharge(drone.Id);
                drone = bl.DroneToListToDrone(drone.Id);
                MessageBox.Show("Drone released from charge successfully");
                DroneView.Text = drone.ToString();
                
            }
            catch(BL.MessageException m)
            {
                MessageBox.Show(m.ToString());
            }
        }
        #endregion
        /// <summary>
        /// close the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_ButtonClick(object sender, RoutedEventArgs e)
        {


            if (!Thread_Running)
            {
                myClosing = true;
                Close();
            }
            else
            {
                MessageBox.Show("Don't close me; Thread is running.");
            }
        
        }

        #region AddDrone

        //Variables for input
        string modelString;
        string weightString;
        int stationId;

        /// <summary>
        /// Constructor for adding a drone
        /// </summary>
        public DroneDisplay(BlApi.IBL BL)
        {
            bl = BL;
            InitializeComponent();
            Simulator.Visibility = Visibility.Hidden;
            //Make Elements visible
            Model.Visibility = Visibility.Visible;
            ModelTextBox.Visibility = Visibility.Visible;
            Weight.Visibility = Visibility.Visible;
            WeightComboBox.Visibility = Visibility.Visible;
            BaseStationId.Visibility = Visibility.Visible;
            BaseStationTextBox.Visibility = Visibility.Visible;
            AddDroneTitle.Visibility = Visibility.Visible;
            AddDroneButton.Visibility = Visibility.Visible;
            UpdateModel.Visibility = Visibility.Visible;


            //Add drone by just accepting the information, (valid only on the most basic level, rest of the validation done by BL)
            //Send info to logic layer to be added to the system
        }
        /// <summary>
        /// Model name textbox changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// validate weather the locatoin entered is valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LocationValidation(object sender, TextCompositionEventArgs e)
        {
            //do not allow futher incorrect typing, got rid of max
            e.Handled = !(int.TryParse(((TextBox)sender).Text + e.Text, out int i) && i >= 1);
        }
        /// <summary>
        /// Change in the location txt box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// clicking this will add the drone requested to be added and close the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDroneButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                drone = bl.AddDrone(modelString, weightString, stationId);
                MessageBox.Show("Drone Succesfully Added.");
                //ListDisplay l = new ListDisplay(bl);
                //l.Show();
                myClosing = true;
                Close();
            }
            catch (BL.MessageException m)
            {
                MessageBox.Show(m.ToString());
            }
            
        }

        #endregion

        /*int ChargingTime;
        private void HoursCharged_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!(int.TryParse(((TextBox)sender).Text, out int i) && i >= 1 && i <= 23))
            {
                ((TextBox)sender).Text = "";
            }
            
            ChargingTime = i;
        }*/

        /// <summary>
        /// Displays the pakcageDisplay for package in display
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayPackage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (drone.PackageInTransfer != null)
                {
                    PackageDisplay pd = new PackageDisplay(bl, drone.PackageInTransfer, drone);
                    pd.Show();
                }
            }
            catch (BL.MessageException m)
            {
                MessageBox.Show(m.ToString());
            }
        }
       /// <summary>
       /// pass a drone to be updated to the droneView window
       /// </summary>
       /// <param name="drone"></param>
        private void UpdateDroneView(BL.Drone drone)
        {
            DroneView.Text = drone.ToString();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Action sim = new(Simulate);
            bl.ActivateSimulator(drone.Id, sim);
            
        }
        public void Simulate()
        {
            worker.ReportProgress(1);
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            UpdateDroneView(bl.DroneToListToDrone(drone.Id));
        }

        BackgroundWorker worker;
        private void Simulator_Button_Click(object sender, RoutedEventArgs e)
        {
            Thread_Running = true;
            #region Visibilities
            Simulator.Visibility = Visibility.Hidden;
            Update.Visibility = Visibility.Hidden;
            UpdateModel.Visibility = Visibility.Hidden;
            Send.Visibility = Visibility.Hidden;
            Charge.Visibility = Visibility.Hidden;
            Release.Visibility = Visibility.Hidden;
            CollectPakcage.Visibility = Visibility.Hidden;
            DeliverPackage.Visibility = Visibility.Hidden;
            newModel.Visibility = Visibility.Hidden;
            DroneUpdateOptions.Visibility = Visibility.Hidden;
            DisplayPackage.Visibility = Visibility.Hidden;

            StopSimulator.Visibility = Visibility.Visible;
            #endregion

            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_WorkComplete;
            
            try
            {
                worker.RunWorkerAsync();
            }
            catch (Exception except)
            {
                worker.CancelAsync();
                MessageBox.Show(except.Message);
            }
        }

        /// <summary>
        /// What to do when the wroker simulation is complete. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_WorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            bl.StopTheSimulator();
            MessageBox.Show("Simulator was completed successfully.");
            StopSimulator.Visibility = Visibility.Hidden;
            Simulator.Visibility = Visibility.Visible;
            #region Make Update options visible
            Update.Visibility = Visibility.Visible;
            UpdateModel.Visibility = Visibility.Visible;
            Send.Visibility = Visibility.Visible;
            Charge.Visibility = Visibility.Visible;
            Release.Visibility = Visibility.Visible;
            CollectPakcage.Visibility = Visibility.Visible;
            DeliverPackage.Visibility = Visibility.Visible;
            newModel.Visibility = Visibility.Visible;
            DroneView.Visibility = Visibility.Visible;
            DroneUpdateOptions.Visibility = Visibility.Visible;
            DisplayPackage.Visibility = Visibility.Visible;
            #endregion  
            Thread_Running = false;
        }
        /// <summary>
        /// clicking this will stop the simulator.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StopSimulator_Click(object sender, RoutedEventArgs e)
        {
            worker.CancelAsync();
            bl.StopTheSimulator();

            

        }
    }
}
