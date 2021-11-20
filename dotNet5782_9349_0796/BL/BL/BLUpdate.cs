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
        public void UpdateDrone(int Id, string Model)
        {

            int Dronei = DalObject.DataSource.DroneList.FindIndex(x => x.Id == Id);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if(Dronei == -1)
            {
                throw new IBL.BO.MessageException("Error: Drone not found\n");
            }
            
            IDAL.DO.Drone Drone = DalObject.DataSource.DroneList[Dronei];
            Drone.Model = Model;
            DalObject.DataSource.DroneList[Dronei] = Drone;

            int Listi = BLObject.DroneList.FindIndex(x => x.Id == Id);
            BLObject.DroneList[Listi].Model = Model;

        }

        public void UpdateStation(int Id, int StationName = 0, int AmountOfChargingStation = -1)
        {
            int Stationi = DalObject.DataSource.StationList.FindIndex(x => x.Id == Id);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (Stationi == -1)
            {
                throw new IBL.BO.MessageException("Error: Station not found\n");
            }

            IDAL.DO.Station Station = DalObject.DataSource.StationList[Stationi];
            if(StationName != 0)
            {
                Station.Name = StationName;
            }
            if(AmountOfChargingStation != -1)
            {
                Station.ChargeSlots = AmountOfChargingStation;
            }
            
            DalObject.DataSource.StationList[Stationi] = Station;
            //DalObject.DataSource.DroneList.
        }

        public void UpdateCustomer(int Id, string Name = "", string Phone = "")
        {
            int Customeri = DalObject.DataSource.CustomerList.FindIndex(x => x.Id == Id);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (Customeri == -1)
            {
                throw new IBL.BO.MessageException("Error: Customer not found\n");
            }

            IDAL.DO.Customer Customer = DalObject.DataSource.CustomerList[Customeri];
            if (Name != "")
            {
                Customer.Name = Name;
            }
            if (Phone != "")
            {
                Customer.Phone = Phone;
            }

            DalObject.DataSource.CustomerList[Customeri] = Customer;
        }
    }
}
