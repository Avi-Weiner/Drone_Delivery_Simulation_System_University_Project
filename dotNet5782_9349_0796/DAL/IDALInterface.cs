﻿using System;
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
        public int GetCustomer(int CustomerId);

        //Drone DalObject
        public DO.Drone GetDrone(int DroneId);
        public void AddDrone(string model, DO.WeightCategory Weight);

        //Station DalObject
        public DO.Station GetStation(int StationId);
        public void AddStation(int name, double longitude, double latitude, int slots);

        //No other methods work with static,
        //Without static they don't work in the main.
    }
}
