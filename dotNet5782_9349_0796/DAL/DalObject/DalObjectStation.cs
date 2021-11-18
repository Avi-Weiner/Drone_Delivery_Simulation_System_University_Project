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
        /// Receives Station Id and returns index of station in StationList
        /// </summary>
        public static int GetStation(int StationId)
        {
            int i = DataSource.CustomerList.FindIndex(x => x.Id == StationId);

            if (DataSource.StationList[i].Id != StationId)
                throw new IDAL.DO.MessageException("Error: Station not found.");
            return i;
        }

        /// <summary>
        /// adding base station to the stations list
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="slots"></param>
        public static void AddStation(double longitude, double latitude, int slots)
        {
            //Console.WriteLine("Enter Longitude: ");
            //double longitude = Convert.ToDouble(Console.ReadLine());
            //Console.WriteLine("Enter Latitude: ");
            //double latitude = Convert.ToDouble(Console.ReadLine());
            //Console.WriteLine("Enter number of charge slots: ");
            //int slots = Convert.ToInt32(Console.ReadLine());

            //assuming that no maximum amount of sattions
            //add station to the back of the station list
            DataSource.StationList.Add(new IDAL.DO.Station
            {
                Id = DataSource.GetNextUniqueID(),
                Longitude = longitude,
                Latitude = latitude,
                ChargeSlots = slots
            });
        }
        ////-------------------------------------------Below this is printing which will be put in the main-------------------
        ///// <summary>
        ///// Prints Station from received station s
        ///// </summary>
        ///// <param name="i"></param>
        //public static void PrintStation(IDAL.DO.Station s)
        //{
        //    Console.WriteLine("\nBase Station ID: " + s.Id
        //       + "\nBase Station  name: " + s.Name
        //       + "\nBase Station Longitude: " + s.Longitude
        //       + "\nBase Station Latitude: " + s.Latitude
        //       + "\nBase Station # of Charging slots: " + s.ChargeSlots);
        //}

        ///// <summary>
        ///// Prints the base station from the given ID
        ///// </summary>
        ///// <param name="Id"></param>
        //public static void DisplayBaseStation(int Id)
        //{
        //    try
        //    {
        //        IDAL.DO.Station s = DataSource.StationList.Find(x => x.Id == Id);
        //        PrintStation(s);
        //    }
        //    catch (IDAL.DO.MessageException e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //}

        ///// <summary>
        ///// Displays all the stations in StationList.
        ///// </summary>
        //public static void DisplayStationList()
        //{
        //    foreach (IDAL.DO.Station s in DataSource.StationList)
        //    {
        //        PrintStation(s);
        //    }

        //}

        ///// <summary>
        ///// Displays Stations with unoccupied charging stations
        ///// </summary>
        //public static void DisplayFreeChargingStations()
        //{
        //    foreach (IDAL.DO.Station s in DataSource.StationList)
        //    {
        //        if (s.ChargeSlots > 0)
        //            PrintStation(s);
        //    }
        //}
    }
}
