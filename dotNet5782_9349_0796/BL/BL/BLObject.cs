using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalObject;


namespace BL
{
    public partial class BL : IBL.IBL
    {
        public partial class BLObject
        {
            public static List<IBL.BO.DroneToList> DroneList = new List<IBL.BO.DroneToList>();

            public BLObject()
            {
                IDAL.IDAL Dal = new DalObject.DalObject();
                var rand = new Random();

                double[] PowerConsumptions = DalObject.DalObject.GetPowerConsumptions();//Returns an array of the power consumptions { Free, Light, Medium, Heavy, ChargingRate }
                double Free = PowerConsumptions[0];
                double Light = PowerConsumptions[1];
                double Medium = PowerConsumptions[2];
                double Heavy = PowerConsumptions[3];
                double ChargingRate = PowerConsumptions[4];

                List<IDAL.DO.Drone> Drones = DataSource.DroneList;
                foreach (IDAL.DO.Drone Drone in Drones)
                {
                    IBL.BO.DroneToList NewDrone = new IBL.BO.DroneToList();
                    NewDrone.Id = Drone.Id;
                    NewDrone.Model = Drone.Model;
                    NewDrone.Weight = Drone.MaxWeight;
                    NewDrone.PackageId = 0;

                    DroneList.Add(NewDrone);
                }

                List<IDAL.DO.Package> deliveredPackages = new();

                //For Drone that is delivering:
                foreach (IDAL.DO.Package Pack in DataSource.PackageList)
                {
                    if(Pack.DroneId != 0 && Pack.Delivered == null)
                    {
                        int index = DroneList.FindIndex(x => x.Id == Pack.DroneId);
                        DroneList[index].DroneStatus = IBL.BO.DroneStatus.delivery;
                        DroneList[index].PackageId = Pack.Id;
                        
                        //Find sender Location
                        IDAL.DO.Customer sender = DataSource.CustomerList.Find(x => x.Id == Pack.SenderId);
                        IBL.BO.Location senderLocation = MakeLocation(sender.Longitude, sender.Latitude);
                        if (Pack.PickedUp == null)
                        {
                            //If package not picked up, Drone at the station closest to the sender
                            IDAL.DO.Station station = ClosestStation(senderLocation);
                            DroneList[index].Location = MakeLocation(station.Longitude, station.Latitude);

                        }
                        else if(Pack.Delivered == null)
                        {
                            //if package has been collected but hasn't been delivered - location of drone will be at sender.
                            DroneList[index].Location = senderLocation;
                        }
                        else
                        {
                            throw new IBL.BO.MessageException("Error: BLObject Constructor: Package has undefined status\n");
                        }

                        //battery state shall be randomized minimum charge for distance between: drone, receiver and then the closest station
                        IDAL.DO.Customer receiver = DataSource.CustomerList.Find(x => x.Id == Pack.ReceiverId);
                        IBL.BO.Location receiverLocation = MakeLocation(receiver.Longitude, receiver.Latitude);
                        IBL.BO.Location closestStationLocation 
                            = MakeLocation(ClosestStation(receiverLocation).Longitude, ClosestStation(receiverLocation).Latitude);
                        double DistanceNeeded = DistanceBetween(DroneList[index].Location, receiverLocation) 
                            + DistanceBetween(receiverLocation, closestStationLocation);
                        double minCharge = ChargeForDistance(DroneList[index].Weight, DistanceNeeded);

                        DroneList[index].BatteryStatus = rand.NextDouble() * (1 - minCharge) + minCharge;
                    }
                    else if (Pack.Delivered != null)
                    {
                        deliveredPackages.Add(Pack);
                    }
                }
                
                //Finally going back through DroneList to deal with Drones not on Delivery
                foreach(IBL.BO.DroneToList DroneL in DroneList)
                {
                    if(DroneL.DroneStatus != IBL.BO.DroneStatus.delivery)
                    {
                        //Drone status shall be available or maintnence (Random) 
                        DroneL.DroneStatus = (IBL.BO.DroneStatus)rand.Next(0, 1);

                        if (DroneL.DroneStatus == IBL.BO.DroneStatus.maintenance)
                        {
                            //Count stations
                            int stationCount = 0;
                            foreach(IDAL.DO.Station s in DataSource.StationList) { stationCount++; }
                            //Get random station 
                            int stationi = rand.Next(0, stationCount);
                            //Drone location is at random location
                            DroneL.Location = MakeLocation(DataSource.StationList[stationi].Longitude, DataSource.StationList[stationi].Latitude);
                            //Battery state between 0 and 0.2
                            DroneL.BatteryStatus = rand.NextDouble() * (0.2);

                        }
                        else if (DroneL.DroneStatus == IBL.BO.DroneStatus.free)
                        {
                            //Avi Weiner - battery state will be between the minimum allows it to get the closest station and 100(random)///////////////////////////

                            //location: random customer that has already received a package
                            int deliveredCount = 0;
                            foreach (IDAL.DO.Package p in deliveredPackages) { deliveredCount++; }
                            IDAL.DO.Customer c = DataSource.CustomerList[rand.Next(0, deliveredCount)];
                            DroneL.Location = MakeLocation(c.Longitude, c.Latitude);

                            //Battery: mimimum to get to closest station
                            IBL.BO.Location l = MakeLocation(ClosestStation(DroneL.Location).Longitude, ClosestStation(DroneL.Location).Latitude);
                            double DistanceNeeded = DistanceBetween(DroneL.Location, l);
                            double minCharge = ChargeForDistance(DroneL.Weight, DistanceNeeded);
                            DroneL.BatteryStatus = rand.NextDouble() * (1 - minCharge) + minCharge;
                        }
                        else
                        {
                            throw new IBL.BO.MessageException("Error: BLObject Constructor: DroneStatus incorrect\n");
                        }

                    }
                }
                


            }
        }
    }
}
