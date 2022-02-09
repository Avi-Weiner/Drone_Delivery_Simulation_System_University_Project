using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.CompilerServices;

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
        [MethodImpl(MethodImplOptions.Synchronized)]
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

            if (BLObject.ChargeForDistance(DO.WeightCategory.light, 
                BLObject.DistanceBetween(BLObject.BLDroneList[DroneIndex].Location, BLObject.MakeLocation(StationClose.Longitude, StationClose.Latitude)))
                > BLObject.BLDroneList[DroneIndex].BatteryStatus)
            {
            
                throw new MessageException("Error: Not enough charge.\n");
            }

            if(StationClose.ChargeSlots<=0)
            {
                throw new MessageException("Error: not enough charging slots in closest station.\n");
            }
            #endregion

            lock (BLObject.Dal)
            {
                //update battery state 
                BLObject.BLDroneList[DroneIndex].BatteryStatus -= 
                    BLObject.ChargeForDistance(BLObject.BLDroneList[DroneIndex].Weight, 
                    BLObject.DistanceBetween(BLObject.BLDroneList[DroneIndex].Location, BLObject.MakeLocation(StationClose.Longitude, StationClose.Latitude)));
                BLObject.BLDroneList[DroneIndex].Location = BLObject.MakeLocation(StationClose.Longitude, StationClose.Latitude);
                BLObject.BLDroneList[DroneIndex].DroneStatus = DroneStatus.maintenance;
                List<DO.Station> StationList = BLObject.Dal.GetStationList();
                int StationIndex = StationList.FindIndex(x => x.Id == StationClose.Id);
                StationClose.ChargeSlots -= 1;
                StationList[StationIndex] = StationClose;
                BLObject.Dal.SetStationList(StationList);
                BLObject.BLDroneList[DroneIndex].ChargingTimeStarted = DateTime.Now;
            }
        }

        /// <summary>
        /// drone will be released from charging station and appropriate battery will be added
        /// location will be the statoin where it was charged.
        /// </summary>
        /// <param name="DroneId"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void ReleaseDroneFromCharge(int DroneId)
        {
            DateTime ReleaseTime = DateTime.Now;
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
            DateTime StartingTime = (DateTime)BLObject.BLDroneList[DroneIndex].ChargingTimeStarted;
            TimeSpan ChargeTime = ReleaseTime - StartingTime;

            lock (BLObject.Dal)
            {
                double Charge = BLObject.BLDroneList[DroneIndex].BatteryStatus;
                Charge += BLObject.ChargeForTime(ChargeTime);
                if (Charge > 1)
                    Charge = 1;
                BLObject.BLDroneList[DroneIndex].BatteryStatus = Charge;
                BLObject.BLDroneList[DroneIndex].DroneStatus = DroneStatus.free;
                BLObject.BLDroneList[DroneIndex].ChargingTimeStarted = null;
                DO.Station StationClose = BLObject.ClosestStation(BLObject.BLDroneList[DroneIndex].Location);
                List<DO.Station> StationList = BLObject.Dal.GetStationList();
                int StationIndex = StationList.FindIndex(x => x.Id == StationClose.Id);
                DO.Station station = StationList[StationIndex];
                station.ChargeSlots++;

                //set back updated station.
                StationList[StationIndex] = station;
                BLObject.Dal.SetStationList(StationList);
            }
        }

        /// <summary>
        /// Checks if the drone with the given ID has enough battery to drop off receive and deliver the
        /// given package and return to the closest base station.
        /// returns true if its not close enough
        /// </summary>
        /// <param name="pack"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool CheckCloseEnough(DO.Package pack, int id)
        {
            lock (BLObject.Dal)
            {
                DO.Customer customerSender = BLObject.Dal.GetCustomerList().Find(x => x.Id == pack.SenderId);//changed from findIndex to Find...
                DO.Customer customerReciever = BLObject.Dal.GetCustomerList().Find(x => x.Id == pack.ReceiverId);
                Location senderLocation = BLObject.MakeLocation(customerSender.Longitude, customerSender.Latitude);
                Location recieverLocation = BLObject.MakeLocation(customerReciever.Longitude, customerReciever.Latitude);
                Location ClosesetStation = BLObject.MakeLocation(BLObject.ClosestStation(recieverLocation).Longitude, BLObject.ClosestStation(recieverLocation).Latitude);
                double DistanceBetweenDroneAndPackage = BLObject.DistanceBetween(senderLocation, BLObject.BLDroneList[id].Location);
                if (BLObject.BLDroneList[id].BatteryStatus < BLObject.ChargeForDistance(DO.WeightCategory.light, DistanceBetweenDroneAndPackage) + BLObject.ChargeForDistance(pack.Weight, BLObject.DistanceBetween(senderLocation, recieverLocation)) +
                    BLObject.ChargeForDistance(DO.WeightCategory.light, BLObject.DistanceBetween(recieverLocation, ClosesetStation)))
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// if input is valid the heaviest, closest package will be assgined to the drone
        /// if anything goes wrong, appropriate exception will be thrown.
        /// </summary>
        /// <param name="DroneId"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AssignPackageToDrone(int DroneId)
        {
            #region Check basic validity
            int DroneIndex = BLObject.BLDroneList.FindIndex(x => x.Id == DroneId);
            List<DO.Package> PackageList = BLObject.Dal.GetPackageList();
            //if findIndex returned -1 then the drone does not exist. Error Will be thrown.
            if (DroneIndex == -1)
            {
                throw new MessageException("Error: Drone not found.\n");
            }
            if (BLObject.BLDroneList[DroneIndex].DroneStatus != DroneStatus.free)
            {
                throw new MessageException("Error: Drone is not free.\n");
            }
            if(PackageList.Count == 0)
            {
                throw new MessageException("Error: No packages to be collected.\n");
            }
            #endregion

            #region prioritise package selection
            List<DO.Package> Packages = PackageList;
            //List<DO.Package> tempPack = new List<DO.Package>();
            Packages.RemoveAll(x => x.Delivered != null);
            Packages.RemoveAll(x => x.Weight > BLObject.BLDroneList[DroneIndex].Weight);
            Packages.RemoveAll(x => CheckCloseEnough(x, DroneIndex));
            if(Packages.Count == 0)
            {
                throw new MessageException("Error: No packages close enough with current charge.\n");
            }
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
            if(Packages.Count == 0)
            {
                throw new MessageException("Error: Drone can't take any Package.\n");
            }
            #endregion

            lock (BLObject.Dal)
            {
                //Choose prioritised package
                drone.PackageId = Packages[0].Id;

                drone.DroneStatus = DroneStatus.delivery;
                DO.Package finalPackage = Packages.Find(x => x.Id == drone.PackageId);
                finalPackage.DroneId = drone.Id;
                finalPackage.Scheduled = DateTime.Now;

                List<DO.Package> PackageList1 = BLObject.Dal.GetPackageList();
                int finalPackageIndex = PackageList1.FindIndex(x => x.Id == drone.PackageId);
                PackageList1[finalPackageIndex] = finalPackage;

                BLObject.BLDroneList[DroneIndex] = drone;
                BLObject.Dal.SetPackageList(PackageList1);
            }
        }

        /// <summary>
        /// if a package was assinged to a drone the drone will be sent to collect the package it was assigned
        /// and collect the package. appropriate battery percentage will drop.
        /// otherwise an exception will be thrown.
        /// </summary>
        /// <param name="DroneId"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DroneCollectsAPackage(int DroneId)
        {
            int DroneIndex = BLObject.BLDroneList.FindIndex(x => x.Id == DroneId);
            List<DO.Package> PackageList = BLObject.Dal.GetPackageList();
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
            int PackageIndex = PackageList.FindIndex(x => x.Id == Drone.PackageId);
            DO.Package Package = PackageList[PackageIndex];
            if(Package.PickedUp != null)
            {
                throw new MessageException("Error: Package was picked up already.\n");
            }

            lock (BLObject.Dal)
            {
                DO.Customer Sender = DalObject.DataSource.CustomerList.Find(x => x.Id == Package.SenderId);
                Location SenderLocation = BLObject.MakeLocation(Sender.Longitude, Sender.Latitude);
                double DistanceBetween = BLObject.DistanceBetween(SenderLocation, Drone.Location);
                Drone.BatteryStatus -= BLObject.ChargeForDistance(Package.Weight, DistanceBetween);
                Drone.Location = SenderLocation;
                Package.PickedUp = DateTime.Now;
                BLObject.BLDroneList[DroneIndex] = Drone;
                PackageList[PackageIndex] = Package;
                BLObject.Dal.SetPackageList(PackageList);
            }
        }

        /// <summary>
        /// If the drone picked up a package the package will be delivered the new location will be 
        /// the recievers location and the battery percentage will decrese approriately 
        /// if the drone doesn't have a package appropriate message will be thrown.
        /// </summary>
        /// <param name="DroneId"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void DroneDeliversPakcage(int DroneId)
        {
            int DroneIndex = BLObject.BLDroneList.FindIndex(x => x.Id == DroneId);
            List<DO.Package> PackageList = BLObject.Dal.GetPackageList();
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
            int PackageIndex = PackageList.FindIndex(x => x.Id == Drone.PackageId);
            DO.Package Package = PackageList[PackageIndex];
            if (Package.PickedUp == null)
            {
                throw new MessageException("Error: Package was not picked up yet.");
            }
            if (Package.Delivered != null)
            {
                throw new MessageException("Error: Package was delivered already");
            }

            lock (BLObject.Dal)
            {
                DO.Customer Sender = DalObject.DataSource.CustomerList.Find(x => x.Id == Package.SenderId);
                Location SenderLocation = BLObject.MakeLocation(Sender.Longitude, Sender.Latitude);
                DO.Customer Reciever = DalObject.DataSource.CustomerList.Find(x => x.Id == Package.ReceiverId);
                Location RecieverLocation = BLObject.MakeLocation(Reciever.Longitude, Sender.Latitude);
                Drone.BatteryStatus -= BLObject.ChargeForDistance(Package.Weight, BLObject.DistanceBetween(SenderLocation, RecieverLocation));
                if (Drone.BatteryStatus < 0)
                    Drone.BatteryStatus = 0;
                Drone.Location = RecieverLocation;
                Drone.DroneStatus = DroneStatus.free;
                Package.Delivered = DateTime.Now;

                BLObject.BLDroneList[DroneIndex] = Drone;
                //PackageList[PackageIndex] = Package;
                List<DO.Package> PackageList1 = BLObject.Dal.GetPackageList();
                int Index = PackageList1.FindIndex(x => x.Id == Package.Id);
                PackageList1[Index] = Package;
                BLObject.Dal.SetPackageList(PackageList1);
            }
        }



    }
}
