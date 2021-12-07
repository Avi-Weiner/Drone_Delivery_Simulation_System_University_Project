using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBL.BO;

namespace IBL
{
    public interface IBL
    {
        BaseStation AddBaseStation(int name, double longitude, double Latitude, int availableSlots);
        Drone AddDrone(string Model, string Weight, int StationId);
        Customer AddCustomer(string name, string phone, double Longitude, double Latitude);
        Package AddPackage(int SenderId, int ReceiverId, string Weight, string Priority );
        Drone UpdateDrone(int Id, string Model);
        void UpdateStation(int Id, int StationName, int chargingStation);
        void UpdateCustomer(int Id, string Name = "", string Phone = "");
        void SendDroneToCharge(int DroneId);
        void AssignPackageToDrone(int DroneId);
        void DroneCollectsAPackage(int DroneId);
        void DroneDeliversPakcage(int DroneId);
        List<DroneToList> DroneListFilter(string option);
    }
}
