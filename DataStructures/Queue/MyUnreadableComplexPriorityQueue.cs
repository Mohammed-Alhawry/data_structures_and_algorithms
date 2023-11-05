namespace Queue;
public class MyUnreadableComplexPriorityQueue
{
    private readonly int[] _array;
    private int _count;
    private int _front;
    private int _rear;
    public MyUnreadableComplexPriorityQueue(int size)
    {
        _array = new int[size];
    }

    public void Enqueue(int value)
    {
        if (IsFull())
            throw new OverflowException();

        AddAndSort(value);
        IncrementRear();
        _count++;
    }


    public void AddAndSort(int value)
    {
        var end = DecrementUsingReminder(_rear);
        while (end != _front && _array[end] > value)
        {
            _array[GetIncrementUsingReminder(end)] = _array[end];
            end = DecrementUsingReminder(end);
        }

        if (_array[_front] > value)
        {
            _array[GetIncrementUsingReminder(_front)] = _array[_front];
            _array[_front] = value;
        }
        else
            _array[GetIncrementUsingReminder(end)] = value;

    }

    private int GetIncrementUsingReminder(int value)
    {
        return (value + 1) % _array.Length;
    }
    private int DecrementUsingReminder(int value)
    {
        return (value - 1 < 0) ? _array.Length - 1 : value - 1;
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


    public int Count()
    {
        return _count;
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
}