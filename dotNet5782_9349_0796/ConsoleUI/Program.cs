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
            Console.WriteLine("2. Updating options: " );
            Console.WriteLine("3. Display options: ");
            Console.WriteLine("4. List display options: ");
                "\n - Displaying customers list" +
                "\n - Displaying packages list" +
                "\n - Displaying packages not assigned yet to a drone" +

        static void PrintAddingMenu()
        {
            Console.WriteLine("\n1. - adding base station to the stations list" +
                "\n2. - adding a drone to the existing drones list" +
                "\n3. - adding a new customer to the customers list" +
                "\n4. - receiving a package to deliver");
        }

        static void PrintUpdatingMenu()
        {
            Console.WriteLine("\n1. - assigning a package to a drone" +
                "\n2. - collecting a package by a drone" +
                "\n3. - providing a package to a customer" +
                "\n4. - sending a drone to a charge in a base station" +
                "\n5. - releasing a drone from charging in a base station");
        }

        static void PrintDisplayMenu()
        {
            Console.WriteLine("\n1. - Display a base station" +
                "\n2. - Display a drone" +
                "\n3. - Display a customer" +
                "\n4. - Display a package");
        }

        static void PrintListDisplayMenu()
        {
            Console.WriteLine("\n1. - Displaying base stations list" +
                "\n2. - Displaying drones list" +
                "\n3. - Displaying customers list" +
                "\n4. - Displaying packages list" +
                "\n5. - Displaying packages not assigned yet to a drone" +
                "\n6. - Displaying base stations with unoccupied charging stations");
        }
        
            Console.WriteLine("5. Exit");
        #endregion
        }
        

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

            }
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
                            case 1:

                }
            }

        }
    }
}

