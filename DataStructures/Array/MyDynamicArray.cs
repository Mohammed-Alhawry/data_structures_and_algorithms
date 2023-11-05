namespace Tuple;

public class MyDynamicArray<T>
{

    private T[] _array;
    public int Count { get; private set; }
    public bool IsEmpty => Count == 0;

    public void Reverse()
    {
        T[] newArray = new T[_array.Length];

        int counter = 0;
        for (int i = Count - 1; i >= 0; i--)
        {
            newArray[counter++] = _array[i];
        }

        _array = newArray;
    }
    public T[] Intersect(T[] comparedByArray)
    {
        if (comparedByArray is null)
            throw new ArgumentNullException("'comparedByArray' parameter cannot be null");

        var commonValues = new List<T>(_array.Length);
        foreach (var item in _array)
            if (comparedByArray.Contains(item))
                commonValues.Add(item);

        return commonValues.ToArray();
    }
    public MyDynamicArray(int size = 1)
    {
        _array = new T[size];
    }


    public T GetByIndex(int index)
    {
        if (index >= 0 && index < Count)
            return _array[index];

        throw new IndexOutOfRangeException($"item not found at index '{index}'");
    }

    private void ShiftElementsToEndByOneFrom(int currentPosition)
    {
        if (IsFull())
            throw new InvalidOperationException("elements cannot be shift to end because the array is full");

        for (int i = (Count - 1); i >= currentPosition; i--)
            _array[i + 1] = _array[i];
    }

    private bool IsFull()
    {
        return Count == _array.Length;
    }

    public void InsertAt(int index, T value)
    {
        if (index < 0)
            throw new IndexOutOfRangeException();

        ResizeIfRequired();
        ShiftElementsToEndByOneFrom(index);
        _array[index] = value;
        Count++;
    }
    public void PrintArray()
    {
        for (int i = 0; i < Count; i++)
            Console.WriteLine(_array[i]);
    }
    private void ShiftElementsFromEndTo(int index)
    {
        for (int j = index; j < (Count - 1); j++)
            _array[j] = _array[j + 1];
    }

    public void DeleteAtIfExists(int index)
    {
        if (index >= 0 && index < Count)
        {
            ShiftElementsFromEndTo(index);
            Count--;
        }
    }

    public void DeleteIfExists(int numberToDelete)
    {
        for (int i = 0; i < Count; i++)
            if (_array[i]!.Equals(numberToDelete))
            {
                ShiftElementsFromEndTo(i);
                Count--;
                break;
            }
    }
    public void Add(T value)
    {
        ResizeIfRequired();
        _array[Count++] = value;
    }


    private void ResizeIfRequired()
    {
        if (Count == _array.Length)
        {
            var half = (int)Math.Ceiling((double)Count / 2);
            T[] newArray = new T[_array.Length + half];

            for (int i = 0; i < Count; i++)
                newArray[i] = _array[i];

            _array = newArray;
        }
    }
}