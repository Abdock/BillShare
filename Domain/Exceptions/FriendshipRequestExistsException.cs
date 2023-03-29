using System.Runtime.Serialization;

namespace Domain.Exceptions;

public class FriendshipRequestExistsException : Exception
{
    public FriendshipRequestExistsException()
    {
    }

    protected FriendshipRequestExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public FriendshipRequestExistsException(string? message) : base(message)
    {
    }

    public FriendshipRequestExistsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}