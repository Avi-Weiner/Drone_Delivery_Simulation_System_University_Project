using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DalObject
{
    class DataSource
    {
        class Config
        {
            /// <summary>
            /// Index of first free element in DroneList. 
            /// </summary>
            static int FreeDronei = 0;
            /// <summary>
            /// Index of first free element in StationList.
            /// </summary>
            static int FreeStationi = 0;
            /// <summary>
            /// Index of first free element in CustomerList.
            /// </summary>
            static int FreeCustomeri = 0;
            /// <summary>
            /// Index of first free element in ParcelList.
            /// </summary>
            static int FreeParceli = 0;

            //Told to include the below field in exercise 1 but I am not sure what it is.
            //static int PackagesId;
        }

        static IDAL.DO.Drone[] DroneList = new IDAL.DO.Drone[10];
        static IDAL.DO.Station[] StationList = new IDAL.DO.Station[5];
        static IDAL.DO.Customer[] CustomerList = new IDAL.DO.Customer[100];
        static IDAL.DO.Parcel[] ParcelList = new IDAL.DO.Parcel[1000];

        /// <summary>
        /// Initialie all the arrays according to exercise 1 specs.
        /// </summary>
        static void Initialize() 
        {
            var rand = new Random();

            //2 base stations
            for (int i = 0; i < 2; i++)
            {
                //StationList[i].Id = rand.Next(10000,99999);


                
                //increment 
            }
            //5 drones
            for (int i = 0; i < 5; i++)
            {

            }
            //10 customers
            for (int i = 0; i < 10; i++)
            {

            }
            //10 packages
            for (int i = 0; i < 10; i++)
            {

            }
        }


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




    }
}

