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
        /// If drone Id given exists, the Drone Model will be updated and a Drone will be returned
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Model"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public  Drone UpdateDrone(int Id, string Model)
        {
            List<DO.Drone> DroneList = BLObject.Dal.GetDroneList();
            int Dronei = DroneList.FindIndex(x => x.Id == Id);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if(Dronei == -1)
            {
                throw new MessageException("Error: Drone not found\n");
            }
            

            DO.Drone Drone = DroneList[Dronei];
            Drone.Model = Model;
            DroneList[Dronei] = Drone;

            lock (BLObject.Dal)
            {
                //save back to dal layer
                BLObject.Dal.SetDroneList(DroneList);
            }

            //drone list in bl layer
            int Listi = BLObject.BLDroneList.FindIndex(x => x.Id == Id);
            BLObject.BLDroneList[Listi].Model = Model;


            //Create Drone to return
            Drone d = new();
            d.Id = Drone.Id;
            d.Model = Model;
            d.Weight = BLObject.BLDroneList[Listi].Weight;
            d.Location = BLObject.BLDroneList[Listi].Location;
            d.BatteryStatus = BLObject.BLDroneList[Listi].BatteryStatus;
            d.Status = BLObject.BLDroneList[Listi].DroneStatus;

            return d;
        }

        /// <summary>
        /// Updates Station
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="StationName"></param>
        /// <param name="AmountOfChargingStation"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public  void UpdateStation(int Id, int StationName = -1, int AmountOfChargingStation = -1)
        {
            List<DO.Station> StationList = BLObject.Dal.GetStationList();
            int Stationi = StationList.FindIndex(x => x.Id == Id);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (Stationi == -1)
            {
                throw new MessageException("Error: Station not found\n");
            }

            DO.Station Station = StationList[Stationi];
            if(StationName != -1)
            {
                Station.Name = StationName;
            }
            if(AmountOfChargingStation != -1)
            {
                Station.ChargeSlots = AmountOfChargingStation;
            }

            lock (BLObject.Dal)
            {
                StationList[Stationi] = Station;
                BLObject.Dal.SetStationList(StationList);
            }

        }

        /// <summary>
        /// updates a customer: options to update are name and or phone.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public  void UpdateCustomer(int Id, string Name = "-1", string Phone = "-1")
        {
            List<DO.Customer> CustomerList = BLObject.Dal.GetCustomerList();
            int Customeri = CustomerList.FindIndex(x => x.Id == Id);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (Customeri == -1)
            {
                throw new MessageException("Error: Customer not found\n");
            }

            DO.Customer Customer = CustomerList[Customeri];
            if (Name != "-1")
            {
                Customer.Name = Name;
            }
            if (Phone != "-1")
            {
                Customer.Phone = Phone;
            }

            lock (BLObject.Dal)
            {
                CustomerList[Customeri] = Customer;
                BLObject.Dal.SetCustomerList(CustomerList);
            }
        }

        /// <summary>
        /// updates a customer: options to update are name and or phone.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Name"></param>
        /// <param name="Phone"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdatePackage(int Id, int SenderId = 0, int ReceiverId = 0, string Weight = "", string Priority = "")
        {
            List<DO.Package> PackageList = BLObject.Dal.GetPackageList();
            int packagei = PackageList.FindIndex(x => x.Id == Id);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (packagei == -1)
            {
                throw new MessageException("Error: Package not found\n");
            }

            DO.Package package = PackageList[packagei];

            if (SenderId != 0) { 
                int Senderi = DalObject.DataSource.CustomerList.FindIndex(x => x.Id == SenderId);
                if (Senderi == -1)
                {
                    throw new MessageException("Error: Sender not found\n");
                }
                package.SenderId = SenderId;
            }

            if (ReceiverId != 0)
            {
                int Receiveri = DalObject.DataSource.CustomerList.FindIndex(x => x.Id == ReceiverId);
                if (Receiveri == -1)
                {
                    throw new MessageException("Error: Receiver not found\n");
                }
                package.ReceiverId = ReceiverId;
            }

            //Check if Weight is valid
            if (Weight != "")
            {
                if (Weight != "light" && Weight != "medium" && Weight != "heavy")
                    throw new MessageException("Error: Weight status invalid\n");
                //If valid, convert to WeightCatagory
                DO.WeightCategory weightCatagory = (DO.WeightCategory)Enum.Parse(typeof(DO.WeightCategory), Weight);

                package.Weight = weightCatagory;
            }

            //Check if Priority is valid
            if (Priority != "")
            {
                if (Priority != "regular" && Priority != "fast" && Priority != "emergency")
                    throw new MessageException("Error: Weight status invalid\n");
                //If valid, convert to WeightCatagory
                DO.Priority priorityCatagory = (DO.Priority)Enum.Parse(typeof(DO.Priority), Priority);

                package.Priority = priorityCatagory;
            }

            lock (BLObject.Dal)
            {
                PackageList[packagei] = package;
                BLObject.Dal.SetPackageList(PackageList);
            }
        }

        /// <summary>
        /// Receives a package Id and deletes the package, freeing up a drone if necessary
        /// </summary>
        /// <param name="Id"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DeletePackage(int Id)
        {
            List<DO.Package> PackageList = BLObject.Dal.GetPackageList();
            int packagei = PackageList.FindIndex(x => x.Id == Id);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (packagei == -1)
            {
                throw new MessageException("Error: Package not found\n");
            }

            Package package = DalToBlPackage(Id);

            if (package.DroneId != 0)
            {
                int Dronei = BLObject.BLDroneList.FindIndex(x => x.Id == package.DroneId);
                //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
                if (Dronei == -1)
                {
                    throw new MessageException("Error: Drone assigned to package not found.");
                }

                //Unassign package from its drone
                BLObject.BLDroneList[Dronei].PackageId = null;
            }

            lock (BLObject.Dal)
            {
                //Delete package from package list
                PackageList.RemoveAt(packagei);
                BLObject.Dal.SetPackageList(PackageList);
            }

        }
    }
}
