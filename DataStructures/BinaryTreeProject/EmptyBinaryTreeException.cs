
using System.Runtime.Serialization;

namespace BinaryTreeProject;

[Serializable]
internal class EmptyBinaryTreeException : Exception
{
    public EmptyBinaryTreeException() : this("Binary Tree is empty.")
    {
    }

    public EmptyBinaryTreeException(string? message) : base(message)
    {
    }

    public EmptyBinaryTreeException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected EmptyBinaryTreeException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}