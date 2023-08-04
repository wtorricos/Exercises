﻿using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Exercises.Medium
{
    /// <summary>
    /// “Length of the largest subarray with contiguous elements” states that you are given an integer array.
    /// The problem statement asks to find out the length of the longest contiguous sub-array of which elements
    /// can be arranged in a sequence (continuous, either ascending or descending). The numbers in the array
    /// can occur multiple times.
    ///
    /// Example 1:
    /// arr[] = {10, 12, 13, 11, 8, 10, 11, 10}
    /// output = 4
    ///
    /// Example 2:
    /// arr[] = {1, 1, 3, 2, 8, 4, 8, 10}
    /// output = 3
    /// </summary>
    public sealed class LengthOfContiguousElements
    {
        [Theory]
        [InlineData(new[]{ 1, 5, 7 }, 0)]
        [InlineData(new[]{ 1, 2, 3, 4, 5 }, 5)]
        [InlineData(new[]{ 10, 12, 13, 11, 8, 10, 11, 10 }, 4)]
        [InlineData(new[]{ 1, 1, 3, 2, 8, 4, 8, 10 }, 3)]
        public void Test(int[] arr, int expected)
        {
            int actual = Solve(arr);

            Assert.Equal(expected, actual);
        }

        private int Solve(int[] arr)
        {
            int longest = 0;
            int window = 2;
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i + window; j <= arr.Length; j++)
                {
                    HashSet<int> subArray = new();
                    bool isContiguous = true;
                    for (int k = i; k < j; k++)
                    {
                        if (subArray.Contains(arr[k]))
                        {
                            isContiguous = false;
                            break;
                        }
                        subArray.Add(arr[k]);
                    }

                    int min = subArray.Min();
                    for (int n = min; n < min + subArray.Count; n++)
                    {
                        if (!subArray.Contains(n))
                        {
                            isContiguous = false;
                            break;
                        }
                    }

                    if (!isContiguous)
                    {
                        continue;
                    }

                    longest = subArray.Count > longest ? subArray.Count : longest;
                }
            }

            return longest;
        }
    }
}
