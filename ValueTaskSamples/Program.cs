using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ValueTaskSamples
{
    class Program
    {
        static void Main()
        {
            // это параллельное создание тасок
            // нужно изучить почему падает скорость из за использования Task.Delay
            // и как это избежать
            // 100 за 1.5 секунды
            CheckGenerateSimpleTaskAndValueTask(1, 1, 100);
            CheckGenerateSimpleAsyncTaskAndValueTask(1, 1, 100);
            

            //Console.WriteLine($"{await CreateAsyncValueTask()}");
            //Console.WriteLine($"{await CreateAsyncTask()}");
            //Console.WriteLine($"{CreateAsyncValueTask().AsTask().GetAwaiter().GetResult()}");
            //Console.WriteLine($"{CreateAsyncTask().GetAwaiter().GetResult()}");
        }

        public static void CheckGenerateSimpleAsyncTaskAndValueTask(int k, int l, int m)
        {
            for (int i = 0; i < k; i++)
            {
                GenerateSimpleAsyncValueTask(l, m).Wait();
                GenerateSimpleAsyncTask(l, m).Wait();
                Console.WriteLine("--------------");
            }
        }

        public static async Task GenerateSimpleAsyncValueTask(int n, int m)
        {
            Stopwatch watch = new();

            int result = 0;
            watch.Start();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    result += await CreateAsyncValueTask();
                }
            }
            watch.Stop();
            Console.WriteLine($"Result: {result}, Value time: {watch.ElapsedMilliseconds}");
        }

        public static async Task GenerateSimpleAsyncTask(int n, int m)
        {
            Stopwatch watch = new();

            int result = 0;
            watch.Start();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    result += await CreateAsyncTask();
                }
            }
            watch.Stop();
            Console.WriteLine($"Result: {result}, Task time: {watch.ElapsedMilliseconds}");
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

        public static void CheckGenerateSimpleTaskAndValueTask(int k, int l, int m)
        {
            for (int i = 0; i < k; i++)
            {
                GenerateSimpleValueTask(l, m).Wait();
                GenerateSimpleTask(l, m).Wait();
                Console.WriteLine("--------------");
            }
        }

        public static async Task GenerateSimpleValueTask(int n, int m)
        {
            Stopwatch watch = new();

            int result = 0;
            watch.Start();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    result += await CreateValueTask();
                }
            }
            watch.Stop();
            Console.WriteLine($"Result: {result}, Value time: {watch.ElapsedMilliseconds}");
        }

        public static async Task GenerateSimpleTask(int n, int m)
        {
            Stopwatch watch = new();

            int result = 0;
            watch.Start();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    result += await CreateTask();
                }
            }
            watch.Stop();
            Console.WriteLine($"Result: {result}, Task time: {watch.ElapsedMilliseconds}");
        }

        public static ValueTask<int> CreateValueTask()
        {
            Task.Delay(1).Wait();
            return new(11);
        }

        public static Task<int> CreateTask()
        {
            Task.Delay(1).Wait();
            return Task.FromResult(11);
        }
    }
}