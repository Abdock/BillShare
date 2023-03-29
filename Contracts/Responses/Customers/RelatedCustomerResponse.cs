namespace Contracts.Responses.Customers;

public record RelatedCustomerResponse
{
    public required Guid UserId { get; init; }
    public required string UserName { get; init; }
    public required string AvatarUrl { get; init; }
    public bool IsFriend { get; set; }
}