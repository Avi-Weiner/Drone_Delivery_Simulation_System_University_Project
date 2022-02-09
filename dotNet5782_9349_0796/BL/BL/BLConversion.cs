using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace BL
{
    public partial class BL : BlApi.IBL
    {
        /// <summary>
        /// Returns BL Drone from given id, converting it from the DroneToList list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Drone DroneToListToDrone(int id)
        {
            Drone d = new();
            DroneToList DroneToList = BLObject.BLDroneList.Find(x => x.Id == id);
            if (DroneToList == null)
                throw new MessageException("Error: Object of id " + id + " not found.");

            d.Id = DroneToList.Id;
            d.Model = DroneToList.Model;
            d.Weight = DroneToList.Weight;
            d.BatteryStatus = DroneToList.BatteryStatus;
            d.Status = DroneToList.DroneStatus;
            d.Location = DroneToList.Location;

            DO.Package p = DalObject.DataSource.PackageList.Find(x => x.Id == DroneToList.PackageId);
            // need to do a package conversion
            if (p.Id != default(DO.Package).Id)
                d.PackageInTransfer = DalToBlPackage((int)DroneToList.PackageId);
            else
            {
                d.PackageInTransfer = null;
            }

            return d;
        }

        /// <summary>
        /// Returns a BL package from the DalPackage from the given id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Package DalToBlPackage(int id)
        {

            Package p = new();
            List<DO.Package> ListOfPackages = BLObject.Dal.GetPackageList();
            int IndexOfPackage = ListOfPackages.FindIndex(x => x.Id == id);
            DO.Package DalP = ListOfPackages[IndexOfPackage];
            

            if (DalP.Id != id)
                throw new MessageException("Error: Object of id " + id + " not found.");

            p.Id = DalP.Id;
            //Get the senders and receivers ID's from customerList and convert to BL Customers
            p.Sender = DalToBlCustomer(BLObject.Dal.GetCustomerList().Find(x => x.Id == DalP.SenderId).Id);
            p.Receiver = DalToBlCustomer(BLObject.Dal.GetCustomerList().Find(x => x.Id == DalP.ReceiverId).Id);
            p.Weight = DalP.Weight;
            p.Priority = DalP.Priority;
            p.DroneId = DalP.DroneId;
            p.CreationTime = DalP.Requested;
            p.AssigningTime = DalP.Scheduled;
            p.CollectingTime = DalP.PickedUp;
            p.DeliveringTime = DalP.Delivered;
            return p;
        }

        /// <summary>
        /// Receives the nullable DateTimes of scheduled, pickedUp and Delivered and 
        /// returns the packagage status of the corresponding item.
        /// </summary>
        /// <param name="scheduled"></param>
        /// <param name="PickedUp"></param>
        /// <param name="Delivered"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public PackageStatus TimesToStatus(DateTime? scheduled, DateTime? PickedUp, DateTime? Delivered)
        {
            if (scheduled == null)
                return PackageStatus.created;
            else if (scheduled != null && PickedUp == null)
                return PackageStatus.assigned;
            else if (PickedUp != null && Delivered == null)
                return PackageStatus.collected;
            else if (Delivered != null)
                return PackageStatus.delivered;
            else 
                throw new MessageException("Error: TimesToStatus() failed");
        }

        /// <summary>
        /// Receives the ID of a DAl package and returns the corresponding PackageToList item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public PackageToList DalPackageToList(int id)
        {
            PackageToList PList = new();
            DO.Package p = BLObject.Dal.GetPackageList().Find(x => x.Id == id);

            // Not sure how to implement the following
            if (p.Id == default(DO.Package).Id)
                throw new MessageException("Error: Object of id" + id + " not found.");
            

            PList.Id = p.Id;
            PList.SenderId = p.SenderId;
            PList.ReceiverId = p.ReceiverId;
            PList.Weight = p.Weight;
            PList.Priority = p.Priority;
            PList.PackageStatus = TimesToStatus(p.Scheduled, p.PickedUp, p.Delivered);
            return PList;
        }

        /// <summary>
        /// Receives a customer ID and gets the DAl customer and coverts it to a BL Customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer DalToBlCustomer(int id)
        {
            
            Customer c = new();
            
            DO.Customer DalC = BLObject.Dal.GetCustomerList().Find(x => x.Id == id);

            if (DalC.Id == 0)
                throw new MessageException("Error: Object of id " + id + " not found.");

            c.Id = DalC.Id;
            c.Name = DalC.Name;
            c.Phone = DalC.Phone;
            c.Location = BLObject.MakeLocation(DalC.Longitude, DalC.Latitude);

            List<PackageToList> PackagesFromCustomer = new();
            List<PackageToList> PackagesToCustomer = new();
            List<DO.Package> Packages = BLObject.Dal.GetPackageList();
            //Go through packageList and see if the customer is receiving or sending packages
            foreach (DO.Package package in Packages)
            {
                if (package.ReceiverId == c.Id)
                    PackagesToCustomer.Add(DalPackageToList(package.Id));
                if (package.SenderId == c.Id)
                    PackagesFromCustomer.Add(DalPackageToList(package.Id));
            }

            c.PackagesFromCustomer = PackagesFromCustomer;
            c.PackagesToCustomer = PackagesToCustomer;

            return c;
        }

        /// <summary>
        /// Receives ID and converts droneToList drone to DroneInCharge
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public DroneInCharge DroneToListToInCharge(int id)
        {
            
            DroneInCharge d = new();
            DroneToList DroneToList = BLObject.BLDroneList.Find(x => x.Id == id);

            if (DroneToList == null)
                throw new MessageException("Error: Object of id " + id + " not found.");

            d.Id = DroneToList.Id;
            d.BatteryStatus = DroneToList.BatteryStatus;
            return d;
        }

        /// <summary>
        /// Receives Base station ID and returns the BL base station from the Dal Base Station
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BaseStation DalToBlStation(int id)
        {
            List<DO.Station> StationList = BLObject.Dal.GetStationList();
            BaseStation b = new();
            DO.Station s = StationList.Find(x => x.Id == id);

            if (s.Id == 0)
                throw new MessageException("Error: Object of id " + id + " not found.");

            b.Id = s.Id;
            b.Name = s.Name;
            b.Location = BLObject.MakeLocation(s.Longitude, s.Latitude);
            b.AvailableChargeSlots = s.ChargeSlots;

            //create list of DroneInCharge
            List<DroneInCharge> ChargeList = new();
            foreach(DroneToList Drone in BLObject.BLDroneList)
            {// Drone.Location == b.Location&& Drone.DroneStatus == DroneStatus.maintenance
                if ((Drone.Location.latitude == b.Location.latitude && Drone.Location.longitude == b.Location.longitude) && (Drone.DroneStatus == DroneStatus.maintenance) )//always returns false no matter what
                    ChargeList.Add(DroneToListToInCharge(Drone.Id));
            }

            b.ChargingDroneList = ChargeList;
            return b;
        }

        /// <summary>
        /// Receives Id and returns BL BaseStationToList from Bl BaseStation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public BaseStationToList StationToStationToList(int id)
        {
            BaseStation s = DalToBlStation(id);
            BaseStationToList ListS = new();

            ListS.Id = s.Id;
            ListS.Name = s.Name;
            ListS.AvailableChargeSlots = s.AvailableChargeSlots;
            ListS.OccupiedChargeSlots = s.ChargingDroneList.Count();

            return ListS;
        }

        /// <summary>
        /// Receives ID of customer and returns CustomerToList Customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public CustomerToList CustomerToCustomerToList(int id)
        {
            Customer c = DalToBlCustomer(id);
            CustomerToList listC = new();

            listC.Id = c.Id;
            listC.Name = c.Name;
            listC.phone = c.Phone;

            //initialise ints to be counted from packages in c.Pack
            listC.NumberOfDeliveredPackagesSent = 0;
            listC.NumberOfUndeliveredPackagesSent = 0;
            listC.NumberOfRecievedPackages = 0;
            listC.NumberOfExpectedPackages = 0;
            
            foreach(PackageToList p in c.PackagesFromCustomer)
            {
                if (p.SenderId == id && p.PackageStatus == PackageStatus.delivered)
                    listC.NumberOfDeliveredPackagesSent++;
                if (p.SenderId == id && p.PackageStatus != PackageStatus.delivered)
                    listC.NumberOfUndeliveredPackagesSent++;
            }

            foreach (PackageToList p in c.PackagesToCustomer)
            {
                if (p.ReceiverId == id && p.PackageStatus == PackageStatus.delivered)
                    listC.NumberOfRecievedPackages++;
                if (p.ReceiverId == id && p.PackageStatus != PackageStatus.delivered)
                    listC.NumberOfExpectedPackages++;
            }

            return listC;
        }
    }
}
