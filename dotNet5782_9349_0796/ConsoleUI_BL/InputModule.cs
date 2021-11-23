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
            Console.WriteLine("Enter: (Name, Longitude, Latitude, Available Slots)\n");
            name = Convert.ToInt32(Console.ReadLine());
            longitude = Convert.ToDouble(Console.ReadLine());
            latitude = Convert.ToDouble(Console.ReadLine());
            availableSlots = Convert.ToInt32(Console.ReadLine());

            Bl.AddBaseStation(name, longitude, latitude, availableSlots);
        }
        /// <summary>
        /// adds a drone
        /// </summary>
        /// <param name="Bl"></param>
        static void AddDrone(BL.BL Bl)
        {
            Console.WriteLine("Enter Model:\n");
            string Model = Console.ReadLine();
            Console.WriteLine("Enter WeightCategory(string):\n");
            IDAL.DO.WeightCategory Weight = (IDAL.DO.WeightCategory)Enum.Parse(typeof(IDAL.DO.WeightCategory), Console.ReadLine());
            Console.WriteLine("Enter StationId(For Charging):\n");
            int stationId = Convert.ToInt32(Console.ReadLine());
            Bl.AddDrone(Model, Weight, stationId);
        }
        /// <summary>
        /// adds a new customer
        /// </summary>
        /// <param name="Bl"></param>
        static void AddNewCustomer(BL.BL Bl)
        {
            Console.WriteLine("Enter CustomerId:\n");
            int CustomerId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(" Enter Name:\n");
            string name = Console.ReadLine();
            Console.WriteLine(" Enter Phone:\n");
            string phone = Console.ReadLine();
            Console.WriteLine(" Enter Longitude:\n");
            double Longitude = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine(" Enter Latitude:\n");
            double Latitude = Convert.ToDouble(Console.ReadLine());
            Bl.AddCustomer(CustomerId, name, phone, Longitude, Latitude);
        }
        /// <summary>
        /// Adds a new package
        /// </summary>
        /// <param name="Bl"></param>
        static void AddAPakcage(BL.BL Bl)
        {
            Console.WriteLine("Enter SenderId:\n");
            int SenderId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter ReceiverId:\n");
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
            Console.WriteLine("Enter Drone Id:\n");
            int Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter new Model:\n");
            string Model = Console.ReadLine();
            Bl.UpdateDrone(Id, Model);
        }

        /// <summary>
        /// Gives the user input for updating the station and calls BL UpdateStation
        /// </summary>
        /// <param name="Bl"></param>
        static void UpdateStation(BL.BL Bl)
        {
            Console.WriteLine("Enter Station ID: ");
            int Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter new station name or -1 to not change: ");
            int stationName = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter new charge station amount or -1 to not change: ");
            int chargeStations = Convert.ToInt32(Console.ReadLine());
            Bl.UpdateStation(Id, stationName, chargeStations);
        }

        static void UpdateCustomer(BL.BL Bl)
        {
            Console.WriteLine("Enter Customer ID: ");
            int Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter new customer name or -1 to not change: ");
            string Name = Console.ReadLine();
            Console.WriteLine("Enter new phone number amount or -1 to not change: ");
            string phone = Console.ReadLine();
            Bl.UpdateCustomer(Id, Name, phone);
        }
    }
}