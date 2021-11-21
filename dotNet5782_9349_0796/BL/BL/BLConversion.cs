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

    }

}
