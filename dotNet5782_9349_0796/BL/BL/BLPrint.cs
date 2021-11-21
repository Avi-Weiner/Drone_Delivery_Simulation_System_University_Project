using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        public class BLPrint
        {
            public void PrintBaseSationById(int Id)
            {
                int Stationi = DalObject.DataSource.StationList.FindIndex(x => x.Id == Id);
                //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
                if (Stationi == -1)
                {
                    throw new IBL.BO.MessageException("Error: Station not found\n");
                }

            }
            public void PrintDroneById(int Id)
            {

            }
            public void PritnPackageById(int Id)
            {

            }
            public void PrintBaseStationById(int Id)
            {

            }
            public void PrintBaseStationList()
            {
                //foreach(BL.BLObject.)
            }
            public void PrintDroneList()
            {

            }
            public void PrintCustomerList()
            {

            }
            public void PrintPackageList()
            {

            }
            public void PrintUnassignedPackages()
            {

            }
            public void PrintBaseStationsWithAvailableChargingSlots()
            {

            }
        }
    }
}
