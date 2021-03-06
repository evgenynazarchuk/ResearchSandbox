using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RandomTimer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            for (var i = 0; i < 10; i++)
            {
                Trace(() => Wait(3.Seconds()));
                Trace(() => WaitMaximum(5.Seconds()));
                Trace(() => WaitMinimum(3.Seconds(), 7.Seconds()));
                Console.WriteLine($"-----------------");
            }
        }

        public static void Trace(Action action)
        {
            var watch = new Stopwatch();
            watch.Start();
            action();
            watch.Stop();
            Console.WriteLine($"{watch.ElapsedMilliseconds}");
        }

        public static void Wait(TimeSpan time) => Task.Delay(time).GetAwaiter().GetResult();

        public static void Wait(params Task[] tasks) => Task.WaitAll(tasks);

        public static void Wait(Task[,] tasks)
        {
            ;
        }

        public static void Wait(Task[][] tasks)
        {
            for (var i = 0; i < tasks.Length; i++)
            {
                Task.WaitAll(tasks[i]);
            }
        }

        public static void WaitMaximum(TimeSpan maximumTime)
        {
            var random = new Random();
            var wait = random.Next(0, (int)maximumTime.TotalMilliseconds);
            Task.Delay(wait).GetAwaiter().GetResult();
        }

        public static void WaitMinimum(TimeSpan minimumTime, TimeSpan maximumTime)
        {
            var random = new Random();
            var wait = random.Next((int)minimumTime.TotalMilliseconds, (int)maximumTime.TotalMilliseconds);
            Task.Delay(wait).GetAwaiter().GetResult();
        }
    }

    public static class IntExt
    {
        public static TimeSpan Seconds(this int n)
        {
            return TimeSpan.FromSeconds(n);
        }
    }
}
