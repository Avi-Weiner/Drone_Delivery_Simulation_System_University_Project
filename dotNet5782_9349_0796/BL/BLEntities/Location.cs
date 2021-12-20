using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL
{
    public class Location
    {
        public double longitude { get; set; }
        public double latitude { get; set; }

        public override string ToString()
        {
            return "Longitude: " + string.Format("{0:0.00}", longitude) + " Latitude: " + string.Format("{0:0.00}", latitude); 
        }
    }
}


