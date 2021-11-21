using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{

    public partial class BL : IBL.IBL
    {

        public IBL.BO.BaseStation DalToBLStation(int id)
        {
            IBL.BO.BaseStation b = new();
            IDAL.DO.Station s = DalObject.DataSource.StationList.Find(x => x.Id == id);

            b.Id = s.Id;
            b.Name = s.Name;
            b.Location = BL.BLObject.MakeLocation(s.Longitude, s.Latitude);



            return b;
        }

    }

}
