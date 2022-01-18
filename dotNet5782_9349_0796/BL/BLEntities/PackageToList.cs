using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class PackageToList
    {
        public int Id { get; set; }
        /// <summary>
        /// Changed to Id (int) which makes a lot more sense then name
        /// </summary>
        public int SenderId { get; set; }
        /// <summary>
        /// Changed to Id (int) which makes a lot more sense then name
        /// </summary>
        public int ReceiverId { get; set; }
        public DO.WeightCategory Weight { get; set; }
        public DO.Priority Priority { get; set; }
        public PackageStatus PackageStatus { get; set; }

        public override string ToString()
        {
            return "PackageToList ID: " + Id +
                "\nSender ID: " + SenderId +
                "\nReceiver ID: " + ReceiverId +
                "\nWeight: " + Weight.ToString() +
                "\nPriority: " + Priority.ToString() +
                "\nStatus: " + PackageStatus.ToString() + '\n';
        }
    }
}

