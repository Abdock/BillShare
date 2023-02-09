using System.Runtime.Serialization;

namespace Domain.Exceptions;

public class NotEnoughPermissionsException : Exception
{
    public NotEnoughPermissionsException()
    {
    }

    protected NotEnoughPermissionsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public NotEnoughPermissionsException(string? message) : base(message)
    {
    }

    public NotEnoughPermissionsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}