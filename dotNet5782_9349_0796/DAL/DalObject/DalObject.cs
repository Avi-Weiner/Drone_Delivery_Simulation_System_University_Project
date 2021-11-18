using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static IDAL.IDALInterface;


namespace DalObject
{
    public partial class DalObject : IDAL.IDALInterface
    {
        //creates a DAL object by intializing values accordign to Initialize
        DalObject()
        {
            DataSource.Initialize();//constructor for DalObjects
        }

    }
}




