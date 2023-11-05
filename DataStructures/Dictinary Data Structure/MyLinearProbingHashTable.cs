
namespace Dictionary_Data_Structure;

public class MyLinearProbingHashTable<Int32, String>
{
    private MyKeyValuePair?[] _arr = new MyKeyValuePair[5];
    private int _count;

    public int Size() => _count;
    public void Put(int key, string value)
    {
        var index = GetValidIndexToInsert(key);
        if (index == -1)
            throw new FullDictionaryException();

        InsertOrReplaceValueAtIndex(index, key, value);
    }

    private void InsertOrReplaceValueAtIndex(int index, int key, string value)
    {
        if (_arr[index] is null)
        {
            _arr[index] = new MyKeyValuePair(key, value);
            _count++;
        }
        else
            _arr[index].Value = value;
    }

    private int GetValidIndexToInsert(int key)
    {
        var index = GetHash(key);
        for (int i = index; i < _arr.Length; i++)
        {
            if (_arr[i] is null)
                return i;

            else if (_arr[i].Key == key)
                return i;
        }

        return -1;
    }
    private int GetIndexOfItemIfExists(int key)
    {
        var index = GetHash(key);
        for (int i = index; i < _arr.Length; i++)
        {
            if (_arr[i] is not null && _arr[i].Key == key)
                return i;
        }

        return -1;
    }
    public string Remove(int key)
    {
        var index = GetIndexOfItemIfExists(key);
        if (index == -1)
            return string.Empty;

        var value = _arr[index].Value;
        _arr[index] = null;
        _count--;
        return value;
    }
    public string GetValue(int key)
    {
        var index = GetIndexOfItemIfExists(key);
        if (index == -1)
            return string.Empty;

        return _arr[index].Value;
    }
    private void ResizeTheArray()
    {
        var newArr = new MyKeyValuePair[_arr.Length + 2];
        for (int i = 0; i < _arr.Length; i++)
            newArr[i] = _arr[i];

        _arr = newArr;
    }
    private int GetHash(int key)
    {
        return key % _arr.Length;
    }

    private class MyKeyValuePair
    {
        public int Key { get; }
        public string Value { get; set; }
        public MyKeyValuePair(int key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
