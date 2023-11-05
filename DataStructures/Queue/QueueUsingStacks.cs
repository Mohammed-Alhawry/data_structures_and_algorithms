namespace Queue;
public class QueueUsingTwoStacks
{
    private readonly Stack<int> _stack1 = new();
    private readonly Stack<int> _stack2 = new();

    public void Enqueue(int value)
    {
        _stack1.Push(value);
    }

    public int Dequeue()
    {
        if (IsEmpty())
            throw new EmptyQueueException();

        if (Stack2IsEmpty())
            MoveStack1ToStack2();

        return _stack2.Pop();
    }

    private bool Stack2IsEmpty()
    {
        return _stack2.Count == 0;
    }

    public int Peek()
    {
        if (IsEmpty())
            throw new EmptyQueueException();

        if (_stack2.Count == 0)
            MoveStack1ToStack2();

        return _stack2.Pop();
    }

    private void MoveStack1ToStack2()
    {
        while (_stack1.Count != 0)
            _stack2.Push(_stack1.Pop());
    }

    public bool IsEmpty()
    {
        return _stack1.Count == 0 && _stack2.Count == 0;
    }


}
