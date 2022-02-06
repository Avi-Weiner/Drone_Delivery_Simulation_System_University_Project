using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace BL
{
    public partial class BL : BlApi.IBL
    {
        private void Idle()
        {
            Thread.Sleep(4000);
        }
        public void StopTheSimulator()
        {
            Should_Stop = true;
        }
        bool Should_Stop = false;
        public void ActivateSimulator(int Id, Action action)
        {
            Thread thread = Thread.CurrentThread;
            Should_Stop = false;
            while (!Should_Stop)
            {
                bool stillPackages = true;
                while (stillPackages)
                {
                    try
                    {
                        //Need to add time taken for each process somehow (maybe)
                        Thread.Sleep(1000);
                        AssignPackageToDrone(Id);
                        action();
                        Thread.Sleep(1000);
                        DroneCollectsAPackage(Id);
                        action();
                        Thread.Sleep(1000);
                        DroneDeliversPakcage(Id);
                        action();
                        Thread.Sleep(3000);
                    }


                    catch (MessageException e)
                    {

                        if(e.Message == "Error: No packages close enough with current charge.\n")
                        {
                            break;
                        }
                        else if (e.Message == "Error: No packages to be collected.\n" )
                            //||
                            //e.Message == "Error: No packages close enough with current charge.\n")
                        {
                            stillPackages = false;
                            Should_Stop = true;//Chaim. Should this belong here? 
                            //break;  // Why is this here?
                        }
                        else
                        {
                            throw new MessageException(e.Message);
                        }
                    }
                }

                try
                {
                    //SendDroneToCharge(Id);
                    //Thread.Sleep(4000);
                    //ReleaseDroneFromCharge(Id);
                    while(DroneToListToDrone(Id).BatteryStatus < 1 || !Should_Stop)
                    {
                        SendDroneToCharge(Id);
                        Thread.Sleep(1000);
                        ReleaseDroneFromCharge(Id);
                        action();
                    }
                    Idle();

                }
                catch (MessageException e)
                {
                    if (e.Message == "Error: not enough charging slots in closest station.\n")
                    {
                        //TODO: what happens if there isn't enough charging slots in the closest station (step 3, stage 4)
                    }
                    else
                    {
                        throw new MessageException(e.Message);
                    }
                }
                
                
            }
        }
        


    }
}