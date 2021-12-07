﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class DroneToList
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public IDAL.DO.WeightCategory Weight { get; set; }
            public double BatteryStatus { get; set; }
            public DroneStatus DroneStatus { get; set; }
            public Location Location { get; set; }
            public int? PackageId { get; set; }

            public override string ToString()
            {
                return "\nDrone ID: " + Id +
                    " Model: " + Model +
                    " Weight: " + Weight.ToString() +
                    "\nBattery status: " + string.Format("{0:0.00}%", BatteryStatus) +
                    " Drone status: " + DroneStatus.ToString() +
                    " Location: " + Location.ToString() +
                    " Package: " + PackageId;
            }
        }
    }
}

