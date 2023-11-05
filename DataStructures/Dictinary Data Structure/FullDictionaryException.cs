
using System.Runtime.Serialization;

namespace Dictionary_Data_Structure;

[Serializable]
internal class FullDictionaryException : Exception
{
    public FullDictionaryException() : this("Dictionary is Full of items.")
    {
    }

    public FullDictionaryException(string? message) : base(message)
    {
    }

    public FullDictionaryException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected FullDictionaryException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}