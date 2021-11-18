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
        void AddBaseStation(int Id, int name, double longitude, double latitude, int slots)
        {
            DalObject.DalObject.AddStation(longitude, latitude, slots);
        }

        void AddDrone(int manufactureId, string model, IDAL.DO.WeightCategory Weight, int chargingStation)
        {
            
        }
        void AddCustomer(int CustomerId, string name, string phone, double Longitude, double Latitude)
        {

        }

        void AddPackage(int CustomerId, int ReceiverId, IDAL.DO.WeightCategory Weight, IDAL.DO.Priority Priority)
        {

        }
    }
}

