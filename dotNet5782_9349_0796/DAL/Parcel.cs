using System;

namespace IDAL
{
    namespace DO
    {
        
        public struct Parcel
        {
            public int Id { get; set; }
            public int SenderId { get; set; }
            public WeightCategory Weight { get; set; }

            //Priorities Priority; Priority needs to be defined
            public DateTime DateTime { get; set; }
            public int DroneId { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }
            
        }
        
    }
}
