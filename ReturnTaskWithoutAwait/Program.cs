using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ReturnTaskWithoutAwait
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var watch = new Stopwatch();

            watch.Start();
            CheckWait1(1000, 1000000);
            watch.Stop();

            Console.WriteLine($"ms: {watch.ElapsedMilliseconds}");
            watch.Reset();

            watch.Start();
            CheckWait2(1000, 1000000);
            watch.Stop();

            Console.WriteLine($"ms: {watch.ElapsedMilliseconds}");

            // нет разницы
        }

        public static void CheckWait1(int n, int m)
        {
            var result = 0;

            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < m; j++)
                {
                    result += Wait1().Result;
                }
            }

            Console.WriteLine($"Result: {result}");
        }

        public static void CheckWait2(int n, int m)
        {
            var result = 0;

            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < m; j++)
                {
                    result += Wait1().Result;
                }
            }

            Console.WriteLine($"Result: {result}");
        }

        public static Task<int> Wait1()
        {
            return Task.FromResult(1);
        }

        public static async Task<int> Wait2()
        {
            var i = await Task.FromResult(1);
            return i;
        }
    }
}
