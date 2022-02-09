using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace DalObject
{
    public partial class DalObject : DalApi.IDAL
    {
        /// <summary>
        /// Receives Customer Id and returns its index in CustomerList
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public DO.Customer GetCustomer(int CustomerId)
        {
            int i = DataSource.CustomerList.FindIndex(x => x.Id == CustomerId);
            if (i == -1)
                throw new DO.MessageException("Error: Customer not found.");
            return DataSource.CustomerList.Find(x => x.Id == CustomerId);
            
        }

        /// <summary>
        /// adding a new customer to the customers list 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        public void AddCustomer(string name, string phone, double longitude, double latitude)
        {
            ////add customer to the back of the customer list
            DataSource.CustomerList.Add(new DO.Customer
            {
                Id = DataSource.GetNextUniqueID(),
                Name = name,
                Phone = phone,
                Longitude = longitude,
                Latitude = latitude
            });
        }

        /// <summary>
        /// returns list of all customers
        /// </summary>
        /// <returns></returns>
        public List<DO.Customer> GetCustomerList()
        {
            return DataSource.CustomerList;
        }

        /// <summary>
        /// Sets the Customer List
        /// </summary>
        /// <param name="Customer"></param>
        public void SetCustomerList(List<DO.Customer> Customers)
        {
            DataSource.CustomerList = Customers;
        }
    }
}
