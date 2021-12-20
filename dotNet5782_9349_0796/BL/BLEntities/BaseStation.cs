 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL
{
    public class BaseStation
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public Location Location { get; set; }
        public int AvailableChargeSlots { get; set; }
        public List<DroneInCharge> ChargingDroneList { get; set; }

        public override string ToString()
        {
            String ToReturn = "Base Station ID: " + Id + "\nName: " + Name
                + "\nLocation: " + "Longitude: " + Location.longitude + " Latitude: " + Location.latitude +
                "\nAvailable Charge Slots: " + AvailableChargeSlots +
                "\n Charging Drone List: ";
            foreach(DroneInCharge Drone in ChargingDroneList)
            {
                ToReturn += "\nID: " + Drone.Id + "\nBatteryStatus: " + Drone.BatteryStatus;
            }
            ToReturn += '\n';
            return ToReturn;

        }
    }
}
