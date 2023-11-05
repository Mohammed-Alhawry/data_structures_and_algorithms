using System.Text;

namespace Stack;
public class TwoStacks
{
    private readonly ValueWithType<int>[] _stack;
    private int _count = 0;
    private int _index1 = -1;
    private int _index2 = -1;

    public TwoStacks(int size)
    {
        _stack = new ValueWithType<int>[size];
    }

    public void Push1(int value)
    {
        if (IsFull())
            throw new OutOfMemoryException("cannot add more, Stack is Full");
        
        _stack[_count++] = new ValueWithType<int>(value, StackType.Type1);
        _index1 = _count - 1;
    }

    public void Push2(int value){
        if (IsFull())
            throw new OutOfMemoryException("cannot add more, Stack is Full");
        
        _stack[_count++] = new ValueWithType<int>(value, StackType.Type2);
        _index2 = _count - 1;
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
    public int Pop1()
    {
        if (_index1 < 0)
            return -1;
        var value = _stack[_index1--].Value;
        LetIndex1GoToPrevious1();
        return value;
    }

    private void LetIndex1GoToPrevious1()
    {
        for (int i = _index1; i >= 0 ; i--)
        {
            if (_stack[i].Type is not null && _stack[i].Type == StackType.Type1)
            {
                _index1 = i;
                return;
            }
        }
        _index1 = -1;
    }
    private void LetIndex2GoToPrevious1()
    {
        for (int i = _index2; i >= 0 ; i--)
        {
            if (_stack[i].Type is not null && _stack[i].Type == StackType.Type2)
            {
                _index2 = i;
                return;
            }
        }
        _index2 = -1;
    }

public int Pop2(){
        if (_index2 < 0)
            return -1;
        var value = _stack[_index2--].Value;
        LetIndex2GoToPrevious1();
        return value;

}

    public int Peek1()
    {
        if (IsEmpty())
            return -1;

        return _stack[_count - 1].Value;
    }

    public bool IsEmpty1()
    {
        return IsEmpty();
    }
    private bool IsEmpty()
    {
        return _count == 0;
    }

    public bool IsFull1()
    {
        return IsFull();
    }
    private bool IsFull()
    {
        return _count == _stack.Length;
    }
}
