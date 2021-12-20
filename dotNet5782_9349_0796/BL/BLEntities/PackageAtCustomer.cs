using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class PackageAtCustomer
    {
        public int Id { get; set; }
        public DO.WeightCategory Weight { get; set; }
        public PackageStatus PakcageStatus { get; set; }
        public Customer SourceCustomer { get; set; }
        public Customer DestinationCustomer { get; set; }
    }
}


