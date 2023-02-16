namespace Contracts.Responses;

public class CustomerResponse
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
    public required string Name { get; init; }
}