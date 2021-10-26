using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DalObject
{
    public class DataSource
    {
        class Config
        {
            /// <summary>
            /// Index of first free element in DroneList. 
            /// </summary>
            public static int FreeDronei { get; set; } = 0;
            /// <summary>
            /// Index of first free element in StationList.
            /// </summary>
            public static int FreeStationi { get; set; } = 0;
            /// <summary>
            /// Index of first free element in CustomerList.
            /// </summary>
            public static int FreeCustomeri { get; set; } = 0;
            /// <summary>
            /// Index of first free element in ParcelList.
            /// </summary>
            public static int FreeParceli { get; set; } = 0;

            //Told to include the below field in exercise 1 but I am not sure what it is.
            //static int PackagesId;
        }

        public static IDAL.DO.Drone[] DroneList = new IDAL.DO.Drone[10];
        public static IDAL.DO.Station[] StationList = new IDAL.DO.Station[5];
        public static IDAL.DO.Customer[] CustomerList = new IDAL.DO.Customer[100];
        public static IDAL.DO.Parcel[] ParcelList = new IDAL.DO.Parcel[1000];

        /// <summary>
        /// Initialie all the arrays according to exercise 1 specs.
        /// </summary>
        public static void Initialize()
        {
            var rand = new Random();

            //2 base stations
            for (int i = 0; i < 2; i++)
            {
                StationList[i].Id = StationList[i].Name = Config.FreeStationi;
                //Random double longitude between -180 and 180
                StationList[i].Longitude = rand.NextDouble() * 360 - 180;
                //Random double latitude between -90 and 90
                StationList[i].Latitude = rand.NextDouble() * 180 - 90;
                //Random int between 2 and 10
                StationList[i].ChargeSlots = rand.Next(2, 10);
                //Increment to the new emtpy station index.
                Config.FreeStationi++;
            }

            //5 drones
            for (int i = 0; i < 5; i++)
            {
                DroneList[i].Id = Config.FreeDronei;
                DroneList[i].Model = "";




        //public int Id { get; set; }
        //public string Model { get; set; }
        //public WeightCategory MaxWeight { get; set; }


        //public DroneStatus Status { get; set; }
        //public double battery { get; set; }

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

