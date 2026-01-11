using FsCheck;
using FsCheck.Fluent;
using FsCheck.Xunit;
using System.Collections.Generic;
using System.Linq;

namespace Exercises.DataStructures
{
    public class PriorityQueueTest
    {
        [Property]
        public Property TheElementWithMostPriorityIsAlwaysPopedFirst(int[] arr)
        {
            var sorted = arr.OrderBy(i => i).ToList();
            var sut = new IntMinHeap();

            foreach (var i in arr) sut.Add(i);
            var actual = new int[arr.Length];
            for (var i = 0; i < arr.Length; i++) actual[i] = sut.Pop();

            return (sorted.SequenceEqual(actual)).ToProperty();
        }

        [Property]
        public Property RandomOperations(PriorityQueueTestOperations[] operations, int[] arr)
        {
            var oracle = new List<int>();
            var sut = new IntMinHeap();

            var i = -1;
            foreach (var op in operations)
            {
                if (op == PriorityQueueTestOperations.Add)
                {
                    i++;
                    if (arr.Length <= i) continue;
                    oracle.Add(arr[i]);
                    oracle = oracle.OrderBy(e => e).ToList();
                    sut.Add(arr[i]);
                    if (oracle.Count != sut.Length) return (oracle.Count == sut.Length).ToProperty();
                }
                if (op == PriorityQueueTestOperations.Length)
                {
                    if (oracle.Count != sut.Length) return (oracle.Count == sut.Length).ToProperty();
                }
                if (op == PriorityQueueTestOperations.Pop)
                {
                    if (!oracle.Any()) continue;
                    var expected = oracle.ElementAt(0);
                    oracle.RemoveAt(0);
                    var actual = sut.Pop();

                    if (expected != actual) return (expected == actual).ToProperty();
                }
                if (op == PriorityQueueTestOperations.Peek)
                {
                    if (!oracle.Any()) continue;
                    var expected = oracle.ElementAt(0);
                    var actual = sut.Peek();

                    if (expected != actual) return (expected == actual).ToProperty();
                    if (oracle.Count != sut.Length) return (oracle.Count == sut.Length).ToProperty();
                }
            }

            return (true).ToProperty();
        }

        public enum PriorityQueueTestOperations
        {
            Add,
            Pop,
            Peek,
            Length
        }
    }
}
