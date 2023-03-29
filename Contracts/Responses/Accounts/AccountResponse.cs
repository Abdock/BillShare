namespace Contracts.Responses.Accounts;

public record AccountResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required decimal Amount { get; init; }
    public required string ExternalId { get; init; }
}