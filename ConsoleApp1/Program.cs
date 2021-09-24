using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int? x = null;

            while (x is null)
            {
                Console.Write("x: ");
                var numStr = Console.ReadLine();

                try
                {
                    x = Int32.Parse(numStr);
                }
                catch
                {
                    Console.WriteLine("Error convertation string to integer");
                }
            }

            Console.WriteLine($"Your x: {x}");
        }
    }
}
