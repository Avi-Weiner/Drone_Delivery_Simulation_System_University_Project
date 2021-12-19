using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public partial class DalObject : DalApi.IDAL
    {
        /// <summary>
        /// Receives Package Id and returns its index in packageList
        /// </summary>
        /// <param name="PackageId"></param>
        /// <returns></returns>
        public static int GetPackage(int PackageId)
        {
            int i = DataSource.CustomerList.FindIndex(x => x.Id == PackageId);
            return i;
        }

        /// <summary>
        /// receiving a package to deliver
        /// </summary>
        public static void AddPackage(int InputSender, int InputReceiver, DO.WeightCategory InputWeight, DO.Priority InputPriority)
        {
                DataSource.PackageList.Add(new DO.Package
                {
                    Id = DataSource.GetNextUniqueID(),
                    SenderId = InputSender,
                    ReceiverId = InputReceiver,
                    Weight = InputWeight,
                    Priority = InputPriority,
                    Requested = DateTime.Now,
                    DroneId = 0, //No drone assigned yet
                    Scheduled = null,
                    PickedUp = null,
                    Delivered = null

                });

        }

        /// <summary>
        /// Assigning a drone to a package
        /// </summary>
        /// <param name="PackageId"></param>
        /// <param name="DroneId"></param>
        public static void AssignDroneToPackage(int PackageId, int DroneId)
        {
                GetDrone(DroneId);
                DO.Package P = DataSource.PackageList.Find(x => x.Id == PackageId);

                P.DroneId = DroneId;
                P.Scheduled = DateTime.Now;
        }

        /// <summary>
        /// Receives a package from Id PackageId and "drops it off" with a customer
        /// </summary>
        /// <param name="PackageId"></param>
        public static void PackageDropOff(int PackageId)
        {
                DO.Package P = DataSource.PackageList.Find(x => x.Id == PackageId);
                P.Delivered = DateTime.Now;
        }
    }
}