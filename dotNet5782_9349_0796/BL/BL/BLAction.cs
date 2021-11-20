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
            int Dronei = DalObject.DataSource.DroneList.FindIndex(x => x.Id == DroneId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (Dronei == -1)
            {
                throw new IBL.BO.MessageException("Error: Drone not found\n");
            }



        }

        public void ReleaseDroneFromCharge(int DroneId)
        {
            int Dronei = DalObject.DataSource.DroneList.FindIndex(x => x.Id == DroneId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (Dronei == -1)
            {
                throw new IBL.BO.MessageException("Error: Drone not found\n");
            }
        }

        public void AssignPackageToDrone(int DroneId)
        {
            int Dronei = DalObject.DataSource.DroneList.FindIndex(x => x.Id == DroneId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (Dronei == -1)
            {
                throw new IBL.BO.MessageException("Error: Drone not found\n");
            }
        }
        

        public void DroneCollectsAPackage(int DroneId)
        {
            int Dronei = DalObject.DataSource.DroneList.FindIndex(x => x.Id == DroneId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (Dronei == -1)
            {
                throw new IBL.BO.MessageException("Error: Drone not found\n");
            }
            //ifDroneisUnavailable
            
        }

        public void DroneDeliversPakcage(int DroneId)
        {
            int Dronei = DalObject.DataSource.DroneList.FindIndex(x => x.Id == DroneId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (Dronei == -1)
            {
                throw new IBL.BO.MessageException("Error: Drone not found\n");
            }
        }
    }
}
