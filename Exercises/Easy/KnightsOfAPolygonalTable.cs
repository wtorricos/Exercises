using System;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Exercises.Easy
{
    /*
        Knights of a Polygonal Table
        time limit per test1 second
        memory limit per test256 megabytes
        inputstandard input
        outputstandard output
        Unlike Knights of a Round Table, Knights of a Polygonal Table deprived of nobility and happy to kill each other. But each knight has some power and a knight can kill another knight if and only if his power is greater than the power of victim. However, even such a knight will torment his conscience, so he can kill no more than 𝑘 other knights. Also, each knight has some number of coins. After a kill, a knight can pick up all victim's coins.

        Now each knight ponders: how many coins he can have if only he kills other knights?

        You should answer this question for each knight.

        Input
        The first line contains two integers 𝑛 and 𝑘 (1≤𝑛≤105,0≤𝑘≤min(𝑛−1,10)) — the number of knights and the number 𝑘 from the statement.

        The second line contains 𝑛 integers 𝑝1,𝑝2,…,𝑝𝑛 (1≤𝑝𝑖≤109) — powers of the knights. All 𝑝𝑖 are distinct.

        The third line contains 𝑛 integers 𝑐1,𝑐2,…,𝑐𝑛 (0≤𝑐𝑖≤109) — the number of coins each knight has.

        Output
        Print 𝑛 integers — the maximum number of coins each knight can have it only he kills other knights.

        Examples
        input
        4 2
        4 5 9 7
        1 2 11 33
        output
        1 3 46 36

        Explanation
        The first knight is the weakest, so he can't kill anyone. That leaves him with the only coin he initially has.
        The second knight can kill the first knight and add his coin to his own two.
        The third knight is the strongest, but he can't kill more than 𝑘=2 other knights. It is optimal to kill the second and the fourth knights: 2+11+33=46.
        The fourth knight should kill the first and the second knights: 33+1+2=36.
     */
    public class KnightsOfAPolygonalTable
    {
        [Theory]
        [InlineData("4 2", "4 5 9 7", "1 2 11 33", "1 3 46 36 ")]
        [InlineData("4 0", "4 5 9 7", "1 2 11 33", "1 2 11 33 ")]
        [InlineData("5 1", "1 2 3 4 5", "1 2 3 4 5", "1 3 5 7 9 ")]
        [InlineData("1 0", "2", "3", "3 ")]
        [InlineData("7 1", "2 3 4 5 7 8 9", "0 3 7 9 5 8 9", "0 3 10 16 14 17 18 ")]

        public void Scenarios(string input1, string input2, string input3, string expected)
        {
            var actual = KnightsOfAPolygonalTableSolution.Solve(input1, input2, input3);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task Performance()
        {
            var solveTask = Task.Run(() =>
            {
                var input1 = "100000 1";
                var input2 = new StringBuilder();
                var expected = new StringBuilder();
                for (var i = 0; i < 100000; i++)
                {
                    if (i == 0) expected.Append("1 ");
                    if (i == 99999) input2.Append("1");
                    else input2.Append("1 ");
                    expected.Append("2 ");
                }

                KnightsOfAPolygonalTableSolution.Solve(input1, input2.ToString(), input2.ToString());
            });

            await solveTask.WaitAsync(TimeSpan.FromMilliseconds(2000));
        }
    }
}
