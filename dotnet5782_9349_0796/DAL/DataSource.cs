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
            /// Zero electricity consumption per km
            /// </summary>
            public static double Free { get; set; } = 0;
            /// <summary>
            /// percentage light electricity consumption per km
            /// </summary>
            public static double LightWeight { get; set; } = 0.001;
            /// <summary>
            /// percentage medium electricity consumption per km
            /// </summary>
            public static double MediumWeight { get; set; } = 0.005;
            /// <summary>
            /// percentage heavy electricity consumption per km
            /// </summary>
            public static double HeavyWeight { get; set; } = 0.01;
            /// <summary>
            /// percent charge rate per hour (120 = full charge in 30s)
            /// </summary>
            public static double ChargingRate { get; set; } = 120;

            //Told to include the below field in exercise 1 but I am not sure what it is.
            //static int PackagesId;

            /// <summary>
            /// Next Unique Id tracks the next availbe ID to be given out.
            /// </summary>
            public static int NextUniqueId { get; set; } = 1;

        }

        public static List<DO.Drone> DroneList = new List<DO.Drone>();
        public static List<DO.Station> StationList = new List<DO.Station>();
        public static List<DO.Customer> CustomerList = new List<DO.Customer>();
        public static List<DO.Package> PackageList = new List<DO.Package>();

        //Config getters
        public static double GetFree() { return Config.Free; }
        public static double GetLightWeight() { return Config.LightWeight; }
        public static double GetMediumWeight() { return Config.MediumWeight; }
        public static double GetHeavyWeight() { return Config.HeavyWeight; }
        /// <summary>
        /// Returns % charge rate per hour
        /// </summary>
        /// <returns></returns>
        public static double GetChargingRate() { return Config.ChargingRate; }
        /// <summary>
        /// Gets the next unique Id to be given out. 
        /// Does not increment NextUniqueID which is done by SetNextUniqueID()
        /// </summary>
        /// <returns></returns>
        public static int GetNextUniqueID() {Config.NextUniqueId += 1; return Config.NextUniqueId - 1; }

        //Config setters
        public static void SetNextUniqueID() { Config.NextUniqueId += 1; }



        /// <summary>
        /// Initialie all the arrays according to exercise 1 specs.
        /// </summary>
        public static void Initialize()
        {
            var rand = new Random();

            //2 base stations
            for (int i = 0; i < 2; i++)
            {
                StationList.Add(new DO.Station() { 
                    Id = GetNextUniqueID(),
                    Name = i,
                    Longitude = rand.NextDouble() * 360 - 180,
                    Latitude = rand.NextDouble() * 180 - 90,
                    ChargeSlots = rand.Next(2, 10)
                });
            }

            //5 drones
            for (int i = 0; i < 5; i++)
            {
                DroneList.Add(new DO.Drone()
                {
                    Id = GetNextUniqueID(),
                    Model = "Model T",
                    MaxWeight = (DO.WeightCategory)(rand.Next(0, 2)),
                });
            }

            //10 customers
            int ASCIIvalue = 65;
            for (int i = 0; i < 10; i++)
            {
                CustomerList.Add(new DO.Customer()
                {
                    Id = GetNextUniqueID(),
                    Name = ((char)ASCIIvalue).ToString(),
                    Longitude = rand.NextDouble() * 360 - 180,
                    Latitude = rand.NextDouble() * 180 - 90,
                    Phone = "000-0000-0000"
                });
                ASCIIvalue++;
            }

            //10 packages
            for (int i = 0; i < 10; i++)
            {
                PackageList.Add(new DO.Package()
                {
                    Id = GetNextUniqueID(),
                    SenderId = CustomerList[i].Id,
                    ReceiverId = CustomerList[(i + 1) % 10].Id,
                    Weight = (DO.WeightCategory)(rand.Next(0, 2)),
                    Priority = (DO.Priority)(rand.Next(0, 2)),
                    Requested = DateTime.Now,
                    DroneId = 0, //Not picked up yet = 0
                    Scheduled = (DateTime.Now).AddDays(5),
                    PickedUp = null,
                    Delivered = null
                });
            }//end of for loop for pakcages
        }//end of intialize;
    }//end of dataSource
}//end of dalObject

