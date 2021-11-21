using System;

namespace ConsoleUI_BL
{
    class Program
    {
        /// <summary>
        /// prints the outer main menu
        /// </summary>
        static void PrintMainMenu()
        {
            Console.WriteLine("\n1. Adding options: ");
            Console.WriteLine("\n2. Updating options: ");
            Console.WriteLine("\n3. Drone Actions: ");
            Console.WriteLine("\n4. Display options: ");
            Console.WriteLine("\n5. List display options: ");
            Console.WriteLine("\n6. Exit");
        }
        /// <summary>
        /// prints the adding menu
        /// </summary>
        static void PrintAddingMenu()
        {
            Console.WriteLine("\n1. - adding base station to the stations list" +
                "\n2. - adding a drone to the existing drones list" +
                "\n3. - adding a new customer to the customers list" +
                "\n4. - adding a package to the package list");
        }
        /// <summary>
        /// prints the updating menu
        /// </summary>
        static void PrintUpdatingOptionsMenu()
        {
            Console.WriteLine("\n1. - Update drone: name only." +
                "\n2. - Update Station." + " i. Station name." +
                                        "\n\t\tii. Total NumberOf charging Stations." +
                "\n3. - Update Customer." + " i. New Name." +
                                      "\n\t\t ii. New phone");
        }

        /// <summary>
        /// prints the Drone Action menu
        /// </summary>
        static void PrintDroneActionsMenu()
        {
            Console.WriteLine("\n\t1. - Send a Drone to charge\n"
                + "\t2. - Release a drone from charge\n"
                + "\t3. - Assign a pakcage to a drone\n"
                + "\t4. - Collect a pakcage by a drone\n"
                + "\t5. - Deliver a package by a drone\n");
        }
        /// <summary>
        /// print the display menu 1-4 display specific field by id
        /// 5 another menu will be printed for options to print out a whole list
        /// </summary>
        static void PrintDisplayOptions()
        {
            Console.WriteLine("\n\t1. - Base Station by Id." +
                "\n\t2. - Drone by Id." +
                "\n\t3. - Package by Id." +
                "\n\t4. - Base Station by Id." +
                "\n\t5. - Display list.\n");
        }
        /// <summary>
        /// prints printing a list menu
        /// </summary>
        static void PrintListDisplayOptions()
        {
            Console.WriteLine("\n\t\t1. - Base Station list." +
                "n\t\t2. - Drones list." +
                "\n\t\t3. - Customers list." +
                "\n\t\t4. - Packages list." +
                "\n\t\t5. - Unassigned Packages." +
                "\n\\t\t6. - Display base Stations with available charging slots.");
        }
        /// <summary>
        /// Adds a base station
        /// </summary>
        /// <param name="Bl"></param>
        static void AddBaseStation(BL.BL Bl)
        {
            int name; double longitude, latitude; int availableSlots;
            Console.WriteLine("Enter: (Name, Longitude, Latitude, Available Slots)\n");
            name = Convert.ToInt32(Console.ReadLine());
            longitude = Convert.ToDouble(Console.ReadLine());
            latitude = Convert.ToDouble(Console.ReadLine());
            availableSlots = Convert.ToInt32(Console.ReadLine());

            Bl.AddBaseStation(name, longitude, latitude, availableSlots);
        }
        /// <summary>
        /// adds a drone
        /// </summary>
        /// <param name="Bl"></param>
        static void AddDrone(BL.BL Bl)
        {
            Console.WriteLine("Enter Model:\n");
            string Model = Console.ReadLine();
            Console.WriteLine("Enter WeightCategory(string):\n");
            IDAL.DO.WeightCategory Weight = (IDAL.DO.WeightCategory)Enum.Parse(typeof(IDAL.DO.WeightCategory), Console.ReadLine());
            Console.WriteLine("Enter StationId(For Charging):\n");
            int stationId = Convert.ToInt32(Console.ReadLine());
            Bl.AddDrone(Model, Weight, stationId);
        }
        /// <summary>
        /// adds a new customer
        /// </summary>
        /// <param name="Bl"></param>
        static void AddNewCustomer(BL.BL Bl)
        {
            Console.WriteLine("Enter CustomerId:\n");
            int CustomerId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(" Enter Name:\n");
            string name = Console.ReadLine();
            Console.WriteLine(" Enter Phone:\n");
            string phone = Console.ReadLine();
            Console.WriteLine(" Enter Longitude:\n");
            double Longitude = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine(" Enter Latitude:\n");
            double Latitude = Convert.ToDouble(Console.ReadLine());
            Bl.AddCustomer(CustomerId, name, phone, Longitude, Latitude);
        }
        /// <summary>
        /// Adds a new package
        /// </summary>
        /// <param name="Bl"></param>
        static void AddAPakcage(BL.BL Bl)
        {
            Console.WriteLine("Enter SenderId:\n");
            int SenderId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter ReceiverId:\n");
            int RecieiverId = Convert.ToInt32(Console.ReadLine());
            IDAL.DO.WeightCategory Weight = (IDAL.DO.WeightCategory)Enum.Parse(typeof(IDAL.DO.WeightCategory), Console.ReadLine());
            IDAL.DO.Priority Priority = (IDAL.DO.Priority)Enum.Parse(typeof(IDAL.DO.Priority), Console.ReadLine());
            Bl.AddPackage(SenderId, RecieiverId, Weight, Priority);
        }
        /// <summary>
        /// update a drone
        /// Model could be updated
        /// </summary>
        /// <param name="Bl"></param>
        static void UpdateDrone(BL.BL Bl)
        {
            Console.WriteLine("Enter Drone Id:\n");
            int Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter new Model:\n");
            string Model = Console.ReadLine();
            Bl.UpdateDrone(Id, Model);
        }

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
                    case 1:
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

                    case 2:
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

                    case 3:
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

                    case 4:

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