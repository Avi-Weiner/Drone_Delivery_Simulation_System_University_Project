using System;

namespace IDAL
{
    namespace DO
    {
        public struct Drone
        {
            public int Id { get; set; }
            public string Model { get; set; }
            //WeightCategories MaxWeight;
            //DroneStatuses Status;
            public double battery { get; set; }
        }
    }
}
