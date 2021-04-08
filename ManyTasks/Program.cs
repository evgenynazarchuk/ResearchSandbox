using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;

namespace Research
{
    class Program
    {
        static void Main(string[] args)
        {
            PerformanceCreate();
        }

        static void Create()
        {
            var tasks = new List<Task>();
            var stopWatch = new Stopwatch();

            for (int i = 0; i < 30; i++)
            {
                stopWatch.Start();
                for (int j = 0; j < 10_000_000; j++)
                {
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        Thread.Sleep(100);
                    }));
                }
                stopWatch.Stop();
                double second = stopWatch.ElapsedMilliseconds;
                Console.WriteLine($"{second}\t\t{tasks.Count}");
                stopWatch.Reset();
            } // 3-4 seconds for created 10 000 000 threads (16gb i5)

            Task.WaitAll(tasks.ToArray()); // it is not optimal!
        }

        static void PerformanceCreate()
        {
            var createTimeTasks = new Stopwatch();
            var collectTime = new Stopwatch();
            int countTasks = 0;

            //GC.AddMemoryPressure(999999999999); // ?

            for (int i = 0; i < 1_000; i++)
            {
                createTimeTasks.Start();
                for (int j = 0; j < 1_000_000; j++)
                {
                    var t = Task.Factory.StartNew(async () =>
                    {
                        await Task.Delay(100);
                        //Thread.Sleep(10000); // do not use thread, do not work deallocate thread memory
                    });
                }
                createTimeTasks.Stop();
                countTasks++;

                Thread.Sleep(1000);

                //collectTime.Start();
                //GC.Collect();
                //GC.WaitForPendingFinalizers();
                //GC.Collect();
                //collectTime.Stop();

                Console.WriteLine($"{createTimeTasks.ElapsedMilliseconds}\t\t{countTasks}\t\t{GC.GetTotalMemory(false)}");
                //Console.WriteLine($"{createTimeTasks.ElapsedMilliseconds}\t\t{collectTime.ElapsedMilliseconds}\t\t{countTasks}\t\t{GC.GetTotalMemory(false)}");
                createTimeTasks.Reset();
                //collectTime.Reset();
            }
            // how wait ???

            // 217 million task
        }
    }
}
