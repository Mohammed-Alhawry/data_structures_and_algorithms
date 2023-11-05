namespace Queue;
public class IntPriorityQueue
{
    private  Queue<int> _queue = new();

    public void Enqueue(int value)
    {
        if (IsEmpty())
            _queue.Enqueue(value);
        else
        {
            SortQueue(value);

        }

    }

    private void SortQueue(int addedValue)
    {
        var temporaryQueue = new Queue<int>();

        while (_queue.Count != 0 && addedValue >= _queue.Peek())
            temporaryQueue.Enqueue(_queue.Dequeue());

        temporaryQueue.Enqueue(addedValue);
        
        while (_queue.Count != 0)
            temporaryQueue.Enqueue(_queue.Dequeue());

        _queue = temporaryQueue;
    }

    public int Dequeue()
    {
        return _queue.Dequeue();
    }


    public bool IsEmpty() => _queue.Count == 0;
}