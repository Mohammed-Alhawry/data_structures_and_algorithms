using System.Text;

namespace TrieDataStructure;
#nullable disable
public class Trie
{
    private readonly Node _root = new(' ');

    public Trie()
    {

    }
    public Trie(IEnumerable<string> words)
    {
        if (words is null) return;

        foreach (var word in words)
        {
            Add(word);
        }
    }
    public void Add(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
            return;

        var current = _root;
        foreach (var item in word)
        {
            if (!current.HasChild(item))
                current.AddChild(item);

            current = current.GetChild(item);
        }
        current.IsEndOfWord = true;
    }

    public IEnumerable<string> GetWordsOfPrefix(string prefix)
    {
        var words = new List<string>();
        if (string.IsNullOrWhiteSpace(prefix)) return words;
        prefix = prefix.Trim();

        var lastNode = GetLastNode(prefix);
        GetWordsOfPrefix(lastNode, prefix, words);
        return words;
    }


    private void GetWordsOfPrefix(Node root, string word, List<string> words)
    {
        if (root is null) return;
        if (root.IsEndOfWord) words.Add(word);
        foreach (var child in root.GetChildren())
        {
            GetWordsOfPrefix(child, word + child.Value, words);
        }
    }
    private Node GetLastNode(string prefix)
    {
        if (string.IsNullOrWhiteSpace(prefix)) return null;

        var current = _root;
        foreach (var ch in prefix)
        {
            if (!current.HasChild(ch)) return null;
            current = current.GetChild(ch);
        }
        return current;
    }

    public int CountWords()
    {
        return CountWords(_root);
    }

    private int CountWords(Node root)
    {
        int total = 0;
        if (root.IsEndOfWord)
            total++;

        foreach (var item in root.GetChildren())
        {
            total += CountWords(item);
        }
        return total;
    }

    public static string LongestPrefix(string[] words)
    {
        if (words is null || words.Length == 0) return "";
        var longestPrefix = new StringBuilder();
        var shortest = GetShortest(words);
        var trie = new Trie(words);
        var current = trie._root;

        for (int i = 0; i < shortest.Length; i++)
        {
            if (current.CountOfChildren != 1)
                break;
            
            longestPrefix.Append(shortest[i]);
            current = current.GetChild(shortest[i]);
        }

        return longestPrefix.ToString();
    }

    private static string GetShortest(string[] words)
    {
        if (words is null || words.Length == 0) return "";

        var shortest = words[0];
        foreach (var word in words)
            if (word.Length < shortest.Length)
                shortest = word;
        return shortest;
    }

    public bool Contains(string word)
    {
        if (string.IsNullOrWhiteSpace(word)) return false;
        word = word.Trim();
        var current = _root;
        foreach (var item in word)
        {
            if (!current.HasChild(item)) return false;
            current = current.GetChild(item);
        }

        return current.IsEndOfWord;
    }


    public void Remove(string word)
    {
        if (string.IsNullOrWhiteSpace(word))
            return;

        Remove(_root, word, index: 0);
    }

    private void Remove(Node root, string word, int index)
    {
        if (index == word.Length)
        {
            root.IsEndOfWord = false;
            return;
        }

        var child = root.GetChild(word[index]);
        if (child is null) return;

        Remove(child, word, index + 1);
        if (!child.HasChildren() && !child.IsEndOfWord)
            root.RemoveChild(child.Value);
    }

    private class Node
    {
        public static int Alphabet_Size = 26;
        public char Value { get; }
        public bool IsEndOfWord { get; set; }
        public int CountOfChildren { get; private set; } = 0;
        public Node(char value)
        {
            Value = value;
        }
        public bool RemoveChild(char ch)
        {
            return Children.Remove(ch);
        }
        private readonly Dictionary<char, Node> Children = new();


        public override string ToString()
        {
            return $"Node='{Value}'";
        }

        public bool HasChildren()
        {
            return Children.Count > 0;
        }
        public Node GetChild(char ch)
        {
            return Children.GetValueOrDefault(ch);
        }
        public bool HasChild(char ch)
        {
            return Children.ContainsKey(ch);
        }

        public Node[] GetChildren()
        {
            return Children.Values.ToArray();
        }

        public void AddChild(char ch)
        {
            Children[ch] = new Node(ch);
            CountOfChildren++;
        }
    }
}