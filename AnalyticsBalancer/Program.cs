using System;

namespace AnalyticsBalancer
{
    class Program
    {
        static void Main(string[] args)
        {
            CalcUsersForPeriods(25, TimeSpan.FromSeconds(15));
            //var duration = TimeSpan.FromSeconds(120);
            //var period = TimeSpan.FromSeconds(60);
            //var countPeriods = TimeSpan.FromSeconds(duration / period);
            //
            //Console.WriteLine($"{countPeriods}");
            //
            //Console.WriteLine($"{duration / countPeriods}");
        }

        //public int[] NewUserByPeriod(int maxUsers, TimeSpan performanceDuration, TimeSpan period)
        //{
        //    if (period > performanceDuration)
        //        throw new ApplicationException("period must be > performanceDuration");
        //
        //    var countPeriods = performanceDuration / period;
        //    if(countPeriods < 1)
        //
        //    var usersByPeriods = new int[countPeriods];
        //    for (int i = 0; i < maxUsers; i++)
        //    {
        //        
        //    }
        //    return usersByPeriods;
        //}

        public static int[] CalcUsersForPeriods(int totalUser, TimeSpan duration)
        {
            var countPeriods = (int)duration.TotalSeconds;
            var usersSchedule = new int[countPeriods];
            var usersStep = totalUser / countPeriods;
            var currentUsersStep = usersStep;

            for (var i = 0; i < countPeriods; i++)
            {
                Console.Write($"{currentUsersStep} ");
                currentUsersStep += usersStep;
            }
            return usersSchedule;
        }
    }
}
