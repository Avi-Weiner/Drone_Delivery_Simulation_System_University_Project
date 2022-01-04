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
        }

        private void Close_ButtonClick(object sender, RoutedEventArgs e)
        {
            Close();

        }
    }
}
