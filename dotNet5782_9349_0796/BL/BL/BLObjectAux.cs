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
    public partial class BL : BlApi.IBL
    {
        public partial class BLObject
        {
            /// <summary>
            /// Returns the distance (double) between 2 Location s
            /// </summary>
            /// <param name="first"></param>
            /// <param name="second"></param>
            /// <returns></returns>
            public static double DistanceBetween(Location first, Location second)
            {
                double x = (first.latitude) - (second.latitude) + (first.longitude) - (second.longitude);
                if (x < 0)
                    x = x * -1;
                return Math.Sqrt(x);
            }

            /// <summary>
            /// Receives the doubles longitude and latitude and returns their Location
            /// </summary>
            /// <param name="longitude"></param>
            /// <param name="latitude"></param>
            /// <returns></returns>
            public static Location MakeLocation(double longitude, double latitude)
            {
                Location l = new();
                l.latitude = latitude;
                l.longitude = longitude;
                return l;
            }

            /// <summary>
            /// Returns closest station to location parameter or 0 if their are no stations or an error
            /// </summary>
            /// <param name="location"></param>
            /// <returns></returns>
            public static DO.Station ClosestStation(Location location )
            {
                List<DO.Station> StationList = BLObject.Dal.GetStationList();
                //starting minDistance is bigger then highest possible distance, the diagonal between 360 and 180 = 402
                double minDistance = 420;

                List<DO.Station>.Enumerator enumerator = StationList.GetEnumerator();
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

                return StationList.Find(x => x.Id == closestStationId);
            }

            /// <summary>
            /// receives weight int, { 1 = light, 2 = medium, 3 = heavy }
            /// and returns the minimum charge required to go the inputed distance
            /// </summary>
            /// <param name="weightClass"></param>
            /// <param name="distance"></param>
            /// <returns></returns>
            public static double ChargeForDistance(DO.WeightCategory weight, double distance)
            {
                int weightClass = (int)weight;
                if (weightClass < 0 || weightClass > 2)
                    throw new MessageException("Error: powerConsumption exceeds bounds");
                //Get specific power of weight class
                double powerPerKm = DalObject.DalObject.GetPowerConsumptions()[weightClass + 1];

                return distance * powerPerKm;
            }       

            /// <summary>
            /// function retunrs how much charge you get for how much time inputed
            /// </summary>
            /// <param name="ChargeTime"></param>
            /// <returns></returns>
            public static double ChargeForTime(TimeSpan ChargeTime)
            {
                double Hours = ChargeTime.TotalHours;
                double Charge = Hours * DalObject.DataSource.GetChargingRate();
                return Charge;
            }
        }
    }
}
