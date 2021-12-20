using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BaseStationToList
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int AvailableChargeSlots { get; set; }
        public int OccupiedChargeSlots { get; set; }

        public override string ToString()
        {
            string ToReturn = "Base Station ID: " + Id + "\nName: " + Name + 
                "\nAvailable Charge Slots: " + AvailableChargeSlots +
                "\n Charging Drone List: " + OccupiedChargeSlots;
                
            return ToReturn;

        }
    }
}

