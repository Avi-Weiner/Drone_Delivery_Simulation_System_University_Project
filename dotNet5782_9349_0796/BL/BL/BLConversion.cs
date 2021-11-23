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
        public IBL.BO.BaseStation DalToBLStation(int id)
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
            d.PackageInTransfer = p;

            return d;
        }

        /// <summary>
        /// Returns a BL package from the DalPackage from the given id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IBL.BO.Package DalToBLPackage(int id)
        {
            IBL.BO.Package p = new();
            IDAL.DO.Package DalP = DalObject.DataSource.PackageList.Find(x => x.Id == id);

            p.Id = DalP.Id;


            return p;
        }
    }
}
