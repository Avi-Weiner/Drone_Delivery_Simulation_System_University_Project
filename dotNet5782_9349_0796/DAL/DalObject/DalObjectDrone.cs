using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public partial class DalObject : IDAL.IDALInterface
    {
        /// <summary>
        /// Receives Drone ID and returns its index in DroneList
        /// </summary>
        public static int GetDrone(int DroneId)
        {
            int i = 0;
            while (i < DataSource.GetFreeDroneI() && DataSource.DroneList[i].Id != DroneId) //Cycle through StationList until StationId is found
                i++;
            if (DataSource.DroneList[i].Id != DroneId)
                throw new IDAL.DO.MessageException("Error: Drone not found.");
            return i;
        }

        /// <summary>
        /// adding a drone to the existing drones list
        /// </summary>
        public static void AddDrone()
        {
            try
            {
                Console.WriteLine("Enter drone model:");
                string model = Console.ReadLine();
                Console.WriteLine("Enter weight category as string: option are light, medium and heavy:");
                string weightString = Console.ReadLine();
                //Check if valid input
                if (weightString != "light" && weightString != "medium" && weightString != "heavy")
                    throw new IDAL.DO.MessageException("Error: Invalid weight.");

                IDAL.DO.WeightCategory ThisWeight = (IDAL.DO.WeightCategory)Enum.Parse(typeof(IDAL.DO.WeightCategory), weightString);

                //add the new drone to the back of the list
                DataSource.DroneList.Add(new IDAL.DO.Drone
                {
                    Id = DataSource.GetNextUniqueID(),
                    Model = model,
                    MaxWeight = ThisWeight
                });
            }
            catch (IDAL.DO.MessageException e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// Receives Drone with ID DroneId and picks up package with ID PackageId
        /// </summary>
        /// <param name="PackageId"></param>
        /// <param name="DroneId"></param>
        public static void DronePickUp(int PackageId, int DroneId)
        {
            try
            {
                IDAL.DO.Package P = DataSource.PackageList.Find(x => x.Id == PackageId);
                IDAL.DO.Drone D = DataSource.DroneList.Find(x => x.Id == DroneId);
                //if (D == null)
                //Do we need to check if the drone is free?
                if (P.Id == PackageId && D.Id == DroneId)
                {
                    P.PickedUp = DateTime.Now;
                    P.DroneId = DroneId;
                }
            }
            catch (IDAL.DO.MessageException e)
            {
                Console.WriteLine(e);
            }

        }

        /// <summary>
        /// Sends a drone to get charged at the specified station
        /// </summary>
        /// <param name="DroneId"></param>
        /// <param name="StationId"></param>
        public static void ChargeDrone(int DroneId, int StationId)
        {
            try
            {
                //Get Drone
                int i = GetDrone(DroneId);

                //Get station
                IDAL.DO.Station S = DataSource.StationList.Find(x => x.Id == StationId);
                IDAL.DO.Drone D = DataSource.DroneList.Find(x => x.Id == DroneId);
                if (S.ChargeSlots == 0)
                    throw new IDAL.DO.MessageException("Error: No free charge slots at specified station.");

                //minus 1 to charge slots
                S.ChargeSlots--;
                int j = GetStation(StationId);
                DataSource.StationList[j] = S;

                //Adding instance of Dronecharger (Need to save this somewhere or it will just get deleted...),
                //specs unspecific of where to save it so for the meantime it will be deleted
                IDAL.DO.DroneCharger newCharger = new IDAL.DO.DroneCharger();
                newCharger.DroneId = D.Id;
                newCharger.StationId = S.Id;
            }
            catch (IDAL.DO.MessageException e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        /// Releases Drone with ID DroneID from station with ID StationID
        /// </summary>
        /// <param name="DroneID"></param>
        /// <param name="StationID"></param>
        public static void ReleaseDrone(int DroneId, int StationId)
        {
            try
            {
                //Get Drone
                int i = GetDrone(DroneId);

                //Get station
                int j = GetStation(StationId);

                //Free up charge slot 
                IDAL.DO.Station s = DataSource.StationList.Find(x => x.Id == StationId);
                s.ChargeSlots++;
                DataSource.StationList[j] = s;
            }
            catch (IDAL.DO.MessageException e)
            {
                Console.WriteLine(e);
            }

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
            try
            {
                int p = GetDrone(Id);
                PrintDrone(p);
            }
            catch (IDAL.DO.MessageException e)
            {
                Console.WriteLine(e);
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
    }
}
