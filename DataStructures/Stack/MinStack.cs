using System.Text;

namespace Stack;
public class MinStack
{
    private readonly int[] _stack;
    private readonly Stack<int> _minsStack;
    private int _count = 0;
    private int _min;
    public MinStack(int size)
    {
        _stack = new int[size];
        _minsStack = new Stack<int>(size);
    }

    public int Min()
    {
        if (_minsStack.TryPeek(out int min))
            return min;
            
        return -1;
    }
    public void Push(int value)
    {
        if (IsFull())
            throw new OutOfMemoryException("cannot add more, Stack is Full");

        if (IsEmpty())
            _minsStack.Push(value);

        else
        {
            if (value < _minsStack.Peek())
                _minsStack.Push(value);
        }

        _stack[_count++] = value;

    }

    public override string ToString()
    {
        var content = new StringBuilder();
        content.Append('[');
        for (var i = 0; i < _count; i++)
            content.Append($"{_stack[i]}, ");

        if (!IsEmpty())
            content.Remove(content.Length - 2, 2);

        content.Append(']');
        return content.ToString();
    }
    public int Pop()
    {
        if (IsEmpty())
            return -1;
        var value = _stack[--_count];
        if (_minsStack.Peek() == value)
            _minsStack.Pop();

        return value;
    }

    public int Peek()
    {
        if (IsEmpty())
            return -1;

        return _stack[_count - 1];
    }

    public bool IsEmpty()
    {
        return _count == 0;
    }

    public bool IsFull()
    {
        return _count == _stack.Length;
    }

}