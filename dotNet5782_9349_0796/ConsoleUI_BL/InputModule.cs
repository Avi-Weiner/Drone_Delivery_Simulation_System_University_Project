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
        /// Adds a base station
        /// </summary>
        /// <param name="Bl"></param>
        static void AddBaseStation(BL.BL Bl)
        {
            int name; double longitude, latitude; int availableSlots;
            Console.WriteLine("\nEnter Name (Number):");
            name = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nEnter Longitude (Decimal): ");
            longitude = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("\nEnter Latitude (Decimal): ");
            latitude = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("\nEnter Available Slots (Number): ");
            availableSlots = Convert.ToInt32(Console.ReadLine());

            Bl.AddBaseStation(name, longitude, latitude, availableSlots);
        }

        /// <summary>
        /// adds a drone
        /// </summary>
        /// <param name="Bl"></param>
        static void AddDrone(BL.BL Bl)
        {
            Console.WriteLine("\nEnter Model (Characters): ");
            string Model = Console.ReadLine();
            Console.WriteLine("\nEnter Weight Category(case sensitive: light, medium or heavy): ");
            string Weight = Console.ReadLine();
            Console.WriteLine("\nEnter StationId that the drone is charging at (Number): ");
            int stationId = Convert.ToInt32(Console.ReadLine());
            Bl.AddDrone(Model, Weight, stationId);
        }

        /// <summary>
        /// adds a new customer
        /// </summary>
        /// <param name="Bl"></param>
        static void AddNewCustomer(BL.BL Bl)
        {
            Console.WriteLine("\nEnter Name (Characters): ");
            string name = Console.ReadLine();
            Console.WriteLine("\nEnter Phone (Characters): ");
            string phone = Console.ReadLine();
            Console.WriteLine("\nEnter Longitude (Decimal): ");
            double Longitude = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("\nEnter Latitude (Decimal): ");
            double Latitude = Convert.ToDouble(Console.ReadLine());
            Bl.AddCustomer(name, phone, Longitude, Latitude);
        }

        /// <summary>
        /// Adds a new package
        /// </summary>
        /// <param name="Bl"></param>
        static void AddAPakcage(BL.BL Bl)
        {
            Console.WriteLine("\nEnter SenderId (Number): ");
            int SenderId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nEnter ReceiverId (Number): ");
            int RecieiverId = Convert.ToInt32(Console.ReadLine());
            string Weight = Console.ReadLine();
            string Priority = Console.ReadLine();
            Bl.AddPackage(SenderId, RecieiverId, Weight, Priority);
        }

        /// <summary>
        /// update a drone
        /// Model could be updated
        /// </summary>
        /// <param name="Bl"></param>
        static void UpdateDrone(BL.BL Bl)
        {
            Console.WriteLine("\nEnter Drone Id (Number): ");
            int Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nEnter new Model (Characters): ");
            string Model = Console.ReadLine();
            Bl.UpdateDrone(Id, Model);
        }

        /// <summary>
        /// Gives the user input for updating the station and calls BL UpdateStation
        /// </summary>
        /// <param name="Bl"></param>
        static void UpdateStation(BL.BL Bl)
        {
            Console.WriteLine("\nEnter Station ID (Number): ");
            int Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nEnter new station name (Number) or -1 to not change: ");
            int stationName = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nEnter new charge station amount (Number) or -1 to not change: ");
            int chargeStations = Convert.ToInt32(Console.ReadLine());
            Bl.UpdateStation(Id, stationName, chargeStations);
        }

        static void UpdateCustomer(BL.BL Bl)
        {
            Console.WriteLine("\nEnter Customer ID (Number): ");
            int Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nEnter new customer name (Characters) or -1 to not change: ");
            string Name = Console.ReadLine();
            Console.WriteLine("\nEnter new phone number (Characters) or -1 to not change: ");
            string phone = Console.ReadLine();
            Bl.UpdateCustomer(Id, Name, phone);
        }
    }
}