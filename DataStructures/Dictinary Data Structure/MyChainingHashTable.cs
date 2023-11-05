using System.Security.Cryptography;
using System.Text;
namespace Dictionary_Data_Structure;

public class MyChainingHashTable<K, V>
{
    private readonly LinkedList<MyKeyValuePair>?[] _hashArray = new LinkedList<MyKeyValuePair>[5];


    public void PrintValues()
    {
        for (var i = 0; i < _hashArray.Length; i++)
        {
            if (_hashArray[i] is not null)
                foreach (var item in _hashArray[i]!)
                {
                    System.Console.WriteLine($"[{item.Key}, {item.Value}]");
                }
        }
    }

    public void Put(K key, V value)
    {

        var pair = GetMyKeyValuePair(key);
        if (pair is not null)
        {
            pair.Value = value;
            return;
        }

        GetOrCreateBucket(key).AddLast(new MyKeyValuePair(key, value));
    }

    private LinkedList<MyKeyValuePair> GetOrCreateBucket(K key)
    {
        var index = GetIndexForKey(key);
        if (_hashArray[index] is null)
            _hashArray[index] = new LinkedList<MyKeyValuePair>();

        return _hashArray[index]!;
    }
    private LinkedList<MyKeyValuePair>? GetBucket(K key)
    {
        var index = GetIndexForKey(key);
        return _hashArray[index];
    }
    public V? Get(K key)
    {
        var pair = GetMyKeyValuePair(key);
        if (pair is null)
            return default;

        return pair.Value;
    }

    private MyKeyValuePair? GetMyKeyValuePair(K key)
    {

        var bucket = GetBucket(key);
        if (bucket is not null)
        {
            foreach (var pair in bucket)
            {
                if (pair.Key.Equals(key))
                    return pair;
            }
        }

        return null;
    }
    public V? Remove(K key)
    {
        var pair = GetMyKeyValuePair(key);
        if (pair is null)
            return default;

        GetBucket(key)!.Remove(pair);

        return pair.Value;
    }
    private int GetIndexForKey(K key)
    {
        int hash = ConvertHashCodeToInt32(HashCode(key));
        return hash % 5;
    }
    private int ConvertHashCodeToInt32(string hashCode)
    {

        var hashNumber = 0;
        foreach (var i in hashCode)
            hashNumber += i;
        return hashNumber;
    }

    private string HashCode<T>(T str)
    {
        if (str is null)
            throw new ArgumentNullException();

        var hashBytes = SHA1.HashData(Encoding.UTF8.GetBytes(str.ToString()));
        return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
    }

    private class MyKeyValuePair
    {
        public K Key { get; }
        public V Value { get; set; }
        public MyKeyValuePair(K key, V value)
        {
            Key = key;
            Value = value;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType())
                return false;

            var thisObject = obj as MyKeyValuePair;
            return Key!.Equals(thisObject!.Key);
        }

        public override int GetHashCode()
        {
            return Key!.GetHashCode();
        }

    }
}