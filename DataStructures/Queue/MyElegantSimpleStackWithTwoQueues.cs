namespace Queue;
public class MyElegantSimpleStackWithTwoQueues
{
    private Queue<int> _firstQueue = new();
    private Queue<int> _secondQueue = new();
    int _top;
    // O(1)
    public void Push(int value)
    {
        _firstQueue.Enqueue(value);
        _top = value;
    }


    // O(n)
    public int Pop()
    {
        if (IsEmpty())
            throw new EmptyStackException();


        while (_firstQueue.Count > 1)
        {
            _top = _firstQueue.Dequeue();
            _secondQueue.Enqueue(_top);
        }

        SwapQueues();

        return _secondQueue.Dequeue();
    }

    private void SwapQueues()
    {
        (_firstQueue, _secondQueue) = (_secondQueue, _firstQueue);
    }

    // O(1)
    public int Peek()
    {
        if (IsEmpty())
            throw new EmptyStackException();
        return _top;
    }

    // O(1)
    public bool IsEmpty()
    {
        return _firstQueue.Count == 0;
    }
}
