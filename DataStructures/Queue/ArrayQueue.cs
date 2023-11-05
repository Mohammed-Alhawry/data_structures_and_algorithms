namespace Queue;
public class ArrayQueue
{
    private readonly int[] _array;
    private int _count;
    private int _front;
    private int _rear;
    public ArrayQueue(int size)
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
