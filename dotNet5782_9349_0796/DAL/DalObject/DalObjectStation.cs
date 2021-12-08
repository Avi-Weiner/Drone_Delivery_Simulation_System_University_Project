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
        /// Receives Station Id and returns index of station in StationList
        /// </summary>
        public static int GetStation(int StationId)
        {
            int i = DataSource.CustomerList.FindIndex(x => x.Id == StationId);
            return i;
        }

        /// <summary>
        /// adding base station to the stations list
        /// </summary>
        /// <param name="name"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="slots"></param>
        public static void AddStation(int name, double longitude, double latitude, int slots)
        {
            //add station to the back of the station list
            DataSource.StationList.Add(new IDAL.DO.Station
            {
                Id = DataSource.GetNextUniqueID(),
                Name = name,
                Longitude = longitude,
                Latitude = latitude,
                ChargeSlots = slots
            });;
        }
    }
}
