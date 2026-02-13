using System;
using System.Threading.Tasks;
using Xunit;

namespace Exercises.Easy
{
    /*
    Lilah has a string, , of lowercase English letters that she repeated infinitely many times.

    Given an integer, , find and print the number of letter a's in the first  letters of Lilah's infinite string.

    For example, if the string s = "abcac" and n = 10, the substring we consider is "abcacabcac", the first 10 characters
    of her infinite string. There are 4 occurrences of a in the substring.

    constraints:
        * 1 <= |s| <= 100
        * 1 <= n <= 10^12
     */
    public class RepeatedString
    {
        [Theory]
        [InlineData("abcac", 10, 4)]
        [InlineData("abcac", -1, 0)]
        [InlineData("aba", 10, 7)]
        [InlineData("ababa", 3, 2)]
        [InlineData("a", 1000000000000, 1000000000000)]
        public void Scenarios(string s, long n, long expected)
        {
            var actual = new RepeatedStringSolution().Solve(s, n);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task PerformanceTest()
        {
            string s = "a";
            long n = 1000000000000;
            long expected = 1000000000000;
            var solveTask = Task.Run(() =>
            {
                var actual = new RepeatedStringSolution().Solve(s, n);
                Assert.Equal(expected, actual);
            });

            await solveTask.WaitAsync(TimeSpan.FromMilliseconds(500));
        }
    }
}
