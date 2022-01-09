//using System;
////Change for final commit
//namespace ConsoleUI_BL
//{
//    partial class Program
//    {
//        static void Main(string[] args)
//        {
            
//            int option = 0;
//            int InnerOption;
//            BlApi.IBL Bl = BL.BlFactory.GetBl();

//            //ID for all the Id's to be entered by the user
//            int id;

//            while (option != 6) //Option = 6 for exit
//            {
//                try 
//                {
//                    PrintMainMenu();
//                    Console.WriteLine("\nEnter number of option: ");
//                    option = Convert.ToInt32(Console.ReadLine());
//                    switch (option)
//                    {
//                        case 1://adding options
//                            PrintAddingMenu();
//                            Console.WriteLine("\nEnter number of option: ");
//                            InnerOption = Convert.ToInt32(Console.ReadLine());
//                            switch (InnerOption)
//                            {
//                                case 1:
//                                  //  AddBaseStation(Bl);
//                                    break;
//                                case 2:
//                                    //AddDrone(Bl);
//                                    break;
//                                case 3:
//                                    //AddNewCustomer(Bl);
//                                    break;
//                                case 4:
//                                    //AddAPakcage(Bl);
//                                    break;
//                            }
//                            break;

//                        case 2://updating uptions
//                            PrintUpdatingOptionsMenu();
//                            Console.WriteLine("\nEnter number of option: ");
//                            InnerOption = Convert.ToInt32(Console.ReadLine());
//                            switch (InnerOption)
//                            {
//                                case 1:
//                                   // UpdateDrone(Bl);
//                                    break;
//                                case 2:
//                                    //UpdateStation(Bl);
//                                    break;
//                                case 3:
//                                    //UpdateCustomer(Bl);
//                                    break;

//                            }

//                            break;

//                        case 3://Drone actions
//                            PrintDroneActionsMenu();
//                            Console.WriteLine("\nEnter number of option: ");
//                            InnerOption = Convert.ToInt32(Console.ReadLine());
//                            Console.WriteLine("\nEnter drone Id: ");
//                            int DroneId = Convert.ToInt32(Console.ReadLine());
//                            switch (InnerOption)
//                            {
//                                case 1:
//                                    Bl.SendDroneToCharge(DroneId);
//                                    break;
//                                case 2:
//                                    Console.WriteLine("\nEnter charge time in dateTime Format: ");
//                                    DateTime ChargeTime = Convert.ToDateTime(Console.ReadLine());
                                   
//                                    break;
//                                case 3:
//                                    Bl.DroneCollectsAPackage(DroneId);
//                                    break;
//                                case 4:
//                                    Bl.DroneDeliversPakcage(DroneId);
//                                    break;
//                            }

//                            break;

//                        case 4://Dispaly option
//                            PrintDisplayMenu();
//                            Console.WriteLine("\nEnter number of option: ");
//                            InnerOption = Convert.ToInt32(Console.ReadLine());
//                            switch (InnerOption)
//                            {
//                                case 1:
//                                    //Base Station Print
//                                    Console.WriteLine("\nEnter Base Station Id: ");
//                                    id = Convert.ToInt32(Console.ReadLine());
//                                    Console.WriteLine(Bl.DalToBlStation(id).ToString());
//                                    break;
//                                case 2:
//                                    //Drone Print
//                                    Console.WriteLine("\nEnter Drone Id: ");
//                                    id = Convert.ToInt32(Console.ReadLine());
//                                    Console.WriteLine(Bl.DroneToListToDrone(id).ToString());
//                                    break;
//                                case 3:
//                                    //Customer Print
//                                    Console.WriteLine("\nEnter Customer Id: ");
//                                    id = Convert.ToInt32(Console.ReadLine());
//                                    //Console.WriteLine(Bl.DalToBlCustomer(id).ToString());
//                                    break;
//                                case 4:
//                                    //Package print
//                                    Console.WriteLine("\nEnter Package Id: ");
//                                    id = Convert.ToInt32(Console.ReadLine());
//                                    Console.WriteLine(Bl.DalToBlPackage(id).ToString());
//                                    break;
//                            }
//                            break;

//                        case 5:
//                            PrintListDisplayMenu();
//                            Console.WriteLine("\nEnter number of option: ");
//                            InnerOption = Convert.ToInt32(Console.ReadLine());
//                            switch (InnerOption)
//                            {
//                                case 1:
//                                    //Base Station List
//                                    PrintStationList(Bl);
//                                    break;
//                                case 2:
//                                    //Drone List
//                                    PrintDroneList(Bl);
//                                    break;
//                                case 3:
//                                    //Customer List
//                                    PrintCustomerList(Bl);
//                                    break;
//                                case 4:
//                                    //Package List
//                                    PrintPackageList(Bl);
//                                    break;
//                                case 5:
//                                    //Unassigned Package List
//                                    PrintUnassignedPackages(Bl);
//                                    break;
//                                case 6:
//                                    //Base stations with available charge slot/s list
//                                    PrintStationsWithAvailableChargeSlots(Bl);
//                                    break;
//                            }
//                            break;
//                    }
//                }
//                catch (BL.MessageException e)
//                {
//                    Console.WriteLine(e);
//                }
//            }
//        }
//    }
//}