using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public partial class DalObject : IDAL.IDALInterface
    {
        /// <summary>
        /// Receives Customer Id and returns its index in CustomerList
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public static int GetCustomer(int CustomerId)
        {
            int i = 0;
            while (i < DataSource.GetFreeCustomerI() && DataSource.CustomerList[i].Id != CustomerId)
                i++;

            if (DataSource.CustomerList[i].Id != CustomerId)
                throw new IDAL.DO.MessageException("Error: Customer not found.");
            return i;
        }

        /// <summary>
        /// adding a new customer to the customers list
        /// </summary>
        public static void AddCustomer()
        {
            Console.WriteLine("Enter name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Enter phone: ");
            string phone = Console.ReadLine();
            Console.WriteLine("Enter longitude: ");
            double longitude = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter latitude: ");
            double latitude = Convert.ToDouble(Console.ReadLine());
            //add customer to the back of the customer list
            DataSource.CustomerList.Add(new IDAL.DO.Customer
            {
                Id = DataSource.GetNextUniqueID(),
                Name = name,
                Phone = phone,
                Longitude = longitude,
                Latitude = latitude

            });
        }

        /// <summary>
        /// Prints a customer at indexx in CustomerList
        /// </summary>
        /// <param name="i"></param>
        public static void PrintCustomer(int i)
        {
            Console.WriteLine("\nCustomer ID: " + DataSource.CustomerList[i].Id
                + "\nCustomer Name: " + DataSource.CustomerList[i].Name
                + "\nCustomer Phone: " + DataSource.CustomerList[i].Phone
                + "\nCustomer Longitude: " + DataSource.CustomerList[i].Longitude
                + "\nCustomer Latitude: " + DataSource.CustomerList[i].Latitude);
        }

        /// <summary>
        /// Prints the customer with ID Id
        /// </summary>
        /// <param name="Id"></param>
        public static void DisplayCustomer(int Id)
        {
            try
            {
                int p = GetCustomer(Id);
                PrintCustomer(p);
            }
            catch (IDAL.DO.MessageException e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// Displays all the customers in CustomerList
        /// </summary>
        public static void DisplayCustomerList()
        {
            for (int i = 0; i < DataSource.GetFreeCustomerI(); i++)
                PrintCustomer(i);
        }
    }
}
