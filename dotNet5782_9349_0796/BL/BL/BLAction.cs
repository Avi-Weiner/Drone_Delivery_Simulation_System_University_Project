using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        public void SendDroneToCharge(int DroneId)
        {
            int DroneIndex = BLObject.DroneList.FindIndex(x => x.Id == DroneId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (DroneIndex == -1)
            {
                throw new IBL.BO.MessageException("Error: Drone not found.\n");
            }
            if(BL.BLObject.DroneList[DroneIndex].DroneStatus != IBL.BO.DroneStatus.free)
            {
                throw new IBL.BO.MessageException("Error: Drone is not free.\n");
            }

            IDAL.DO.Station StationClose = BLObject.ClosestStation(BL.BLObject.DroneList[DroneIndex].Location);

            if (BLObject.ChargeForDistance(BL.BLObject.DroneList[DroneIndex].Weight, BLObject.DistanceBetween(BL.BLObject.DroneList[DroneIndex].Location, BLObject.MakeLocation(StationClose.Longitude, StationClose.Latitude)))
                > BL.BLObject.DroneList[DroneIndex].BatteryStatus)
            {
                throw new IBL.BO.MessageException("Error: Not enough charge.\n");
            }

            if(StationClose.ChargeSlots<=0)
            {
                throw new IBL.BO.MessageException("Error: not enough charging slots");
            }
            //update battery state 
            BL.BLObject.DroneList[DroneIndex].BatteryStatus -= 
                BLObject.ChargeForDistance(BL.BLObject.DroneList[DroneIndex].Weight, BLObject.DistanceBetween(BL.BLObject.DroneList[DroneIndex].Location, BLObject.MakeLocation(StationClose.Longitude, StationClose.Latitude)));
            BL.BLObject.DroneList[DroneIndex].Location = BLObject.MakeLocation(StationClose.Longitude, StationClose.Latitude);
            BL.BLObject.DroneList[DroneIndex].DroneStatus = IBL.BO.DroneStatus.maintenance;
            int StationIndex = DalObject.DataSource.StationList.FindIndex(x => x.Id == StationClose.Id);
            StationClose.ChargeSlots -= 1;
            DalObject.DataSource.StationList[StationIndex] = StationClose;
            ///iii adding a mathcing instance///////////////////////////////////////////////////////////////////////////////////////
        }

        public void ReleaseDroneFromCharge(int DroneId)
        {
            int DroneIndex = BLObject.DroneList.FindIndex(x => x.Id == DroneId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (DroneIndex == -1)
            {
                throw new IBL.BO.MessageException("Error: Drone not found.\n");
            }
            if (BL.BLObject.DroneList[DroneIndex].DroneStatus != IBL.BO.DroneStatus.maintenance)
            {
                throw new IBL.BO.MessageException("Error: Drone is not in maintenance.\n");
            }


        }

            public void AssignPackageToDrone(int DroneId)
        {
            int DroneIndex = BLObject.DroneList.FindIndex(x => x.Id == DroneId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (DroneIndex == -1)
            {
                throw new IBL.BO.MessageException("Error: Drone not found.\n");
            }
            if (BL.BLObject.DroneList[DroneIndex].DroneStatus != IBL.BO.DroneStatus.maintenance)
            {
                throw new IBL.BO.MessageException("Error: Drone is not in maintenance.\n");
            }
        }
        

        public void DroneCollectsAPackage(int DroneId)
        {
            int DroneIndex = BLObject.DroneList.FindIndex(x => x.Id == DroneId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (DroneIndex == -1)
            {
                throw new IBL.BO.MessageException("Error: Drone not found.\n");
            }
            if (BL.BLObject.DroneList[DroneIndex].DroneStatus != IBL.BO.DroneStatus.maintenance)
            {
                throw new IBL.BO.MessageException("Error: Drone is not in maintenance.\n");
            }

        }

        public void DroneDeliversPakcage(int DroneId)
        {
            int DroneIndex = BLObject.DroneList.FindIndex(x => x.Id == DroneId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (DroneIndex == -1)
            {
                throw new IBL.BO.MessageException("Error: Drone not found.\n");
            }
            if (BL.BLObject.DroneList[DroneIndex].DroneStatus != IBL.BO.DroneStatus.maintenance)
            {
                throw new IBL.BO.MessageException("Error: Drone is not in maintenance.\n");
            }
        }
    }
}
