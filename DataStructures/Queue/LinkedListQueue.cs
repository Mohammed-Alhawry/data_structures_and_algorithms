namespace Queue;

public class LinkedListQueue
{
    private Node? _head;
    private Node? _tail;
    public int Count { get; private set; }
    public void Enqueue(int value)
    {
        var node = new Node(value);

        if (IsEmpty())
            _head = _tail = node;
        else
        {
            _tail.Next = new Node(value);
            _tail = _tail.Next;
        }

        Count++;
    }

    public void Reverse(int k)
    {
        if (k > Count)
            throw new ArgumentOutOfRangeException($"Queue elements are less than '{k}'");
        var stack = new Stack<int>();
        for (int i = 0; i < k; i++)
        {
            stack.Push(this.Dequeue());
        }

        for (int i = 0; i < k; i++)
        {
            this.Enqueue(stack.Pop());
        }

        var restElementsCount = Count - k;
        for (int i = 0; i < restElementsCount; i++)
        {
            this.Enqueue(this.Dequeue());
        }
    }
    public int Dequeue()
    {
        if (IsEmpty())
            throw new EmptyQueueException();

        var value = _head!.Value;

        if (_head == _tail)
            _head = _tail = null;
        else
        {
            var second = _head.Next;
            _head.Next = null;
            _head = second;
        }

        Count--;

        return value;
    }

    public int Peek()
    {
        if (IsEmpty())
            throw new EmptyQueueException();

        return _head!.Value;
    }

    public bool IsEmpty()
    {
        return _head == null;
    }






    private class Node
    {
        public Node? Next { get; set; }
        public int Value { get; }

        public Node(int value)
        {
            Value = value;
        }
    }
}