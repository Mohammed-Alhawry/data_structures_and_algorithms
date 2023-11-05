namespace Queue;

public partial class MyReadableSimplePriorityQueue
{
    private int[] _array;
    private int _count;

    public MyReadableSimplePriorityQueue(int size)
    {
        _array = new int[size];
    }

    public void Enqueue(int value)
    {
        if (IsFull())
            throw new FullQueueException();

        var index = ShiftValuesToInsert(value);
        _array[index] = value;
        _count++;
    }
    private int ShiftValuesToInsert(int value)
    {
        int i;
        for (i = _count - 1; i >= 0; i--)
        {
            if (_array[i] > value)
                break;

            _array[i + 1] = _array[i];
        }

        return i + 1;
    }

    public int Dequeue()
    {
        if (IsEmpty())
            throw new EmptyQueueException();

        return _array[--_count];
    }



    public bool IsEmpty()
    {
        return _count == 0;
    }

    public bool IsFull()
    {
        return _count == _array.Length;
    }
}
