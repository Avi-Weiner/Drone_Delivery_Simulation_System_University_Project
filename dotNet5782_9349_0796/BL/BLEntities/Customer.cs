using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Location Location { get; set; }
        public List<PackageToList> PackagesFromCustomer { get; set; }
        public List<PackageToList> PackagesToCustomer { get; set; }

        public override string ToString()
        {
            string toReturn = "Customer ID: " + Id + "\nName: " + Name + "\nPhone: " + Phone + "\nLocation: " + Location;
            foreach(PackageToList Pack in PackagesFromCustomer)
            {
                toReturn += "\n" + Pack.ToString();
            }
            foreach(PackageToList pack in PackagesToCustomer)
            {
                toReturn += "\n" + pack.ToString();
            }
            toReturn += "\n";
            return toReturn;
        }
    }
}

