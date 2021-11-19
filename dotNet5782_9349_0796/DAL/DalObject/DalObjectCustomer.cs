using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public partial class DalObject : IDAL.IDAL
    {
        /// <summary>
        /// Receives Customer Id and returns its index in CustomerList
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public int GetCustomer(int CustomerId)
        {
            int i = DataSource.CustomerList.FindIndex(x => x.Id == CustomerId);

            //if (DataSource.CustomerList[i].Id != CustomerId)
            //    throw new IDAL.DO.MessageException("Error: Customer not found.");
            return i;
        }

        /// <summary>
        /// adding a new customer to the customers list 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        static public void AddCustomer(string name, string phone, double longitude, double latitude)
        {
            //Console.WriteLine("Enter name: ");
            //string name = Console.ReadLine();

            //Console.WriteLine("Enter phone: ");
            //string phone = Console.ReadLine();
            //Console.WriteLine("Enter longitude: ");
            //double longitude = Convert.ToDouble(Console.ReadLine());
            //Console.WriteLine("Enter latitude: ");
            //double latitude = Convert.ToDouble(Console.ReadLine());
            ////add customer to the back of the customer list
            DataSource.CustomerList.Add(new IDAL.DO.Customer
            {
                Id = DataSource.GetNextUniqueID(),
                Name = name,
                Phone = phone,
                Longitude = longitude,
                Latitude = latitude

            });
        }
        ///////////////////////////////////////////////////// Do in Main //////////////////////////////////////////////////////////
        /// <summary>
        /// Prints a customer
        /// </summary>
        /// <param name="i"></param>
        // static public void PrintCustomer(IDAL.DO.Customer c)
        //{
        //   Console.WriteLine("\nCustomer ID: " + c.Id
        //      + "\nCustomer Name: " + c.Name
        //     + "\nCustomer Phone: " + c.Phone
        //    + "\nCustomer Longitude: " + c.Longitude
        //   + "\nCustomer Latitude: " + c.Latitude);
        // }

        /// <summary>
        /// Prints the customer with ID Id
        /// </summary>
        /// <param name="Id"></param>
        //static public void DisplayCustomer(int Id)
        //{
        //    try
        //    {
        //        IDAL.DO.Customer c = DataSource.CustomerList.Find(x => x.Id == Id);
        //        PrintCustomer(c);
        //    }
        //    catch (IDAL.DO.MessageException e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //}

        /// <summary>
        /// Displays all the customers in CustomerList
        /// </summary>
        //static public void DisplayCustomerList()
        /// {
        //foreach (IDAL.DO.Customer c in DataSource.CustomerList)
        //  {
        //        PrintCustomer(c);
        //      }
        //    }
    }
}
