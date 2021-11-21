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
                throw new IBL.BO.MessageException("Error: Drone not found\n");
            }
            if(BL.BLObject.DroneList[DroneIndex].DroneStatus != IBL.BO.DroneStatus.free)
            {
                throw new IBL.BO.MessageException("Error: Drone is not free\n");
            }

            IDAL.DO.Station StationClose = BLObject.ClosestStation(BL.BLObject.DroneList[DroneIndex].Location);
            if(BLObject.ChargeForDistance(BL.BLObject.DroneList[DroneIndex].Weight, DistanceBetween(BL.BLObject.DroneList[DroneIndex].Location, BL.BLObject.DroneList[DroneIndex].)))
            

        }

        public void ReleaseDroneFromCharge(int DroneId)
        {
            int DronePlacement = DalObject.DataSource.DroneList.FindIndex(x => x.Id == DroneId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (DronePlacement == -1)
            {
                throw new IBL.BO.MessageException("Error: Drone not found\n");
            }
        }

        public void AssignPackageToDrone(int DroneId)
        {
            int DronePlacement = DalObject.DataSource.DroneList.FindIndex(x => x.Id == DroneId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (DronePlacement == -1)
            {
                throw new IBL.BO.MessageException("Error: Drone not found\n");
            }
        }
        

        public void DroneCollectsAPackage(int DroneId)
        {
            int DronePlacement = DalObject.DataSource.DroneList.FindIndex(x => x.Id == DroneId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (DronePlacement == -1)
            {
                throw new IBL.BO.MessageException("Error: Drone not found\n");
            }
            //ifDroneisUnavailable
            
        }

        public void DroneDeliversPakcage(int DroneId)
        {
            int DronePlacement = DalObject.DataSource.DroneList.FindIndex(x => x.Id == DroneId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (DronePlacement == -1)
            {
                throw new IBL.BO.MessageException("Error: Drone not found\n");
            }
        }
    }
}
