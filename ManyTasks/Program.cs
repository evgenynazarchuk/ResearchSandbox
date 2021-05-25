using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LargeAmountOfTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            TestForPT();
        }


        static void TestForPT()
        {
            var stopWatch = new Stopwatch();
            var taskList = new List<Task>();

            Console.WriteLine($"Time point 1: {DateTime.Now}");
            stopWatch.Start();
            for (int i = 0; i < 10000; i++)
            {
                taskList.Add(Task.Run(async () =>
                {
                    await Task.Delay(5000);

                    // dont use Task.Delay GetAwaiter GetResult, it is very slow, block
                    //Task.Delay(5000).ConfigureAwait(false).GetAwaiter().GetResult();
                    //Task.Delay(5000).GetAwaiter().GetResult();
                    //Task.Delay(5000).Wait();
                    await Task.CompletedTask;
                }));
            }
            stopWatch.Stop();
            Console.WriteLine($"Time point 2: {DateTime.Now}, time: {stopWatch.Elapsed.TotalMilliseconds} ms");

            stopWatch.Restart();
            foreach (var task in taskList)
            {
                if (task is not null)
                {
                    task.GetAwaiter().GetResult();
                }
            }
            stopWatch.Stop();
            Console.WriteLine($"Time point 3: {DateTime.Now}, time: {stopWatch.Elapsed.TotalMilliseconds} ms");
        }

        static void CreateWithWait()
        {
            var tasks = new List<Task>();
            var stopWatch = new Stopwatch();

            for (int i = 0; i < 30; i++)
            {
                stopWatch.Start();
                for (int j = 0; j < 10_000_000; j++)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        //Thread.Sleep(100); // dont use thread!!!!! use await task.delay!!!!
                        Task.Delay(10).Wait();
                    }));
                }
                stopWatch.Stop();
                double second = stopWatch.ElapsedMilliseconds;
                Console.WriteLine($"{second}\t\t{tasks.Count}");
                stopWatch.Reset();
            } // 3-4 seconds for created 10 000 000 threads (16gb i5)

            Task.WaitAll(tasks.ToArray()); // it is not optimal!
        }

        static void CreateWithoutWait()
        {
            var createTimeTasks = new Stopwatch();
            //var collectTime = new Stopwatch();
            int countTasks = 0;

            //GC.AddMemoryPressure(999999999999); // ?

            for (int i = 0; i < 1_000; i++)
            {
                createTimeTasks.Start();
                for (int j = 0; j < 1_000_000; j++)
                {
                    var t = Task.Run(async () =>
                    {
                        await Task.Delay(100);
                        //Thread.Sleep(10000); // do not use thread, do not work deallocate thread memory
                    });
                }
                createTimeTasks.Stop();
                countTasks++;

                //Thread.Sleep(1000);
                Task.Delay(1000).Wait();

                //collectTime.Start();
                //GC.Collect();
                //GC.WaitForPendingFinalizers();
                //GC.Collect();
                //collectTime.Stop();

                Console.WriteLine($"time: {createTimeTasks.ElapsedMilliseconds}\t\tcount tasks: {countTasks}");
                //Console.WriteLine($"{createTimeTasks.ElapsedMilliseconds}\t\t{countTasks}\t\t{GC.GetTotalMemory(false)}");
                //Console.WriteLine($"{createTimeTasks.ElapsedMilliseconds}\t\t{collectTime.ElapsedMilliseconds}\t\t{countTasks}\t\t{GC.GetTotalMemory(false)}");
                createTimeTasks.Reset();
                //collectTime.Reset();
            }
            // how wait ???

            // 217 million task
        }
    }
}
