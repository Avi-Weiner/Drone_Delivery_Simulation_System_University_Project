//using System;

//namespace ConsoleUI
//{
//    class Program
//    {
//        /// <summary>
//        /// Prints main menu
//        /// </summary>
//        static void PrintMainMenu()
//        {
//            Console.WriteLine("\n1. Adding options: ");
//            Console.WriteLine("2. Updating options: ");
//            Console.WriteLine("3. Display options: ");
//            Console.WriteLine("4. List display options: ");
//            Console.WriteLine("5. Exit");
//        }

//        static void PrintAddingMenu()
//        {
//            Console.WriteLine("\n1. - adding base station to the stations list" +
//                "\n2. - adding a drone to the existing drones list" +
//                "\n3. - adding a new customer to the customers list" +
//                "\n4. - adding a package to the package list");
//        }

//        static void PrintUpdatingMenu()
//        {
//            Console.WriteLine("\n1. - assigning a package to a drone" +
//                "\n2. - collecting a package by a drone" +
//                "\n3. - providing a package to a customer" +
//                "\n4. - sending a drone to a charge in a base station" +
//                "\n5. - releasing a drone from charging in a base station");
//        }

//        static void PrintDisplayMenu()
//        {
//            Console.WriteLine("\n1. - Display a base station" +
//                "\n2. - Display a drone" +
//                "\n3. - Display a customer" +
//                "\n4. - Display a package");
//        }

//        static void PrintListDisplayMenu()
//        {
//            Console.WriteLine("\n1. - Displaying base stations list" +
//                "\n2. - Displaying drones list" +
//                "\n3. - Displaying customers list" +
//                "\n4. - Displaying packages list" +
//                "\n5. - Displaying packages not assigned yet to a drone" +
//                "\n6. - Displaying base s1tations with unoccupied charging stations");
//        }


//        static void Main(string[] args)
//        {
//            int option = 0;
//            int InnerOption;
//            //ID1 and ID2 are for all the ID pickups in the switches
//            int ID1, ID2;
//            IDAL.IDAL IDAL = new DalObject.DalObject();

//            while (option != 5) //Option = 5 for exit
//                {
//                    PrintMainMenu();
//                    Console.WriteLine("\nEnter number of option: ");
//                    option = Convert.ToInt32(Console.ReadLine());
//                switch (option)
//                {
//                    case 1:
//                        PrintAddingMenu();
//                        Console.WriteLine("\nEnter number of option: ");
//                        InnerOption = Convert.ToInt32(Console.ReadLine());
//                        switch(InnerOption)
//                        {
//                            case 1:
//                                DalObject.DalObject.AddStation();
//                                break;
//                            case 2:
//                                DalObject.DalObject.AddDrone();
//                                break;
//                            case 3:
//                                DalObject.DalObject.AddCustomer();
//                                break;
//                            case 4:
//                                DalObject.DalObject.AddPackage();
//                                break;
//                        }  
//                        break;

//                    case 2:
//                        PrintUpdatingMenu();
//                        Console.WriteLine("\nEnter number of option: ");
//                        InnerOption = Convert.ToInt32(Console.ReadLine());
//                        switch (InnerOption)
//                        {
//                            case 1:
//                                Console.WriteLine("Enter Package ID: ");
//                                ID1 = Convert.ToInt32(Console.ReadLine());
//                                Console.WriteLine("Enter Drone ID: ");
//                                ID2 = Convert.ToInt32(Console.ReadLine());
//                                DalObject.DalObject.AssignDroneToPackage(ID1,ID2);
//                                break;

//                            case 2:
//                                Console.WriteLine("Enter Package ID: ");
//                                ID1 = Convert.ToInt32(Console.ReadLine());
//                                Console.WriteLine("Enter Drone ID: ");
//                                ID2 = Convert.ToInt32(Console.ReadLine());
//                                DalObject.DalObject.DronePickUp(ID1,ID2);
//                                break;

//                            case 3:
//                                Console.WriteLine("Enter package ID: ");
//                                ID1 = Convert.ToInt32(Console.ReadLine());
//                                DalObject.DalObject.PackageDropOff(ID1);
//                                break;

//                            case 4:
//                                Console.WriteLine("Enter Drone ID: ");
//                                ID1 = Convert.ToInt32(Console.ReadLine());
//                                Console.WriteLine("Enter Station ID: ");
//                                ID2 = Convert.ToInt32(Console.ReadLine());
//                                DalObject.DalObject.ChargeDrone(ID1,ID2);
//                                break;

//                            case 5:
//                                Console.WriteLine("Enter Drone ID: ");
//                                ID1 = Convert.ToInt32(Console.ReadLine());
//                                Console.WriteLine("Enter Station ID: ");
//                                ID2 = Convert.ToInt32(Console.ReadLine());
//                                DalObject.DalObject.ReleaseDrone(ID1, ID2);
//                                break;
//                        }
//                        break;

//                    case 3:
//                        PrintDisplayMenu();
//                        Console.WriteLine("\nEnter number of option: ");
//                        InnerOption = Convert.ToInt32(Console.ReadLine());
//                        switch (InnerOption)
//                        {
//                            case 1:
//                                Console.WriteLine("Enter Station ID: ");
//                                ID1 = Convert.ToInt32(Console.ReadLine());
//                                DalObject.DalObject.DisplayBaseStation(ID1);
//                                break;

//                            case 2:
//                                Console.WriteLine("Enter Drone ID: ");
//                                ID1 = Convert.ToInt32(Console.ReadLine());
//                                DalObject.DalObject.DisplayDrone(ID1);
//                                break;

//                            case 3:
//                                Console.WriteLine("Enter customer ID: ");
//                                ID1 = Convert.ToInt32(Console.ReadLine());
//                                DalObject.DalObject.DisplayCustomer(ID1);
//                                break;

//                            case 4:
//                                Console.WriteLine("Enter Package ID: ");
//                                ID1 = Convert.ToInt32(Console.ReadLine());
//                                DalObject.DalObject.DisplayPackage(ID1);
//                                break;
//                        }
//                        break;

//                    case 4:
//                        PrintListDisplayMenu();
//                        Console.WriteLine("\nEnter number of option: ");
//                        InnerOption = Convert.ToInt32(Console.ReadLine());
//                        switch (InnerOption)
//                        {
//                            case 1:
//                                DalObject.DalObject.DisplayStationList();
//                                break;

//                            case 2:
//                                DalObject.DalObject.DisplayDroneList();
//                                break;

//                            case 3:
//                                DalObject.DalObject.DisplayCustomerList();
//                                break;

//                            case 4:
//                                DalObject.DalObject.DisplayPackageList();
//                                break;

//                            case 5:
//                                DalObject.DalObject.DisplayUnassignedPackages();
//                                break;

//                            case 6:
//                                DalObject.DalObject.DisplayFreeChargingStations();
//                                break;
//                        }
//                        break;
//                }
                                


//            }
//        }
//    }
//}
