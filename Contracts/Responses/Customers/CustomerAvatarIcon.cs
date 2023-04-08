namespace Contracts.Responses.Customers;

public record CustomerAvatarIcon
{
    public required string AvatarUrl { get; init; }
}