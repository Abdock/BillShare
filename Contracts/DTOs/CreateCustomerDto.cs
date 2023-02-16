namespace Contracts.DTOs;

public record CreateCustomerDto
{
    public required string Name { get; init; }
    public required string Email { get; init; }
}