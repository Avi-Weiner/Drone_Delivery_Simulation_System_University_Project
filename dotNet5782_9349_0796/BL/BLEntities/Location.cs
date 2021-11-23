using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Location
        {
            public double longitude { get; set; }
            public double latitude { get; set; }

            public override string ToString()
            {
                return "Longitude: " + longitude + "    Latitude: " + latitude; 
            }
        }
    }
}

