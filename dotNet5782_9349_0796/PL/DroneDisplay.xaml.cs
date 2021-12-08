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
        public DroneDisplay(IBL.BO.Drone Drone)
        {
            InitializeComponent();
        }

        private void Close_Button(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Model_TextBox_Changed(object sender, TextChangedEventArgs e)
        {

        }
        /// <summary>
        /// Combo Box Weight selection changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CMB_Weight_Changed(object sender, SelectionChangedEventArgs e)
        {

        }
        /// <summary>
        /// ComboBox status selection changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CMB_Status_Changed(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LocationValidation(object sender, TextCompositionEventArgs e)
        {
            int max = 100;

            //do not allow futher incorrect typing
            e.Handled = !(int.TryParse(((TextBox)sender).Text + e.Text, out int i) && i >= 1 && i <= max);
        }

        private void Location_TextBox_Changed(object sender, TextChangedEventArgs e)
        {
            #region validation
            int max = 100;

            if (!int.TryParse(((TextBox)sender).Text, out int j) || j < 1 || j > max)
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
            
        }
    }
}
