namespace BillShare.Requests.Accounts;

public record ChangeAccountAmountRequest
{
    public required decimal Amount { get; init; }
}