using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        /// <summary>
        /// Status of a Drone: free, maintenance, deliver
        /// </summary>
        public enum DroneStatus {free, maintenance, delivery }
        /// <summary>
        /// Status of Package: created, assigned, collected, delivered
        /// </summary>
        public enum PackageStatus { created, assigned, collected, delivered }

    }
}