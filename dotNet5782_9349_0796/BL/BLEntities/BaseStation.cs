using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class BaseStation
        {
            public int Id { get; set; }
            public int Name { get; set; }
            public Location Location { get; set; }
            public int AvailableChargeSlots { get; set; }
            public List<DroneInCharge> ChargingDroneList { get; set; }
        }
    }
}
