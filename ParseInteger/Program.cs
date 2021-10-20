using System;

namespace ParseInteger
{
    class Program
    {
        static void Main(string[] args)
        {
            Sample1();
            Sample2();
        }

        static void Sample1()
        {
            var str = Console.ReadLine();

            if (!Int32.TryParse(str, out int result))
            {
                Console.WriteLine("Error convertation string to integer");
                return;
            }

            Console.WriteLine($"result: {result}");
        }

        static void Sample2()
        {
            var str = Console.ReadLine();
            int result = 0;

            try
            {
                result = Int32.Parse(str);
            }
            catch
            {
                Console.WriteLine("Error convertation string to integer");
                return;
            }

            Console.WriteLine($"result: {result}");
        }
    }
}
