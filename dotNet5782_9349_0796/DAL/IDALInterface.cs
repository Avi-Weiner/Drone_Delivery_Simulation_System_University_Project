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
        public DO.Drone GetDrone(int DroneId);
        public void AddDrone(string model, DO.WeightCategory Weight);

        //Package DalObject
        public DO.Package GetPackage(int PackageId);
        public void AddPackage(int InputSender, int InputReceiver, DO.WeightCategory InputWeight, DO.Priority InputPriority);
        public List<DO.Package> GetPackageList();
        public void SetPackageList(List<DO.Package> Packages);

        //Station DalObject
        public DO.Station GetStation(int StationId);
        public void AddStation(int name, double longitude, double latitude, int slots);

        //No other methods work with static,
        //Without static they don't work in the main.
    }
}
