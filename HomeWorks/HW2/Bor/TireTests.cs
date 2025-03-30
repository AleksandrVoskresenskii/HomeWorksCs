// Модульные тесты Trie с использованием NUnit
using NUnit.Framework;
using TrieHomework;

namespace TrieHomework.Tests;

[TestFixture]
public class TrieTests
{
    private Trie _trie;

    [SetUp]
    public void Setup()
    {
        _trie = new Trie();
    }

    [Test]
    public void Add_NewElement_ReturnsTrue()
    {
        Assert.IsTrue(_trie.Add("test"));
        Assert.AreEqual(1, _trie.Size);
    }

    [Test]
    public void Add_ExistingElement_ReturnsFalse()
    {
        _trie.Add("test");
        Assert.IsFalse(_trie.Add("test"));
        Assert.AreEqual(1, _trie.Size);
    }

    [Test]
    public void Contains_ElementExists_ReturnsTrue()
    {
        _trie.Add("hello");
        Assert.IsTrue(_trie.Contains("hello"));
    }

    [Test]
    public void Contains_ElementDoesNotExist_ReturnsFalse()
    {
        Assert.IsFalse(_trie.Contains("world"));
    }

    [Test]
    public void Remove_ExistingElement_ReturnsTrue()
    {
        _trie.Add("delete");
        Assert.IsTrue(_trie.Remove("delete"));
        Assert.IsFalse(_trie.Contains("delete"));
        Assert.AreEqual(0, _trie.Size);
    }

    [Test]
    public void Remove_NonExistingElement_ReturnsFalse()
    {
        Assert.IsFalse(_trie.Remove("nonexistent"));
    }

    [Test]
    public void HowManyStartsWithPrefix_CorrectCount()
    {
        _trie.Add("apple");
        _trie.Add("app");
        _trie.Add("application");
        _trie.Add("banana");

        Assert.AreEqual(3, _trie.HowManyStartsWithPrefix("app"));
        Assert.AreEqual(1, _trie.HowManyStartsWithPrefix("ban"));
        Assert.AreEqual(0, _trie.HowManyStartsWithPrefix("cat"));
    }

    [Test]
    public void Size_CorrectCountAfterMultipleOperations()
    {
        _trie.Add("one");
        _trie.Add("two");
        _trie.Add("three");
        _trie.Remove("two");
        _trie.Remove("four");

        Assert.AreEqual(2, _trie.Size);
    }
}