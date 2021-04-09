using System;
using System.Threading.Tasks;

namespace AsyncAwait
{
    class Program
    {
        static void Main(string[] args)
        {
            CallFunc1Func2();
        }

        public static void CallFunc1Func2()
        {
            var t1 = Func1();
            var t2 = Func2();
            Task.WaitAll(t1, t2); // Func2, Func1
        }

        public static async Task Func1()
        {
            await Task.Delay(500);
            Console.WriteLine($"{nameof(Func1)}");
        }

        public static async Task Func2()
        {
            await Task.Delay(100);
            Console.WriteLine($"{nameof(Func2)}");
        }
    }
}
