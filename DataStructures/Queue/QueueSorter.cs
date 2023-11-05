namespace Queue;

public class QueueSorter
{
    private readonly int[] _array;
    private int _count;
    private int _front;
    private int _rear;
    public QueueSorter(int size)
    {
        _array = new int[size];
    }

    public void Enqueue(int value)
    {
        if (IsFull())
            throw new OverflowException();

        _array[_rear] = value;
        IncrementRear();
        _count++;
    }

    public void SortFirstItems(int k)
    {
        if (k > _count)
            throw new ArgumentOutOfRangeException($"Elements in the queue are less than '{k}'");

        SortFirstElements(k);

    }

    private void SortFirstElements(int k)
    {
        int kIndex = GetElementIndexAfter(k - 1);

        int swapCount = k - 1;
        for (int i = 0; i < swapCount; i++)
        {
            for (int j = kIndex; j != _front; j = DecrementUsingReminder(j))
            {
                if (_array[kIndex] > _array[DecrementUsingReminder(j)])
                {
                    int swap = _array[DecrementUsingReminder(j)];
                    _array[DecrementUsingReminder(j)] = _array[j];
                    _array[j] = swap;
                }

                else
                    break;
            }
        }
    }

    private int GetElementIndexAfter(int numberOfMoves)
    {
        int end = _front;
        for (int i = 0; i < numberOfMoves; i++)
        {
            end = GetIncrementUsingReminder(end);
        }
        return end;
    }

    public int Dequeue()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Queue is empty.");

        var front = _front;
        IncrementFront();
        _count--;

        return _array[front];
    }


    public bool IsFull()
    {
        return _count == _array.Length;
    }

    public bool IsEmpty()
    {
        return _count == 0;
    }

    private void IncrementFront()
    {
        _front = (_front + 1) % _array.Length;
    }

    private void IncrementRear()
    {
        _rear = (_rear + 1) % _array.Length;
    }
    private int GetIncrementUsingReminder(int value)
    {
        return (value + 1) % _array.Length;
    }

    private int DecrementUsingReminder(int value)
    {
        return (value - 1 < 0) ? _array.Length - 1 : value - 1;
    }
}

