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
            Console.WriteLine("\n1. Adding options: ");
            Console.WriteLine("\n2. Updating options: ");
            Console.WriteLine("\n3. Drone Actions: ");
            Console.WriteLine("\n4. Display options: ");
            Console.WriteLine("\n5. List display options: ");
            Console.WriteLine("\n6. Exit");
        }
        /// <summary>
        /// prints the adding menu
        /// </summary>
        static void PrintAddingMenu()
        {
            Console.WriteLine("\n1. - adding base station to the stations list" +
                "\n2. - adding a drone to the existing drones list" +
                "\n3. - adding a new customer to the customers list" +
                "\n4. - adding a package to the package list");
        }
        /// <summary>
        /// prints the updating menu
        /// </summary>
        static void PrintUpdatingOptionsMenu()
        {
            Console.WriteLine("\n1. - Update drone: name only." +
                "\n2. - Update Station." + " i. Station name." +
                                        "\n\t\tii. Total NumberOf charging Stations." +
                "\n3. - Update Customer." + " i. New Name." +
                                      "\n\t\t ii. New phone");
        }

        /// <summary>
        /// prints the Drone Action menu
        /// </summary>
        static void PrintDroneActionsMenu()
        {
            Console.WriteLine("\n\t1. - Send a Drone to charge\n"
                + "\t2. - Release a drone from charge\n"
                + "\t3. - Assign a pakcage to a drone\n"
                + "\t4. - Collect a pakcage by a drone\n"
                + "\t5. - Deliver a package by a drone\n");
        }
        /// <summary>
        /// print the display menu 1-4 display specific field by id
        /// 5 another menu will be printed for options to print out a whole list
        /// </summary>
        static void PrintDisplayOptions()
        {
            Console.WriteLine("\n\t1. - Base Station by Id." +
                "\n\t2. - Drone by Id." +
                "\n\t3. - Package by Id." +
                "\n\t4. - Base Station by Id." +
                "\n\t5. - Display list.\n");
        }
        /// <summary>
        /// prints printing a list menu
        /// </summary>
        static void PrintListDisplayOptions()
        {
            Console.WriteLine("\n\t\t1. - Base Station list." +
                "\n\t\t2. - Drones list." +
                "\n\t\t3. - Customers list." +
                "\n\t\t4. - Packages list." +
                "\n\t\t5. - Unassigned Packages." +
                "\n\\t\t6. - Display base Stations with available charging slots.");
        }

        //static string PrintLocation(BL.BL Bl, IBL.BO.Location location)
        //{
        //    return
        //}

        /// <summary>
        /// Prints BL Base station from given id, need to use ToString!!!!!!!!!!!!!!!!!!!!!!!!!
        /// </summary>
        /// <param name="id"></param>
        static void PrintBaseStation(BL.BL Bl, int id)
        {
            IBL.BO.BaseStation b = Bl.DalToBLStation(id);
            Console.WriteLine("Base Station ID: " + b.Id +
                "\nName: " + b.Name +
                "\nLocation");
        }
    }
}
