using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Linq;

namespace AsyncAccumulator
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentQueue<int> queue = new();
            List<int> list = new();
            List<Task> tasks = new();

            foreach(var i in Enumerable.Range(1, 10000))
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    queue.Enqueue(i);
                }));
            }

            Task.WaitAll(tasks.ToArray());

            foreach (var i in Enumerable.Range(1, 10000))
            {
                queue.TryDequeue(out int item);
                list.Add(item);
            }

            list.Sort();

            for (int i = 0; i < 10000 - 1; i++)
            {
                if (list[i] != list[i + 1] - 1)
                {
                    Console.WriteLine("Error");
                }
            }
        }
    }
}
