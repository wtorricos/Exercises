﻿using Xunit;

namespace Exercises.Easy
{
    /*
        Vanya and Table
        time limit per test2 seconds
        memory limit per test256 megabytes
        inputstandard input
        outputstandard output
        Vanya has a table consisting of 100 rows, each row contains 100 cells. The rows are numbered by integers from 1 to 100 from bottom to top, the columns are numbered from 1 to 100 from left to right.

        In this table, Vanya chose n rectangles with sides that go along borders of squares (some rectangles probably occur multiple times). After that for each cell of the table he counted the number of rectangles it belongs to and wrote this number into it. Now he wants to find the sum of values in all cells of the table and as the table is too large, he asks you to help him find the result.

        Input
        The first line contains integer n (1 ≤ n ≤ 100) — the number of rectangles.

        Each of the following n lines contains four integers x1, y1, x2, y2 (1 ≤ x1 ≤ x2 ≤ 100, 1 ≤ y1 ≤ y2 ≤ 100), where x1 and y1 are the number of the column and row of the lower left cell and x2 and y2 are the number of the column and row of the upper right cell of a rectangle.

        Output
        In a single line print the sum of all values in the cells of the table.
     */
    public class VanyaAndTable
    {
        [Theory]
        [InlineData(
            2,
            new string[]
            {
                "1 1 2 3",
                "2 2 3 3"
            },
            10)]
        [InlineData(
            2,
            new string[]
            {
                "1 1 3 3",
                "1 1 3 3"
            },
            18)]
        public void Scenarios(int n, string[] rows, long expected)
        {
            var actual = VanyaAndTableSolution.Solve(n, rows);

            Assert.Equal(expected, actual);
        }
    }
}
