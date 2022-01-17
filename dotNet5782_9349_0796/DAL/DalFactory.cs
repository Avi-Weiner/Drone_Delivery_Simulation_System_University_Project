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
        /// Creates and returns a DalObject (Singleton)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static DalApi.IDAL GetDal(string param)
        {
            if(param == "DalXml")
            {
                return DalXML.DalXML.GetDalXML();
            }
            return DalObject.DalObject.GetDalObject();
        }
    }
    
}


