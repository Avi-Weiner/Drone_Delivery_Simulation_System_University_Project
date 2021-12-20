using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class CustomerToList
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string phone { get; set; }
        public int NumberOfDeliveredPackagesSent { get; set; }
        public int NumberOfUndeliveredPackagesSent { get; set; }
        public int NumberOfRecievedPackages { get; set; }
        public int NumberOfExpectedPackages { get; set; }

        public override string ToString()
        {
            return "Customer To List ID: " + Id +
                "\nName: " + Name +
                "\nPhone: " + phone +
                "\nNumber of Delivered Packages sent: " + NumberOfDeliveredPackagesSent +
                "\nNumber of Underlivered Packages Sent: " + NumberOfUndeliveredPackagesSent +
                "\nNumber of Received Packages: " + NumberOfRecievedPackages +
                "\nNumber of Expected Packages: " + NumberOfExpectedPackages + '\n';
        }
    }
}

