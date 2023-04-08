namespace Contracts.DTOs.Customers;

public record ChangeCustomerAvatarDto
{
    public required Guid CustomerId { get; init; }
    public required string ImageData { get; init; }
    public required string Extension { get; init; }
}