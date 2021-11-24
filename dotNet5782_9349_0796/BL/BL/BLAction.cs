using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : IBL.IBL
    {
        /// <summary>
        /// sends drone to charge 
        /// if the id is not valid or the drone is not free drone exception will be thrown
        /// drone will be updated and station chargers will be updated.
        /// </summary>
        /// <param name="DroneId"></param>
        public void SendDroneToCharge(int DroneId)
        {
            int DroneIndex = BLObject.DroneList.FindIndex(x => x.Id == DroneId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (DroneIndex == -1)
            {
                throw new IBL.BO.MessageException("Error: Drone not found.\n");
            }
            if(BL.BLObject.DroneList[DroneIndex].DroneStatus != IBL.BO.DroneStatus.free)
            {
                throw new IBL.BO.MessageException("Error: Drone is not free.\n");
            }

            IDAL.DO.Station StationClose = BLObject.ClosestStation(BL.BLObject.DroneList[DroneIndex].Location);

            if (BLObject.ChargeForDistance(BL.BLObject.DroneList[DroneIndex].Weight, 
                BLObject.DistanceBetween(BL.BLObject.DroneList[DroneIndex].Location, BLObject.MakeLocation(StationClose.Longitude, StationClose.Latitude)))
                > BL.BLObject.DroneList[DroneIndex].BatteryStatus)
            {
                throw new IBL.BO.MessageException("Error: Not enough charge.\n");
            }

            if(StationClose.ChargeSlots<=0)
            {
                throw new IBL.BO.MessageException("Error: not enough charging slots");
            }
            //update battery state 
            BL.BLObject.DroneList[DroneIndex].BatteryStatus -= 
                BLObject.ChargeForDistance(BL.BLObject.DroneList[DroneIndex].Weight, 
                BLObject.DistanceBetween(BL.BLObject.DroneList[DroneIndex].Location, BLObject.MakeLocation(StationClose.Longitude, StationClose.Latitude)));
            BL.BLObject.DroneList[DroneIndex].Location = BLObject.MakeLocation(StationClose.Longitude, StationClose.Latitude);
            BL.BLObject.DroneList[DroneIndex].DroneStatus = IBL.BO.DroneStatus.maintenance;
            int StationIndex = DalObject.DataSource.StationList.FindIndex(x => x.Id == StationClose.Id);
            StationClose.ChargeSlots -= 1;
            DalObject.DataSource.StationList[StationIndex] = StationClose;
            ///iii adding a mathcing instance///////////////////////////////////////////////////////////////////////////////////////
        }
        /// <summary>
        /// drone will be released from charging station and appropriate battery will be added
        /// 
        /// </summary>
        /// <param name="DroneId"></param>
        /// <param name="ChargeTime"></param>
        public void ReleaseDroneFromCharge(int DroneId, DateTime ChargeTime)//supposed to take in a time not sure whatit's supposed to do so skipped it for now.
        {
            int DroneIndex = BLObject.DroneList.FindIndex(x => x.Id == DroneId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (DroneIndex == -1)
            {
                throw new IBL.BO.MessageException("Error: Drone not found.\n");
            }
            if (BL.BLObject.DroneList[DroneIndex].DroneStatus != IBL.BO.DroneStatus.maintenance)
            {
                throw new IBL.BO.MessageException("Error: Drone is not in maintenance.\n");
            }

            double Charge = BLObject.DroneList[DroneIndex].BatteryStatus;
            Charge += BLObject.ChargeForTime(ChargeTime);
            if (Charge > 1)
                Charge = 1;
            BLObject.DroneList[DroneIndex].BatteryStatus = Charge;
            BLObject.DroneList[DroneIndex].DroneStatus = IBL.BO.DroneStatus.free;
            IDAL.DO.Station StationClose = BLObject.ClosestStation(BL.BLObject.DroneList[DroneIndex].Location);

            int StationIndex = DalObject.DataSource.StationList.FindIndex(x => x.Id == StationClose.Id);
            IDAL.DO.Station station = DalObject.DataSource.StationList[StationIndex];
            station.ChargeSlots++;
            DalObject.DataSource.StationList[StationIndex] = station;
            //again not sure what the mathcing instance is.
        
        }

        public void AssignPackageToDrone(int DroneId)
        {
            int DroneIndex = BLObject.DroneList.FindIndex(x => x.Id == DroneId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (DroneIndex == -1)
            {
                throw new IBL.BO.MessageException("Error: Drone not found.\n");
            }
            if (BL.BLObject.DroneList[DroneIndex].DroneStatus != IBL.BO.DroneStatus.free)
            {
                throw new IBL.BO.MessageException("Error: Drone is not free.\n");
            }
            List<IDAL.DO.Package> Packages = DalObject.DataSource.PackageList;

            foreach(IDAL.DO.Package pack in Packages)
            {
                //drone can reach sender and deliver to the reciever and make it to the nearest
                //charging station if() not delete the packages.
                IDAL.DO.Customer customerSender = DalObject.DataSource.CustomerList[DalObject.DataSource.CustomerList.FindIndex(x => x.Id == pack.SenderId)];
                IDAL.DO.Customer customerReciever = DalObject.DataSource.CustomerList[DalObject.DataSource.CustomerList.FindIndex(x => x.Id == pack.ReceiverId)];
                IBL.BO.Location senderLocation = BLObject.MakeLocation(customerSender.Longitude, customerSender.Latitude);
                IBL.BO.Location recieverLocation = BLObject.MakeLocation(customerReciever.Longitude, customerReciever.Latitude);

                if (BL.BLObject.DroneList[DroneIndex].BatteryStatus < BLObject.ChargeForDistance(pack.Weight, BLObject.DistanceBetween(senderLocation, recieverLocation)))
                {
                    Packages.Remove(pack);
                }
            }

            int PackIndex = Packages.FindIndex(x => x.Priority == IDAL.DO.Priority.emergency);
            if (PackIndex != -1)
            {
                foreach (IDAL.DO.Package pack in Packages)
                {
                    if(pack.Priority != IDAL.DO.Priority.emergency)
                    {
                        Packages.Remove(pack);
                    }

                }//remove all non emergency this is if there are available emergency
            }
            else
            {
                PackIndex = Packages.FindIndex(x => x.Priority == IDAL.DO.Priority.fast);
                if(PackIndex != -1)
                {
                    foreach(IDAL.DO.Package pack in Packages)
                    {
                        if(pack.Priority != IDAL.DO.Priority.fast)
                        {
                            Packages.Remove(pack);
                        }
                    }//end fore each to remove all not fast packs is becasue there are no faster ones
                }
            }

            IBL.BO.DroneToList drone = BL.BLObject.DroneList[DroneIndex];
            if(Packages == null)
            {
                throw new IBL.BO.MessageException("Error: Drone can't take any Package.\n");
            }
            drone.PackageId = Packages[0].Id;
            foreach(IDAL.DO.Package pack in Packages)
            {
                IDAL.DO.Customer customerSender = DalObject.DataSource.CustomerList[DalObject.DataSource.CustomerList.FindIndex(x => x.Id == pack.SenderId)];
                IBL.BO.Location senderLocation = BLObject.MakeLocation(customerSender.Longitude, customerSender.Latitude);
                IDAL.DO.Package package = Packages.Find(x => x.Id == drone.PackageId);
                IDAL.DO.Customer thisPackageSender = DalObject.DataSource.CustomerList[DalObject.DataSource.CustomerList.FindIndex(x => x.Id == package.SenderId)];
                IBL.BO.Location thisSenderLocation = BLObject.MakeLocation(thisPackageSender.Longitude, thisPackageSender.Latitude);
                if (BLObject.DistanceBetween(senderLocation, drone.Location) < BLObject.DistanceBetween(thisSenderLocation, drone.Location)) ;
                {
                    drone.PackageId = pack.Id;
                }
            }
            drone.DroneStatus = IBL.BO.DroneStatus.delivery;
            IDAL.DO.Package  finalPackage = Packages.Find(x => x.Id == drone.PackageId);
            finalPackage.DroneId = drone.Id;
            finalPackage.Scheduled = DateTime.Now;
            int finalPackageIndex = DalObject.DataSource.PackageList.FindIndex(x => x.Id == drone.PackageId);
            DalObject.DataSource.PackageList[finalPackageIndex] = finalPackage;
            BLObject.DroneList[DroneIndex] = drone;
        }
        

        public void DroneCollectsAPackage(int DroneId)
        {
            int DroneIndex = BLObject.DroneList.FindIndex(x => x.Id == DroneId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (DroneIndex == -1)
            {
                throw new IBL.BO.MessageException("Error: Drone not found.\n");
            }
            if (BL.BLObject.DroneList[DroneIndex].DroneStatus != IBL.BO.DroneStatus.delivery)
            {
                throw new IBL.BO.MessageException("Error: Drone is not in delivery.\n");
            }
            IBL.BO.DroneToList Drone = BLObject.DroneList[DroneId];
            int PackageIndex = DalObject.DataSource.PackageList.FindIndex(x => x.Id == Drone.PackageId);
            IDAL.DO.Package Package = DalObject.DataSource.PackageList[PackageIndex];
            if(Package.PickedUp != null)
            {
                throw new IBL.BO.MessageException("Error: Package was picked up already.\n");
            }
            IDAL.DO.Customer Sender = DalObject.DataSource.CustomerList.Find(x => x.Id == Package.SenderId);
            IBL.BO.Location SenderLocation = BLObject.MakeLocation(Sender.Longitude, Sender.Latitude);
            double DistanceBetween = BLObject.DistanceBetween(SenderLocation, Drone.Location);
            Drone.BatteryStatus -= BLObject.ChargeForDistance(Package.Weight, DistanceBetween);
            Drone.Location = SenderLocation;
            Package.PickedUp = DateTime.Now;
            BLObject.DroneList[DroneIndex] = Drone;
            DalObject.DataSource.PackageList[PackageIndex] = Package;
        }

        public void DroneDeliversPakcage(int DroneId)
        {
            int DroneIndex = BLObject.DroneList.FindIndex(x => x.Id == DroneId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (DroneIndex == -1)
            {
                throw new IBL.BO.MessageException("Error: Drone not found.\n");
            }
            if (BL.BLObject.DroneList[DroneIndex].DroneStatus != IBL.BO.DroneStatus.delivery)
            {
                throw new IBL.BO.MessageException("Error: Drone is not in delivery.\n");
            }
            IBL.BO.DroneToList Drone = BLObject.DroneList[DroneId];
            int PackageIndex = DalObject.DataSource.PackageList.FindIndex(x => x.Id == Drone.PackageId);
            IDAL.DO.Package Package = DalObject.DataSource.PackageList[PackageIndex];
            if(Package.Delivered != null)
            {
                throw new IBL.BO.MessageException("Error: Package was delivered already");
            }
            IDAL.DO.Customer Sender = DalObject.DataSource.CustomerList.Find(x => x.Id == Package.SenderId);
            IBL.BO.Location SenderLocation = BLObject.MakeLocation(Sender.Longitude, Sender.Latitude);
            IDAL.DO.Customer Reciever = DalObject.DataSource.CustomerList.Find(x => x.Id == Package.ReceiverId);
            IBL.BO.Location RecieverLocation = BLObject.MakeLocation(Reciever.Longitude, Sender.Latitude);
            Drone.BatteryStatus -= BLObject.ChargeForDistance(Package.Weight, BLObject.DistanceBetween(SenderLocation, RecieverLocation));
            Drone.Location = RecieverLocation;
            Drone.DroneStatus = IBL.BO.DroneStatus.free;
            Package.Delivered = DateTime.Now;
            BLObject.DroneList[DroneIndex] = Drone;
            DalObject.DataSource.PackageList[PackageIndex] = Package;
        }
    }
}
