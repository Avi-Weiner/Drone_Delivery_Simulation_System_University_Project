using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        
        
        /// <summary>
        /// Returns a list of base stations
        /// </summary>
        /// <returns></returns>
        public List<IBL.BO.BaseStationToList> ListOfStations()
        {
            List<IBL.BO.BaseStationToList> list = new();
            foreach(IDAL.DO.Station station in DalObject.DataSource.StationList)
            {
                //converts DAL station to BL station to StationToList and adds to list
                list.Add(StationToStationToList(station.Id));
            }

            return list;
        }

        /// <summary>
        /// Returns a list of customers
        /// </summary>
        /// <returns></returns>
        public List<IBL.BO.CustomerToList> ListOfCustomers()
        {
            List<IBL.BO.CustomerToList> list = new();
            foreach (IDAL.DO.Customer c in DalObject.DataSource.CustomerList)
            {
                //converts DAL customer to BL Customer to CustomerToList and adds to list
                list.Add(CustomerToCustomerToList(c.Id));
            }

            return list;
        }

        /// <summary>
        /// Returns a list of packages
        /// </summary>
        /// <returns></returns>
        public List<IBL.BO.PackageToList> ListOfPackages()
        {
            List<IBL.BO.PackageToList> list = new();
            foreach(IDAL.DO.Package p in DalObject.DataSource.PackageList)
            {
                //converts DAP package to BL package to packageToList and adds to list
                list.Add(DalPackageToList(p.Id));
            }
            return list;
        }

        /// <summary>
        /// Returns a list of packages unassigned to drones
        /// </summary>
        /// <returns></returns>
        public List<IBL.BO.PackageToList> ListOfUnassignedPackages()
        {
            List<IBL.BO.PackageToList> list = new();
            foreach (IDAL.DO.Package p in DalObject.DataSource.PackageList)
            {
                //converts DAP package to BL package to packageToList and adds to list
                IBL.BO.PackageToList Plist = DalPackageToList(p.Id);
                if (Plist.PackageStatus != IBL.BO.PackageStatus.assigned)
                    list.Add(Plist);
            }
            return list;
        }

        /// <summary>
        /// Returns a list of base stations that have availabe charging stations
        /// </summary>
        /// <returns></returns>
        public List<IBL.BO.BaseStationToList> ListOfStationsWithChargeSlots()
        {
            List<IBL.BO.BaseStationToList> list = new();
            foreach (IDAL.DO.Station station in DalObject.DataSource.StationList)
            {
                if( DalToBlStation(station.Id).AvailableChargeSlots > 0)
                    list.Add(StationToStationToList(station.Id));
            }
            return list;
        }

        /// <summary>
        /// Returns a filtered DroneToList depending on the entered option:
        /// "all"
        /// States:
        ///     1: "free"
        ///     2: "maintenance" 
        ///     3: "delivery"
        /// Max Weight:
        ///     1: "light"
        ///     2: "medium"
        ///     3: "heavy"
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public List<IBL.BO.DroneToList> DroneListFilter(string option)
        {
            Predicate<IBL.BO.DroneToList> predicate;

            switch (option)
            {
                case "All Drones":
                    predicate = d => true;
                    break;
                case "Free Drones":
                    predicate = d => d.DroneStatus == IBL.BO.DroneStatus.free;
                    break;
                case "Maintenance Drones":
                    predicate = d => d.DroneStatus == IBL.BO.DroneStatus.maintenance;
                    break;
                case "Delivery Drones":
                    predicate = d => d.DroneStatus == IBL.BO.DroneStatus.delivery;
                    break;
                case "Light":
                    predicate = d => d.Weight == IDAL.DO.WeightCategory.light;
                    break;
                case "Medium":
                    predicate = d => d.Weight == IDAL.DO.WeightCategory.medium; 
                    break;
                case "Heavy":
                    predicate = d => d.Weight == IDAL.DO.WeightCategory.heavy; 
                    break;
                default:
                    throw new IBL.BO.MessageException("Error: Invalid drone list filter option entered.");
            }

            return BLObject.BLDroneList.FindAll(predicate);
        }


    }
}
