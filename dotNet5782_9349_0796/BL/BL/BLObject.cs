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

            BLObject()
            {
                IDAL.IDAL DalObject = new DalObject.DalObject();
             
                double[] PowerConsumptions = DalObject.PowerConsumptions();//Returns an array of the power consumptions { Free, Light, Medium, Heavy, ChargingRate }
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
                    //Avi Weiner - battery status////////////////////////////////////////////////////////////////////////////// 
                    //Avi Weiner - location////////////////////////////////////////////////////////////////
                    NewDrone.PackageId = 0;

                    DroneList.Add(NewDrone);
                }
                foreach (IDAL.DO.Package Pack in DataSource.PackageList)
                {
                    if(Pack.DroneId != 0 && Pack.Delivered == null)
                    {
                        int index = DroneList.FindIndex(x => x.Id == Pack.DroneId);
                        DroneList[index].DroneStatus = IBL.BO.DroneStatus.delivery;
                        DroneList[index].PackageId = Pack.Id;
                        if (Pack.PickedUp == null)
                        {
                            //Avi Weiner - DroneList[index].Location = //////////////////////////////////////////////////////////////////////
                            //if package is assigned but hasn't collected yet - location will be in a station closed to the sender
                        }
                        else if(Pack.Delivered == null)
                        {
                            //Avi Winer - if package has been collected but hasn't delivered yet - location of drone will be at sender.///////////////////////////////

                        }
                        //Avi Weiner = battery state shall be randomized between the minimum charge to allow the done to deliver and reach the closeset station and 100
                    }
                   
                }
                foreach(IBL.BO.DroneToList DroneL in DroneList)
                {
                    if(DroneL.DroneStatus != IBL.BO.DroneStatus.delivery)
                    {
                        //Avi Weiner - Drone status shall be available or maintnence (Random) /////////////////////////////////////////////////////////////////////
                    }
                    if(DroneL.DroneStatus == IBL.BO.DroneStatus.maintenance)
                    {
                        //Avi Weiner - Drone Location will bone of the existing stations (random)
                        //Avi weiner - battery state will be 0-20%///////////////////////////////////////////////////////////////////////////////////////
                    }
                    if(DroneL.DroneStatus == IBL.BO.DroneStatus.free)
                    {
                        //Avi Weiner - location will be one of the cutomers already recieived a package (random)
                        //Avi Weiner - battery state will be between the minimum allows it to get the closest station and 100(random)///////////////////////////

                    }
                }
                


            }
        }
    }
}
