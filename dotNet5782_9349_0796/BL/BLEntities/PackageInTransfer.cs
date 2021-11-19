using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class PackageInTransfer
        {
            public int ID { get; set; }
            public IDAL.DO.WeightCategory Weight { get; set; }
            public IDAL.DO.Priority Priority { get; set; }
            /// <summary>
            /// 0 = waiting for delivery, 1 = on delivery
            /// </summary>
            public bool DeliveryStatus { get; set; }
            public CustomerInPackage Sender { get; set; }
            public CustomerInPackage Receiver { get; set; }
            public Location CollectLocation { get; set; }
            public Location DeliveryLocation { get; set; }
            public int DeliveryDistance { get; set; }

        }
    }
}

