namespace HeapDataStructure;

public class PriorityQueueUsingHeap<TElement, TPriority> where TPriority : IComparable<TPriority>
{
    private readonly MinHeapUsingNodes<TPriority, TElement> _heap = new();

    public void Enqueue(TElement value, TPriority priority)
    {
        _heap.Add(priority, value);
    }

    public TElement Dequeue()
    {
        return _heap.Remove().Value;
    }

    public bool IsEmpty() => _heap.IsEmpty();
    
}
