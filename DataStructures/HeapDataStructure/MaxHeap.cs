using System.Globalization;
using System.Runtime.CompilerServices;

namespace HeapDataStructure;
public class MaxHeap<T> where T : IComparable<T>
{
    private readonly List<T> _items = new();
    public int Count => _items.Count;
    public MaxHeap() { }
    public MaxHeap(int capacity = 10)
    {
        _items = new List<T>(capacity);
    }

    public MaxHeap(IEnumerable<T> items) : this(items.Count())
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

        BubbleDown();

        return root;
    }

    public T Max()
    {
        if (IsEmpty())
            throw new EmptyHeapException();

        return _items[0];
    }

    public T GetKthLargest(int kth)
    {
        if (kth < 1 || kth > _items.Count)
            throw new ArgumentOutOfRangeException();

        var arr = new T[kth - 1];
        for (int i = 0; i < kth - 1; i++)
            arr[i] = Remove();

        var root = _items[0];
        foreach (var item in arr)
            Add(item);

        return root;
    }

    public bool IsEmpty() => Count == 0;



    /////////////////////////// Public Static Methods  /////////////////////////////////////////////////

    public static bool IsMaxHeap(T[] arr)
    {
        var lastParentIndex = arr.Length / 2 - 1;
        for (int i = 0; i <= lastParentIndex; i++)
        {
            if (!IsValidParent(arr, i))
                return false;
        }

        return true;
    }

    public static void Heapify(T[] arr)
    {
        var lastParentIndex = arr.Length / 2 - 1;
        for (int i = lastParentIndex; i >= 0; i--)
        {
            Heapify(arr, i);
        }
    }

    ////////////////////////////////////// private Helper Methods //////////////////////////////////////////////////

    private static void Heapify(T[] arr, int index)
    {
        var largerIndex = index;

        var leftIndex = index * 2 + 1;
        if (leftIndex < arr.Length && Comparable.IsLessThan(arr[largerIndex], arr[leftIndex]))
            largerIndex = leftIndex;

        var rightIndex = index * 2 + 2;
        if (rightIndex < arr.Length && Comparable.IsLessThan(arr[largerIndex], arr[rightIndex]))
            largerIndex = rightIndex;

        if (largerIndex == index)
            return;

        Swap(arr, index, largerIndex);
        Heapify(arr, largerIndex);
    }

    private static void Swap(T[] arr, int firstIndex, int secondIndex)
    => (arr[firstIndex], arr[secondIndex]) = (arr[secondIndex], arr[firstIndex]);

    private void BubbleUp()
    {
        var index = Count - 1;
        while (index > 0 && Comparable.IsLessThan(_items[MaxHeap<T>.Parent(index)], _items[index]))
        {
            Swap(MaxHeap<T>.Parent(index), index);
            index = MaxHeap<T>.Parent(index);
        }
    }

    private void BubbleDown()
    {
        var index = 0;
        while (index < Count && !IsValidParent(index))
        {
            Swap(index, LargerChildIndex(index));
            index = LargerChildIndex(index);
        }
    }

    private bool IsValidParent(int index)
    {
        if (!HasLeftChild(index))
            return true;

        var isValid = Comparable.IsGreaterOrEqual(_items[index], LeftChild(index));

        if (!HasRightChild(index))
            return isValid;

        isValid = isValid && Comparable.IsGreaterOrEqual(_items[index], RightChild(index));

        return isValid;
    }

    private static bool IsValidParent(T[] arr, int index)
    {
        var leftIndex = index * 2 + 1;
        var hasLeft = leftIndex < arr.Length;
        var rightIndex = index * 2 + 2;
        var hasRight = rightIndex < arr.Length;
        if (!hasLeft)
            return true;

        var isValid = Comparable.IsGreaterOrEqual(arr[index], arr[leftIndex]);

        if (!hasRight)
            return isValid;

        isValid = isValid && Comparable.IsGreaterOrEqual(arr[index], arr[rightIndex]);

        return isValid;
    }
    private bool HasLeftChild(int index)
    => MaxHeap<T>.LeftChildIndex(index) < Count;

    private bool HasRightChild(int index)
    => MaxHeap<T>.RightChildIndex(index) < Count;

    private int LargerChildIndex(int index)
    {
        if (!HasLeftChild(index))
            return index;

        else if (!HasRightChild(index))
            return MaxHeap<T>.LeftChildIndex(index);

        return Comparable.IsLessThan(LeftChild(index), RightChild(index))
        ? MaxHeap<T>.RightChildIndex(index)
        : MaxHeap<T>.LeftChildIndex(index);
    }

    private T LeftChild(int index)
    => _items[MaxHeap<T>.LeftChildIndex(index)];

    private T RightChild(int index)
    => _items[MaxHeap<T>.RightChildIndex(index)];

    private void Swap(int first, int second)
    => (_items[first], _items[second]) = (_items[second], _items[first]);

    private static int Parent(int index)
    => (index - 1) / 2;

    private static int LeftChildIndex(int parentIndex)
    => parentIndex * 2 + 1;

    private static int RightChildIndex(int parentIndex)
    => parentIndex * 2 + 2;
}
