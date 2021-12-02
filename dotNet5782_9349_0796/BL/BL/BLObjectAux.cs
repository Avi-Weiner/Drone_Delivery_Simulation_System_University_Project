using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Auxiliary BLClass methods
/// </summary>
namespace BL
{
    public partial class BL : IBL.IBL
    {
        public partial class BLObject
        {
            /// <summary>
            /// Returns the distance (double) between 2 IBL.BO.Location s
            /// </summary>
            /// <param name="first"></param>
            /// <param name="second"></param>
            /// <returns></returns>
            public static double DistanceBetween(IBL.BO.Location first, IBL.BO.Location second)
            {
                return Math.Sqrt((first.latitude) - (second.latitude) + (first.longitude) - (second.longitude));
            }

            /// <summary>
            /// Receives the doubles longitude and latitude and returns their IBL.BO.Location
            /// </summary>
            /// <param name="longitude"></param>
            /// <param name="latitude"></param>
            /// <returns></returns>
            public static IBL.BO.Location MakeLocation(double longitude, double latitude)
            {
                IBL.BO.Location l = new();
                l.latitude = latitude;
                l.longitude = longitude;
                return l;
            }

            /// <summary>
            /// Returns closest station to location parameter or 0 if their are no stations or an error
            /// </summary>
            /// <param name="location"></param>
            /// <returns></returns>
            public static IDAL.DO.Station ClosestStation(IBL.BO.Location location )
            {
                //starting minDistance is bigger then highest possible distance, the diagonal between 360 and 180 = 402
                double minDistance = 420;

                List<IDAL.DO.Station>.Enumerator enumerator = DalObject.DataSource.StationList.GetEnumerator();
                int closestStationId = -1;

                while (enumerator.MoveNext())
                {
                    double d = DistanceBetween(location,
                    MakeLocation(enumerator.Current.Latitude, enumerator.Current.Longitude));
                    if (d < minDistance)
                    {
                        d = minDistance;
                        closestStationId = enumerator.Current.Id;
                    }
                }

                return DalObject.DataSource.StationList.Find(x => x.Id == closestStationId);
            }

            /// <summary>
            /// receives weight int, { 1 = light, 2 = medium, 3 = heavy }
            /// and returns the minimum charge required to go the inputed distance
            /// </summary>
            /// <param name="weightClass"></param>
            /// <param name="distance"></param>
            /// <returns></returns>
            public static double ChargeForDistance(IDAL.DO.WeightCategory weight, double distance)
            {
                int weightClass = (int)weight;
                if (weightClass < 0 || weightClass > 2)
                    throw new IBL.BO.MessageException("Error: powerConsumption exceeds bounds");
                //Get specific power of weight class
                double powerPerKm = DalObject.DalObject.GetPowerConsumptions()[weightClass + 1];

                return distance * powerPerKm;
            }       

            /// <summary>
            /// function retunrs how much charge you get for how much time inputed
            /// </summary>
            /// <param name="ChargeTime"></param>
            /// <returns></returns>
            public static double ChargeForTime(DateTime ChargeTime)
            {
                int Hours = ChargeTime.Hour;
                int Minutes = ChargeTime.Minute;
                double Charge = Hours * DalObject.DataSource.GetChargingRate();
                Charge += Minutes * 0.016666;
                return Charge;
            }
        }
    }
}
