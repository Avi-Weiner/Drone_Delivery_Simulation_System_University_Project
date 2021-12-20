using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DalFactory
    {
        static DalApi.IDAL GetDal(string param)
        {
            return DalObject.DalObject.GetDalObject();
        }
    }
    
}


