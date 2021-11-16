using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DalObject
{
    public class DalObject
    {
        //creates a DAL object by intializing values accordign to Initialize
        DalObject()
        {
            DataSource.Initialize();//constructor for DalObjects
        }

        /// <summary>
        /// Receives Station Id and returns index of station in StationList
        /// </summary>
        public static int GetStation(int StationId)
        {
            int i = 0;
            while (i < DataSource.GetFreeStationI() && DataSource.StationList[i].Id != StationId) //Cycle through StationList until StationId is found
                i++;
            if (DataSource.StationList[i].Id != StationId)
            {
                Console.WriteLine("Error: Station not found.");
                return 0;
            }
            return i;
        }

        /// <summary>
        /// Receives Drone ID and returns its index in DroneList
        /// </summary>
        public static int GetDrone(int DroneId)
        {
            int i = 0;
            while (i < DataSource.GetFreeDroneI() && DataSource.DroneList[i].Id != DroneId) //Cycle through StationList until StationId is found
                i++;
            if (DataSource.DroneList[i].Id != DroneId)
            {
                Console.WriteLine("Error: Drone not found.");
                return 0;
            }
            return i;
        }
            
        /// <summary>
        /// Receives Package Id and returns its index in ParcelList
        /// </summary>
        /// <param name="PackageId"></param>
        /// <returns></returns>
        public static int GetPackage(int PackageId)
        {
            int i = 0;
            while (i < DataSource.GetFreeParcelI() && DataSource.ParcelList[i].Id != PackageId) //Cycle through ParcelList until Package is found
                i++;

            if (DataSource.ParcelList[i].Id != PackageId)
            {
                Console.WriteLine("Error: Package not found.");
                return 0;
            }
            return i;
        }

        /// <summary>
        /// Receives Customer Id and returns its index in CustomerList
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public static int GetCustomer(int CustomerId)
        {
            int i = 0;
            while (i < DataSource.GetFreeCustomerI() && DataSource.CustomerList[i].Id != CustomerId)
                i++;

            if (DataSource.CustomerList[i].Id != CustomerId)
            {
                Console.WriteLine("Error: Customer not found.");
                return 0;
            }
            return i;
        }

        /// <summary>
        /// adding base station to the stations list
        /// </summary>
        public static void AddStation()
        {
            Console.WriteLine("Enter Longitude: ");
            double longitude = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter Latitude: ");
            double latitude = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter number of charge slots: ");
            int slots = Convert.ToInt32(Console.ReadLine());
            //assuming that no maximum amount of sattions
            //add station to the back of the station list
            DataSource.StationList.Add(new IDAL.DO.Station
            {
                Id = DataSource.GetNextUniqueID(),
                Longitude = longitude,
                Latitude = latitude,
                ChargeSlots = slots
            });
        }

        /// <summary>
        /// adding a drone to the existing drones list
        /// </summary>
        public static void AddDrone()
        {
            Console.WriteLine("Enter drone model:");
            string model = Console.ReadLine();
            Console.WriteLine("Enter weight category as string: option are light, medium and heavy:");
            IDAL.DO.WeightCategory ThisWeight = (IDAL.DO.WeightCategory)Enum.Parse(typeof(IDAL.DO.WeightCategory), Console.ReadLine());
            //add the new drone to the back of the list
            DataSource.DroneList.Add(new IDAL.DO.Drone
            {
                Id = DataSource.GetNextUniqueID(),
                Model = model,
                MaxWeight = ThisWeight
            });
        }

        /// <summary>
        /// adding a new customer to the customers list
        /// </summary>
        public static void AddCustomer()
        {
            Console.WriteLine("Enter name: ");
            string name = Console.ReadLine();

            Console.WriteLine("Enter phone: ");
            string phone = Console.ReadLine();
            Console.WriteLine("Enter longitude: ");
            double longitude = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter latitude: ");
            double latitude = Convert.ToDouble(Console.ReadLine());
            //add customer to the back of the customer list
            DataSource.CustomerList.Add(new IDAL.DO.Customer
            {
                Id = DataSource.GetNextUniqueID(),
                Name = name,
                Phone = phone,
                Longitude = longitude,
                Latitude = latitude

            });
        }

        /// <summary>
        /// receiving a package to deliver
        /// </summary>
        public static void AddPackage()
        {
            Console.WriteLine("Enter Sender ID: ");
            int InputSender = Convert.ToInt32(Console.ReadLine()); //Does not check if Customer exists as Exercise 1 says not to
            Console.WriteLine("Enter Receiver ID: ");
            int InputReciever = Convert.ToInt32(Console.ReadLine());//Does not check if Customer exists as Exercise 1 says not to
            Console.WriteLine("Enter Weight Catagory ('light', 'medium', 'heavy'): ");
            IDAL.DO.WeightCategory InputWeight = (IDAL.DO.WeightCategory)Enum.Parse(typeof(IDAL.DO.WeightCategory), Console.ReadLine());
            Console.WriteLine("Enter Priority ('regular', 'fast', 'emergency'): ");
            IDAL.DO.Priority InputPriority = (IDAL.DO.Priority)Enum.Parse(typeof(IDAL.DO.Priority), Console.ReadLine());
            DataSource.ParcelList.Add(new IDAL.DO.Parcel
            {
                Id = DataSource.GetNextUniqueID(),
                SenderId = InputSender,
                ReceiverId = InputReciever,
                Weight = InputWeight,
                Priority = InputPriority,
                Requested = DateTime.Now,
                DroneId = 0, //No drone assigned yet
                Scheduled = null,
                PickedUp = null,
                Delivered = null

        });
           
        }

        /// <summary>
        /// Assigning a package to a drone
        /// </summary>
        /// <param name="PackageId"></param>
        /// <param name="DroneId"></param>
        public static void AssignDroneToPackage(int PackageId, int DroneId)
        {   
            //Checks if drone Id is valid
            GetDrone(DroneId);
            IDAL.DO.Parcel P = DataSource.ParcelList.Find(x => x.Id == PackageId);
            
            P.DroneId = DroneId;
            P.Scheduled = DateTime.Now;
         
        }


        /// <summary>
        /// Receives Drone with ID DroneId and picks up package with ID PackageId
        /// </summary>
        /// <param name="PackageId"></param>
        /// <param name="DroneId"></param>
        public static void DronePickUp(int PackageId, int DroneId)
        {
            IDAL.DO.Parcel P = DataSource.ParcelList.Find(x => x.Id == PackageId);
            IDAL.DO.Drone D = DataSource.DroneList.Find(x => x.Id == DroneId);
            //Do we need to check if the drone is free?
            if(P.Id == PackageId && D.Id == DroneId)
            {
                P.PickedUp = DateTime.Now;
                P.DroneId = DroneId;
            }
        }

        /// <summary>
        /// Receives a package from Id PackageId and "drops it off" with a customer
        /// </summary>
        /// <param name="PackageId"></param>
        public static void PackageDropOff(int PackageId)
        {
            IDAL.DO.Parcel P = DataSource.ParcelList.Find(x => x.Id == PackageId);
            P.Delivered = DateTime.Now;

            //DataSource.ParcelList[p].Delivered = DateTime.Now;
            //int DroneId = DataSource.ParcelList[p].DroneId;
            //int i = GetDrone(DroneId);
        }

        /// <summary>
        /// Sends a drone to get charged at the specified station
        /// </summary>
        /// <param name="DroneId"></param>
        /// <param name="StationId"></param>
        public static void ChargeDrone(int DroneId, int StationId)
        {
            //Get Drone
            int i = GetDrone(DroneId);

            //Get station
            IDAL.DO.Station S = DataSource.StationList.Find(x => x.Id == StationId);
            IDAL.DO.Drone D = DataSource.DroneList.Find(x => x.Id == DroneId);
            if (S.ChargeSlots == 0)
            {
                Console.WriteLine("Error: No free charge slots at specified station.");
            }

            //minus 1 to charge slots
            S.ChargeSlots--;

            //Adding instance of Dronecharger (Need to save this somewhere or it will just get deleted...),
            //specs unspecific of where to save it so for the meantime it will be deleted
            IDAL.DO.DroneCharger newCharger = new IDAL.DO.DroneCharger();
            newCharger.DroneId = D.Id;
            newCharger.StationId = S.Id;
        }

        /// <summary>
        /// Releases Drone with ID DroneID from station with ID StationID
        /// </summary>
        /// <param name="DroneID"></param>
        /// <param name="StationID"></param>
        public static void ReleaseDrone(int DroneId, int StationId)
        {
            //Get Drone
            int i = GetDrone(DroneId);

            //Get station
            int j = GetStation(StationId);

            //Free up charge slot 
            IDAL.DO.Station s = DataSource.StationList.Find(x => x.Id == StationId);
            s.ChargeSlots++; //Not sure if this line actually increments the station or just a copy of the station in s
            
            //DataSource.StationList[j].ChargeSlots++;
            
        }

        /// <summary>
        /// Prints Station from received station s
        /// </summary>
        /// <param name="i"></param>
        public static void PrintStation(IDAL.DO.Station s)
        {
            Console.WriteLine("\nBase Station ID: " + s.Id
               + "\nBase Station  name: " + s.Name
               + "\nBase Station Longitude: " + s.Longitude
               + "\nBase Station Latitude: " + s.Latitude
               + "\nBase Station # of Charging slots: " + s.ChargeSlots);
        }

        /// <summary>
        /// Prints the base station from the given ID
        /// </summary>
        /// <param name="Id"></param>
        public static void DisplayBaseStation(int Id)
        {
            IDAL.DO.Station s = DataSource.StationList.Find(x => x.Id == Id);
            PrintStation(s);
        }

        /// <summary>
        /// Print Drone from the index i
        /// </summary>
        /// <param name="i"></param>
        public static void PrintDrone(int i)
        {
            Console.WriteLine("\nDrone ID: " + DataSource.DroneList[i].Id
                + "\nDrone Model: " + DataSource.DroneList[i].Model
                + "\nDrone MaxWeight: " + DataSource.DroneList[i].MaxWeight.ToString());
        }

        /// <summary>
        /// Prints the details of the drone with the given ID
        /// </summary>
        /// <param name="Id"></param>
        public static void DisplayDrone(int Id)
        {
            int p = GetDrone(Id);
            PrintDrone(p);
        }

        /// <summary>
        /// Prints a customer at indexx in CustomerList
        /// </summary>
        /// <param name="i"></param>
        public static void PrintCustomer(int i)
        {
            Console.WriteLine("\nCustomer ID: " + DataSource.CustomerList[i].Id
                + "\nCustomer Name: " + DataSource.CustomerList[i].Name
                + "\nCustomer Phone: " + DataSource.CustomerList[i].Phone
                + "\nCustomer Longitude: " + DataSource.CustomerList[i].Longitude
                + "\nCustomer Latitude: " + DataSource.CustomerList[i].Latitude);
        }

        /// <summary>
        /// Prints the customer with ID Id
        /// </summary>
        /// <param name="Id"></param>
        public static void DisplayCustomer(int Id)
        {
            int p = GetCustomer(Id);
            PrintCustomer(p);
        }

        /// <summary>
        /// Prints a package given its' index in ParcelList
        /// </summary>
        /// <param name="i"></param>
        public static void PrintPackage(int i)
        {
            Console.WriteLine("\nPackage ID: " + DataSource.ParcelList[i].Id
                + "\nSender ID: " + DataSource.ParcelList[i].SenderId
                + "\nReceiver ID: " + DataSource.ParcelList[i].ReceiverId
                + "\nWeight: " + DataSource.ParcelList[i].Weight.ToString()
                + "\nPriority: " + DataSource.ParcelList[i].Priority.ToString()
                + "\nRequested Date: " + DataSource.ParcelList[i].Requested.ToString()
                + "\nDrone ID: " + DataSource.ParcelList[i].DroneId
                + "\nScheduled: " + DataSource.ParcelList[i].Scheduled.ToString()
                + "\nPick up Date: " + DataSource.ParcelList[i].PickedUp.ToString()
                + "\nDelivery Date: " + DataSource.ParcelList[i].Delivered.ToString());
        }

        /// <summary>
        /// Displays the package info of the given package id.
        /// </summary>
        /// <param name="Id"></param>
        public static void DisplayPackage(int Id)
        {
            int i = GetPackage(Id);
            PrintPackage(i);
        }

        /// <summary>
        /// Displays all the stations in StationList.
        /// </summary>
        public static void DisplayStationList()
        {
            foreach (IDAL.DO.Station s in DataSource.StationList)
            {
                PrintStation(s);
            }

        }

        /// <summary>
        /// Displays all the drones in DroneList
        /// </summary>
        public static void DisplayDroneList()
        {
            for (int i = 0; i < DataSource.GetFreeDroneI(); i++)
                PrintDrone(i);
        }

        /// <summary>
        /// Displays all the customers in CustomerList
        /// </summary>
        public static void DisplayCustomerList()
        {
            for (int i = 0; i < DataSource.GetFreeCustomerI(); i++)
                PrintCustomer(i);
        }

        /// <summary>
        /// Displays all the Parcels in ParcelList
        /// </summary>
        public static void DisplayParcelList()
        {
            for (int i = 0; i < DataSource.GetFreeParcelI(); i++)
                PrintPackage(i);
        }

        /// <summary>
        /// Displays packages that are not yet assigned to a drone
        /// </summary>
        public static void DisplayUnassignedPackages()
        {
            for (int i = 0; i < DataSource.GetFreeParcelI(); i++)
            {
                if (DataSource.ParcelList[i].DroneId == 0)
                    PrintPackage(i);
            }
        }

        /// <summary>
        /// Displays Stations with unoccupied charging stations
        /// </summary>
        public static void DisplayFreeChargingStations()
        {
            foreach(IDAL.DO.Station s in DataSource.StationList)
            {
                if (s.ChargeSlots > 0)
                    PrintStation(s);     
            }
        }
    }
}




