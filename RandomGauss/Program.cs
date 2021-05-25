using System;

namespace RandomGauss
{
    internal class Program
    {
        public const int Min = 10;
        public const int Max = 30;
        public const int CountGenerateNumber = 10;
        public const int GraphPeriod = CountGenerateNumber / 100;

        private static void Main(string[] args)
        {
            //var random = new Random();
            //var u1 = random.Next(5, 10);
            //var u2 = random.Next(5, 10);
            //Console.WriteLine($"{u1} {u2}");
            //var data = GenerateGauss(Min, Max, CountGenerateNumber);
            //PrintStat(data, Min, Max);
            //var random = new Random();
            for (int i = 0; i < 100; i++)
            {
                //NextGauss(random, Min, Max);
                //Console.WriteLine(rand_gauss());
                Console.WriteLine(BoxMuller());
                //Console.WriteLine(RandomInternal(10, 20));
            }
            //Console.WriteLine();
            //foreach (var i in data)
            //{
            //    Console.Write($"{i} ");
            //}
            Console.WriteLine();
            //PrintStat(data, Min, Max);
        }

        public static int[] GenerateStandardRandom(int min, int max, int countGenerateNumber)
        {
            var random = new Random();
            var intBuffer = new int[countGenerateNumber];

            for (var i = 0; i < countGenerateNumber; i++)
            {
                intBuffer[i] = random.Next(min, max + 1);
            }

            return intBuffer;
        }

        public static void PrintStat(int[] data, int min, int max)
        {
            var counter = new int[max - min + 1];

            for (var i = 0; i < data.Length; i++)
            {
                counter[data[i] - min]++;
            }

            for (var i = min; i < max + 1; i++)
            {
                Console.Write($"{i}: ");
                for (var j = 0; j < counter[i - min]; j++)
                {
                    if (j % GraphPeriod == 0)
                    {
                        Console.Write($"*");
                    }
                }
                Console.WriteLine();
            }
        }

        public static int[] GenerateGauss(int min, int max, int countNumber)
        {
            var data = new int[countNumber];
            for (int i = 0; i < countNumber; i++)
            {
                data[i] = (int)Gauss(min, max);
            }
            return data;
        }

        public static double Gauss()
        {
            var random = new Random((int)DateTime.Now.Ticks);
            var x = random.NextDouble();
            var stdDev = 1;
            var mean = 0.5;

            var result = (1 / (stdDev * Math.Sqrt(2 * Math.PI)))
                * Math.Exp(-(((x - mean) * (x - mean)) / (2 * stdDev * stdDev)));

            return result;
        }

        public static double Gauss(int min, int max) => (Gauss() * (max - min) + min);

        //
        public static double BoxMuller()
        {
            var random = new Random();
            var r = random.NextDouble();
            var f = random.NextDouble();

            var z0 = Math.Cos(2 * Math.PI * f) * Math.Sqrt(-2 * Math.Log(r));
            var z1 = Math.Sin(2 * Math.PI * f) * Math.Sqrt(-2 * Math.Log(r));

            return z1;
        }
    }
}
