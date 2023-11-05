
#nullable disable
using System.Runtime.Serialization;

[Serializable]
internal class GraphHasCycleException : Exception
{
    public GraphHasCycleException() : this("Graph has a Cycle.")
    {
    }

    public GraphHasCycleException(string message) : base(message)
    {
    }

    public GraphHasCycleException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected GraphHasCycleException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}