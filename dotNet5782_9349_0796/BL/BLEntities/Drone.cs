using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Drone
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public DO.WeightCategory Weight { get; set; }
        public double BatteryStatus { get; set; }
        public DroneStatus Status { get; set; }
        public Location Location { get; set; }
#nullable enable
        /// <summary>
        /// Nullable package, if the drone isn't assigned to a package it is null
        /// </summary>
        public Package? PackageInTransfer { get; set; }
        public DateTime? ChargeStart { get; set; }
#nullable disable

        public override string ToString()
        {
            string x = "Drone ID: " + Id +
                "\nModel: " + Model +
                "\nWeight: " + Weight.ToString() +
                "\nBattery status: " + BatteryStatus.ToString("P") +
                "\nDrone status: " + Status.ToString() +
                "\nLocation: " + Location.ToString();
                 if (PackageInTransfer != null) {
                return x + ("\nPackage: " + PackageInTransfer.ToString());
            }
            return x;
        }
    }
}


