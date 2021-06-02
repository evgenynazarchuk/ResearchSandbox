using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;

namespace TaskProcessIdPrint
{
    static class Config
    {
        public const int N = 1000000;
    }
    class Program
    {
        static void Main()
        {
            List<Task> tasks = new();
            ConcurrentDictionary<int, int> counter = new();

            for (int i = 0; i < Config.N; i++)
            {
                tasks.Add(Task.Run(async () =>
                {
                    await Task.Delay(1000);

                    var n = Thread.CurrentThread.ManagedThreadId;
                    if (counter.TryGetValue(n, out int count))
                    {
                        var newCount = count + 1;
                        counter.TryUpdate(n, newCount, count);
                    }
                    else
                    {
                        counter.TryAdd(n, 1);
                    }
                    //counter.AddOrUpdate(1, 1, (n, oldCount) => oldCount + 1); // do not work

                    //Console.Write($"{n} ");
                }));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine();
            foreach ((var processId, var count) in counter)
            {
                Console.WriteLine($"ProcessId {processId} = {count}");
            }
            Console.WriteLine($"Count ProcessId: {counter.Count}");

            Console.ReadKey();
        }
    }
}
