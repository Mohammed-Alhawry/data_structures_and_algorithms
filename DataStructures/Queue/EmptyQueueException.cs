using System.Runtime.Serialization;

namespace Queue;

[Serializable]
internal class EmptyQueueException : Exception
{
    public EmptyQueueException() : base("Queue is Empty.")
    {
        
    }

    public EmptyQueueException(string? message) : base(message)
    {
    }

    public EmptyQueueException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected EmptyQueueException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}