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
        /// Returns a list of base stations
        /// </summary>
        /// <returns></returns>
        public List<IBL.BO.BaseStationToList> ListOfStations()
        {
            List<IBL.BO.BaseStationToList> list = new();
            foreach(IDAL.DO.Station station in DalObject.DataSource.StationList)
            {
                //converts DAL station to BL station to StationToList and adds to list
                list.Add(StationToStationToList(DalToBlStation(station.Id).Id));
            }

            return list;
        }

        /// <summary>
        /// Returns a list of customers
        /// </summary>
        /// <returns></returns>
        public List<IBL.BO.CustomerToList> ListOfCustomerss()
        {
            List<IBL.BO.CustomerToList> list = new();
            foreach (IDAL.DO.Customer c in DalObject.DataSource.CustomerList)
            {
                //converts DAL station to BL station to StationToList and adds to list
                //list.Add(StationToStationToList(DalToBlStation(station.Id).Id));
            }

            return list;
        }

    }
}
