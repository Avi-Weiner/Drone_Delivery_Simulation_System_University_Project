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
                        AssignPackageToDrone(Id);
                        DroneCollectsAPackage(Id);
                        DroneDeliversPakcage(Id);
                    }
                    catch (MessageException e)
                    {
                        if (e.Message == "Error: No packages to be collected.\n")
                        {
                            stillPackages = false;
                            break;
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
}