using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace DalObject
    {
        public class DalObjectClass
        {
            //creates a DAL object by intializing values accordign to Initialize
            DalObjectClass()
            {
            DataSource.Initialize();//constructor for DalObjects
            }
            //I think the following methods need to be added.
            //Adding to lists methods

            //adding base station to the stations list
            public void AddStation()
            {
            int ThisStationNumber = DataSource.GetFreeStationI();
            var rand = new Random();
            DataSource.StationList[ThisStationNumber].Id = DataSource.GetNextUniqueID();
            DataSource.StationList[ThisStationNumber].Longitude = rand.NextDouble() *360 - 180;
            DataSource.StationList[ThisStationNumber].Latitude =  rand.NextDouble() * 180 - 90;
            DataSource.StationList[ThisStationNumber].ChargeSlots = rand.Next(2,10);
            DataSource.SetNextUniqueID();
            DataSource.SetFreeCustomer();

        }

            //adding a drone to the existing drones list
            public void AddDrone()
            {
              
            }


            //adding a new customer to the customers list
            public void AddCustomer()
            {

            }


            //receiving a package to deliver
            public void AddPackage()
            {

            }


            //Updating existing data
            //assigning a package to a drone" +

            public void AssignDroneToPackage()
            {

            }


            //        "\n - collecting a package by a drone" +
            public void DronePickUp()
            {

            }

            //        "\n - providing a package to a customer" +
            public void PackageDropOff()
            {

            }


            //        "\n - sending a drone to a charge in a base station" +
            //        "\n   - by changing the drone’s status and adding a record(instance) of" +
            //        "\n     a drone battery charger entity" +

            //        "\n   - the station is selected by the user in the main menu(It is" +
            //        "\n     recommended to provide a list of stations to the user)" +
            //        "\n - releasing a drone from charging in a base station");
            //public void ChargeDrone(IDAL.DO.Drone drone, IDAL.DO.DroneCharge)
            //{

            //}






        }
    }


