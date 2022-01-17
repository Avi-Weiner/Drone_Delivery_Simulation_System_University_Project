using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;

namespace DalApi
{
    public interface IDAL
    {
        //General DalObject

        //Customer DalObject
        public DO.Customer GetCustomer(int CustomerId);
        public void AddCustomer(string name, string phone, double longitude, double latitude);
        public List<DO.Customer> GetCustomerList();
        public void SetCustomerList(List<DO.Customer> Customers);

        //Drone DalObject
        public void AddDrone(string model, DO.WeightCategory Weight);

        //No other methods work with static,
        //Without static they don't work in the main.
    }
}
