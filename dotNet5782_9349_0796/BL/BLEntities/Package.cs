using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Package
        {
            public int Id { get; set; }
            public Customer Sender { get; set; }
            public Customer Receiver { get; set; }
            public IDAL.DO.WeightCategory Weight { get; set; }
            public IDAL.DO.Priority Priority { get; set; }
            /// <summary>
            /// #nullable
            /// </summary>
            public Drone? Drone { get; set; } 
            public DateTime CreationTime { get; set; }
            public DateTime AssigningTime { get; set; }
            public DateTime CollectingTime { get; set; }
            public DateTime DeliveringTime { get; set; }

        }
    }
}
