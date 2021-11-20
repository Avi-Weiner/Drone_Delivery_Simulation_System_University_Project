using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;


namespace BL
{
    public partial class BL : IBL.IBL
    {
        public partial class BLObject
        {
            public static List<IBL.BO.DroneToList> DroneList = new List<IBL.BO.DroneToList>();
        }
    }
}
