namespace Dictionary_Data_Structure;

public class CharFinder
{
    public char FindFirstUnrepeatedLetterIn(string text)
    {
        Dictionary<char, int> chars = new();

        foreach (var ch in text)
            chars[ch] = chars.ContainsKey(ch) ? ++chars[ch] : 1;

        foreach (var ch in text)
            if (chars[ch] == 1)
                return ch;

        return char.MinValue;
    }

    public char FindFirstRepeatedLetterIn(string text)
    {
        var chars = new HashSet<char>();

        foreach (var ch in text)
            if (!chars.Add(ch))
                return ch;

        return char.MinValue;
    }
}

