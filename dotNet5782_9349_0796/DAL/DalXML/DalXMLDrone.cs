using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DalXML
{
    public partial class DalXML : DalApi.IDAL
    {
        /// <summary>
        /// Receives Drone ID and returns its index in DroneList
        /// </summary>
        public DO.Drone GetDrone(int DroneId)
        {
            List<DO.Drone> DroneList = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dir + DronesFilePath);

            if (DroneList.Exists(x => x.Id == DroneId) == false)
                throw new Exception("customer does not exist");
            return DroneList.Find(x => x.Id == DroneId);
        }

        /// <summary>
        /// adding a drone to the existing drones list
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Weight"></param>
        public void AddDrone(string model, DO.WeightCategory Weight)
        {
            List<DO.Drone> DroneList = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dir + DronesFilePath);
            //add the new drone to the back of the list
            DroneList.Add(new DO.Drone
            {
                Id = GetNextUniqueId(),
                Model = model,
                MaxWeight = Weight
            });
            XMLTools.SaveListToXMLSerializer<DO.Drone>(DroneList, dir + DronesFilePath);
        }

        /// <summary>
        /// returns list of all Drones
        /// </summary>
        /// <returns></returns>
        public List<DO.Drone> GetDroneList()
        {
            return XMLTools.LoadListFromXMLSerializer<DO.Drone>(dir + DronesFilePath);
        }

        /// <summary>
        /// Set Drone List
        /// </summary>
        /// <param name="Customer"></param>
        public void SetDroneList(List<DO.Drone> Drones)
        {
            XMLTools.SaveListToXMLSerializer<DO.Drone>(Drones, dir + DronesFilePath);
        }

        /// <summary>
        /// Receives Drone with ID DroneId and picks up package with ID PackageId
        /// </summary>
        /// <param name="PackageId"></param>
        /// <param name="DroneId"></param>
        public static void DronePickUp(int PackageId, int DroneId)
        {
            List<DO.Package> PackageList = XMLTools.LoadListFromXMLSerializer<DO.Package>(dir + PackagesFilePath);
            List<DO.Drone> DroneList = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dir + DronesFilePath);
            try
            {
                DO.Package P = PackageList.Find(x => x.Id == PackageId);
                DO.Drone D = DroneList.Find(x => x.Id == DroneId);
                if (P.Id == PackageId && D.Id == DroneId)
                {
                    P.PickedUp = DateTime.Now;
                    P.DroneId = DroneId;
                }
            }
            catch (DO.MessageException e)
            {
                throw new DO.MessageException("Error: DronePickUp(): List is Null");
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
            List<DO.Drone> DroneList = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dir + DronesFilePath);
            List<DO.Station> StationList = XMLTools.LoadListFromXMLSerializer<DO.Station>(dir + StationsFilePath);
            int i = DroneList.FindIndex(x => x.Id == DroneId);
            if (i == -1)
                throw new DO.MessageException("Error: Drone not found.");

            //Get station
            DO.Station S = StationList.Find(x => x.Id == StationId);
            DO.Drone D = DroneList.Find(x => x.Id == DroneId);

            //minus 1 to charge slots
            S.ChargeSlots--;

            //Get Station i
            int j = StationList.FindIndex(x => x.Id == StationId);
            if (j == -1)
                throw new DO.MessageException("Error: Station not found.");

            StationList[j] = S;

            //Adding instance of Dronecharger (Need to save this somewhere or it will just get deleted...),
            //specs unspecific of where to save it so for the meantime it will be deleted
            DO.DroneCharger newCharger = new DO.DroneCharger();
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
            List<DO.Drone> DroneList = XMLTools.LoadListFromXMLSerializer<DO.Drone>(dir + DronesFilePath);
            List<DO.Station> StationList = XMLTools.LoadListFromXMLSerializer<DO.Station>(dir + StationsFilePath);
            //Check Drone
            int i = DroneList.FindIndex(x => x.Id == DroneId);
            if (i == -1)
                throw new DO.MessageException("Error: Drone not found.");

            //Check station
            int j = StationList.FindIndex(x => x.Id == StationId);
            if (j == -1)
                throw new DO.MessageException("Error: Station not found.");

            //Free up charge slot 
            DO.Station s = StationList.Find(x => x.Id == StationId);
            s.ChargeSlots++;
            StationList[j] = s;
            XMLTools.SaveListToXMLSerializer<DO.Station>(StationList, dir + StationsFilePath);

        }
    }
}
