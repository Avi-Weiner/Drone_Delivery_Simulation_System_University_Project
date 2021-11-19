using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Location Location { get; set; }
            List<Package> PackagesFromCustomer { get; set; }
            List<Package> PackagesToCustomer { get; set; }
        }
    }
}
