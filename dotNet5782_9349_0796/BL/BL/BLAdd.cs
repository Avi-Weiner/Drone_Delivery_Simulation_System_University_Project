using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{

    public partial class BL : BlApi.IBL
    {
        /// <summary>
        /// Adds a new IDAL base station and returns a new BL base station
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="name"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="slots"></param>

        public  BaseStation AddBaseStation(int name, double longitude, double latitude, int availableSlots)
        {//Took out id parameter as it is created automatically 

            //Input checking:
            if (longitude < -180 || longitude > 180)
                throw new MessageException("Error: Longitude exceeds bounds");
            if (latitude < -90 || latitude > 90)
                throw new MessageException("Error: latitude exceeds bounds");
            if (availableSlots < 0)
                throw new MessageException("Error: ChargeSlots must be positive");

            BLObject.Dal.AddStation(name, longitude, latitude, availableSlots);

            //Create BaseStation
            BaseStation b = new();
            DO.Station S = DalObject.DataSource.StationList.Find(x => x.Name == name);
            b.Id = S.Id;
            b.Name = name;

            Location l = new();
            l.latitude = latitude;
            l.longitude = longitude;
            b.Location = l;
            b.AvailableChargeSlots = availableSlots;

            List<DroneInCharge> DroneChargeList = new List<DroneInCharge>();
            b.ChargingDroneList = DroneChargeList;

            return b;
        }

        /// <summary>
        /// StationId for charging
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Weight"></param>
        /// <param name="chargingStation"></param>
        public  Drone AddDrone(string Model, string Weight, int StationId)
        {   //manufacturerId was also included as a parameter but our
            //Id's are automatically created in the data layer for each entered object

            #region Input Checking
            //Input checking:
            int Stationi = DalObject.DataSource.StationList.FindIndex(x => x.Id == StationId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (Stationi == -1)
            {
                throw new MessageException("Error: Station not found\n");
            }

            //Check if Weight is valid
            if (Weight != "light" && Weight != "medium" && Weight != "heavy")
                throw new MessageException("Error: Weight status invalid\n");

            //If valid, convert to WeightCatagory
            DO.WeightCategory WeightCatagory = (DO.WeightCategory)Enum.Parse(typeof(DO.WeightCategory), Weight);
            #endregion

            int UniqueId = DalObject.DataSource.GetNextUniqueID(); //getting next unique id for immediate access
            BLObject.Dal.AddDrone(Model, WeightCatagory);

            //Creading new Drone 
            Drone d = new();
            d.Id = UniqueId;
            d.Model = Model;
            d.Weight = WeightCatagory;

            Location l = new();
            l.latitude = DalObject.DataSource.StationList[Stationi].Latitude;
            l.longitude = DalObject.DataSource.StationList[Stationi].Longitude;
            d.Location = l;

            var rand = new Random();

            d.BatteryStatus = rand.NextDouble() * (0.2) + 0.2; //create random battery status between 0.2 and 0.4
            d.Status = (DroneStatus)(1); //put in maintenance status

            //Adding to DroneToList
            DroneToList DTL = new();
            DTL.Id = UniqueId;
            DTL.Model = Model;
            DTL.Weight = WeightCatagory;
            DTL.BatteryStatus = d.BatteryStatus;
            DTL.DroneStatus = d.Status;
            DTL.Location = d.Location;
            DTL.PackageId = 0; //No package yet on a new drone

            BLObject.BLDroneList.Add(DTL);

            return d;
        }

        /// <summary>
        /// Add customerList to list and return customer BL
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="Longitude"></param>
        /// <param name="Latitude"></param>
        /// <returns></returns>
        public  Customer AddCustomer(string name, string phone, double Longitude, double Latitude)
        {//Customer id created automatically and therefore removed
            if (Longitude < -180 || Longitude > 180)
                throw new MessageException("Error: Longitude exceeds bounds");
            if (Latitude < -90 || Latitude > 90)
                throw new MessageException("Error: latitude exceeds bounds");
            if (name == "")
                throw new MessageException("Error: Name is empty");


            DalObject.DalObject.AddCustomer(name, phone, Longitude, Latitude);

            //Create BaseStation
            Customer b = new();
            DO.Customer c = DalObject.DataSource.CustomerList.Find(x => x.Name == name);
            b.Id = c.Id;
            b.Name = name;
            b.Phone = phone;

            Location l = new();
            l.latitude = Latitude;
            l.longitude = Longitude;
            b.Location = l;

            return b;
        }

        /// <summary>
        /// adds a package to the package list in DataSource.customerlist.
        /// </summary>
        /// <param name="SenderId"></param>
        /// <param name="ReceiverId"></param>
        /// <param name="Weight"></param>
        /// <param name="Priority"></param>
        /// <returns></returns>
        public  Package AddPackage(int SenderId, int ReceiverId, string Weight, string Priority)
        {
            #region InputChecking
            //------------------Input checking:----------------
            int senderi = DalObject.DataSource.CustomerList.FindIndex(x => x.Id == SenderId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (senderi == -1)
            {
                throw new MessageException("Error: Sender not found.\n");
            }
            int receiveri = DalObject.DataSource.CustomerList.FindIndex(x => x.Id == ReceiverId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (receiveri == -1)
            {
                throw new MessageException("Error: Receiver not found.\n");
            }
            //Check if Weight is valid
            if (Weight != "light" || Weight != "medium" || Weight != "heavy")
                throw new MessageException("Error: Weight status invalid\n");
            //Check if Priority is valid
            if (Priority != "regular" || Priority != "fast" || Priority != "emergency")
                throw new MessageException("Error: Priority invalide");
            #endregion

            DO.WeightCategory WeightCatagory = (DO.WeightCategory)Enum.Parse(typeof(DO.WeightCategory), Weight);
            DO.Priority priorityStatus = (DO.Priority)Enum.Parse(typeof(DO.Priority), Priority);

            int id = DalObject.DataSource.GetNextUniqueID(); //Get the next UniqueID which will be the ID of this package
            DalObject.DalObject.AddPackage(SenderId, ReceiverId, WeightCatagory, priorityStatus);

            Package p = new();

            p.Id = id;
            p.Weight = WeightCatagory;
            p.Priority = priorityStatus;

            p.CreationTime = DateTime.Now;
            p.AssigningTime = DateTime.MinValue;
            p.CollectingTime = DateTime.MinValue;
            p.DeliveringTime = DateTime.MinValue;

            //No drone is an Id of 0
            p.DroneId = 0;

            return p;
        }
    }
    
}


