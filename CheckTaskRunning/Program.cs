using System;
using System.Threading.Tasks;

namespace CheckTaskRunning
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Task t = Task.Run(async () =>
            {
                Console.WriteLine("Start");
                await Task.Delay(5000); // ???????????????? do not work!!!!!!!
                Console.WriteLine("End");
            });
            Console.WriteLine(t.Status);
            Console.WriteLine(t.Status);
            Task.WaitAll(t);
            Console.WriteLine(t.Status);
            //await Task.Delay(6000);
            Console.WriteLine(t.Status);
        }
    }
}
