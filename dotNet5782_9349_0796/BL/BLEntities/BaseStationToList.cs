using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class BaseStationToList
        {
            public int Id { get; set; }
            public int name { get; set; }
            public int NumberOfAvailableChargingSlots { get; set; }
            public int NumberOfOccupiedChargingSlots { get; set; }
        }
    }
}

