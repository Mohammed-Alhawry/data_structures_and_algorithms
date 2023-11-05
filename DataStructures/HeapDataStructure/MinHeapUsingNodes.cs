namespace HeapDataStructure;
public partial class MinHeapUsingNodes<K, V> where K : IComparable<K>
{
    private readonly List<Node> _items = new();
    public int Count => _items.Count;
    public void Add(K key, V value)
    {
        _items.Add(new Node(key, value));
        BubbleUp();
    }

    public Node Remove()
    {
        if (IsEmpty())
            throw new EmptyHeapException();

        var root = _items[0];
        _items[0] = _items[Count - 1];
        _items.RemoveAt(Count - 1);

        BubbleDown();

        return root;
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
    private bool HasLeftChild(int index)
    {
        return LeftChildIndex(index) < Count;
    }

    private bool HasRightChild(int index) => RightChildIndex(index) < Count;

    private int SmallerChildIndex(int index)
    {
        if (!HasLeftChild(index))
            return index;

        else if (!HasRightChild(index))
            return LeftChildIndex(index);

        return (Comparable.IsGreaterThan(LeftChild(index), RightChild(index))) ? RightChildIndex(index) : LeftChildIndex(index);
    }
    private Node LeftChild(int index) => _items[LeftChildIndex(index)];

    private int LeftChildIndex(int parentIndex)
    {
        return parentIndex * 2 + 1;
    }

    private int RightChildIndex(int parentIndex)
    {
        return parentIndex * 2 + 2;
    }
    private Node RightChild(int index) => _items[RightChildIndex(index)];

    public bool IsEmpty()
    {
        return Count == 0;
    }

    public MinHeapUsingNodes()
    {

    }
    public MinHeapUsingNodes(IEnumerable<Node> nodes)
    {
        foreach (var item in nodes)
        {
            Add(item.Key, item.Value);
        }
    }

    private void BubbleUp()
    {
        var index = Count - 1;
        while (index > 0 && Comparable.IsGreaterThan(_items[Parent(index)], _items[index]))
        {
            Swap(Parent(index), index);
            index = Parent(index);
        }
    }
    private void Swap(int first, int second)
        => (_items[first], _items[second]) = (_items[second], _items[first]);

    private int Parent(int index) => (index - 1) / 2;

    public class Node : IComparable<Node>
    {
        public K Key { get; set; }
        public V Value { get; set; }
        public Node(K key, V value)
        {
            Key = key;
            Value = value;
        }

        public int CompareTo(MinHeapUsingNodes<K, V>.Node? other)
        {
            return this.Key.CompareTo(other.Key);
        }
    }
}