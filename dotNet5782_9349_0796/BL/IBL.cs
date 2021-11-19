﻿using System;
using IBL.BO;
namespace IBL
{
    public interface IBL
    {
        BaseStation AddBaseStation(int name, double longitude, double Latitude, int availableSlots);
        Drone AddDrone(string model, IDAL.DO.WeightCategory Weight, int StationId);
        Customer AddCustomer(int CustomerId, string name, string phone, double Longitude, double Latitude);
        void AddPackage(int CustomerId, int ReceiverId, IDAL.DO.WeightCategory Weight, IDAL.DO.Priority Priority );
        void UpdateDrone(int Id, string Model);
        void UpdateStation(int Id, int StationName, int chargingStation);
        void UpdateCustomer(int Id, string Name = "", string Phone = "");
        void SendDroneToCharge(int DroneId);
        void AssignPackageToDrone(int DroneId);
        void DroneCollectsAPackage(int DroneId);
        void DroneDeliversPakcage(int DroneId);

    }
}
