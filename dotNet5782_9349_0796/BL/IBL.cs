using System;

namespace IBL
{
    public interface IBL
    {
        void AddBaseStation(int Id, string name, double longitude, double Latitude, int slots);
        void AddDrone(int manufactureId, string model, IDAL.DO.WeightCategory Weight, int chargingStation);
        void AddCustomer(int CustomerId, string name, string phone, double Longitude, double Latitude);
        void AddPackage(int CustomerId, int ReceiverId, IDAL.DO.WeightCategory Weight, IDAL.DO.Priority Priority );
        void UpdateDrone(int Id, string Model);
        void UpdateStation(string StationName = "", int chargingStation = 0);
        void UpdateCustomer(int Id, string Name = "", string Phone = "");
        void SendDroneToCharge(int DroneId);
        void AssignPackageToDrone(int DroneId);
        void DroneCollectsAPackage(int DroneId);
        void DroneDeliversPakcage(int DroneId);

    }
}
