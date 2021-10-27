using System;

namespace ConsoleUI
{
    class Program
    {
        /// <summary>
        /// Prints main menu
        /// </summary>
        static void PrintMainMenu()
        {
            #region printing menue
            Console.WriteLine("1. Adding options: ");
            Console.WriteLine("2. Updating options" +
                "\n 1. - assigning a package to a drone" +
                "\n 2. - collecting a package by a drone" +
                "\n 3. - providing a package to a customer" +
                "\n 4. - sending a drone to a charge in a base station" +
                "\n 5. - by changing the drone’s status and adding a record(instance) of" +
                "\n     a drone battery charger entity" +
                "\n   - the station is selected by the user in the main menu(It is" +
                "\n     recommended to provide a list of stations to the user)" +
                "\n - releasing a drone from charging in a base station");
            Console.WriteLine("3. Display options (all chosen by a number):" +
                "\n - Display a base station" +
                "\n - Display a drone" +
                "\n - Display a customer" +
                "\n - Display a package");
            Console.WriteLine("4. List display options" +
                "\n - Displaying base stations list" +
                "\n - Displaying drones list" +
                "\n - Displaying customers list" +
                "\n - Displaying packages list" +
                "\n - Displaying packages not assigned yet to a drone" +
                "\n - Displaying base stations with unoccupied charging stations");
            Console.WriteLine("5. Exit");
        #endregion
        }

        static void PrintAddingMenu()
        {
            "\n 1. - adding base station to the stations list" +
                "\n 2. - adding a drone to the existing drones list" +
                "\n 3. - adding a new customer to the customers list" +
                "\n 4. - receiving a package to deliver"
        }

        static void PrintUpdatingMenu()
        {

        }

        static void PrintDisplayMenu()
        {

        }

        static void PrintListDisplayMenu()
        {

        }
        

        static void Main(string[] args)
        {
            int option = 0;
      
            while (option != 5) //Option = 5 for exit
            {
                PrintMainMenu();
                Console.WriteLine("Enter number of option: ");
                option = Convert.ToInt32(Console.ReadLine());
                switch(option)
                    {
                    case 1:
                        PrintAddingMenu();
                        int InnerOption = Convert.ToInt32(Console.ReadLine());
                        switch(InnerOption)
                        {
                            case 1:

                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            case 4
                                break;
                        }
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                }

            }

        }
    }
}

