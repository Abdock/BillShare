using Domain.Base;

namespace Domain.Models;

public class Password : BaseEntity
{
    public Guid CustomerId { get; init; }
    public Customer Customer { get; init; } = default!;
    public string Salt { get; init; } = default!;
    public string EncryptedPassword { get; init; } = default!;

    public static bool operator ==(Password first, Password second)
    {
        return first.Salt == second.Salt && first.EncryptedPassword == second.EncryptedPassword;
    }

    public static bool operator !=(Password first, Password second)
    {
        return !(first == second);
    }
    
    private bool Equals(Password other)
    {
        return base.Equals(other) && CustomerId.Equals(other.CustomerId) && Salt == other.Salt && EncryptedPassword == other.EncryptedPassword;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        return obj.GetType() == GetType() && Equals((Password) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), CustomerId, Salt, EncryptedPassword);
    }
}