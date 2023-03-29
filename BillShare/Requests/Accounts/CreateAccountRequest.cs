namespace BillShare.Requests.Accounts;

public record CreateAccountRequest
{
    public required string Name { get; init; }
    public required decimal Amount { get; init; }
    public string? ExternalId { get; init; }
}