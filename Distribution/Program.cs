using System;

namespace Distribution
{
    class Program
    {
        //50 за 11 шагов
        //50 за 5 шагов(т.е. 10 секунда)
        //10 20 30 40 50 ... на 1 меньше, итого 9
        //50 за 10 шагов(т.е. 20 секунда)
        //5 10 15 20 25 30 35 40 45 50 ... на 1 меньше, итого 19

        static void Main(string[] args)
        {
            var users = 10;
            var seconds = 30;

            if (seconds % 2 == 1)
            {
                seconds -= 1;
            }

            var countActiveUsersPerSecond = new int[seconds];
            var numberOfAscendingStepsPerSecond = seconds / 2;
            var increment = users / (numberOfAscendingStepsPerSecond);
            var needUsersForCurrentSecond = increment;

            for (var i = 0; i < countActiveUsersPerSecond.Length / 2; i++)
            {
                countActiveUsersPerSecond[i] = needUsersForCurrentSecond;
                countActiveUsersPerSecond[i + numberOfAscendingStepsPerSecond] = users - needUsersForCurrentSecond;
                needUsersForCurrentSecond += increment;
            }

            for (var i = 0; i < countActiveUsersPerSecond.Length; i++)
            {
                Console.Write($"{countActiveUsersPerSecond[i]} ");
            }
            Console.WriteLine();
        }
    }
}
