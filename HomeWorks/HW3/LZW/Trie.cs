namespace LZW;

public class Trie
{
    private class Node
    {
        public Dictionary<char, Node> Children { get; } = new();
        public bool IsEnd { get; set; }
        public int Count { get; set; }
    }

    private readonly Node _root = new();

    /// <summary>
    /// Gets the number of unique elements stored in the trie.
    /// </summary>
    public int Size { get; private set; }

    /// <summary>
    /// Adds a string to the trie.
    /// </summary>
    /// <param name="element">The string to add.</param>
    /// <returns>True if the string was successfully added; otherwise, false if the string already exists.</returns>
    public bool Add(string element)
    {
        var current = _root;
        var isNew = false;

        foreach (var ch in element)
        {
            if (!current.Children.ContainsKey(ch))
            {
                current.Children[ch] = new Node();
            }

            current = current.Children[ch];
            current.Count++;
        }

        if (!current.IsEnd)
        {
            current.IsEnd = true;
            Size++;
            isNew = true;
        }

        return isNew;
    }

    /// <summary>
    /// Checks if the trie contains the specified string.
    /// </summary>
    /// <param name="element">The string to check.</param>
    /// <returns>True if the trie contains the string; otherwise, false.</returns>
    public bool Contains(string element)
    {
        var current = _root;
        foreach (var ch in element)
        {
            if (!current.Children.ContainsKey(ch))
            {
                return false;
            }

            current = current.Children[ch];
        }

        return current.IsEnd;
    }

    /// <summary>
    /// Counts how many strings in the trie start with the given prefix.
    /// </summary>
    /// <param name="prefix">The prefix to search for.</param>
    /// <returns>The number of strings that start with the given prefix.</returns>
    public int HowManyStartsWithPrefix(string prefix)
    {
        var current = _root;
        foreach (var ch in prefix)
        {
            if (!current.Children.TryGetValue(ch, out current))
            {
                return 0;
            }
        }

        return current.Count;
    }

    /// <summary>
    /// Removes a string from the trie.
    /// </summary>
    /// <param name="element">The string to remove.</param>
    /// <returns>True if the string was successfully removed; otherwise, false if the string was not found.</returns>
    public bool Remove(string element)
    {
        if (!Contains(element))
        {
            return false;
        }

        Remove(_root, element, 0);
        Size--;
        return true;
    }

    private bool Remove(Node node, string element, int index)
    {
        if (index == element.Length)
        {
            node.IsEnd = false;
            node.Count--;
            return true;
        }

        var ch = element[index];
        var child = node.Children[ch];
        var removed = Remove(child, element, index + 1);

        if (removed)
        {
            child.Count--;
            if (child.Count == 0)
            {
                node.Children.Remove(ch);
            }
        }

        return removed;
    }
}