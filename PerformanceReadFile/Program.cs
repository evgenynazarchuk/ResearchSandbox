using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace PerformanceReadFile
{
    class Program
    {
        static void Main()
        {
            Watch(PerformanceRead1);
            Watch(PerformanceRead2);
        }

        public static void Watch(Action action)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            action();
            stopWatch.Stop();
            Console.WriteLine($"Time: {stopWatch.ElapsedMilliseconds}ms");
        }

        public static void PerformanceRead1()
        {
            var path = "./PerformanceRead.txt";
            string line;
            using var fileStream = new StreamReader(path, Encoding.UTF8, true, 65535);

            while ((line = fileStream.ReadLine()) != null)
            {
                ;
            }
        }

        public static void PerformanceRead2()
        {
            var path = "./PerformanceRead.txt";
            var data = File.ReadAllBytes(path);
        }
    }
}
