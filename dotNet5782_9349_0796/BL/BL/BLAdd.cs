using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        void AddBaseStation(int Id, string name, double location, int slots)
        {

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

