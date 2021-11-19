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
        /// Adds Station List to list.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="name"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="slots"></param>
        IBL.BO.BaseStation AddBaseStation(int name, double longitude, double latitude, int availableSlots)
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
            if (latitude <-90 || latitude > 90)
                throw new IBL.BO.MessageException("Error: latitude exceeds bounds");
            if (availableSlots < 0)
                throw new IBL.BO.MessageException("Error: ChargeSlots must be positive");

            DalObject.DalObject.AddStation(name, longitude, latitude, availableSlots);

            //Create IBL.BO.BaseStation
            IBL.BO.BaseStation b = new();
            IDAL.DO.Station S = DalObject.DataSource.StationList.Find(x => x.Name == name);
            return b;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Weight"></param>
        /// <param name="chargingStation"></param>
        void AddDrone(string model, IDAL.DO.WeightCategory Weight, int chargingStation)
        { //manufacturerId was also included as a parameter but our
          //Id's are automatically created in the data layer for each entered object
            DalObject.DalObject.AddDrone(model, Weight);
        }
        void AddCustomer(int CustomerId, string name, string phone, double Longitude, double Latitude)
        {

        }

        void AddPackage(int CustomerId, int ReceiverId, IDAL.DO.WeightCategory Weight, IDAL.DO.Priority Priority)
        {

        }
    }
}

