using Domain.Enums;

namespace Contracts.Responses.ExpenseTypes;

public record ExpenseTypeResponse
{
    public required ExpenseTypeId Id { get; init; }
    public required string Name { get; init; }
}