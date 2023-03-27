namespace Contracts.Responses.ExpenseItems;

public record ExpenseItemActionResponse
{
    public required string SelectItemUrl { get; init; }
    public required string UnselectItemUrl { get; init; }
    public required string DeleteItemUrl { get; init; }
}