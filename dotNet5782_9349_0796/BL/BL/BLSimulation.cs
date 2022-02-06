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

        const int StepTimer = 1000;

        private void Idle()
        {
            Thread.Sleep(StepTimer*4);
        }

        public void StopTheSimulator()
        {
            Should_Stop = true;
        }

        static bool Should_Stop = false;
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
                        Thread.Sleep(StepTimer);
                        AssignPackageToDrone(Id);
                        action();
                        Thread.Sleep(StepTimer);
                        DroneCollectsAPackage(Id);
                        action();
                        Thread.Sleep(StepTimer);
                        DroneDeliversPakcage(Id);
                        action();
                        Thread.Sleep(StepTimer);
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
                    //Charging / Maintinance 
                    while(DroneToListToDrone(Id).BatteryStatus < 1 && !Should_Stop)
                    {
                        SendDroneToCharge(Id);
                        Thread.Sleep(StepTimer);
                        ReleaseDroneFromCharge(Id);
                        SendDroneToCharge(Id);
                        action();
                        ReleaseDroneFromCharge(Id);
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