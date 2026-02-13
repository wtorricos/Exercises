using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Exercises.Medium
{
    /*
        It's New Year's Day and everyone's in line for the Wonderland rollercoaster ride!
        There are a number of people queued up, and each person wears a sticker indicating their
        initial position in the queue. Initial positions increment by 1 from 1 at the front of the line
        to n at the back.

        Any person in the queue can bribe the person directly in front of them to swap positions.
        If two people swap positions, they still wear the same sticker denoting their original places in line.
        One person can bribe at most two others.

        For example if n=8 and person 5 brives person 4 then the queue looks like this: 1,2,3,5,4,6,7,8

        Fascinated by this chaotic queue, you decide you must know the minimum number of bribes that took place
        to get the queue into its current state!

        Input:
        Q -> An array of numbers where
            t -> Is the first number in Q and contains the number of test cases
            n -> The number of people in the queue
            q -> an array of integers denoting the current state of the queue

        Output
        The minimum number of brives or -1 if the configuration is not possible.

        Constraints
        1 <= t <= 10
        1 <= n <= 10^5
     */
    public class NewYearChaos
    {
        [Theory]
        [InlineData(
            new int[]
            {
                1, // number of test cases
                2, // number of people in the queue
                2, 1 // queue 1
            },
            new int[]
            {
                1, // expected for queue 1
            })]
        [InlineData(
            new int[]
            {
                2, // number of test cases
                5, // number of people in the queue
                2, 1, 5, 3, 4, // queue 1
                5, // number of people in the queue
                2, 5, 1, 3, 4 // queue 2
            },
            new int[]
            {
                3, // expected for queue 1
                -1 // expected for queue 2
            })]
        [InlineData(
            new int[]
            {
                1,
                8,
                1, 2, 5, 3, 4, 7, 8, 6
            },
            new int[] { 4 })]
        [InlineData(
            new int[]
            {
                2,
                8,
                5, 1, 2, 3, 7, 8, 6, 4,
                8,
                1, 2, 5, 3, 7, 8, 6, 4
            },
            new int[] { -1, 7 })]
        public void Scenarios(IEnumerable<int> input, IEnumerable<int> expected)
        {
            //var actual = new NewYearChaosCuadraticSolution().Solve(input);
            var actual = new NewYearChaosSolution().Solve(input);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task PerformanceTest()
        {
            var input = new List<int> { 10 }; //test cases;
            var n = 100000; // q length
            var q = Enumerable.Range(1, n);
            for (var i = 0; i < 10; i++)
            {
                input.Add(n);
                input.AddRange(q);
            }
            var expected = Enumerable.Range(1, n).Select(i => 0).ToList();

            var solveTask = Task.Run(() =>
            {
                var actual = new NewYearChaosSolution().Solve(input);
                Assert.Equal(expected, actual);
            });

            await solveTask.WaitAsync(TimeSpan.FromMilliseconds(2000));
        }
    }
}
