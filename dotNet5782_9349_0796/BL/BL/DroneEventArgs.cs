using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class DroneEventArgs
    {
        private Drone drone;
        public Drone Drone { get { return drone; } private set { drone = value; } }
        public DroneEventArgs(Drone drone)
        {
            Drone = drone;
        }
    }
}
