using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace TaskMatrixWithWait
{
    class Program
    {
        static void Main(string[] args)
        {
            //RunnerOnArray();
            RunnerOnList();
            //RunnerTasks();
        }

        public static void RunnerOnArray()
        {
            Stopwatch stopwatch = new();
            const int height = 10_000;
            const int weight = 120;

            stopwatch.Start();
            Task[][] arrayTasks = new Task[height][];
            for (int i = 0; i < height; i++) // 1 200 000
            {
                arrayTasks[i] = new Task[weight];
                for (int j = 0; j < weight; j++)
                {
                    arrayTasks[i][j] = Task.Factory.StartNew(async () =>
                    {
                        await Task.Delay(10);
                    });
                }
            }
            stopwatch.Stop();

            Console.WriteLine($"1 {DateTime.Now.ToString("O")}");
            Console.WriteLine($"2 memory: {GC.GetTotalMemory(false)}\n{stopwatch.ElapsedMilliseconds}");
            for (int i = 0; i < height; i++)
            {
                Task.WaitAll(arrayTasks[i]);
            }
            Console.WriteLine($"3 {DateTime.Now.ToString("O")}");
            Console.WriteLine($"4 memory: {GC.GetTotalMemory(false)}\n{stopwatch.ElapsedMilliseconds}");
            Console.WriteLine($"5 {DateTime.Now.ToString("O")}");
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
                    listTasks.Add(Task.Factory.StartNew(async () =>
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
                    tasks[i][j] = Task.Factory.StartNew(async () =>
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
