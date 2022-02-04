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
    /// Interaction logic for PackageDisplay.xaml
    /// </summary>
    public partial class PackageDisplay : Window
    {

        BlApi.IBL bl;
        BL.Package package;
        BL.Drone? Drone;
        int senderId, receiverId;
        string weightString, priorityString;
        ListDisplay lst;

        /// <summary>
        /// Update package display constructor
        /// </summary>
        /// <param name="BL"></param>
        /// <param name="Package"></param>
        public PackageDisplay(BlApi.IBL BL, BL.Package Package, ListDisplay l)
        {
            package = Package;
            bl = BL;
            InitializeComponent();
            PackageView.Text = package.ToString();
            lst = l;
            UpdatePackageTitle.Visibility = Visibility.Visible;
            UpdateButton.Visibility = Visibility.Visible;
            DeleteButton.Visibility = Visibility.Visible;
            PackageView.Visibility = Visibility.Visible;

        }
        /// <summary>
        /// constructor for opening a pakcage from a drone
        /// </summary>
        /// <param name="BL"></param>
        /// <param name="Package"></param>
        /// <param name="drone"></param>
        public PackageDisplay(BlApi.IBL BL, BL.Package Package, BL.Drone drone)
        {
            package = Package;
            Drone = drone;
            bl = BL;
            InitializeComponent();
            PackageView.Text = package.ToString();

            UpdatePackageTitle.Visibility = Visibility.Visible;
            UpdateButton.Visibility = Visibility.Visible;
            DeleteButton.Visibility = Visibility.Visible;
            PackageView.Visibility = Visibility.Visible;
        }

        private void Update_Package_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdatePackage(package.Id, senderId, receiverId, weightString, priorityString);
                MessageBox.Show("Package updated succesfully");
            }
            catch (BL.MessageException m)
            {
                MessageBox.Show(m.ToString());
            }
        }

        private void Delete_Package_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DeletePackage(package.Id);
                MessageBox.Show("Package deleted succesfully");
                lst.Refresh();
                Close();
            }
            catch (BL.MessageException m)
            {
                MessageBox.Show(m.ToString());
            }
        }


        #region AddPackageOnly
        /// <summary>
        /// Constructor for adding a station
        /// </summary>
        public PackageDisplay(BlApi.IBL BL)
        {
            package = new BL.Package();
            bl = BL;
            InitializeComponent();

            AddPackageTitle.Visibility = Visibility.Visible;
            AddPackageButton.Visibility = Visibility.Visible;

        }
        private void Add_Package_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                bl.AddPackage(senderId, receiverId, weightString, priorityString);
                MessageBox.Show("Package added succesfully");
                Close();
            }
            catch (BL.MessageException m)
            {
                MessageBox.Show(m.ToString());
            }
        }
        #endregion

        #region Both
        private void SenderId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(((TextBox)sender).Text, out int i) && ((TextBox)sender).Text != "")
            {
                MessageBox.Show("Invalid input");
            }

            senderId = i;
        }

        private void ReceiverId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!int.TryParse(((TextBox)sender).Text, out int i) && ((TextBox)sender).Text != "")
            {
                MessageBox.Show("Invalid input");
            }

            receiverId = i;
        }

        private void CMB_Weight_Changed(object sender, SelectionChangedEventArgs e)
        {
            weightString = WeightComboBox.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last();
        }

        private void CMB_Priority_Changed(object sender, SelectionChangedEventArgs e)
        {
            priorityString = PriorityComboBox.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last();
        }


        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();


        }
        #endregion
    }
}
