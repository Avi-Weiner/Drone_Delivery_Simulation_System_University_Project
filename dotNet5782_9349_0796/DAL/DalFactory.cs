using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DalFactory
    {
        /// <summary>
        /// method recieves a string that will be updated to specify 
        /// Dal.xml or Dal..... this is because we are on stage 1 and
        /// need to do it in stage three.
        /// the method return an instance of the object. (which is a singleton)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static DalApi.IDAL GetDal(string param)
        {
            return DalObject.DalObject.GetDalObject();
        }
    }
    
}


