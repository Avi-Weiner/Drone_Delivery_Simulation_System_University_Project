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
        /// Receives Station Id and returns index of station in StationList
        /// </summary>
        public DO.Station GetStation(int StationId)
        {
            List<DO.Station> StationList = XMLTools.LoadListFromXMLSerializer<DO.Station>(dir + StationsFilePath);
            int i = StationList.FindIndex(x => x.Id == StationId);
            if (i == -1)
                throw new DO.MessageException("Error: Station not found.");

            return StationList[i];
        }

        /// <summary>
        /// adding base station to the stations list
        /// </summary>
        /// <param name="name"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="slots"></param>
        public void AddStation(int name, double longitude, double latitude, int slots)
        {
            List<DO.Station> StationList = XMLTools.LoadListFromXMLSerializer<DO.Station>(dir + StationsFilePath);
            //add station to the back of the station list
            StationList.Add(new DO.Station
            {
                Id = GetNextUniqueId(),
                Name = name,
                Longitude = longitude,
                Latitude = latitude,
                ChargeSlots = slots
            });
            XMLTools.SaveListToXMLSerializer<DO.Station>(StationList, dir + StationsFilePath);
        }

        public List<DO.Station> GetStationList()
        {
            return XMLTools.LoadListFromXMLSerializer<DO.Station>(dir + StationsFilePath);
        }
        public void SetStationList(List<DO.Station> Stations)
        {
            XMLTools.SaveListToXMLSerializer<DO.Station>(Stations, dir + StationsFilePath);
        }
    }
}
