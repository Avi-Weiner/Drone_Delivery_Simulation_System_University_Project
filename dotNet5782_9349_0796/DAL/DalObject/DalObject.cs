using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace DalObject
{
    public partial class DalObject : DalApi.IDAL
    {
        //creates a DAL object by intializing values accordign to Initialize
        
        static DalObject ThisObject = null;
        
        /// <summary>
        /// Constructor for DalObject, initialises all entities
        /// </summary>
        private DalObject()
        {
            DataSource.Initialize();//constructor for DalObjects
        }

        /// <summary>
        /// if dal was already intialized the object already there will be returned 
        /// otherwise a new one will be created.
        /// </summary>
        /// <returns></returns>
        public static DalObject GetDalObject()
        {
            if(ThisObject == null)
            {
                ThisObject = new DalObject();
            }

            return ThisObject;       
            
        }

        /// <summary>
        /// Returns an array of the power consumptions { Free, Light, Medium, Heavy, ChargingRate }
        /// </summary>
        /// <returns></returns>
        public static double[] GetPowerConsumptions()
        {
            double[] array = new double[5];
            array[0] = DataSource.GetFree();
            array[1] = DataSource.GetLightWeight();
            array[2] = DataSource.GetMediumWeight();
            array[3] = DataSource.GetHeavyWeight();
            array[4] = DataSource.GetChargingRate();

            return array;
        }
    }
}




