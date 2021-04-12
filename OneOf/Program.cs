using System;

namespace OneOf
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            for (var i = 0; i < 10; i++)
            {
                OneOf(
                    Func1,
                    Func2,
                    () => Func3(42)
                );
            }

            for (var i = 0; i < 10; i++)
            {
                OneOf(
                    () => Func4(i)
                );
            }
        }

        public static void OneOf(params Action[] actions)
        {
            var random = new Random();
            var randomNumber = random.Next(0, actions.Length);
            actions[randomNumber]();
        }

        public static void Func1() => Console.WriteLine($"1");
        public static void Func2() => Console.WriteLine($"2");
        public static void Func3(int n) => Console.WriteLine($"{n}");
        public static void Func4(int i) => Console.WriteLine($"4: {i}");
    }
}
