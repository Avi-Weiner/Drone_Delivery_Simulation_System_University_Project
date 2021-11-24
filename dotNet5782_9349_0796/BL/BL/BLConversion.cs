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
        /// Returns BL BaseStation from given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IBL.BO.BaseStation DalToBlStation(int id)
        {
            IBL.BO.BaseStation b = new();
            IDAL.DO.Station s = DalObject.DataSource.StationList.Find(x => x.Id == id);

            b.Id = s.Id;
            b.Name = s.Name;
            b.Location = BL.BLObject.MakeLocation(s.Longitude, s.Latitude);
            b.AvailableChargeSlots = s.ChargeSlots;

            List<IBL.BO.DroneInCharge> chargingDrones = new();

            foreach(IBL.BO.DroneToList d in BLObject.DroneList)
            {
                if (d.Location == b.Location && d.DroneStatus == IBL.BO.DroneStatus.maintenance)
                {
                    IBL.BO.DroneInCharge droneInCharge = new();
                    droneInCharge.Id = d.Id;
                    droneInCharge.BatteryStatus = d.BatteryStatus;
                    chargingDrones.Add(droneInCharge);
                }
            }
            b.ChargingDroneList = chargingDrones;

            return b;
        }

        /// <summary>
        /// Returns BL Drone from given id, converting it from the DroneToList list
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IBL.BO.Drone DroneToListToDrone(int id)
        {
            IBL.BO.Drone d = new();
            IBL.BO.DroneToList DroneToList = BLObject.DroneList.Find(x => x.Id == id);

            d.Id = DroneToList.Id;
            d.Model = DroneToList.Model;
            d.Weight = DroneToList.Weight;
            d.BatteryStatus = DroneToList.BatteryStatus;
            d.Status = DroneToList.DroneStatus;
            d.Location = DroneToList.Location;

            IDAL.DO.Package p = DalObject.DataSource.PackageList.Find(x => x.Id == DroneToList.PackageId);
            // need to do a package conversion
            d.PackageInTransfer = DalToBlPackage(DroneToList.Id);

            return d;
        }

        /// <summary>
        /// Returns a BL package from the DalPackage from the given id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IBL.BO.Package DalToBlPackage(int id)
        {
            IBL.BO.Package p = new();
            IDAL.DO.Package DalP = DalObject.DataSource.PackageList.Find(x => x.Id == id);

            p.Id = DalP.Id;
            //Get the senders and receivers ID's from customerList and convert to BL Customers
            p.Sender = DalToBlCustomer(DalObject.DataSource.CustomerList.Find(x => x.Id == DalP.SenderId).Id);
            p.Receiver = DalToBlCustomer(DalObject.DataSource.CustomerList.Find(x => x.Id == DalP.ReceiverId).Id);
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
        public IBL.BO.PackageStatus TimesToStatus(DateTime? scheduled, DateTime? PickedUp, DateTime? Delivered)
        {
            if (scheduled == null)
                return IBL.BO.PackageStatus.created;
            else if (scheduled != null && PickedUp == null)
                return IBL.BO.PackageStatus.assigned;
            else if (PickedUp != null && Delivered == null)
                return IBL.BO.PackageStatus.collected;
            else if (Delivered != null)
                return IBL.BO.PackageStatus.delivered;
            else 
                throw new IBL.BO.MessageException("Error: TimesToStatus() failed");
        }

        /// <summary>
        /// Receives the ID of a DAl package and returns the corresponding IBL.BO.PackageToList item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IBL.BO.PackageToList DalPackageToList(int id)
        {
            IBL.BO.PackageToList PList = new();
            IDAL.DO.Package p = DalObject.DataSource.PackageList.Find(x => x.Id == id);

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
        public IBL.BO.Customer DalToBlCustomer(int id)
        {
            IBL.BO.Customer c = new();
            IDAL.DO.Customer DalC = DalObject.DataSource.CustomerList.Find(x => x.Id == id);

            c.Id = DalC.Id;
            c.Name = DalC.Name;
            c.Phone = DalC.Phone;
            c.Location = BLObject.MakeLocation(DalC.Longitude, DalC.Latitude);

            List<IBL.BO.PackageToList> PackagesFromCustomer = new();
            List<IBL.BO.PackageToList> PackagesToCustomer = new();

            //Go throw packageList and see if the customer is receiving or sending packages
            foreach(IDAL.DO.Package package in DalObject.DataSource.PackageList)
            {
                if (package.ReceiverId == c.Id)
                    PackagesToCustomer.Add(DalPackageToList(package.Id));
                if (package.SenderId == c.Id)
                    PackagesFromCustomer.Add(DalPackageToList(package.Id));
            }

            return c;
        }
    }
}
