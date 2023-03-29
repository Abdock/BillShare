using System.Runtime.Serialization;

namespace Domain.Exceptions;

public class InvalidRequestException : Exception
{
    public InvalidRequestException()
    {
    }

    protected InvalidRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public InvalidRequestException(string? message) : base(message)
    {
    }

    public InvalidRequestException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}