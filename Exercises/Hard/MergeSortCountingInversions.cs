using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Exercises.Hard
{
    public class MergeSortCountingInversions
    {
        /*
        In an array, arr, the elements at indices i and j (where i < j) form an inversion if
        arr[i] > arr[j]. In other words, inverted elements arr[i] and arr[j] are considered
        to be "out of order". To correct an inversion, we can swap adjacent elements.

        For example, consider the dataset [2,4,1]. It has two inversions:
        (4,1) and (2,1). To sort the array, we must perform the following two swaps to
        correct the inversions:
            arr[2,4,1] = swap(arr[1], arr[2]) -> swap(arr[0], arr[1]) -> [1,2,4]

        Constraints
        1 <= n <= 10^5 //number of elements in the array
        1 <= arr[i] <= 10^7

        */
        [Theory]
        [InlineData(new int[]{1,1,1,2,2}, 0L)]
        [InlineData(new int[]{2,1,3,1,2}, 4L)]
        public void Scenarios(int[] arr, long expected)
        {
            var (sorted, actual) = MergeSortCountingInversionsSolution.SortAndCount(arr);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Performance()
        {
            var arr = Enumerable.Repeat(1, 10^5).ToArray();

            var solveTask = Task.Run(() =>
            {
                var actual = MergeSortCountingInversionsSolution.SortAndCount(arr);

                Assert.Equal(0, actual.Item2);
            });

            await solveTask.WaitAsync(TimeSpan.FromMilliseconds(2000));
        }
    }
}
