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

            /// <summary>
            /// Next Unique Id tracks the next availbe ID to be given out.
            /// </summary>
            public static int NextUniqueId { get; set; } = 0;
            
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
                StationList[i].Id = StationList[i].Name = Config.NextUniqueId;
                //Random double longitude between -180 and 180
                StationList[i].Longitude = rand.NextDouble() * 360 - 180;
                //Random double latitude between -90 and 90
                StationList[i].Latitude = rand.NextDouble() * 180 - 90;
                //Random int between 2 and 10
                StationList[i].ChargeSlots = rand.Next(2, 10);
                //Increment to the new emtpy station index.
                Config.FreeStationi++;
                Config.NextUniqueId++;
            }

            //5 drones
            for (int i = 0; i < 5; i++)
            {
                DroneList[i].Id = Config.NextUniqueId;
                DroneList[i].Model = "Model T";
                //Make the weight catagory have a random value between 0 and 2
                DroneList[i].MaxWeight = (IDAL.DO.WeightCategory)(rand.Next(0,2));
                //All drones start free.
                DroneList[i].Status = IDAL.DO.DroneStatus.free;
                //Max battery
                DroneList[i].battery = 1;
                Config.FreeDronei++;
                Config.NextUniqueId++;
            }

            //10 customers
            char letter = 'A'; //Customer names will just be letters of the alphabet, incremented.
            for (int i = 0; i < 10; i++)
            {
                CustomerList[i].Id = Config.FreeCustomeri;
                CustomerList[i].Name = letter.ToString();
                CustomerList[i].Phone = "000-0000-0000";

                //public int Id { get; set; }
                //public string Name { get; set; }
                //public string Phone { get; set; }
                //public double Longitude { get; set; }
                //public double Latitude { get; set; }

                Config.FreeCustomeri++;
                
            }
            //10 packages
            for (int i = 0; i < 10; i++)
            {

            }
        }

    }
}

