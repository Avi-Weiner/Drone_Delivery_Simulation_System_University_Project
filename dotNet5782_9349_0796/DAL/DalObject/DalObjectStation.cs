using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace DalObject
{
    public partial class DalObject : DalApi.IDAL
    {
        /// <summary>
        /// Receives Station Id and returns index of station in StationList
        /// </summary>
        public DO.Station GetStation(int StationId)
        {
            int i = DataSource.StationList.FindIndex(x => x.Id == StationId);
            if (i == -1)
                throw new DO.MessageException("Error: Station not found.");

            return DataSource.StationList[i];
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
            //add station to the back of the station list
            DataSource.StationList.Add(new DO.Station
            {
                Id = DataSource.GetNextUniqueID(),
                Name = name,
                Longitude = longitude,
                Latitude = latitude,
                ChargeSlots = slots
            });
        }

        public List<DO.Station> GetStationList()
        {
            return DataSource.StationList;
        }
        public void SetStationList(List<DO.Station> Stations)
        {
            DataSource.StationList = Stations;
        }
    }
}
