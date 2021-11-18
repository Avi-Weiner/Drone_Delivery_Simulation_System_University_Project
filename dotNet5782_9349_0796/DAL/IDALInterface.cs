using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;

namespace IDAL
{
    public interface IDAL
    {
        //General DalObject
        public double[] PowerConsumptions();

        //Customer DalObject
        public int GetCustomer(int CustomerId);

        //No other methods work with static,
        //Without static they don't work in the main.
    }
}
