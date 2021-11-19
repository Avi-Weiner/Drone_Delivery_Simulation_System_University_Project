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
        /// If drone Id given exists, the Drone Model will be updated param Model
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Model"></param>
        void UpdateDrone(int Id, string Model)
        {

            int DronePlacement = DalObject.DataSource.DroneList.FindIndex(x => x.Id == Id);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if(DronePlacement == -1)
            {
                throw new IBL.BO.MessageException("Error: Drone not found\n");
            }
            
            IDAL.DO.Drone Drone = DalObject.DataSource.DroneList[DronePlacement];
            Drone.Model = Model;
            DalObject.DataSource.DroneList[DronePlacement] = Drone;
            //DalObject.DataSource.DroneList.
        }

        void UpdateStation(int Id, int StationName = 0, int AmountOfChargingStation = -1)
        {
            int StationPlacement = DalObject.DataSource.StationList.FindIndex(x => x.Id == Id);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (StationPlacement == -1)
            {
                throw new IBL.BO.MessageException("Error: Station not found\n");
            }

            IDAL.DO.Station Station = DalObject.DataSource.StationList[StationPlacement];
            if(StationName != 0)
            {
                Station.Name = StationName;
            }
            if(AmountOfChargingStation != -1)
            {
                Station.ChargeSlots = AmountOfChargingStation;
            }
            
            DalObject.DataSource.StationList[StationPlacement] = Station;
            //DalObject.DataSource.DroneList.
        }

        void UpdateCustomer(int Id, string Name = "", string Phone = "")
        {
            int CustomerPlacement = DalObject.DataSource.CustomerList.FindIndex(x => x.Id == Id);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (CustomerPlacement == -1)
            {
                throw new IBL.BO.MessageException("Error: Customer not found\n");
            }

            IDAL.DO.Customer Customer = DalObject.DataSource.CustomerList[CustomerPlacement];
            if (Name != "")
            {
                Customer.Name = Name;
            }
            if (Phone != "")
            {
                Customer.Phone = Phone;
            }

            DalObject.DataSource.CustomerList[CustomerPlacement] = Customer;
        }
    }
}
