using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GetNumFromEnumerableWithTask
{
    class Program
    {
        static void Main()
        {
            const int countTask = 1000;
            var intList = new List<int>();
            var tasks = new Task[countTask];

            for (int i = 1; i <= countTask; i++)
            {
                intList.Add(i);
            }

            for (int i = 0; i < countTask; i++)
            {
                tasks[i] = PrintNumFromEnumarable(intList[i]);
            }

            Task.WaitAll(tasks);
        }

        public static async Task PrintNumFromEnumarable(int i)
        {
            Console.Write($"{i} ");
        }
    }
}
