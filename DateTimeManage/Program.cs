using System;

namespace DateTimeManage
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime time = new DateTime();
            Console.WriteLine(time);

            PrintDateTime();
            Console.WriteLine(DateTime.MinValue);
        }

        static void PrintDateTime(DateTime time = new DateTime())
        {
            Console.WriteLine(time);
        }
    }
}
