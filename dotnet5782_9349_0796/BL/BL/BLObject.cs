using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;
using System.Runtime.CompilerServices;

namespace BL
{
    public partial class BL : BlApi.IBL
    {
        /// <summary>
        /// ThisBL is a singleton BL
        /// </summary>
        static BL ThisBL = null;

        public partial class BLObject
        {
            public static List<DroneToList> BLDroneList = new List<DroneToList>();
            public static DalApi.IDAL Dal;

            /// <summary>
            /// BLObject constructor
            /// </summary>
            public BLObject()
            {
                Dal = DAL.DalFactory.GetDal("DalXml");
                var rand = new Random();
                List<DO.Drone> DroneList = BLObject.Dal.GetDroneList();

                double[] PowerConsumptions = DalObject.DalObject.GetPowerConsumptions();//Returns an array of the power consumptions { Free, Light, Medium, Heavy, ChargingRate }
                double Free = PowerConsumptions[0];
                double Light = PowerConsumptions[1];
                double Medium = PowerConsumptions[2];
                double Heavy = PowerConsumptions[3];
                double ChargingRate = PowerConsumptions[4];

                List<DO.Drone> Drones = DroneList;
                foreach (DO.Drone Drone in Drones)
                {
                    DroneToList NewDrone = new DroneToList();
                    NewDrone.Id = Drone.Id;
                    NewDrone.Model = Drone.Model;
                    NewDrone.Weight = Drone.MaxWeight;
                    NewDrone.PackageId = 0;

                    BLDroneList.Add(NewDrone);
                }

                List<DO.Package> deliveredPackages = new();
                List<DO.Customer> Customers = BLObject.Dal.GetCustomerList();
                List<DO.Package> PackageList = BLObject.Dal.GetPackageList();
                //For Drone that is delivering:
                foreach (DO.Package Pack in PackageList)
                {
                    if(Pack.DroneId != 0 && Pack.Delivered == null)
                    {
                        int index = BLDroneList.FindIndex(x => x.Id == Pack.DroneId);
                        BLDroneList[index].DroneStatus = DroneStatus.delivery;
                        BLDroneList[index].PackageId = Pack.Id;
                        
                        //Find sender Location
                        DO.Customer sender = Customers.Find(x => x.Id == Pack.SenderId);
                        Location senderLocation = MakeLocation(sender.Longitude, sender.Latitude);
                        if (Pack.PickedUp == null)
                        {
                            //If package not picked up, Drone at the station closest to the sender
                            DO.Station station = ClosestStation(senderLocation);
                            BLDroneList[index].Location = MakeLocation(station.Longitude, station.Latitude);

                        }
                        else if(Pack.Delivered == null)
                        {
                            //if package has been collected but hasn't been delivered - location of drone will be at sender.
                            BLDroneList[index].Location = senderLocation;
                        }
                        else
                        {
                            throw new MessageException("Error: BLObject Constructor: Package has undefined status\n");
                        }

                        //battery state shall be randomized minimum charge for distance between: drone, receiver and then the closest station
                        DO.Customer receiver = Customers.Find(x => x.Id == Pack.ReceiverId);
                        Location receiverLocation = MakeLocation(receiver.Longitude, receiver.Latitude);
                        Location closestStationLocation 
                            = MakeLocation(ClosestStation(receiverLocation).Longitude, ClosestStation(receiverLocation).Latitude);
                        double DistanceNeeded = DistanceBetween(BLDroneList[index].Location, receiverLocation) 
                            + DistanceBetween(receiverLocation, closestStationLocation);
                        double minCharge = ChargeForDistance(BLDroneList[index].Weight, DistanceNeeded);

                        BLDroneList[index].BatteryStatus = rand.NextDouble() * (1 - minCharge) + minCharge;
                    }
                    else if (Pack.Delivered != null)
                    {
                        deliveredPackages.Add(Pack);
                    }
                }
                
                //Finally going back through DroneList to deal with Drones not on Delivery
                foreach(DroneToList DroneL in BLDroneList)
                {
                    if(DroneL.DroneStatus != DroneStatus.delivery)
                    {
                        //Drone status shall be available or maintnence (Random) 
                        DroneL.DroneStatus = (DroneStatus)rand.Next(0, 1);

                        if (DroneL.DroneStatus == DroneStatus.maintenance)
                        {
                            //Count stations
                            int stationCount = 0;
                            List<DO.Station> StationList = BLObject.Dal.GetStationList();
                            foreach (DO.Station s in StationList) { stationCount++; }
                            //Get random station 
                            int stationi = rand.Next(0, stationCount);
                            //Drone location is at random location
                            DroneL.Location = MakeLocation(StationList[stationi].Longitude, StationList[stationi].Latitude);
                            //Battery state between 0 and 0.2
                            DroneL.BatteryStatus = rand.NextDouble() * (0.2);

                        }
                        else if (DroneL.DroneStatus == DroneStatus.free)
                        {
                            //location: random customer that has already received a package
                            //List<DO.Customer> CustomersThatReceived = Dal.GetCustomerList();
                            //int deliveredCount = 0;
                            //foreach (DO.Package p in deliveredPackages) { deliveredCount++; }
                            DO.Customer c = Customers[rand.Next(0, Customers.Count)];
                            DroneL.Location = MakeLocation(c.Longitude, c.Latitude);

                            //Battery: mimimum to get to closest station
                            Location l = MakeLocation(ClosestStation(DroneL.Location).Longitude, ClosestStation(DroneL.Location).Latitude);
                            double DistanceNeeded = DistanceBetween(DroneL.Location, l);
                            double minCharge = ChargeForDistance(DroneL.Weight, DistanceNeeded);
                            DroneL.BatteryStatus = rand.NextDouble() * (1 - minCharge) + minCharge;
                        }
                        else
                        {
                            throw new MessageException("Error: BLObject Constructor: DroneStatus incorrect\n");
                        }

                    }//if(DroneL.DroneStatus != BlApi.DroneStatus.delivery)
                }//end of foreach
            }

            /// <summary>
            /// Selects and returns the first available drone,
            /// </summary>
            /// <returns></returns>
            public static DroneToList SelectDrone()
            {
                //Check if there is an available drone
                int DroneIndex = BLDroneList.FindIndex(d => d.DroneStatus == DroneStatus.free);
                //if findIndex returned -1 then there are no available drones. Error Will be thrown.
                if (DroneIndex == -1)
                {
                    throw new MessageException("Error: No available drones.\n");
                }

                return BLDroneList.Find(d => d.DroneStatus == DroneStatus.free);
            }
        }//Class BLObject

        /// <summary>
        /// BL Constructor
        /// </summary>
        private BL()
        {
            BLObject Object = new BLObject();
        }

        /// <summary>
        /// if BL was already intialized the object already there will be returned 
        /// otherwise a new one will be created.
        /// </summary>
        /// <returns></returns>
        public static BL GetBLObject()
        {
            if (ThisBL == null)
            {
                ThisBL = new BL();
            }

            return ThisBL;
        }


    }//Class BL
}//namespace BL
