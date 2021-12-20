using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BL : BlApi.IBL
    {
        /// <summary>
        /// sends drone to charge 
        /// if the id is not valid or the drone is not free drone exception will be thrown
        /// drone will be updated and station chargers will be updated.
        /// </summary>
        /// <param name="DroneId"></param>
        public void SendDroneToCharge(int DroneId)
        {
            #region Input Checking
            int DroneIndex = BLObject.BLDroneList.FindIndex(x => x.Id == DroneId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (DroneIndex == -1)
            {
                throw new MessageException("Error: Drone not found.\n");
            }
            if(BLObject.BLDroneList[DroneIndex].DroneStatus != DroneStatus.free)
            {
                throw new MessageException("Error: Drone is not free.\n");
            }

            DO.Station StationClose = BLObject.ClosestStation(BLObject.BLDroneList[DroneIndex].Location);

            if (BLObject.ChargeForDistance(BLObject.BLDroneList[DroneIndex].Weight, 
                BLObject.DistanceBetween(BLObject.BLDroneList[DroneIndex].Location, BLObject.MakeLocation(StationClose.Longitude, StationClose.Latitude)))
                > BLObject.BLDroneList[DroneIndex].BatteryStatus)
            {
                throw new MessageException("Error: Not enough charge.\n");
            }

            if(StationClose.ChargeSlots<=0)
            {
                throw new MessageException("Error: not enough charging slots");
            }
            #endregion

            //update battery state 
            BLObject.BLDroneList[DroneIndex].BatteryStatus -= 
                BLObject.ChargeForDistance(BLObject.BLDroneList[DroneIndex].Weight, 
                BLObject.DistanceBetween(BLObject.BLDroneList[DroneIndex].Location, BLObject.MakeLocation(StationClose.Longitude, StationClose.Latitude)));
            BLObject.BLDroneList[DroneIndex].Location = BLObject.MakeLocation(StationClose.Longitude, StationClose.Latitude);
            BLObject.BLDroneList[DroneIndex].DroneStatus = DroneStatus.maintenance;
            int StationIndex = DalObject.DataSource.StationList.FindIndex(x => x.Id == StationClose.Id);
            StationClose.ChargeSlots -= 1;
            DalObject.DataSource.StationList[StationIndex] = StationClose;
            ///iii adding a mathcing instance///////////////////////////////////////////////////////////////////////////////////////
        }




        /// <summary>
        /// drone will be released from charging station and appropriate battery will be added
        /// location will be the statoin where it was charged.
        /// </summary>
        /// <param name="DroneId"></param>
        /// <param name="ChargeTime"></param>
        public void ReleaseDroneFromCharge(int DroneId, DateTime ChargeTime)//supposed to take in a time not sure whatit's supposed to do so skipped it for now.
        {

            int DroneIndex = BLObject.BLDroneList.FindIndex(x => x.Id == DroneId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (DroneIndex == -1)
            {
                throw new MessageException("Error: Drone not found.\n");
            }
            if (BLObject.BLDroneList[DroneIndex].DroneStatus != DroneStatus.maintenance)
            {
                throw new MessageException("Error: Drone is not in maintenance.\n");
            }

            double Charge = BLObject.BLDroneList[DroneIndex].BatteryStatus;
            Charge += BLObject.ChargeForTime(ChargeTime);
            if (Charge > 1)
                Charge = 1;
            BLObject.BLDroneList[DroneIndex].BatteryStatus = Charge;
            BLObject.BLDroneList[DroneIndex].DroneStatus = DroneStatus.free;
            DO.Station StationClose = BLObject.ClosestStation(BLObject.BLDroneList[DroneIndex].Location);

            int StationIndex = DalObject.DataSource.StationList.FindIndex(x => x.Id == StationClose.Id);
            DO.Station station = DalObject.DataSource.StationList[StationIndex];
            station.ChargeSlots++;
            DalObject.DataSource.StationList[StationIndex] = station;
            //again not sure what the mathcing instance is.
        
        }

        public bool CheckCloseEnough(DO.Package pack, int id)
        {
            DO.Customer customerSender = DalObject.DataSource.CustomerList[DalObject.DataSource.CustomerList.FindIndex(x => x.Id == pack.SenderId)];
            DO.Customer customerReciever = DalObject.DataSource.CustomerList[DalObject.DataSource.CustomerList.FindIndex(x => x.Id == pack.ReceiverId)];
            Location senderLocation = BLObject.MakeLocation(customerSender.Longitude, customerSender.Latitude);
            Location recieverLocation = BLObject.MakeLocation(customerReciever.Longitude, customerReciever.Latitude);

            if (BLObject.BLDroneList[id].BatteryStatus < BLObject.ChargeForDistance(pack.Weight, BLObject.DistanceBetween(senderLocation, recieverLocation)))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// if input is valid the heviest closest package will be assgined to the drone
        /// if anything goes wrong appropriate exception will be thrown.
        /// </summary>
        /// <param name="DroneId"></param>
        public void AssignPackageToDrone(int DroneId)
        {
            int DroneIndex = BLObject.BLDroneList.FindIndex(x => x.Id == DroneId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (DroneIndex == -1)
            {
                throw new MessageException("Error: Drone not found.\n");
            }
            if (BLObject.BLDroneList[DroneIndex].DroneStatus != DroneStatus.free)
            {
                throw new MessageException("Error: Drone is not free.\n");
            }
            List<DO.Package> Packages = DalObject.DataSource.PackageList;
            List<DO.Package> tempPack = new List<DO.Package>();
            Packages.RemoveAll(x => x.Delivered != null);

            Packages.RemoveAll(x => CheckCloseEnough(x, DroneIndex));
            
            int PackIndex = Packages.FindIndex(x => x.Priority == DO.Priority.emergency);
            if (PackIndex != -1)
            {
                Packages.RemoveAll(x => x.Priority != DO.Priority.emergency);
            }
            else
            {
                PackIndex = Packages.FindIndex(x => x.Priority == DO.Priority.fast);
                if(PackIndex != -1)
                {
                    Packages.RemoveAll(x => x.Priority != DO.Priority.fast);
                   
                }
            }
            

            DroneToList drone = BLObject.BLDroneList[DroneIndex];
            if(Packages == null)
            {
                throw new MessageException("Error: Drone can't take any Package.\n");
            }
            drone.PackageId = Packages[0].Id;
            foreach(DO.Package pack in Packages)
            {
                DO.Customer customerSender = DalObject.DataSource.CustomerList[DalObject.DataSource.CustomerList.FindIndex(x => x.Id == pack.SenderId)];
                Location senderLocation = BLObject.MakeLocation(customerSender.Longitude, customerSender.Latitude);
                DO.Package package = Packages.Find(x => x.Id == drone.PackageId);
                DO.Customer thisPackageSender = DalObject.DataSource.CustomerList[DalObject.DataSource.CustomerList.FindIndex(x => x.Id == package.SenderId)];
                Location thisSenderLocation = BLObject.MakeLocation(thisPackageSender.Longitude, thisPackageSender.Latitude);
                if (BLObject.DistanceBetween(senderLocation, drone.Location) < BLObject.DistanceBetween(thisSenderLocation, drone.Location))
                {
                    drone.PackageId = pack.Id;
                }
            }
            drone.DroneStatus = DroneStatus.delivery;
            DO.Package  finalPackage = Packages.Find(x => x.Id == drone.PackageId);
            finalPackage.DroneId = drone.Id;
            finalPackage.Scheduled = DateTime.Now;
            int finalPackageIndex = DalObject.DataSource.PackageList.FindIndex(x => x.Id == drone.PackageId);
            DalObject.DataSource.PackageList[finalPackageIndex] = finalPackage;
            BLObject.BLDroneList[DroneIndex] = drone;
        }
        
        /// <summary>
        /// if a package was assinged to a drone the drone will be sent to collect the package it was assigned
        /// and collect the package. appropriate battery percentage will drop.
        /// otherwise an exception will be thrown.
        /// </summary>
        /// <param name="DroneId"></param>
        public void DroneCollectsAPackage(int DroneId)
        {
            int DroneIndex = BLObject.BLDroneList.FindIndex(x => x.Id == DroneId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (DroneIndex == -1)
            {
                throw new MessageException("Error: Drone not found.\n");
            }
            if (BLObject.BLDroneList[DroneIndex].DroneStatus != DroneStatus.delivery)
            {
                throw new MessageException("Error: Drone is not in delivery.\n");
            }
            DroneToList Drone = BLObject.BLDroneList[DroneIndex];
            int PackageIndex = DalObject.DataSource.PackageList.FindIndex(x => x.Id == Drone.PackageId);
            DO.Package Package = DalObject.DataSource.PackageList[PackageIndex];
            if(Package.PickedUp != null)
            {
                throw new MessageException("Error: Package was picked up already.\n");
            }
            DO.Customer Sender = DalObject.DataSource.CustomerList.Find(x => x.Id == Package.SenderId);
            Location SenderLocation = BLObject.MakeLocation(Sender.Longitude, Sender.Latitude);
            double DistanceBetween = BLObject.DistanceBetween(SenderLocation, Drone.Location);
            Drone.BatteryStatus -= BLObject.ChargeForDistance(Package.Weight, DistanceBetween);
            Drone.Location = SenderLocation;
            Package.PickedUp = DateTime.Now;
            BLObject.BLDroneList[DroneIndex] = Drone;
            DalObject.DataSource.PackageList[PackageIndex] = Package;
        }

        /// <summary>
        /// If the drone picked up a package the package will be delivered the new location will be 
        /// the recievers location and the battery percentage will decrese approriately 
        /// if the drone doesn't have a package appropriate message will be thrown.
        /// </summary>
        /// <param name="DroneId"></param>
        public void DroneDeliversPakcage(int DroneId)
        {
            int DroneIndex = BLObject.BLDroneList.FindIndex(x => x.Id == DroneId);
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (DroneIndex == -1)
            {
                throw new MessageException("Error: Drone not found.\n");
            }
            if (BLObject.BLDroneList[DroneIndex].DroneStatus != DroneStatus.delivery)
            {
                throw new MessageException("Error: Drone is not in delivery.\n");
            }
            DroneToList Drone = BLObject.BLDroneList[DroneIndex];
            int PackageIndex = DalObject.DataSource.PackageList.FindIndex(x => x.Id == Drone.PackageId);
            DO.Package Package = DalObject.DataSource.PackageList[PackageIndex];
            if(Package.Delivered != null)
            {
                throw new MessageException("Error: Package was delivered already");
            }
            DO.Customer Sender = DalObject.DataSource.CustomerList.Find(x => x.Id == Package.SenderId);
            Location SenderLocation = BLObject.MakeLocation(Sender.Longitude, Sender.Latitude);
            DO.Customer Reciever = DalObject.DataSource.CustomerList.Find(x => x.Id == Package.ReceiverId);
            Location RecieverLocation = BLObject.MakeLocation(Reciever.Longitude, Sender.Latitude);
            Drone.BatteryStatus -= BLObject.ChargeForDistance(Package.Weight, BLObject.DistanceBetween(SenderLocation, RecieverLocation));
            Drone.Location = RecieverLocation;
            Drone.DroneStatus = DroneStatus.free;
            Package.Delivered = DateTime.Now;
            BLObject.BLDroneList[DroneIndex] = Drone;
            DalObject.DataSource.PackageList[PackageIndex] = Package;
        }
    }
}
