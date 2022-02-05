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

                Thread.Sleep(3000);
                AssignPackageToDrone(Id);
                DroneCollectsAPackage(Id);
                DroneDeliversPakcage(Id);


            }

        }
    }
}