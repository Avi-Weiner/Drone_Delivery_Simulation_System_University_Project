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
    public partial class CustomerDisplay : Window
    {
        BlApi.IBL bl;
        BL.Customer customer;
        BL.Location location;
        // this line should probalby be deleted...............public Visibility SendButton { get; set; }
        /// <summary>
        /// Constructor for updating a customer
        /// </summary>
        /// <param name="Drone"></param>
        /// <param name="BL"></param>
        public CustomerDisplay(BL.Customer Customer, BlApi.IBL BL)
        {
            bl = BL;
            customer = Customer;
       
            InitializeComponent();
            CustomerView.Text = customer.ToString();
            Name.Text = customer.Name;
            Phone1.Text = customer.Phone.Substring(0, 3);
            Phone2.Text = customer.Phone.Substring(4, 4);
            Phone3.Text = customer.Phone.Substring(9, 4);

            UpdateCustomerTitle.Visibility = Visibility.Visible;
            CustomerView.Visibility = Visibility.Visible;
            UpdateButton.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Constructor for adding a customer
        /// </summary>
        /// <param name="BL"></param>
        public CustomerDisplay(BlApi.IBL BL)
        {
            customer = new BL.Customer();
            location = new BL.Location();
            bl = BL;
            InitializeComponent();
            customer = new BL.Customer();
            AddButton.Visibility = Visibility.Visible;
            LongitudeText.Visibility = Visibility.Visible;
            LatitudeText.Visibility = Visibility.Visible;
            Longitude.Visibility = Visibility.Visible;
            Latitude.Visibility = Visibility.Visible;
            AddCustomerTitle.Visibility = Visibility.Visible;
            LongitudeRestrictions.Visibility = Visibility.Visible;
            LatitudeRestrictions.Visibility = Visibility.Visible;
            
        }

        private void Close_ButtonClick(object sender, RoutedEventArgs e)
        {
            //ListDisplay dl = new ListDisplay(bl);
            //dl.Show();
            Close();

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //check if phone number is valid
                if (Phone1.Text.Length < 3 || Phone2.Text.Length < 4 || Phone3.Text.Length < 4)
                {
                    MessageBox.Show("Invalid phone number. \n Please reenter your phone number.");
                }
                else
                {
                    string validPhone = Phone1.Text + "-" + Phone2.Text + "-" + Phone3.Text;
                    bl.UpdateCustomer(customer.Id, Name.Text, validPhone);
                    customer.Phone = validPhone;
                    customer.Name = Name.Text;
                    CustomerView.Text = customer.ToString();
                    MessageBox.Show("Customer updated successfully.");
                }
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string cName = Name.Text;
                string Phone = Phone1.Text + '-' + Phone2.Text + '-' + Phone3.Text;
                bl.AddCustomer(cName, Phone, Double.Parse(Longitude.Text), Double.Parse(Latitude.Text));
                Close();
            }
            catch (BL.MessageException m)
            {
                MessageBox.Show(m.ToString());
            }
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            customer.Name = Name.Text;
        }
    }
}
