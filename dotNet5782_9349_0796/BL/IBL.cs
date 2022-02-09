using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BlApi
{
    public interface IBL
    {
        BaseStation AddBaseStation(int name, double longitude, double Latitude, int availableSlots);
        Drone AddDrone(string Model, string Weight, int StationId);
        Customer AddCustomer(string name, string phone, double Longitude, double Latitude);
        Package AddPackage(int SenderId, int ReceiverId, string Weight, string Priority);
        Drone UpdateDrone(int Id, string Model);
        void UpdateStation(int Id, int StationName, int chargingStation);
        void UpdateCustomer(int Id, string Name = "", string Phone = "");
        void UpdatePackage(int Id, int SenderId = 0, int ReceiverId = 0, string Weight = "", string Priority = "");
        void SendDroneToCharge(int DroneId);
        void AssignPackageToDrone(int DroneId);
        void DroneCollectsAPackage(int DroneId);
        void DroneDeliversPakcage(int DroneId);
        public void ReleaseDroneFromCharge(int DroneId);
        void DeletePackage(int Id);

        /// <summary>
        /// Returns a filtered DroneToList depending on the entered option:
        /// States:
        ///     1: "free"
        ///     2: "maintenance" 
        ///     3: "delivery"
        /// Max Weight:
        ///     1: "light"
        ///     2: "medium"
        ///     3: "heavy"
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        List<DroneToList> DroneListFilter(string option);

        /// <summary>
        /// Returns BL Drone from given id, converting it from the DroneToList list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Drone DroneToListToDrone(int id);

        /// <summary>
        /// Returns a BL package from the DalPackage from the given id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Package DalToBlPackage(int id);
        public List<BaseStationToList> StationListFilter(string option);
        public BaseStationToList StationToStationToList(int id);
        public BaseStation DalToBlStation(int id);
        public Customer DalToBlCustomer(int id);
        public List<CustomerToList> ListOfCustomers();
        public List<PackageToList> PackageListFilter(string option);
        public void ActivateSimulator(int Id, Action action);
        public void StopTheSimulator();
    }
}
