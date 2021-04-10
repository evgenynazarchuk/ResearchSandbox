using System;

//task
//sec	    ms			users		ms / users
//10    	10 000		5			2 000 / 1 = (ms / users)
//10        10 000      50          2 00 / 1 = (ms / users)
//10        10 000      500         20 / 1 = (ms / users)
//10        10 000      5 000       2 / 1 = (ms / users)
//10        10 000      10 000      1 / 1 = (ms / users)
//
//10        10 000      50 000      1 / 5 = (users / ms)
//10        10 000      500 000     1 / 50 = (users / ms)
//10        10 000      5 000 000   1 / 500 = (users / ms)

//1         1 000       10000        1 / 1
//
//
///// 1000 / 10 = 100

namespace CountUsersForPeriod
{
    class Program
    {
        static void Main(string[] args)
        {
            var seconds = 10;
            var totalUsers = 5000000;
            var milliseconds = seconds * 1000;
            var interval = 1;
            var users = 1;

            if (milliseconds > totalUsers)
            {
                interval = milliseconds / totalUsers;
            }
            else
            {
                users = totalUsers / milliseconds;
            }
            
            Console.WriteLine($"every milliseconds {interval}, run users {users}");
        }
    }
}
