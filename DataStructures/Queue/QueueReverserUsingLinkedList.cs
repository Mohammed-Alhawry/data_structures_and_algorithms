namespace Queue;

public class QueueReverserUsingLinkedList
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
            _tail!.Next = new Node(value);
            _tail = _tail.Next;
        }

        Count++;
    }

    public void ReverseFromBeginning(int count)
    {
        if (count > Count)
            throw new ArgumentOutOfRangeException($"Queue elements are less than '{count}'");

        var stack = new Stack<int>();

        PushFirstElementsTo(stack, count);
        EnqueueElementsFrom(stack);
        MoveElementsFromFirstToEndExcept(count);
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



    private void MoveElementsFromFirstToEndExcept(int countOfFirstElements)
    {
        var restElementsCount = Count - countOfFirstElements;
        for (int i = 0; i < restElementsCount; i++)
            this.Enqueue(this.Dequeue());
    }

    private void EnqueueElementsFrom(Stack<int> stack)
    {
        var count = stack.Count;
        for (int i = 0; i < count; i++)
            this.Enqueue(stack.Pop());
    }

    private void PushFirstElementsTo(Stack<int> stack, int elementsCount)
    {
        for (int i = 0; i < elementsCount; i++)
            stack.Push(this.Dequeue());
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