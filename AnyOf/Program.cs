using System;

namespace MayBeCallApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MayBeCall(0.5, () =>
            {
                Func1("func1");
            });
        }

        public static void Func1(string msg) => Console.WriteLine(msg);

        public static void MayBeCall(double probability, Action act)
        {
            var random = new Random(DateTime.Now.Millisecond);
            if (probability > random.NextDouble())
            {
                act();
            }
        }
    }
}
