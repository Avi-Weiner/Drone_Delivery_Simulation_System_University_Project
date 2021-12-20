using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI_BL
{
    partial class Program
    {
        /// <summary>
        /// prints the outer main menu
        /// </summary>
        static void PrintMainMenu()
        {
            Console.WriteLine("\n  Main Menu:   ");
            Console.WriteLine("1. Adding options: ");
            Console.WriteLine("2. Updating options: ");
            Console.WriteLine("3. Drone Actions: ");
            Console.WriteLine("4. Display options: ");
            Console.WriteLine("5. List display options: ");
            Console.WriteLine("6. Exit");
        }
        /// <summary>
        /// prints the adding menu
        /// </summary>
        static void PrintAddingMenu()
        {
            Console.WriteLine("\nAdding Menu: " +
                "\n1. - adding base station to the stations list" +
                "\n2. - adding a drone to the existing drones list" +
                "\n3. - adding a new customer to the customers list" +
                "\n4. - adding a package to the package list");
        }
        /// <summary>
        /// prints the updating menu
        /// </summary>
        static void PrintUpdatingOptionsMenu()
        {
            Console.WriteLine("\nUpdating Menu: " +
                "\n1. - Update drone: name only." +
                "\n2. - Update Station:" + " i. Station name." +
                "\n\t\tii. Total NumberOf charging Stations." +
                "\n3. - Update Customer:" + " i. New Name." +
                "\n\t\t ii. New phone");
        }

        /// <summary>
        /// prints the Drone Action menu
        /// </summary>
        static void PrintDroneActionsMenu()
        {
            Console.WriteLine("\nAction Menu: " +
                "\n\t1. - Send a Drone to charge\n"
                + "\t2. - Release a drone from charge\n"
                + "\t3. - Assign a pakcage to a drone\n"
                + "\t4. - Collect a pakcage by a drone\n"
                + "\t5. - Deliver a package by a drone\n");
        }

        /// <summary>
        /// print the display menu 1-4 display specific field by id
        /// 5 another menu will be printed for options to print out a whole list
        /// </summary>
        static void PrintDisplayMenu()
        {
            Console.WriteLine("\nDisplay Menu: " +
                "\n\t1. - Base Station by ID." +
                "\n\t2. - Drone by ID." +
                "\n\t3. - Package by ID" +
                "\n\t4. - Package by ID.");
        }
        /// <summary>
        /// prints printing a list menu
        /// </summary>
        static void PrintListDisplayMenu()
        {
            Console.WriteLine("\n\tList Display Menu: " +
                "\n\t1. - Base Station list." +
                "\n\t2. - Drones list." +
                "\n\t3. - Customers list." +
                "\n\t4. - Packages list." +
                "\n\t5. - Unassigned Packages." +
                "\n\t6. - Display base Stations with available charging slots.");
        }

        /// <summary>
        /// Prints list of stations
        /// </summary>
        /// <param name="Bl"></param>
        static void PrintStationList(BL.BL bl)
        {
            foreach(BL.BaseStationToList b in bl.ListOfStations())
            {
                Console.WriteLine(b.ToString() + '\n');
            }
        }

        /// <summary>
        /// Prints list of drones
        /// </summary>
        /// <param name="bl"></param>
        static void PrintDroneList(BL.BL bl)
        {
            foreach(BL.DroneToList d in BL.BL.BLObject.BLDroneList)
            {
                Console.WriteLine(d.ToString() + '\n');
            }
        }

        /// <summary>
        /// Prints list of customers
        /// </summary>
        /// <param name="bl"></param>
        static void PrintCustomerList(BL.BL bl)
        {
            foreach(BL.CustomerToList c in bl.ListOfCustomers())
            {
                Console.WriteLine(c.ToString() + '\n');
            }
        }

        /// <summary>
        /// Prints a list of packages
        /// </summary>
        /// <param name="bl"></param>
        static void PrintPackageList(BL.BL bl)
        {
            foreach(BL.PackageToList p in bl.ListOfPackages())
            {
                Console.WriteLine(p.ToString() + '\n');
            }
        }

        /// <summary>
        /// Prints list of unassigned packages
        /// </summary>
        /// <param name="bl"></param>
        static void PrintUnassignedPackages(BL.BL bl)
        {
            foreach(BL.PackageToList p in bl.ListOfUnassignedPackages())
            {
                Console.WriteLine(p.ToString() + '\n');
            }
        }

        /// <summary>
        /// prints list of stations with available charge slots
        /// </summary>
        /// <param name="bl"></param>
        static void PrintStationsWithAvailableChargeSlots(BL.BL bl)
        {
            foreach(BL.BaseStationToList b in bl.ListOfStationsWithChargeSlots())
            {
                Console.WriteLine(b.ToString() + '\n');
            }
        }
    }
}
