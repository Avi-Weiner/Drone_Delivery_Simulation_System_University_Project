//Final Tag Commit
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace DAL.DalXML
{
    public partial class DalXML : DalApi.IDAL
    {
        //creates a DAL object by intializing values accordign to Initialize

        static DalXML ThisObject = null;
        static string dir = @"..\..\..\..\xmlData\";
        static string CustomersFilePath = @"Customers.xml";
        static string DronesFilePath = @"Drones.xml";
        static string PackagesFilePath = @"Packages.xml";
        static string StationsFilePath = @"Stations.xml";
        static string NextUniqueId = @"NextUniqueId.xml";

        /// <summary>
        /// Constructor for DalObject, initialises all entities
        /// </summary>
        private DalXML()
        {
            DalObject.DataSource.Initialize();//constructor for DalObjects
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            if (!File.Exists(dir + CustomersFilePath))
               XMLTools.SaveListToXMLSerializer<DO.Customer>(DalObject.DataSource.CustomerList, dir + CustomersFilePath);
            if (!File.Exists(dir + DronesFilePath))
                XMLTools.SaveListToXMLSerializer<DO.Drone>(DalObject.DataSource.DroneList, dir + DronesFilePath);
            if (!File.Exists(dir + PackagesFilePath))
                XMLTools.SaveListToXMLSerializer<DO.Package>(DalObject.DataSource.PackageList, dir + PackagesFilePath);
            if (!File.Exists(dir + StationsFilePath))
                XMLTools.SaveListToXMLSerializer<DO.Station>(DalObject.DataSource.StationList, dir + StationsFilePath);
            if (!File.Exists(dir + NextUniqueId))
                XMLTools.SaveIntToXMLSerializer(DalObject.DataSource.GetNextUniqueID(), dir + NextUniqueId);
        }

        /// <summary>
        /// if dal was already intialized the object already there will be returned 
        /// otherwise a new one will be created.
        /// </summary>
        /// <returns></returns>
        public static DalXML GetDalXML()
        {
            if (ThisObject == null)
            {
                ThisObject = new DalXML();
            }

            return ThisObject;

        }
        public static int GetNextUniqueId()
        {
            int Id = XMLTools.LoadIntFromXML(dir + NextUniqueId);
            XMLTools.SaveIntToXMLSerializer(Id + 1, dir + NextUniqueId);
            return Id;
        }

        /// <summary>
        /// Returns an array of the power consumptions { Free, Light, Medium, Heavy, ChargingRate }
        /// </summary>
        /// <returns></returns>
        public static double[] GetPowerConsumptions()
        {
            double[] array = new double[5];
            array[0] = DalObject.DataSource.GetFree();
            array[1] = DalObject.DataSource.GetLightWeight();
            array[2] = DalObject.DataSource.GetMediumWeight();
            array[3] = DalObject.DataSource.GetHeavyWeight();
            array[4] = DalObject.DataSource.GetChargingRate();

            return array;
        }
    }
}
