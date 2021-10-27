using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace DalObject
    {
        public class DalObjectClass
        {
            //creates a DAL object by intializing values accordign to Initialize
            DalObjectClass()
            {
            DataSource.Initialize();//constructor for DalObjects
            }
            //I think the following methods need to be added.
            //Adding to lists methods

            /// <summary>
            /// Receives Station Id and returns index of station in StationList
            /// </summary>
            public int GetStation(int StationId)
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
            public int GetDrone(int DroneId)
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
            public int GetPackage(int PackageId)
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

        //adding base station to the stations list
        public void AddStation()
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

            //adding a drone to the existing drones list
            public void AddDrone()
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


            //adding a new customer to the customers list
            public void AddCustomer()
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


            //receiving a package to deliver
            public void AddPackage()
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


            //Updating existing data
            //assigning a package to a drone" +
            public void AssignDroneToPackage(int PackageId, int DroneId)
            {   
                //Checks if drone Id is valid
                GetDrone(DroneId);

                int j = GetPackage(PackageId);
                
                //Asign Drone to package
                DataSource.ParcelList[j].DroneId = DroneId;
                DataSource.ParcelList[j].Scheduled = DateTime.Now;
            }


            //        "\n - collecting a package by a drone" +
            public void DronePickUp()
            {

            }

            //        "\n - providing a package to a customer" +
            public void PackageDropOff()
            {

            }
            
            public void ChargeDrone(int DroneId, int StationId)
            {
                //Get Drone
                int i = GetDrone(DroneId);

                //Get station
                int j = GetStation(StationId);

                //Make the battery full
                DataSource.DroneList[i].battery = 1;
                DataSource.DroneList[i].Status = IDAL.DO.DroneStatus.maintenance;

                //Adding instance of Dronecharger
                IDAL.DO.DroneCharger newCharger = new IDAL.DO.DroneCharger();
                newCharger.DroneId = DataSource.DroneList[i].Id;
                newCharger.StationId = DataSource.StationList[j].Id;
            }

            

            //        "\n - sending a drone to a charge in a base station" +
            //        "\n   - by changing the drone’s status and adding a record(instance) of" +
            //        "\n     a drone battery charger entity" +

            //        "\n   - the station is selected by the user in the main menu(It is" +
            //        "\n     recommended to provide a list of stations to the user)" +
            //        "\n - releasing a drone from charging in a base station");
            //public void ChargeDrone(IDAL.DO.Drone drone, IDAL.DO.DroneCharge)
            //{

            //}






        }
    }


