using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalObject
{
    public partial class DalObject : IDAL.IDAL
    {
        /// <summary>
        /// Receives Drone ID and returns its index in DroneList
        /// </summary>
        public static int GetDrone(int DroneId)
        {
            int i = DataSource.CustomerList.FindIndex(x => x.Id == DroneId);
  
            if (DataSource.DroneList[i].Id != DroneId)
                throw new IDAL.DO.MessageException("Error: Drone not found.");
            return i;
        }

        /// <summary>
        ///  adding a drone to the existing drones list
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Weight"></param>
        public static void AddDrone(string model, IDAL.DO.WeightCategory Weight)
        {
                //add the new drone to the back of the list
                DataSource.DroneList.Add(new IDAL.DO.Drone
                {
                    Id = DataSource.GetNextUniqueID(),
                    Model = model,
                    MaxWeight = Weight
                });
        }

        /// <summary>
        /// Receives Drone with ID DroneId and picks up package with ID PackageId
        /// </summary>
        /// <param name="PackageId"></param>
        /// <param name="DroneId"></param>
        public static void DronePickUp(int PackageId, int DroneId)
        {
            try
            {
                IDAL.DO.Package P = DataSource.PackageList.Find(x => x.Id == PackageId);
                IDAL.DO.Drone D = DataSource.DroneList.Find(x => x.Id == DroneId);
                if (P.Id == PackageId && D.Id == DroneId)
                {
                    P.PickedUp = DateTime.Now;
                    P.DroneId = DroneId;
                }
            }
            catch (IDAL.DO.MessageException e)
            {
                Console.WriteLine(e);
            }

        }

        /// <summary>
        /// Sends a drone to get charged at the specified station
        /// </summary>
        /// <param name="DroneId"></param>
        /// <param name="StationId"></param>
        public static void ChargeDrone(int DroneId, int StationId)
        {
                //Get Drone
                int i = GetDrone(DroneId);

                //Get station
                IDAL.DO.Station S = DataSource.StationList.Find(x => x.Id == StationId);
                IDAL.DO.Drone D = DataSource.DroneList.Find(x => x.Id == DroneId);

                //minus 1 to charge slots
                S.ChargeSlots--;
                int j = GetStation(StationId);
                DataSource.StationList[j] = S;

                //Adding instance of Dronecharger (Need to save this somewhere or it will just get deleted...),
                //specs unspecific of where to save it so for the meantime it will be deleted
                IDAL.DO.DroneCharger newCharger = new IDAL.DO.DroneCharger();
                newCharger.DroneId = D.Id;
                newCharger.StationId = S.Id;
        }

        /// <summary>
        /// Releases Drone with ID DroneID from station with ID StationID
        /// </summary>
        /// <param name="DroneID"></param>
        /// <param name="StationID"></param>
        public static void ReleaseDrone(int DroneId, int StationId)
        {
                //Get Drone
                int i = GetDrone(DroneId);

                //Get station
                int j = GetStation(StationId);

                //Free up charge slot 
                IDAL.DO.Station s = DataSource.StationList.Find(x => x.Id == StationId);
                s.ChargeSlots++;
                DataSource.StationList[j] = s;

        }
    }
}
