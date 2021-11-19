using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class CustomerToList
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public string phone { get; set; }
            public int NumberOfDeliveredPckagesSent { get; set; }
            public int NumberOfUndeliveredPackagesSent { get; set; }
            public int NumberOfRecievedPackages { get; set; }
            public int NumberOfExpectedPackages { get; set; }

        }
    }
}

