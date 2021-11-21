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
                                //not sure about the funciton to update Station 
                                break;
                            case 3:
                                //not sure about the function to update Customer the question is how to I make the adding optional?
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
                                Bl.ReleaseDroneFromCharge(DroneId);
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

                        Console.WriteLine("\nEnter number of option: ");
                        InnerOption = Convert.ToInt32(Console.ReadLine());
                        switch (InnerOption)
                        {
                            //            Console.WriteLine("\n\t1. - Base Station by Id." +
                            //"\n\t2. - Drone by Id." +
                            //"\n\t3. - Package by Id." +
                            //"\n\t4. - Base Station by Id." +
                            //"\n\t5. - List display option: ");
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
                            case 5:
                                PrintListDisplayOptions();
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
                        break;
                }
            }
        }
    }
}