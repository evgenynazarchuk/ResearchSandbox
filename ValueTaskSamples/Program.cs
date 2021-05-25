using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ValueTaskSamples
{
    class Program
    {
        public static void Main()
        {
            CheckGenerateSimpleAsyncTaskAndValueTask(10, 10, 1000000);
        }

        public static void CheckGenerateSimpleAsyncTaskAndValueTask(int k, int l, int m)
        {
            var valueTasks = new Task[k];
            var tasks = new Task[k];

            for (var i = 0; i < k; i++)
            {
                valueTasks[i] = GenerateSimpleAsyncValueTask(l, m);
                tasks[i] = GenerateSimpleAsyncTask(l, m);
                Console.WriteLine("--------------");
            }

            for (var i = 0; i < k; i++)
            {
                valueTasks[i].Wait();
                tasks[i].Wait();
                Console.WriteLine("--------------");
            }
        }

        public static async Task GenerateSimpleAsyncValueTask(int n, int m)
        {
            var watch = new Stopwatch();
            var tasks = new ValueTask<int>[n, m];
            var result = 0;

            watch.Start();
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < m; j++)
                {
                    tasks[i, j] = CreateAsyncValueTask();
                }
            }

            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < m; j++)
                {
                    result += tasks[i, j].Result;
                }
            }
            watch.Stop();

            Console.WriteLine($"Result: {result}, Value time: {watch.ElapsedMilliseconds}");
            await Task.CompletedTask;
        }

        public static async Task GenerateSimpleAsyncTask(int n, int m)
        {
            var watch = new Stopwatch();
            var tasks = new Task<int>[n, m];
            var result = 0;

            watch.Start();
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < m; j++)
                {
                    tasks[i, j] = CreateAsyncTask();
                }
            }

            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < m; j++)
                {
                    result += tasks[i, j].Result;
                }
            }
            watch.Stop();

            Console.WriteLine($"Result: {result}, Task time: {watch.ElapsedMilliseconds}");
            await Task.CompletedTask;
        }

        public static async ValueTask<int> CreateAsyncValueTask()
        {
            await Task.Delay(1);
            return 11;
        }

        public static async Task<int> CreateAsyncTask()
        {
            await Task.Delay(1);
            return 11;
        }
    }
}