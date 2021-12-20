using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class BlFactory
    {

        /// <summary>
        /// Creates and returns a DalObject (Singleton)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public static BlApi.IBL GetBl()
        {
            return BL.GetBLObject();
        }

    }
}
