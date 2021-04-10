using System;
using System.Threading.Tasks;

namespace CheckTaskRunning
{
    class Program
    {
        static async Task Main(string[] args)
        {
            SecondSample();
        }

        public static void SecondSample()
        {
            var countTask = 10;
            var tasks = new Task[countTask];
            Action<object> action = async (i) => Console.WriteLine($"{i}");

            for (int i = 0; i < countTask; i++)
            {
                tasks[i] = Task.Factory.StartNew(action, i);
            }

            Task.WaitAll(tasks);
        }

        public static void FirstSample()
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
