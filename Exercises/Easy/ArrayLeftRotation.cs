using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Exercises.Easy
{
    /*
        A left rotation operation on an array shifts each of the array's elements 1 unit to the left.
        For example, if 2 left rotations are performed on array [1,2,3,4,5], then the array would become [3,4,5,1,2].

        Given an array a of n integers and a number d, perform d left rotations on the array.
        Return the updated array to be printed as a single line of space-separated integers.

        Constraints
            * 1 <= n <= 10^5
            * 0 <= d <= n
            * 1 <= a[i] <= 10^6
     */
    public class ArrayLeftRotation
    {
        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 2, new int[] { 3, 4, 5, 1, 2 })]
        [InlineData(new int[] { 1 }, 10, new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3, 4, 5 }, 0, new int[] { 1, 2, 3, 4, 5 })]
        public void Scenarios(IEnumerable<int> a, int d, IEnumerable<int> expected)
        {
            var actual = new ArrayLeftRotationSolution().Solve(a, d);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task PerformanceTest()
        {
            var a = Enumerable.Range(0, 1000000);
            var d = 1000000;
            var expected = Enumerable.Range(0, 1000000);

            var solveTask = Task.Run(() =>
            {
                var actual = new ArrayLeftRotationSolution().Solve(a, d);
                Assert.Equal(expected, actual);
            });

            await solveTask.WaitAsync(TimeSpan.FromMilliseconds(2000));
        }
    }
}
