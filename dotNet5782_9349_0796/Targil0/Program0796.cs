using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome0796();
            Welcome9349();
            Console.ReadKey();
        }
        static partial void Welcome9349();

        private static void Welcome0796()
        {
            Console.WriteLine("Enter your name: ");
            String name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }

        
    }
}
