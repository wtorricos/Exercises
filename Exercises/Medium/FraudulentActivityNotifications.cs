using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Exercises.Medium
{
    // https://www.hackerrank.com/challenges/fraudulent-activity-notifications/problem?h_l=interview&playlist_slugs%5B%5D=interview-preparation-kit&playlist_slugs%5B%5D=sorting&h_r=next-challenge&h_v=zen
    /*
        Fraudulent Activity Notifications

        HackerLand National Bank has a simple policy for warning clients about possible fraudulent account
        activity. If the amount spent by a client on a particular day is greater than or equal to 2X the client's
        median spending for a trailing number of days, they send the client a notification about potential fraud.
        The bank doesn't send the client any notifications until they have at least that trailing number of prior
        days' transaction data.
        Given the number of trailing days d and a client's total daily expenditures for a period of n days,
        find and print the number of times the client will receive a notification over all n days.

        For example, d=3 and expenditures=[10, 20, 30, 40, 50]. On the first three days, they just collect
        spending data. At day 4, we have trailing expenditures of [10,20,30]. The median is 20 and the day's
        expenditure is 40. Because 40 >= 2 * 20, there will be a notice. The next day, our trailing expenditures
        are [20,30,40] and the expenditures are 50. This is less than 2*30 so no notice will be sent.
        Over the period, there was one notice sent.

        Note: The median of a list of numbers can be found by arranging all the numbers from smallest to greatest.
        If there is an odd number of numbers, the middle one is picked. If there is an even number of numbers,
        median is then defined to be the average of the two middle values.

        Input:
        two space separated integers n and d the number of days of transaction data, and the number of
        trailing days' data used to calculate median spending.
        The second line contains  space-separated non-negative integers where each integer i denotes
        expenditure[i]
        Output
        The number of notifications n

        Constraints
        1 <= n <= 2*10^5
        1 <= d <= n
        0 <= expenditure[i] <= 200
     */
    public class FraudulentActivityNotifications
    {
        [Theory]
        [InlineData("9 5", "2 3 4 2 3 6 8 4 5", 2)]
        [InlineData("5 3", "10 20 30 40 50", 1)]
        [InlineData("5 4", "1 2 3 4 4", 0)]
        public void Scenarios(string in1, string in2, int expected)
        {
            var args1 = in1.Split(' ');
            var d = int.Parse(args1[1]);
            var expenditure = in2.Split(' ').Select(c => int.Parse(c)).ToArray();
            var actual = FraudulentActivityNotificationsSolution.Solve(expenditure, d);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Performance()
        {
            var d = 10000;
            var expenditure = Enumerable.Repeat(1, 100000).ToArray();
            var solveTask = Task.Run(() =>
            {
                var actual = FraudulentActivityNotificationsSolution.Solve(expenditure, d);
                Assert.Equal(0, d);
            });

            await solveTask.WaitAsync(TimeSpan.FromMilliseconds(5000));
        }
    }
}
