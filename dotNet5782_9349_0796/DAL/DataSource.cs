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

       
        

    }
}

