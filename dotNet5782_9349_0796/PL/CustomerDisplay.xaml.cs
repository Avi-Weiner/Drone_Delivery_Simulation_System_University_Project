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
        // this line should probalby be deleted...............public Visibility SendButton { get; set; }
        /// <summary>
        /// Constructor for updating a drone
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
        }

        private void Close_ButtonClick(object sender, RoutedEventArgs e)
        {
            DroneList dl = new DroneList(bl);
            dl.Show();
            Close();

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
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
    }
}
