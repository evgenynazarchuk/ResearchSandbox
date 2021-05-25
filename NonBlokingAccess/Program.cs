using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace NonBlockingAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckFileAccess1();
            //CheckConsoleAccess1();
            //CheckConsoleAccess2();
            //CheckConsoleAccess3();
        }

        static void CheckFileAccess1()
        {
            const int tasksCount = 100_000;
            var watch = new Stopwatch();
            var tasks = new Task[tasksCount];
            object @lock = new();

            //FileStream f = new FileStream("data.txt", FileMode.Truncate, FileAccess.Write, FileShare.Write);
            StreamWriter sw = new("data.txt");
            Action<object> action = async (msg) =>
            {
                lock (@lock)
                {
                    sw.WriteLine(msg.ToString());
                    sw.Flush();
                    //f.Write(Encoding.ASCII.GetBytes(msg.ToString() + "\n")); // так же
                    //f.Flush();
                    //await sw.WriteLineAsync(msg.ToString()); // dot not work, await not use with lock
                    //await sw.FlushAsync();
                }
                //f.Write(Encoding.ASCII.GetBytes(msg.ToString() + "\n")); // do not work, need lock
                //f.Flush();
                //await sw.WriteLineAsync(msg.ToString()); // do not work, need lock
                //await sw.FlushAsync();
            };
            //Task t111 = new Task()
            watch.Start();
            for (int i = 0; i < tasksCount; i++)
            {
                tasks[i] = Task.Factory.StartNew(action, i); // work
                //tasks[i] = new Task(action, i); // work
                //tasks[i].Start();
            }
            Task.WaitAll(tasks);
            watch.Stop(); // ~1 second for 100 000 write file tasks

            Console.WriteLine($"Completed tasks time:\t{watch.ElapsedMilliseconds}");
        }

        static void CheckConsoleAccess3()
        {
            var watch = new Stopwatch();
            const int tasksCount = 100_000;
            watch.Start();
            for (var i = 0; i < tasksCount; i++)
            {
                //Console.Write($"{i}\r"); // console write busy more time
                ;
            }

            watch.Stop();
            Console.WriteLine($"Time {watch.ElapsedMilliseconds}");
        }

        static void CheckConsoleAccess2()
        {
            const int tasksCount = 100_000;
            var watch = new Stopwatch();
            var tasks = new Task[tasksCount];
            object @lock = new();

            StreamWriter sw = new("data.txt");
            Action<object> action = (msg) =>
            {
                lock (@lock)
                {
                    Console.Write(msg.ToString() + "\r");
                }
                //Console.Write(msg.ToString() + "\r");
            };

            watch.Start();
            for (var i = 0; i < tasksCount; i++)
            {
                tasks[i] = Task.Factory.StartNew(action, i);
            }
            Task.WaitAll(tasks);
            watch.Stop();

            Console.WriteLine($"Completed tasks time:\t{watch.ElapsedMilliseconds}");
        }

        static void CheckConsoleAccess1()
        {
            var watch = new Stopwatch();
            var iteration = 10 * 60; // 10 minute for monitor
            var tasksCount = 100_000;
            var tasks = new Task[iteration, tasksCount];

            //var output = new ConcurrentConsoleOutput(TimeSpan.FromSeconds(iteration));
            //var taskMonitor = Task.Run(() => output.StartMonitor());

            watch.Start();
            for (var i = 0; i < iteration; i++)
            {
                for (var j = 0; j < tasksCount; j++)
                {
                    tasks[i, j] = Task.Run(() =>
                    {
                        Console.Write($"Message 123456789 !@#$%^&*() {j}\r");
                        //output.SendMessage($"Message 123456789 !@#$%^&*() {j}\r");
                    });
                }
                Console.WriteLine($"\nIteration {i + 1}, GC.GetTotalMemory {GC.GetTotalMemory(true)}");
                //output.SendMessage($"\nIteration {i + 1}, GC.GetTotalMemory {GC.GetTotalMemory(true)}\n");
                Task.Delay(1000).Wait();
            }

            //taskMonitor.Wait();

            for (int i = 0; i < iteration; i++)
            {
                for (int j = 0; j < tasksCount; j++)
                {
                    tasks[i, j].Wait();
                }
            }

            watch.Stop();
            Console.WriteLine($"{watch.ElapsedMilliseconds}");
        }
    }
}
