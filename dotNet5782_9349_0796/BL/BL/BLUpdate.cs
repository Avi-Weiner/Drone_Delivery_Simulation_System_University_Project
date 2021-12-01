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
        /// If drone Id given exists, the Drone Model will be updated and a BL.Drone will be returned
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Model"></param>
        public  IBL.BO.Drone UpdateDrone(int Id, string Model)
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

            int Listi = BLObject.BLDroneList.FindIndex(x => x.Id == Id);
            BLObject.BLDroneList[Listi].Model = Model;


            //Create IBL.BO.Drone to return
            IBL.BO.Drone d = new();
            d.Id = Drone.Id;
            d.Model = Model;
            d.Weight = BLObject.BLDroneList[Listi].Weight;
            d.Location = BLObject.BLDroneList[Listi].Location;
            d.BatteryStatus = BLObject.BLDroneList[Listi].BatteryStatus;
            d.Status = BLObject.BLDroneList[Listi].DroneStatus;

            return d;
        }

        /// <summary>
        /// Updates 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="StationName"></param>
        /// <param name="AmountOfChargingStation"></param>
        public  void UpdateStation(int Id, int StationName = -1, int AmountOfChargingStation = -1)
        {
            int Stationi = DalObject.DataSource.StationList.FindIndex(x => x.Id == Id);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (Stationi == -1)
            {
                throw new IBL.BO.MessageException("Error: Station not found\n");
            }

            IDAL.DO.Station Station = DalObject.DataSource.StationList[Stationi];
            if(StationName != -1)
            {
                Station.Name = StationName;
            }
            if(AmountOfChargingStation != -1)
            {
                Station.ChargeSlots = AmountOfChargingStation;
            }
            
            DalObject.DataSource.StationList[Stationi] = Station;

        }
        /// <summary>
        /// updates a customer: opriotns to udate are name and or phone.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        public  void UpdateCustomer(int Id, string Name = "-1", string Phone = "-1")
        {
            int Customeri = DalObject.DataSource.CustomerList.FindIndex(x => x.Id == Id);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (Customeri == -1)
            {
                throw new IBL.BO.MessageException("Error: Customer not found\n");
            }

            IDAL.DO.Customer Customer = DalObject.DataSource.CustomerList[Customeri];
            if (Name != "-1")
            {
                Customer.Name = Name;
            }
            if (Phone != "-1")
            {
                Customer.Phone = Phone;
            }

            DalObject.DataSource.CustomerList[Customeri] = Customer;
        }
    }
}
