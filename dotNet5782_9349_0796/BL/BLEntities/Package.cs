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
            /// <summary>
            /// Time package was created
            /// </summary>
            public DateTime CreationTime { get; set; }
            /// <summary>
            /// Time assigned to a drone
            /// </summary>
            public DateTime AssigningTime { get; set; }
            public DateTime CollectingTime { get; set; }
            public DateTime DeliveringTime { get; set; }

            public override string ToString()
            {
                string toReturn = "Package Id: " + Id + "\nSender: " + Sender.ToString() +
                    "\nReceiver: " + Receiver.ToString() + "\nWeight: " + Weight.ToString() + 
                    "\nPriority: " + Priority.ToString() + "\nDrone: " + Drone.ToString() + 
                    "\nCreation time: " + CreationTime + "\n Assigning time: " + AssigningTime 
                    + "\ncollecting time: " + CollectingTime + "\nDelivering time: " + DeliveringTime + "\n";
                return toReturn;
            }

        }
    }
}
