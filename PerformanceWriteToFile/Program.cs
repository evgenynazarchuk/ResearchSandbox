using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace PerformanceWriteToFile
{
    class Program
    {
        const int nWrite = 10_000_000;
        const string textToWrite = "Hello world Hello world Hello world Hello world Hello world";

        static void Main()
        {
            //Watch(PerformanceWrite1); // ~700
            // Watch(PerformanceWrite2); very slow
            //Watch(PerformanceWrite3); // ~1000
            Watch(PerformanceWrite4); // ~335 ------- winner! --------
            //Watch(PerformanceWrite5); // PerformanceWrite4 is fastes
        }

        public static void Watch(Action action)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            action();
            stopWatch.Stop();
            Console.WriteLine($"Time: {stopWatch.ElapsedMilliseconds}ms");
        }

        public static void PerformanceWrite1() // fast, 10 000 000 = ~0.7s
        {
            var path = $"./{nameof(PerformanceWrite1)}.txt";
            using var fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
            for (var i = 0; i < nWrite; i++)
            {
                fileStream.Write(Encoding.ASCII.GetBytes(textToWrite));
            }
            //fileStream.Flush();
        }

        //public static void PerformanceWrite2() // very low
        //{
        //    var path = $"./{nameof(PerformanceWrite2)}.txt";
        //    for (int i = 0; i < n; i++)
        //    {
        //        File.AppendAllText(path, textToWrite);
        //    }
        //}

        public static void PerformanceWrite3() // fast, 10 000 000 = ~1s
        {
            var path = $"./{nameof(PerformanceWrite3)}.txt";
            var lines = new List<string>();
            for (var i = 0; i < nWrite; i++)
            {
                lines.Add(textToWrite);
            }
            File.AppendAllLines(path, lines);
        }

        public static void PerformanceWrite4() // very very fast, 10 000 000 = ~0.35s
        {
            var path = $"./{nameof(PerformanceWrite4)}.txt";
            using var fileStream = new StreamWriter(path, false, Encoding.UTF8, 65535);
            for (var i = 0; i < nWrite; i++)
            {
                fileStream.WriteLine(textToWrite);
            }
            // fileStream.Flush();
        }

        public static void PerformanceWrite5() // fast, 10 000 000 = ~0.9s
        {
            var path = $"./{nameof(PerformanceWrite5)}.txt";
            using var fileStream = new FileStream(
                path,
                FileMode.OpenOrCreate,
                FileAccess.Write,
                FileShare.ReadWrite,
                bufferSize: 65535);

            for (var i = 0; i < nWrite; i++)
            {
                fileStream.Write(Encoding.ASCII.GetBytes(textToWrite));
            }
            // fileStream.Flush();
        }
    }
}
