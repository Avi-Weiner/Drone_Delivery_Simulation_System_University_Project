using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public IDAL.DO.WeightCategory Weight { get; set; }
            public double BatteryStatus { get; set; }
            public DroneStatus Status { get; set; }
            public Location Location { get; set; }
            public Package PackageInTransfer { get; set; }

        }
    }
}

