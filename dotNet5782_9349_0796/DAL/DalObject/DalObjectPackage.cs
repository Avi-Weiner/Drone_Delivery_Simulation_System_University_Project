using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public partial class DalObject : IDAL.IDAL
    {
        /// <summary>
        /// Receives Package Id and returns its index in packageList
        /// </summary>
        /// <param name="PackageId"></param>
        /// <returns></returns>
        public static int GetPackage(int PackageId)
        {
            int i = DataSource.CustomerList.FindIndex(x => x.Id == PackageId);

            if (DataSource.PackageList[i].Id != PackageId)
                throw new IDAL.DO.MessageException("Error: Package not found.");
            return i;
        }

        /// <summary>
        /// receiving a package to deliver
        /// </summary>
        public static void AddPackage(int InputSender, int InputReceiver, IDAL.DO.WeightCategory InputWeight)
        {
            try
            {
                //Console.WriteLine("Enter Sender ID: ");
                //int InputSender = Convert.ToInt32(Console.ReadLine()); //Does not check if Customer exists as Exercise 1 says not to
                //Console.WriteLine("Enter Receiver ID: ");
                //int InputReciever = Convert.ToInt32(Console.ReadLine());//Does not check if Customer exists as Exercise 1 says not to
                //Console.WriteLine("Enter Weight Catagory ('light', 'medium', 'heavy'): ");
                //string weightString = Console.ReadLine();
                ////Check if valid input
                //if (weightString != "light" && weightString != "medium" && weightString != "heavy")
                //    throw new IDAL.DO.MessageException("Error: Invalid weight.");

                //IDAL.DO.WeightCategory InputWeight = (IDAL.DO.WeightCategory)Enum.Parse(typeof(IDAL.DO.WeightCategory), weightString);

                Console.WriteLine("Enter Priority ('regular', 'fast', 'emergency'): ");
                string priorityString = Console.ReadLine();
                //Check if valid input
                if (priorityString != "regular" && priorityString != "fast" && priorityString != "emergency")
                    throw new IDAL.DO.MessageException("Error: Invalid priority");

                IDAL.DO.Priority InputPriority = (IDAL.DO.Priority)Enum.Parse(typeof(IDAL.DO.Priority), priorityString);
                DataSource.PackageList.Add(new IDAL.DO.Package
                {
                    Id = DataSource.GetNextUniqueID(),
                    SenderId = InputSender,
                    ReceiverId = InputReceiver,
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

        }

        /// <summary>
        /// Assigning a drone to a package
        /// </summary>
        /// <param name="PackageId"></param>
        /// <param name="DroneId"></param>
        public static void AssignDroneToPackage(int PackageId, int DroneId)
        {
            //try
            //{
                //Checks if drone Id is valid
                GetDrone(DroneId);
                IDAL.DO.Package P = DataSource.PackageList.Find(x => x.Id == PackageId);

                P.DroneId = DroneId;
                P.Scheduled = DateTime.Now;
            //}
            //catch (IDAL.DO.MessageException e)
            //{
            //    Console.WriteLine(e);
            //}
        }

        /// <summary>
        /// Receives a package from Id PackageId and "drops it off" with a customer
        /// </summary>
        /// <param name="PackageId"></param>
        public static void PackageDropOff(int PackageId)
        {
            //try
            //{
                //hopefully will catch if packageId is not found
                IDAL.DO.Package P = DataSource.PackageList.Find(x => x.Id == PackageId);
                P.Delivered = DateTime.Now;

                //Possible place to free up drone:
                //DataSource.PackageList[p].Delivered = DateTime.Now;
                //int DroneId = DataSource.PackageList[p].DroneId;
                //int i = GetDrone(DroneId);
            //}
            //catch (IDAL.DO.MessageException e)
            //{
            //    Console.WriteLine(e);
            //}

        }
        //-------------------------------------------------------------Below this will be put in the main-------------------
        ///// <summary>
        ///// Prints a package given its' index in PackageList
        ///// </summary>
        ///// <param name="i"></param>
        //public static void PrintPackage(IDAL.DO.Package p)
        //{
        //    Console.WriteLine("\nPackage ID: " + p.Id
        //        + "\nSender ID: " + p.SenderId
        //        + "\nReceiver ID: " + p.ReceiverId
        //        + "\nWeight: " + p.ToString()
        //        + "\nPriority: " + p.Priority.ToString()
        //        + "\nRequested Date: " + p.Requested.ToString()
        //        + "\nDrone ID: " + p.DroneId
        //        + "\nScheduled: " + p.Scheduled.ToString()
        //        + "\nPick up Date: " + p.PickedUp.ToString()
        //        + "\nDelivery Date: " + p.Delivered.ToString());
        //}

        ///// <summary>
        ///// Displays the package info of the given package id.
        ///// </summary>
        ///// <param name="Id"></param>
        //public static void DisplayPackage(int Id)
        //{
        //    try
        //    {
        //        IDAL.DO.Package p = DataSource.PackageList.Find(x => x.Id == Id);
        //        PrintPackage(p);
        //    }
        //    catch (IDAL.DO.MessageException e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //}

        ///// <summary>
        ///// Displays all the Packages in PackageList
        ///// </summary>
        //public static void DisplayPackageList()
        //{
        //    foreach (IDAL.DO.Package p in DataSource.PackageList)
        //    {
        //        PrintPackage(p);
        //    }
        //}

        ///// <summary>
        ///// Displays packages that are not yet assigned to a drone
        ///// </summary>
        //public static void DisplayUnassignedPackages()
        //{
        //    foreach (IDAL.DO.Package p in DataSource.PackageList)
        //    {
        //        if (p.DroneId == 0)
        //            PrintPackage(p);
        //    }
        //}
    }
}