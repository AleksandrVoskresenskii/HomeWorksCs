// <copyright file="SkipListTests.cs" company="SPbSU">
//   Copyright (c) SPbSU. All rights reserved.
// </copyright>

namespace SkipListCollection.Tests
{
    using System;
    using Xunit;
    using SkipListCollection;

    public sealed class SkipListTests
    {
        [Fact]
        public void AddAndCount_Works()
        {
            var list = new SkipList<int>();
            Assert.Equal(0, list.Count);

            list.Add(5);
            list.Add(1);
            list.Add(3);

            Assert.Equal(3, list.Count);
        }

        [Fact]
        public void Enumeration_Yields_Sorted()
        {
            var list = new SkipList<int>();
            list.Add(5);
            list.Add(1);
            list.Add(3);

            int[] expected = { 1, 3, 5 };
            int idx = 0;

            foreach (var v in list)
            {
                Assert.Equal(expected[idx], v);
                idx++;
            }
        }

        [Fact]
        public void Indexer_ReturnsByIndex()
        {
            var list = new SkipList<string>();
            list.Add("b");
            list.Add("a");
            list.Add("c");

            Assert.Equal("a", list[0]);
            Assert.Equal("b", list[1]);
            Assert.Equal("c", list[2]);
        }

        [Fact]
        public void Remove_Works()
        {
            var list = new SkipList<int>();
            list.Add(2);
            list.Add(1);
            list.Add(3);

            Assert.True(list.Remove(2));
            Assert.False(list.Contains(2));
            Assert.Equal(2, list.Count);
        }

        [Fact]
        public void Clear_Works()
        {
            var list = new SkipList<int>();
            list.Add(1);

            list.Clear();

            Assert.Empty(list);
        }

        [Fact]
        public void CopyTo_Works()
        {
            var list = new SkipList<int>();
            list.Add(2);
            list.Add(1);
            list.Add(3);

            int[] arr = new int[5];
            list.CopyTo(arr, 1);

            Assert.Equal(new[] { 0, 1, 2, 3, 0 }, arr);
        }

        [Fact]
        public void InvalidArgs_Throw()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new SkipList<int>(0, 0.5));
            Assert.Throws<ArgumentOutOfRangeException>(() => new SkipList<int>(5, 1));

            var list = new SkipList<int>();
            Assert.Throws<ArgumentNullException>(() => list.Add(null!));
            Assert.Throws<ArgumentOutOfRangeException>(() => { var x = list[0]; });
            Assert.Throws<ArgumentOutOfRangeException>(() => list.RemoveAt(0));
        }
    }
}
