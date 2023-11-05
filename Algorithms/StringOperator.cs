using System.Text;
#nullable disable
public static class StringUtilities
{
    public static int CountVowels(string text)
    {
        if (text is null)
            return 0;

        text = text.ToLower();
        var vowels = new HashSet<char>("aioue");
        int counter = 0;

        foreach (var ch in text)
        {
            if (vowels.Contains(ch))
                counter++;
        }

        return counter;
    }

    public static string Reverse(string text)
    {
        if (text is null)
            return "";

        var newText = new StringBuilder();
        for (var i = text.Length - 1; i >= 0; i--)
            newText.Append(text[i]);

        return newText.ToString();
    }

    public static string ReverseOrderOfWords(string text)
    {
        if (text is null)
            return "";

        var words = text.Trim().Split(' ');
        Array.Reverse(words);
        return string.Join(' ', words);
    }

    public static bool IsRotated(string str1, string str2)
    {
        if (str1 is null || str2 is null
            || str1.Length != str2.Length)
            return false;

        str1 = str1.ToLower();
        str2 = str2.ToLower();

        var length = str1.Length;
        for (int i = 0; i < length; i++)
        {
            var sub1 = str1[i] + str1[GetNextPosition(i, length)];
            var sub2 = str1[i] + GetNextLetter(str2, str1[i]);
            if (sub1 != sub2)
                return false;
        }

        return true;
    }

    public static char FindMostRepeatedCharacter(string str)
    {
        if (str is null || str.Length == 0)
            return char.MinValue;

        var dictionary = new Dictionary<char, int>();

        foreach (var item in str)
            dictionary[item] = !dictionary.ContainsKey(item) ? 1 : ++dictionary[item];

        var character = str[0];
        foreach (var item in dictionary)
        {
            if (item.Value > dictionary[character])
                character = item.Key;
        }

        return character;
    }

    public static string CapitalizeEveryWord(string str)
    {
        if (str is null || str.Length == 0)
            return "";

        str = str.ToLower();

        var words = new List<StringBuilder>();
        for (int i = 0; i < str.Length; i++)
        {
            var word = new StringBuilder();
            while (i < str.Length && !char.IsWhiteSpace(str[i]))
                word.Append(str[i++]);

            if (word.Length > 0)
            {
                word[0] = char.ToUpper(word[0]);
                words.Add(word);
            }
        }

        return string.Join(' ', words);
    }

    public static bool AreAnagrams(string first, string second)
    {
        if (first is null || second is null)
            return false;

        if (first.Length != second.Length)
            return false;

        var dictionary = new Dictionary<char, int>();
        foreach (var item in first)
            dictionary[item] = dictionary.ContainsKey(item) ? ++dictionary[item] : 1;

        foreach (var item in second)
        {
            if (dictionary.ContainsKey(item))
            {
                dictionary[item]--;
                if (dictionary[item] == 0)
                    dictionary.Remove(item);
            }

            else
                return false;
        }

        return true;
    }

    public static bool IsPalinDrome(string str)
    {
        if (str is null)
            return false;

        str = str.ToLower();
        var length = str.Length;
        for (int i = 0; i < length / 2; i++)
            if (str[i] != str[length - (i + 1)])
                return false;

        return true;
    }

    public static string RemoveDuplicateCharacters(string str)
    {
        if (str is null)
            return "";

        var newStr = new StringBuilder(str.Length);
        var set = new HashSet<char>();

        foreach (var item in str)
        {
            if (set.Add(item))
                newStr.Append(item);
        }

        return newStr.ToString();
    }

    private static char GetNextLetter(string str, char ch)
    {
        for (var i = 0; i < str.Length; i++)
        {
            if (str[i] == ch)
                return str[GetNextPosition(i, str.Length)];
        }

        return char.MaxValue;
    }

    private static int GetNextPosition(int index, int count)
    {
        return (index + 1) % count;
    }
}

