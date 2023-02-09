using System.Runtime.Serialization;

namespace Domain.Exceptions;

public class UnknownUserException : Exception
{
    public UnknownUserException()
    {
    }

    protected UnknownUserException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public UnknownUserException(string? message) : base(message)
    {
    }

    public UnknownUserException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}