namespace Stack;

public struct ValueWithType<T>
{
    public T Value { get; }
    public StackType? Type { get; }
    public ValueWithType(T value, StackType type)
    {
        Value = value;
        Type = type;
    }
}
