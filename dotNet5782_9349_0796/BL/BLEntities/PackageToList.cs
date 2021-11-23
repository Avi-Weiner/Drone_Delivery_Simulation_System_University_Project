using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class PackageToList
        {
            public int Id { get; set; }
            public string SenderName { get; set; }
            public string ReceiverName { get; set; }
            public IDAL.DO.WeightCategory Weight { get; set; }
            public IDAL.DO.Priority Priority { get; set; }
            public PackageStatus PackageStatus { get; set; }

            public override string ToString()
            {
                return "PackageToList ID: " + Id +
                    "\nSender Name: " + SenderName +
                    "\nReceiver Name: " + ReceiverName +
                    "\nWeight: " + Weight.ToString() +
                    "\nPriority: " + Priority.ToString() +
                    "\nStatus: " + PackageStatus.ToString() + '\n';
            }
        }
    }
}

