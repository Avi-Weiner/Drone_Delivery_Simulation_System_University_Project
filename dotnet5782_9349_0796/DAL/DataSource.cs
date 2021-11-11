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
            public static int NextUniqueId { get; set; } = 1;

        }

        //public static IDAL.DO.Drone[] DroneList = new IDAL.DO.Drone[10];
        //public static IDAL.DO.Station[] StationList = new IDAL.DO.Station[5];
        //public static IDAL.DO.Customer[] CustomerList = new IDAL.DO.Customer[100];
        //public static IDAL.DO.Parcel[] ParcelList = new IDAL.DO.Parcel[1000];
        public static List<IDAL.DO.Drone> DroneList = new List<IDAL.DO.Drone>();
        public static List<IDAL.DO.Station> StationList = new List<IDAL.DO.Station>();
        public static List<IDAL.DO.Customer> CustomerList = new List<IDAL.DO.Customer>();
        public static List<IDAL.DO.Parcel> ParcelList = new List<IDAL.DO.Parcel>();

        //getters for knowing how many stations/drones/customer we have and have
        //left. Mainly used for public access by DalObjects.
        //!!!!It is the responsibilty of the user to use the set method as well
        //when allocating a new station/drone/custoemr/parcel/ID
        public static int GetFreeStationI() { return Config.FreeStationi; }  
        public static int GetFreeCustomerI() { return Config.FreeCustomeri; }
        public static int GetFreeParcelI() { return Config.FreeParceli; }
        public static int GetNextUniqueID() {  return Config.NextUniqueId; }
        public static int GetFreeDroneI() { return Config.FreeDronei; }

        //setters for new amount of station/drone/cutomer.
        public static void SetFreeStation() { Config.FreeStationi += 1; }
        public static void SetFreeDrone() { Config.FreeDronei += 1; }
        public static void SetFreeCustomer() { Config.FreeCustomeri += 1; }
        public static void SetFreeParcel() { Config.FreeParceli += 1; }
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
                StationList.Add(new IDAL.DO.Station() { 
                    Id = Config.NextUniqueId,
                    Name = i,
                    Longitude = rand.NextDouble() * 360 - 180,
                    Latitude = rand.NextDouble() * 180 - 90,
                    ChargeSlots = rand.Next(2, 10)
                });
                //Increment to the new emtpy station index.
                
                Config.NextUniqueId++;
            }

            //5 drones
            for (int i = 0; i < 5; i++)
            {
                DroneList.Add(new IDAL.DO.Drone()
                {
                    Id = Config.NextUniqueId,
                    Model = "Model T",
                    MaxWeight = (IDAL.DO.WeightCategory)(rand.Next(0, 2)),
                });
                
                Config.NextUniqueId++;
            }

            //10 customers
            char letter = 'A'; //Customer names will just be letters of the alphabet, incremented.
            for (int i = 0; i < 10; i++)
            {
                CustomerList.Add(new IDAL.DO.Customer()
                {
                    Id = Config.NextUniqueId,
                    Name = letter.ToString(),
                    Longitude = rand.NextDouble() * 360 - 180,
                    Latitude = rand.NextDouble() * 180 - 90,
                    Phone = "000-0000-0000"
                });
                
                Config.NextUniqueId++;
            }

            //10 packages
            for (int i = 0; i < 10; i++)
            {
                ParcelList.Add(new IDAL.DO.Parcel()
                {
                    Id = Config.NextUniqueId,
                    SenderId = CustomerList[i].Id,
                    ReceiverId = CustomerList[i%10].Id,
                    Weight = (IDAL.DO.WeightCategory)(rand.Next(0, 2)),
                    Priority = (IDAL.DO.Priority)(rand.Next(0, 2)),
                    Requested = DateTime.Now,
                    DroneId = 0, //Not picked up yet = 0
                    Scheduled = (DateTime.Now).AddDays(5),
                    PickedUp = null,
                    Delivered = null
                }) ;
               
                
            }

        }

    }
}

