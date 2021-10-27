using System;

namespace IDAL
{
    namespace DO
    {
        public struct Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            public WeightCategory MaxWeight { get; set; }
            
                
            public DroneStatus Status { get; set; }
            public double battery { get; set; }
        }
    }
}
