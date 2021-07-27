using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DateTimeManage
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch watch = new();
            watch.Start();
            Task.Delay(1000).Wait();
            watch.Stop();
            Console.WriteLine($"{watch.Elapsed}");
            //var time1 = TimeSpan.FromMilliseconds(0.1d);
            //Console.WriteLine(time1);
            //var time2 = TimeSpan.FromMilliseconds(0.01d);
            //Console.WriteLine(time2);
            //DateTime time = new DateTime();
            //Console.WriteLine(time);
            //
            //PrintDateTime();
            //Console.WriteLine(DateTime.MinValue);
        }

        //static void PrintDateTime(DateTime time = new DateTime())
        //{
        //    Console.WriteLine(time);
        //}
    }
}
