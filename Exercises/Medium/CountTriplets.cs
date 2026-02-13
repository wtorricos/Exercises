using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Exercises.Medium
{
    /*
    You are given an array and you need to find number of tripets of indices (i,j,k)
    such that the elements at those indices are in geometric progression (https://en.wikipedia.org/wiki/Geometric_progression)
    for a given common ratio r and i < j < k.

    For example, arr=[1,4,16,64]. If r=4, we have [1,4,16] and [4,16,64] at indices
    [0,1,2] and [1,2,3].

    Constraints
    Given an array arr of length n and a ratio r:
        * 1 <= n <= 10^5
        * 1 <= r <= 10^9
        * 1 <= arr[i] <= 10^9
     */
    public class CountTriplets
    {
        [Theory]
        [InlineData(new long[] { 1, 2, 2, 4 }, 2, 2)]
        [InlineData(new long[] { 2, 1, 4 }, 2, 0)]
        [InlineData(new long[] { 1, 3, 9, 9, 27, 81 }, 3, 6)]
        [InlineData(new long[] { 1, 5, 5, 25, 125 }, 5, 4)]
        public void Scenarios(long[] arr, long r, long expected)
        {
            var actual = new CountTripletsSolution().Solve(arr, r);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Scenario1()
        {
            // small arr
            var arr = Enumerable.Range(1, 4)
                .Select(i => 1L)
                .ToArray();
            var r = 1;
            var expected = 4;

            var actual = new CountTripletsSolution().Solve(arr, r);

            Assert.Equal(expected, actual);

            // larger arr
            arr = Enumerable.Range(1, 100)
                .Select(i => 1L)
                .ToArray();
            r = 1;
            expected = 161700;

            actual = new CountTripletsSolution().Solve(arr, r);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Performance()
        {
            var arr = Enumerable.Range(1, 100000)
                .Select(i => (long)1237)
                .ToArray();
            var r = 1;

            var solveTask = Task.Run(() =>
            {
                var actual = new CountTripletsSolution().Solve(arr, r);
                Assert.Equal(166661666700000, actual);
            });

            await solveTask.WaitAsync(TimeSpan.FromMilliseconds(2000));
        }
    }
}
