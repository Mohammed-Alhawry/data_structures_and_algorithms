using System.Runtime.Serialization;

namespace HeapDataStructure;

[Serializable]
internal class EmptyHeapException : Exception
{
    public EmptyHeapException() : this("the Heap is empty.")
    {
    }

    public EmptyHeapException(string? message) : base(message)
    {
    }

    public EmptyHeapException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected EmptyHeapException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}