﻿using System;

namespace BL
{
    public interface IBL
    {
        void AddBaseStation(int Id, string name, double location, int slots);
        void AddDrone(int manufactureId, string model, /*enum wieght*/ int chargingStation);
        void AddCustomer(int CustomerId, string name, string phone, double Location);
        void AddPackage(int CustomerId, int ReceiverId /*weight, Priority*/ );
        void UpdateDrone(int Id, string Model);
        void UpdateStation(string StationName = "", int chargingStation = 0);
        void UpdateCustomer(int Id, string Name = "", string Phone = "");
        void SendDroneToCharge(int DroneId);

    }
}