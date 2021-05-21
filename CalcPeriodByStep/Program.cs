using System;

namespace CalcPeriodByStep
{
    class Program
    {
        // it is wrong!!!!!! use simple div :)
        static void Main(string[] args)
        {
            Console.WriteLine($"(1, 1, 1) {CalcPeriodsCount(1, 1, 1)}");
            //Console.WriteLine($"{CalcPeriodsCount(1, 1, 2)}");
            Console.WriteLine($"(1, 2, 1) {CalcPeriodsCount(1, 2, 1)}");
            Console.WriteLine($"(1, 2, 2) {CalcPeriodsCount(1, 2, 2)}");
            Console.WriteLine($"(1, 3, 2) {CalcPeriodsCount(1, 3, 2)}");
            Console.WriteLine($"(1, 4, 2) {CalcPeriodsCount(1, 4, 2)}");
            Console.WriteLine($"(1, 5, 2) {CalcPeriodsCount(1, 5, 2)}");
            Console.WriteLine($"(1, 5, 3) {CalcPeriodsCount(1, 5, 3)}");

            Console.WriteLine($"(2, 1, 1) {CalcPeriodsCount(2, 1, 1)}");
            Console.WriteLine($"(2, 2, 1) {CalcPeriodsCount(2, 2, 1)}");
            Console.WriteLine($"(2, 2, 2) {CalcPeriodsCount(2, 2, 2)}");
            //Console.WriteLine($"{CalcPeriodsCount(2, 1, 2)}");
        }

        private static int CalcPeriodsCount(int start, int end, int stepsCount)
        {
            int periodsCount = 0;

            if (start <= end)
            {
                while (start <= end)
                {
                    periodsCount++;
                    start += stepsCount;
                }
            }
            else if (start > end)
            {
                while (start > end)
                {
                    periodsCount++;
                    start -= stepsCount;
                }
            }

            return periodsCount;
        }
    }
}
