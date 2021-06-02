using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TimeCreateTask
{
    class Program
    {
        static async Task Main()
        {
            //Stopwatch watch = new();

            // 900 ~ 15 min
            //for (int j = 0; j < 900; j++)
            //{
            //    watch.Start();
            //    for (int i = 0; i < 50_000; i++)
            //    {
            //        InvokeAsync();
            //    }
            //    watch.Stop();
            //    Console.WriteLine($"Created tasks time: {watch.ElapsedMilliseconds} ms");
            //
            //    if(TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds) > TimeSpan.FromSeconds(1))
            //    {
            //        Console.Beep();
            //    }
            //    watch.Reset();
            //}

            System.Timers.Timer timer = new(1000);
            Stopwatch watch = new();

            timer.Elapsed += (object sender, System.Timers.ElapsedEventArgs e) =>
            {
                watch.Start();
                for (int i = 0; i < 50_000; i++)
                {
                    InvokeAsync();
                }
                watch.Stop();
                Console.WriteLine($"Created tasks time: {watch.ElapsedMilliseconds} ms");

                if (TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds) > TimeSpan.FromSeconds(1))
                {
                    Console.Beep();
                }
                watch.Reset();
            };
            timer.Start();
            

            Console.ReadKey();
        }
        static async Task InvokeAsync()
        {
            await Task.Run(async () => await Task.Delay(1000));
            await Task.Run(async () => await Task.Delay(1000));
            await Task.Run(async () => await Task.Delay(1000));
            await Task.Run(async () => await Task.Delay(1000));
            await Task.Run(async () => await Task.Delay(1000));
            await Task.CompletedTask;
        }
    }
}
