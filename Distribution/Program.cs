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

            var countActiveUsers = new int[seconds];
            var numberOfAscendingSteps = seconds / 2;
            var increment = users / (numberOfAscendingSteps);
            var countUsers = increment;

            for (var i = 0; i < countActiveUsers.Length / 2; i++)
            {
                countActiveUsers[i] = countUsers;
                countActiveUsers[i + numberOfAscendingSteps] = users - countUsers;
                countUsers += increment;
            }

            for (var i = 0; i < countActiveUsers.Length; i++)
            {
                Console.Write($"{countActiveUsers[i]} ");
            }
            Console.WriteLine();
        }
    }
}
