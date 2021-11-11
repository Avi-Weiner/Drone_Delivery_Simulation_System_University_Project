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
            if (DataSource.GetFreeStationI() < 5)
            {
                int ThisStationNumber = DataSource.GetFreeStationI();
                DataSource.StationList[ThisStationNumber].Id = DataSource.GetNextUniqueID();
                Console.WriteLine("Enter Longitude: ");
                DataSource.StationList[ThisStationNumber].Longitude = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter Latitude: ");
                DataSource.StationList[ThisStationNumber].Latitude = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter number of charge slots: ");
                DataSource.StationList[ThisStationNumber].ChargeSlots = Convert.ToInt32(Console.ReadLine());
                DataSource.SetNextUniqueID();
                DataSource.SetFreeStation();
            }
            else
            {
            Console.WriteLine("Error: Too many Stations");
            }

        }

        /// <summary>
        /// adding a drone to the existing drones list
        /// </summary>
        public static void AddDrone()
        {
            int ThisDroneNumber = DataSource.GetFreeDroneI();
            if (ThisDroneNumber < 10)
            {
            DataSource.SetFreeDrone();
                DataSource.DroneList[ThisDroneNumber].Id = DataSource.GetNextUniqueID();
                DataSource.SetNextUniqueID();
                Console.WriteLine("Enter drone model:");
                DataSource.DroneList[ThisDroneNumber].Model = Console.ReadLine();
                Console.WriteLine("Enter weight category as string: option are light, medium and heavy:");
                IDAL.DO.WeightCategory ThisWeight = (IDAL.DO.WeightCategory)Enum.Parse(typeof(IDAL.DO.WeightCategory), Console.ReadLine());
            //All Drones start Free.
                DataSource.DroneList[ThisDroneNumber].Status = IDAL.DO.DroneStatus.free;
            //Max battery. Drone arrives fully charged. 0 is empty 1 is full and everything in between.
                DataSource.DroneList[ThisDroneNumber].battery = 1;
            }
            else
            {
                Console.WriteLine("Error: Too many drones.\n");
            }
        }

        /// <summary>
        /// adding a new customer to the customers list
        /// </summary>
        public static void AddCustomer()
        {
            int ThisCustomerNumber = DataSource.GetFreeCustomerI();
            if(ThisCustomerNumber < 100)
            {
            DataSource.SetFreeCustomer();
            DataSource.CustomerList[ThisCustomerNumber].Id = DataSource.GetNextUniqueID();
            DataSource.SetNextUniqueID();
            DataSource.CustomerList[ThisCustomerNumber].Name = Console.ReadLine();
            DataSource.CustomerList[ThisCustomerNumber].Phone = Console.ReadLine();
            Console.WriteLine("Enter longitude: ");
            DataSource.CustomerList[ThisCustomerNumber].Longitude = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter latitude: ");
            DataSource.CustomerList[ThisCustomerNumber].Latitude = Convert.ToDouble(Console.ReadLine());
            }
        }

        /// <summary>
        /// receiving a package to deliver
        /// </summary>
        public static void AddPackage()
        {
            if (DataSource.GetFreeParcelI() < 1000) // 1000 max customers
            {
                int ThisPackage = DataSource.GetFreeParcelI();
                DataSource.ParcelList[ThisPackage].Id = DataSource.GetNextUniqueID();

                //User inputs:
                Console.WriteLine("Enter Sender ID: ");
                DataSource.ParcelList[ThisPackage].SenderId = Convert.ToInt32(Console.ReadLine()); //Does not check if Customer exists as Exercise 1 says not to
                Console.WriteLine("Enter Receiver ID: ");
                DataSource.ParcelList[ThisPackage].ReceiverId = Convert.ToInt32(Console.ReadLine());//Does not check if Customer exists as Exercise 1 says not to
                Console.WriteLine("Enter Weight Catagory ('light', 'medium', 'heavy'): ");
                DataSource.ParcelList[ThisPackage].Weight = (IDAL.DO.WeightCategory)Enum.Parse(typeof(IDAL.DO.WeightCategory), Console.ReadLine());
                Console.WriteLine("Enter Priority ('regular', 'fast', 'emergency'): ");
                DataSource.ParcelList[ThisPackage].Priority = (IDAL.DO.Priority)Enum.Parse(typeof(IDAL.DO.Priority), Console.ReadLine());

                //Rest are System inputs
                DataSource.ParcelList[ThisPackage].Requested = DateTime.Now;
                DataSource.ParcelList[ThisPackage].DroneId = 0; //No drone assigned yet
                DataSource.ParcelList[ThisPackage].Scheduled = null;
                DataSource.ParcelList[ThisPackage].PickedUp = null;
                DataSource.ParcelList[ThisPackage].Delivered = null;

                DataSource.SetNextUniqueID();
                DataSource.SetFreeParcel();
            }
            else
            {
                Console.WriteLine("Error: Too many Packages \n");
            }
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

            int j = GetPackage(PackageId);
                
            //Asign Drone to package
            DataSource.ParcelList[j].DroneId = DroneId;
            DataSource.ParcelList[j].Scheduled = DateTime.Now;
        }


        /// <summary>
        /// Receives Drone with ID DroneId and picks up package with ID PackageId
        /// </summary>
        /// <param name="PackageId"></param>
        /// <param name="DroneId"></param>
        public static void DronePickUp(int PackageId, int DroneId)
        {
            int i = GetDrone(DroneId);
            int p = GetPackage(PackageId);

            if(DataSource.ParcelList[p].Id == PackageId && DataSource.DroneList[i].Id == DroneId)
            {
            DataSource.ParcelList[p].PickedUp = DateTime.Now;
            DataSource.DroneList[i].Status = IDAL.DO.DroneStatus.delivery;
            DataSource.ParcelList[p].DroneId = DroneId;
            }
        }

        /// <summary>
        /// Receives a package from Id PackageId and "drops it off" with a customer
        /// </summary>
        /// <param name="PackageId"></param>
        public static void PackageDropOff(int PackageId)
        {
            int p = GetPackage(PackageId);

            DataSource.ParcelList[p].Delivered = DateTime.Now;
            int DroneId = DataSource.ParcelList[p].DroneId;
            int i = GetDrone(DroneId);

            DataSource.DroneList[i].Status = IDAL.DO.DroneStatus.free;
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
            int j = GetStation(StationId);
            if (DataSource.StationList[j].ChargeSlots == 0)
            {
                Console.WriteLine("Error: No free charge slots at specified station.");
            }

            //minus 1 to charge slots
            DataSource.StationList[j].ChargeSlots--;

            //Make the battery full
            DataSource.DroneList[i].battery = 1;
            DataSource.DroneList[i].Status = IDAL.DO.DroneStatus.maintenance;

            //Adding instance of Dronecharger (Need to save this somewhere or it will just get deleted...),
            //specs unspecific of where to save it so for the meantime it will be deleted
            IDAL.DO.DroneCharger newCharger = new IDAL.DO.DroneCharger();
            newCharger.DroneId = DataSource.DroneList[i].Id;
            newCharger.StationId = DataSource.StationList[j].Id;
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
            DataSource.StationList[j].ChargeSlots++;
            
        }

        /// <summary>
        /// Prints Station from index in StationList.
        /// </summary>
        /// <param name="i"></param>
        public static void PrintStation(int i)
        {
            Console.WriteLine("Base Station ID: " + DataSource.StationList[i].Id
               + "\nBase Station  name: " + DataSource.StationList[i].Name
               + "\nBase Station Longitude: " + DataSource.StationList[i].Longitude
               + "\nBase Station Latitude: " + DataSource.StationList[i].Latitude
               + "\nBase Station # of Charging slots: " + DataSource.StationList[i].ChargeSlots);
        }

        /// <summary>
        /// Prints the base station from the given ID
        /// </summary>
        /// <param name="Id"></param>
        public static void DisplayBaseStation(int Id)
        {
            int p = GetStation(Id);
            PrintStation(p);
        }

        /// <summary>
        /// Print Drone from the index i
        /// </summary>
        /// <param name="i"></param>
        public static void PrintDrone(int i)
        {
            Console.WriteLine("Drone ID: " + DataSource.DroneList[i].Id
                + "\nDrone Model: " + DataSource.DroneList[i].Model
                + "\nDrone MaxWeight: " + DataSource.DroneList[i].MaxWeight.ToString()
                + "\nDrone Status: " + DataSource.DroneList[i].Status.ToString()
                + "\nDrone Battery: " + DataSource.DroneList[i].battery);
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
            Console.WriteLine("Customer ID: " + DataSource.CustomerList[i].Id
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
            Console.WriteLine("Package ID: " + DataSource.ParcelList[i].Id
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
            for (int i = 0; i < DataSource.GetFreeStationI(); i++)
                PrintStation(i);
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
            for (int i = 0; i < DataSource.GetFreeStationI(); i++)
            {
                if (DataSource.StationList[i].ChargeSlots > 0)
                    PrintStation(i);
            }
        }


    }
}




