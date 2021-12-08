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
    }
}
