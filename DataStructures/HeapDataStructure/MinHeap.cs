namespace HeapDataStructure;
public class MinHeap<T> where T : IComparable<T>
{
    private readonly List<T> _items;
    public int Count => _items.Count;

    public MinHeap()
    {
        _items = new();
    }

    public MinHeap(int capacity = 10)
    {
        _items = new List<T>(capacity);
    }

    public MinHeap(IEnumerable<T> items) : this(items.Count())
    {
        if (items is null) throw new ArgumentNullException();
        foreach (var item in items)
        {
            Add(item);
        }
    }

    public void Add(T value)
    {
        _items.Add(value);
        BubbleUp();
    }

    public T Remove()
    {
        if (IsEmpty())
            throw new EmptyHeapException();

        var root = _items[0];
        _items[0] = _items[Count - 1];
        _items.RemoveAt(Count - 1);

        BubbleDown();

        return root;
    }

    public T Min()
    {
        if (IsEmpty())
            throw new EmptyHeapException();

        return _items[0];
    }

    public T GetKthSmallest(int kth)
    {
        if (kth < 1 || kth > Count)
            throw new ArgumentOutOfRangeException();

        var temp = new List<T>(kth - 1);
        for (int i = 0; i < kth - 1; i++)
            temp.Add(Remove());

        var root = _items[0];
        foreach (var item in temp)
            Add(item);

        return root;
    }

    //////////////////////////// Public Static Methods ////////////////////////////////

    public static void Heapify(T[] arr)
    {
        var lastParentIndex = arr.Length / 2 - 1;
        for (int i = lastParentIndex; i >= 0; i--)
        {
            Heapify(arr, i);
        }
    }

    //////////////////////////// Private Helper Methods ////////////////////////////////////

    private static void Heapify(T[] arr, int index)
    {
        var smallerIndex = index;

        var leftIndex = index * 2 + 1;
        if (leftIndex < arr.Length && Comparable.IsGreaterThan(arr[smallerIndex], arr[leftIndex]))
            smallerIndex = leftIndex;

        var rightIndex = index * 2 + 2;
        if (rightIndex < arr.Length && Comparable.IsGreaterThan(arr[smallerIndex], arr[rightIndex]))
            smallerIndex = rightIndex;

        if (smallerIndex == index)
            return;

        Swap(arr, index, smallerIndex);
        Heapify(arr, smallerIndex);
    }


    private void BubbleUp()
    {
        var index = Count - 1;
        while (index > 0 && Comparable.IsGreaterThan(_items[MinHeap<T>.Parent(index)], _items[index]))
        {
            Swap(MinHeap<T>.Parent(index), index);
            index = MinHeap<T>.Parent(index);
        }
    }

    private void BubbleDown()
    {
        var index = 0;
        while (index < Count && !IsValidParent(index))
        {
            Swap(index, SmallerChildIndex(index));
            index = SmallerChildIndex(index);
        }
    }

    private bool IsValidParent(int index)
    {
        if (!HasLeftChild(index))
            return true;

        var isValid = Comparable.IsLessOrEqual(_items[index], LeftChild(index));

        if (!HasRightChild(index))
            return isValid;

        isValid = isValid && Comparable.IsLessOrEqual(_items[index], RightChild(index));

        return isValid;
    }

    private int SmallerChildIndex(int index)
    {
        if (!HasLeftChild(index))
            return index;

        else if (!HasRightChild(index))
            return MinHeap<T>.LeftChildIndex(index);

        return Comparable.IsGreaterThan(LeftChild(index), RightChild(index)) 
        ? MinHeap<T>.RightChildIndex(index) 
        : MinHeap<T>.LeftChildIndex(index);
    }

    private static void Swap(T[] arr, int firstIndex, int secondIndex)
    => (arr[firstIndex], arr[secondIndex]) = (arr[secondIndex], arr[firstIndex]);

    private bool HasLeftChild(int index)
    => MinHeap<T>.LeftChildIndex(index) < Count;

    private bool HasRightChild(int index) 
    => MinHeap<T>.RightChildIndex(index) < Count;

    private T LeftChild(int index) 
    => _items[MinHeap<T>.LeftChildIndex(index)];

    private T RightChild(int index) 
    => _items[MinHeap<T>.RightChildIndex(index)];

    private void Swap(int first, int second)
    => (_items[first], _items[second]) = (_items[second], _items[first]);

    public bool IsEmpty()
    => Count == 0;

    private static int Parent(int index)
    => (index - 1) / 2;

    private static int LeftChildIndex(int parentIndex)
    => parentIndex * 2 + 1;

    private static int RightChildIndex(int parentIndex)
    => parentIndex * 2 + 2;    
}
