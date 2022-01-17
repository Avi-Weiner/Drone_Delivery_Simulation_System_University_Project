using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace DAL.DalXML
{
    public partial class DalXML : DalApi.IDAL
    {
        static string dir = @"..\..\..\..\xmlData\";
        static DalXML()
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
        static string CustomersFilePath = @"Customers.xml";
        static string DronesFilePath = @"Drones.xml";
        static string PackagesFilePath = @"Packages.xml";
        static string StationsFilePath = @"Stations.xml";
        public DalXML()
        {
            if (!File.Exists(dir + CustomersFilePath))
                XMLTools.SaveListToXMLSerializer<DO.Customer>(DalObject.DataSource.CustomerList, dir + CustomersFilePath);
                
        }

        /// <summary>
        /// Receives Customer Id and returns its index in CustomerList
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public DO.Customer GetCustomer(int CustomerId)
        {
            List<DO.Customer> customerList = XMLTools.LoadListFromXMLSerializer<DO.Customer>(dir + CustomersFilePath);

            if (customerList.Exists(x => x.Id == CustomerId) == false)
                throw new Exception("customer does not exist");
            return customerList.Find(x => x.Id == CustomerId);
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
            List<DO.Customer> customerList = XMLTools.LoadListFromXMLSerializer<DO.Customer>(dir + CustomersFilePath);
            ////add customer to the back of the customer list
            customerList.Add(new DO.Customer
            {
                Id = DalObject.DataSource.GetNextUniqueID(),
                Name = name,
                Phone = phone,
                Longitude = longitude,
                Latitude = latitude

            });
            XMLTools.SaveListToXMLSerializer<DO.Customer>(customerList, dir + CustomersFilePath);
        }

        /// <summary>
        /// returns list of all customers
        /// </summary>
        /// <returns></returns>
        public List<DO.Customer> GetCustomerList()
        {
            return XMLTools.LoadListFromXMLSerializer<DO.Customer>(dir + CustomersFilePath);
        }

        /// <summary>
        /// Sets the Customer List
        /// </summary>
        /// <param name="Customer"></param>
        public void SetCustomerList(List<DO.Customer> Customers)
        {
            XMLTools.SaveListToXMLSerializer<DO.Customer>(Customers, dir + CustomersFilePath);
        }
    }
}
