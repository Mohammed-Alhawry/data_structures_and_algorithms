using System.Runtime.Serialization;

namespace Queue;

[Serializable]
internal class EmptyStackException : Exception
{
    public EmptyStackException()
    {
    }

    public EmptyStackException(string? message) : base(message)
    {
    }

    public EmptyStackException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected EmptyStackException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}