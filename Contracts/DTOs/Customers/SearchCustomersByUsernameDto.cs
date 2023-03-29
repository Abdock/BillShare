using Contracts.DTOs.General;

namespace Contracts.DTOs.Customers;

public record SearchCustomersByUsernameDto : GetPaginationDto
{
    public required Guid UserId { get; init; } // Request sender id
    public required string Username { get; init; }
}