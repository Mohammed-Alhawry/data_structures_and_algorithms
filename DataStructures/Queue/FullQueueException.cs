using System.Runtime.Serialization;

namespace Queue;

[Serializable]
internal class FullQueueException : Exception
{
    public FullQueueException() : this("Queue is Full.")
    {
    }

    public FullQueueException(string? message) : base(message)
    {
    }

    public FullQueueException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected FullQueueException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}