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

            public Priority Priority { get; set; }
            public DateTime DateTime { get; set; }
            public int DroneId { get; set; }
            public DateTime Scheduled { get; set; }
            public DateTime PickedUp { get; set; }
            public DateTime Delivered { get; set; }
            
        }
        
    }
}
