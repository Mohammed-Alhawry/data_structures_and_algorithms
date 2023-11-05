namespace Queue;
public class MyComplexStackWithTwoQueues
{
    private Queue<int> _firstQueue = new();
    private Queue<int> _secondQueue = new();
    public int Count { get; private set; }
    
    // O(1)
    public void Push(int value)
    {
        _firstQueue.Enqueue(value);
        Count++;
    }


    // O(n)
    public int Pop()
    {
        if (IsEmpty())
            throw new EmptyStackException();


        TransferItemsExceptLastOneFrom(_firstQueue, _secondQueue);
        var last = _firstQueue.Dequeue();
        Swap(ref _firstQueue, ref _secondQueue);

        Count--;
        return last;
    }

    private void Swap(ref Queue<int> first, ref Queue<int> second)
    {
        var firstQueue = first;
        first = second;
        second = firstQueue;
    }

    private void TransferItemsExceptLastOneFrom(Queue<int> first, Queue<int> second)
    {
        while (first.Count > 1)
            second.Enqueue(first.Dequeue());
    }
    private void TransferItemsFrom(Queue<int> first, Queue<int> second)
    {
        while (first.Count > 0)
            second.Enqueue(first.Dequeue());
    }

    // O(n)
    public int Peek()
    {
        if (IsEmpty())
            throw new EmptyStackException();

        TransferItemsExceptLastOneFrom(_firstQueue, _secondQueue);
        var last = _firstQueue.Dequeue();
        _secondQueue.Enqueue(last);

        Swap(ref _firstQueue, ref _secondQueue);
        return last;
    }

    // O(1)
    public bool IsEmpty()
    {
        return Count == 0;
    }
}
