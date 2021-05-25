using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TaskMatrixWithWait
{
    class Program
    {
        static void Main(string[] args)
        {
            RunnerOnArray();
            //RunnerOnList();
            //RunnerTasks();
        }

        public static void RunnerOnArray()
        {
            //Stopwatch stopwatch = new();
            //Stopwatch disposeWatch = new();
            const int height = 100_000;
            const int weight = 10;
            int i = 0;
            int j = 0;

            //stopwatch.Start();
            Task[,] arrayTasks = new Task[height, weight];
            for (i = 0; i < height; i++) // 1 200 000
            {
                //arrayTasks[i] = new Task[weight];
                for (j = 0; j < weight; j++)
                {
                    //stopwatch.Start();
                    //disposeWatch.Start();
                    if (arrayTasks[i, j] is not null)
                    {
                        arrayTasks[i, j].Dispose();
                    }
                    //disposeWatch.Stop();
                    //disposeWatch.Reset();
                    //Console.WriteLine($"3 Dispose: {disposeWatch.ElapsedMilliseconds}");

                    arrayTasks[i, j] = Task.Run(async () =>
                     {
                         ;
                         await Task.Delay(1);
                     });

                    //stopwatch.Stop();

                    //Console.WriteLine($"1 {DateTime.Now.ToString("O")}");

                    //Console.WriteLine($"2 Memory: {GC.GetTotalMemory(false)}, Time: {stopwatch.ElapsedMilliseconds}");

                    //stopwatch.Reset();
                }

                Console.WriteLine($"4 Memory: {GC.GetTotalMemory(false)}");

                if (i == height - 1 && j == weight)
                {

                    i = 0;
                    j = 0;
                }
            }
            //stopwatch.Stop();


            //Console.WriteLine($"1 {DateTime.Now.ToString("O")}");
            //Console.WriteLine($"2 memory: {GC.GetTotalMemory(false)}\n{stopwatch.ElapsedMilliseconds}");
            // never run!!!!)))
            //for (int k = 0; k < height; k++)
            //{
            //    for (int n = 0; n < weight; n++)
            //    {
            //        arrayTasks[k, n].Wait();
            //    }
            //}
            //Console.WriteLine($"3 {DateTime.Now.ToString("O")}");
            //Console.WriteLine($"4 memory: {GC.GetTotalMemory(false)}\n{stopwatch.ElapsedMilliseconds}");
            //Console.WriteLine($"5 {DateTime.Now.ToString("O")}");
        }

        public static void RunnerOnList()
        {
            Stopwatch stopwatch = new();
            const int height = 10_000;
            const int weight = 120;

            List<Task> listTasks = new();

            stopwatch.Start();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < weight; j++)
                {
                    listTasks.Add(Task.Run(async () =>
                    {
                        await Task.Delay(10);
                    }));
                }
            }
            stopwatch.Stop();

            Console.WriteLine($"1 {DateTime.Now.ToString("O")}");
            Console.WriteLine($"2 memory: {GC.GetTotalMemory(false)}\n{stopwatch.ElapsedMilliseconds}");
            Console.WriteLine($"3 {DateTime.Now.ToString("O")}");
            Task.WaitAll(listTasks.ToArray());
            Console.WriteLine($"4 {DateTime.Now.ToString("O")}");
            Console.WriteLine($"5 memory: {GC.GetTotalMemory(false)}\n{stopwatch.ElapsedMilliseconds}");
            Console.WriteLine($"6 {DateTime.Now.ToString("O")}");
        }

        public static async Task RunnerTasks()
        {
            //const int height = 10_000;
            //const int weight = 18_000;
            const int height = 3; // 
            const int weight = 3; //
            int i = 0;
            int j = 0;
            Task[][] tasks = new Task[height][];

            Console.WriteLine($"{DateTime.Now.ToString("O")}");
            for (i = 0; i < height; i++)
            {
                tasks[i] = new Task[weight];
                for (j = 0; j < weight; j++)
                {
                    tasks[i][j] = Task.Run(async () =>
                    {
                        await Task.Delay(1000);
                    });
                }
                await Task.Delay(1000);
                Console.WriteLine($"{DateTime.Now.ToString("O")}");

                if (i == height - 1 && j == weight)
                {
                    i = 0;
                    j = 0;
                }
            }

            // not run, never)
            for (int k = 0; k < height; k++)
            {
                Task.WaitAll(tasks[k]);
            }
        }
    }
}
