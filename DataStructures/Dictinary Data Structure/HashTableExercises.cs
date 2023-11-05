
namespace Dictionary_Data_Structure;

public class MyHashTable
{

    public static int FindMostFrequentIn(int[] arr)
    {
        if (arr is null)
            throw new ArgumentNullException();

        if (arr.Length == 0)
            return int.MinValue;

        var hashTable = new Dictionary<int, int>();
        foreach (var item in arr)
            hashTable[item] = hashTable.ContainsKey(item) ? ++hashTable[item] : 1;

        int mostFrequentKey = arr[0];
        foreach (var item in hashTable)
            if (item.Value > hashTable[mostFrequentKey])
                mostFrequentKey = item.Key;

        return mostFrequentKey;
    }

    // O(n)
    public static string FindMostFrequentIn(string str)
    {
        var arr = str.Split(' ') ?? throw new ArgumentNullException();
        if (arr.Length == 0)
            return string.Empty;

        var hashTable = new Dictionary<string, int>();
        foreach (var item in arr)
            hashTable[item] = hashTable.ContainsKey(item) ? ++hashTable[item] : 1;

        string mostFrequentKey = arr[0];
        foreach (var item in hashTable)
            if (item.Value > hashTable[mostFrequentKey])
                mostFrequentKey = item.Key;

        return mostFrequentKey;
    }

    // O(n)
    public static (int, int) TwoSum(int[] ints, int target)
    {
        var hash = new Dictionary<int, int>();

        int complement;
        for (int i = 0; i < ints.Length; i++)
        {
            complement = target - ints[i];
            if (hash.ContainsKey(complement))
                return (hash[complement], i);
            hash[ints[i]] = i;
        }

        return (0, 0);
    }

    // O(N)
    public static int CountPairsWithDiff(int diff, int[] ints)
    {
        var set = new HashSet<int>(ints);
        var count = 0;
        foreach (var i in ints)
        {
            if (set.Contains(i + diff))
                count++;
            if (set.Contains(i - diff))
                count++;
            set.Remove(i);
        }

        return count;
    }
}
