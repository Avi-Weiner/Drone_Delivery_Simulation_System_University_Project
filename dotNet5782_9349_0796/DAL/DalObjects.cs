using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    namespace DalObject
    {
        public class DalObject
        {
            //I think the following methods need to be added.
            //Adding to lists methods

            //adding base station to the stations list
            public void AddStation()
            {

            }

            //adding a drone to the existing drones list
            public IDAL.DO.Drone AddDrone()
            {

            }


            //adding a new customer to the customers list
            public IDAL.DO.Customer AddCustomer()
            {

            }


            //receiving a package to deliver
            public IDAL.DO.Parcel AddPackage()
            {

            }


            //Updating existing data
            //assigning a package to a drone" +

            public void AssignDroneToPackage()
            {

            }


            //        "\n - collecting a package by a drone" +
            public void DronePickUp()
            {

            }

            //        "\n - providing a package to a customer" +
            public void PackageDropOff()
            {

            }


            //        "\n - sending a drone to a charge in a base station" +
            //        "\n   - by changing the drone’s status and adding a record(instance) of" +
            //        "\n     a drone battery charger entity" +

            //        "\n   - the station is selected by the user in the main menu(It is" +
            //        "\n     recommended to provide a list of stations to the user)" +
            //        "\n - releasing a drone from charging in a base station");
            public void ChargeDrone(IDAL.DO.Drone drone, IDAL.DO.DroneCharge)
            {

            }






        }
    }
}
}
