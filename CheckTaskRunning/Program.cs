using System;
using System.Threading.Tasks;

namespace CheckTaskRunning
{
    class Program
    {
        static async Task Main(string[] args)
        {
            FirstSample();
            //SecondSample();
        }

        public static void FirstSample()
        {
            Task t = Task.Run(async () =>
            {
                Console.WriteLine("App: start");
                await Task.Delay(5000); // work
                Console.WriteLine("App: end");
            });
            //Task t = Task.Factory.StartNew(() =>
            //{
            //    Console.WriteLine("App: start");
            //    Task.Delay(5000).Wait(); // work
            //    Console.WriteLine("App: end");
            //});
            //Task t = Task.Factory.StartNew(async () =>
            //{
            //    Console.WriteLine("App: start");
            //    await Task.Delay(5000); // do not work!!!!!!!!!!!!!!
            //    Console.WriteLine("App: end");
            //});

            Console.WriteLine(t.Status);
            Console.WriteLine(t.Status);
            Task.WaitAll(t);
            Console.WriteLine(t.Status);
        }

        public static void SecondSample()
        {
            var countTask = 100_000;
            var tasks = new Task[countTask];
            Action<object> action = (i) => Console.WriteLine($"{i}");

            for (int i = 0; i < countTask; i++)
            {
                tasks[i] = Task.Factory.StartNew(action, i);
            }

            Task.WaitAll(tasks);
        }
    }
}
