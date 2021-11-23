using System;

namespace ConsoleUI_BL
{
    partial class Program
    {
        static void Main(string[] args)
        {
            int option = 0;
            int InnerOption;
            BL.BL Bl = new BL.BL();
            
            
            while (option != 6) //Option = 6 for exit
            {
                PrintMainMenu();
                Console.WriteLine("\nEnter number of option: ");
                option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1://adding options
                        PrintAddingMenu();
                        Console.WriteLine("\nEnter number of option: ");
                        InnerOption = Convert.ToInt32(Console.ReadLine());
                        switch (InnerOption)
                        {
                            case 1:
                                AddBaseStation(Bl);
                                break;
                            case 2:
                                AddDrone(Bl);
                                break;
                            case 3:
                                AddNewCustomer(Bl);
                                break;
                            case 4:
                                AddAPakcage(Bl);
                                break;
                        }
                        break;

                    case 2://updating uptions
                        PrintUpdatingOptionsMenu();
                        Console.WriteLine("Enter number of option: ");
                        InnerOption = Convert.ToInt32(Console.ReadLine());
                        switch (InnerOption)
                        {
                            case 1:
                                UpdateDrone(Bl);
                                break;
                            case 2:
                                UpdateStation(Bl);
                                break;
                            case 3:
                                UpdateCustomer(Bl);
                                break;

                        }

                        break;

                    case 3://Drone actions
                        PrintDroneActionsMenu();
                        Console.WriteLine("Enter number of option: ");
                        InnerOption = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter drone Id:\n");
                        int DroneId = Convert.ToInt32(Console.ReadLine());
                        switch (InnerOption)
                        {
                            case 1:
                                Bl.SendDroneToCharge(DroneId);
                                break;
                            case 2:
                                Console.WriteLine("Enter charge time in dateTime Format.\n");
                                DateTime ChargeTime = Convert.ToDateTime(Console.ReadLine());
                                Bl.ReleaseDroneFromCharge(DroneId, ChargeTime);
                                break;
                            case 3:
                                Bl.DroneCollectsAPackage(DroneId);
                                break;
                            case 4:
                                Bl.DroneDeliversPakcage(DroneId);
                                break;
                        }

                        break;

                    case 4://Dispaly option
                        PrintDisplayMenu();
                        Console.WriteLine("\nEnter number of option: ");
                        InnerOption = Convert.ToInt32(Console.ReadLine());
                        switch (InnerOption)
                        {
                            case 1:
                                Console.WriteLine("Enter Base Station Id:\n");
                                int Id = Convert.ToInt32(Console.ReadLine());
                                //are there any print functions?
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            case 4:
                                break;
                        }
                        break;

                    case 5:
                        PrintListDisplayMenu();
                        Console.WriteLine("Enter number of option:");
                        InnerOption = Convert.ToInt32(Console.ReadLine());
                        switch (InnerOption)
                        {
                            case 1:

                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            case 4:
                                break;
                            case 5:
                                break;
                            case 6:
                                break;
                        }
                        break;
                }
            }
        }
    }
}