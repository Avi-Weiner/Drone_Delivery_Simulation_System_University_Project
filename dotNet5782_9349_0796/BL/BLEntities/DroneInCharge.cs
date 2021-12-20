using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class DroneInCharge
    {
        public int Id{ get; set; }
        public double BatteryStatus { get; set; }
        public override string ToString()
        {
            return "Drone in charge ID: " + Id + "\nBattery Status: " + BatteryStatus + "\n";
        }
    }
}


