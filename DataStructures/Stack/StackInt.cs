using System.Text;
namespace Stack;

public class StackInt
{
    private readonly int[] _stack;
    private int _count = 0;

    public StackInt(int size)
    {
        _stack = new int[size];
    }

    public void Push(int value)
    {
        if (IsFull())
            throw new OutOfMemoryException("cannot add more, Stack is Full");
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

        return _stack[--_count];
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
