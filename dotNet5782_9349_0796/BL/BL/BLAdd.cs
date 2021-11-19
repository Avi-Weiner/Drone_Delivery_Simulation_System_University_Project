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
        /// Adds a new IDAL base station and returns a new IBL.BO base station
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="name"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="slots"></param>

        public IBL.BO.BaseStation AddBaseStation(int name, double longitude, double latitude, int availableSlots)
        {//Took out id parameter as it is created automatically 

            //Console.WriteLine("Enter Longitude: ");
            //double longitude = Convert.ToDouble(Console.ReadLine());
            //Console.WriteLine("Enter Latitude: ");
            //double latitude = Convert.ToDouble(Console.ReadLine());
            //Console.WriteLine("Enter number of charge slots: ");
            //int slots = Convert.ToInt32(Console.ReadLine());

            //Input checking:
            if (longitude < -180 || longitude > 180)
                throw new IBL.BO.MessageException("Error: Longitude exceeds bounds");
            if (latitude < -90 || latitude > 90)
                throw new IBL.BO.MessageException("Error: latitude exceeds bounds");
            if (availableSlots < 0)
                throw new IBL.BO.MessageException("Error: ChargeSlots must be positive");

            DalObject.DalObject.AddStation(name, longitude, latitude, availableSlots);

            //Create IBL.BO.BaseStation
            IBL.BO.BaseStation b = new();
            IDAL.DO.Station S = DalObject.DataSource.StationList.Find(x => x.Name == name);
            b.Id = S.Id;
            b.Name = name;

            IBL.BO.Location l = new();
            l.latitude = latitude;
            l.longitude = longitude;
            b.Location = l;
            b.AvailableChargeSlots = availableSlots;

            List<IBL.BO.DroneInCharge> DroneChargeList = new List<IBL.BO.DroneInCharge>();
            b.ChargingDroneList = DroneChargeList;

            return b;
        }

        /// <summary>
        /// StationId for charging
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Weight"></param>
        /// <param name="chargingStation"></param>
        public IBL.BO.Drone AddDrone(string model, IDAL.DO.WeightCategory Weight, int StationId)
        {   //manufacturerId was also included as a parameter but our
            //Id's are automatically created in the data layer for each entered object

            //Input checking:
            int Stationi = DalObject.DataSource.StationList.FindIndex(x => x.Id == StationId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (Stationi == -1)
            {
                throw new IBL.BO.MessageException("Error: Station not found\n");
            }


            DalObject.DalObject.AddDrone(model, Weight);
            //Creading new IBL.BO.Drone 
            IBL.BO.Drone d = new();
            d.Model = model;
            d.Weight = Weight;

            IBL.BO.Location l = new();
            l.latitude = DalObject.DataSource.StationList[Stationi].Latitude;
            l.longitude = DalObject.DataSource.StationList[Stationi].Longitude;
            d.Location = l;

            var rand = new Random();
            d.BatteryStatus = rand.NextDouble() * (0.2) + 0.2; //create random battery status between 0.2 and 0.4
            d.Status = (IBL.BO.DroneStatus)(1); //put in maintenance status

            return d;
        }

        /// <summary>
        /// Add customerList to list and return customer IBL.BO
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="Longitude"></param>
        /// <param name="Latitude"></param>
        /// <returns></returns>
        public IBL.BO.Customer AddCustomer(int CustomerId, string name, string phone, double Longitude, double Latitude)
        {
            if (Longitude < -180 || Longitude > 180)
                throw new IBL.BO.MessageException("Error: Longitude exceeds bounds");
            if (Latitude < -90 || Latitude > 90)
                throw new IBL.BO.MessageException("Error: latitude exceeds bounds");


            DalObject.DalObject.AddCustomer(name, phone, Longitude, Latitude);

            //Create IBL.BO.BaseStation
            IBL.BO.Customer b = new();
            IDAL.DO.Customer S = DalObject.DataSource.CustomerList.Find(x => x.Name == name);
            b.Id = S.Id;
            b.Name = name;
            b.Phone = phone;

            IBL.BO.Location l = new();
            l.latitude = Latitude;
            l.longitude = Longitude;
            b.Location = l;

            return b;
        }



        public void AddPackage(int CustomerId, int ReceiverId, IDAL.DO.WeightCategory Weight, IDAL.DO.Priority Priority)
        {

        }
    }
    
}


