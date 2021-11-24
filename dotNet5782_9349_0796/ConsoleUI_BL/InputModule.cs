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
            Console.WriteLine("\nEnter Name:");
            name = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nEnter Longitude: ");
            longitude = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("\nEnter: Latitude: ");
            latitude = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("\nEnter Available Slots: ");
            availableSlots = Convert.ToInt32(Console.ReadLine());

            Bl.AddBaseStation(name, longitude, latitude, availableSlots);
        }
        /// <summary>
        /// adds a drone
        /// </summary>
        /// <param name="Bl"></param>
        static void AddDrone(BL.BL Bl)
        {
            Console.WriteLine("\nEnter Model: ");
            string Model = Console.ReadLine();
            Console.WriteLine("\nEnter WeightCategory(string): ");
            IDAL.DO.WeightCategory Weight = (IDAL.DO.WeightCategory)Enum.Parse(typeof(IDAL.DO.WeightCategory), Console.ReadLine());
            Console.WriteLine("\nEnter StationId(For Charging): ");
            int stationId = Convert.ToInt32(Console.ReadLine());
            Bl.AddDrone(Model, Weight, stationId);
        }
        /// <summary>
        /// adds a new customer
        /// </summary>
        /// <param name="Bl"></param>
        static void AddNewCustomer(BL.BL Bl)
        {
            Console.WriteLine("\nEnter Name: ");
            string name = Console.ReadLine();
            Console.WriteLine("\nEnter Phone: ");
            string phone = Console.ReadLine();
            Console.WriteLine("\nEnter Longitude: ");
            double Longitude = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("\nEnter Latitude: ");
            double Latitude = Convert.ToDouble(Console.ReadLine());
            Bl.AddCustomer(name, phone, Longitude, Latitude);
        }
        /// <summary>
        /// Adds a new package
        /// </summary>
        /// <param name="Bl"></param>
        static void AddAPakcage(BL.BL Bl)
        {
            Console.WriteLine("\nEnter SenderId: ");
            int SenderId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nEnter ReceiverId: ");
            int RecieiverId = Convert.ToInt32(Console.ReadLine());
            IDAL.DO.WeightCategory Weight = (IDAL.DO.WeightCategory)Enum.Parse(typeof(IDAL.DO.WeightCategory), Console.ReadLine());
            IDAL.DO.Priority Priority = (IDAL.DO.Priority)Enum.Parse(typeof(IDAL.DO.Priority), Console.ReadLine());
            Bl.AddPackage(SenderId, RecieiverId, Weight, Priority);
        }
        /// <summary>
        /// update a drone
        /// Model could be updated
        /// </summary>
        /// <param name="Bl"></param>
        static void UpdateDrone(BL.BL Bl)
        {
            Console.WriteLine("\nEnter Drone Id: ");
            int Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nEnter new Model: ");
            string Model = Console.ReadLine();
            Bl.UpdateDrone(Id, Model);
        }

        /// <summary>
        /// Gives the user input for updating the station and calls BL UpdateStation
        /// </summary>
        /// <param name="Bl"></param>
        static void UpdateStation(BL.BL Bl)
        {
            Console.WriteLine("\nEnter Station ID: ");
            int Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nEnter new station name or -1 to not change: ");
            int stationName = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nEnter new charge station amount or -1 to not change: ");
            int chargeStations = Convert.ToInt32(Console.ReadLine());
            Bl.UpdateStation(Id, stationName, chargeStations);
        }

        static void UpdateCustomer(BL.BL Bl)
        {
            Console.WriteLine("\nEnter Customer ID: ");
            int Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nEnter new customer name or -1 to not change: ");
            string Name = Console.ReadLine();
            Console.WriteLine("\nEnter new phone number amount or -1 to not change: ");
            string phone = Console.ReadLine();
            Bl.UpdateCustomer(Id, Name, phone);
        }
    }
}