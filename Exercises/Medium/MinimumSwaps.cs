using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Exercises.Medium
{
    /*
     You are given an unordered array consisting of consecutive integers
     from the set [1, 2, 3, ..., n] without any duplicates. You are allowed to swap
     any two elements. You need to find the minimum number of swaps required to sort
     the array in ascending order.

     For example, given the array arr=[7,1,3,2,4,5,6] we perform the following steps:
            [7,1,3,2,4,5,6]
        1.  [1,7,3,2,4,5,6] swap(7,1)
        2.  [1,2,3,7,4,5,6] swap(7,2)
        3.  [1,2,3,4,7,5,6] swap(7,4)
        4.  [1,2,3,4,5,7,6] swap(7,5)
        5.  [1,2,3,4,5,6,7] swap(7,6)
        It took 5 swaps to sort the array.

     The function must return the miminum number of swaps to sort the array.
     Constraints:
        1 <= n <= 10^5
        1 <= arr[i] <= n
     */
    public class MinimumSwaps
    {
        [Theory]
        [InlineData(new int[] { 7, 1, 3, 2, 4, 5, 6 }, 5)]
        [InlineData(new int[] { 4, 3, 1, 2 }, 3)]
        public void Scenarios(IEnumerable<int> arr, int expected)
        {
            var actual = new MinimumSwapsSolution().Solve(arr);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Performance()
        {
            var arr = Enumerable.Range(1, 1000000);

            var solveTask = Task.Run(() =>
            {
                var actual = new MinimumSwapsSolution().Solve(arr);
                Assert.Equal(0, actual);
            });

            await solveTask.WaitAsync(TimeSpan.FromMilliseconds(2000));
        }
    }
}
