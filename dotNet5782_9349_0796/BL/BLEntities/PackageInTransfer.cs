using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL
{
    public class PackageInTransfer
    {
        public int ID { get; set; }
        public DO.WeightCategory Weight { get; set; }
        public DO.Priority Priority { get; set; }
        /// <summary>
        /// 0 = waiting for delivery, 1 = on delivery
        /// </summary>
        public bool DeliveryStatus { get; set; }
        public CustomerInPackage Sender { get; set; }
        public CustomerInPackage Receiver { get; set; }
        public Location CollectLocation { get; set; }
        public Location DeliveryLocation { get; set; }
        public int DeliveryDistance { get; set; }

        public override string ToString()
        {
            return "Package ID: " + ID +
                "\nWeight Category: " + Weight.ToString() +
                "\nPriority: " + Priority.ToString() +
                "\nDelivery Status (true = delivered, false = undelivered): " + DeliveryStatus.ToString() +
                "\nSender: " + Sender.ToString() +
                "\nReceiver: " + Receiver.ToString() +
                "\nCollection Location: " + CollectLocation.ToString() + 
                "\nDelivery LocationL " + DeliveryLocation + '\n';
        }
    }
}


