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
        /// Receives Package Id and returns its index in packageList
        /// </summary>
        /// <param name="PackageId"></param>
        /// <returns></returns>
        public static int GetPackage(int PackageId)
        {
            int i = 0;
            while (i < DataSource.GetFreePackageI() && DataSource.PackageList[i].Id != PackageId) //Cycle through PackageList until Package is found
                i++;

            if (DataSource.PackageList[i].Id != PackageId)
                throw new IDAL.DO.MessageException("Error: Package not found.");
            return i;
        }

        /// <summary>
        /// receiving a package to deliver
        /// </summary>
        public static void AddPackage()
        {
            try
            {
                Console.WriteLine("Enter Sender ID: ");
                int InputSender = Convert.ToInt32(Console.ReadLine()); //Does not check if Customer exists as Exercise 1 says not to
                Console.WriteLine("Enter Receiver ID: ");
                int InputReciever = Convert.ToInt32(Console.ReadLine());//Does not check if Customer exists as Exercise 1 says not to
                Console.WriteLine("Enter Weight Catagory ('light', 'medium', 'heavy'): ");
                string weightString = Console.ReadLine();
                //Check if valid input
                if (weightString != "light" || weightString != "medium" || weightString != "heavy")
                    throw new IDAL.DO.MessageException("Error: Invalid weight.");

                IDAL.DO.WeightCategory InputWeight = (IDAL.DO.WeightCategory)Enum.Parse(typeof(IDAL.DO.WeightCategory), weightString);

                Console.WriteLine("Enter Priority ('regular', 'fast', 'emergency'): ");
                string priorityString = Console.ReadLine();
                //Check if valid input
                if (priorityString != "regular" || priorityString != "fast" || priorityString != "emergency")
                    throw new IDAL.DO.MessageException("Error: Invalid priority");

                IDAL.DO.Priority InputPriority = (IDAL.DO.Priority)Enum.Parse(typeof(IDAL.DO.Priority), priorityString);
                DataSource.PackageList.Add(new IDAL.DO.Package
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
            catch (IDAL.DO.MessageException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine("Unknown Error");
            }

        }

        /// <summary>
        /// Assigning a drone to a package
        /// </summary>
        /// <param name="PackageId"></param>
        /// <param name="DroneId"></param>
        public static void AssignDroneToPackage(int PackageId, int DroneId)
        {
            try
            {
                //Checks if drone Id is valid
                GetDrone(DroneId);
                IDAL.DO.Package P = DataSource.PackageList.Find(x => x.Id == PackageId);

                P.DroneId = DroneId;
                P.Scheduled = DateTime.Now;
            }
            catch (IDAL.DO.MessageException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine("Unknown Error");
            }
        }

        /// <summary>
        /// Receives a package from Id PackageId and "drops it off" with a customer
        /// </summary>
        /// <param name="PackageId"></param>
        public static void PackageDropOff(int PackageId)
        {
            try
            {
                //hopefully will catch if packageId is not found
                IDAL.DO.Package P = DataSource.PackageList.Find(x => x.Id == PackageId);
                P.Delivered = DateTime.Now;

                //Possible place to free up drone:
                //DataSource.PackageList[p].Delivered = DateTime.Now;
                //int DroneId = DataSource.PackageList[p].DroneId;
                //int i = GetDrone(DroneId);
            }
            catch (IDAL.DO.MessageException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine("Unknown Error");
            }
        }

        /// <summary>
        /// Prints a package given its' index in PackageList
        /// </summary>
        /// <param name="i"></param>
        public static void PrintPackage(int i)
        {
            Console.WriteLine("\nPackage ID: " + DataSource.PackageList[i].Id
                + "\nSender ID: " + DataSource.PackageList[i].SenderId
                + "\nReceiver ID: " + DataSource.PackageList[i].ReceiverId
                + "\nWeight: " + DataSource.PackageList[i].Weight.ToString()
                + "\nPriority: " + DataSource.PackageList[i].Priority.ToString()
                + "\nRequested Date: " + DataSource.PackageList[i].Requested.ToString()
                + "\nDrone ID: " + DataSource.PackageList[i].DroneId
                + "\nScheduled: " + DataSource.PackageList[i].Scheduled.ToString()
                + "\nPick up Date: " + DataSource.PackageList[i].PickedUp.ToString()
                + "\nDelivery Date: " + DataSource.PackageList[i].Delivered.ToString());
        }

        /// <summary>
        /// Displays the package info of the given package id.
        /// </summary>
        /// <param name="Id"></param>
        public static void DisplayPackage(int Id)
        {
            try
            {
                int i = GetPackage(Id);
                PrintPackage(i);
            }
            catch (IDAL.DO.MessageException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Console.WriteLine("Unknown Error");
            }
        }

        /// <summary>
        /// Displays all the Packages in PackageList
        /// </summary>
        public static void DisplayPackageList()
        {
            for (int i = 0; i < DataSource.GetFreePackageI(); i++)
                PrintPackage(i);
        }

        /// <summary>
        /// Displays packages that are not yet assigned to a drone
        /// </summary>
        public static void DisplayUnassignedPackages()
        {
            for (int i = 0; i < DataSource.GetFreePackageI(); i++)
            {
                if (DataSource.PackageList[i].DroneId == 0)
                    PrintPackage(i);
            }
        }
    }
}